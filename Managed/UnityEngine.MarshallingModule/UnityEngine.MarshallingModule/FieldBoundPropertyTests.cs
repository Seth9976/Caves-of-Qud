using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeType("Modules/Marshalling/MarshallingTests.h")]
	internal class FieldBoundPropertyTests
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000154 RID: 340
		// (set) Token: 0x06000155 RID: 341
		[NativeProperty(TargetType = TargetType.Field)]
		public static extern int StaticProp
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000156 RID: 342
		// (set) Token: 0x06000157 RID: 343
		[NativeProperty("foo", false, TargetType.Field)]
		[StaticAccessor("FieldBoundPropertyTests::GetNativeStaticPropContainer()", StaticAccessorType.Dot)]
		public static extern int StaticAccessorProp
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
