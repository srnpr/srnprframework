<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfigPage.aspx.cs" Inherits="Test_ConfigPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <script type="text/javascript" src="/ckeditor/ckeditor.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
  
		<p>
			<label for="editor1">
				Editor 1:</label><br />
			<textarea cols="80" id="editor1" name="editor1" rows="10">&lt;html&gt;&lt;head&gt;&lt;title&gt;CKEditor Sample&lt;/title&gt;&lt;/head&gt;&lt;body&gt;&lt;p&gt;This is some &lt;strong&gt;sample text&lt;/strong&gt;. You are using &lt;a href="http://ckeditor.com/"&gt;CKEditor&lt;/a&gt;.&lt;/p&gt;&lt;/body&gt;&lt;/html&gt;</textarea>
			<script type="text/javascript">
			    //<![CDATA[

			    var editor = CKEDITOR.replace('editor1');





			    //]]>
			</script>
		</p>
    </div>
    </form>
</body>
</html>
