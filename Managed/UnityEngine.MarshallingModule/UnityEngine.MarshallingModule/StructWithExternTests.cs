using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal struct StructWithExternTests
	{
		// Token: 0x06000104 RID: 260
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetTimesTwo();

		// Token: 0x06000105 RID: 261
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTimesThree();

		// Token: 0x06000106 RID: 262
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int ParameterWritable([Writable] Object unityObject);

		// Token: 0x06000107 RID: 263
		[NativeThrows]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ParameterInt(int param);

		// Token: 0x06000108 RID: 264
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ReturnInt();

		// Token: 0x0400001A RID: 26
		public int a;
	}
}
