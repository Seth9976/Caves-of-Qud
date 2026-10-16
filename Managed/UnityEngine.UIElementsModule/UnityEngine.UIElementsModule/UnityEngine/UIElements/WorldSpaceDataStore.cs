using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DD RID: 1501
	internal static class WorldSpaceDataStore
	{
		// Token: 0x06002C40 RID: 11328 RVA: 0x0009FD3B File Offset: 0x0009DF3B
		public static void SetWorldSpaceData(VisualElement ve, WorldSpaceData data)
		{
			WorldSpaceDataStore.m_WorldSpaceData[ve.controlid] = data;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0009FD50 File Offset: 0x0009DF50
		public static WorldSpaceData GetWorldSpaceData(VisualElement ve)
		{
			return WorldSpaceDataStore.m_WorldSpaceData[ve.controlid];
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0009FD74 File Offset: 0x0009DF74
		public static void ClearWorldSpaceData(VisualElement ve)
		{
			ve.isLocalBounds3DDirty = true;
			ve.needs3DBounds = false;
			WorldSpaceDataStore.m_WorldSpaceData.Remove(ve.controlid);
			for (int i = ve.hierarchy.childCount - 1; i >= 0; i--)
			{
				WorldSpaceDataStore.ClearWorldSpaceData(ve.hierarchy[i]);
			}
		}

		// Token: 0x04001440 RID: 5184
		private static Dictionary<uint, WorldSpaceData> m_WorldSpaceData = new Dictionary<uint, WorldSpaceData>();
	}
}
