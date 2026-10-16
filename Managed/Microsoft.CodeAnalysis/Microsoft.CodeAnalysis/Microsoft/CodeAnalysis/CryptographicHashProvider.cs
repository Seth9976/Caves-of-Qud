using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200015D RID: 349
	internal abstract class CryptographicHashProvider
	{
		// Token: 0x06000E35 RID: 3637
		internal abstract ImmutableArray<byte> ComputeHash([Nullable(1)] HashAlgorithm algorithm);

		// Token: 0x06000E36 RID: 3638 RVA: 0x000287F0 File Offset: 0x000269F0
		internal ImmutableArray<byte> GetHash(AssemblyHashAlgorithm algorithmId)
		{
			ImmutableArray<byte> immutableArray;
			using (HashAlgorithm hashAlgorithm = CryptographicHashProvider.TryGetAlgorithm(algorithmId))
			{
				if (hashAlgorithm != null)
				{
					if (algorithmId <= AssemblyHashAlgorithm.MD5)
					{
						if (algorithmId != AssemblyHashAlgorithm.None)
						{
							if (algorithmId != AssemblyHashAlgorithm.MD5)
							{
								goto IL_009C;
							}
							return this.GetHash(ref this._lazyMD5Hash, hashAlgorithm);
						}
					}
					else if (algorithmId != AssemblyHashAlgorithm.Sha1)
					{
						switch (algorithmId)
						{
						case AssemblyHashAlgorithm.Sha256:
							return this.GetHash(ref this._lazySHA256Hash, hashAlgorithm);
						case AssemblyHashAlgorithm.Sha384:
							return this.GetHash(ref this._lazySHA384Hash, hashAlgorithm);
						case AssemblyHashAlgorithm.Sha512:
							return this.GetHash(ref this._lazySHA512Hash, hashAlgorithm);
						default:
							goto IL_009C;
						}
					}
					return this.GetHash(ref this._lazySHA1Hash, hashAlgorithm);
					IL_009C:
					throw ExceptionUtilities.UnexpectedValue(algorithmId);
				}
				immutableArray = ImmutableArray.Create<byte>();
			}
			return immutableArray;
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000288C0 File Offset: 0x00026AC0
		internal static int GetHashSize(SourceHashAlgorithm algorithmId)
		{
			if (algorithmId == SourceHashAlgorithm.Sha1)
			{
				return 20;
			}
			if (algorithmId != SourceHashAlgorithm.Sha256)
			{
				throw ExceptionUtilities.UnexpectedValue(algorithmId);
			}
			return 32;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000288DD File Offset: 0x00026ADD
		[NullableContext(2)]
		internal static HashAlgorithm TryGetAlgorithm(SourceHashAlgorithm algorithmId)
		{
			if (algorithmId == SourceHashAlgorithm.Sha1)
			{
				return SHA1.Create();
			}
			if (algorithmId != SourceHashAlgorithm.Sha256)
			{
				return null;
			}
			return SHA256.Create();
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000288F6 File Offset: 0x00026AF6
		internal static HashAlgorithmName GetAlgorithmName(SourceHashAlgorithm algorithmId)
		{
			if (algorithmId == SourceHashAlgorithm.Sha1)
			{
				return HashAlgorithmName.SHA1;
			}
			if (algorithmId != SourceHashAlgorithm.Sha256)
			{
				throw ExceptionUtilities.UnexpectedValue(algorithmId);
			}
			return HashAlgorithmName.SHA256;
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0002891C File Offset: 0x00026B1C
		[NullableContext(2)]
		internal static HashAlgorithm TryGetAlgorithm(AssemblyHashAlgorithm algorithmId)
		{
			if (algorithmId <= AssemblyHashAlgorithm.MD5)
			{
				if (algorithmId != AssemblyHashAlgorithm.None)
				{
					if (algorithmId != AssemblyHashAlgorithm.MD5)
					{
						goto IL_0055;
					}
					return MD5.Create();
				}
			}
			else if (algorithmId != AssemblyHashAlgorithm.Sha1)
			{
				switch (algorithmId)
				{
				case AssemblyHashAlgorithm.Sha256:
					return SHA256.Create();
				case AssemblyHashAlgorithm.Sha384:
					return SHA384.Create();
				case AssemblyHashAlgorithm.Sha512:
					return SHA512.Create();
				default:
					goto IL_0055;
				}
			}
			return SHA1.Create();
			IL_0055:
			return null;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002897F File Offset: 0x00026B7F
		internal static bool IsSupportedAlgorithm(AssemblyHashAlgorithm algorithmId)
		{
			return algorithmId == AssemblyHashAlgorithm.None || algorithmId - AssemblyHashAlgorithm.MD5 <= 1 || algorithmId - AssemblyHashAlgorithm.Sha256 <= 2;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0002899C File Offset: 0x00026B9C
		private ImmutableArray<byte> GetHash(ref ImmutableArray<byte> lazyHash, [Nullable(1)] HashAlgorithm algorithm)
		{
			if (lazyHash.IsDefault)
			{
				ImmutableInterlocked.InterlockedCompareExchange<byte>(ref lazyHash, this.ComputeHash(algorithm), default(ImmutableArray<byte>));
			}
			return lazyHash;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000289D0 File Offset: 0x00026BD0
		internal static ImmutableArray<byte> ComputeSha1([Nullable(1)] Stream stream)
		{
			if (stream != null)
			{
				stream.Seek(0L, 0);
				using (SHA1 sha = SHA1.Create())
				{
					return ImmutableArray.Create<byte>(sha.ComputeHash(stream));
				}
			}
			return ImmutableArray<byte>.Empty;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00028A20 File Offset: 0x00026C20
		internal static ImmutableArray<byte> ComputeSha1(ImmutableArray<byte> bytes)
		{
			return CryptographicHashProvider.ComputeSha1(bytes.ToArray<byte>());
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00028A30 File Offset: 0x00026C30
		internal static ImmutableArray<byte> ComputeSha1([Nullable(1)] byte[] bytes)
		{
			ImmutableArray<byte> immutableArray;
			using (SHA1 sha = SHA1.Create())
			{
				immutableArray = ImmutableArray.Create<byte>(sha.ComputeHash(bytes));
			}
			return immutableArray;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00028A70 File Offset: 0x00026C70
		internal static ImmutableArray<byte> ComputeHash(HashAlgorithmName algorithmName, [Nullable(1)] IEnumerable<Blob> bytes)
		{
			ImmutableArray<byte> immutableArray;
			using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(algorithmName))
			{
				incrementalHash.AppendData(bytes);
				immutableArray = ImmutableArray.Create<byte>(incrementalHash.GetHashAndReset());
			}
			return immutableArray;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00028AB4 File Offset: 0x00026CB4
		internal static ImmutableArray<byte> ComputeHash(HashAlgorithmName algorithmName, [Nullable(new byte[] { 1, 0 })] IEnumerable<ArraySegment<byte>> bytes)
		{
			ImmutableArray<byte> immutableArray;
			using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(algorithmName))
			{
				incrementalHash.AppendData(bytes);
				immutableArray = ImmutableArray.Create<byte>(incrementalHash.GetHashAndReset());
			}
			return immutableArray;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00028AF8 File Offset: 0x00026CF8
		internal static ImmutableArray<byte> ComputeSourceHash(ImmutableArray<byte> bytes, SourceHashAlgorithm hashAlgorithm = SourceHashAlgorithm.Sha256)
		{
			ImmutableArray<byte> immutableArray;
			using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(CryptographicHashProvider.GetAlgorithmName(hashAlgorithm)))
			{
				incrementalHash.AppendData(bytes.ToArray<byte>());
				immutableArray = ImmutableArray.Create<byte>(incrementalHash.GetHashAndReset());
			}
			return immutableArray;
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00028B48 File Offset: 0x00026D48
		internal static ImmutableArray<byte> ComputeSourceHash([Nullable(1)] IEnumerable<Blob> bytes, SourceHashAlgorithm hashAlgorithm = SourceHashAlgorithm.Sha256)
		{
			return CryptographicHashProvider.ComputeHash(CryptographicHashProvider.GetAlgorithmName(hashAlgorithm), bytes);
		}

		// Token: 0x040003E6 RID: 998
		private ImmutableArray<byte> _lazySHA1Hash;

		// Token: 0x040003E7 RID: 999
		private ImmutableArray<byte> _lazySHA256Hash;

		// Token: 0x040003E8 RID: 1000
		private ImmutableArray<byte> _lazySHA384Hash;

		// Token: 0x040003E9 RID: 1001
		private ImmutableArray<byte> _lazySHA512Hash;

		// Token: 0x040003EA RID: 1002
		private ImmutableArray<byte> _lazyMD5Hash;

		// Token: 0x040003EB RID: 1003
		internal const int Sha1HashSize = 20;
	}
}
