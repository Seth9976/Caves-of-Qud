using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Internal
{
	// Token: 0x02000180 RID: 384
	internal static class Hash
	{
		// Token: 0x06000BB7 RID: 2999 RVA: 0x000235EB File Offset: 0x000217EB
		internal static int Combine(int newKey, int currentKey)
		{
			return currentKey * -1521134295 + newKey;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000235F6 File Offset: 0x000217F6
		internal static int Combine(uint newKey, int currentKey)
		{
			return currentKey * -1521134295 + (int)newKey;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00023601 File Offset: 0x00021801
		internal static int Combine(bool newKeyPart, int currentKey)
		{
			return Hash.Combine(currentKey, newKeyPart ? 1 : 0);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00023610 File Offset: 0x00021810
		[NullableContext(1)]
		internal static int GetFNVHashCode(byte[] data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x00023640 File Offset: 0x00021840
		internal static int GetFNVHashCode(ImmutableArray<byte> data)
		{
			int num = -2128831035;
			for (int i = 0; i < data.Length; i++)
			{
				num = (num ^ (int)data[i]) * 16777619;
			}
			return num;
		}

		// Token: 0x04000963 RID: 2403
		internal const int FnvOffsetBias = -2128831035;

		// Token: 0x04000964 RID: 2404
		internal const int FnvPrime = 16777619;
	}
}
