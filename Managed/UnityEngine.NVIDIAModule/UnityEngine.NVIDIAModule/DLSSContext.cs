using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000010 RID: 16
	public class DLSSContext
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002840 File Offset: 0x00000A40
		public readonly ref DLSSCommandInitializationData initData
		{
			get
			{
				return ref this.m_InitData.Value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002860 File Offset: 0x00000A60
		public ref DLSSCommandExecutionData executeData
		{
			get
			{
				return ref this.m_ExecData.Value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002880 File Offset: 0x00000A80
		internal unsafe uint featureSlot
		{
			get
			{
				DLSSCommandInitializationData dlsscommandInitializationData = *this.initData;
				return dlsscommandInitializationData.featureSlot;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000028A5 File Offset: 0x00000AA5
		internal DLSSContext()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000028C5 File Offset: 0x00000AC5
		internal void Init(DLSSCommandInitializationData initSettings, uint featureSlot)
		{
			this.m_InitData.Value = initSettings;
			this.m_InitData.Value.featureSlot = featureSlot;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000028E6 File Offset: 0x00000AE6
		internal void Reset()
		{
			this.m_InitData.Value = default(DLSSCommandInitializationData);
			this.m_ExecData.Value = default(DLSSCommandExecutionData);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000290C File Offset: 0x00000B0C
		internal IntPtr GetInitCmdPtr()
		{
			return this.m_InitData.Ptr;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000292C File Offset: 0x00000B2C
		internal IntPtr GetExecuteCmdPtr()
		{
			this.m_ExecData.Value.featureSlot = this.featureSlot;
			return this.m_ExecData.Ptr;
		}

		// Token: 0x0400004B RID: 75
		private NativeData<DLSSCommandInitializationData> m_InitData = new NativeData<DLSSCommandInitializationData>();

		// Token: 0x0400004C RID: 76
		private NativeData<DLSSCommandExecutionData> m_ExecData = new NativeData<DLSSCommandExecutionData>();
	}
}
