using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000045 RID: 69
	internal static class MarshallingTests
	{
		// Token: 0x060001E6 RID: 486
		[FreeFunction("MarshallingTest::DisableMarshallingTestsVerification")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableMarshallingTestsVerification();
	}
}
