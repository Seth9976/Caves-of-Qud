using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using XRL.UI;

namespace XRL.World.Skills
{
	// Token: 0x02000BA6 RID: 2982
	[Serializable]
	public class PowerEntry : IBaseSkillEntry
	{
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x060076C0 RID: 30400 RVA: 0x002CB3E4 File Offset: 0x002C95E4
		public List<PowerEntryRequirement> requirements
		{
			get
			{
				this.InitRequirements();
				return this._requirements;
			}
		}

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x060076C1 RID: 30401 RVA: 0x002CB3F2 File Offset: 0x002C95F2
		public bool IsSkillInitiatory
		{
			get
			{
				SkillEntry parentSkill = this.ParentSkill;
				return parentSkill != null && parentSkill.Initiatory;
			}
		}

		// Token: 0x060076C2 RID: 30402 RVA: 0x002CB408 File Offset: 0x002C9608
		public void InitRequirements()
		{
			if (this._requirements == null)
			{
				this._requirements = new List<PowerEntryRequirement>();
				if (!this.Attribute.IsNullOrEmpty() && !this.Minimum.IsNullOrEmpty())
				{
					DelimitedEnumeratorChar delimitedEnumeratorChar = this.Attribute.DelimitedBy('|');
					DelimitedEnumeratorChar delimitedEnumeratorChar2 = this.Minimum.DelimitedBy('|');
					while (delimitedEnumeratorChar.MoveNext() && delimitedEnumeratorChar2.MoveNext())
					{
						PowerEntryRequirement powerEntryRequirement = new PowerEntryRequirement(this);
						DelimitedEnumeratorChar delimitedEnumeratorChar3 = delimitedEnumeratorChar.Current.DelimitedBy(',');
						DelimitedEnumeratorChar delimitedEnumeratorChar4 = delimitedEnumeratorChar2.Current.DelimitedBy(',');
						while (delimitedEnumeratorChar3.MoveNext() && delimitedEnumeratorChar4.MoveNext())
						{
							powerEntryRequirement.Attributes.Add(new string(delimitedEnumeratorChar3.Current));
							powerEntryRequirement.Minimums.Add(int.Parse(delimitedEnumeratorChar4.Current, NumberStyles.Integer, null));
						}
						this._requirements.Add(powerEntryRequirement);
					}
				}
			}
		}

