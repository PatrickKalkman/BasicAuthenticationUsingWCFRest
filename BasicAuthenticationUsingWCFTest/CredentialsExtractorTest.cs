using System;
using BasicAuthenticationUsingWCF;
using NUnit.Framework;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class CredentialsExtractorTest
   {
      [Test]
      public void ShouldExtractUserNameAndPasswordFromSecurityString()
      {
         const string UserNameAndPasswordString = "DummyUser:DummyPassword";
         var credentialsExtractor = new DecodedCredentialsExtractor();
         Credentials credentials = credentialsExtractor.Extract(UserNameAndPasswordString);
         Assert.That(credentials.UserName, Is.EqualTo("DummyUser"));
         Assert.That(credentials.Password, Is.EqualTo("DummyPassword"));
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void ShouldThrowArgumentExceptionWhenInvalidCredentialStringIsSupplied()
      {
         const string UserNameAndPasswordString = "sdfsdfsdf";
         var credentialsExtractor = new DecodedCredentialsExtractor();
         credentialsExtractor.Extract(UserNameAndPasswordString);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void ShouldThrowArgumentExceptionWhenNullIsSupplied()
      {
         var credentialsExtractor = new DecodedCredentialsExtractor();
         credentialsExtractor.Extract(null);
      }
   }
}