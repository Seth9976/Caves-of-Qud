using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200003F RID: 63
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class BoolStructTests
	{
		// Token: 0x0600016D RID: 365 RVA: 0x00003744 File Offset: 0x00001944
		[NativeThrows]
		public static void ParameterStructWith8ByteAndBoolFields(StructWith8ByteAndBoolFields param)
		{
			BoolStructTests.ParameterStructWith8ByteAndBoolFields_Injected(ref param);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00003758 File Offset: 0x00001958
		[NativeThrows]
		public unsafe static void ParameterStructWith8ByteAndBoolFieldsArray(StructWith8ByteAndBoolFields[] param)
		{
			Span<StructWith8ByteAndBoolFields> span = new Span<StructWith8ByteAndBoolFields>(param);
			fixed (StructWith8ByteAndBoolFields* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				BoolStructTests.ParameterStructWith8ByteAndBoolFieldsArray_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000170 RID: 368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWith8ByteAndBoolFields_Injected([In] ref StructWith8ByteAndBoolFields param);

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWith8ByteAndBoolFieldsArray_Injected(ref ManagedSpanWrapper param);
	}
}