		// Token: 0x060076C3 RID: 30403 RVA: 0x002CB4F4 File Offset: 0x002C96F4
		public override bool MeetsRequirements(GameObject Object, bool ShowPopup = false)
		{
			if (!this.MeetsAttributeMinimum(Object, ShowPopup))
			{
				return false;
			}
			if (!this.Exclusion.IsNullOrEmpty())
			{
				foreach (string text in this.Exclusion.CachedCommaExpansion())
				{
					IBaseSkillEntry baseSkillEntry;
					MutationEntry mutationEntry;
					if (SkillFactory.Factory.TryGetFirstEntry(text, out baseSkillEntry))
					{
						if (Object.HasSkill(text))
						{
							if (ShowPopup)
							{
								Popup.Show("You may not learn this skill if you already have " + baseSkillEntry.Name + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							return false;
						}
					}
					else if (MutationFactory.TryGetMutationEntry(text, out mutationEntry) && Object.HasPart(mutationEntry.Class))
					{
						if (ShowPopup)
						{
							Popup.Show("You may not learn this skill if you have " + mutationEntry.GetDisplayName(false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						return true;
					}
				}
			}
			if (!this.Requires.IsNullOrEmpty())
			{
				foreach (string text2 in this.Requires.CachedCommaExpansion())
				{
					IBaseSkillEntry baseSkillEntry2;
					MutationEntry mutationEntry2;
					if (SkillFactory.Factory.TryGetFirstEntry(text2, out baseSkillEntry2))
					{
						if (!Object.HasSkill(text2))
						{
							if (ShowPopup)
							{
								Popup.Show("You may not learn this skill until you have " + baseSkillEntry2.Name + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							return false;
						}
					}
					else if (MutationFactory.TryGetMutationEntry(text2, out mutationEntry2) && !Object.HasPart(mutationEntry2.Class))
					{
						if (ShowPopup)
						{
							Popup.Show("You may not learn this skill until you have " + mutationEntry2.GetDisplayName(false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						return false;
					}
				}
			}
			return base.Generic.MeetsRequirements(Object, ShowPopup);
		}

		// Token: 0x060076C4 RID: 30404 RVA: 0x002CB6F0 File Offset: 0x002C98F0
		public bool MeetsAttributeMinimum(GameObject Object, bool ShowPopup = false)
		{
			if (this.Attribute.IsNullOrEmpty() || this.Minimum.IsNullOrEmpty())
			{
				return true;
			}
			this.InitRequirements();
			for (int i = this.requirements.Count - 1; i >= 0; i--)
			{
				if (this._requirements[i].MeetsRequirement(Object, ShowPopup && i == 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060076C5 RID: 30405 RVA: 0x002CB758 File Offset: 0x002C9958
		public string Render(GameObject GO)
		{
			if (PowerEntry.SB == null)
			{
				PowerEntry.SB = new StringBuilder();
			}
			else
			{
				PowerEntry.SB.Clear();
			}
			if (this.MeetsRequirements(GO, false))
			{
				if (this.IsSkillInitiatory)
				{
					PowerEntry.SB.Append("{{g|" + this.Name + "}}");
				}
				else if (this.Cost <= GO.Stat("SP", 0))
				{
					PowerEntry.SB.Append(string.Concat(new string[]
					{
						"{{g|",
						this.Name,
						"}} [{{C|",
						this.Cost.ToString(),
						"}}sp]"
					}));
				}
				else
				{
					PowerEntry.SB.Append(string.Concat(new string[]
					{
						"{{g|",
						this.Name,
						"}} [{{R|",
						this.Cost.ToString(),
						"}}sp]"
					}));
				}
			}
			else if (this.IsSkillInitiatory)
			{
				PowerEntry.SB.Append("{{K|" + this.Name + "}}");
			}
			else
			{
				PowerEntry.SB.Append(string.Concat(new string[]
				{
					"{{K|",
					this.Name,
					"}} [{{K|",
					this.Cost.ToString(),
					"}}sp]"
				}));
			}
			this.InitRequirements();
			int num = 0;
			foreach (PowerEntryRequirement powerEntryRequirement in this.requirements)
			{
				if (num == 0)
				{
					PowerEntry.SB.Append(' ');
				}
				else
				{
					PowerEntry.SB.Append(" or ");
				}
				num++;
				powerEntryRequirement.Render(GO, PowerEntry.SB);
			}
			return PowerEntry.SB.ToString();
		}

		// Token: 0x060076C6 RID: 30406 RVA: 0x002CB954 File Offset: 0x002C9B54
		public override void HandleXMLNode(XmlDataHelper Reader)
		{
			base.HandleXMLNode(Reader);
			this.Exclusion = Reader.ParseAttribute<string>("Exclusion", this.Exclusion, false, null);
			this.Minimum = Reader.ParseAttribute<string>("Minimum", this.Minimum, false, null);
			this.Requires = Reader.ParseAttribute<string>("Prereq", this.Requires, false, null);
			this.Requires = Reader.ParseAttribute<string>("Requires", this.Requires, false, null);
			this.Tile = Reader.ParseAttribute<string>("Tile", null, false, null);
			this.Foreground = Reader.ParseAttribute<string>("Foreground", "w", false, null);
			this.Detail = Reader.ParseAttribute<string>("Detail", "B", false, null);
			if (!this.Requires.IsNullOrEmpty())
			{
				if (this.Requires.Contains(","))
				{
					string[] array = this.Requires.Split(',', StringSplitOptions.None);
					bool flag = false;
					int i = 0;
					int num = array.Length;
					while (i < num)
					{
						string text = CompatManager.ProcessSkill(array[i]);
						if (text != array[i])
						{
							array[i] = text;
							flag = true;
						}
						i++;
					}
					if (flag)
					{
						this.Requires = string.Join(",", array);
						return;
					}
				}
				else
				{
					CompatManager.ProcessSkill(ref this.Requires);
				}
			}
		}

		// Token: 0x040036A8 RID: 13992
		public string Minimum;

		// Token: 0x040036A9 RID: 13993
		public string Requires;

		// Token: 0x040036AA RID: 13994
		public string Exclusion;

		// Token: 0x040036AB RID: 13995
		public SkillEntry ParentSkill;

		// Token: 0x040036AC RID: 13996
		[NonSerialized]
		private List<PowerEntryRequirement> _requirements;

		// Token: 0x040036AD RID: 13997
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();
	}
}
