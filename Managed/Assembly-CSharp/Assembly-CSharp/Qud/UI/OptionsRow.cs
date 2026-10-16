using System;
using ModelShark;
using UnityEngine;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000448 RID: 1096
	[ExecuteInEditMode]
	public class OptionsRow : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x060029A3 RID: 10659 RVA: 0x000D9EF0 File Offset: 0x000D80F0
		public void SetupContexts(ScrollChildContext scontext)
		{
			this.frameworkContext.context = scontext;
			IFrameworkControlSubcontexts frameworkControlSubcontexts = this.CurrentControl as IFrameworkControlSubcontexts;
			if (frameworkControlSubcontexts != null)
			{
				frameworkControlSubcontexts.SetupContexts(scontext);
				return;
			}
			if (scontext != null)
			{
				NavigationContext navigationContext;
				if ((navigationContext = this.proxyContext) == null)
				{
					navigationContext = (this.proxyContext = new NavigationContext());
				}
				scontext.proxyTo = navigationContext;
				this.proxyContext.parentContext = scontext;
			}
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x000D9F4D File Offset: 0x000D814D
		public NavigationContext GetNavigationContext()
		{
			return this.frameworkContext.context;
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x000D9F5C File Offset: 0x000D815C
		public void setData(FrameworkDataElement newData)
		{
			IFrameworkControl currentControl = this.CurrentControl;
			if (currentControl != null)
			{
				currentControl.setData(null);
			}
			this.data = newData as OptionsDataRow;
			OptionsDataRow optionsDataRow = this.data;
			if (string.IsNullOrEmpty((optionsDataRow != null) ? optionsDataRow.HelpText : null))
			{
				this.TooltipTrigger.enabled = false;
			}
			else
			{
				this.TooltipTrigger.enabled = true;
				this.TooltipTrigger.SetText("BodyText", RTF.FormatToRTF(this.data.HelpText, "FF", -1, false));
			}
			this.TooltipTrigger.maxTextWidth = (int)(base.transform as RectTransform).rect.width;
			bool flag = false;
			string text = ((newData != null) ? newData.GetType().Name : null);
			foreach (object obj in base.transform)
			{
				RectTransform rectTransform = (RectTransform)obj;
				if (rectTransform.name == text)
				{
					rectTransform.gameObject.SetActive(true);
					this.CurrentControl = rectTransform.GetComponent<IFrameworkControl>();
					IFrameworkControl currentControl2 = this.CurrentControl;
					if (currentControl2 != null)
					{
						currentControl2.setData(newData);
					}
					flag = true;
					if (text == "OptionsCategoryRow")
					{
						this.layoutGroup.enabled = false;
						this.layoutElement.minHeight = 20f;
					}
					else if (text == "OptionsSliderRow")
					{
						this.layoutGroup.enabled = false;
						this.layoutElement.minHeight = 50f;
					}
					else if (text == "OptionsCheckboxRow")
					{
						if (this.data.Title != null && this.data.Title.Length >= 120)
						{
							this.layoutGroup.enabled = true;
							this.layoutElement.minHeight = 40f;
						}
						else if (this.data.Title != null && this.data.Title.Length >= 67)
						{
							this.layoutGroup.enabled = false;
							this.layoutElement.minHeight = 40f;
						}
						else
						{
							this.layoutGroup.enabled = false;
							this.layoutElement.minHeight = 20f;
						}
					}
					else if (text == "OptionsMenubuttonRow")
					{
						this.layoutGroup.enabled = false;
						this.layoutElement.minHeight = 20f;
					}
					else
					{
						this.layoutGroup.enabled = true;
						this.layoutElement.minHeight = -1f;
					}
				}
				else if (rectTransform.name != "UISelectionBackground" && rectTransform.name != "SelectionCaret")
				{
					rectTransform.gameObject.SetActive(false);
				}
			}
			if (newData != null && !flag)
			{
				Transform transform = base.transform.Find("Unknown");
				transform.gameObject.SetActive(true);
				transform.GetComponent<UITextSkin>().SetText("Unhandled " + ((newData != null) ? newData.GetType().Name : null) + ":" + newData.Id);
				this.layoutGroup.enabled = false;
				this.layoutElement.minHeight = 20f;
			}
		}

		// Token: 0x060029A6 RID: 10662 RVA: 0x000DA2B0 File Offset: 0x000D84B0
		public void Update()
		{
			NavigationContext navigationContext = this.GetNavigationContext();
			bool? flag = ((navigationContext != null) ? new bool?(navigationContext.IsActive(true)) : null);
			bool? flag2 = this.wasSelected;
			bool? flag3 = flag;
			if (!((flag2.GetValueOrDefault() == flag3.GetValueOrDefault()) & (flag2 != null == (flag3 != null))))
			{
				this.wasSelected = flag;
				this.selectedMode = flag.GetValueOrDefault();
				if (this.selectedMode)
				{
					if (this.background.color.a != 0.2470588f)
					{
						this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0.2470588f);
						return;
					}
				}
				else if (this.background.color.a != 0f)
				{
					this.background.color = new Color(this.background.color.r, this.background.color.g, this.background.color.b, 0f);
				}
			}
		}

		// Token: 0x04001587 RID: 5511
		public Image background;

		// Token: 0x04001588 RID: 5512
		public NavigationContext proxyContext;

		// Token: 0x04001589 RID: 5513
		public FrameworkContext frameworkContext;

		// Token: 0x0400158A RID: 5514
		public OptionsDataRow data;

		// Token: 0x0400158B RID: 5515
		public TooltipTrigger TooltipTrigger;

		// Token: 0x0400158C RID: 5516
		public IFrameworkControl CurrentControl;

		// Token: 0x0400158D RID: 5517
		public bool selectedMode;

		// Token: 0x0400158E RID: 5518
		public VerticalLayoutGroup layoutGroup;

		// Token: 0x0400158F RID: 5519
		public LayoutElement layoutElement;

		// Token: 0x04001590 RID: 5520
		private bool? wasSelected;
	}
}
