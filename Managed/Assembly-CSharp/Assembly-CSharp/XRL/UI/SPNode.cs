using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Console;
using XRL.World;
using XRL.World.Skills;

namespace XRL.UI
{
	// Token: 0x020014AC RID: 5292
	public class SPNode
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600CFF1 RID: 53233 RVA: 0x004C7ACC File Offset: 0x004C5CCC
		public Renderable UIIcon
		{
			get
			{
				if (this._UIIcon == null)
				{
					if (this.Skill != null)
					{
						this._UIIcon = new Renderable(this.Skill.Tile, " ", "", "&" + this.Skill.Foreground, this.Skill.Detail[0]);
					}
					else if (this.Power != null)
					{
						this._UIIcon = new Renderable(this.Power.Tile, " ", "", "&" + this.Power.Foreground, this.Power.Detail[0]);
					}
				}
				return this._UIIcon;
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600CFF2 RID: 53234 RVA: 0x004C7B88 File Offset: 0x004C5D88
		public IEnumerable<SPNode> powers
		{
			get
			{
				if (this.Skill != null)
				{
					foreach (SPNode spnode in SkillsAndPowersScreen.Nodes.Where<SPNode>((SPNode n) => n.ParentNode == this))
					{
						yield return spnode;
					}
					IEnumerator<SPNode> enumerator = null;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600CFF3 RID: 53235 RVA: 0x004C7B98 File Offset: 0x004C5D98
		// (set) Token: 0x0600CFF4 RID: 53236 RVA: 0x004C7BA0 File Offset: 0x004C5DA0
		public bool Expand
		{
			get
			{
				return this._Expand;
			}
			set
			{
				this._Expand = value;
				if (this.Skill != null && this.Skill.Name != null)
				{
					XRLGame game = The.Game;
					if (game == null)
					{
						return;
					}
					game.SetBooleanGameState("UI_Expand_Skill_" + this.Name, value);
				}
			}
		}

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x0600CFF5 RID: 53237 RVA: 0x004C7BDE File Offset: 0x004C5DDE
		public string Description
		{
			get
			{
				if (this._Description == null)
				{
					if (this.Skill != null)
					{
						this._Description = this.Skill.GetFormattedDescription();
					}
					else
					{
						this._Description = this.Power.GetFormattedDescription();
					}
				}
				return this._Description;
			}
		}

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x0600CFF6 RID: 53238 RVA: 0x004C7C1A File Offset: 0x004C5E1A
		public string Name
		{
			get
			{
				if (this.Skill != null)
				{
					return this.Skill.Name;
				}
				return this.Power.Name;
			}
		}

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x0600CFF7 RID: 53239 RVA: 0x004C7C3C File Offset: 0x004C5E3C
		public string SearchText
		{
			get
			{
				if (this._SearchText == null)
				{
					if (this.Skill != null)
					{
						this._SearchText = this.Skill.Name + " " + this.Skill.Description;
					}
					if (this.Power != null)
					{
						this._SearchText = this.Power.Name + " " + this.Power.Description;
					}
					string searchText = this._SearchText;
					this._SearchText = ((searchText != null) ? searchText.ToLower() : null);
				}
				return this._SearchText;
			}
		}

		// Token: 0x0600CFF8 RID: 53240 RVA: 0x004C7CCC File Offset: 0x004C5ECC
		public SPNode.LearnedStatus IsLearned(GameObject go)
		{
			if (this.Power != null)
			{
				if (!go.HasPart(this.Power.Class))
				{
					return SPNode.LearnedStatus.None;
				}
				return SPNode.LearnedStatus.Learned;
			}
			else
			{
				if (this.Skill == null)
				{
					return SPNode.LearnedStatus.None;
				}
				if (go.HasPart(this.Skill.Class))
				{
					return SPNode.LearnedStatus.Learned;
				}
				if (SkillsAndPowersScreen.HasAnyPower(go, this.Skill))
				{
					return SPNode.LearnedStatus.Partial;
				}
				return SPNode.LearnedStatus.None;
			}
		}

		// Token: 0x0600CFF9 RID: 53241 RVA: 0x004C7D28 File Offset: 0x004C5F28
		public string ModernUIText(GameObject GO)
		{
			SPNode.sb.Length = 0;
			if (this.Skill != null)
			{
				bool flag = SkillsAndPowersScreen.HasAnyPower(GO, this.Skill);
				if (GO.HasPart(this.Skill.Class))
				{
					SPNode.sb.Append("{{W|" + this.Skill.Name + "}}");
				}
				else if (this.Skill.Initiatory)
				{
					SPNode.sb.Append("{{w|" + this.Skill.Name + "}}");
				}
				else if (flag)
				{
					if (this.Skill.Cost <= GO.Stat("SP", 0))
					{
						SPNode.sb.Append(string.Concat(new string[]
						{
							"{{W|",
							this.Skill.Name,
							"}} [{{C|",
							this.Skill.Cost.ToString(),
							"}}sp]"
						}));
					}
					else
					{
						SPNode.sb.Append(string.Concat(new string[]
						{
							"{{W|",
							this.Skill.Name,
							"}} [{{R|",
							this.Skill.Cost.ToString(),
							"}}sp]"
						}));
					}
				}
				else if (this.Skill.Cost <= GO.Stat("SP", 0))
				{
					SPNode.sb.Append(string.Concat(new string[]
					{
						"{{w|",
						this.Skill.Name,
						"}} [{{C|",
						this.Skill.Cost.ToString(),
						"}}sp]"
					}));
				}
				else
				{
					SPNode.sb.Append(string.Concat(new string[]
					{
						"{{w|",
						this.Skill.Name,
						"}} [{{R|",
						this.Skill.Cost.ToString(),
						"}}sp]"
					}));
				}
			}
			else
			{
				SPNode.sb.Append("    ");
				string text = "";
				PowerEntry power = this.Power;
				if (GO.HasPart(power.Class))
				{
					SPNode.sb.Append("{{G|:" + power.Name + "}}");
				}
				else
				{
					if (power.Requires != null)
					{
						foreach (string text2 in power.Requires.CachedCommaExpansion())
						{
							string text3 = text2;
							bool flag2 = false;
							IBaseSkillEntry baseSkillEntry;
							if (SkillFactory.Factory.TryGetFirstEntry(text2, out baseSkillEntry))
							{
								if (power.IsSkillInitiatory)
								{
									int num = power.ParentSkill.PowerList.IndexOf(power);
									if (num > 0 && power.ParentSkill.PowerList[num - 1] == baseSkillEntry)
									{
										continue;
									}
								}
								text3 = baseSkillEntry.Name;
								flag2 = GO.HasSkill(text2);
							}
							else if (MutationFactory.HasMutation(text2))
							{
								text3 = MutationFactory.GetMutationEntryByName(text2).Name;
								flag2 = GO.HasPart(text2);
							}
							if (flag2)
							{
								text = text + ", {{G|" + text3 + "}}";
							}
							else
							{
								text = text + ", {{R|" + text3 + "}}";
							}
						}
					}
					if (power.Exclusion != null)
					{
						foreach (string text4 in power.Exclusion.CachedCommaExpansion())
						{
							string text5 = text4;
							bool flag3 = false;
							IBaseSkillEntry baseSkillEntry2;
							if (SkillFactory.Factory.TryGetFirstEntry(text4, out baseSkillEntry2))
							{
								text5 = baseSkillEntry2.Name;
								flag3 = !GO.HasSkill(text4);
							}
							else if (MutationFactory.HasMutation(text4))
							{
								text5 = MutationFactory.GetMutationEntryByName(text4).Name;
								flag3 = !GO.HasPart(text4);
							}
							if (flag3)
							{
								text = text + ", Ex: {{g|" + text5 + "}}";
							}
							else
							{
								text = text + ", Ex: {{R|" + text5 + "}}";
							}
						}
					}
					SPNode.sb.Append("{{K|:}}");
					SPNode.sb.Append(power.Render(GO));
					SPNode.sb.Append(text);
				}
			}
			return SPNode.sb.ToString();
		}

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x0600CFFA RID: 53242 RVA: 0x004C81B4 File Offset: 0x004C63B4
		public bool Visible
		{
			get
			{
				return this.ParentNode == null || this.ParentNode.Expand;
			}
		}

		// Token: 0x0600CFFB RID: 53243 RVA: 0x004C81CB File Offset: 0x004C63CB
		public SPNode(SkillEntry Skill, PowerEntry Power, bool Expand, SPNode ParentNode)
		{
			this.Skill = Skill;
			this.Power = Power;
			this.Expand = Expand;
			this.ParentNode = ParentNode;
		}

		// Token: 0x0400518D RID: 20877
		public Renderable _UIIcon;

		// Token: 0x0400518E RID: 20878
		public SkillEntry Skill;

		// Token: 0x0400518F RID: 20879
		public PowerEntry Power;

		// Token: 0x04005190 RID: 20880
		public bool _Expand;

		// Token: 0x04005191 RID: 20881
		public SPNode ParentNode;

		// Token: 0x04005192 RID: 20882
		public string _Description;

		// Token: 0x04005193 RID: 20883
		public string _SearchText;

		// Token: 0x04005194 RID: 20884
		private static StringBuilder sb = new StringBuilder(80);

		// Token: 0x02001DC3 RID: 7619
		public enum LearnedStatus
		{
			// Token: 0x040073FD RID: 29693
			None,
			// Token: 0x040073FE RID: 29694
			Partial,
			// Token: 0x040073FF RID: 29695
			Learned
		}
	}
}
