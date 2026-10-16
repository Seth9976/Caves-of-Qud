using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Collections;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001F8 RID: 504
	internal static class CryptoBlobParser
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		internal static bool IsValidPublicKey(ImmutableArray<byte> blob)
		{
			if (blob.IsDefault || blob.Length < 13)
			{
				return false;
			}
			LittleEndianReader littleEndianReader = new LittleEndianReader(blob.AsSpan());
			uint num = littleEndianReader.ReadUInt32();
			uint num2 = littleEndianReader.ReadUInt32();
			uint num3 = littleEndianReader.ReadUInt32();
			byte b = littleEndianReader.ReadByte();
			if ((long)blob.Length != (long)((ulong)(12U + num3)))
			{
				return false;
			}
			if (ByteSequenceComparer.Equals(blob, CryptoBlobParser.s_ecmaKey))
			{
				return true;
			}
			if (b != 6)
			{
				return false;
			}
			CryptoBlobParser.AlgorithmId algorithmId = new CryptoBlobParser.AlgorithmId(num);
			if (algorithmId.IsSet && algorithmId.Class != CryptoBlobParser.AlgorithmClass.Signature)
			{
				return false;
			}
			CryptoBlobParser.AlgorithmId algorithmId2 = new CryptoBlobParser.AlgorithmId(num2);
			return !algorithmId2.IsSet || (algorithmId2.Class == CryptoBlobParser.AlgorithmClass.Hash && algorithmId2.SubId >= CryptoBlobParser.AlgorithmSubId.Sha1Hash);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x00040084 File Offset: 0x0003E284
		private unsafe static ImmutableArray<byte> CreateSnPublicKeyBlob(byte type, byte version, uint algId, uint magic, uint bitLen, uint pubExp, ReadOnlySpan<byte> pubKeyData)
		{
			BlobWriter blobWriter = new BlobWriter(32 + pubKeyData.Length);
			blobWriter.WriteUInt32(9216U);
			blobWriter.WriteUInt32(32772U);
			blobWriter.WriteUInt32((uint)(20 + pubKeyData.Length));
			blobWriter.WriteByte(type);
			blobWriter.WriteByte(version);
			blobWriter.WriteUInt16(0);
			blobWriter.WriteUInt32(algId);
			blobWriter.WriteUInt32(magic);
			blobWriter.WriteUInt32(bitLen);
			blobWriter.WriteUInt32(pubExp);
			fixed (byte* pinnableReference = pubKeyData.GetPinnableReference())
			{
				byte* ptr = pinnableReference;
				blobWriter.WriteBytes(ptr, pubKeyData.Length);
			}
			return blobWriter.ToImmutableArray();
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x0004012C File Offset: 0x0003E32C
		public static bool TryParseKey(ImmutableArray<byte> blob, out ImmutableArray<byte> snKey, out RSAParameters? privateKey)
		{
			privateKey = default(RSAParameters?);
			snKey = default(ImmutableArray<byte>);
			if (CryptoBlobParser.IsValidPublicKey(blob))
			{
				snKey = blob;
				return true;
			}
			if (blob.Length < 20)
			{
				return false;
			}
			bool flag;
			try
			{
				LittleEndianReader littleEndianReader = new LittleEndianReader(blob.AsSpan());
				byte b = littleEndianReader.ReadByte();
				byte b2 = littleEndianReader.ReadByte();
				littleEndianReader.ReadUInt16();
				uint num = littleEndianReader.ReadUInt32();
				uint num2 = littleEndianReader.ReadUInt32();
				uint num3 = littleEndianReader.ReadUInt32();
				uint num4 = littleEndianReader.ReadUInt32();
				int num5 = (int)(num3 / 8U);
				if (blob.Length - 20 < num5)
				{
					flag = false;
				}
				else
				{
					ReadOnlySpan<byte> readOnlySpan = littleEndianReader.ReadBytes(num5);
					if ((b != 7 || num2 != 843141970U) && (b != 6 || num2 != 826364754U))
					{
						flag = false;
					}
					else
					{
						if (b == 7)
						{
							privateKey = new RSAParameters?(blob.AsSpan().ToRSAParameters(true));
							num = 9216U;
						}
						snKey = CryptoBlobParser.CreateSnPublicKeyBlob(6, b2, num, 826364754U, num3, num4, readOnlySpan);
						flag = true;
					}
				}
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00040254 File Offset: 0x0003E454
		internal static RSAParameters ToRSAParameters(this ReadOnlySpan<byte> cspBlob, bool includePrivateParameters)
		{
			LittleEndianReader littleEndianReader = new LittleEndianReader(cspBlob);
			littleEndianReader.ReadByte();
			littleEndianReader.ReadByte();
			littleEndianReader.ReadUInt16();
			littleEndianReader.ReadInt32();
			littleEndianReader.ReadInt32();
			int num = littleEndianReader.ReadInt32() / 8;
			int num2 = (num + 1) / 2;
			uint num3 = littleEndianReader.ReadUInt32();
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Exponent = CryptoBlobParser.ExponentAsBytes(num3);
			rsaparameters.Modulus = littleEndianReader.ReadReversed(num);
			if (includePrivateParameters)
			{
				rsaparameters.P = littleEndianReader.ReadReversed(num2);
				rsaparameters.Q = littleEndianReader.ReadReversed(num2);
				rsaparameters.DP = littleEndianReader.ReadReversed(num2);
				rsaparameters.DQ = littleEndianReader.ReadReversed(num2);
				rsaparameters.InverseQ = littleEndianReader.ReadReversed(num2);
				rsaparameters.D = littleEndianReader.ReadReversed(num);
			}
			return rsaparameters;
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x0004032C File Offset: 0x0003E52C
		private static byte[] ExponentAsBytes(uint exponent)
		{
			if (exponent <= 255U)
			{
				return new byte[] { (byte)exponent };
			}
			if (exponent <= 65535U)
			{
				return new byte[]
				{
					(byte)(exponent >> 8),
					(byte)exponent
				};
			}
			if (exponent <= 16777215U)
			{
				return new byte[]
				{
					(byte)(exponent >> 16),
					(byte)(exponent >> 8),
					(byte)exponent
				};
			}
			return new byte[]
			{
				(byte)(exponent >> 24),
				(byte)(exponent >> 16),
				(byte)(exponent >> 8),
				(byte)exponent
			};
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x000403AD File Offset: 0x0003E5AD
		private static byte[] ReadReversed(this BinaryReader br, int count)
		{
			byte[] array = br.ReadBytes(count);
			Array.Reverse(array);
			return array;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x000403BC File Offset: 0x0003E5BC
		// Note: this type is marked as 'beforefieldinit'.
		static CryptoBlobParser()
		{
			byte[] array = new byte[16];
			array[8] = 4;
			CryptoBlobParser.s_ecmaKey = ImmutableArray.Create<byte>(array);
		}

		// Token: 0x040006E5 RID: 1765
		private static readonly ImmutableArray<byte> s_ecmaKey;

		// Token: 0x040006E6 RID: 1766
		private const int SnPublicKeyBlobSize = 13;

		// Token: 0x040006E7 RID: 1767
		private const byte PublicKeyBlobId = 6;

		// Token: 0x040006E8 RID: 1768
		private const byte PrivateKeyBlobId = 7;

		// Token: 0x040006E9 RID: 1769
		internal const int s_publicKeyHeaderSize = 12;

		// Token: 0x040006EA RID: 1770
		private const int BlobHeaderSize = 8;

		// Token: 0x040006EB RID: 1771
		private const int RsaPubKeySize = 12;

		// Token: 0x040006EC RID: 1772
		private const uint RSA1 = 826364754U;

		// Token: 0x040006ED RID: 1773
		private const uint RSA2 = 843141970U;

		// Token: 0x040006EE RID: 1774
		private const int s_offsetToKeyData = 20;

		// Token: 0x020005E3 RID: 1507
		private enum AlgorithmClass
		{
			// Token: 0x040016E0 RID: 5856
			Signature = 1,
			// Token: 0x040016E1 RID: 5857
			Hash = 4
		}

		// Token: 0x020005E4 RID: 1508
		private enum AlgorithmSubId
		{
			// Token: 0x040016E3 RID: 5859
			Sha1Hash = 4,
			// Token: 0x040016E4 RID: 5860
			MacHash,
			// Token: 0x040016E5 RID: 5861
			RipeMdHash,
			// Token: 0x040016E6 RID: 5862
			RipeMd160Hash,
			// Token: 0x040016E7 RID: 5863
			Ssl3ShaMD5Hash,
			// Token: 0x040016E8 RID: 5864
			HmacHash,
			// Token: 0x040016E9 RID: 5865
			Tls1PrfHash,
			// Token: 0x040016EA RID: 5866
			HashReplacOwfHash,
			// Token: 0x040016EB RID: 5867
			Sha256Hash,
			// Token: 0x040016EC RID: 5868
			Sha384Hash,
			// Token: 0x040016ED RID: 5869
			Sha512Hash
		}

		// Token: 0x020005E5 RID: 1509
		private struct AlgorithmId
		{
			// Token: 0x17000F8C RID: 3980
			// (get) Token: 0x060036DD RID: 14045 RVA: 0x0009A010 File Offset: 0x00098210
			public bool IsSet
			{
				get
				{
					return this._flags > 0U;
				}
			}

			// Token: 0x17000F8D RID: 3981
			// (get) Token: 0x060036DE RID: 14046 RVA: 0x0009A01B File Offset: 0x0009821B
			public CryptoBlobParser.AlgorithmClass Class
			{
				get
				{
					return (CryptoBlobParser.AlgorithmClass)((this._flags >> 13) & 7U);
				}
			}

			// Token: 0x17000F8E RID: 3982
			// (get) Token: 0x060036DF RID: 14047 RVA: 0x0009A028 File Offset: 0x00098228
			public CryptoBlobParser.AlgorithmSubId SubId
			{
				get
				{
					return (CryptoBlobParser.AlgorithmSubId)(this._flags & 511U);
				}
			}

			// Token: 0x060036E0 RID: 14048 RVA: 0x0009A036 File Offset: 0x00098236
			public AlgorithmId(uint flags)
			{
				this._flags = flags;
			}

			// Token: 0x040016EE RID: 5870
			private const int AlgorithmClassOffset = 13;

			// Token: 0x040016EF RID: 5871
			private const int AlgorithmClassMask = 7;

			// Token: 0x040016F0 RID: 5872
			private const int AlgorithmSubIdOffset = 0;

			// Token: 0x040016F1 RID: 5873
			private const int AlgorithmSubIdMask = 511;

			// Token: 0x040016F2 RID: 5874
			private readonly uint _flags;

			// Token: 0x040016F3 RID: 5875
			public const int RsaSign = 9216;

			// Token: 0x040016F4 RID: 5876
			public const int Sha = 32772;
		}
	}
}
