using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x0200000F RID: 15
	internal class InitDeviceContext
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00002764 File Offset: 0x00000964
		public InitDeviceContext(string projectId, string engineVersion, string appDir)
		{
			this.m_ProjectId.Str = projectId;
			this.m_EngineVersion.Str = engineVersion;
			this.m_AppDir.Str = appDir;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000027CC File Offset: 0x000009CC
		internal IntPtr GetInitCmdPtr()
		{
			this.m_InitData.Value.projectId = this.m_ProjectId.Ptr;
			this.m_InitData.Value.engineVersion = this.m_EngineVersion.Ptr;
			this.m_InitData.Value.appDir = this.m_AppDir.Ptr;
			return this.m_InitData.Ptr;
		}

		// Token: 0x04000047 RID: 71
		private NativeStr m_ProjectId = new NativeStr();

		// Token: 0x04000048 RID: 72
		private NativeStr m_EngineVersion = new NativeStr();

		// Token: 0x04000049 RID: 73
		private NativeStr m_AppDir = new NativeStr();

		// Token: 0x0400004A RID: 74
		private NativeData<InitDeviceCmdData> m_InitData = new NativeData<InitDeviceCmdData>();
	}
}
