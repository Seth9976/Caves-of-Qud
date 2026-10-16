using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000006 RID: 6
	public struct DLSSCommandInitializationData
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C8 File Offset: 0x000002C8
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020BC File Offset: 0x000002BC
		public uint inputRTWidth
		{
			get
			{
				return this.m_InputRTWidth;
			}
			set
			{
				this.m_InputRTWidth = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020EC File Offset: 0x000002EC
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020E0 File Offset: 0x000002E0
		public uint inputRTHeight
		{
			get
			{
				return this.m_InputRTHeight;
			}
			set
			{
				this.m_InputRTHeight = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002110 File Offset: 0x00000310
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002104 File Offset: 0x00000304
		public uint outputRTWidth
		{
			get
			{
				return this.m_OutputRTWidth;
			}
			set
			{
				this.m_OutputRTWidth = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002134 File Offset: 0x00000334
		// (set) Token: 0x0600000F RID: 15 RVA: 0x00002128 File Offset: 0x00000328
		public uint outputRTHeight
		{
			get
			{
				return this.m_OutputRTHeight;
			}
			set
			{
				this.m_OutputRTHeight = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002158 File Offset: 0x00000358
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000214C File Offset: 0x0000034C
		public DLSSQuality quality
		{
			get
			{
				return this.m_Quality;
			}
			set
			{
				this.m_Quality = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000217C File Offset: 0x0000037C
		// (set) Token: 0x06000013 RID: 19 RVA: 0x00002170 File Offset: 0x00000370
		public DLSSFeatureFlags featureFlags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021A0 File Offset: 0x000003A0
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002194 File Offset: 0x00000394
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

		// Token: 0x06000017 RID: 23 RVA: 0x000021B8 File Offset: 0x000003B8
		public void SetFlag(DLSSFeatureFlags flag, bool value)
		{
			if (value)
			{
				this.m_Flags |= flag;
			}
			else
			{
				this.m_Flags &= ~flag;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021F0 File Offset: 0x000003F0
		public bool GetFlag(DLSSFeatureFlags flag)
		{
			return (this.m_Flags & flag) > DLSSFeatureFlags.None;
		}

		// Token: 0x04000010 RID: 16
		private uint m_InputRTWidth;

		// Token: 0x04000011 RID: 17
		private uint m_InputRTHeight;

		// Token: 0x04000012 RID: 18
		private uint m_OutputRTWidth;

		// Token: 0x04000013 RID: 19
		private uint m_OutputRTHeight;

		// Token: 0x04000014 RID: 20
		private DLSSQuality m_Quality;

		// Token: 0x04000015 RID: 21
		private DLSSFeatureFlags m_Flags;

		// Token: 0x04000016 RID: 22
		private uint m_FeatureSlot;
	}
}
