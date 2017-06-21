using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using System.Diagnostics;

namespace DataTier.Utils
{
    public static class SimpleCtyptoLibrary
    {
		private const string User = "user54";
		private const string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQC2VKy0OMXoFvuxGeP/n92VV3wIt2X/kIG2BhuY6WE+SrvUOuxR
4hH5FT7fFWR0kVPBHJmUwwu8egJo+D7UyYF0d7A0UjVzFL1t02OsPcUnIXWs0PlO
Nz+nbhDDB//IWyR5iJejwCrZt0fBpISPmlSxyjp+uThtdPX1JtSQVv7iHQIDAQAB
AoGBAJIkIl09mBsjuM9F0kKEr4VRHsCZxy5ldCIimSIiBWh5XD2KkPo8um0sQz1p
lx/7j+cb9lmPUCvcm2vpder2LESA+rVoLqMpTQFh1ynLAjYXT3HTN8ZRRxBYY3mA
Fg/UbtjSB098GFWH4AV3LOGPfYhNrsVsiuhz0nQX7ADYIjchAkEAuz1iEYCRyBJa
DkCJsIO6gbu8A4ezuAm00ppbcRHxlILDEarG5ABdo8X3Q7Sx5Vh69GBpD++6hMO2
0UzOEIvQ1QJBAPlJyhhVk79wN3bdv3VF5w7sHhV2Hl5gSR1SmD+r0+1oJVAY8iZ+
GKLlshK1JVw2x7B2SNsYKfDPj3idORDosCkCQQCmpMMbgKo+vtaXyKjDCPp9bHCx
U52INltQ9UBdKfMwkhC7MJtDYW/1ysN+5ttNm6oSxZu8K0h90RJsxUbBQy7hAkEA
8+MKMhZ/TvrFeKhnqJ8z9/hvUkXWXjTLM0HcK+a6lvieEKfnOFuDVNNuDTlmDLqX
UP/YNWmFltAqKDGBZBaSSQJAJI7KrB9m/C874oxqv54izkfKwjCpoD/OvZ0h61Yl
1e7E1sB495nH617WpM1fFEqAuZUgdhb33VGkty1xFsqyxQ==
-----END RSA PRIVATE KEY-----
";

		/// <summary>
		/// Returns the authentication token of the given username and private key
		/// </summary>
		/// <param name="username"></param>
		/// <param name="privateKey"></param>
		/// <returns>authentication token</returns>
		public static string CreateToken(int nonce)
        {
            return RSASignWithSHA256(User+"_"+nonce, PrivateKey);
        }


        /// <summary>
        /// This method extract the private ket from PEM format string
        /// </summary>
        /// <param name="privateKey">the private key in a PEM format</param>
        /// <returns>PrivateKey for RSACryptoServiceProvider</returns>
        private static RSAParameters ExtractRSAPrivateKey(string privateKey)
        {
            using (var txtreader = new StringReader(privateKey))
            {
                var keyPair = (AsymmetricCipherKeyPair)new PemReader(txtreader).ReadObject();
                RsaPrivateCrtKeyParameters rsaPrivKey=(RsaPrivateCrtKeyParameters)keyPair.Private;
                RSAParameters rsaKeyInfo = Org.BouncyCastle.Security.DotNetUtilities.ToRSAParameters(rsaPrivKey);
                return rsaKeyInfo;
            }
        }

        /// <summary>
        /// Sign on a message using SHA256 and RSA, the signed value is encoded in base64 format
        /// </summary>
        /// <param name="message">to sign</param>
        /// <param name="privateKey">of RSA in a PEM format</param>
        /// <returns>the signed value encoded in base64 format</returns>
        private static string RSASignWithSHA256(string message, string privateKey)
        {
            RSACryptoServiceProvider rsaAlgo = new RSACryptoServiceProvider();
            rsaAlgo.ImportParameters(ExtractRSAPrivateKey(privateKey));
            return Convert.ToBase64String(rsaAlgo.SignData(Encoding.UTF8.GetBytes(message), "SHA256"));
        }

		public static string Decrypt(string message)
		{
			RSACryptoServiceProvider rsaAlgo = new RSACryptoServiceProvider();
			rsaAlgo.ImportParameters(ExtractRSAPrivateKey(PrivateKey));
			byte[] encrypted = Convert.FromBase64String(message);
			StringBuilder decrypted = new StringBuilder();
			Trace.WriteLine(message);
			for (int i = 0; i<encrypted.Length; i+=128)
			{
				byte[] block = new byte[128];
				Array.Copy(encrypted, i, block, 0, Math.Min(encrypted.Length-i, 128));
				String decblock = Encoding.ASCII.GetString(rsaAlgo.Decrypt(block, false));
				decrypted.Append(decblock);
			}

			return decrypted.ToString();
		}
	}
}