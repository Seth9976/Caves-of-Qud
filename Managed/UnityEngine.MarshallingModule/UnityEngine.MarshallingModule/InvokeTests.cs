using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000044 RID: 68
	[NativeType("Modules/Marshalling/MarshallingTests.h")]
	internal class InvokeTests
	{
		// Token: 0x060001C9 RID: 457
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool TestInvokeBool(bool arg);

		// Token: 0x060001CA RID: 458
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern sbyte TestInvokeSByte(sbyte arg);

		// Token: 0x060001CB RID: 459
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern byte TestInvokeByte(byte arg);

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern char TestInvokeChar(char arg);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern short TestInvokeShort(short arg);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ushort TestInvokeUShort(ushort arg);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int TestInvokeInt(int arg);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint TestInvokeUInt(uint arg);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long TestInvokeLong(long arg);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong TestInvokeULong(ulong arg);

		// Token: 0x060001D3 RID: 467
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr TestInvokeIntPtr(IntPtr arg);

		// Token: 0x060001D4 RID: 468
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern UIntPtr TestInvokeUIntPtr(UIntPtr arg);

		// Token: 0x060001D5 RID: 469
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float TestInvokeFloat(float arg);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double TestInvokeDouble(double arg);

		// Token: 0x060001D7 RID: 471 RVA: 0x000042D0 File Offset: 0x000024D0
		[RequiredByNativeCode(Optional = true)]
		[RequiredMember]
		private static bool InvokeBool(bool arg)
		{
			return arg;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000042E4 File Offset: 0x000024E4
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static sbyte InvokeSByte(sbyte arg)
		{
			return arg;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x000042F8 File Offset: 0x000024F8
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static byte InvokeByte(byte arg)
		{
			return arg;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000430C File Offset: 0x0000250C
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static char InvokeChar(char arg)
		{
			return arg;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00004320 File Offset: 0x00002520
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static short InvokeShort(short arg)
		{
			return arg;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00004334 File Offset: 0x00002534
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static ushort InvokeUShort(ushort arg)
		{
			return arg;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00004348 File Offset: 0x00002548
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static int InvokeInt(int arg)
		{
			return arg;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000435C File Offset: 0x0000255C
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static uint InvokeUInt(uint arg)
		{
			return arg;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00004370 File Offset: 0x00002570
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static long InvokeLong(long arg)
		{
			return arg;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00004384 File Offset: 0x00002584
		[RequiredByNativeCode(Optional = true)]
		[RequiredMember]
		private static ulong InvokeULong(ulong arg)
		{
			return arg;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00004398 File Offset: 0x00002598
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static IntPtr InvokeIntPtr(IntPtr arg)
		{
			return arg;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000043AC File Offset: 0x000025AC
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static UIntPtr InvokeUIntPtr(UIntPtr arg)
		{
			return arg;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000043C0 File Offset: 0x000025C0
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static float InvokeFloat(float arg)
		{
			return arg;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x000043D4 File Offset: 0x000025D4
		[RequiredMember]
		[RequiredByNativeCode(Optional = true)]
		private static double InvokeDouble(double arg)
		{
			return arg;
		}
	}
}
