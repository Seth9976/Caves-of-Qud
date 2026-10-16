using System;
using UnityEngine;
using UnityEngine.UI;

namespace XRL.UI.Framework
{
	// Token: 0x020014F7 RID: 5367
	public class FrameworkHeader : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1B0 RID: 53680 RVA: 0x004DE690 File Offset: 0x004DC890
		public void setData(FrameworkDataElement d)
		{
			Image[] componentsInChildren = base.GetComponentsInChildren<Image>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].color = this.headerColor;
			}
			this.textSkin.color = this.headerColor;
			this.textSkin.SetText(d.Description);
		}

		// Token: 0x0600D1B1 RID: 53681 RVA: 0x004DE6E3 File Offset: 0x004DC8E3
		public NavigationContext GetNavigationContext()
		{
			FrameworkContext component = base.GetComponent<FrameworkContext>();
			if (component == null)
			{
				return null;
			}
			return component.context;
		}

		// Token: 0x040052E2 RID: 21218
		public UITextSkin textSkin;

		// Token: 0x040052E3 RID: 21219
		public Color headerColor = Color.white;
	}
}
