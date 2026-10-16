using System;
using System.Runtime.CompilerServices;

namespace Unity.IntegerTime
{
	// Token: 0x0200005D RID: 93
	public static class DiscreteTimeTimeExtensions
	{
		// Token: 0x06000163 RID: 355 RVA: 0x00003948 File Offset: 0x00001B48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Abs(this DiscreteTime lhs)
		{
			return DiscreteTime.FromTicks(Math.Abs(lhs.Value));
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000396C File Offset: 0x00001B6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Min(this DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(Math.Min(lhs.Value, rhs.Value));
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00003994 File Offset: 0x00001B94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Max(this DiscreteTime lhs, DiscreteTime rhs)
		{
			return DiscreteTime.FromTicks(Math.Max(lhs.Value, rhs.Value));
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000039BC File Offset: 0x00001BBC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Clamp(this DiscreteTime x, DiscreteTime a, DiscreteTime b)
		{
			return a.Max(b.Min(x));
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000039DC File Offset: 0x00001BDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Floor(this DiscreteTime x)
		{
			return (DiscreteTime)Math.Floor((double)x);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00003A00 File Offset: 0x00001C00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static DiscreteTime Select(this DiscreteTime a, DiscreteTime b, bool c)
		{
			return DiscreteTime.FromTicks(c ? b.Value : a.Value);
		}
	}
}
