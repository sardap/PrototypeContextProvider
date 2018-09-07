using System;
using System.Text;
using System.Security.Cryptography;

namespace PrototypeContexProvider.src
{
	public class KeyCreator
	{
		public string CreateKey(int numBytes)
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[numBytes];

			rng.GetBytes(buff);
			return BytesToHexString(buff);
		}

		private string BytesToHexString(byte[] bytes)
		{
			StringBuilder hexString = new StringBuilder(64);

			for (int counter = 0; counter < bytes.Length; counter++)
			{
				hexString.Append(string.Format("{0:X2}", bytes[counter]));
			}
			return hexString.ToString();
		}
	}
}
