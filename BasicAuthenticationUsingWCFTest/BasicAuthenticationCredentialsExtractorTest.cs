using BasicAuthenticationUsingWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class BasicAuthenticationCredentialsExtractorTest
   {
      [Test]
      public void ShouldExtractCredentialsFromBasicAuthenticationString()
      {
         var mockRepository = new MockRepository();
         const string BasicAuthenticationCredentialString = "Basic SGVsbG8gQmFzZTY0";

         string authenticationStringOnly = BasicAuthenticationCredentialString.Replace("Basic", string.Empty);

         const string DecodedCredentialString = "DummyUser:DummyPassword";
         var base64Decoder = mockRepository.StrictMock<Base64Decoder>();
         Expect.Call(base64Decoder.Decode(authenticationStringOnly)).Return(DecodedCredentialString);

         var credentials = new Credentials("DummyUser", "DummyPassword");
         var credentialsExtractor = mockRepository.StrictMock<DecodedCredentialsExtractor>();
         Expect.Call(credentialsExtractor.Extract(DecodedCredentialString)).Return(credentials);

         var basicCredentialsExtractor = new BasicAuthenticationCredentialsExtractor(base64Decoder, credentialsExtractor);
         mockRepository.ReplayAll();
         basicCredentialsExtractor.Extract(BasicAuthenticationCredentialString);
         mockRepository.VerifyAll();
      }
   }
}
