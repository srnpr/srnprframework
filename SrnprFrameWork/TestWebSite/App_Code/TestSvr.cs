using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class TestSvr
{
    // 添加 [WebGet] 属性以使用 HTTP GET
    [OperationContract]
    public void DoWork()
    {
        // 在此处添加操作实现
        return;
    }

    // 在此处添加更多操作并使用 [OperationContract] 标记它们


    
}



