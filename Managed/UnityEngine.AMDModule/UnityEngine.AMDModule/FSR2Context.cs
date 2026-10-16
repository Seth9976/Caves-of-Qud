using System;

namespace UnityEngine.AMD
{
	// Token: 0x0200000A RID: 10
	public class FSR2Context
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002220 File Offset: 0x00000420
		public readonly ref FSR2CommandInitializationData initData
		{
			get
			{
				return ref this.m_InitData.Value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002240 File Offset: 0x00000440
		public ref FSR2CommandExecutionData executeData
		{
			get
			{
				return ref this.m_ExecData.Value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002260 File Offset: 0x00000460
		internal uint featureSlot
		{
			get
			{
				return this.initData.featureSlot;
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000227D File Offset: 0x0000047D
		internal FSR2Context()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000229D File Offset: 0x0000049D
		internal void Init(FSR2CommandInitializationData initSettings, uint featureSlot)
		{
			this.m_InitData.Value = initSettings;
			this.m_InitData.Value.featureSlot = featureSlot;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022BD File Offset: 0x000004BD
		internal void Reset()
		{
			this.m_InitData.Value = default(FSR2CommandInitializationData);
			this.m_ExecData.Value = default(FSR2CommandExecutionData);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022E4 File Offset: 0x000004E4
		internal IntPtr GetInitCmdPtr()
		{
			return this.m_InitData.Ptr;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002304 File Offset: 0x00000504
		internal IntPtr GetExecuteCmdPtr()
		{
			this.m_ExecData.Value.featureSlot = this.featureSlot;
			return this.m_ExecData.Ptr;
		}

		// Token: 0x04000037 RID: 55
		private NativeData<FSR2CommandInitializationData> m_InitData = new NativeData<FSR2CommandInitializationData>();

		// Token: 0x04000038 RID: 56
		private NativeData<FSR2CommandExecutionData> m_ExecData = new NativeData<FSR2CommandExecutionData>();
	}
}
