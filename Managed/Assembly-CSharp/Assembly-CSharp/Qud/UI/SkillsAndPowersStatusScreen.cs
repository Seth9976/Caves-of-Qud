using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using FuzzySharp;
using FuzzySharp.Extractor;
using UnityEngine;
using UnityEngine.Events;
using XRL;
using XRL.Language;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Skills;

namespace Qud.UI
{
	// Token: 0x02000477 RID: 1143
	[UIView("SkillsAndPowersStatusScreen", false, false, false, null, null, false, 0, false, NavCategory = "Menus", UICanvas = "StatusScreens", UICanvasHost = 1)]
	public class SkillsAndPowersStatusScreen : BaseStatusScreen<SkillsAndPowersStatusScreen>
	{
		// Token: 0x06002B84 RID: 11140 RVA: 0x000E8476 File Offset: 0x000E6676
		public override IRenderable GetTabIcon()
		{
			if (this.icon == null)
			{
				this.icon = new Renderable("Items/sw_longblade_3.bmp", " ", "&B", null, 'w');
			}
			return this.icon;
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x000E84A3 File Offset: 0x000E66A3
		public override string GetTabString()
		{
			return "Skills";
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000E84AA File Offset: 0x000E66AA
		public void Left()
		{
			global::XRL.UI.Framework.Event currentEvent = NavigationController.currentEvent;
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000E84B2 File Offset: 0x000E66B2
		public void Right()
		{
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000E84B4 File Offset: 0x000E66B4
		public void UpdateDetailsFromNode(SPNode node)
		{
			this.detailsText.SetText(node.Description);
			this.skillIcon.FromRenderable(node.UIIcon, true);
			this.smallSkillIcon.FromRenderable(node.UIIcon, true);
			this.skillNameText.SetText(node.Name);
			SPNode.LearnedStatus learnedStatus = node.IsLearned(this.GO);
			if (learnedStatus == SPNode.LearnedStatus.None)
			{
				this.learnedText.SetText("{{R|[Unlearned]}}");
			}
			if (learnedStatus == SPNode.LearnedStatus.Partial)
			{
				this.learnedText.SetText("{{W|[Unlearned]}}");
			}
			if (learnedStatus == SPNode.LearnedStatus.Learned)
			{
				this.learnedText.SetText("{{G|[Learned]}}");
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (learnedStatus == SPNode.LearnedStatus.Learned)
			{
				stringBuilder.Length = 0;
			}
			else if (node.Skill != null)
			{
				if (this.GO.GetStatValue("SP", 0) >= node.Skill.Cost)
				{
					stringBuilder.Append(string.Format(":: {{{{C|{0}}}}} SP ::", node.Skill.Cost));
				}
				else
				{
					stringBuilder.Append(string.Format(":: {{{{R|{0}}}}} SP ::", node.Skill.Cost));
				}
			}
			else if (this.GO.GetStatValue("SP", 0) >= node.Power.Cost)
			{
				stringBuilder.Append(string.Format(":: {{{{C|{0}}}}} SP ::", node.Power.Cost));
			}
			else
			{
				stringBuilder.Append(string.Format(":: {{{{R|{0}}}}} SP ::", node.Power.Cost));
			}
			if (learnedStatus == SPNode.LearnedStatus.None && node.Power != null)
			{
				int num = 0;
				foreach (PowerEntryRequirement powerEntryRequirement in node.Power.requirements)
				{
					if (num == 0)
					{
						stringBuilder.Append("\n:: ");
					}
					else
					{
						stringBuilder.Append(" or \n:: ");
					}
					num++;
					powerEntryRequirement.Render(this.GO, stringBuilder);
					stringBuilder.Append(" ::\n");
				}
			}
			this.requirementsText.SetText(stringBuilder.ToString());
			if (learnedStatus == SPNode.LearnedStatus.Learned)
			{
				this.requiredSkillsHeader.SetActive(false);
			}
			else
			{
				this.requiredSkillsHeader.SetActive(true);
			}
			if (learnedStatus == SPNode.LearnedStatus.Learned)
			{
				this.requiredSkillsText.SetText("");
				return;
			}
			if (node.Skill != null)
			{
				this.requiredSkillsText.SetText("{{K|[none]}}");
				return;
			}
			string text = "";
			if (string.IsNullOrEmpty(node.Power.Requires))
			{
				text = "{{K|[none]}}";
			}
			else
			{
				foreach (string text2 in node.Power.Requires.CachedCommaExpansion())
				{
					string text3 = text2;
					bool flag = false;
					IBaseSkillEntry baseSkillEntry;
					if (SkillFactory.Factory.TryGetFirstEntry(text2, out baseSkillEntry))
					{
						if (node.Power.IsSkillInitiatory)
						{
							int num2 = node.Power.ParentSkill.PowerList.IndexOf(node.Power);
							if (num2 > 0 && node.Power.ParentSkill.PowerList[num2 - 1] == baseSkillEntry)
							{
								continue;
							}
						}
						text3 = baseSkillEntry.Name;
						flag = this.GO.HasSkill(text2);
					}
					else if (MutationFactory.HasMutation(text2))
					{
						text3 = MutationFactory.GetMutationEntryByName(text2).Name;
						flag = this.GO.HasPart(text2);
					}
					if (flag)
					{
						if (text != "")
						{
							text += "\n";
						}
						text = text + "{{G|[" + text3 + "]}}";
					}
					else
					{
						if (text != "")
						{
							text += "\n";
						}
						text = text + "{{R|[" + text3 + "]}}";
					}
				}
			}
			if (node.Power.Exclusion != null)
			{
				foreach (string text4 in node.Power.Exclusion.CachedCommaExpansion())
				{
					string text5 = text4;
					bool flag2 = false;
					IBaseSkillEntry baseSkillEntry2;
					if (SkillFactory.Factory.TryGetFirstEntry(text4, out baseSkillEntry2))
					{
						text5 = baseSkillEntry2.Name;
						flag2 = !this.GO.HasSkill(text4);
					}
					else if (MutationFactory.HasMutation(text4))
					{
						text5 = MutationFactory.GetMutationEntryByName(text4).Name;
						flag2 = !this.GO.HasPart(text4);
					}
					if (flag2)
					{
						if (text != "")
						{
							text += "\n";
						}
						text = text + "Ex: {{g|" + text5 + "}}";
					}
					else
					{
						if (text != "")
						{
							text += "\n";
						}
						text = text + "Ex: {{R|" + text5 + "}}";
					}
				}
			}
			this.requiredSkillsText.SetText(text);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000E89F8 File Offset: 0x000E6BF8
		public void HandleSelectItem(FrameworkDataElement element)
		{
			SkillsAndPowersLineData skillsAndPowersLineData = element as SkillsAndPowersLineData;
			if (skillsAndPowersLineData != null)
			{
				this.UpdateDetailsFromNode(skillsAndPowersLineData.entry);
			}
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000E8A1C File Offset: 0x000E6C1C
		public void HandleHighlightObject(FrameworkDataElement element)
		{
			SkillsAndPowersLineData skillsAndPowersLineData = element as SkillsAndPowersLineData;
			if (skillsAndPowersLineData != null)
			{
				this.UpdateDetailsFromNode(skillsAndPowersLineData.entry);
			}
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000E8A40 File Offset: 0x000E6C40
		public void HandleVPositive()
		{
			foreach (SPNode spnode in SkillsAndPowersScreen.Nodes)
			{
				if (spnode.ParentNode == null)
				{
					spnode.Expand = true;
				}
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000E8AA0 File Offset: 0x000E6CA0
		public void HandleVNegative()
		{
			foreach (SPNode spnode in SkillsAndPowersScreen.Nodes)
			{
				if (spnode.ParentNode == null)
				{
					spnode.Expand = false;
				}
			}
			this.UpdateViewFromData();
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000E8B00 File Offset: 0x000E6D00
		public override NavigationContext ShowScreen(global::XRL.World.GameObject GO, StatusScreensScreen parent)
		{
			this.filterText = null;
			this.GO = GO;
			if (GO != null)
			{
				this.nameBlockText.SetText(Grammar.MakePossessive(GO.DisplayName ?? "") + " Skills");
				this.statBlockText.SetText(string.Format("{{{{K|{{{{g|STR:}}}}{0} ■ {{{{g|AGI}}}}: {1} ■ {{{{g|TOU}}}}: {2} ■ {{{{g|INT}}}}: {3} ■ {{{{g|WIL}}}}: {4} ■ {{{{g|EGO}}}}: {5}}}}}", new object[]
				{
					GO.GetStat("Strength").Value,
					GO.GetStat("Agility").Value,
					GO.GetStat("Toughness").Value,
					GO.GetStat("Intelligence").Value,
					GO.GetStat("Willpower").Value,
					GO.GetStat("Ego").Value
				}));
				this.playerIcon.FromRenderable(GO.RenderForUI(null, false), true);
			}
			this.categoryBar.SetupContext(InputAxisTypes.NavigationXAxis);
			this.vertNav.SetAxis(InputAxisTypes.NavigationYAxis, false);
			this.vertNav.contexts.Add(this.categoryBar.scrollContext);
			this.vertNav.contexts.Add(this.controller.scrollContext);
			this.vertNav.wraps = false;
			this.vertNav.Setup();
			this.controller.onSelected.RemoveAllListeners();
			this.controller.onSelected.AddListener(new UnityAction<FrameworkDataElement>(this.HandleSelectItem));
			this.controller.onHighlight.RemoveAllListeners();
			this.controller.onHighlight.AddListener(new UnityAction<FrameworkDataElement>(this.HandleHighlightObject));
			this.controller.scrollContext.wraps = false;
			this.controller.scrollContext.SetAxis(InputAxisTypes.NavigationYAxis, false);
			ScrollContext<FrameworkDataElement, NavigationContext> scrollContext = this.controller.scrollContext;
			if (scrollContext.axisHandlers == null)
			{
				scrollContext.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			this.controller.scrollContext.axisHandlers[InputAxisTypes.NavigationXAxis] = global::XRL.UI.Framework.Event.Helpers.Handle(global::XRL.UI.Framework.Event.Helpers.Axis(new Action(this.Left), new Action(this.Right), null));
			scrollContext = this.controller.scrollContext;
			if (scrollContext.commandHandlers == null)
			{
				scrollContext.commandHandlers = new Dictionary<string, Action>();
			}
			this.controller.scrollContext.commandHandlers["V Positive"] = new Action(this.HandleVPositive);
			this.controller.scrollContext.commandHandlers["V Negative"] = new Action(this.HandleVNegative);
			base.ShowScreen(GO, parent);
			this.UpdateData();
			this.UpdateViewFromData();
			if (this.lineData.Count > 0)
			{
				this.UpdateDetailsFromNode(this.lineData.First<SkillsAndPowersLineData>().entry);
			}
			this.controller.selectedPosition = 0;
			this.controller.scrollContext.ActivateAndEnable();
			return this.vertNav;
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000E8E0C File Offset: 0x000E700C
		public void UpdateData()
		{
			SkillsAndPowersScreen.BuildNodes(StatusScreensScreen.GO, false);
			if (this.GO != null)
			{
				this.spText.SetText(string.Format("Skill Points (SP): {{{{C|{0}}}}}", this.GO.GetStat("SP").Value));
			}
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000E8E5C File Offset: 0x000E705C
		public bool IsSearching()
		{
			return !this.filterText.IsNullOrEmpty();
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000E8E6C File Offset: 0x000E706C
		public override void UpdateViewFromData()
		{
			this.lineData.ForEach(delegate(SkillsAndPowersLineData l)
			{
				l.free();
			});
			this.lineData.Clear();
			this.lineData.AddRange(from spnode in SkillsAndPowersScreen.Nodes
				where spnode.ParentNode == null || spnode.ParentNode.Expand || this.IsSearching()
				select spnode into n
				select PooledFrameworkDataElement<SkillsAndPowersLineData>.next().set(n, this, this.GO));
			if (this.searcher == null)
			{
				this.searcher = new SkillsAndPowersLineData();
				this.searcher.entry = new SPNode(null, null, false, null);
			}
			SPNode entry = this.searcher.entry;
			string filterText = this.filterText;
			entry._SearchText = ((filterText != null) ? filterText.ToLower() : null);
			if (this.filterText.IsNullOrEmpty())
			{
				this.controller.BeforeShow(this.lineData);
				return;
			}
			List<SkillsAndPowersLineData> searchResult = (from m in Process.ExtractTop<SkillsAndPowersLineData>(this.searcher, this.lineData, (SkillsAndPowersLineData i) => i.entry.SearchText.ToLower(), null, this.lineData.Count, 50)
				select m.Value).ToList<SkillsAndPowersLineData>();
			this.controller.BeforeShow(this.lineData.Where<SkillsAndPowersLineData>((SkillsAndPowersLineData i) => searchResult.Contains(i) || searchResult.Any<SkillsAndPowersLineData>((SkillsAndPowersLineData r) => r.entry.ParentNode == i.entry)));
		}

		// Token: 0x040017A9 RID: 6057
		public ScrollContext<NavigationContext> vertNav = new ScrollContext<NavigationContext>();

		// Token: 0x040017AA RID: 6058
		public ButtonBar categoryBar;

		// Token: 0x040017AB RID: 6059
		public FrameworkScroller controller;

		// Token: 0x040017AC RID: 6060
		public UIThreeColorProperties skillIcon;

		// Token: 0x040017AD RID: 6061
		public UIThreeColorProperties smallSkillIcon;

		// Token: 0x040017AE RID: 6062
		private Renderable icon;

		// Token: 0x040017AF RID: 6063
		public global::UnityEngine.GameObject requiredSkillsHeader;

		// Token: 0x040017B0 RID: 6064
		public UITextSkin detailsText;

		// Token: 0x040017B1 RID: 6065
		public UITextSkin skillNameText;

		// Token: 0x040017B2 RID: 6066
		public UITextSkin learnedText;

		// Token: 0x040017B3 RID: 6067
		public UITextSkin requirementsText;

		// Token: 0x040017B4 RID: 6068
		public UITextSkin requiredSkillsText;

		// Token: 0x040017B5 RID: 6069
		public UITextSkin statBlockText;

		// Token: 0x040017B6 RID: 6070
		public UITextSkin nameBlockText;

		// Token: 0x040017B7 RID: 6071
		public UIThreeColorProperties playerIcon;

		// Token: 0x040017B8 RID: 6072
		public global::XRL.World.GameObject GO;

		// Token: 0x040017B9 RID: 6073
		public UITextSkin spText;

		// Token: 0x040017BA RID: 6074
		private SkillsAndPowersLineData searcher;

		// Token: 0x040017BB RID: 6075
		private List<SkillsAndPowersLineData> lineData = new List<SkillsAndPowersLineData>(80);
	}
}
