using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000038 RID: 56
	[UsedByNativeCode]
	[NativeHeader("Modules/Terrain/Public/SpeedTreeWind.h")]
	internal struct SpeedTreeWindParamsBufferIterator
	{
		// Token: 0x040000E4 RID: 228
		public IntPtr bufferPtr;

		// Token: 0x040000E5 RID: 229
		[FixedBuffer(typeof(int), 16)]
		public SpeedTreeWindParamsBufferIterator.<uintParamOffsets>e__FixedBuffer uintParamOffsets;

		// Token: 0x040000E6 RID: 230
		public int uintStride;

		// Token: 0x040000E7 RID: 231
		public int elementOffset;

		// Token: 0x040000E8 RID: 232
		public int elementsCount;

		// Token: 0x02000039 RID: 57
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 64)]
		public struct <uintParamOffsets>e__FixedBuffer
		{
			// Token: 0x040000E9 RID: 233
			public int FixedElementField;
		}
	}
}
