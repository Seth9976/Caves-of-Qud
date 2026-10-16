using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x0200106C RID: 4204
	[Serializable]
	public class MultiNavigationBonus : IPoweredPart
	{
		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600A76D RID: 42861 RVA: 0x003D2922 File Offset: 0x003D0B22
		// (set) Token: 0x0600A76E RID: 42862 RVA: 0x003D292A File Offset: 0x003D0B2A
		public string Bonuses
		{
			get
			{
				return this._Bonuses;
			}
			set
			{
				this._Bonuses = value;
				this._TravelClasses = null;
				this._TravelClassBonuses = null;
				this._TravelClassApplicationKeys = null;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600A76F RID: 42863 RVA: 0x003D2948 File Offset: 0x003D0B48
		public List<string> TravelClasses
		{
			get
			{
				if (this._TravelClasses == null)
				{
					this.ParseBonuses();
				}
				return this._TravelClasses;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600A770 RID: 42864 RVA: 0x003D295E File Offset: 0x003D0B5E
		public Dictionary<string, string> TravelClassBonuses
		{
			get
			{
				if (this._TravelClassBonuses == null)
				{
					this.ParseBonuses();
				}
				return this._TravelClassBonuses;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x0600A771 RID: 42865 RVA: 0x003D2974 File Offset: 0x003D0B74
		public Dictionary<string, string> TravelClassApplicationKeys
		{
			get
			{
				if (this._TravelClassApplicationKeys == null)
				{
					this.ParseBonuses();
				}
				return this._TravelClassApplicationKeys;
			}
		}

		// Token: 0x0600A772 RID: 42866 RVA: 0x003D298A File Offset: 0x003D0B8A
		public MultiNavigationBonus()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A773 RID: 42867 RVA: 0x003D29A0 File Offset: 0x003D0BA0
		public void ParseBonuses()
		{
			if (this.Bonuses == null)
			{
				Debug.LogError("no bonuses defined");
				return;
			}
			this._TravelClasses = new List<string>();
			this._TravelClassBonuses = new Dictionary<string, string>();
			this._TravelClassApplicationKeys = new Dictionary<string, string>();
			foreach (string text in this.Bonuses.Split(',', StringSplitOptions.None))
			{
				string[] array2 = text.Split(':', StringSplitOptions.None);
				if (array2.Length < 2 || array2.Length > 3)
				{
					Debug.LogError("Invalid bonus subpart specification, " + text);
				}
				else if (this._TravelClasses.Contains(array2[0]))
				{
					Debug.LogError("Bonuses contain multiple specifications for " + array2[0]);
				}
				else if (this._TravelClasses.Contains("*"))
				{
					Debug.LogError("Bonuses contain specifications after a * specification");
				}
				else
				{
					this._TravelClasses.Add(array2[0]);
					this._TravelClassBonuses.Add(array2[0], array2[1]);
					if (array2.Length >= 3)
					{
						this._TravelClassApplicationKeys.Add(array2[0], array2[2]);
					}
				}
			}
		}

		// Token: 0x0600A774 RID: 42868 RVA: 0x003D2AA8 File Offset: 0x003D0CA8
		public override bool SameAs(IPart p)
		{
			MultiNavigationBonus multiNavigationBonus = p as MultiNavigationBonus;
			return !(multiNavigationBonus._Bonuses != this._Bonuses) && !(multiNavigationBonus.DefaultApplicationKey != this.DefaultApplicationKey) && multiNavigationBonus.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A775 RID: 42869 RVA: 0x003D2AFD File Offset: 0x003D0CFD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetLostChanceEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x0600A776 RID: 42870 RVA: 0x003D2B24 File Offset: 0x003D0D24
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (base.WasReady() && this.IsObjectActivePartSubject(E.Actor))
			{
				string text = null;
				string text2 = null;
				if (!string.IsNullOrEmpty(E.TravelClass) && this.TravelClasses.Contains(E.TravelClass))
				{
					text = this.TravelClassBonuses[E.TravelClass];
					this.TravelClassApplicationKeys.TryGetValue(E.TravelClass, out text2);
				}
				else if (this.TravelClasses.Contains("*"))
				{
					text = this.TravelClassBonuses["*"];
					this.TravelClassApplicationKeys.TryGetValue("*", out text2);
				}
				if (!string.IsNullOrEmpty(text))
				{
					int num = text.RollCached();
					if (num != 0)
					{
						int applied = E.GetApplied(text2);
						int num2 = num;
						if (applied != 0)
						{
							if (num > 0 && applied > 0)
							{
								num = Math.Max(num - applied, 0);
								num2 = applied + num;
							}
							else if (num < 0 && applied < 0)
							{
								num = Math.Min(num - applied, 0);
								num2 = applied + num;
							}
							else if (Math.Abs(num) > Math.Abs(applied))
							{
								num2 = num;
								num -= applied;
							}
							else
							{
								num = 0;
								num2 = applied;
							}
						}
						if (num != 0)
						{
							E.PercentageBonus += num;
							E.SetApplied(text2, num2);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A777 RID: 42871 RVA: 0x003D2C64 File Offset: 0x003D0E64
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				foreach (string text in this.TravelClasses)
				{
					string text2 = this.TravelClassBonuses[text];
					int num = Stat.RollMin(text2);
					int num2 = Stat.RollMax(text2);
					if (num != 0 || num2 != 0)
					{
						E.Postfix.Append("\n{{rules|Chance of becoming lost ");
						if (!string.IsNullOrEmpty(text))
						{
							if (text == "*")
							{
								if (this.TravelClasses.Count > 1)
								{
									E.Postfix.Append("in other terrain ");
								}
							}
							else
							{
								Type type = ModManager.ResolveType("XRL.World.Parts.Skill." + text, false, false, true);
								BaseTerrainSurvivalSkill baseTerrainSurvivalSkill = ((type != null) ? (Activator.CreateInstance(type) as BaseTerrainSurvivalSkill) : null);
								if (baseTerrainSurvivalSkill == null)
								{
									E.Postfix.Append("in certain terrain ");
								}
								else
								{
									E.Postfix.Append("in ").Append(baseTerrainSurvivalSkill.GetTerrainName()).Append(' ');
								}
							}
						}
						if (num >= 0 == num2 >= 0)
						{
							E.Postfix.Append((num >= 0) ? "reduced" : "increased").Append(" by ");
							if (num == num2)
							{
								E.Postfix.Append((num > 0) ? num : (-num));
							}
							else if (num <= 0)
							{
								E.Postfix.Append(-num2).Append('-').Append(-num);
							}
							else
							{
								E.Postfix.Append(num).Append('-').Append(num2);
							}
							E.Postfix.Append("%.");
						}
						else
						{
							E.Postfix.Append(" adjusted by ").Append(-num2).Append("% to ");
							if (num < 0)
							{
								E.Postfix.Append('+');
							}
							E.Postfix.Append(-num).Append("%.");
						}
						base.AddStatusSummary(E.Postfix);
						E.Postfix.Append("}}");
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A778 RID: 42872 RVA: 0x003D2EB8 File Offset: 0x003D10B8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A779 RID: 42873 RVA: 0x003D2EBC File Offset: 0x003D10BC
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ChargeUse > 0)
			{
				GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
				if (activePartFirstSubject != null && activePartFirstSubject.OnWorldMap())
				{
					base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
				}
			}
		}

		// Token: 0x0600A77A RID: 42874 RVA: 0x003D2F0A File Offset: 0x003D110A
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0400451C RID: 17692
		public string DefaultApplicationKey;

		// Token: 0x0400451D RID: 17693
		public bool ShowInShortDescription = true;

		// Token: 0x0400451E RID: 17694
		public string _Bonuses;

		// Token: 0x0400451F RID: 17695
		[NonSerialized]
		private List<string> _TravelClasses;

		// Token: 0x04004520 RID: 17696
		[NonSerialized]
		private Dictionary<string, string> _TravelClassBonuses;

		// Token: 0x04004521 RID: 17697
		[NonSerialized]
		private Dictionary<string, string> _TravelClassApplicationKeys;
	}
}
