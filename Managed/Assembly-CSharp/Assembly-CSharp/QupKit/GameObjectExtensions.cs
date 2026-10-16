using System;
using UnityEngine;

namespace QupKit
{
	// Token: 0x020003A3 RID: 931
	public static class GameObjectExtensions
	{
		// Token: 0x060022F3 RID: 8947 RVA: 0x000B1B9E File Offset: 0x000AFD9E
		public static GameObject AddChild(this GameObject go, GameObject childGo)
		{
			if (childGo.transform.parent != go)
			{
				childGo.transform.SetParent(go.transform, false);
			}
			return childGo;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000B1BC8 File Offset: 0x000AFDC8
		public static void ForeachChild(this GameObject go, Action<GameObject> action)
		{
			for (int i = go.transform.childCount - 1; i >= 0; i--)
			{
				action(go.transform.GetChild(i).gameObject);
			}
		}
	}
}
