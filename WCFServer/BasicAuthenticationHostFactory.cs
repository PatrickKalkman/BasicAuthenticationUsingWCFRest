using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BasicAuthenticationUsingWCF;
using Microsoft.ServiceModel.Web;

namespace WCFServer
{
   /// <summary>
   /// This class is responsible for creating a servicehost that includes a basic 
   /// authentication request interceptor.
   /// </summary>
   public class BasicAuthenticationHostFactory : ServiceHostFactory
   {
      protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
      {
         var serviceHost = new WebServiceHost2(serviceType, true, baseAddresses);
         serviceHost.Interceptors.Add(RequestInterceptorFactory.Create("DataWebService", new CustomMembershipProvider()));
         return serviceHost;
      }
   }
}