using System.ServiceModel.Channels;
using BasicAuthenticationUsingWCF;
using NUnit.Framework;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class HttpRequestAuthorizationExtractorTest
   {
      [Test]
      public void ShouldExtractBasicAuthorizationHeaderIfExists()
      {
         const string AuthorizationHeader = "Basic SGVsbG8gQmFzZTY0";
         Message dummyMessage = CreateRequestMessage(AuthorizationHeader);
         var extractor = new AuthorizationStringExtractor();
         string returnedAuthorizationHeader;
         bool result = extractor.TryExtractAuthorizationHeader(dummyMessage, out returnedAuthorizationHeader);
         Assert.That(returnedAuthorizationHeader, Is.EqualTo(AuthorizationHeader));
         Assert.That(result, Is.True);
      }

      [Test]
      public void ShouldValidateIfAuthorizationHeaderExists()
      {
         Message dummyMessage = CreateNormalRequestMessage();
         var extractor = new AuthorizationStringExtractor();
         string returnedAuthorizationHeader;
         bool result = extractor.TryExtractAuthorizationHeader(dummyMessage, out returnedAuthorizationHeader);
         Assert.That(result, Is.False);
      }

      private static Message CreateRequestMessage(string authorizationheaderToAdd)
      {
         const string BasicAuthenticationHeaderName = "Authorization";
         Message requestMessage = Message.CreateMessage(MessageVersion.None, null);
         var requestProperty = new HttpRequestMessageProperty();
         requestProperty.Headers.Add(BasicAuthenticationHeaderName, authorizationheaderToAdd);
         requestMessage.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
         return requestMessage;
      }

      private static Message CreateNormalRequestMessage()
      {
         var requestProperty = new HttpRequestMessageProperty();
         Message requestMessage = Message.CreateMessage(MessageVersion.None, null);
         requestMessage.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
         return requestMessage;
      }
   }
}