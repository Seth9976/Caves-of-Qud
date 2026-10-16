using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using UnityEngine;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x02000475 RID: 1141
	public class SkillsAndPowersLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts
	{
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x000E7AE8 File Offset: 0x000E5CE8
		public bool selected
		{
			get
			{
				SkillsAndPowersLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000E7AFC File Offset: 0x000E5CFC
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000E7B04 File Offset: 0x000E5D04
		public async void Accept()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			await APIDispatch.RunAndWaitAsync(delegate
			{
				SkillsAndPowersScreen.SelectNode(this.context.data.entry, this.context.data.screen.GO);
			});
			this.context.data.screen.UpdateData();
			this.context.data.screen.UpdateViewFromData();
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000E7B3C File Offset: 0x000E5D3C
		public void XAxis()
		{
			XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
			if (this.context.data.entry.Skill != null)
			{
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					if (!this.context.data.entry.Expand)
					{
						return;
					}
					this.context.data.entry.Expand = false;
				}
				else
				{
					num = currentEvent.axisValue;
					num2 = 0;
					if ((num.GetValueOrDefault() > num2) & (num != null))
					{
						if (this.context.data.entry.Expand)
						{
							return;
						}
						this.context.data.entry.Expand = true;
					}
				}
			}
			else
			{
				int num3 = 1 + this.context.data.entry.ParentNode.Skill.PowerList.IndexOf(this.context.data.entry.Power);
				this.context.data.screen.controller.scrollContext.SelectIndex(this.context.data.screen.controller.selectedPosition - num3);
				int? num = currentEvent.axisValue;
				int num2 = 0;
				if ((num.GetValueOrDefault() < num2) & (num != null))
				{
					if (!this.context.data.entry.ParentNode.Expand)
					{
						return;
					}
					this.context.data.entry.ParentNode.Expand = false;
				}
				else
				{
					num = currentEvent.axisValue;
					num2 = 0;
					if ((num.GetValueOrDefault() > num2) & (num != null))
					{
						if (this.context.data.entry.ParentNode.Expand)
						{
							return;
						}
						this.context.data.entry.ParentNode.Expand = true;
					}
				}
			}
			this.context.data.screen.UpdateViewFromData();
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x000E7D44 File Offset: 0x000E5F44
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			SkillsAndPowersLine.Context context = this.context;
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
			this.context.commandHandlers["Accept"] = new Action(this.Accept);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x000E7DCF File Offset: 0x000E5FCF
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000E7DEC File Offset: 0x000E5FEC
		public void ExpanderClicked()
		{
			if (this.context.IsActive(true))
			{
				this.context.data.entry.Expand = !this.context.data.entry.Expand;
				this.context.data.screen.UpdateViewFromData();
			}
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000E7E4C File Offset: 0x000E604C
		public void BodyClicked()
		{
			if (this.context.IsActive(true))
			{
				if (this.context.data.entry.Skill != null && this.context.data.screen.GO.HasPart(this.context.data.entry.Skill.Class))
				{
					this.ExpanderClicked();
					return;
				}
				this.Accept();
			}
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000E7EC4 File Offset: 0x000E60C4
		public void setData(FrameworkDataElement data)
		{
			SkillsAndPowersLineData d = data as SkillsAndPowersLineData;
			if (d != null)
			{
				this.context.data = d;
				this.skillType.SetActive(d.entry.Skill != null);
				this.powerType.SetActive(d.entry.Power != null);
				if (d.entry.Skill != null)
				{
					this.skillExpander.SetText(d.entry.Expand ? "[-]" : "[+]");
					Renderable renderable = new Renderable(d.entry.UIIcon);
					if (d.entry.IsLearned(d.go) == SPNode.LearnedStatus.None)
					{
						this.skillText.SetText(d.entry.Name ?? "");
						renderable.TileColor = "&K";
						renderable.DetailColor = 'k';
						if (d.go.GetStatValue("SP", 0) >= d.entry.Skill.Cost)
						{
							this.skillRightText.SetText(string.Format("Starting Cost {{{{g|[{0} sp]}}}}", d.entry.Skill.Cost));
						}
						else
						{
							this.skillRightText.SetText(string.Format("Starting Cost {{{{r|[{0} sp]}}}}", d.entry.Skill.Cost));
						}
					}
					else
					{
						int num = d.entry.powers.Count<SPNode>();
						int num2 = d.entry.powers.Where<SPNode>((SPNode p) => p.IsLearned(d.go) == SPNode.LearnedStatus.Learned).Count<SPNode>();
						if (d.entry.IsLearned(d.go) == SPNode.LearnedStatus.Partial)
						{
							this.skillText.SetText(" {{g|" + d.entry.Name + "}}");
							if (d.go.GetStatValue("SP", 0) >= d.entry.Skill.Cost)
							{
								this.skillRightText.SetText(string.Format("Starting Cost {{{{g|[{0} sp]}}}} [{1}/{2}]", d.entry.Skill.Cost, num2, num));
							}
							else
							{
								this.skillRightText.SetText(string.Format("Starting Cost {{{{r|[{0} sp]}}}} [{1}/{2}]", d.entry.Skill.Cost, num2, num));
							}
						}
						else
						{
							this.skillText.SetText(" {{G|" + d.entry.Name + "}}");
							this.skillRightText.SetText(string.Format("{{{{g|Learned}}}} [{0}/{1}]", num2, num));
						}
					}
					this.skillIcon.FromRenderable(renderable, true);
					return;
				}
				this.powerText.SetText(d.entry.ModernUIText(d.screen.GO) ?? "");
			}
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000E8244 File Offset: 0x000E6444
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

		// Token: 0x04001797 RID: 6039
		private SkillsAndPowersLine.Context context = new SkillsAndPowersLine.Context();

		// Token: 0x04001798 RID: 6040
		public UITextSkin skillText;

		// Token: 0x04001799 RID: 6041
		public UITextSkin skillRightText;

		// Token: 0x0400179A RID: 6042
		public UITextSkin skillExpander;

		// Token: 0x0400179B RID: 6043
		public UIThreeColorProperties skillIcon;

		// Token: 0x0400179C RID: 6044
		public UITextSkin powerText;

		// Token: 0x0400179D RID: 6045
		public GameObject skillType;

		// Token: 0x0400179E RID: 6046
		public GameObject powerType;

		// Token: 0x0400179F RID: 6047
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040017A0 RID: 6048
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040017A1 RID: 6049
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040017A2 RID: 6050
		public bool wasSelected;

		// Token: 0x040017A3 RID: 6051
		public bool wasHighlighted;

		// Token: 0x040017A4 RID: 6052
		public bool isHighlighted;

		// Token: 0x040017A5 RID: 6053
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018B3 RID: 6323
		public class Context : NavigationContext
		{
			// Token: 0x0400634C RID: 25420
			public SkillsAndPowersLineData data;
		}
	}
}
