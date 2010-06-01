using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Microsoft.CSharp;
using System.Web.Services.Description;
using System.Reflection;
using System.CodeDom.Compiler;
using System.CodeDom;

namespace SrnprWeb.CommonFunction
{
    /// <summary>
    /// 
    /// </summary>
    public class GetWebServicesWCF
    {

        static SortedList<string, Type> _typeList = new SortedList<string, Type>();

        #region InvokeWebService

        /// <summary>
        /// 取得缓存Key
        /// </summary>
        /// <param name="url"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        static string GetCacheKey(string url, string className)
        {
            return url.ToLower() + className;
        }

        /// <summary>
        /// 取得缓存
        /// </summary>
        /// <param name="url"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        static Type GetTypeFromCache(string url, string className)
        {
            string key = GetCacheKey(url, className);
            foreach (KeyValuePair<string, Type> pair in _typeList)
            {
                if (key == pair.Key)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// 得到WebService类型
        /// </summary>
        /// <param name="url"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        static Type GetTypeFromWebService(string url, string className)
        {
            string @namespace = "GetWebServicesAllServices.WebService.SrALLServices";
            if ((className == null) || (className == ""))
            {
                className = GetWsClassName(url);
            }


            //获取WSDL

            WebRequest wc = WebRequest.Create(url + "?WSDL");
            wc.Proxy = WebRequest.GetSystemWebProxy();

            Stream stream = wc.GetResponse().GetResponseStream();

            ServiceDescription sd = ServiceDescription.Read(stream);
            ServiceDescriptionImporter sdi = new ServiceDescriptionImporter();



            sdi.AddServiceDescription(sd, "", "");
            CodeNamespace cn = new CodeNamespace(@namespace);

            //生成客户端代理类代码
            CodeCompileUnit ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            CSharpCodeProvider csc = new CSharpCodeProvider();
            ICodeCompiler icc = csc.CreateCompiler();

            //设定编译参数
            CompilerParameters cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (true == cr.Errors.HasErrors)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (System.CodeDom.Compiler.CompilerError ce in cr.Errors)
                {
                    sb.Append(ce.ToString());
                    sb.Append(System.Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例，并调用方法
            System.Reflection.Assembly assembly = cr.CompiledAssembly;

            Type t = assembly.GetType(@namespace + "." + className, true, true);

            return t;
        }


        /// <summary>
        /// 动态调用web服务
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string methodName, object[] args)
        {
            return InvokeWebService(url, null, methodName, args);
        }

        /// <summary>
        ///  动态调用web服务
        /// </summary>
        /// <param name="url">URL地址</param>
        /// <param name="className">类名</param>
        /// <param name="methodName">方法</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string className, string methodName, object[] args)
        {
            System.Net.ServicePointManager.Expect100Continue = false;

            Type t = GetTypeFromCache(url, className);
            if (t == null)
            {
                t = GetTypeFromWebService(url, className);

                //添加到缓冲中
                string key = GetCacheKey(url, className);
                _typeList.Add(key, t);
            }

            object obj = Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(methodName);

            return mi.Invoke(obj, args);

        }

        /// <summary>
        /// 取得webservcie
        /// </summary>
        /// <param name="wsUrl"></param>
        /// <returns></returns>
        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }
        #endregion
    }
}
