using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000007 RID: 7
	internal static class DecimalEx
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002149 File Offset: 0x00000349
		private static ref DecimalEx.DecCalc AsMutable(ref decimal d)
		{
			return Unsafe.As<decimal, DecimalEx.DecCalc>(ref d);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002151 File Offset: 0x00000351
		internal static uint High(this decimal value)
		{
			return Unsafe.As<decimal, DecimalEx.DecCalc>(ref value).uhi;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000215F File Offset: 0x0000035F
		internal static uint Low(this decimal value)
		{
			return Unsafe.As<decimal, DecimalEx.DecCalc>(ref value).ulo;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000216D File Offset: 0x0000036D
		internal static uint Mid(this decimal value)
		{
			return Unsafe.As<decimal, DecimalEx.DecCalc>(ref value).umid;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000217B File Offset: 0x0000037B
		internal static bool IsNegative(this decimal value)
		{
			return Unsafe.As<decimal, DecimalEx.DecimalBits>(ref value).flags < 0;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000218C File Offset: 0x0000038C
		internal static int Scale(this decimal value)
		{
			return (int)((byte)(Unsafe.As<decimal, DecimalEx.DecimalBits>(ref value).flags >> 16));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000219E File Offset: 0x0000039E
		internal static uint DecDivMod1E9(ref decimal value)
		{
			return DecimalEx.DecCalc.DecDivMod1E9(DecimalEx.AsMutable(ref value));
		}

		// Token: 0x04000003 RID: 3
		private const int ScaleShift = 16;

		// Token: 0x02000046 RID: 70
		[StructLayout(LayoutKind.Explicit)]
		private struct DecimalBits
		{
			// Token: 0x040000E7 RID: 231
			[FieldOffset(0)]
			public int flags;

			// Token: 0x040000E8 RID: 232
			[FieldOffset(4)]
			public int hi;

			// Token: 0x040000E9 RID: 233
			[FieldOffset(8)]
			public int lo;

			// Token: 0x040000EA RID: 234
			[FieldOffset(12)]
			public int mid;
		}

		// Token: 0x02000047 RID: 71
		[StructLayout(LayoutKind.Explicit)]
		private struct DecCalc
		{
			// Token: 0x0600036A RID: 874 RVA: 0x00022A30 File Offset: 0x00020C30
			internal static uint DecDivMod1E9(ref DecimalEx.DecCalc value)
			{
				ulong num = ((ulong)value.uhi << 32) + (ulong)value.umid;
				ulong num2 = num / 1000000000UL;
				value.uhi = (uint)(num2 >> 32);
				value.umid = (uint)num2;
				ulong num3 = (num - (ulong)((uint)num2 * 1000000000U) << 32) + (ulong)value.ulo;
				uint num4 = (uint)(num3 / 1000000000UL);
				value.ulo = num4;
				return (uint)num3 - num4 * 1000000000U;
			}

			// Token: 0x040000EB RID: 235
			private const uint TenToPowerNine = 1000000000U;

			// Token: 0x040000EC RID: 236
			[FieldOffset(0)]
			public uint uflags;

			// Token: 0x040000ED RID: 237
			[FieldOffset(4)]
			public uint uhi;

			// Token: 0x040000EE RID: 238
			[FieldOffset(8)]
			public uint ulo;

			// Token: 0x040000EF RID: 239
			[FieldOffset(12)]
			public uint umid;

			// Token: 0x040000F0 RID: 240
			[FieldOffset(8)]
			private ulong ulomidLE;
		}
	}
}
