using System.ServiceModel.Channels;
using BasicAuthenticationUsingWCF;
using NUnit.Framework;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class HttpHeaderAuthorizationManagerTest
   {
      [Test]
      public void ShouldAddBasicAuthenticationHeaderToHttpResponse()
      {
         const string AuthorizationRealm = "MyAuthorizationRealm";
         var responseMessageFactory = new ResponseMessageFactory(AuthorizationRealm);
         const string AuthorizationHeaderValue = "Basic realm=\"MyAuthorizationRealm\"";
         Message message = responseMessageFactory.CreateInvalidAuthorizationMessage();
         var authenticationRealm = (HttpResponseMessageProperty)message.Properties[HttpResponseMessageProperty.Name];
         const string BasicAuthenticationHeaderName = "WWW-Authenticate";
         Assert.That(authenticationRealm.Headers[BasicAuthenticationHeaderName], Is.EqualTo(AuthorizationHeaderValue));
      }
   }
}