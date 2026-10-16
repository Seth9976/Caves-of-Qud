using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace Unity.IntegerTime
{
	// Token: 0x02000060 RID: 96
	public static class RationalTimeExtensions
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00003CB8 File Offset: 0x00001EB8
		[FreeFunction("IntegerTime::RationalTime::ToDouble", IsFreeFunction = true, ThrowsException = true)]
		public static double ToDouble(this RationalTime value)
		{
			return RationalTimeExtensions.ToDouble_Injected(ref value);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00003CCC File Offset: 0x00001ECC
		[FreeFunction("IntegerTime::RationalTime::IsValid", IsFreeFunction = true, ThrowsException = false)]
		public static bool IsValid(this RationalTime value)
		{
			return RationalTimeExtensions.IsValid_Injected(ref value);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00003CE0 File Offset: 0x00001EE0
		[FreeFunction("IntegerTime::RationalTime::ConvertRate", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Convert(this RationalTime time, RationalTime.TicksPerSecond rate)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Convert_Injected(ref time, ref rate, out rationalTime);
			return rationalTime;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00003CFC File Offset: 0x00001EFC
		[FreeFunction("IntegerTime::RationalTime::Add", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Add(this RationalTime lhs, RationalTime rhs)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Add_Injected(ref lhs, ref rhs, out rationalTime);
			return rationalTime;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003D18 File Offset: 0x00001F18
		[FreeFunction("IntegerTime::RationalTime::Subtract", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Subtract(this RationalTime lhs, RationalTime rhs)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Subtract_Injected(ref lhs, ref rhs, out rationalTime);
			return rationalTime;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00003D34 File Offset: 0x00001F34
		[FreeFunction("IntegerTime::RationalTime::Multiply", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Multiply(this RationalTime lhs, RationalTime rhs)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Multiply_Injected(ref lhs, ref rhs, out rationalTime);
			return rationalTime;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003D50 File Offset: 0x00001F50
		[FreeFunction("IntegerTime::RationalTime::Divide", IsFreeFunction = true, ThrowsException = true)]
		public static RationalTime Divide(this RationalTime lhs, RationalTime rhs)
		{
			RationalTime rationalTime;
			RationalTimeExtensions.Divide_Injected(ref lhs, ref rhs, out rationalTime);
			return rationalTime;
		}

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double ToDouble_Injected([In] ref RationalTime value);

		// Token: 0x06000183 RID: 387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected([In] ref RationalTime value);

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Convert_Injected([In] ref RationalTime time, [In] ref RationalTime.TicksPerSecond rate, out RationalTime ret);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add_Injected([In] ref RationalTime lhs, [In] ref RationalTime rhs, out RationalTime ret);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Subtract_Injected([In] ref RationalTime lhs, [In] ref RationalTime rhs, out RationalTime ret);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Multiply_Injected([In] ref RationalTime lhs, [In] ref RationalTime rhs, out RationalTime ret);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Divide_Injected([In] ref RationalTime lhs, [In] ref RationalTime rhs, out RationalTime ret);
	}
}
