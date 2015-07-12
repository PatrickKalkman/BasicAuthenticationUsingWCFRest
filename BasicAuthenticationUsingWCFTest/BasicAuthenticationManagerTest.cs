using System.ServiceModel.Channels;
using System.Web.Security;
using BasicAuthenticationUsingWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class BasicAuthenticationManagerTest
   {
      [Test]
      public void ShouldPositivelyAuthenticateValidRequest()
      {
         var repository = new MockRepository();

         var basicAuthenticationExtractor = repository.StrictMock<BasicAuthenticationCredentialsExtractor>(null, null);

         Message requestMessage = CreateAuthorizedRequestMessage();

         var securityContextFactory = repository.Stub<ServiceSecurityContextFactory>(null);
         Expect.Call(securityContextFactory.Create(null)).IgnoreArguments().Return(null);

         var httpRequestExtractor = repository.StrictMock<AuthorizationStringExtractor>();
         const string BasicAuthenticationCredentialString = "Basic SGVsbG8gQmFzZTY0";
         string dummyString;
         Expect.Call(httpRequestExtractor.TryExtractAuthorizationHeader(requestMessage, out dummyString)).Return(true).OutRef(BasicAuthenticationCredentialString);

         const string UserName = "DummyUser";
         const string Password = "DummyPassword";
         Credentials dummyCredentials = CreateDummyCredentials(UserName, Password);
         Expect.Call(basicAuthenticationExtractor.Extract(BasicAuthenticationCredentialString)).Return(dummyCredentials);

         var membershipProvider = repository.StrictMock<MembershipProvider>();
         Expect.Call(membershipProvider.ValidateUser(UserName, Password)).Return(true);

         var manager = new BasicAuthenticationManager(basicAuthenticationExtractor, httpRequestExtractor, membershipProvider, null, securityContextFactory);

         repository.ReplayAll();
         manager.AuthenticateRequest(requestMessage);
         repository.VerifyAll();
      }

      [Test]
      public void ShouldGenerateRequestForAuthorization()
      {
         var repository = new MockRepository();

         var basicAuthenticationExtractor = repository.Stub<BasicAuthenticationCredentialsExtractor>(null, null);
         var httpRequestExtractor = repository.Stub<AuthorizationStringExtractor>();
         var membershipProvider = repository.Stub<MembershipProvider>();

         var securityContextFactory = repository.Stub<ServiceSecurityContextFactory>(null);
         Expect.Call(securityContextFactory.Create(null)).IgnoreArguments().Return(null);

         var responseMessageFactory = repository.StrictMock<ResponseMessageFactory>(string.Empty);
         Expect.Call(responseMessageFactory.CreateInvalidAuthorizationMessage()).Return(null);

         var manager = new BasicAuthenticationManager(basicAuthenticationExtractor, httpRequestExtractor, membershipProvider, responseMessageFactory, securityContextFactory);
         repository.ReplayAll();
         manager.CreateInvalidAuthenticationRequest();
         repository.VerifyAll();
      }

      private static Credentials CreateDummyCredentials(string userName, string password)
      {
         return new Credentials(userName, password);
      }

      private static Message CreateAuthorizedRequestMessage()
      {
         const string BasicAuthenticationHeaderName = "Authorization";
         Message requestMessage = Message.CreateMessage(MessageVersion.None, null);
         var requestProperty = new HttpRequestMessageProperty();
         const string BasicAuthenticationHeaderValue = "Basic SGVsbG8gQmFzZTY0";
         requestProperty.Headers.Add(BasicAuthenticationHeaderName, BasicAuthenticationHeaderValue);
         requestMessage.Properties.Add(HttpRequestMessageProperty.Name, requestProperty);
         return requestMessage;
      }
   }
}