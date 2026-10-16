using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000008 RID: 8
	public struct DLSSCommandExecutionData
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002284 File Offset: 0x00000484
		public int reset
		{
			get
			{
				return this.m_Reset;
			}
			set
			{
				this.m_Reset = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022B4 File Offset: 0x000004B4
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000022A8 File Offset: 0x000004A8
		public float sharpness
		{
			get
			{
				return this.m_Sharpness;
			}
			set
			{
				this.m_Sharpness = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000022D8 File Offset: 0x000004D8
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000022CC File Offset: 0x000004CC
		public float mvScaleX
		{
			get
			{
				return this.m_MVScaleX;
			}
			set
			{
				this.m_MVScaleX = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000022FC File Offset: 0x000004FC
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000022F0 File Offset: 0x000004F0
		public float mvScaleY
		{
			get
			{
				return this.m_MVScaleY;
			}
			set
			{
				this.m_MVScaleY = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002320 File Offset: 0x00000520
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002314 File Offset: 0x00000514
		public float jitterOffsetX
		{
			get
			{
				return this.m_JitterOffsetX;
			}
			set
			{
				this.m_JitterOffsetX = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002338 File Offset: 0x00000538
		public float jitterOffsetY
		{
			get
			{
				return this.m_JitterOffsetY;
			}
			set
			{
				this.m_JitterOffsetY = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002368 File Offset: 0x00000568
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000235C File Offset: 0x0000055C
		public float preExposure
		{
			get
			{
				return this.m_PreExposure;
			}
			set
			{
				this.m_PreExposure = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002380 File Offset: 0x00000580
		public uint subrectOffsetX
		{
			get
			{
				return this.m_SubrectOffsetX;
			}
			set
			{
				this.m_SubrectOffsetX = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000023B0 File Offset: 0x000005B0
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000023A4 File Offset: 0x000005A4
		public uint subrectOffsetY
		{
			get
			{
				return this.m_SubrectOffsetY;
			}
			set
			{
				this.m_SubrectOffsetY = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000023D4 File Offset: 0x000005D4
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000023C8 File Offset: 0x000005C8
		public uint subrectWidth
		{
			get
			{
				return this.m_SubrectWidth;
			}
			set
			{
				this.m_SubrectWidth = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000023F8 File Offset: 0x000005F8
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000023EC File Offset: 0x000005EC
		public uint subrectHeight
		{
			get
			{
				return this.m_SubrectHeight;
			}
			set
			{
				this.m_SubrectHeight = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000241C File Offset: 0x0000061C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002410 File Offset: 0x00000610
		public uint invertXAxis
		{
			get
			{
				return this.m_InvertXAxis;
			}
			set
			{
				this.m_InvertXAxis = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002440 File Offset: 0x00000640
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002434 File Offset: 0x00000634
		public uint invertYAxis
		{
			get
			{
				return this.m_InvertYAxis;
			}
			set
			{
				this.m_InvertYAxis = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002464 File Offset: 0x00000664
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002458 File Offset: 0x00000658
		internal uint featureSlot
		{
			get
			{
				return this.m_FeatureSlot;
			}
			set
			{
				this.m_FeatureSlot = value;
			}
		}

		// Token: 0x0400001E RID: 30
		private int m_Reset;

		// Token: 0x0400001F RID: 31
		private float m_Sharpness;

		// Token: 0x04000020 RID: 32
		private float m_MVScaleX;

		// Token: 0x04000021 RID: 33
		private float m_MVScaleY;

		// Token: 0x04000022 RID: 34
		private float m_JitterOffsetX;

		// Token: 0x04000023 RID: 35
		private float m_JitterOffsetY;

		// Token: 0x04000024 RID: 36
		private float m_PreExposure;

		// Token: 0x04000025 RID: 37
		private uint m_SubrectOffsetX;

		// Token: 0x04000026 RID: 38
		private uint m_SubrectOffsetY;

		// Token: 0x04000027 RID: 39
		private uint m_SubrectWidth;

		// Token: 0x04000028 RID: 40
		private uint m_SubrectHeight;

		// Token: 0x04000029 RID: 41
		private uint m_InvertXAxis;

		// Token: 0x0400002A RID: 42
		private uint m_InvertYAxis;

		// Token: 0x0400002B RID: 43
		private uint m_FeatureSlot;

		// Token: 0x02000009 RID: 9
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
			BiasColorMask
		}
	}
}
