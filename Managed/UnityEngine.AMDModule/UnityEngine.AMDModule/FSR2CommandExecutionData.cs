using System;

namespace UnityEngine.AMD
{
	// Token: 0x02000007 RID: 7
	public struct FSR2CommandExecutionData
	{
		// Token: 0x0400001D RID: 29
		public float jitterOffsetX;

		// Token: 0x0400001E RID: 30
		public float jitterOffsetY;

		// Token: 0x0400001F RID: 31
		public float MVScaleX;

		// Token: 0x04000020 RID: 32
		public float MVScaleY;

		// Token: 0x04000021 RID: 33
		public uint renderSizeWidth;

		// Token: 0x04000022 RID: 34
		public uint renderSizeHeight;

		// Token: 0x04000023 RID: 35
		public int enableSharpening;

		// Token: 0x04000024 RID: 36
		public float sharpness;

		// Token: 0x04000025 RID: 37
		public float frameTimeDelta;

		// Token: 0x04000026 RID: 38
		public float preExposure;

		// Token: 0x04000027 RID: 39
		public int reset;

		// Token: 0x04000028 RID: 40
		public float cameraNear;

		// Token: 0x04000029 RID: 41
		public float cameraFar;

		// Token: 0x0400002A RID: 42
		public float cameraFovAngleVertical;

		// Token: 0x0400002B RID: 43
		internal uint featureSlot;

		// Token: 0x02000008 RID: 8
		internal enum Textures
		{
			// Token: 0x0400002D RID: 45
			ColorInput,
			// Token: 0x0400002E RID: 46
			ColorOutput,
			// Token: 0x0400002F RID: 47
			Depth,
			// Token: 0x04000030 RID: 48
			MotionVectors,
			// Token: 0x04000031 RID: 49
			TransparencyMask,
			// Token: 0x04000032 RID: 50
			ExposureTexture,
			// Token: 0x04000033 RID: 51
			ReactiveMask,
			// Token: 0x04000034 RID: 52
			BiasColorMask
		}
	}
}
