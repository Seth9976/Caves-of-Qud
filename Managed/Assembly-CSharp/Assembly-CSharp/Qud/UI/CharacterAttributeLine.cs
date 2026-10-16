using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.Rules;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x0200045A RID: 1114
	public class CharacterAttributeLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06002A34 RID: 10804 RVA: 0x000DDB1F File Offset: 0x000DBD1F
		public bool selected
		{
			get
			{
				CharacterAttributeLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000DDB33 File Offset: 0x000DBD33
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x000DDB3C File Offset: 0x000DBD3C
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			CharacterAttributeLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x000DDB89 File Offset: 0x000DBD89
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x000DDBA4 File Offset: 0x000DBDA4
		public void setData(FrameworkDataElement data)
		{
			CharacterAttributeLineData characterAttributeLineData = data as CharacterAttributeLineData;
			if (characterAttributeLineData != null)
			{
				this.context.data = characterAttributeLineData;
				this.attributeText.SetText((characterAttributeLineData.data == null) ? characterAttributeLineData.stat : characterAttributeLineData.data.GetShortDisplayName());
				string text = "C";
				if (characterAttributeLineData.data != null)
				{
					if (characterAttributeLineData.data.Value > characterAttributeLineData.data.BaseValue)
					{
						text = "G";
					}
					else if (characterAttributeLineData.data.Value < characterAttributeLineData.data.BaseValue)
					{
						text = "r";
					}
				}
				if (characterAttributeLineData.stat == "CP")
				{
					text = "C";
					this.valueText.SetText(string.Format("{{{{{0}|{1}}}}}", text, CharacterStatusScreen.CP));
				}
				else if (characterAttributeLineData.data.GetShortDisplayName() == "MS")
				{
					if (characterAttributeLineData.data.Value < characterAttributeLineData.data.BaseValue)
					{
						text = "G";
					}
					else if (characterAttributeLineData.data.Value > characterAttributeLineData.data.BaseValue)
					{
						text = "r";
					}
					this.valueText.SetText(string.Concat(new string[]
					{
						"{{",
						text,
						"|",
						(100 - characterAttributeLineData.data.Value + 100).ToStringCached(),
						"}}"
					}));
				}
				else if (characterAttributeLineData.data.GetShortDisplayName() == "AV")
				{
					this.valueText.SetText(string.Format("{{{{{0}|{1}}}}}", text, Stats.GetCombatAV(characterAttributeLineData.go)));
				}
				else if (characterAttributeLineData.data.GetShortDisplayName() == "DV")
				{
					this.valueText.SetText(string.Format("{{{{{0}|{1}}}}}", text, Stats.GetCombatDV(characterAttributeLineData.go)));
				}
				else if (characterAttributeLineData.data.GetShortDisplayName() == "MA")
				{
					this.valueText.SetText(string.Format("{{{{{0}|{1}}}}}", text, Stats.GetCombatMA(characterAttributeLineData.go)));
				}
				else
				{
					this.valueText.SetText(string.Concat(new string[]
					{
						"{{",
						text,
						"|",
						characterAttributeLineData.data.Value.ToStringCached(),
						"}}"
					}));
				}
				if (characterAttributeLineData.data != null)
				{
					UITextSkin uitextSkin = this.modifierText;
					if (uitextSkin == null)
					{
						return;
					}
					uitextSkin.SetText(((characterAttributeLineData.data.Modifier > -1) ? "{{G|[+" : "{{R|[") + characterAttributeLineData.data.Modifier.ToStringCached() + "]}}");
				}
			}
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x000DDE7D File Offset: 0x000DC07D
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A3A RID: 10810 RVA: 0x000DDE80 File Offset: 0x000DC080
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

		// Token: 0x06002A3B RID: 10811 RVA: 0x000DDFA4 File Offset: 0x000DC1A4
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A3C RID: 10812 RVA: 0x000DDFA6 File Offset: 0x000DC1A6
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x000DDFA8 File Offset: 0x000DC1A8
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001615 RID: 5653
		private CharacterAttributeLine.Context context = new CharacterAttributeLine.Context();

		// Token: 0x04001616 RID: 5654
		public UITextSkin attributeText;

		// Token: 0x04001617 RID: 5655
		public UITextSkin valueText;

		// Token: 0x04001618 RID: 5656
		public UITextSkin modifierText;

		// Token: 0x04001619 RID: 5657
		public StringBuilder SB = new StringBuilder();

		// Token: 0x0400161A RID: 5658
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x0400161B RID: 5659
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x0400161C RID: 5660
		public Image background;

		// Token: 0x0400161D RID: 5661
		public bool wasSelected;

		// Token: 0x0400161E RID: 5662
		public bool wasHighlighted;

		// Token: 0x0400161F RID: 5663
		public bool isHighlighted;

		// Token: 0x04001620 RID: 5664
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x0200186F RID: 6255
		public class Context : NavigationContext
		{
			// Token: 0x0400626C RID: 25196
			public CharacterAttributeLineData data;
		}
	}
}
