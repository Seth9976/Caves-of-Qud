using System;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000113 RID: 275
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Boxes
	{
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001B986 File Offset: 0x00019B86
		public static object Box(bool b)
		{
			if (!b)
			{
				return Boxes.BoxedFalse;
			}
			return Boxes.BoxedTrue;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0001B996 File Offset: 0x00019B96
		public static object Box(byte b)
		{
			if (b != 0)
			{
				return b;
			}
			return Boxes.BoxedByteZero;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0001B9A7 File Offset: 0x00019BA7
		public static object Box(sbyte sb)
		{
			if (sb != 0)
			{
				return sb;
			}
			return Boxes.BoxedSByteZero;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public static object Box(short s)
		{
			if (s != 0)
			{
				return s;
			}
			return Boxes.BoxedInt16Zero;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0001B9C9 File Offset: 0x00019BC9
		public static object Box(ushort us)
		{
			if (us != 0)
			{
				return us;
			}
			return Boxes.BoxedUInt16Zero;
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0001B9DA File Offset: 0x00019BDA
		public static object Box(int i)
		{
			if (i == 0)
			{
				return Boxes.BoxedInt32Zero;
			}
			if (i != 1)
			{
				return i;
			}
			return Boxes.BoxedInt32One;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0001B9F7 File Offset: 0x00019BF7
		public static object Box(uint u)
		{
			if (u != 0U)
			{
				return u;
			}
			return Boxes.BoxedUInt32Zero;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0001BA08 File Offset: 0x00019C08
		public static object Box(long l)
		{
			if (l != 0L)
			{
				return l;
			}
			return Boxes.BoxedInt64Zero;
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0001BA19 File Offset: 0x00019C19
		public static object Box(ulong ul)
		{
			if (ul != 0UL)
			{
				return ul;
			}
			return Boxes.BoxedUInt64Zero;
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0001BA2A File Offset: 0x00019C2A
		public unsafe static object Box(float f)
		{
			if (*(int*)(&f) != 0)
			{
				return f;
			}
			return Boxes.BoxedSingleZero;
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0001BA3E File Offset: 0x00019C3E
		public static object Box(double d)
		{
			if (BitConverter.DoubleToInt64Bits(d) != 0L)
			{
				return d;
			}
			return Boxes.BoxedDoubleZero;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0001BA54 File Offset: 0x00019C54
		public static object Box(char c)
		{
			if (c >= '\u0080')
			{
				return c;
			}
			object obj;
			if ((obj = Boxes.s_boxedAsciiChars[(int)c]) == null)
			{
				obj = (Boxes.s_boxedAsciiChars[(int)c] = c);
			}
			return obj;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public unsafe static object Box(decimal d)
		{
			ulong* ptr = (ulong*)(&d);
			if (*ptr != 0UL || ptr[1] != 0UL)
			{
				return d;
			}
			return Boxes.BoxedDecimalZero;
		}

		// Token: 0x040002AE RID: 686
		public static readonly object BoxedTrue = true;

		// Token: 0x040002AF RID: 687
		public static readonly object BoxedFalse = false;

		// Token: 0x040002B0 RID: 688
		public static readonly object BoxedByteZero = 0;

		// Token: 0x040002B1 RID: 689
		public static readonly object BoxedSByteZero = 0;

		// Token: 0x040002B2 RID: 690
		public static readonly object BoxedInt16Zero = 0;

		// Token: 0x040002B3 RID: 691
		public static readonly object BoxedUInt16Zero = 0;

		// Token: 0x040002B4 RID: 692
		public static readonly object BoxedInt32Zero = 0;

		// Token: 0x040002B5 RID: 693
		public static readonly object BoxedInt32One = 1;

		// Token: 0x040002B6 RID: 694
		public static readonly object BoxedUInt32Zero = 0U;

		// Token: 0x040002B7 RID: 695
		public static readonly object BoxedInt64Zero = 0L;

		// Token: 0x040002B8 RID: 696
		public static readonly object BoxedUInt64Zero = 0UL;

		// Token: 0x040002B9 RID: 697
		public static readonly object BoxedSingleZero = 0f;

		// Token: 0x040002BA RID: 698
		public static readonly object BoxedDoubleZero = 0.0;

		// Token: 0x040002BB RID: 699
		public static readonly object BoxedDecimalZero = 0m;

		// Token: 0x040002BC RID: 700
		[Nullable(new byte[] { 1, 2 })]
		private static readonly object[] s_boxedAsciiChars = new object[128];
	}
}
