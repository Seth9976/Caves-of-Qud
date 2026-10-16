using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil.PE
{
	// Token: 0x020000D1 RID: 209
	internal sealed class ByteBufferEqualityComparer : IEqualityComparer<ByteBuffer>
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x0001CFD8 File Offset: 0x0001B1D8
		public bool Equals(ByteBuffer x, ByteBuffer y)
		{
			if (x.length != y.length)
			{
				return false;
			}
			byte[] buffer = x.buffer;
			byte[] buffer2 = y.buffer;
			for (int i = 0; i < x.length; i++)
			{
				if (buffer[i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0001D020 File Offset: 0x0001B220
		public int GetHashCode(ByteBuffer buffer)
		{
			int num = -2128831035;
			byte[] buffer2 = buffer.buffer;
			for (int i = 0; i < buffer.length; i++)
			{
				num = (num ^ (int)buffer2[i]) * 16777619;
			}
			return num;
		}
	}
}
