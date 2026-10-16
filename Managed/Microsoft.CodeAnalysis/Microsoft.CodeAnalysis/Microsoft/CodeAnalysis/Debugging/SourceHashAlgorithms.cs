using System;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Debugging
{
	// Token: 0x02000359 RID: 857
	internal static class SourceHashAlgorithms
	{
		// Token: 0x060024C8 RID: 9416 RVA: 0x00072274 File Offset: 0x00070474
		public static bool IsSupportedAlgorithm(SourceHashAlgorithm algorithm)
		{
			return algorithm == SourceHashAlgorithm.Sha1 || algorithm == SourceHashAlgorithm.Sha256;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x00072298 File Offset: 0x00070498
		public static Guid GetAlgorithmGuid(SourceHashAlgorithm algorithm)
		{
			Guid guid;
			if (algorithm != SourceHashAlgorithm.Sha1)
			{
				if (algorithm != SourceHashAlgorithm.Sha256)
				{
					throw ExceptionUtilities.UnexpectedValue(algorithm);
				}
				guid = SourceHashAlgorithms.s_guidSha256;
			}
			else
			{
				guid = SourceHashAlgorithms.s_guidSha1;
			}
			return guid;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000722CC File Offset: 0x000704CC
		public static SourceHashAlgorithm GetSourceHashAlgorithm(Guid guid)
		{
			if (guid == SourceHashAlgorithms.s_guidSha256)
			{
				return SourceHashAlgorithm.Sha256;
			}
			if (!(guid == SourceHashAlgorithms.s_guidSha1))
			{
				return SourceHashAlgorithm.None;
			}
			return SourceHashAlgorithm.Sha1;
		}

		// Token: 0x04000FC1 RID: 4033
		private static readonly Guid s_guidSha1 = new Guid(-15198484, -21922, 19728, 135, 247, 111, 73, 99, 131, 52, 96);

		// Token: 0x04000FC2 RID: 4034
		private static readonly Guid s_guidSha256 = new Guid(-2010525681, 4536, 16915, 135, 139, 119, 14, 133, 151, 172, 22);
	}
}
