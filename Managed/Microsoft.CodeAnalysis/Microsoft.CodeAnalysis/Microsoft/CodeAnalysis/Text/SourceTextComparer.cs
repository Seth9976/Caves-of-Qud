using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002EB RID: 747
	[NullableContext(2)]
	[Nullable(0)]
	internal class SourceTextComparer : IEqualityComparer<SourceText>
	{
		// Token: 0x06002010 RID: 8208 RVA: 0x00063423 File Offset: 0x00061623
		public bool Equals(SourceText x, SourceText y)
		{
			if (x == null)
			{
				return y == null;
			}
			return y != null && x.ContentEquals(y);
		}

		// Token: 0x06002011 RID: 8209 RVA: 0x0006343C File Offset: 0x0006163C
		public int GetHashCode(SourceText obj)
		{
			if (obj == null)
			{
				return 0;
			}
			ImmutableArray<byte> checksum = obj.GetChecksum();
			int num = ((!checksum.IsDefault) ? Hash.CombineValues<byte>(checksum, int.MaxValue) : 0);
			int num2 = ((obj.Encoding != null) ? obj.Encoding.GetHashCode() : 0);
			return Hash.Combine(obj.Length, Hash.Combine(num, Hash.Combine(num2, obj.ChecksumAlgorithm.GetHashCode())));
		}

		// Token: 0x04000E0D RID: 3597
		[Nullable(1)]
		public static readonly SourceTextComparer Instance = new SourceTextComparer();
	}
}
