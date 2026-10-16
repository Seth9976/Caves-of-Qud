using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000B RID: 11
	public readonly struct DLSSDebugFeatureInfos
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002524 File Offset: 0x00000724
		public bool validFeature
		{
			get
			{
				return this.m_ValidFeature;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000253C File Offset: 0x0000073C
		public uint featureSlot
		{
			get
			{
				return this.m_FeatureSlot;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002554 File Offset: 0x00000754
		public DLSSCommandExecutionData execData
		{
			get
			{
				return this.m_ExecData;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000256C File Offset: 0x0000076C
		public DLSSCommandInitializationData initData
		{
			get
			{
				return this.m_InitData;
			}
		}

		// Token: 0x0400003B RID: 59
		private readonly bool m_ValidFeature;

		// Token: 0x0400003C RID: 60
		private readonly uint m_FeatureSlot;

		// Token: 0x0400003D RID: 61
		private readonly DLSSCommandExecutionData m_ExecData;

		// Token: 0x0400003E RID: 62
		private readonly DLSSCommandInitializationData m_InitData;
	}
}
