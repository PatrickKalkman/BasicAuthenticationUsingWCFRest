/// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Activation;

// The following line sets the default namespace for DataContract serialized typed to be ""
[assembly: ContractNamespace("", ClrNamespace = "WCFServer")]

namespace WCFServer
{
   // TODO: Please set IncludeExceptionDetailInFaults to false in production environments
   [ServiceBehavior(IncludeExceptionDetailInFaults = true), AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed), ServiceContract]
   public partial class Service
   {
      [WebGet(UriTemplate = "DoWork")]
      [OperationContract]
      public string DoWork()
      {
         string name = ServiceSecurityContext.Current.PrimaryIdentity.Name;
         return "Hello World " + name;
      }
   }
}