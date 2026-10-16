using System;
using System.Security.Cryptography;

namespace Trivial.Mono.Security.Cryptography
{
	// Token: 0x02000008 RID: 8
	internal static class CryptoConvert
	{
		// Token: 0x0600000E RID: 14 RVA: 0x000020EC File Offset: 0x000002EC
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return ((int)bytes[offset + 3] << 24) | ((int)bytes[offset + 2] << 16) | ((int)bytes[offset + 1] << 8) | (int)bytes[offset];
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)(((int)bytes[offset + 3] << 24) | ((int)bytes[offset + 2] << 16) | ((int)bytes[offset + 1] << 8) | (int)bytes[offset]);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000210C File Offset: 0x0000030C
		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002148 File Offset: 0x00000348
		private static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 843141970U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse(array);
				rsaparameters.Exponent = CryptoConvert.Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				rsaparameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, rsaparameters.Modulus, 0, num3);
				Array.Reverse(rsaparameters.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				rsaparameters.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.P, 0, num4);
				Array.Reverse(rsaparameters.P);
				num2 += num4;
				rsaparameters.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.Q, 0, num4);
				Array.Reverse(rsaparameters.Q);
				num2 += num4;
				rsaparameters.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.DP, 0, num4);
				Array.Reverse(rsaparameters.DP);
				num2 += num4;
				rsaparameters.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.DQ, 0, num4);
				Array.Reverse(rsaparameters.DQ);
				num2 += num4;
				rsaparameters.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.InverseQ, 0, num4);
				Array.Reverse(rsaparameters.InverseQ);
				num2 += num4;
				rsaparameters.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, rsaparameters.D, 0, num3);
					Array.Reverse(rsaparameters.D);
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			RSA rsa = null;
			RSA rsa2;
			try
			{
				rsa = RSA.Create();
				rsa.ImportParameters(rsaparameters);
				rsa2 = rsa;
			}
			catch (CryptographicException)
			{
				bool flag = false;
				try
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = 1
					});
					rsa.ImportParameters(rsaparameters);
				}
				catch
				{
					flag = true;
				}
				if (flag)
				{
					throw;
				}
				rsa2 = rsa;
			}
			return rsa2;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023D8 File Offset: 0x000005D8
		private static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			RSA rsa2;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 826364754U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				RSAParameters rsaparameters = default(RSAParameters);
				rsaparameters.Exponent = new byte[3];
				RSAParameters rsaparameters2 = rsaparameters;
				rsaparameters2.Exponent[0] = blob[offset + 18];
				rsaparameters2.Exponent[1] = blob[offset + 17];
				rsaparameters2.Exponent[2] = blob[offset + 16];
				int num2 = offset + 20;
				int num3 = num >> 3;
				rsaparameters2.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, rsaparameters2.Modulus, 0, num3);
				Array.Reverse(rsaparameters2.Modulus);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(rsaparameters2);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = 1
					});
					rsa.ImportParameters(rsaparameters2);
				}
				rsa2 = rsa;
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			return rsa2;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002500 File Offset: 0x00000700
		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlob(blob, 0);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000250C File Offset: 0x0000070C
		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b != 0)
			{
				if (b == 6)
				{
					return CryptoConvert.FromCapiPublicKeyBlob(blob, offset);
				}
				if (b == 7)
				{
					return CryptoConvert.FromCapiPrivateKeyBlob(blob, offset);
				}
			}
			else if (blob[offset + 12] == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlob(blob, offset + 12);
			}
			throw new CryptographicException("Unknown blob format.");
		}
	}
}
