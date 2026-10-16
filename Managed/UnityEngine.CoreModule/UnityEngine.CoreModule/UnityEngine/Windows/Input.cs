using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Windows
{
	// Token: 0x0200030B RID: 779
	[NativeHeader("PlatformDependent/Win/Bindings/InputBindings.h")]
	public static class Input
	{
		// Token: 0x060026AA RID: 9898
		[NativeName("ForwardRawInput")]
		[StaticAccessor("", StaticAccessorType.DoubleColon)]
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ForwardRawInputImpl(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize);

		// Token: 0x060026AB RID: 9899 RVA: 0x0004A6AC File Offset: 0x000488AC
		public unsafe static void ForwardRawInput(IntPtr rawInputHeaderIndices, IntPtr rawInputDataIndices, uint indicesCount, IntPtr rawInputData, uint rawInputDataSize)
		{
			Input.ForwardRawInput((uint*)(void*)rawInputHeaderIndices, (uint*)(void*)rawInputDataIndices, indicesCount, (byte*)(void*)rawInputData, rawInputDataSize);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0004A6CC File Offset: 0x000488CC
		public unsafe static void ForwardRawInput(uint* rawInputHeaderIndices, uint* rawInputDataIndices, uint indicesCount, byte* rawInputData, uint rawInputDataSize)
		{
			bool flag = rawInputHeaderIndices == null;
			if (flag)
			{
				throw new ArgumentNullException("rawInputHeaderIndices");
			}
			bool flag2 = rawInputDataIndices == null;
			if (flag2)
			{
				throw new ArgumentNullException("rawInputDataIndices");
			}
			bool flag3 = rawInputData == null;
			if (flag3)
			{
				throw new ArgumentNullException("rawInputData");
			}
			Input.ForwardRawInputImpl(rawInputHeaderIndices, rawInputDataIndices, indicesCount, rawInputData, rawInputDataSize);
		}
	}
}
