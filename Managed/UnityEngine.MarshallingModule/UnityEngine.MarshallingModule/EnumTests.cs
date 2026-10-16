using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200002E RID: 46
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class EnumTests
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00003404 File Offset: 0x00001604
		[NativeThrows]
		public unsafe static void ParameterDynamicArrayEnum(SomeEnum[] enumArray)
		{
			Span<SomeEnum> span = new Span<SomeEnum>(enumArray);
			fixed (SomeEnum* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				EnumTests.ParameterDynamicArrayEnum_Injected(ref managedSpanWrapper);
			}
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000343C File Offset: 0x0000163C
		public unsafe static void ParameterOutDynamicArrayEnum([Out] SomeEnum[] enumArray)
		{
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				if (enumArray != null)
				{
					fixed (SomeEnum[] array = enumArray)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				EnumTests.ParameterOutDynamicArrayEnum_Injected(out blittableArrayWrapper);
			}
			finally
			{
				SomeEnum[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<SomeEnum>(ref array);
			}
		}

		// Token: 0x06000134 RID: 308
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterDynamicArrayEnum_Injected(ref ManagedSpanWrapper enumArray);

		// Token: 0x06000135 RID: 309
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterOutDynamicArrayEnum_Injected(out BlittableArrayWrapper enumArray);
	}
}
