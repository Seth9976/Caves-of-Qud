using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x0200016F RID: 367
	internal sealed class ByteSequenceComparer : IEqualityComparer<byte[]>, IEqualityComparer<ImmutableArray<byte>>
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x000229E6 File Offset: 0x00020BE6
		private ByteSequenceComparer()
		{
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000229F0 File Offset: 0x00020BF0
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

		// Token: 0x06000B5F RID: 2911 RVA: 0x00022A54 File Offset: 0x00020C54
		[NullableContext(1)]
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

		// Token: 0x06000B60 RID: 2912 RVA: 0x00022A94 File Offset: 0x00020C94
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

		// Token: 0x06000B61 RID: 2913 RVA: 0x00022AD0 File Offset: 0x00020CD0
		[NullableContext(1)]
		internal static int GetHashCode(byte[] x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x00022AD8 File Offset: 0x00020CD8
		internal static int GetHashCode(ImmutableArray<byte> x)
		{
			return Hash.GetFNVHashCode(x);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x00022AE0 File Offset: 0x00020CE0
		bool IEqualityComparer<byte[]>.Equals(byte[] x, byte[] y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00022AE9 File Offset: 0x00020CE9
		int IEqualityComparer<byte[]>.GetHashCode(byte[] x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00022AF1 File Offset: 0x00020CF1
		bool IEqualityComparer<ImmutableArray<byte>>.Equals(ImmutableArray<byte> x, ImmutableArray<byte> y)
		{
			return ByteSequenceComparer.Equals(x, y);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00022AFA File Offset: 0x00020CFA
		int IEqualityComparer<ImmutableArray<byte>>.GetHashCode(ImmutableArray<byte> x)
		{
			return ByteSequenceComparer.GetHashCode(x);
		}

		// Token: 0x04000942 RID: 2370
		[Nullable(1)]
		internal static readonly ByteSequenceComparer Instance = new ByteSequenceComparer();
	}
}
