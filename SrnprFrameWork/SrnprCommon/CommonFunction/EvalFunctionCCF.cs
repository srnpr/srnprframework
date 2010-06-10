using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.CommonFunction
{
    public class EvalFunctionCCF
    {


        public static string Eval(string sExpress)
        {
           
            object ret;
            try
            {
                ret = Microsoft.JScript.Eval.JScriptEvaluate(sExpress, Microsoft.JScript.Vsa.VsaEngine.CreateEngine());
            }
            catch (Exception e)
            {
                ret = e.Message;
            }
            return ret.ToString().Trim();
        }
    }
}
