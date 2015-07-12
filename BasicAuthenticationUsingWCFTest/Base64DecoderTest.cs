using BasicAuthenticationUsingWCF;
using NUnit.Framework;

namespace BasicAuthenticationUsingWCFTest
{
   [TestFixture]
   public class Base64DecoderTest
   {
      [Test]
      public void ShouldDecodeBase64String()
      {
         const string Base64EncodedData = "SGVsbG8gQmFzZTY0";
         const string DecodedData = "Hello Base64";

         var base64Decoder = new Base64Decoder();
         string result = base64Decoder.Decode(Base64EncodedData);

         Assert.That(DecodedData, Is.EqualTo(result));
      }
   }
}
