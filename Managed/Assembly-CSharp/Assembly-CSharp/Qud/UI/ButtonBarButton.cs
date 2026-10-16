using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x020004A7 RID: 1191
	public class ButtonBarButton : FrameworkUnityScrollChild, IFrameworkControl, IPointerClickHandler, IEventSystemHandler
	{
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002D9F RID: 11679 RVA: 0x000F94BD File Offset: 0x000F76BD
		public bool selected
		{
			get
			{
				NavigationContext navigationContext = this.GetNavigationContext();
				return navigationContext != null && navigationContext.IsActive(true);
			}
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000F94D1 File Offset: 0x000F76D1
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000F94DC File Offset: 0x000F76DC
		public void LateUpdate()
		{
			if (this.selectionBackgroundColor)
			{
				if (this.selected == this.wasSelected)
				{
					ButtonBar.ButtonBarButtonData.HighlightState highlightState = this.lastHighlightState;
					ButtonBar.ButtonBarButtonData buttonBarButtonData = this.data;
					if (highlightState == ((buttonBarButtonData != null) ? buttonBarButtonData.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.Normal) || !(this.background != null))
					{
						goto IL_00EF;
					}
				}
				Color color;
				if (this.data != null && this.data.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.Highlighted)
				{
					if (this.selected)
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color);
					}
					else
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFF00", out color);
					}
				}
				else if (this.data != null && this.data.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.NotHighlighted)
				{
					if (this.selected)
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color);
					}
					else
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#4A757E", out color);
					}
				}
				else if (this.selected)
				{
					global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color);
				}
				else
				{
					global::UnityEngine.ColorUtility.TryParseHtmlString("#4A757E", out color);
				}
				this.background.color = color;
			}
			IL_00EF:
			if (this.selectionTextColor)
			{
				if (this.selected == this.wasSelected)
				{
					ButtonBar.ButtonBarButtonData.HighlightState highlightState2 = this.lastHighlightState;
					ButtonBar.ButtonBarButtonData buttonBarButtonData2 = this.data;
					if (highlightState2 == ((buttonBarButtonData2 != null) ? buttonBarButtonData2.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.Normal) || !(this.text != null))
					{
						goto IL_01E9;
					}
				}
				Color color2;
				if (this.data != null && this.data.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.Highlighted)
				{
					if (this.selected)
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color2);
					}
					else
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#DFFFDF", out color2);
					}
				}
				else if (this.data != null && this.data.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.NotHighlighted)
				{
					if (this.selected)
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color2);
					}
					else
					{
						global::UnityEngine.ColorUtility.TryParseHtmlString("#4A757E", out color2);
					}
				}
				else if (this.selected)
				{
					global::UnityEngine.ColorUtility.TryParseHtmlString("#FFFFFF", out color2);
				}
				else
				{
					global::UnityEngine.ColorUtility.TryParseHtmlString("#4A757E", out color2);
				}
				this.text.color = color2;
				this.text.Apply();
			}
			IL_01E9:
			if (this.selectionIconColor)
			{
				if (this.selected == this.wasSelected)
				{
					ButtonBar.ButtonBarButtonData.HighlightState highlightState3 = this.lastHighlightState;
					ButtonBar.ButtonBarButtonData buttonBarButtonData3 = this.data;
					if (highlightState3 == ((buttonBarButtonData3 != null) ? buttonBarButtonData3.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.Normal))
					{
						goto IL_02B3;
					}
				}
				if (this.icon != null && this.data != null && this.data.icon != null && this.icon != null)
				{
					if (this.selected || this.data.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.Highlighted)
					{
						this.icon.FromRenderable(this.data.icon, true);
					}
					else
					{
						Renderable renderable = new Renderable(this.data.icon);
						renderable.DetailColor = 'K';
						renderable.ColorString = "&K";
						this.icon.FromRenderable(renderable, true);
					}
				}
			}
			IL_02B3:
			this.wasSelected = this.selected;
			ButtonBar.ButtonBarButtonData buttonBarButtonData4 = this.data;
			this.lastHighlightState = ((buttonBarButtonData4 != null) ? buttonBarButtonData4.Highlighted : ButtonBar.ButtonBarButtonData.HighlightState.Normal);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000F97C0 File Offset: 0x000F79C0
		public void setData(FrameworkDataElement data)
		{
			ButtonBar.ButtonBarButtonData buttonBarButtonData = data as ButtonBar.ButtonBarButtonData;
			if (buttonBarButtonData != null)
			{
				if (this.spacer != null)
				{
					this.spacer.SetActive(buttonBarButtonData.n != 0);
				}
				this.data = buttonBarButtonData;
				if (buttonBarButtonData.icon != null)
				{
					if (buttonBarButtonData.Highlighted == ButtonBar.ButtonBarButtonData.HighlightState.Highlighted)
					{
						this.icon.FromRenderable(buttonBarButtonData.icon, true);
					}
					else
					{
						Renderable renderable = new Renderable(buttonBarButtonData.icon);
						renderable.DetailColor = 'K';
						renderable.ColorString = "&K";
						this.icon.FromRenderable(renderable, true);
					}
				}
				buttonBarButtonData.button = this;
				this.text.SetText(buttonBarButtonData.label);
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000F986D File Offset: 0x000F7A6D
		public void HandleSelect()
		{
			ButtonBar.ButtonBarButtonData buttonBarButtonData = this.data;
			if (buttonBarButtonData == null)
			{
				return;
			}
			buttonBarButtonData.onSelect();
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000F9884 File Offset: 0x000F7A84
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.GetNavigationContext().IsActive(true) && eventData.button == PointerEventData.InputButton.Left)
			{
				this.HandleSelect();
			}
		}

		// Token: 0x040019DA RID: 6618
		public UIThreeColorProperties icon;

		// Token: 0x040019DB RID: 6619
		public UITextSkin text;

		// Token: 0x040019DC RID: 6620
		public GameObject spacer;

		// Token: 0x040019DD RID: 6621
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040019DE RID: 6622
		public static List<MenuOption> itemOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Select"
			}
		};

		// Token: 0x040019DF RID: 6623
		public Image background;

		// Token: 0x040019E0 RID: 6624
		public bool wasSelected;

		// Token: 0x040019E1 RID: 6625
		public ButtonBar.ButtonBarButtonData.HighlightState lastHighlightState = ButtonBar.ButtonBarButtonData.HighlightState.Invalid;

		// Token: 0x040019E2 RID: 6626
		public bool selectionBackgroundColor = true;

		// Token: 0x040019E3 RID: 6627
		public bool selectionTextColor;

		// Token: 0x040019E4 RID: 6628
		public bool selectionIconColor = true;

		// Token: 0x040019E5 RID: 6629
		public ButtonBar.ButtonBarButtonData data;
	}
}
