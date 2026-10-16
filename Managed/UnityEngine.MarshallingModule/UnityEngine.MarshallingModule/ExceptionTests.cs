using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200002B RID: 43
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class ExceptionTests
	{
		// Token: 0x06000116 RID: 278 RVA: 0x0000310C File Offset: 0x0000130C
		[NativeThrows]
		public unsafe static void VoidReturnStringParameter(string param)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(param, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = param.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ExceptionTests.VoidReturnStringParameter_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000117 RID: 279
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NonUnmarshallingReturn();

		// Token: 0x06000118 RID: 280 RVA: 0x00003160 File Offset: 0x00001360
		[NativeThrows]
		public static string UnmarshallingReturn()
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				ExceptionTests.UnmarshallingReturn_Injected(out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00003190 File Offset: 0x00001390
		[NativeThrows]
		public static StructInt BlittableStructReturn()
		{
			StructInt structInt;
			ExceptionTests.BlittableStructReturn_Injected(out structInt);
			return structInt;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000031A8 File Offset: 0x000013A8
		[NativeThrows]
		public static StructCoreString NonblittableStructReturn()
		{
			StructCoreString structCoreString;
			ExceptionTests.NonblittableStructReturn_Injected(out structCoreString);
			return structCoreString;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600011B RID: 283
		// (set) Token: 0x0600011C RID: 284
		[NativeThrows]
		public static extern int PropertyThatCanThrow
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600011D RID: 285
		// (set) Token: 0x0600011E RID: 286
		public static extern int PropertyGetThatCanThrow
		{
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600011F RID: 287
		// (set) Token: 0x06000120 RID: 288
		public static extern int PropertySetThatCanThrow
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[NativeThrows]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000122 RID: 290
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void VoidReturnStringParameter_Injected(ref ManagedSpanWrapper param);

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnmarshallingReturn_Injected(out ManagedSpanWrapper ret);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void BlittableStructReturn_Injected(out StructInt ret);

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void NonblittableStructReturn_Injected(out StructCoreString ret);
	}
}
