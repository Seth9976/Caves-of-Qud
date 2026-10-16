using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x0200003D RID: 61
	[NativeType(Header = "Modules/Marshalling/MarshallingTests.h")]
	internal abstract class AbstractClass
	{
		// Token: 0x0600016B RID: 363
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int MethodInAbstractClass();
	}
}
