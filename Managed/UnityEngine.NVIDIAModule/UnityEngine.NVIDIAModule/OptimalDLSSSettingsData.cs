using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000A RID: 10
	public readonly struct OptimalDLSSSettingsData
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000247C File Offset: 0x0000067C
		public uint outRenderWidth
		{
			get
			{
				return this.m_OutRenderWidth;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002494 File Offset: 0x00000694
		public uint outRenderHeight
		{
			get
			{
				return this.m_OutRenderHeight;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024AC File Offset: 0x000006AC
		public float sharpness
		{
			get
			{
				return this.m_Sharpness;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000024C4 File Offset: 0x000006C4
		public uint maxWidth
		{
			get
			{
				return this.m_MaxWidth;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000024DC File Offset: 0x000006DC
		public uint maxHeight
		{
			get
			{
				return this.m_MaxHeight;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000024F4 File Offset: 0x000006F4
		public uint minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000250C File Offset: 0x0000070C
		public uint minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly uint m_OutRenderWidth;

		// Token: 0x04000035 RID: 53
		private readonly uint m_OutRenderHeight;

		// Token: 0x04000036 RID: 54
		private readonly float m_Sharpness;

		// Token: 0x04000037 RID: 55
		private readonly uint m_MaxWidth;

		// Token: 0x04000038 RID: 56
		private readonly uint m_MaxHeight;

		// Token: 0x04000039 RID: 57
		private readonly uint m_MinWidth;

		// Token: 0x0400003A RID: 58
		private readonly uint m_MinHeight;
	}
}
