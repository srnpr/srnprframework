using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Runtime.Serialization;  
using System.Runtime.Serialization.Json;  
using System.IO;  
using System.Text; 

/// <summary>
///GridShowWebServices 的摘要说明
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
//若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。 
// [System.Web.Script.Services.ScriptService]
public class GridShowWebServices : System.Web.Services.WebService
{

    public GridShowWebServices()
    {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    
    [WebMethod]
    public TestUser CreateUser(string name, int age)
    {
        return new TestUser { Name = name, Age = age };
    }



    [WebMethod]

    public string TTT()
    {
        return JSONHelper.Serialize<TestUser>( new TestUser { Name = "aa", Age = 11 });

    }



}

public class TestUser
{
public string Name { get; set; }
public int Age { get; set; }
}



public class JSONHelper
{
    public static string Serialize<T>(T obj)
    {
       
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        MemoryStream ms = new MemoryStream();
        serializer.WriteObject(ms, obj);
        string retVal = Encoding.UTF8.GetString(ms.ToArray());
        return retVal;
    }
    public static T Deserialize<T>(string json)
    {
        MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
        T obj = (T)serializer.ReadObject(ms);
        ms.Close();
        return obj;
    }
}



public class TestListString
{

    public List<List<string>> ListString { get; set; }
}