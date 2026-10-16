using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000037 RID: 55
	[NativeType(Header = "Modules/Marshalling/MarshallingTests.h")]
	[ExcludeFromDocs]
	internal class TypedefManagedNameTests
	{
		// Token: 0x06000151 RID: 337 RVA: 0x000035AC File Offset: 0x000017AC
		public static void ParameterStructWithTypedefManagedName(StructWithTypedefManagedName param)
		{
			TypedefManagedNameTests.ParameterStructWithTypedefManagedName_Injected(ref param);
		}

		// Token: 0x06000153 RID: 339
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ParameterStructWithTypedefManagedName_Injected([In] ref StructWithTypedefManagedName param);
	}
}
