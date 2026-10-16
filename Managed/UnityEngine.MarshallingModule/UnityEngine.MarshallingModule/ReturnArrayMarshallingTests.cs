using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200003A RID: 58
	[NativeHeader("Modules/Marshalling/ReturnArrayMarshallingTests.h")]
	[ExcludeFromDocs]
	internal static class ReturnArrayMarshallingTests
	{
		// Token: 0x06000165 RID: 357
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern float[] ReturnArrayOfPrimitiveTypeWorks_Float1D();

		// Token: 0x06000166 RID: 358
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern float[,] ReturnArrayOfPrimitiveTypeWorks_Float2D();

		// Token: 0x06000167 RID: 359
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: Unmarshalled]
		public static extern float[,,] ReturnArrayOfPrimitiveTypeWorks_Float3D();
	}
}
