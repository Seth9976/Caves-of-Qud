using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Collections
{
	// Token: 0x0200049A RID: 1178
	internal sealed class ByteSequenceComparer : IEqualityComparer<byte[]>, IEqualityComparer<ImmutableArray<byte>>
	{
		// Token: 0x06002E85 RID: 11909 RVA: 0x000833D6 File Offset: 0x000815D6
		private ByteSequenceComparer()
		{
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x000833E0 File Offset: 0x000815E0
		internal static bool Equals(ImmutableArray<byte> x, ImmutableArray<byte> y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.IsDefault || y.IsDefault || x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if (x[i] != y[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x00083444 File Offset: 0x00081644
		[NullableContext(2)]
		internal static bool Equals(byte[] left, int leftStart, byte[] right, int rightStart, int length)
		{
			if (left == null || right == null)
			{
				return left == right;
			}
			if (left == right && leftStart == rightStart)
			{
				return true;
			}
			for (int i = 0; i < length; i++)
			{
				if (left[leftStart + i] != right[rightStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x00083484 File Offset: 0x00081684
		[NullableContext(2)]
		internal static bool Equals(byte[] left, byte[] right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null || left.Length != right.Length)
			{
				return false;
			}
			for (int i = 0; i < left.Length; i++)
			{
				if (left[i] != right[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x000834C0 File Offset: 0x000816C0
		[NullableContext(1)]
		internal static int GetHashCode(byte[] x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x000834C8 File Offset: 0x000816C8
		internal static int GetHashCode(ImmutableArray<byte> x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x000834D0 File Offset: 0x000816D0
		[NullableContext(2)]
		bool IEqualityComparer<byte[]>.Equals(byte[] x, byte[] y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x000834D9 File Offset: 0x000816D9
		[NullableContext(1)]
		int IEqualityComparer<byte[]>.GetHashCode(byte[] x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000834E1 File Offset: 0x000816E1
		bool IEqualityComparer<ImmutableArray<byte>>.Equals(ImmutableArray<byte> x, ImmutableArray<byte> y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000834EA File Offset: 0x000816EA
		int IEqualityComparer<ImmutableArray<byte>>.GetHashCode(ImmutableArray<byte> x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x04001273 RID: 4723
		[Nullable(1)]
		internal static readonly ByteSequenceComparer Instance = new ByteSequenceComparer();
	}
}
