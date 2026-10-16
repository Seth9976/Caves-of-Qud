using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200004E RID: 78
	public abstract class AbstractEventData
	{
		// Token: 0x06000534 RID: 1332 RVA: 0x00018143 File Offset: 0x00016343
		public virtual void Reset()
		{
			this.m_Used = false;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001814C File Offset: 0x0001634C
		public virtual void Use()
		{
			this.m_Used = true;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00018155 File Offset: 0x00016355
		public virtual bool used
		{
			get
			{
				return this.m_Used;
			}
		}

		// Token: 0x040001AE RID: 430
		protected bool m_Used;
	}
}
