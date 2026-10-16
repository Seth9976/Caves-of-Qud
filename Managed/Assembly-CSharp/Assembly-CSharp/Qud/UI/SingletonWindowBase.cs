using System;
using UnityEngine;

namespace Qud.UI
{
	// Token: 0x02000411 RID: 1041
	public class SingletonWindowBase<WindowType> : WindowBase where WindowType : class, new()
	{
		// Token: 0x06002775 RID: 10101 RVA: 0x000CCBBF File Offset: 0x000CADBF
		public GameObject FindChild(string path)
		{
			return base.gameObject.transform.Find(path).gameObject;
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x000CCBD7 File Offset: 0x000CADD7
		public ComponentType GetChildComponent<ComponentType>(string path) where ComponentType : class
		{
			return base.gameObject.transform.Find(path).GetComponent<ComponentType>();
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x000CCBEF File Offset: 0x000CADEF
		public override void Init()
		{
			SingletonWindowBase<WindowType>.instance = this as WindowType;
			base.Init();
		}

		// Token: 0x04001381 RID: 4993
		public static WindowType instance;
	}
}
