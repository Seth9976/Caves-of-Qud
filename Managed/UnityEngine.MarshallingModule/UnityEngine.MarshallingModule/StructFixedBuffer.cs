using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	[ExcludeFromDocs]
	[NativeHeader("Modules/Marshalling/MarshallingTests.h")]
	internal struct StructFixedBuffer
	{
		// Token: 0x04000006 RID: 6
		[FixedBuffer(typeof(int), 4)]
		public StructFixedBuffer.<SomeInts>e__FixedBuffer SomeInts;

		// Token: 0x0200000A RID: 10
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <SomeInts>e__FixedBuffer
		{
			// Token: 0x04000007 RID: 7
			public int FixedElementField;
		}
	}
}
