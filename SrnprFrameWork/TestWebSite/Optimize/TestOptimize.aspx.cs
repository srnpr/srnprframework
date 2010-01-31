using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Optimize_TestOptimize : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}

/*
public class Parser
{
    // TokenInformation flags : currentFlaggedToken stores them together
    // with token type
    const int
        CLEAR_TI_MASK  = 0xFFFF,   // mask to clear token information bits
        TI_AFTER_EOL   = 1 << 16,  // first token of the source line
        TI_CHECK_LABEL = 1 << 17;  // indicates to check for label

    CompilerEnvirons compilerEnv;
    private ErrorReporter errorReporter;
    private String sourceURI;
    boolean calledByCompileFunction;

    private TokenStream ts;
    private int currentFlaggedToken;
    private int syntaxErrorCount;

    private IRFactory nf;

    private int nestingOfFunction;

    private Decompiler decompiler;
    private String encodedSource;

// The following are per function variables and should be saved/restored
// during function parsing.
// XXX Move to separated class?
    ScriptOrFnNode currentScriptOrFn;
    private int nestingOfWith;
    private Hashtable labelSet; // map of label names into nodes
    private ObjArray loopSet;
    private ObjArray loopAndSwitchSet;
    private boolean hasReturnValue;
    private int functionEndFlags;
// end of per function variables

    // Exception to unwind
    private static class ParserException extends RuntimeException
    {
        static final long serialVersionUID = 5882582646773765630L;
    }

    public Parser(CompilerEnvirons compilerEnv, ErrorReporter errorReporter)
    {
        this.compilerEnv = compilerEnv;
        this.errorReporter = errorReporter;
    }

    protected Decompiler createDecompiler(CompilerEnvirons compilerEnv)
    {
        return new Decompiler();
    }

    void addStrictWarning(String messageId, String messageArg)
    {
        if (compilerEnv.isStrictMode())
            addWarning(messageId, messageArg);
    }

    void addWarning(String messageId, String messageArg)
    {
        String message = ScriptRuntime.getMessage1(messageId, messageArg);
        if (compilerEnv.reportWarningAsError()) {
            ++syntaxErrorCount;
            errorReporter.error(message, sourceURI, ts.getLineno(),
                                ts.getLine(), ts.getOffset());
        } else
            errorReporter.warning(message, sourceURI, ts.getLineno(),
                                  ts.getLine(), ts.getOffset());
    }

    void addError(String messageId)
    {
        ++syntaxErrorCount;
        String message = ScriptRuntime.getMessage0(messageId);
        errorReporter.error(message, sourceURI, ts.getLineno(),
                            ts.getLine(), ts.getOffset());
    }

    void addError(String messageId, String messageArg)
    {
        ++syntaxErrorCount;
        String message = ScriptRuntime.getMessage1(messageId, messageArg);
        errorReporter.error(message, sourceURI, ts.getLineno(),
                            ts.getLine(), ts.getOffset());
    }

    RuntimeException reportError(String messageId)
    {
        addError(messageId);

        // Throw a ParserException exception to unwind the recursive descent
        // parse.
        throw new ParserException();
    }

    private int peekToken()
        throws IOException
    {
        int tt = currentFlaggedToken;
        if (tt == Token.EOF) {

            while ((tt = ts.getToken()) == Token.CONDCOMMENT || tt == Token.KEEPCOMMENT) {
                if (tt == Token.CONDCOMMENT) {
                    // Support for JScript conditional comments 
                    decompiler.addJScriptConditionalComment(ts.getString());
                } else {
                    // Support for preserved comments 
                    decompiler.addPreservedComment(ts.getString());
                }
            }

            if (tt == Token.EOL) {
                do {
                    tt = ts.getToken();

                    if (tt == Token.CONDCOMMENT) {
                        // Support for JScript conditional comments 
                        decompiler.addJScriptConditionalComment(ts.getString());
                    } else if (tt == Token.KEEPCOMMENT) {
                        // Support for preserved comments 
                        decompiler.addPreservedComment(ts.getString());
                    }

                } while (tt == Token.EOL || tt == Token.CONDCOMMENT || tt == Token.KEEPCOMMENT);
                tt |= TI_AFTER_EOL;
            }
            currentFlaggedToken = tt;
        }
        return tt & CLEAR_TI_MASK;
    }

    private int peekFlaggedToken()
        throws IOException
    {
        peekToken();
        return currentFlaggedToken;
    }

    private void consumeToken()
    {
        currentFlaggedToken = Token.EOF;
    }

    private int nextToken()
        throws IOException
    {
        int tt = peekToken();
        consumeToken();
        return tt;
    }

    private int nextFlaggedToken()
        throws IOException
    {
        peekToken();
        int ttFlagged = currentFlaggedToken;
        consumeToken();
        return ttFlagged;
    }

    private boolean matchToken(int toMatch)
        throws IOException
    {
        int tt = peekToken();
        if (tt != toMatch) {
            return false;
        }
        consumeToken();
        return true;
    }

    private int peekTokenOrEOL()
        throws IOException
    {
        int tt = peekToken();
        // Check for last peeked token flags
        if ((currentFlaggedToken & TI_AFTER_EOL) != 0) {
            tt = Token.EOL;
        }
        return tt;
    }

    private void setCheckForLabel()
    {
        if ((currentFlaggedToken & CLEAR_TI_MASK) != Token.NAME)
            throw Kit.codeBug();
        currentFlaggedToken |= TI_CHECK_LABEL;
    }

    private void mustMatchToken(int toMatch, String messageId)
        throws IOException, ParserException
    {
        if (!matchToken(toMatch)) {
            reportError(messageId);
        }
    }

    private void mustHaveXML()
    {
        if (!compilerEnv.isXmlAvailable()) {
            reportError("msg.XML.not.available");
        }
    }

    public String getEncodedSource()
    {
        return encodedSource;
    }

    public boolean eof()
    {
        return ts.eof();
    }

    boolean insideFunction()
    {
        return nestingOfFunction != 0;
    }

    private Node enterLoop(Node loopLabel)
    {
        Node loop = nf.createLoopNode(loopLabel, ts.getLineno());
        if (loopSet == null) {
            loopSet = new ObjArray();
            if (loopAndSwitchSet == null) {
                loopAndSwitchSet = new ObjArray();
            }
        }
        loopSet.push(loop);
        loopAndSwitchSet.push(loop);
        return loop;
    }

    private void exitLoop()
    {
        loopSet.pop();
        loopAndSwitchSet.pop();
    }

    private Node enterSwitch(Node switchSelector, int lineno)
    {
        Node switchNode = nf.createSwitch(switchSelector, lineno);
        if (loopAndSwitchSet == null) {
            loopAndSwitchSet = new ObjArray();
        }
        loopAndSwitchSet.push(switchNode);
        return switchNode;
    }

    private void exitSwitch()
    {
        loopAndSwitchSet.pop();
    }

   
     //* Build a parse tree from the given sourceString.
     //*
     //* @return an Object representing the parsed
     //* program.  If the parse fails, null will be returned.  (The
     //* parse failure will result in a call to the ErrorReporter from
     //* CompilerEnvirons.)
    
    public ScriptOrFnNode parse(String sourceString,
                                String sourceURI, int lineno)
    {
        this.sourceURI = sourceURI;
        this.ts = new TokenStream(this, null, sourceString, lineno);
        try {
            return parse();
        } catch (IOException ex) {
            // Should never happen
            throw new IllegalStateException();
        }
    }

   
     //* Build a parse tree from the given sourceString.
     //*
     //* @return an Object representing the parsed
     //* program.  If the parse fails, null will be returned.  (The
     //* parse failure will result in a call to the ErrorReporter from
     //* CompilerEnvirons.)
    
    public ScriptOrFnNode parse(Reader sourceReader,
                                String sourceURI, int lineno)
        throws IOException
    {
        this.sourceURI = sourceURI;
        this.ts = new TokenStream(this, sourceReader, null, lineno);
        return parse();
    }

    private ScriptOrFnNode parse()
        throws IOException
    {
        this.decompiler = createDecompiler(compilerEnv);
        this.nf = new IRFactory(this);
        currentScriptOrFn = nf.createScript();
        int sourceStartOffset = decompiler.getCurrentOffset();
        this.encodedSource = null;
        decompiler.addToken(Token.SCRIPT);

        this.currentFlaggedToken = Token.EOF;
        this.syntaxErrorCount = 0;

        int baseLineno = ts.getLineno();  // line number where source starts

        //so we have something to add nodes to until
        // * we've collected all the source 
        Node pn = nf.createLeaf(Token.BLOCK);

        try {
            for (;;) {
                int tt = peekToken();

                if (tt <= Token.EOF) {
                    break;
                }

                Node n;
                if (tt == Token.FUNCTION) {
                    consumeToken();
                    try {
                        n = function(calledByCompileFunction
                                     ? FunctionNode.FUNCTION_EXPRESSION
                                     : FunctionNode.FUNCTION_STATEMENT);
                    } catch (ParserException e) {
                        break;
                    }
                } else {
                    n = statement();
                }
                nf.addChildToBack(pn, n);
            }
        } catch (StackOverflowError ex) {
            String msg = ScriptRuntime.getMessage0(
                "msg.too.deep.parser.recursion");
            throw Context.reportRuntimeError(msg, sourceURI,
                                             ts.getLineno(), null, 0);
        }

        if (this.syntaxErrorCount != 0) {
            String msg = String.valueOf(this.syntaxErrorCount);
            msg = ScriptRuntime.getMessage1("msg.got.syntax.errors", msg);
            throw errorReporter.runtimeError(msg, sourceURI, baseLineno,
                                             null, 0);
        }

        currentScriptOrFn.setSourceName(sourceURI);
        currentScriptOrFn.setBaseLineno(baseLineno);
        currentScriptOrFn.setEndLineno(ts.getLineno());

        int sourceEndOffset = decompiler.getCurrentOffset();
        currentScriptOrFn.setEncodedSourceBounds(sourceStartOffset,
                                                 sourceEndOffset);

        nf.initScript(currentScriptOrFn, pn);

        if (compilerEnv.isGeneratingSource()) {
            encodedSource = decompiler.getEncodedSource();
        }
        this.decompiler = null; // It helps GC

        return currentScriptOrFn;
    }

   
     //* The C version of this function takes an argument list,
     //* which doesn't seem to be needed for tree generation...
     //* it'd only be useful for checking argument hiding, which
     //* I'm not doing anyway...
     
    private Node parseFunctionBody()
        throws IOException
    {
        ++nestingOfFunction;
        Node pn = nf.createBlock(ts.getLineno());
        try {
            bodyLoop: for (;;) {
                Node n;
                int tt = peekToken();
                switch (tt) {
                  case Token.ERROR:
                  case Token.EOF:
                  case Token.RC:
                    break bodyLoop;

                  case Token.FUNCTION:
                    consumeToken();
                    n = function(FunctionNode.FUNCTION_STATEMENT);
                    break;
                  default:
                    n = statement();
                    break;
                }
                nf.addChildToBack(pn, n);
            }
        } catch (ParserException e) {
            // Ignore it
        } finally {
            --nestingOfFunction;
        }

        return pn;
    }

    private Node function(int functionType)
        throws IOException, ParserException
    {
        int syntheticType = functionType;
        int baseLineno = ts.getLineno();  // line number where source starts

        int functionSourceStart = decompiler.markFunctionStart(functionType);
        String name;
        Node memberExprNode = null;
        if (matchToken(Token.NAME)) {
            name = ts.getString();
            decompiler.addName(name);
            if (!matchToken(Token.LP)) {
                if (compilerEnv.isAllowMemberExprAsFunctionName()) {
                    // Extension to ECMA: if 'function <name>' does not follow
                    // by '(', assume <name> starts memberExpr
                    Node memberExprHead = nf.createName(name);
                    name = "";
                    memberExprNode = memberExprTail(false, memberExprHead);
                }
                mustMatchToken(Token.LP, "msg.no.paren.parms");
            }
        } else if (matchToken(Token.LP)) {
            // Anonymous function
            name = "";
        } else {
            name = "";
            if (compilerEnv.isAllowMemberExprAsFunctionName()) {
                // Note that memberExpr can not start with '(' like
                // in function (1+2).toString(), because 'function (' already
                // processed as anonymous function
                memberExprNode = memberExpr(false);
            }
            mustMatchToken(Token.LP, "msg.no.paren.parms");
        }

        if (memberExprNode != null) {
            syntheticType = FunctionNode.FUNCTION_EXPRESSION;
        }

        boolean nested = insideFunction();

        FunctionNode fnNode = nf.createFunction(name);
        if (nested || nestingOfWith > 0) {
            // 1. Nested functions are not affected by the dynamic scope flag
            // as dynamic scope is already a parent of their scope.
            // 2. Functions defined under the with statement also immune to
            // this setup, in which case dynamic scope is ignored in favor
            // of with object.
            fnNode.itsIgnoreDynamicScope = true;
        }

        int functionIndex = currentScriptOrFn.addFunction(fnNode);

        int functionSourceEnd;

        ScriptOrFnNode savedScriptOrFn = currentScriptOrFn;
        currentScriptOrFn = fnNode;
        int savedNestingOfWith = nestingOfWith;
        nestingOfWith = 0;
        Hashtable savedLabelSet = labelSet;
        labelSet = null;
        ObjArray savedLoopSet = loopSet;
        loopSet = null;
        ObjArray savedLoopAndSwitchSet = loopAndSwitchSet;
        loopAndSwitchSet = null;
        boolean savedHasReturnValue = hasReturnValue;
        int savedFunctionEndFlags = functionEndFlags;

        Node body;
        try {
            decompiler.addToken(Token.LP);
            if (!matchToken(Token.RP)) {
                boolean first = true;
                do {
                    if (!first)
                        decompiler.addToken(Token.COMMA);
                    first = false;
                    mustMatchToken(Token.NAME, "msg.no.parm");
                    String s = ts.getString();
                    if (fnNode.hasParamOrVar(s)) {
                        addWarning("msg.dup.parms", s);
                    }
                    fnNode.addParam(s);
                    decompiler.addName(s);
                } while (matchToken(Token.COMMA));

                mustMatchToken(Token.RP, "msg.no.paren.after.parms");
            }
            decompiler.addToken(Token.RP);

            mustMatchToken(Token.LC, "msg.no.brace.body");
            decompiler.addEOL(Token.LC);
            body = parseFunctionBody();
            mustMatchToken(Token.RC, "msg.no.brace.after.body");

            if (compilerEnv.isStrictMode() && !body.hasConsistentReturnUsage())
            {
              String msg = name.length() > 0 ? "msg.no.return.value"
                                             : "msg.anon.no.return.value";
              addStrictWarning(msg, name);
            }

            decompiler.addToken(Token.RC);
            functionSourceEnd = decompiler.markFunctionEnd(functionSourceStart);
            if (functionType != FunctionNode.FUNCTION_EXPRESSION) {
                // Add EOL only if function is not part of expression
                // since it gets SEMI + EOL from Statement in that case
                decompiler.addToken(Token.EOL);
            }
        }
        finally {
            hasReturnValue = savedHasReturnValue;
            functionEndFlags = savedFunctionEndFlags;
            loopAndSwitchSet = savedLoopAndSwitchSet;
            loopSet = savedLoopSet;
            labelSet = savedLabelSet;
            nestingOfWith = savedNestingOfWith;
            currentScriptOrFn = savedScriptOrFn;
        }

        fnNode.setEncodedSourceBounds(functionSourceStart, functionSourceEnd);
        fnNode.setSourceName(sourceURI);
        fnNode.setBaseLineno(baseLineno);
        fnNode.setEndLineno(ts.getLineno());

        if (name != null) {
          int index = currentScriptOrFn.getParamOrVarIndex(name);
          if (index >= 0 && index < currentScriptOrFn.getParamCount())
            addStrictWarning("msg.var.hides.arg", name);
        }

        Node pn = nf.initFunction(fnNode, functionIndex, body, syntheticType);
        if (memberExprNode != null) {
            pn = nf.createAssignment(Token.ASSIGN, memberExprNode, pn);
            if (functionType != FunctionNode.FUNCTION_EXPRESSION) {
                // XXX check JScript behavior: should it be createExprStatement?
                pn = nf.createExprStatementNoReturn(pn, baseLineno);
            }
        }
        return pn;
    }

    private Node statements()
        throws IOException
    {
        Node pn = nf.createBlock(ts.getLineno());

        int tt;
        while((tt = peekToken()) > Token.EOF && tt != Token.RC) {
            nf.addChildToBack(pn, statement());
        }

        return pn;
    }

    private Node condition()
        throws IOException, ParserException
    {
        mustMatchToken(Token.LP, "msg.no.paren.cond");
        decompiler.addToken(Token.LP);
        Node pn = expr(false);
        mustMatchToken(Token.RP, "msg.no.paren.after.cond");
        decompiler.addToken(Token.RP);

        // Report strict warning on code like "if (a = 7) ...". Suppress the
        // warning if the condition is parenthesized, like "if ((a = 7)) ...".
        if (pn.getProp(Node.PARENTHESIZED_PROP) == null &&
            (pn.getType() == Token.SETNAME || pn.getType() == Token.SETPROP ||
             pn.getType() == Token.SETELEM))
        {
            addStrictWarning("msg.equal.as.assign", "");
        }
        return pn;
    }

    // match a NAME; return null if no match.
    private Node matchJumpLabelName()
        throws IOException, ParserException
    {
        Node label = null;

        int tt = peekTokenOrEOL();
        if (tt == Token.NAME) {
            consumeToken();
            String name = ts.getString();
            decompiler.addName(name);
            if (labelSet != null) {
                label = (Node)labelSet.get(name);
            }
            if (label == null) {
                reportError("msg.undef.label");
            }
        }

        return label;
    }

    private Node statement()
        throws IOException
    {
        try {
            Node pn = statementHelper(null);
            if (pn != null) {
                if (compilerEnv.isStrictMode() && !pn.hasSideEffects())
                    addStrictWarning("msg.no.side.effects", "");
                return pn;
            }
        } catch (ParserException e) { }

        // skip to end of statement
        int lineno = ts.getLineno();
        guessingStatementEnd: for (;;) {
            int tt = peekTokenOrEOL();
            consumeToken();
            switch (tt) {
              case Token.ERROR:
              case Token.EOF:
              case Token.EOL:
              case Token.SEMI:
                break guessingStatementEnd;
            }
        }
        return nf.createExprStatement(nf.createName("error"), lineno);
    }

   
     //* Whether the "catch (e: e instanceof Exception) { ... }" syntax
     //* is implemented.
     

    private Node statementHelper(Node statementLabel)
        throws IOException, ParserException
    {
        Node pn = null;

        int tt;

        tt = peekToken();

        switch(tt) {
          case Token.IF: {
            consumeToken();

            decompiler.addToken(Token.IF);
            int lineno = ts.getLineno();
            Node cond = condition();
            decompiler.addEOL(Token.LC);
            Node ifTrue = statement();
            Node ifFalse = null;
            if (matchToken(Token.ELSE)) {
                decompiler.addToken(Token.RC);
                decompiler.addToken(Token.ELSE);
                decompiler.addEOL(Token.LC);
                ifFalse = statement();
            }
            decompiler.addEOL(Token.RC);
            pn = nf.createIf(cond, ifTrue, ifFalse, lineno);
            return pn;
          }

          case Token.SWITCH: {
            consumeToken();

            decompiler.addToken(Token.SWITCH);
            int lineno = ts.getLineno();
            mustMatchToken(Token.LP, "msg.no.paren.switch");
            decompiler.addToken(Token.LP);
            pn = enterSwitch(expr(false), lineno);
            try {
                mustMatchToken(Token.RP, "msg.no.paren.after.switch");
                decompiler.addToken(Token.RP);
                mustMatchToken(Token.LC, "msg.no.brace.switch");
                decompiler.addEOL(Token.LC);

                boolean hasDefault = false;
                switchLoop: for (;;) {
                    tt = nextToken();
                    Node caseExpression;
                    switch (tt) {
                      case Token.RC:
                        break switchLoop;

                      case Token.CASE:
                        decompiler.addToken(Token.CASE);
                        caseExpression = expr(false);
                        mustMatchToken(Token.COLON, "msg.no.colon.case");
                        decompiler.addEOL(Token.COLON);
                        break;

                      case Token.DEFAULT:
                        if (hasDefault) {
                            reportError("msg.double.switch.default");
                        }
                        decompiler.addToken(Token.DEFAULT);
                        hasDefault = true;
                        caseExpression = null;
                        mustMatchToken(Token.COLON, "msg.no.colon.case");
                        decompiler.addEOL(Token.COLON);
                        break;

                      default:
                        reportError("msg.bad.switch");
                        break switchLoop;
                    }

                    Node block = nf.createLeaf(Token.BLOCK);
                    while ((tt = peekToken()) != Token.RC
                           && tt != Token.CASE
                           && tt != Token.DEFAULT
                           && tt != Token.EOF)
                    {
                        nf.addChildToBack(block, statement());
                    }

                    // caseExpression == null => add default lable
                    nf.addSwitchCase(pn, caseExpression, block);
                }
                decompiler.addEOL(Token.RC);
                nf.closeSwitch(pn);
            } finally {
                exitSwitch();
            }
            return pn;
          }

          case Token.WHILE: {
            consumeToken();
            decompiler.addToken(Token.WHILE);

            Node loop = enterLoop(statementLabel);
            try {
                Node cond = condition();
                decompiler.addEOL(Token.LC);
                Node body = statement();
                decompiler.addEOL(Token.RC);
                pn = nf.createWhile(loop, cond, body);
            } finally {
                exitLoop();
            }
            return pn;
          }

          case Token.DO: {
            consumeToken();
            decompiler.addToken(Token.DO);
            decompiler.addEOL(Token.LC);

            Node loop = enterLoop(statementLabel);
            try {
                Node body = statement();
                decompiler.addToken(Token.RC);
                mustMatchToken(Token.WHILE, "msg.no.while.do");
                decompiler.addToken(Token.WHILE);
                Node cond = condition();
                pn = nf.createDoWhile(loop, body, cond);
            } finally {
                exitLoop();
            }
            // Always auto-insert semicon to follow SpiderMonkey:
            // It is required by EMAScript but is ignored by the rest of
            // world, see bug 238945
            matchToken(Token.SEMI);
            decompiler.addEOL(Token.SEMI);
            return pn;
          }

          case Token.FOR: {
            consumeToken();
            boolean isForEach = false;
            decompiler.addToken(Token.FOR);

            Node loop = enterLoop(statementLabel);
            try {

                Node init;  // Node init is also foo in 'foo in Object'
                Node cond;  // Node cond is also object in 'foo in Object'
                Node incr = null; // to kill warning
                Node body;

                // See if this is a for each () instead of just a for ()
                if (matchToken(Token.NAME)) {
                    decompiler.addName(ts.getString());
                    if (ts.getString().equals("each")) {
                        isForEach = true;
                    } else {
                        reportError("msg.no.paren.for");
                    }
                }

                mustMatchToken(Token.LP, "msg.no.paren.for");
                decompiler.addToken(Token.LP);
                tt = peekToken();
                if (tt == Token.SEMI) {
                    init = nf.createLeaf(Token.EMPTY);
                } else {
                    if (tt == Token.VAR) {
                        // set init to a var list or initial
                        consumeToken();    // consume the 'var' token
                        init = variables(Token.FOR);
                    }
                    else {
                        init = expr(true);
                    }
                }

                if (matchToken(Token.IN)) {
                    decompiler.addToken(Token.IN);
                    // 'cond' is the object over which we're iterating
                    cond = expr(false);
                } else {  // ordinary for loop
                    mustMatchToken(Token.SEMI, "msg.no.semi.for");
                    decompiler.addToken(Token.SEMI);
                    if (peekToken() == Token.SEMI) {
                        // no loop condition
                        cond = nf.createLeaf(Token.EMPTY);
                    } else {
                        cond = expr(false);
                    }

                    mustMatchToken(Token.SEMI, "msg.no.semi.for.cond");
                    decompiler.addToken(Token.SEMI);
                    if (peekToken() == Token.RP) {
                        incr = nf.createLeaf(Token.EMPTY);
                    } else {
                        incr = expr(false);
                    }
                }

                mustMatchToken(Token.RP, "msg.no.paren.for.ctrl");
                decompiler.addToken(Token.RP);
                decompiler.addEOL(Token.LC);
                body = statement();
                decompiler.addEOL(Token.RC);

                if (incr == null) {
                    // cond could be null if 'in obj' got eaten
                    // by the init node.
                    pn = nf.createForIn(loop, init, cond, body, isForEach);
                } else {
                    pn = nf.createFor(loop, init, cond, incr, body);
                }
            } finally {
                exitLoop();
            }
            return pn;
          }

          case Token.TRY: {
            consumeToken();
            int lineno = ts.getLineno();

            Node tryblock;
            Node catchblocks = null;
            Node finallyblock = null;

            decompiler.addToken(Token.TRY);
            decompiler.addEOL(Token.LC);
            tryblock = statement();
            decompiler.addEOL(Token.RC);

            catchblocks = nf.createLeaf(Token.BLOCK);

            boolean sawDefaultCatch = false;
            int peek = peekToken();
            if (peek == Token.CATCH) {
                while (matchToken(Token.CATCH)) {
                    if (sawDefaultCatch) {
                        reportError("msg.catch.unreachable");
                    }
                    decompiler.addToken(Token.CATCH);
                    mustMatchToken(Token.LP, "msg.no.paren.catch");
                    decompiler.addToken(Token.LP);

                    mustMatchToken(Token.NAME, "msg.bad.catchcond");
                    String varName = ts.getString();
                    decompiler.addName(varName);

                    Node catchCond = null;
                    if (matchToken(Token.IF)) {
                        decompiler.addToken(Token.IF);
                        catchCond = expr(false);
                    } else {
                        sawDefaultCatch = true;
                    }

                    mustMatchToken(Token.RP, "msg.bad.catchcond");
                    decompiler.addToken(Token.RP);
                    mustMatchToken(Token.LC, "msg.no.brace.catchblock");
                    decompiler.addEOL(Token.LC);

                    nf.addChildToBack(catchblocks,
                        nf.createCatch(varName, catchCond,
                                       statements(),
                                       ts.getLineno()));

                    mustMatchToken(Token.RC, "msg.no.brace.after.body");
                    decompiler.addEOL(Token.RC);
                }
            } else if (peek != Token.FINALLY) {
                mustMatchToken(Token.FINALLY, "msg.try.no.catchfinally");
            }

            if (matchToken(Token.FINALLY)) {
                decompiler.addToken(Token.FINALLY);
                decompiler.addEOL(Token.LC);
                finallyblock = statement();
                decompiler.addEOL(Token.RC);
            }

            pn = nf.createTryCatchFinally(tryblock, catchblocks,
                                          finallyblock, lineno);

            return pn;
          }

          case Token.THROW: {
            consumeToken();
            if (peekTokenOrEOL() == Token.EOL) {
                // ECMAScript does not allow new lines before throw expression,
                // see bug 256617
                reportError("msg.bad.throw.eol");
            }

            int lineno = ts.getLineno();
            decompiler.addToken(Token.THROW);
            pn = nf.createThrow(expr(false), lineno);
            break;
          }

          case Token.BREAK: {
            consumeToken();
            int lineno = ts.getLineno();

            decompiler.addToken(Token.BREAK);

            // matchJumpLabelName only matches if there is one
            Node breakStatement = matchJumpLabelName();
            if (breakStatement == null) {
                if (loopAndSwitchSet == null || loopAndSwitchSet.size() == 0) {
                    reportError("msg.bad.break");
                    return null;
                }
                breakStatement = (Node)loopAndSwitchSet.peek();
            }
            pn = nf.createBreak(breakStatement, lineno);
            break;
          }

          case Token.CONTINUE: {
            consumeToken();
            int lineno = ts.getLineno();

            decompiler.addToken(Token.CONTINUE);

            Node loop;
            // matchJumpLabelName only matches if there is one
            Node label = matchJumpLabelName();
            if (label == null) {
                if (loopSet == null || loopSet.size() == 0) {
                    reportError("msg.continue.outside");
                    return null;
                }
                loop = (Node)loopSet.peek();
            } else {
                loop = nf.getLabelLoop(label);
                if (loop == null) {
                    reportError("msg.continue.nonloop");
                    return null;
                }
            }
            pn = nf.createContinue(loop, lineno);
            break;
          }

          case Token.WITH: {
            consumeToken();

            decompiler.addToken(Token.WITH);
            int lineno = ts.getLineno();
            mustMatchToken(Token.LP, "msg.no.paren.with");
            decompiler.addToken(Token.LP);
            Node obj = expr(false);
            mustMatchToken(Token.RP, "msg.no.paren.after.with");
            decompiler.addToken(Token.RP);
            decompiler.addEOL(Token.LC);

            ++nestingOfWith;
            Node body;
            try {
                body = statement();
            } finally {
                --nestingOfWith;
            }

            decompiler.addEOL(Token.RC);

            pn = nf.createWith(obj, body, lineno);
            return pn;
          }

          case Token.CONST:
          case Token.VAR: {
            consumeToken();
            pn = variables(tt);
            break;
          }

          case Token.RETURN: {
            if (!insideFunction()) {
                reportError("msg.bad.return");
            }
            consumeToken();
            decompiler.addToken(Token.RETURN);
            int lineno = ts.getLineno();

            Node retExpr;
            // This is ugly, but we don't want to require a semicolon. 
            tt = peekTokenOrEOL();
            switch (tt) {
              case Token.SEMI:
              case Token.RC:
              case Token.EOF:
              case Token.EOL:
              case Token.ERROR:
                retExpr = null;
                break;
              default:
                retExpr = expr(false);
                hasReturnValue = true;
            }
            pn = nf.createReturn(retExpr, lineno);

            // see if we need a strict mode warning
            if (retExpr == null) {
                if (functionEndFlags == Node.END_RETURNS_VALUE)
                    addStrictWarning("msg.return.inconsistent", "");

                functionEndFlags |= Node.END_RETURNS;
            } else {
                if (functionEndFlags == Node.END_RETURNS)
                    addStrictWarning("msg.return.inconsistent", "");

                functionEndFlags |= Node.END_RETURNS_VALUE;
            }

            break;
          }

          case Token.LC:
            consumeToken();
            if (statementLabel != null) {
                decompiler.addToken(Token.LC);
            }
            pn = statements();
            mustMatchToken(Token.RC, "msg.no.brace.block");
            if (statementLabel != null) {
                decompiler.addEOL(Token.RC);
            }
            return pn;

          case Token.ERROR:
            // Fall thru, to have a node for error recovery to work on
          case Token.SEMI:
            consumeToken();
            pn = nf.createLeaf(Token.EMPTY);
            return pn;

          case Token.FUNCTION: {
            consumeToken();
            pn = function(FunctionNode.FUNCTION_EXPRESSION_STATEMENT);
            return pn;
          }

          case Token.DEFAULT :
            consumeToken();
            mustHaveXML();

            decompiler.addToken(Token.DEFAULT);
            int nsLine = ts.getLineno();

            if (!(matchToken(Token.NAME)
                  && ts.getString().equals("xml")))
            {
                reportError("msg.bad.namespace");
            }
            decompiler.addName(" xml");

            if (!(matchToken(Token.NAME)
                  && ts.getString().equals("namespace")))
            {
                reportError("msg.bad.namespace");
            }
            decompiler.addName(" namespace");

            if (!matchToken(Token.ASSIGN)) {
                reportError("msg.bad.namespace");
            }
            decompiler.addToken(Token.ASSIGN);

            Node expr = expr(false);
            pn = nf.createDefaultNamespace(expr, nsLine);
            break;

          case Token.NAME: {
            int lineno = ts.getLineno();
            String name = ts.getString();
            setCheckForLabel();
            pn = expr(false);
            if (pn.getType() != Token.LABEL) {
                pn = nf.createExprStatement(pn, lineno);
            } else {
                // Parsed the label: push back token should be
                // colon that primaryExpr left untouched.
                if (peekToken() != Token.COLON) Kit.codeBug();
                consumeToken();
                // depend on decompiling lookahead to guess that that
                // last name was a label.
                decompiler.addName(name);
                decompiler.addEOL(Token.COLON);

                if (labelSet == null) {
                    labelSet = new Hashtable();
                } else if (labelSet.containsKey(name)) {
                    reportError("msg.dup.label");
                }

                boolean firstLabel;
                if (statementLabel == null) {
                    firstLabel = true;
                    statementLabel = pn;
                } else {
                    // Discard multiple label nodes and use only
                    // the first: it allows to simplify IRFactory
                    firstLabel = false;
                }
                labelSet.put(name, statementLabel);
                try {
                    pn = statementHelper(statementLabel);
                } finally {
                    labelSet.remove(name);
                }
                if (firstLabel) {
                    pn = nf.createLabeledStatement(statementLabel, pn);
                }
                return pn;
            }
            break;
          }

          default: {
            int lineno = ts.getLineno();
            pn = expr(false);
            pn = nf.createExprStatement(pn, lineno);
            break;
          }
        }

        int ttFlagged = peekFlaggedToken();
        switch (ttFlagged & CLEAR_TI_MASK) {
          case Token.SEMI:
            // Consume ';' as a part of expression
            consumeToken();
            break;
          case Token.ERROR:
          case Token.EOF:
          case Token.RC:
            // Autoinsert ;
            break;
          default:
            if ((ttFlagged & TI_AFTER_EOL) == 0) {
                // Report error if no EOL or autoinsert ; otherwise
                reportError("msg.no.semi.stmt");
            }
            break;
        }
        decompiler.addEOL(Token.SEMI);

        return pn;
    }

  
     //* Parse a 'var' or 'const' statement, or a 'var' init list in a for
     //* statement.
     //* @param context A token value: either VAR, CONST or FOR depending on
     //* context.
     //* @return The parsed statement
     //* @throws IOException
     //* @throws ParserException
    
    private Node variables(int context)
        throws IOException, ParserException
    {
        Node pn;
        boolean first = true;

        if (context == Token.CONST){
            pn = nf.createVariables(Token.CONST, ts.getLineno());
            decompiler.addToken(Token.CONST);
        } else {
            pn = nf.createVariables(Token.VAR, ts.getLineno());
            decompiler.addToken(Token.VAR);
        }

        for (;;) {
            Node name;
            Node init;
            mustMatchToken(Token.NAME, "msg.bad.var");
            String s = ts.getString();

            if (!first)
                decompiler.addToken(Token.COMMA);
            first = false;

            decompiler.addName(s);

            if (context == Token.CONST) {
                if (!currentScriptOrFn.addConst(s)) {
                    // We know it's already defined, since addConst passes if
                    // it's not defined at all.  The addVar call just confirms
                    // what it is.
                    if (currentScriptOrFn.addVar(s) != ScriptOrFnNode.DUPLICATE_CONST)
                        addError("msg.var.redecl", s);
                    else
                        addError("msg.const.redecl", s);
                }
            } else {
                int dupState = currentScriptOrFn.addVar(s);
                if (dupState == ScriptOrFnNode.DUPLICATE_CONST)
                    addError("msg.const.redecl", s);
                else if (dupState == ScriptOrFnNode.DUPLICATE_PARAMETER)
                    addStrictWarning("msg.var.hides.arg", s);
                else if (dupState == ScriptOrFnNode.DUPLICATE_VAR)
                    addStrictWarning("msg.var.redecl", s);
            }
            name = nf.createName(s);

            // omitted check for argument hiding

            if (matchToken(Token.ASSIGN)) {
                decompiler.addToken(Token.ASSIGN);

                init = assignExpr(context == Token.FOR);
                nf.addChildToBack(name, init);
            }
            nf.addChildToBack(pn, name);
            if (!matchToken(Token.COMMA))
                break;
        }
        return pn;
    }

    private Node expr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = assignExpr(inForInit);
        while (matchToken(Token.COMMA)) {
            decompiler.addToken(Token.COMMA);
            if (compilerEnv.isStrictMode() && !pn.hasSideEffects())
                addStrictWarning("msg.no.side.effects", "");
            pn = nf.createBinary(Token.COMMA, pn, assignExpr(inForInit));
        }
        return pn;
    }

    private Node assignExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = condExpr(inForInit);

        int tt = peekToken();
        if (Token.FIRST_ASSIGN <= tt && tt <= Token.LAST_ASSIGN) {
            consumeToken();
            decompiler.addToken(tt);
            pn = nf.createAssignment(tt, pn, assignExpr(inForInit));
        }

        return pn;
    }

    private Node condExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = orExpr(inForInit);

        if (matchToken(Token.HOOK)) {
            decompiler.addToken(Token.HOOK);
            Node ifTrue = assignExpr(false);
            mustMatchToken(Token.COLON, "msg.no.colon.cond");
            decompiler.addToken(Token.COLON);
            Node ifFalse = assignExpr(inForInit);
            return nf.createCondExpr(pn, ifTrue, ifFalse);
        }

        return pn;
    }

    private Node orExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = andExpr(inForInit);
        if (matchToken(Token.OR)) {
            decompiler.addToken(Token.OR);
            pn = nf.createBinary(Token.OR, pn, orExpr(inForInit));
        }

        return pn;
    }

    private Node andExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = bitOrExpr(inForInit);
        if (matchToken(Token.AND)) {
            decompiler.addToken(Token.AND);
            pn = nf.createBinary(Token.AND, pn, andExpr(inForInit));
        }

        return pn;
    }

    private Node bitOrExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = bitXorExpr(inForInit);
        while (matchToken(Token.BITOR)) {
            decompiler.addToken(Token.BITOR);
            pn = nf.createBinary(Token.BITOR, pn, bitXorExpr(inForInit));
        }
        return pn;
    }

    private Node bitXorExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = bitAndExpr(inForInit);
        while (matchToken(Token.BITXOR)) {
            decompiler.addToken(Token.BITXOR);
            pn = nf.createBinary(Token.BITXOR, pn, bitAndExpr(inForInit));
        }
        return pn;
    }

    private Node bitAndExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = eqExpr(inForInit);
        while (matchToken(Token.BITAND)) {
            decompiler.addToken(Token.BITAND);
            pn = nf.createBinary(Token.BITAND, pn, eqExpr(inForInit));
        }
        return pn;
    }

    private Node eqExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = relExpr(inForInit);
        for (;;) {
            int tt = peekToken();
            switch (tt) {
              case Token.EQ:
              case Token.NE:
              case Token.SHEQ:
              case Token.SHNE:
                consumeToken();
                int decompilerToken = tt;
                int parseToken = tt;
                if (compilerEnv.getLanguageVersion() == Context.VERSION_1_2) {
                    // JavaScript 1.2 uses shallow equality for == and != .
                    // In addition, convert === and !== for decompiler into
                    // == and != since the decompiler is supposed to show
                    // canonical source and in 1.2 ===, !== are allowed
                    // only as an alias to ==, !=.
                    switch (tt) {
                      case Token.EQ:
                        parseToken = Token.SHEQ;
                        break;
                      case Token.NE:
                        parseToken = Token.SHNE;
                        break;
                      case Token.SHEQ:
                        decompilerToken = Token.EQ;
                        break;
                      case Token.SHNE:
                        decompilerToken = Token.NE;
                        break;
                    }
                }
                decompiler.addToken(decompilerToken);
                pn = nf.createBinary(parseToken, pn, relExpr(inForInit));
                continue;
            }
            break;
        }
        return pn;
    }

    private Node relExpr(boolean inForInit)
        throws IOException, ParserException
    {
        Node pn = shiftExpr();
        for (;;) {
            int tt = peekToken();
            switch (tt) {
              case Token.IN:
                if (inForInit)
                    break;
                // fall through
              case Token.INSTANCEOF:
              case Token.LE:
              case Token.LT:
              case Token.GE:
              case Token.GT:
                consumeToken();
                decompiler.addToken(tt);
                pn = nf.createBinary(tt, pn, shiftExpr());
                continue;
            }
            break;
        }
        return pn;
    }

    private Node shiftExpr()
        throws IOException, ParserException
    {
        Node pn = addExpr();
        for (;;) {
            int tt = peekToken();
            switch (tt) {
              case Token.LSH:
              case Token.URSH:
              case Token.RSH:
                consumeToken();
                decompiler.addToken(tt);
                pn = nf.createBinary(tt, pn, addExpr());
                continue;
            }
            break;
        }
        return pn;
    }

    private Node addExpr()
        throws IOException, ParserException
    {
        Node pn = mulExpr();
        for (;;) {
            int tt = peekToken();
            if (tt == Token.ADD || tt == Token.SUB) {
                consumeToken();
                decompiler.addToken(tt);
                // flushNewLines
                pn = nf.createBinary(tt, pn, mulExpr());
                continue;
            }
            break;
        }

        return pn;
    }

    private Node mulExpr()
        throws IOException, ParserException
    {
        Node pn = unaryExpr();
        for (;;) {
            int tt = peekToken();
            switch (tt) {
              case Token.MUL:
              case Token.DIV:
              case Token.MOD:
                consumeToken();
                decompiler.addToken(tt);
                pn = nf.createBinary(tt, pn, unaryExpr());
                continue;
            }
            break;
        }

        return pn;
    }

    private Node unaryExpr()
        throws IOException, ParserException
    {
        int tt;

        tt = peekToken();

        switch(tt) {
        case Token.VOID:
        case Token.NOT:
        case Token.BITNOT:
        case Token.TYPEOF:
            consumeToken();
            decompiler.addToken(tt);
            return nf.createUnary(tt, unaryExpr());

        case Token.ADD:
            consumeToken();
            // Convert to special POS token in decompiler and parse tree
            decompiler.addToken(Token.POS);
            return nf.createUnary(Token.POS, unaryExpr());

        case Token.SUB:
            consumeToken();
            // Convert to special NEG token in decompiler and parse tree
            decompiler.addToken(Token.NEG);
            return nf.createUnary(Token.NEG, unaryExpr());

        case Token.INC:
        case Token.DEC:
            consumeToken();
            decompiler.addToken(tt);
            return nf.createIncDec(tt, false, memberExpr(true));

        case Token.DELPROP:
            consumeToken();
            decompiler.addToken(Token.DELPROP);
            return nf.createUnary(Token.DELPROP, unaryExpr());

        case Token.ERROR:
            consumeToken();
            break;

        // XML stream encountered in expression.
        case Token.LT:
            if (compilerEnv.isXmlAvailable()) {
                consumeToken();
                Node pn = xmlInitializer();
                return memberExprTail(true, pn);
            }
            // Fall thru to the default handling of RELOP

        default:
            Node pn = memberExpr(true);

            // Don't look across a newline boundary for a postfix incop.
            tt = peekTokenOrEOL();
            if (tt == Token.INC || tt == Token.DEC) {
                consumeToken();
                decompiler.addToken(tt);
                return nf.createIncDec(tt, true, pn);
            }
            return pn;
        }
        return nf.createName("err"); // Only reached on error.  Try to continue.

    }

    private Node xmlInitializer() throws IOException
    {
        int tt = ts.getFirstXMLToken();
        if (tt != Token.XML && tt != Token.XMLEND) {
            reportError("msg.syntax");
            return null;
        }

        // Make a NEW node to append to. 
        Node pnXML = nf.createLeaf(Token.NEW);

        String xml = ts.getString();
        boolean fAnonymous = xml.trim().startsWith("<>");

        Node pn = nf.createName(fAnonymous ? "XMLList" : "XML");
        nf.addChildToBack(pnXML, pn);

        pn = null;
        Node expr;
        for (;;tt = ts.getNextXMLToken()) {
            switch (tt) {
            case Token.XML:
                xml = ts.getString();
                decompiler.addName(xml);
                mustMatchToken(Token.LC, "msg.syntax");
                decompiler.addToken(Token.LC);
                expr = (peekToken() == Token.RC)
                    ? nf.createString("")
                    : expr(false);
                mustMatchToken(Token.RC, "msg.syntax");
                decompiler.addToken(Token.RC);
                if (pn == null) {
                    pn = nf.createString(xml);
                } else {
                    pn = nf.createBinary(Token.ADD, pn, nf.createString(xml));
                }
                if (ts.isXMLAttribute()) {
                    // Need to put the result in double quotes 
                    expr = nf.createUnary(Token.ESCXMLATTR, expr);
                    Node prepend = nf.createBinary(Token.ADD,
                                                   nf.createString("\""),
                                                   expr);
                    expr = nf.createBinary(Token.ADD,
                                           prepend,
                                           nf.createString("\""));
                } else {
                    expr = nf.createUnary(Token.ESCXMLTEXT, expr);
                }
                pn = nf.createBinary(Token.ADD, pn, expr);
                break;
            case Token.XMLEND:
                xml = ts.getString();
                decompiler.addName(xml);
                if (pn == null) {
                    pn = nf.createString(xml);
                } else {
                    pn = nf.createBinary(Token.ADD, pn, nf.createString(xml));
                }

                nf.addChildToBack(pnXML, pn);
                return pnXML;
            default:
                reportError("msg.syntax");
                return null;
            }
        }
    }

    private void argumentList(Node listNode)
        throws IOException, ParserException
    {
        boolean matched;
        matched = matchToken(Token.RP);
        if (!matched) {
            boolean first = true;
            do {
                if (!first)
                    decompiler.addToken(Token.COMMA);
                first = false;
                nf.addChildToBack(listNode, assignExpr(false));
            } while (matchToken(Token.COMMA));

            mustMatchToken(Token.RP, "msg.no.paren.arg");
        }
        decompiler.addToken(Token.RP);
    }

    private Node memberExpr(boolean allowCallSyntax)
        throws IOException, ParserException
    {
        int tt;

        Node pn;

        // Check for new expressions. 
        tt = peekToken();
        if (tt == Token.NEW) {
            // Eat the NEW token. 
            consumeToken();
            decompiler.addToken(Token.NEW);

            // Make a NEW node to append to. 
            pn = nf.createCallOrNew(Token.NEW, memberExpr(false));

            if (matchToken(Token.LP)) {
                decompiler.addToken(Token.LP);
                // Add the arguments to pn, if any are supplied. 
                argumentList(pn);
            }

             //XXX there's a check in the C source against
             //* "too many constructor arguments" - how many
             //* do we claim to support?
             

             //Experimental syntax:  allow an object literal to follow a new expression,
             //* which will mean a kind of anonymous class built with the JavaAdapter.
             //* the object literal will be passed as an additional argument to the constructor.
            
            tt = peekToken();
            if (tt == Token.LC) {
                nf.addChildToBack(pn, primaryExpr());
            }
        } else {
            pn = primaryExpr();
        }

        return memberExprTail(allowCallSyntax, pn);
    }

    private Node memberExprTail(boolean allowCallSyntax, Node pn)
        throws IOException, ParserException
    {
      tailLoop:
        for (;;) {
            int tt = peekToken();
            switch (tt) {

              case Token.DOT:
              case Token.DOTDOT:
                {
                    int memberTypeFlags;
                    String s;

                    consumeToken();
                    decompiler.addToken(tt);
                    memberTypeFlags = 0;
                    if (tt == Token.DOTDOT) {
                        mustHaveXML();
                        memberTypeFlags = Node.DESCENDANTS_FLAG;
                    }
                    if (!compilerEnv.isXmlAvailable()) {
                        mustMatchToken(Token.NAME, "msg.no.name.after.dot");
                        s = ts.getString();
                        decompiler.addName(s);
                        pn = nf.createPropertyGet(pn, null, s, memberTypeFlags);
                        break;
                    }

                    tt = nextToken();
                    switch (tt) {
                      // handles: name, ns::name, ns::*, ns::[expr]
                      case Token.NAME:
                        s = ts.getString();
                        decompiler.addName(s);
                        pn = propertyName(pn, s, memberTypeFlags);
                        break;

                      // handles: *, *::name, *::*, *::[expr]
                      case Token.MUL:
                        decompiler.addName("*");
                        pn = propertyName(pn, "*", memberTypeFlags);
                        break;

                      // handles: '@attr', '@ns::attr', '@ns::*', '@ns::*',
                      //          '@::attr', '@::*', '@*', '@*::attr', '@*::*'
                      case Token.XMLATTR:
                        decompiler.addToken(Token.XMLATTR);
                        pn = attributeAccess(pn, memberTypeFlags);
                        break;

                      default:
                        reportError("msg.no.name.after.dot");
                    }
                }
                break;

              case Token.DOTQUERY:
                consumeToken();
                mustHaveXML();
                decompiler.addToken(Token.DOTQUERY);
                pn = nf.createDotQuery(pn, expr(false), ts.getLineno());
                mustMatchToken(Token.RP, "msg.no.paren");
                decompiler.addToken(Token.RP);
                break;

              case Token.LB:
                consumeToken();
                decompiler.addToken(Token.LB);
                pn = nf.createElementGet(pn, null, expr(false), 0);
                mustMatchToken(Token.RB, "msg.no.bracket.index");
                decompiler.addToken(Token.RB);
                break;

              case Token.LP:
                if (!allowCallSyntax) {
                    break tailLoop;
                }
                consumeToken();
                decompiler.addToken(Token.LP);
                pn = nf.createCallOrNew(Token.CALL, pn);
                // Add the arguments to pn, if any are supplied. 
                argumentList(pn);
                break;

              default:
                break tailLoop;
            }
        }
        return pn;
    }

    
     //* Xml attribute expression:
     //*   '@attr', '@ns::attr', '@ns::*', '@ns::*', '@*', '@*::attr', '@*::*'
     
    private Node attributeAccess(Node pn, int memberTypeFlags)
        throws IOException
    {
        memberTypeFlags |= Node.ATTRIBUTE_FLAG;
        int tt = nextToken();

        switch (tt) {
          // handles: @name, @ns::name, @ns::*, @ns::[expr]
          case Token.NAME:
            {
                String s = ts.getString();
                decompiler.addName(s);
                pn = propertyName(pn, s, memberTypeFlags);
            }
            break;

          // handles: @*, @*::name, @*::*, @*::[expr]
          case Token.MUL:
            decompiler.addName("*");
            pn = propertyName(pn, "*", memberTypeFlags);
            break;

          // handles @[expr]
          case Token.LB:
            decompiler.addToken(Token.LB);
            pn = nf.createElementGet(pn, null, expr(false), memberTypeFlags);
            mustMatchToken(Token.RB, "msg.no.bracket.index");
            decompiler.addToken(Token.RB);
            break;

          default:
            reportError("msg.no.name.after.xmlAttr");
            pn = nf.createPropertyGet(pn, null, "?", memberTypeFlags);
            break;
        }

        return pn;
    }

   
     // Check if :: follows name in which case it becomes qualified name
    
    private Node propertyName(Node pn, String name, int memberTypeFlags)
        throws IOException, ParserException
    {
        String namespace = null;
        if (matchToken(Token.COLONCOLON)) {
            decompiler.addToken(Token.COLONCOLON);
            namespace = name;

            int tt = nextToken();
            switch (tt) {
              // handles name::name
              case Token.NAME:
                name = ts.getString();
                decompiler.addName(name);
                break;

              // handles name::*
              case Token.MUL:
                decompiler.addName("*");
                name = "*";
                break;

              // handles name::[expr]
              case Token.LB:
                decompiler.addToken(Token.LB);
                pn = nf.createElementGet(pn, namespace, expr(false),
                                         memberTypeFlags);
                mustMatchToken(Token.RB, "msg.no.bracket.index");
                decompiler.addToken(Token.RB);
                return pn;

              default:
                reportError("msg.no.name.after.coloncolon");
                name = "?";
            }
        }

        pn = nf.createPropertyGet(pn, namespace, name, memberTypeFlags);
        return pn;
    }

    private Node primaryExpr()
        throws IOException, ParserException
    {
        Node pn;

        int ttFlagged = nextFlaggedToken();
        int tt = ttFlagged & CLEAR_TI_MASK;

        switch(tt) {

          case Token.FUNCTION:
            return function(FunctionNode.FUNCTION_EXPRESSION);

          case Token.LB: {
            ObjArray elems = new ObjArray();
            int skipCount = 0;
            decompiler.addToken(Token.LB);
            boolean after_lb_or_comma = true;
            for (;;) {
                tt = peekToken();

                if (tt == Token.COMMA) {
                    consumeToken();
                    decompiler.addToken(Token.COMMA);
                    if (!after_lb_or_comma) {
                        after_lb_or_comma = true;
                    } else {
                        elems.add(null);
                        ++skipCount;
                    }
                } else if (tt == Token.RB) {
                    consumeToken();
                    decompiler.addToken(Token.RB);
                    break;
                } else {
                    if (!after_lb_or_comma) {
                        reportError("msg.no.bracket.arg");
                    }
                    elems.add(assignExpr(false));
                    after_lb_or_comma = false;
                }
            }
            return nf.createArrayLiteral(elems, skipCount);
          }

          case Token.LC: {
            ObjArray elems = new ObjArray();
            decompiler.addToken(Token.LC);
            if (!matchToken(Token.RC)) {

                boolean first = true;
            commaloop:
                do {
                    Object property;

                    if (!first)
                        decompiler.addToken(Token.COMMA);
                    else
                        first = false;

                    tt = peekToken();
                    switch(tt) {
                      case Token.NAME:
                      case Token.STRING:
                        consumeToken();
                        // map NAMEs to STRINGs in object literal context
                        // but tell the decompiler the proper type
                        String s = ts.getString();
                        if (tt == Token.NAME) {
                            if (s.equals("get") &&
                                peekToken() == Token.NAME) {
                                decompiler.addToken(Token.GET);
                                consumeToken();
                                s = ts.getString();
                                decompiler.addName(s);
                                property = ScriptRuntime.getIndexObject(s);
                                if (!getterSetterProperty(elems, property,
                                                          true))
                                    break commaloop;
                                break;
                            } else if (s.equals("set") &&
                                       peekToken() == Token.NAME) {
                                decompiler.addToken(Token.SET);
                                consumeToken();
                                s = ts.getString();
                                decompiler.addName(s);
                                property = ScriptRuntime.getIndexObject(s);
                                if (!getterSetterProperty(elems, property,
                                                          false))
                                    break commaloop;
                                break;
                            }
                            decompiler.addName(s);
                        } else {
                            decompiler.addString(s);
                        }
                        property = ScriptRuntime.getIndexObject(s);
                        plainProperty(elems, property);
                        break;

                      case Token.NUMBER:
                        consumeToken();
                        double n = ts.getNumber();
                        decompiler.addNumber(n);
                        property = ScriptRuntime.getIndexObject(n);
                        plainProperty(elems, property);
                        break;

                      case Token.RC:
                        // trailing comma is OK.
                        break commaloop;
                    default:
                        reportError("msg.bad.prop");
                        break commaloop;
                    }
                } while (matchToken(Token.COMMA));

                mustMatchToken(Token.RC, "msg.no.brace.prop");
            }
            decompiler.addToken(Token.RC);
            return nf.createObjectLiteral(elems);
          }

          case Token.LP:

             //Brendan's IR-jsparse.c makes a new node tagged with
             //* TOK_LP here... I'm not sure I understand why.  Isn't
             //* the grouping already implicit in the structure of the
             //* parse tree?  also TOK_LP is already overloaded (I
             //* think) in the C IR as 'function call.'  
            decompiler.addToken(Token.LP);
            pn = expr(false);
            pn.putProp(Node.PARENTHESIZED_PROP, Boolean.TRUE);
            decompiler.addToken(Token.RP);
            mustMatchToken(Token.RP, "msg.no.paren");
            return pn;

          case Token.XMLATTR:
            mustHaveXML();
            decompiler.addToken(Token.XMLATTR);
            pn = attributeAccess(null, 0);
            return pn;

          case Token.NAME: {
            String name = ts.getString();
            if ((ttFlagged & TI_CHECK_LABEL) != 0) {
                if (peekToken() == Token.COLON) {
                    // Do not consume colon, it is used as unwind indicator
                    // to return to statementHelper.
                    // XXX Better way?
                    return nf.createLabel(ts.getLineno());
                }
            }

            decompiler.addName(name);
            if (compilerEnv.isXmlAvailable()) {
                pn = propertyName(null, name, 0);
            } else {
                pn = nf.createName(name);
            }
            return pn;
          }

          case Token.NUMBER: {
            double n = ts.getNumber();
            decompiler.addNumber(n);
            return nf.createNumber(n);
          }

          case Token.STRING: {
            String s = ts.getString();
            decompiler.addString(s);
            return nf.createString(s);
          }

          case Token.DIV:
          case Token.ASSIGN_DIV: {
            // Got / or /= which should be treated as regexp in fact
            ts.readRegExp(tt);
            String flags = ts.regExpFlags;
            ts.regExpFlags = null;
            String re = ts.getString();
            decompiler.addRegexp(re, flags);
            int index = currentScriptOrFn.addRegexp(re, flags);
            return nf.createRegExp(index);
          }

          case Token.NULL:
          case Token.THIS:
          case Token.FALSE:
          case Token.TRUE:
            decompiler.addToken(tt);
            return nf.createLeaf(tt);

          case Token.RESERVED:
            reportError("msg.reserved.id");
            break;

          case Token.ERROR:
            // the scanner or one of its subroutines reported the error. 
            break;

          case Token.EOF:
            reportError("msg.unexpected.eof");
            break;

          default:
            reportError("msg.syntax");
            break;
        }
        return null;    // should never reach here
    }

    private void plainProperty(ObjArray elems, Object property)
            throws IOException {
        mustMatchToken(Token.COLON, "msg.no.colon.prop");

        // OBJLIT is used as ':' in object literal for
        // decompilation to solve spacing ambiguity.
        decompiler.addToken(Token.OBJECTLIT);
        elems.add(property);
        elems.add(assignExpr(false));
    }

    private boolean getterSetterProperty(ObjArray elems, Object property,
                                         boolean isGetter) throws IOException {
        Node f = function(FunctionNode.FUNCTION_EXPRESSION);
        if (f.getType() != Token.FUNCTION) {
            reportError("msg.bad.prop");
            return false;
        }
        int fnIndex = f.getExistingIntProp(Node.FUNCTION_PROP);
        FunctionNode fn = currentScriptOrFn.getFunctionNode(fnIndex);
        if (fn.getFunctionName().length() != 0) {
            reportError("msg.bad.prop");
            return false;
        }
        elems.add(property);
        if (isGetter) {
            elems.add(nf.createUnary(Token.GET, f));
        } else {
            elems.add(nf.createUnary(Token.SET, f));
        }
        return true;
    }
}





public class Token
{

    // debug flags
    public static bool printTrees = false;
    static bool printICode = false;
    static bool printNames = printTrees || printICode;

    
     //* Token types.  These values correspond to JSTokenType values in
     //* jsscan.c.
     

    public const int
    // start enum
        ERROR          = -1, // well-known as the only code < EOF
        EOF            = 0,  // end of file token - (not EOF_CHAR)
        EOL            = 1,  // end of line

        // Interpreter reuses the following as bytecodes
        FIRST_BYTECODE_TOKEN    = 2,

        ENTERWITH      = 2,
        LEAVEWITH      = 3,
        RETURN         = 4,
        GOTO           = 5,
        IFEQ           = 6,
        IFNE           = 7,
        SETNAME        = 8,
        BITOR          = 9,
        BITXOR         = 10,
        BITAND         = 11,
        EQ             = 12,
        NE             = 13,
        LT             = 14,
        LE             = 15,
        GT             = 16,
        GE             = 17,
        LSH            = 18,
        RSH            = 19,
        URSH           = 20,
        ADD            = 21,
        SUB            = 22,
        MUL            = 23,
        DIV            = 24,
        MOD            = 25,
        NOT            = 26,
        BITNOT         = 27,
        POS            = 28,
        NEG            = 29,
        NEW            = 30,
        DELPROP        = 31,
        TYPEOF         = 32,
        GETPROP        = 33,
        SETPROP        = 34,
        GETELEM        = 35,
        SETELEM        = 36,
        CALL           = 37,
        NAME           = 38,
        NUMBER         = 39,
        STRING         = 40,
        NULL           = 41,
        THIS           = 42,
        FALSE          = 43,
        TRUE           = 44,
        SHEQ           = 45,   // shallow equality (===)
        SHNE           = 46,   // shallow inequality (!==)
        REGEXP         = 47,
        BINDNAME       = 48,
        THROW          = 49,
        RETHROW        = 50, // rethrow caught execetion: catch (e if ) use it
        IN             = 51,
        INSTANCEOF     = 52,
        LOCAL_LOAD     = 53,
        GETVAR         = 54,
        SETVAR         = 55,
        CATCH_SCOPE    = 56,
        ENUM_INIT_KEYS = 57,
        ENUM_INIT_VALUES = 58,
        ENUM_NEXT      = 59,
        ENUM_ID        = 60,
        THISFN         = 61,
        RETURN_RESULT  = 62, // to return prevoisly stored return result
        ARRAYLIT       = 63, // array literal
        OBJECTLIT      = 64, // object literal
        GET_REF        = 65, // *reference
        SET_REF        = 66, // *reference    = something
        DEL_REF        = 67, // delete reference
        REF_CALL       = 68, // f(args)    = something or f(args)++
        REF_SPECIAL    = 69, // reference for special properties like __proto

        // For XML support:
        DEFAULTNAMESPACE = 70, // default xml namespace =
        ESCXMLATTR     = 71,
        ESCXMLTEXT     = 72,
        REF_MEMBER     = 73, // Reference for x.@y, x..y etc.
        REF_NS_MEMBER  = 74, // Reference for x.ns::y, x..ns::y etc.
        REF_NAME       = 75, // Reference for @y, @[y] etc.
        REF_NS_NAME    = 76; // Reference for ns::y, @ns::y@[y] etc.

        // End of interpreter bytecodes
    public const int
        LAST_BYTECODE_TOKEN    = REF_NS_NAME,

        TRY            = 77,
        SEMI           = 78,  // semicolon
        LB             = 79,  // left and right brackets
        RB             = 80,
        LC             = 81,  // left and right curlies (braces)
        RC             = 82,
        LP             = 83,  // left and right parentheses
        RP             = 84,
        COMMA          = 85,  // comma operator

        ASSIGN         = 86,  // simple assignment  (=)
        ASSIGN_BITOR   = 87,  // |=
        ASSIGN_BITXOR  = 88,  // ^=
        ASSIGN_BITAND  = 89,  // |=
        ASSIGN_LSH     = 90,  // <<=
        ASSIGN_RSH     = 91,  // >>=
        ASSIGN_URSH    = 92,  // >>>=
        ASSIGN_ADD     = 93,  // +=
        ASSIGN_SUB     = 94,  // -=
        ASSIGN_MUL     = 95,  // *=
        ASSIGN_DIV     = 96,  // /=
        ASSIGN_MOD     = 97;  // %=

    public const int
        FIRST_ASSIGN   = ASSIGN,
        LAST_ASSIGN    = ASSIGN_MOD,

        HOOK           = 98, // conditional (?:)
        COLON          = 99,
        OR             = 100, // logical or (||)
        AND            = 101, // logical and (&&)
        INC            = 102, // increment/decrement (++ --)
        DEC            = 103,
        DOT            = 104, // member operator (.)
        FUNCTION       = 105, // function keyword
        EXPORT         = 106, // export keyword
        IMPORT         = 107, // import keyword
        IF             = 108, // if keyword
        ELSE           = 109, // else keyword
        SWITCH         = 110, // switch keyword
        CASE           = 111, // case keyword
        DEFAULT        = 112, // default keyword
        WHILE          = 113, // while keyword
        DO             = 114, // do keyword
        FOR            = 115, // for keyword
        BREAK          = 116, // break keyword
        CONTINUE       = 117, // continue keyword
        VAR            = 118, // var keyword
        WITH           = 119, // with keyword
        CATCH          = 120, // catch keyword
        readonlyLY        = 121, // readonlyly keyword
        VOID           = 122, // void keyword
        RESERVED       = 123, // reserved keywords

        EMPTY          = 124,

        //types used for the parse tree - these never get returned
         // by the scanner.
         

        BLOCK          = 125, // statement block
        LABEL          = 126, // label
        TARGET         = 127,
        LOOP           = 128,
        EXPR_VOID      = 129, // expression statement in functions
        EXPR_RESULT    = 130, // expression statement in scripts
        JSR            = 131,
        SCRIPT         = 132, // top-level node for entire script
        TYPEOFNAME     = 133, // for typeof(simple-name)
        USE_STACK      = 134,
        SETPROP_OP     = 135, // x.y op= something
        SETELEM_OP     = 136, // x[y] op= something
        LOCAL_BLOCK    = 137,
        SET_REF_OP     = 138, // *reference op= something

        // For XML support:
        DOTDOT         = 139,  // member operator (..)
        COLONCOLON     = 140,  // namespace::name
        XML            = 141,  // XML type
        DOTQUERY       = 142,  // .() -- e.g., x.emps.emp.(name == "terry")
        XMLATTR        = 143,  // @
        XMLEND         = 144,

        // Optimizer-only-tokens
        TO_OBJECT      = 145,
        TO_DOUBLE      = 146,

        GET            = 147,  // JS 1.5 get pseudo keyword
        SET            = 148,  // JS 1.5 set pseudo keyword
        CONST          = 149,
        SETCONST       = 150,
        SETCONSTVAR    = 151,

        CONDCOMMENT    = 152,  // JScript conditional comment
        KEEPCOMMENT    = 153,  // //comment

        LAST_TOKEN     = 154;

    public static String name(int token)
    {
        if (!printNames) {
            return token.ToString();
        }
        switch (token) {
          case ERROR:           return "ERROR";
          case EOF:             return "EOF";
          case EOL:             return "EOL";
          case ENTERWITH:       return "ENTERWITH";
          case LEAVEWITH:       return "LEAVEWITH";
          case RETURN:          return "RETURN";
          case GOTO:            return "GOTO";
          case IFEQ:            return "IFEQ";
          case IFNE:            return "IFNE";
          case SETNAME:         return "SETNAME";
          case BITOR:           return "BITOR";
          case BITXOR:          return "BITXOR";
          case BITAND:          return "BITAND";
          case EQ:              return "EQ";
          case NE:              return "NE";
          case LT:              return "LT";
          case LE:              return "LE";
          case GT:              return "GT";
          case GE:              return "GE";
          case LSH:             return "LSH";
          case RSH:             return "RSH";
          case URSH:            return "URSH";
          case ADD:             return "ADD";
          case SUB:             return "SUB";
          case MUL:             return "MUL";
          case DIV:             return "DIV";
          case MOD:             return "MOD";
          case NOT:             return "NOT";
          case BITNOT:          return "BITNOT";
          case POS:             return "POS";
          case NEG:             return "NEG";
          case NEW:             return "NEW";
          case DELPROP:         return "DELPROP";
          case TYPEOF:          return "TYPEOF";
          case GETPROP:         return "GETPROP";
          case SETPROP:         return "SETPROP";
          case GETELEM:         return "GETELEM";
          case SETELEM:         return "SETELEM";
          case CALL:            return "CALL";
          case NAME:            return "NAME";
          case NUMBER:          return "NUMBER";
          case STRING:          return "STRING";
          case NULL:            return "NULL";
          case THIS:            return "THIS";
          case FALSE:           return "FALSE";
          case TRUE:            return "TRUE";
          case SHEQ:            return "SHEQ";
          case SHNE:            return "SHNE";
          case REGEXP:          return "OBJECT";
          case BINDNAME:        return "BINDNAME";
          case THROW:           return "THROW";
          case RETHROW:         return "RETHROW";
          case IN:              return "IN";
          case INSTANCEOF:      return "INSTANCEOF";
          case LOCAL_LOAD:      return "LOCAL_LOAD";
          case GETVAR:          return "GETVAR";
          case SETVAR:          return "SETVAR";
          case CATCH_SCOPE:     return "CATCH_SCOPE";
          case ENUM_INIT_KEYS:  return "ENUM_INIT_KEYS";
          case ENUM_INIT_VALUES:  return "ENUM_INIT_VALUES";
          case ENUM_NEXT:       return "ENUM_NEXT";
          case ENUM_ID:         return "ENUM_ID";
          case THISFN:          return "THISFN";
          case RETURN_RESULT:   return "RETURN_RESULT";
          case ARRAYLIT:        return "ARRAYLIT";
          case OBJECTLIT:       return "OBJECTLIT";
          case GET_REF:         return "GET_REF";
          case SET_REF:         return "SET_REF";
          case DEL_REF:         return "DEL_REF";
          case REF_CALL:        return "REF_CALL";
          case REF_SPECIAL:     return "REF_SPECIAL";
          case DEFAULTNAMESPACE:return "DEFAULTNAMESPACE";
          case ESCXMLTEXT:      return "ESCXMLTEXT";
          case ESCXMLATTR:      return "ESCXMLATTR";
          case REF_MEMBER:      return "REF_MEMBER";
          case REF_NS_MEMBER:   return "REF_NS_MEMBER";
          case REF_NAME:        return "REF_NAME";
          case REF_NS_NAME:     return "REF_NS_NAME";
          case TRY:             return "TRY";
          case SEMI:            return "SEMI";
          case LB:              return "LB";
          case RB:              return "RB";
          case LC:              return "LC";
          case RC:              return "RC";
          case LP:              return "LP";
          case RP:              return "RP";
          case COMMA:           return "COMMA";
          case ASSIGN:          return "ASSIGN";
          case ASSIGN_BITOR:    return "ASSIGN_BITOR";
          case ASSIGN_BITXOR:   return "ASSIGN_BITXOR";
          case ASSIGN_BITAND:   return "ASSIGN_BITAND";
          case ASSIGN_LSH:      return "ASSIGN_LSH";
          case ASSIGN_RSH:      return "ASSIGN_RSH";
          case ASSIGN_URSH:     return "ASSIGN_URSH";
          case ASSIGN_ADD:      return "ASSIGN_ADD";
          case ASSIGN_SUB:      return "ASSIGN_SUB";
          case ASSIGN_MUL:      return "ASSIGN_MUL";
          case ASSIGN_DIV:      return "ASSIGN_DIV";
          case ASSIGN_MOD:      return "ASSIGN_MOD";
          case HOOK:            return "HOOK";
          case COLON:           return "COLON";
          case OR:              return "OR";
          case AND:             return "AND";
          case INC:             return "INC";
          case DEC:             return "DEC";
          case DOT:             return "DOT";
          case FUNCTION:        return "FUNCTION";
          case EXPORT:          return "EXPORT";
          case IMPORT:          return "IMPORT";
          case IF:              return "IF";
          case ELSE:            return "ELSE";
          case SWITCH:          return "SWITCH";
          case CASE:            return "CASE";
          case DEFAULT:         return "DEFAULT";
          case WHILE:           return "WHILE";
          case DO:              return "DO";
          case FOR:             return "FOR";
          case BREAK:           return "BREAK";
          case CONTINUE:        return "CONTINUE";
          case VAR:             return "VAR";
          case WITH:            return "WITH";
          case CATCH:           return "CATCH";
          case readonlyLY:         return "readonlyLY";
          case RESERVED:        return "RESERVED";
          case EMPTY:           return "EMPTY";
          case BLOCK:           return "BLOCK";
          case LABEL:           return "LABEL";
          case TARGET:          return "TARGET";
          case LOOP:            return "LOOP";
          case EXPR_VOID:       return "EXPR_VOID";
          case EXPR_RESULT:     return "EXPR_RESULT";
          case JSR:             return "JSR";
          case SCRIPT:          return "SCRIPT";
          case TYPEOFNAME:      return "TYPEOFNAME";
          case USE_STACK:       return "USE_STACK";
          case SETPROP_OP:      return "SETPROP_OP";
          case SETELEM_OP:      return "SETELEM_OP";
          case LOCAL_BLOCK:     return "LOCAL_BLOCK";
          case SET_REF_OP:      return "SET_REF_OP";
          case DOTDOT:          return "DOTDOT";
          case COLONCOLON:      return "COLONCOLON";
          case XML:             return "XML";
          case DOTQUERY:        return "DOTQUERY";
          case XMLATTR:         return "XMLATTR";
          case XMLEND:          return "XMLEND";
          case TO_OBJECT:       return "TO_OBJECT";
          case TO_DOUBLE:       return "TO_DOUBLE";
          case GET:             return "GET";
          case SET:             return "SET";
          case CONST:           return "CONST";
          case SETCONST:        return "SETCONST";
        }

        // Token without name
        throw new Exception(token.ToString());
    }
}


public class JavaScriptToken {

    private int type;
    private String value;

    JavaScriptToken(int type, String value) {
        this.type = type;
        this.value = value;
    }

    int getType() {
        return type;
    }

    String getValue() {
        return value;
    }
}




public class JavaScriptCompressor {

    static ArrayList ones;
    static  ArrayList twos;
    static  ArrayList threes;

    static List<string> builtin = new List<string>();
    static Hashtable literals = new Hashtable();
    static List<string> reserved = new List<string>();

    JavaScriptCompressor() {

        // This list contains all the 3 characters or less built-in global
        // symbols available in a browser. Please add to this list if you
        // see anything missing.
        builtin.Add("NaN");
        builtin.Add("top");

        ones = new ArrayList();
        for (char c = 'a'; c <= 'z'; c++)
            ones.Add(char.ToString(c));
        for (char c = 'A'; c <= 'Z'; c++)
            ones.Add(char.ToString(c));

        twos = new ArrayList();
        for (int i = 0; i < ones.Count; i++) {
            String one = (String) ones[i];
            for (char c = 'a'; c <= 'z'; c++)
                twos.Add(one + char.ToString(c));
            for (char c = 'A'; c <= 'Z'; c++)
                twos.Add(one + char.ToString(c));
            for (char c = '0'; c <= '9'; c++)
                twos.Add(one + char.ToString(c));
        }

        // Remove two-letter JavaScript reserved words and built-in globals...
        
        twos.Remove("as");
        twos.Remove("is");
        twos.Remove("do");
        twos.Remove("if");
        twos.Remove("in");
        
        //twos.RemoveAll(builtin);
        for(int i=0,j=builtin.Count;i<j;i++)
        {
            twos.Remove(builtin[0].ToString());
        }

        threes = new ArrayList();
        for (int i = 0; i < twos.Count; i++) {
            String two = (String) twos[i];
            for (char c = 'a'; c <= 'z'; c++)
                threes.Add(two + char.ToString(c));
            for (char c = 'A'; c <= 'Z'; c++)
                threes.Add(two + char.ToString(c));
            for (char c = '0'; c <= '9'; c++)
                threes.Add(two + char.ToString(c));
        }

        // Remove three-letter JavaScript reserved words and built-in globals...
        threes.Remove("for");
        threes.Remove("int");
        threes.Remove("new");
        threes.Remove("try");
        threes.Remove("use");
        threes.Remove("var");
        //threes.RemoveAll(builtin);
        for(int i=0,j=builtin.Count;i<j;i++)
        {
            twos.Remove(builtin[0].ToString());
        }


        // That's up to ((26+26)*(1+(26+26+10)))*(1+(26+26+10))-8
        // (206,380 symbols per scope)
        
        // The following list comes from org/mozilla/javascript/Decompiler.java...
        literals.Add(Token.GET, "get ");
        literals.Add(Token.SET, "set ");
        literals.Add(Token.TRUE, "true");
        literals.Add(Token.FALSE, "false");
        literals.Add(Token.NULL, "null");
        literals.Add(Token.THIS, "this");
        literals.Add(Token.FUNCTION, "function");
        literals.Add(Token.COMMA, ",");
        literals.Add(Token.LC, "{");
        literals.Add(Token.RC, "}");
        literals.Add(Token.LP, "(");
        literals.Add(Token.RP, ")");
        literals.Add(Token.LB, "[");
        literals.Add(Token.RB, "]");
        literals.Add(Token.DOT, ".");
        literals.Add(Token.NEW, "new ");
        literals.Add(Token.DELPROP, "delete ");
        literals.Add(Token.IF, "if");
        literals.Add(Token.ELSE, "else");
        literals.Add(Token.FOR, "for");
        literals.Add(Token.IN, " in ");
        literals.Add(Token.WITH, "with");
        literals.Add(Token.WHILE, "while");
        literals.Add(Token.DO, "do");
        literals.Add(Token.TRY, "try");
        literals.Add(Token.CATCH, "catch");
        literals.Add(Token.readonlyLY, "readonlyly");
        literals.Add(Token.THROW, "throw");
        literals.Add(Token.SWITCH, "switch");
        literals.Add(Token.BREAK, "break");
        literals.Add(Token.CONTINUE, "continue");
        literals.Add(Token.CASE, "case");
        literals.Add(Token.DEFAULT, "default");
        literals.Add(Token.RETURN, "return");
        literals.Add(Token.VAR, "var ");
        literals.Add(Token.SEMI, ";");
        literals.Add(Token.ASSIGN, "=");
        literals.Add(Token.ASSIGN_ADD, "+=");
        literals.Add(Token.ASSIGN_SUB, "-=");
        literals.Add(Token.ASSIGN_MUL, "*=");
        literals.Add(Token.ASSIGN_DIV, "/=");
        literals.Add(Token.ASSIGN_MOD, "%=");
        literals.Add(Token.ASSIGN_BITOR, "|=");
        literals.Add(Token.ASSIGN_BITXOR, "^=");
        literals.Add(Token.ASSIGN_BITAND, "&=");
        literals.Add(Token.ASSIGN_LSH, "<<=");
        literals.Add(Token.ASSIGN_RSH, ">>=");
        literals.Add(Token.ASSIGN_URSH, ">>>=");
        literals.Add(Token.HOOK, "?");
        literals.Add(Token.OBJECTLIT, ":");
        literals.Add(Token.COLON, ":");
        literals.Add(Token.OR, "||");
        literals.Add(Token.AND, "&&");
        literals.Add(Token.BITOR, "|");
        literals.Add(Token.BITXOR, "^");
        literals.Add(Token.BITAND, "&");
        literals.Add(Token.SHEQ, "===");
        literals.Add(Token.SHNE, "!==");
        literals.Add(Token.EQ, "==");
        literals.Add(Token.NE, "!=");
        literals.Add(Token.LE, "<=");
        literals.Add(Token.LT, "<");
        literals.Add(Token.GE, ">=");
        literals.Add(Token.GT, ">");
        literals.Add(Token.INSTANCEOF, " instanceof ");
        literals.Add(Token.LSH, "<<");
        literals.Add(Token.RSH, ">>");
        literals.Add(Token.URSH, ">>>");
        literals.Add(Token.TYPEOF, "typeof");
        literals.Add(Token.VOID, "void ");
        literals.Add(Token.CONST, "const ");
        literals.Add(Token.NOT, "!");
        literals.Add(Token.BITNOT, "~");
        literals.Add(Token.POS, "+");
        literals.Add(Token.NEG, "-");
        literals.Add(Token.INC, "++");
        literals.Add(Token.DEC, "--");
        literals.Add(Token.ADD, "+");
        literals.Add(Token.SUB, "-");
        literals.Add(Token.MUL, "*");
        literals.Add(Token.DIV, "/");
        literals.Add(Token.MOD, "%");
        literals.Add(Token.COLONCOLON, "::");
        literals.Add(Token.DOTDOT, "..");
        literals.Add(Token.DOTQUERY, ".(");
        literals.Add(Token.XMLATTR, "@");

        // See http://developer.mozilla.org/en/docs/Core_JavaScript_1.5_Reference:Reserved_Words

        // JavaScript 1.5 reserved words
        reserved.Add("break");
        reserved.Add("case");
        reserved.Add("catch");
        reserved.Add("continue");
        reserved.Add("default");
        reserved.Add("delete");
        reserved.Add("do");
        reserved.Add("else");
        reserved.Add("readonlyly");
        reserved.Add("for");
        reserved.Add("function");
        reserved.Add("if");
        reserved.Add("in");
        reserved.Add("instanceof");
        reserved.Add("new");
        reserved.Add("return");
        reserved.Add("switch");
        reserved.Add("this");
        reserved.Add("throw");
        reserved.Add("try");
        reserved.Add("typeof");
        reserved.Add("var");
        reserved.Add("void");
        reserved.Add("while");
        reserved.Add("with");
        // Words reserved for future use
        reserved.Add("abstract");
        reserved.Add("boolean");
        reserved.Add("byte");
        reserved.Add("char");
        reserved.Add("class");
        reserved.Add("const");
        reserved.Add("debugger");
        reserved.Add("double");
        reserved.Add("enum");
        reserved.Add("export");
        reserved.Add("extends");
        reserved.Add("readonly");
        reserved.Add("float");
        reserved.Add("goto");
        reserved.Add("implements");
        reserved.Add("import");
        reserved.Add("int");
        reserved.Add("interface");
        reserved.Add("long");
        reserved.Add("native");
        reserved.Add("package");
        reserved.Add("private");
        reserved.Add("protected");
        reserved.Add("public");
        reserved.Add("short");
        reserved.Add("static");
        reserved.Add("super");
        reserved.Add("synchronized");
        reserved.Add("throws");
        reserved.Add("transient");
        reserved.Add("volatile");
        // These are not reserved, but should be taken into account
        // in isValidIdentifier (See jslint source code)
        reserved.Add("arguments");
        reserved.Add("eval");
        reserved.Add("true");
        reserved.Add("false");
        reserved.Add("Infinity");
        reserved.Add("NaN");
        reserved.Add("null");
        reserved.Add("undefined");
    }

    private static int countChar(String haystack, char needle) {
        int idx = 0;
        int count = 0;
        
        int length = haystack.Length;
        
        while (idx < length) {
            char c = haystack[idx++];
            if (c == needle) {
                count++;
            }
        }
        return count;
    }

    private static int printSourceString(String source, int offset, StringBuilder sb) {
       
        int length = source[offset];
        ++offset;
        if ((0x8000 & length) != 0) {
            length = ((0x7FFF & length) << 16) | source[offset];
            ++offset;
        }
        if (sb != null) {
            String str = source.Substring(offset, offset + length);
            sb.append(str);
        }
        return offset + length;
    }

    private static int printSourceNumber(String source,
            int offset, StringBuilder sb) {
        double number = 0.0;
        char type = source[offset];
        ++offset;
        if (type == 'S') {
            if (sb != null) {
                number = source[offset];
            }
            ++offset;
        } else if (type == 'J' || type == 'D') {
            if (sb != null) {
                long lbits;
                lbits = (long) source[offset] << 48;
                lbits |= (long) source[offset + 1] << 32;
                lbits |= (long) source[offset + 2] << 16;
                lbits |= (long) source[offset + 3];
                if (type == 'J') {
                    number = lbits;
                } else {
                    number = Double.Parse(lbits.ToString());
                }
            }
            offset += 4;
        } else {
            // Bad source
            throw new Exception();
        }
        if (sb != null) {
            sb.append(ScriptRuntime.numberToString(number, 10));
        }
        return offset;
    }

    
    private static ArrayList parse(Reader in, ErrorReporter reporter)
    {

        CompilerEnvirons env = new CompilerEnvirons();
        Parser parser = new Parser(env, reporter);
        parser.parse(in, null, 1);
        String source = parser.getEncodedSource();

        int offset = 0;
        int length = source.length();
        ArrayList tokens = new ArrayList();
        StringBuilder sb = new StringBuilder();

        while (offset < length) {
            int tt = source.charAt(offset++);
            switch (tt) {

                case Token.CONDCOMMENT:
                case Token.KEEPCOMMENT:
                case Token.NAME:
                case Token.REGEXP:
                case Token.STRING:
                    sb.setLength(0);
                    offset = printSourceString(source, offset, sb);
                    tokens.Add(new JavaScriptToken(tt, sb.toString()));
                    break;

                case Token.NUMBER:
                    sb.setLength(0);
                    offset = printSourceNumber(source, offset, sb);
                    tokens.Add(new JavaScriptToken(tt, sb.toString()));
                    break;

                default:
                    String literal = (String) literals.get(new Integer(tt));
                    if (literal != null) {
                        tokens.Add(new JavaScriptToken(tt, literal));
                    }
                    break;
            }
        }

        return tokens;
    }
    
    private static void processStringLiterals(ArrayList tokens, bool merge) {

        String tv;
        int i, length = tokens.Count;
        JavaScriptToken token, prevToken, nextToken;

        if (merge) {

            // Concatenate string literals that are being appended wherever
            // it is safe to do so. Note that we take care of the case:
            //     "a" + "b".toUpperCase()

            for (i = 0; i < length; i++) {
                token = (JavaScriptToken) tokens.get(i);
                switch (token.getType()) {

                    case Token.Add:
                        if (i > 0 && i < length) {
                            prevToken = (JavaScriptToken) tokens.get(i - 1);
                            nextToken = (JavaScriptToken) tokens.get(i + 1);
                            if (prevToken.getType() == Token.STRING && nextToken.getType() == Token.STRING &&
                                    (i == length - 1 || ((JavaScriptToken) tokens.get(i + 2)).getType() != Token.DOT)) {
                                tokens.set(i - 1, new JavaScriptToken(Token.STRING,
                                        prevToken.getValue() + nextToken.getValue()));
                                tokens.Remove(i + 1);
                                tokens.Remove(i);
                                i = i - 1;
                                length = length - 2;
                                break;
                            }
                        }
                }
            }

        }

        // Second pass...

        for (i = 0; i < length; i++) {
            token = (JavaScriptToken) tokens.get(i);
            if (token.getType() == Token.STRING) {
                tv = token.getValue();

                // readonlyly, add the quoting characters and escape the string. We use
                // the quoting character that minimizes the amount of escaping to save
                // a few additional bytes.

                char quotechar;
                int singleQuoteCount = countChar(tv, '\'');
                int doubleQuoteCount = countChar(tv, '"');
                if (doubleQuoteCount <= singleQuoteCount) {
                    quotechar = '"';
                } else {
                    quotechar = '\'';
                }

                tv = quotechar + escapeString(tv, quotechar) + quotechar;

                // String concatenation transforms the old script scheme:
                //     '<scr'+'ipt ...><'+'/script>'
                // into the following:
                //     '<script ...></script>'
                // which breaks if this code is embedded inside an HTML document.
                // Since this is not the right way to do this, let's fix the code by
                // transforming all "</script" into "<\/script"

                if (tv.indexOf("</script") >= 0) {
                    tv = tv.replaceAll("<\\/script", "<\\\\/script");
                }

                tokens.set(i, new JavaScriptToken(Token.STRING, tv));
            }
        }
    }

    // Add necessary escaping that was removed in Rhino's tokenizer.
    private static String escapeString(String s, char quotechar) {

        assert quotechar == '"' || quotechar == '\'';

        if (s == null) {
            return null;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0, L = s.length(); i < L; i++) {
            int c = s.charAt(i);
            if (c == quotechar) {
                sb.append("\\");
            }
            sb.append((char) c);
        }

        return sb.toString();
    }

    
     //* Simple check to see whether a string is a valid identifier name.
     //* If a string matches this pattern, it means it IS a valid
     //* identifier name. If a string doesn't match it, it does not
     //* necessarily mean it is not a valid identifier name.
     
    private static readonly Pattern SIMPLE_IDENTIFIER_NAME_PATTERN = Pattern.compile("^[a-zA-Z_][a-zA-Z0-9_]*$");

    private static boolean isValidIdentifier(String s) {
        Matcher m = SIMPLE_IDENTIFIER_NAME_PATTERN.matcher(s);
        return (m.matches() && !reserved.contains(s));
    }

   
    //Transforms obj["foo"] into obj.foo whenever possible, saving 3 bytes.
   
    private static void optimizeObjectMemberAccess(ArrayList tokens) {

        String tv;
        int i, length;
        JavaScriptToken token;

        for (i = 0, length = tokens.count(); i < length; i++) {

            if (((JavaScriptToken) tokens.get(i)).getType() == Token.LB &&
                    i > 0 && i < length - 2 &&
                    ((JavaScriptToken) tokens.get(i - 1)).getType() == Token.NAME &&
                    ((JavaScriptToken) tokens.get(i + 1)).getType() == Token.STRING &&
                    ((JavaScriptToken) tokens.get(i + 2)).getType() == Token.RB) {
                token = (JavaScriptToken) tokens.get(i + 1);
                tv = token.getValue();
                tv = tv.substring(1, tv.length() - 1);
                if (isValidIdentifier(tv)) {
                    tokens.set(i, new JavaScriptToken(Token.DOT, "."));
                    tokens.set(i + 1, new JavaScriptToken(Token.NAME, tv));
                    tokens.Remove(i + 2);
                    i = i + 2;
                    length = length - 1;
                }
            }
        }
    }

    
     // Transforms 'foo': ... into foo: ... whenever possible, saving 2 bytes.
    
    private static void optimizeObjLitMemberDecl(ArrayList tokens) {

        String tv;
        int i, length;
        JavaScriptToken token;

        for (i = 0, length = tokens.count(); i < length; i++) {
            if (((JavaScriptToken) tokens.get(i)).getType() == Token.OBJECTLIT &&
                    i > 0 && ((JavaScriptToken) tokens.get(i - 1)).getType() == Token.STRING) {
                token = (JavaScriptToken) tokens.get(i - 1);
                tv = token.getValue();
                tv = tv.substring(1, tv.length() - 1);
                if (isValidIdentifier(tv)) {
                    tokens.set(i - 1, new JavaScriptToken(Token.NAME, tv));
                }
            }
        }
    }

    private ErrorReporter logger;

    private boolean munge;
    private boolean verbose;

    private static readonly int BUILDING_SYMBOL_TREE = 1;
    private static readonly int CHECKING_SYMBOL_TREE = 2;

    private int mode;
    private int offset;
    private int braceNesting;
    private ArrayList tokens;
    private Stack scopes = new Stack();
    private ScriptOrFnScope globalScope = new ScriptOrFnScope(-1, null);
    private Hashtable indexedScopes = new Hashtable();

    public JavaScriptCompressor(Reader in, ErrorReporter reporter)
            throws IOException, EvaluatorException {

        this.logger = reporter;
        this.tokens = parse(in, reporter);
    }

    public void compress(Writer out, int linebreak, boolean munge, boolean verbose,
            boolean preserveAllSemiColons, boolean disableOptimizations)
            throws IOException {

        this.munge = munge;
        this.verbose = verbose;

        processStringLiterals(this.tokens, !disableOptimizations);

        if (!disableOptimizations) {
            optimizeObjectMemberAccess(this.tokens);
            optimizeObjLitMemberDecl(this.tokens);
        }

        buildSymbolTree();
        // DO NOT TOUCH this.tokens BETWEEN THESE TWO PHASES (BECAUSE OF this.indexedScopes)
        mungeSymboltree();
        StringBuilder sb = printSymbolTree(linebreak, preserveAllSemiColons);

        out.write(sb.toString());
    }

    private ScriptOrFnScope getCurrentScope() {
        return (ScriptOrFnScope) scopes.peek();
    }

    private void enterScope(ScriptOrFnScope scope) {
        scopes.push(scope);
    }

    private void leaveCurrentScope() {
        scopes.pop();
    }

    private JavaScriptToken consumeToken() {
        return (JavaScriptToken) tokens.get(offset++);
    }

    private JavaScriptToken getToken(int delta) {
        return (JavaScriptToken) tokens.get(offset + delta);
    }

   
     //* Returns the identifier for the specified symbol defined in
     //* the specified scope or in any scope above it. Returns null
     //* if this symbol does not have a corresponding identifier.
     
    private JavaScriptIdentifier getIdentifier(String symbol, ScriptOrFnScope scope) {
        JavaScriptIdentifier identifier;
        while (scope != null) {
            identifier = scope.getIdentifier(symbol);
            if (identifier != null) {
                return identifier;
            }
            scope = scope.getParentScope();
        }
        return null;
    }

   
     //* If either 'eval' or 'with' is used in a local scope, we must make
     //* sure that all containing local scopes don't get munged. Otherwise,
     //* the obfuscation would potentially introduce bugs.
     
    private void protectScopeFromObfuscation(ScriptOrFnScope scope) {
        assert scope != null;

        if (scope == globalScope) {
            // The global scope does not get obfuscated,
            // so we don't need to worry about it...
            return;
        }

        // Find the highest local scope containing the specified scope.
        while (scope.getParentScope() != globalScope) {
            scope = scope.getParentScope();
        }

        assert scope.getParentScope() == globalScope;
        scope.preventMunging();
    }

    private String getDebugString(int max) {
        assert max > 0;
        StringBuilder result = new StringBuilder();
        int start = Math.max(offset - max, 0);
        int end = Math.min(offset + max, tokens.count());
        for (int i = start; i < end; i++) {
            JavaScriptToken token = (JavaScriptToken) tokens.get(i);
            if (i == offset - 1) {
                result.append(" ---> ");
            }
            result.append(token.getValue());
            if (i == offset - 1) {
                result.append(" <--- ");
            }
        }
        return result.toString();
    }

    private void warn(String message, boolean showDebugString) {
        if (verbose) {
            if (showDebugString) {
                message = message + "\n" + getDebugString(10);
            }
            logger.warning(message, null, -1, null, -1);
        }
    }

    private void parseFunctionDeclaration() {

        String symbol;
        JavaScriptToken token;
        ScriptOrFnScope currentScope, fnScope;
        JavaScriptIdentifier identifier;

        currentScope = getCurrentScope();

        token = consumeToken();
        if (token.getType() == Token.NAME) {
            if (mode == BUILDING_SYMBOL_TREE) {
                // Get the name of the function and declare it in the current scope.
                symbol = token.getValue();
                if (currentScope.getIdentifier(symbol) != null) {
                    warn("The function " + symbol + " has already been declared in the same scope...", true);
                }
                currentScope.declareIdentifier(symbol);
            }
            token = consumeToken();
        }

        assert token.getType() == Token.LP;
        if (mode == BUILDING_SYMBOL_TREE) {
            fnScope = new ScriptOrFnScope(braceNesting, currentScope);
            indexedScopes.Add(new Integer(offset), fnScope);
        } else {
            fnScope = (ScriptOrFnScope) indexedScopes.get(new Integer(offset));
        }

        // Parse function arguments.
        int argpos = 0;
        while ((token = consumeToken()).getType() != Token.RP) {
            assert token.getType() == Token.NAME ||
                    token.getType() == Token.COMMA;
            if (token.getType() == Token.NAME && mode == BUILDING_SYMBOL_TREE) {
                symbol = token.getValue();
                identifier = fnScope.declareIdentifier(symbol);
                if (symbol.equals("$super") && argpos == 0) {
                    // Exception for Prototype 1.6...
                    identifier.preventMunging();
                }
                argpos++;
            }
        }

        token = consumeToken();
        assert token.getType() == Token.LC;
        braceNesting++;

        token = getToken(0);
        if (token.getType() == Token.STRING &&
                getToken(1).getType() == Token.SEMI) {
            // This is a hint. Hints are empty statements that look like
            // "localvar1:nomunge, localvar2:nomunge"; They allow developers
            // to prevent specific symbols from getting obfuscated (some heretic
            // implementations, such as Prototype 1.6, require specific variable
            // names, such as $super for example, in order to work appropriately.
            // Note: right now, only "nomunge" is supported in the right hand side
            // of a hint. However, in the future, the right hand side may contain
            // other values.
            consumeToken();
            String hints = token.getValue();
            // Remove the leading and trailing quotes...
            hints = hints.substring(1, hints.length() - 1).trim();
            StringTokenizer st1 = new StringTokenizer(hints, ",");
            while (st1.hasMoreTokens()) {
                String hint = st1.nextToken();
                int idx = hint.indexOf(':');
                if (idx <= 0 || idx >= hint.length() - 1) {
                    if (mode == BUILDING_SYMBOL_TREE) {
                        // No need to report the error twice, hence the test...
                        warn("Invalid hint syntax: " + hint, true);
                    }
                    break;
                }
                String variableName = hint.substring(0, idx).trim();
                String variableType = hint.substring(idx + 1).trim();
                if (mode == BUILDING_SYMBOL_TREE) {
                    fnScope.AddHint(variableName, variableType);
                } else if (mode == CHECKING_SYMBOL_TREE) {
                    identifier = fnScope.getIdentifier(variableName);
                    if (identifier != null) {
                        if (variableType.equals("nomunge")) {
                            identifier.preventMunging();
                        } else {
                            warn("Unsupported hint value: " + hint, true);
                        }
                    } else {
                        warn("Hint refers to an unknown identifier: " + hint, true);
                    }
                }
            }
        }

        parseScope(fnScope);
    }

    private void parseCatch() {

        String symbol;
        JavaScriptToken token;
        ScriptOrFnScope currentScope;
        JavaScriptIdentifier identifier;

        token = getToken(-1);
        assert token.getType() == Token.CATCH;
        token = consumeToken();
        assert token.getType() == Token.LP;
        token = consumeToken();
        assert token.getType() == Token.NAME;

        symbol = token.getValue();
        currentScope = getCurrentScope();

        if (mode == BUILDING_SYMBOL_TREE) {
            // We must declare the exception identifier in the containing function
            // scope to avoid errors related to the obfuscation process. No need to
            // display a warning if the symbol was already declared here...
            currentScope.declareIdentifier(symbol);
        } else {
            identifier = getIdentifier(symbol, currentScope);
            identifier.incrementRefcount();
        }

        token = consumeToken();
        assert token.getType() == Token.RP;
    }

    private void parseExpression() {

        // Parse the expression until we encounter a comma or a semi-colon
        // in the same brace nesting, bracket nesting and paren nesting.
        // Parse functions if any...

        String symbol;
        JavaScriptToken token;
        ScriptOrFnScope currentScope;
        JavaScriptIdentifier identifier;

        int expressionBraceNesting = braceNesting;
        int bracketNesting = 0;
        int parensNesting = 0;

        int length = tokens.count();

        while (offset < length) {

            token = consumeToken();
            currentScope = getCurrentScope();

            switch (token.getType()) {

                case Token.SEMI:
                case Token.COMMA:
                    if (braceNesting == expressionBraceNesting &&
                            bracketNesting == 0 &&
                            parensNesting == 0) {
                        return;
                    }
                    break;

                case Token.FUNCTION:
                    parseFunctionDeclaration();
                    break;

                case Token.LC:
                    braceNesting++;
                    break;

                case Token.RC:
                    braceNesting--;
                    assert braceNesting >= expressionBraceNesting;
                    break;

                case Token.LB:
                    bracketNesting++;
                    break;

                case Token.RB:
                    bracketNesting--;
                    break;

                case Token.LP:
                    parensNesting++;
                    break;

                case Token.RP:
                    parensNesting--;
                    break;

                case Token.CONDCOMMENT:
                    if (mode == BUILDING_SYMBOL_TREE) {
                        protectScopeFromObfuscation(currentScope);
                        warn("Using JScript conditional comments is not recommended." + (munge ? " Moreover, using JScript conditional comments reduces the level of compression!" : ""), true);
                    }
                    break;

                case Token.NAME:
                    symbol = token.getValue();

                    if (mode == BUILDING_SYMBOL_TREE) {

                        if (symbol.equals("eval")) {

                            protectScopeFromObfuscation(currentScope);
                            warn("Using 'eval' is not recommended." + (munge ? " Moreover, using 'eval' reduces the level of compression!" : ""), true);

                        }

                    } else if (mode == CHECKING_SYMBOL_TREE) {

                        if ((offset < 2 ||
                                (getToken(-2).getType() != Token.DOT &&
                                        getToken(-2).getType() != Token.GET &&
                                        getToken(-2).getType() != Token.SET)) &&
                                getToken(0).getType() != Token.OBJECTLIT) {

                            identifier = getIdentifier(symbol, currentScope);

                            if (identifier == null) {

                                if (symbol.length() <= 3 && !builtin.contains(symbol)) {
                                    // Here, we found an undeclared and un-namespaced symbol that is
                                    // 3 characters or less in length. Declare it in the global scope.
                                    // We don't need to declare longer symbols since they won't cause
                                    // any conflict with other munged symbols.
                                    globalScope.declareIdentifier(symbol);
                                    warn("Found an undeclared symbol: " + symbol, true);
                                }

                            } else {

                                identifier.incrementRefcount();
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void parseScope(ScriptOrFnScope scope) {

        String symbol;
        JavaScriptToken token;
        JavaScriptIdentifier identifier;

        int length = tokens.count();

        enterScope(scope);

        while (offset < length) {

            token = consumeToken();

            switch (token.getType()) {

                case Token.VAR:

                    if (mode == BUILDING_SYMBOL_TREE && scope.incrementVarCount() > 1) {
                        warn("Try to use a single 'var' statement per scope.", true);
                    }

                    // FALLSTHROUGH 

                case Token.CONST:

                    // The var keyword is followed by at least one symbol name.
                    // If several symbols follow, they are comma separated.
                    for (; ;) {
                        token = consumeToken();

                        assert token.getType() == Token.NAME;

                        if (mode == BUILDING_SYMBOL_TREE) {
                            symbol = token.getValue();
                            if (scope.getIdentifier(symbol) == null) {
                                scope.declareIdentifier(symbol);
                            } else {
                                warn("The variable " + symbol + " has already been declared in the same scope...", true);
                            }
                        }

                        token = getToken(0);

                        assert token.getType() == Token.SEMI ||
                                token.getType() == Token.ASSIGN ||
                                token.getType() == Token.COMMA ||
                                token.getType() == Token.IN;

                        if (token.getType() == Token.IN) {
                            break;
                        } else {
                            parseExpression();
                            token = getToken(-1);
                            if (token.getType() == Token.SEMI) {
                                break;
                            }
                        }
                    }
                    break;

                case Token.FUNCTION:
                    parseFunctionDeclaration();
                    break;

                case Token.LC:
                    braceNesting++;
                    break;

                case Token.RC:
                    braceNesting--;
                    assert braceNesting >= scope.getBraceNesting();
                    if (braceNesting == scope.getBraceNesting()) {
                        leaveCurrentScope();
                        return;
                    }
                    break;

                case Token.WITH:
                    if (mode == BUILDING_SYMBOL_TREE) {
                        // Inside a 'with' block, it is impossible to figure out
                        // statically whether a symbol is a local variable or an
                        // object member. As a consequence, the only thing we can
                        // do is turn the obfuscation off for the highest scope
                        // containing the 'with' block.
                        protectScopeFromObfuscation(scope);
                        warn("Using 'with' is not recommended." + (munge ? " Moreover, using 'with' reduces the level of compression!" : ""), true);
                    }
                    break;

                case Token.CATCH:
                    parseCatch();
                    break;

                case Token.CONDCOMMENT:
                    if (mode == BUILDING_SYMBOL_TREE) {
                        protectScopeFromObfuscation(scope);
                        warn("Using JScript conditional comments is not recommended." + (munge ? " Moreover, using JScript conditional comments reduces the level of compression." : ""), true);
                    }
                    break;

                case Token.NAME:
                    symbol = token.getValue();

                    if (mode == BUILDING_SYMBOL_TREE) {

                        if (symbol.equals("eval")) {

                            protectScopeFromObfuscation(scope);
                            warn("Using 'eval' is not recommended." + (munge ? " Moreover, using 'eval' reduces the level of compression!" : ""), true);

                        }

                    } else if (mode == CHECKING_SYMBOL_TREE) {

                        if ((offset < 2 || getToken(-2).getType() != Token.DOT) &&
                                getToken(0).getType() != Token.OBJECTLIT) {

                            identifier = getIdentifier(symbol, scope);

                            if (identifier == null) {

                                if (symbol.length() <= 3 && !builtin.contains(symbol)) {
                                    // Here, we found an undeclared and un-namespaced symbol that is
                                    // 3 characters or less in length. Declare it in the global scope.
                                    // We don't need to declare longer symbols since they won't cause
                                    // any conflict with other munged symbols.
                                    globalScope.declareIdentifier(symbol);
                                    warn("Found an undeclared symbol: " + symbol, true);
                                }

                            } else {

                                identifier.incrementRefcount();
                            }
                        }
                    }
                    break;
            }
        }
    }

    private void buildSymbolTree() {
        offset = 0;
        braceNesting = 0;
        scopes.clear();
        indexedScopes.clear();
        indexedScopes.Add(new Integer(0), globalScope);
        mode = BUILDING_SYMBOL_TREE;
        parseScope(globalScope);
    }

    private void mungeSymboltree() {

        if (!munge) {
            return;
        }

        // One problem with obfuscation resides in the use of undeclared
        // and un-namespaced global symbols that are 3 characters or less
        // in length. Here is an example:
        //
        //     var declaredGlobalVar;
        //
        //     function declaredGlobalFn() {
        //         var localvar;
        //         localvar = abc; // abc is an undeclared global symbol
        //     }
        //
        // In the example above, there is a slim chance that localvar may be
        // munged to 'abc', conflicting with the undeclared global symbol
        // abc, creating a potential bug. The following code detects such
        // global symbols. This must be done AFTER the entire file has been
        // parsed, and BEFORE munging the symbol tree. Note that declaring
        // extra symbols in the global scope won't hurt.
        //
        // Note: Since we go through all the tokens to do this, we also use
        // the opportunity to count how many times each identifier is used.

        offset = 0;
        braceNesting = 0;
        scopes.clear();
        mode = CHECKING_SYMBOL_TREE;
        parseScope(globalScope);
        globalScope.munge();
    }

    private StringBuilder printSymbolTree(int linebreakpos, boolean preserveAllSemiColons)
            throws IOException {

        offset = 0;
        braceNesting = 0;
        scopes.clear();

        String symbol;
        JavaScriptToken token;
        ScriptOrFnScope currentScope;
        JavaScriptIdentifier identifier;

        int length = tokens.count();
        StringBuilder result = new StringBuilder();

        int linestartpos = 0;

        enterScope(globalScope);

        while (offset < length) {

            token = consumeToken();
            symbol = token.getValue();
            currentScope = getCurrentScope();

            switch (token.getType()) {

                case Token.NAME:

                    if (offset >= 2 && getToken(-2).getType() == Token.DOT ||
                            getToken(0).getType() == Token.OBJECTLIT) {

                        result.append(symbol);

                    } else {

                        identifier = getIdentifier(symbol, currentScope);
                        if (identifier != null) {
                            if (identifier.getMungedValue() != null) {
                                result.append(identifier.getMungedValue());
                            } else {
                                result.append(symbol);
                            }
                            if (currentScope != globalScope && identifier.getRefcount() == 0) {
                                warn("The symbol " + symbol + " is declared but is apparently never used.\nThis code can probably be written in a more compact way.", true);
                            }
                        } else {
                            result.append(symbol);
                        }
                    }
                    break;

                case Token.REGEXP:
                case Token.NUMBER:
                case Token.STRING:
                    result.append(symbol);
                    break;

                case Token.Add:
                case Token.SUB:
                    result.append((String) literals.get(new Integer(token.getType())));
                    if (offset < length) {
                        token = getToken(0);
                        if (token.getType() == Token.INC ||
                                token.getType() == Token.DEC ||
                                token.getType() == Token.Add ||
                                token.getType() == Token.DEC) {
                            // Handle the case x +/- ++/-- y
                            // We must keep a white space here. Otherwise, x +++ y would be
                            // interpreted as x ++ + y by the compiler, which is a bug (due
                            // to the implicit assignment being done on the wrong variable)
                            result.append(' ');
                        } else if (token.getType() == Token.POS && getToken(-1).getType() == Token.Add ||
                                token.getType() == Token.NEG && getToken(-1).getType() == Token.SUB) {
                            // Handle the case x + + y and x - - y
                            result.append(' ');
                        }
                    }
                    break;

                case Token.FUNCTION:
                    result.append("function");
                    token = consumeToken();
                    if (token.getType() == Token.NAME) {
                        result.append(' ');
                        symbol = token.getValue();
                        identifier = getIdentifier(symbol, currentScope);
                        assert identifier != null;
                        if (identifier.getMungedValue() != null) {
                            result.append(identifier.getMungedValue());
                        } else {
                            result.append(symbol);
                        }
                        if (currentScope != globalScope && identifier.getRefcount() == 0) {
                            warn("The symbol " + symbol + " is declared but is apparently never used.\nThis code can probably be written in a more compact way.", true);
                        }
                        token = consumeToken();
                    }
                    assert token.getType() == Token.LP;
                    result.append('(');
                    currentScope = (ScriptOrFnScope) indexedScopes.get(new Integer(offset));
                    enterScope(currentScope);
                    while ((token = consumeToken()).getType() != Token.RP) {
                        assert token.getType() == Token.NAME || token.getType() == Token.COMMA;
                        if (token.getType() == Token.NAME) {
                            symbol = token.getValue();
                            identifier = getIdentifier(symbol, currentScope);
                            assert identifier != null;
                            if (identifier.getMungedValue() != null) {
                                result.append(identifier.getMungedValue());
                            } else {
                                result.append(symbol);
                            }
                        } else if (token.getType() == Token.COMMA) {
                            result.append(',');
                        }
                    }
                    result.append(')');
                    token = consumeToken();
                    assert token.getType() == Token.LC;
                    result.append('{');
                    braceNesting++;
                    token = getToken(0);
                    if (token.getType() == Token.STRING &&
                            getToken(1).getType() == Token.SEMI) {
                        // This is a hint. Skip it!
                        consumeToken();
                        consumeToken();
                    }
                    break;

                case Token.RETURN:
                case Token.TYPEOF:
                    result.append(literals.get(new Integer(token.getType())));
                    // No space needed after 'return' and 'typeof' when followed
                    // by '(', '[', '{', a string or a regexp.
                    if (offset < length) {
                        token = getToken(0);
                        if (token.getType() != Token.LP &&
                                token.getType() != Token.LB &&
                                token.getType() != Token.LC &&
                                token.getType() != Token.STRING &&
                                token.getType() != Token.REGEXP &&
                                token.getType() != Token.SEMI) {
                            result.append(' ');
                        }
                    }
                    break;

                case Token.CASE:
                case Token.THROW:
                    result.append(literals.get(new Integer(token.getType())));
                    // White-space needed after 'case' and 'throw' when not followed by a string.
                    if (offset < length && getToken(0).getType() != Token.STRING) {
                        result.append(' ');
                    }
                    break;

                case Token.BREAK:
                case Token.CONTINUE:
                    result.append(literals.get(new Integer(token.getType())));
                    if (offset < length && getToken(0).getType() != Token.SEMI) {
                        // If 'break' or 'continue' is not followed by a semi-colon, it must
                        // be followed by a label, hence the need for a white space.
                        result.append(' ');
                    }
                    break;

                case Token.LC:
                    result.append('{');
                    braceNesting++;
                    break;

                case Token.RC:
                    result.append('}');
                    braceNesting--;
                    assert braceNesting >= currentScope.getBraceNesting();
                    if (braceNesting == currentScope.getBraceNesting()) {
                        leaveCurrentScope();
                    }
                    break;

                case Token.SEMI:
                    // No need to output a semi-colon if the next character is a right-curly...
                    if (preserveAllSemiColons || offset < length && getToken(0).getType() != Token.RC) {
                        result.append(';');
                    }

                    if (linebreakpos >= 0 && result.length() - linestartpos > linebreakpos) {
                        // Some source control tools don't like it when files containing lines longer
                        // than, say 8000 characters, are checked in. The linebreak option is used in
                        // that case to split long lines after a specific column.
                        result.append('\n');
                        linestartpos = result.length();
                    }
                    break;

                case Token.CONDCOMMENT:
                case Token.KEEPCOMMENT:
                    if (result.length() > 0 && result.charAt(result.length() - 1) != '\n') {
                        result.append("\n");
                    }
                    result.append("/*");
                    result.append(symbol);
                    result.append("*\\/\\n");
                    break;

                default:
                    String literal = (String) literals.get(new Integer(token.getType()));
                    if (literal != null) {
                        result.append(literal);
                    } else {
                        warn("This symbol cannot be printed: " + symbol, true);
                    }
                    break;
            }
        }

        // Append a semi-colon at the end, even if unnecessary semi-colons are
        // supposed to be removed. This is especially useful when concatenating
        // several minified files (the absence of an ending semi-colon at the
        // end of one file may very likely cause a syntax error)
        if (!preserveAllSemiColons &&
                result.length() > 0 &&
                getToken(-1).getType() != Token.CONDCOMMENT &&
                getToken(-1).getType() != Token.KEEPCOMMENT) {
            if (result.charAt(result.length() - 1) == '\n') {
                result.setCharAt(result.length() - 1, ';');
            } else {
                result.append(';');
            }
        }

        return result;
    }
}
*/