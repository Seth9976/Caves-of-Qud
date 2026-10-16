using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	// Token: 0x020014F8 RID: 5368
	public class KeyMenuOption : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x0600D1B3 RID: 53683 RVA: 0x004DE709 File Offset: 0x004DC909
		public void setData(FrameworkDataElement d)
		{
			if (d is MenuOption)
			{
				this.setDataMenuOption(d as MenuOption);
				return;
			}
			if (d is PrefixMenuOption)
			{
				this.setDataPrefixMenuOption(d as PrefixMenuOption);
				return;
			}
			throw new ArgumentException("KeyMenuOption expects MenuOption or PrefixMenuOption data");
		}

		// Token: 0x0600D1B4 RID: 53684 RVA: 0x004DE740 File Offset: 0x004DC940
		public void Render(string prefix, string text)
		{
			this.textSkin.SetText(text);
			if (string.IsNullOrEmpty(prefix))
			{
				this.prefixSkin.gameObject.SetActive(false);
				return;
			}
			this.prefixSkin.gameObject.SetActive(true);
			this.prefixSkin.SetText(prefix);
		}

		// Token: 0x0600D1B5 RID: 53685 RVA: 0x004DE792 File Offset: 0x004DC992
		public void setDataPrefixMenuOption(PrefixMenuOption data)
		{
			this.Render(data.Prefix, data.Description);
		}

		// Token: 0x0600D1B6 RID: 53686 RVA: 0x004DE7A8 File Offset: 0x004DC9A8
		public void setDataMenuOption(MenuOption data)
		{
			string text = null;
			string keyDescription = data.getKeyDescription();
			if (!string.IsNullOrEmpty(keyDescription))
			{
				text = "[{{W|" + keyDescription + "}}]";
			}
			this.Render(text, data.Description);
			this.GetNavigationContext().disabled = data.disabled;
		}

		// Token: 0x0600D1B7 RID: 53687 RVA: 0x004DE7F5 File Offset: 0x004DC9F5
		public NavigationContext GetNavigationContext()
		{
			FrameworkContext component = base.GetComponent<FrameworkContext>();
			if (component == null)
			{
				return null;
			}
			return component.context;
		}

		// Token: 0x040052E4 RID: 21220
		public UITextSkin textSkin;

		// Token: 0x040052E5 RID: 21221
		public UITextSkin prefixSkin;
	}
}
