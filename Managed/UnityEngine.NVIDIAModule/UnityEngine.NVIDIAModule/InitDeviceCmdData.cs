using System;

namespace UnityEngine.NVIDIA
{
	// Token: 0x02000005 RID: 5
	internal struct InitDeviceCmdData
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000205C File Offset: 0x0000025C
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002050 File Offset: 0x00000250
		public IntPtr projectId
		{
			get
			{
				return this.m_ProjectId;
			}
			set
			{
				this.m_ProjectId = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002074 File Offset: 0x00000274
		public IntPtr engineVersion
		{
			get
			{
				return this.m_EngineVersion;
			}
			set
			{
				this.m_EngineVersion = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public IntPtr appDir
		{
			get
			{
				return this.m_AppDir;
			}
			set
			{
				this.m_AppDir = value;
			}
		}

		// Token: 0x0400000D RID: 13
		private IntPtr m_ProjectId;

		// Token: 0x0400000E RID: 14
		private IntPtr m_EngineVersion;

		// Token: 0x0400000F RID: 15
		private IntPtr m_AppDir;
	}
}
