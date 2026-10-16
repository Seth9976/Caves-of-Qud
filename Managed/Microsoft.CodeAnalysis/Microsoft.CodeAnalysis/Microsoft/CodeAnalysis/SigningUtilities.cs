using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001C7 RID: 455
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SigningUtilities
	{
		// Token: 0x06001306 RID: 4870 RVA: 0x00039FF0 File Offset: 0x000381F0
		internal static byte[] CalculateRsaSignature(IEnumerable<Blob> content, RSAParameters privateKey)
		{
			byte[] array = SigningUtilities.CalculateSha1(content);
			byte[] array3;
			using (RSA rsa = RSA.Create())
			{
				rsa.ImportParameters(privateKey);
				byte[] array2 = rsa.SignHash(array, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
				Array.Reverse(array2);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x0003A048 File Offset: 0x00038248
		internal static byte[] CalculateSha1(IEnumerable<Blob> content)
		{
			byte[] hashAndReset;
			using (IncrementalHash incrementalHash = IncrementalHash.CreateHash(HashAlgorithmName.SHA1))
			{
				incrementalHash.AppendData(content);
				hashAndReset = incrementalHash.GetHashAndReset();
			}
			return hashAndReset;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0003A08C File Offset: 0x0003828C
		internal static int CalculateStrongNameSignatureSize(CommonPEModuleBuilder module, RSAParameters? privateKey)
		{
			ISourceAssemblySymbolInternal sourceAssemblyOpt = module.SourceAssemblyOpt;
			if (sourceAssemblyOpt == null && privateKey == null)
			{
				return 0;
			}
			int num = 0;
			if (num == 0 && sourceAssemblyOpt != null)
			{
				num = ((sourceAssemblyOpt.SignatureKey == null) ? 0 : (sourceAssemblyOpt.SignatureKey.Length / 2));
			}
			if (num == 0 && sourceAssemblyOpt != null)
			{
				num = sourceAssemblyOpt.Identity.PublicKey.Length;
			}
			if (num == 0 && privateKey != null)
			{
				num = privateKey.Value.Modulus.Length;
			}
			if (num == 0)
			{
				return 0;
			}
			if (num >= 160)
			{
				return num - 32;
			}
			return 128;
		}
	}
}
