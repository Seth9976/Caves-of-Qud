using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000461 RID: 1121
	public class FactionsLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06002A73 RID: 10867 RVA: 0x000DF8E0 File Offset: 0x000DDAE0
		public bool selected
		{
			get
			{
				FactionsLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x000DF8F4 File Offset: 0x000DDAF4
		public FactionsStatusScreen screen
		{
			get
			{
				return this.context.screen;
			}
		}

		// Token: 0x06002A75 RID: 10869 RVA: 0x000DF901 File Offset: 0x000DDB01
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002A76 RID: 10870 RVA: 0x000DF909 File Offset: 0x000DDB09
		public void XAxis()
		{
			global::XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			this.context.data.expanded = !this.context.data.expanded;
			this.context.screen.ExpansionUpdated();
		}

		// Token: 0x06002A77 RID: 10871 RVA: 0x000DF944 File Offset: 0x000DDB44
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			FactionsLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.context.axisHandlers[InputAxisTypes.NavigationXAxis] = new Action(this.XAxis);
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action> { 
				{
					"Accept",
					global::XRL.UI.Framework.Event.Helpers.Handle(new Action(this.ToggleExpand))
				} };
			}
			NavigationContext navigationContext = this.context;
			FactionsLineData data = this.context.data;
			navigationContext.menuOptionDescriptions = ((data != null && data.expanded) ? FactionsLine.categoryCollapseOptions : FactionsLine.categoryExpandOptions);
		}

		// Token: 0x06002A78 RID: 10872 RVA: 0x000DF9FA File Offset: 0x000DDBFA
		public void ToggleExpand()
		{
			FactionsStatusScreen screen = this.screen;
			if (screen == null)
			{
				return;
			}
			screen.ToggleExpanded(this.context.data);
		}

		// Token: 0x06002A79 RID: 10873 RVA: 0x000DFA17 File Offset: 0x000DDC17
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002A7A RID: 10874 RVA: 0x000DFA34 File Offset: 0x000DDC34
		public void setData(FrameworkDataElement data)
		{
			FactionsLineData factionsLineData = data as FactionsLineData;
			if (factionsLineData != null)
			{
				this.context.data = factionsLineData;
				if (factionsLineData.expanded != this.detailsSection.activeSelf)
				{
					this.detailsSection.SetActive(factionsLineData.expanded);
				}
				XRLGame game = The.Game;
				int? num;
				if (game == null)
				{
					num = null;
				}
				else
				{
					Reputation playerReputation = game.PlayerReputation;
					num = ((playerReputation != null) ? new int?(playerReputation.Get(factionsLineData.id)) : null);
				}
				int? num2 = num;
				Color color = ConsoleLib.Console.ColorUtility.colorFromChar(Reputation.GetColor(num2.GetValueOrDefault()));
				this.reputationIndicator.color = color;
				this.expanderText.SetText(factionsLineData.expanded ? "{{C|-}}" : "{{C|+}}");
				this.barText.SetText(factionsLineData.label);
				this.barReputationText.SetText("Reputation: " + FactionsScreen.FormatFactionReputation(factionsLineData.id));
				Faction faction = Factions.Get(factionsLineData.id);
				this.detailsText.SetText(faction.GetFeelingText());
				string text = faction.GetRankText();
				if (!string.IsNullOrEmpty(text))
				{
					text = text + " " + faction.GetPetText();
				}
				else
				{
					text = faction.GetPetText();
				}
				if (!string.IsNullOrEmpty(text))
				{
					text = text + " " + faction.GetHolyPlaceText();
				}
				else
				{
					text = faction.GetHolyPlaceText();
				}
				this.detailsText3.blockWrap = ((Media.sizeClass <= Media.SizeClass.Small) ? 40 : 60);
				this.detailsText2.SetText(text);
				this.detailsText3.SetText(Faction.GetPreferredSecretDescription(factionsLineData.id));
				this.icon.FromRenderable(factionsLineData.icon, true);
			}
		}

		// Token: 0x06002A7B RID: 10875 RVA: 0x000DFBE5 File Offset: 0x000DDDE5
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A7C RID: 10876 RVA: 0x000DFBE8 File Offset: 0x000DDDE8
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

		// Token: 0x06002A7D RID: 10877 RVA: 0x000DFD0C File Offset: 0x000DDF0C
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A7E RID: 10878 RVA: 0x000DFD0E File Offset: 0x000DDF0E
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002A7F RID: 10879 RVA: 0x000DFD10 File Offset: 0x000DDF10
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x04001664 RID: 5732
		private FactionsLine.Context context = new FactionsLine.Context();

		// Token: 0x04001665 RID: 5733
		public Image reputationIndicator;

		// Token: 0x04001666 RID: 5734
		public UITextSkin expanderText;

		// Token: 0x04001667 RID: 5735
		public UITextSkin barText;

		// Token: 0x04001668 RID: 5736
		public UITextSkin barReputationText;

		// Token: 0x04001669 RID: 5737
		public UITextSkin detailsText;

		// Token: 0x0400166A RID: 5738
		public global::UnityEngine.GameObject detailsSpacer1;

		// Token: 0x0400166B RID: 5739
		public UITextSkin detailsText2;

		// Token: 0x0400166C RID: 5740
		public global::UnityEngine.GameObject detailsSpacer2;

		// Token: 0x0400166D RID: 5741
		public UITextSkin detailsText3;

		// Token: 0x0400166E RID: 5742
		public UITextSkin check;

		// Token: 0x0400166F RID: 5743
		public UITextSkin hotkey;

		// Token: 0x04001670 RID: 5744
		public UIThreeColorProperties icon;

		// Token: 0x04001671 RID: 5745
		public global::UnityEngine.GameObject iconSpacer;

		// Token: 0x04001672 RID: 5746
		public global::UnityEngine.GameObject detailsSection;

		// Token: 0x04001673 RID: 5747
		public StringBuilder SB = new StringBuilder();

		// Token: 0x04001674 RID: 5748
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x04001675 RID: 5749
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x04001676 RID: 5750
		public Image background;

		// Token: 0x04001677 RID: 5751
		public bool wasSelected;

		// Token: 0x04001678 RID: 5752
		public bool wasHighlighted;

		// Token: 0x04001679 RID: 5753
		public bool isHighlighted;

		// Token: 0x0400167A RID: 5754
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x02001881 RID: 6273
		public class Context : NavigationContext
		{
			// Token: 0x17000E44 RID: 3652
			// (get) Token: 0x0600E0C6 RID: 57542 RVA: 0x0051ADA6 File Offset: 0x00518FA6
			public FactionsStatusScreen.Context screenContext
			{
				get
				{
					return this.parents.OfType<FactionsStatusScreen.Context>().FirstOrDefault<FactionsStatusScreen.Context>();
				}
			}

			// Token: 0x17000E45 RID: 3653
			// (get) Token: 0x0600E0C7 RID: 57543 RVA: 0x0051ADB8 File Offset: 0x00518FB8
			public FactionsStatusScreen screen
			{
				get
				{
					FactionsStatusScreen.Context screenContext = this.screenContext;
					if (screenContext == null)
					{
						return null;
					}
					return screenContext.screen;
				}
			}

			// Token: 0x04006298 RID: 25240
			public FactionsLineData data;
		}
	}
}
