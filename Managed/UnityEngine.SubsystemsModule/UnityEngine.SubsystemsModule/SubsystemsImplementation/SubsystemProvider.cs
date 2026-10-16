using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000015 RID: 21
	public abstract class SubsystemProvider
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002E25 File Offset: 0x00001025
		public bool running
		{
			get
			{
				return this.m_Running;
			}
		}

		// Token: 0x04000014 RID: 20
		internal bool m_Running;
	}
}
