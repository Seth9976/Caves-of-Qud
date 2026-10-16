using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000046 RID: 70
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal class MarshallingTests2
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000043E8 File Offset: 0x000025E8
		public static void ParameterNonBlittableStructReuse(StructCoreString param)
		{
			MarshallingTests2.ParameterNonBlittableStructReuse_Injected(ref param);
		}

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterNonBlittableStructReuse_Injected([In] ref StructCoreString param);
	}
}
