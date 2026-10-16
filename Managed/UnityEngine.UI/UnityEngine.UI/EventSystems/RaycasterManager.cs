using System;
using System.Collections.Generic;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000070 RID: 112
	public static class RaycasterManager
	{
		// Token: 0x06000673 RID: 1651 RVA: 0x0001B803 File Offset: 0x00019A03
		internal static void AddRaycaster(BaseRaycaster baseRaycaster)
		{
			if (RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Add(baseRaycaster);
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001B81E File Offset: 0x00019A1E
		public static List<BaseRaycaster> GetRaycasters()
		{
			return RaycasterManager.s_Raycasters;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001B825 File Offset: 0x00019A25
		internal static void RemoveRaycasters(BaseRaycaster baseRaycaster)
		{
			if (!RaycasterManager.s_Raycasters.Contains(baseRaycaster))
			{
				return;
			}
			RaycasterManager.s_Raycasters.Remove(baseRaycaster);
		}

		// Token: 0x0400023C RID: 572
		private static readonly List<BaseRaycaster> s_Raycasters = new List<BaseRaycaster>();
	}
}
