using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002A6 RID: 678
	[RequiredByNativeCode]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class RuntimeInitializeOnLoadMethodAttribute : PreserveAttribute
	{
		// Token: 0x0600218A RID: 8586 RVA: 0x00041380 File Offset: 0x0003F580
		public RuntimeInitializeOnLoadMethodAttribute()
		{
			this.loadType = RuntimeInitializeLoadType.AfterSceneLoad;
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x00041392 File Offset: 0x0003F592
		public RuntimeInitializeOnLoadMethodAttribute(RuntimeInitializeLoadType loadType)
		{
			this.loadType = loadType;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x000413A4 File Offset: 0x0003F5A4
		// (set) Token: 0x0600218D RID: 8589 RVA: 0x000413BC File Offset: 0x0003F5BC
		public RuntimeInitializeLoadType loadType
		{
			get
			{
				return this.m_LoadType;
			}
			private set
			{
				this.m_LoadType = value;
			}
		}

		// Token: 0x040009D1 RID: 2513
		private RuntimeInitializeLoadType m_LoadType;
	}
}
