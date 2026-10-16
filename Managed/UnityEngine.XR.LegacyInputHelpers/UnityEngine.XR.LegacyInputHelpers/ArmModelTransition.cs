using System;

namespace UnityEngine.XR.LegacyInputHelpers
{
	// Token: 0x02000007 RID: 7
	[Serializable]
	public class ArmModelTransition
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002FCD File Offset: 0x000011CD
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002FD5 File Offset: 0x000011D5
		public string transitionKeyName
		{
			get
			{
				return this.m_KeyName;
			}
			set
			{
				this.m_KeyName = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002FDE File Offset: 0x000011DE
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002FE6 File Offset: 0x000011E6
		public ArmModel armModel
		{
			get
			{
				return this.m_ArmModel;
			}
			set
			{
				this.m_ArmModel = value;
			}
		}

		// Token: 0x04000037 RID: 55
		[SerializeField]
		private string m_KeyName;

		// Token: 0x04000038 RID: 56
		[SerializeField]
		private ArmModel m_ArmModel;
	}
}
