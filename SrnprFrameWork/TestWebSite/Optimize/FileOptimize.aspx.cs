using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;


public partial class Optimize_FileOptimize : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBegin_Click(object sender, EventArgs e)
    {
        new Optimize().Begin(tbFrom.Text, tbTo.Text);
    }
}


public class Optimize
{
    public void Begin(string sFromDir, string sToDir)
    {
        List<FileInfo> allFile = new List<FileInfo>();
        if (Directory.Exists(sFromDir))
        {
            DirectoryInfo dFrom=new DirectoryInfo(sFromDir);

            
            GetAllFileByDirectory(dFrom, allFile,"|.js|.css|");


        }



        Regex reDouble = new Regex("//.*",RegexOptions.Multiline);

        foreach (FileInfo f in allFile)
        {
            string sDir =sToDir+ f.DirectoryName.Replace(sFromDir, "");
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }


            string sFileString = GetOptimizeString(f.OpenText().ReadToEnd());




            
            //sFileString = Regex.Replace(sFileString,"(?<={|;|}|\\))\\s+","");








            //FileInfo fNe = new FileInfo(sDir + "//" + f.Name);

            
            //fNe.CreateText().Write(sFileString);

            FileStream fs = new FileStream(sDir + "//" + f.Name, FileMode.Create, FileAccess.ReadWrite);
            byte[] byteString=Encoding.UTF8.GetBytes(sFileString);
            fs.Write(byteString, 0, byteString.Length);
            fs.Close();
            


        }


    }

    public string GetOptimizeString(string sFileString)
    {
        sFileString = Regex.Replace(sFileString, "//.*\r\n", "\r\n");
        sFileString = Regex.Replace(sFileString, "/\\*.*?\\*/", "",RegexOptions.Singleline);


        

        //sFileString = Regex.Replace(sFileString, "\r\n", "");

        string[] strString = sFileString.Split('"');



        for (int i = 0, j = strString.Length; i < j; i = i + 2)
        {
            strString[i] = Regex.Replace(strString[i], "\\s*(;|{|}|\\(|\\)|,|\\=|[|]|\\+|\\-|==|:|>|<)\\s*", "$1");
        }

        sFileString = string.Join("\"", strString);
        sFileString = Regex.Replace(sFileString, "\r\n", "");



        return sFileString;
    }

    public string GetO(string sFS)
    {
        //去掉所有注释
        sFS = Regex.Replace(sFS, "//.*\r\n", "\r\n");
        sFS = Regex.Replace(sFS, "/\\*.*?\\*/", "", RegexOptions.Singleline);

        string[] strFS = Regex.Split(sFS, "\r\n", RegexOptions.IgnoreCase);


       

        MyJs m = new MyJs();
        





            return sFS;
    }


    public void SetJs(MyJs m, int iIndex,string[] strFS)
    {

    }




    public void GetAllFileByDirectory(DirectoryInfo d, List<FileInfo> MyFile, string sExt)
    {

        foreach (FileInfo f in d.GetFiles())
        {

            if (sExt.IndexOf("|"+f.Extension.ToLower()+"|")>-1)
            {
                MyFile.Add(f);
            }

        }

        foreach (DirectoryInfo di in d.GetDirectories())
        {
            GetAllFileByDirectory(di, MyFile,sExt);
        }
        

    }






}


public class MyJs
{
    public int Dept { get; set; }


   public JsType Type { get; set; }

    public List<MyJs> Child { get; set; }


}


public enum JsType
{
    Function,
    Var
}


