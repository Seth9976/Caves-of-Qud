using System;
using TMPro;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200041E RID: 1054
	public class LeftSideCategory : MonoBehaviour, IFrameworkControl
	{
		// Token: 0x060027FE RID: 10238 RVA: 0x000CEE8C File Offset: 0x000CD08C
		public NavigationContext GetNavigationContext()
		{
			return base.GetComponent<FrameworkContext>().context;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x000CEE99 File Offset: 0x000CD099
		public void Update()
		{
			if (Media.sizeClass != this.lastClass)
			{
				this.lastClass = Media.sizeClass;
				this.Respond();
			}
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x000CEEBC File Offset: 0x000CD0BC
		public void Respond()
		{
			this.lastClass = Media.sizeClass;
			if (Media.sizeClass < Media.SizeClass.Medium)
			{
				if (this.tmp.fontSize != 10f)
				{
					this.text.style = UITextSkin.Size.unset;
					this.tmp.fontSize = 10f;
					return;
				}
			}
			else if (this.tmp.fontSize != 16f)
			{
				this.text.style = UITextSkin.Size.unset;
				this.tmp.fontSize = 16f;
			}
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x000CEF3A File Offset: 0x000CD13A
		public void Awake()
		{
			this.Respond();
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x000CEF44 File Offset: 0x000CD144
		public void setData(FrameworkDataElement data)
		{
			KeybindCategoryRow keybindCategoryRow = data as KeybindCategoryRow;
			if (keybindCategoryRow != null)
			{
				this.text.SetText("{{C|" + keybindCategoryRow.CategoryDescription + "}}");
			}
			MenuOption menuOption = data as MenuOption;
			if (menuOption != null)
			{
				this.text.SetText("{{C|" + menuOption.getMenuText() + "}}");
			}
			HelpDataRow helpDataRow = data as HelpDataRow;
			if (helpDataRow != null)
			{
				this.text.SetText("{{C|" + helpDataRow.CategoryId + "}}");
			}
			OptionsCategoryRow optionsCategoryRow = data as OptionsCategoryRow;
			if (optionsCategoryRow != null)
			{
				this.text.SetText("{{C|" + optionsCategoryRow.CategoryId + "}}");
			}
		}

		// Token: 0x040013D7 RID: 5079
		public TextMeshProUGUI tmp;

		// Token: 0x040013D8 RID: 5080
		public UITextSkin text;

		// Token: 0x040013D9 RID: 5081
		private Media.SizeClass lastClass = Media.SizeClass.Unset;
	}
}
