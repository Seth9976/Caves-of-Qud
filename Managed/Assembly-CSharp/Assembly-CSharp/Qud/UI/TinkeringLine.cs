using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200047D RID: 1149
	public class TinkeringLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x000EA9C7 File Offset: 0x000E8BC7
		public bool selected
		{
			get
			{
				TinkeringLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000EA9DB File Offset: 0x000E8BDB
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x000EA9E4 File Offset: 0x000E8BE4
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			TinkeringLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.context.axisHandlers[InputAxisTypes.NavigationXAxis] = new Action(this.XAxis);
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x000EAA4E File Offset: 0x000E8C4E
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x000EAA68 File Offset: 0x000E8C68
		public void XAxis()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			if (this.context.data.category && this.context.data.screen != null)
			{
				Dictionary<string, bool> categoryCollapsed = this.context.data.screen.categoryCollapsed;
				string categoryName = this.context.data.categoryName;
				int? num = currentEvent.axisValue;
				int num2 = 0;
				categoryCollapsed[categoryName] = !((num.GetValueOrDefault() > num2) & (num != null));
				this.context.data.screen.UpdateViewFromData();
				return;
			}
			if (this.context.data.categoryName != null && this.context.data.screen != null)
			{
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					this.context.data.screen.controller.scrollContext.SelectIndex(this.context.data.screen.controller.scrollContext.selectedPosition - this.context.data.categoryOffset);
					this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, false);
					return;
				}
				this.context.data.screen.SetCategoryExpanded(this.context.data.categoryName, true);
			}
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x000EABF0 File Offset: 0x000E8DF0
		public void setData(FrameworkDataElement data)
		{
			TinkeringLineData tinkeringLineData = data as TinkeringLineData;
			if (tinkeringLineData != null)
			{
				if (tinkeringLineData.category)
				{
					GameObject[] array = this.lineObjects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(false);
					}
					array = this.categoryObjects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(true);
					}
					this.context.data = tinkeringLineData;
					if (tinkeringLineData.categoryName == "~<none>")
					{
						this.categoryText.SetText("{{K|You don't have any schematics.}}");
						return;
					}
					this.categoryText.SetText(string.Format("{0} {1} [{2}]", tinkeringLineData.categoryExpanded ? "[-]" : "[+]", tinkeringLineData.categoryName, tinkeringLineData.categoryCount));
					return;
				}
				else
				{
					GameObject[] array = this.lineObjects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(true);
					}
					array = this.categoryObjects;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].SetActive(false);
					}
					this.context.data = tinkeringLineData;
					if (tinkeringLineData.mode == 0)
					{
						this.text.SetText(string.Concat(new string[]
						{
							"    ",
							tinkeringLineData.data.DisplayName,
							" [",
							tinkeringLineData.costString,
							"]"
						}));
						return;
					}
					if (tinkeringLineData.mode == 1)
					{
						if (tinkeringLineData.modObject == null)
						{
							this.text.SetText("    <no applicable items>");
							return;
						}
						this.text.SetText(string.Concat(new string[]
						{
							"    ",
							tinkeringLineData.modObject.DisplayName,
							" [",
							tinkeringLineData.costString,
							"]"
						}));
					}
				}
			}
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000EADBD File Offset: 0x000E8FBD
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000EADC0 File Offset: 0x000E8FC0
		public int HighlightClosestSpacer(Vector2 screenPosition)
		{
			AbilityManagerSpacer abilityManagerSpacer = ((this.spacers.Count > 0) ? this.spacers[0] : null);
			float num = float.MaxValue;
			int num2 = -1;
			Vector3[] array = new Vector3[4];
			int num3 = 0;
			foreach (AbilityManagerSpacer abilityManagerSpacer2 in this.spacers)
			{
				(abilityManagerSpacer2.transform as RectTransform).GetWorldCorners(array);
				float magnitude = (new Vector2((array[0].x + array[2].x) / 2f, (array[0].y + array[2].y) / 2f) - screenPosition).magnitude;
				if (magnitude < num)
				{
					abilityManagerSpacer = abilityManagerSpacer2;
					num = magnitude;
					num2 = num3;
				}
				abilityManagerSpacer2.image.enabled = false;
				num3++;
			}
			if (abilityManagerSpacer != null)
			{
				abilityManagerSpacer.image.enabled = true;
			}
			return num2;
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000EAEE4 File Offset: 0x000E90E4
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000EAEE6 File Offset: 0x000E90E6
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000EAEE8 File Offset: 0x000E90E8
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x040017FB RID: 6139
		private TinkeringLine.Context context = new TinkeringLine.Context();

		// Token: 0x040017FC RID: 6140
		public UITextSkin text;

		// Token: 0x040017FD RID: 6141
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040017FE RID: 6142
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040017FF RID: 6143
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x04001800 RID: 6144
		public Image background;

		// Token: 0x04001801 RID: 6145
		public bool wasSelected;

		// Token: 0x04001802 RID: 6146
		public bool wasHighlighted;

		// Token: 0x04001803 RID: 6147
		public bool isHighlighted;

		// Token: 0x04001804 RID: 6148
		public GameObject[] categoryObjects;

		// Token: 0x04001805 RID: 6149
		public GameObject[] lineObjects;

		// Token: 0x04001806 RID: 6150
		public UITextSkin categoryText;

		// Token: 0x04001807 RID: 6151
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018C0 RID: 6336
		public class Context : NavigationContext
		{
			// Token: 0x0400636A RID: 25450
			public TinkeringLineData data;
		}
	}
}
