﻿// BasicAuthenticationUsingWCF a library to add Basic Authenticaion
// to a WCF REST based service
//
// Patrick Kalkman  pkalkie@gmail.com
//
// (C) Patrick Kalkman http://www.semanticarchitecture.net
//
using System;
using System.Text;

namespace BasicAuthenticationUsingWCF
{
   /// <summary>
   /// This class is responsible for decoding a base64 encoded string.
   /// </summary>
   internal class Base64Decoder
   {
      internal virtual string Decode(string encodedValue)
      {
         byte[] decodedStringInBytes = Convert.FromBase64String(encodedValue);
         return Encoding.ASCII.GetString(decodedStringInBytes);
      }
   }
}