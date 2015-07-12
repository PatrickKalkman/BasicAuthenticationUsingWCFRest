using System.Security.Principal;
using System.ServiceModel;
using BasicAuthenticationUsingWCF;
using NUnit.Framework;
using Rhino.Mocks;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class ServiceSecurityContextFactoryTest
   {
      [Test]
      public void ShouldCreateSecurityContextFromCredentials()
      {
         var mockRepository = new MockRepository();

         const string UserName = "DummyUser";
         const string Password = "DummyPassword";
         var credentials = new Credentials(UserName, Password);

         var genericPrincipal = mockRepository.Stub<IPrincipal>();
         Expect.Call(genericPrincipal.Identity.Name).Return(UserName);
         
         var authorizationPolicyFactory = mockRepository.StrictMock<AuthorizationPolicyFactory>();
         Expect.Call(authorizationPolicyFactory.Create(credentials)).Return(new PrincipalAuthorizationPolicy(genericPrincipal));

         mockRepository.ReplayAll();
         var serviceSecurityContextFactory = new ServiceSecurityContextFactory(authorizationPolicyFactory);
         ServiceSecurityContext serviceSecurityContext = serviceSecurityContextFactory.Create(credentials);
         Assert.AreEqual(UserName, serviceSecurityContext.PrimaryIdentity.Name);
         mockRepository.VerifyAll();
      }
   }
}