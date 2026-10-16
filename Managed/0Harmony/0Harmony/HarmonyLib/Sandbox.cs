using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HarmonyLib
{
	// Token: 0x02000040 RID: 64
	internal class Sandbox
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000A618 File Offset: 0x00008818
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Sandbox.SomeStruct_Net GetStruct_Net(IntPtr x, IntPtr y)
		{
			throw new Exception("This method should've been detoured!");
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A618 File Offset: 0x00008818
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Sandbox.SomeStruct_NetLinux GetStruct_NetLinux(IntPtr x, IntPtr y)
		{
			throw new Exception("This method should've been detoured!");
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000A618 File Offset: 0x00008818
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal Sandbox.SomeStruct_Mono GetStruct_Mono(IntPtr x, IntPtr y)
		{
			throw new Exception("This method should've been detoured!");
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000A624 File Offset: 0x00008824
		internal static void GetStructReplacement_Net(Sandbox self, IntPtr ptr, IntPtr a, IntPtr b)
		{
			Sandbox.hasStructReturnBuffer_Net = a == Sandbox.magicValue && b == Sandbox.magicValue;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A646 File Offset: 0x00008846
		internal static void GetStructReplacement_Mono(Sandbox self, IntPtr ptr, IntPtr a, IntPtr b)
		{
			Sandbox.hasStructReturnBuffer_Mono = a == Sandbox.magicValue && b == Sandbox.magicValue;
		}

		// Token: 0x040000E4 RID: 228
		internal static bool hasStructReturnBuffer_Net;

		// Token: 0x040000E5 RID: 229
		internal static bool hasStructReturnBuffer_Mono;

		// Token: 0x040000E6 RID: 230
		internal static readonly IntPtr magicValue = (IntPtr)305419896;

		// Token: 0x02000041 RID: 65
		internal struct SomeStruct_Net
		{
			// Token: 0x040000E7 RID: 231
			private readonly byte b1;

			// Token: 0x040000E8 RID: 232
			private readonly byte b2;

			// Token: 0x040000E9 RID: 233
			private readonly byte b3;
		}

		// Token: 0x02000042 RID: 66
		internal struct SomeStruct_NetLinux
		{
			// Token: 0x040000EA RID: 234
			[FixedBuffer(typeof(byte), 17)]
			public Sandbox.SomeStruct_NetLinux.<headerBytes>e__FixedBuffer headerBytes;

			// Token: 0x02000043 RID: 67
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 17)]
			public struct <headerBytes>e__FixedBuffer
			{
				// Token: 0x040000EB RID: 235
				public byte FixedElementField;
			}
		}

		// Token: 0x02000044 RID: 68
		internal struct SomeStruct_Mono
		{
			// Token: 0x040000EC RID: 236
			private readonly byte b1;

			// Token: 0x040000ED RID: 237
			private readonly byte b2;

			// Token: 0x040000EE RID: 238
			private readonly byte b3;

			// Token: 0x040000EF RID: 239
			private readonly byte b4;
		}
	}
}
