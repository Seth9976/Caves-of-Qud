using System;
using Qud.API;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x0200106F RID: 4207
	[Serializable]
	public class NavigationBonus : IPoweredPart
	{
		// Token: 0x0600A794 RID: 42900 RVA: 0x003D344B File Offset: 0x003D164B
		public NavigationBonus()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A795 RID: 42901 RVA: 0x003D346C File Offset: 0x003D166C
		public override bool SameAs(IPart p)
		{
			NavigationBonus navigationBonus = p as NavigationBonus;
			return !(navigationBonus.PercentBonus != this.PercentBonus) && !(navigationBonus.SpeedPercentBonus != this.SpeedPercentBonus) && !(navigationBonus.EncounterPercentBonus != this.EncounterPercentBonus) && !(navigationBonus.EncounterType != this.EncounterType) && !(navigationBonus.EncounterTypePercentBonus != this.EncounterTypePercentBonus) && !(navigationBonus.SingleApplicationKey != this.SingleApplicationKey) && !(navigationBonus.TravelClass != this.TravelClass) && navigationBonus.ShowInShortDescription == this.ShowInShortDescription && navigationBonus.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600A796 RID: 42902 RVA: 0x003D353A File Offset: 0x003D173A
		public override bool WantTurnTick()
		{
			return this.ChargeUse > 0;
		}

		// Token: 0x0600A797 RID: 42903 RVA: 0x003D3548 File Offset: 0x003D1748
		public override void TurnTick(long TimeTick, int Amount)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			if (activePartFirstSubject != null && activePartFirstSubject.OnWorldMap())
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, true, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A798 RID: 42904 RVA: 0x003D358D File Offset: 0x003D178D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EncounterChanceEvent.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == GetLostChanceEvent.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription) || ID == TravelSpeedEvent.ID;
		}

		// Token: 0x0600A799 RID: 42905 RVA: 0x003D35CC File Offset: 0x003D17CC
		public override bool HandleEvent(EncounterChanceEvent E)
		{
			if (!string.IsNullOrEmpty(this.EncounterPercentBonus) && base.WasReady() && (string.IsNullOrEmpty(this.TravelClass) || string.IsNullOrEmpty(E.TravelClass) || E.TravelClass == this.TravelClass) && this.IsObjectActivePartSubject(E.Actor))
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(this, this.EncounterPercentBonus.RollCached(), this.ComputePowerFactor);
				if (num != 0)
				{
					int applied = E.GetApplied(this.SingleApplicationKey);
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
						E.SetApplied(this.SingleApplicationKey, num2);
					}
				}
			}
			if (!string.IsNullOrEmpty(this.EncounterType) && !string.IsNullOrEmpty(this.EncounterTypePercentBonus) && E.Encounter != null && base.WasReady() && this.IsObjectActivePartSubject(E.Actor))
			{
				EncounterEntry encounter = E.Encounter;
				string text = ((encounter != null) ? encounter.secretID : null);
				IBaseJournalEntry baseJournalEntry = (string.IsNullOrEmpty(text) ? null : JournalAPI.GetMapNote(text));
				if (baseJournalEntry != null && baseJournalEntry.Has(this.EncounterType))
				{
					int num3 = GetAvailableComputePowerEvent.AdjustUp(this, this.EncounterTypePercentBonus.RollCached(), this.ComputePowerFactor);
					if (num3 != 0)
					{
						int applied2 = E.GetApplied(this.SingleApplicationKey);
						int num4 = num3;
						if (applied2 != 0)
						{
							if (num3 > 0 && applied2 > 0)
							{
								num3 = Math.Max(num3 - applied2, 0);
								num4 = applied2 + num3;
							}
							else if (num3 < 0 && applied2 < 0)
							{
								num3 = Math.Min(num3 - applied2, 0);
								num4 = applied2 + num3;
							}
							else if (Math.Abs(num3) > Math.Abs(applied2))
							{
								num4 = num3;
								num3 -= applied2;
							}
							else
							{
								num3 = 0;
								num4 = applied2;
							}
						}
						if (num3 != 0)
						{
							E.PercentageBonus += num3;
							E.SetApplied(this.SingleApplicationKey, num4);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A79A RID: 42906 RVA: 0x003D3814 File Offset: 0x003D1A14
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (!string.IsNullOrEmpty(this.PercentBonus) && base.WasReady() && (string.IsNullOrEmpty(this.TravelClass) || string.IsNullOrEmpty(E.TravelClass) || E.TravelClass == this.TravelClass) && this.IsObjectActivePartSubject(E.Actor))
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(this, this.PercentBonus.RollCached(), this.ComputePowerFactor);
				if (num != 0)
				{
					int applied = E.GetApplied(this.SingleApplicationKey);
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
						if (num > 0)
						{
							E.OverrideDefaultLimit = true;
						}
						E.SetApplied(this.SingleApplicationKey, num2);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A79B RID: 42907 RVA: 0x003D3928 File Offset: 0x003D1B28
		public override bool HandleEvent(TravelSpeedEvent E)
		{
			if (!string.IsNullOrEmpty(this.SpeedPercentBonus) && base.WasReady() && (string.IsNullOrEmpty(this.TravelClass) || string.IsNullOrEmpty(E.TravelClass) || E.TravelClass == this.TravelClass) && this.IsObjectActivePartSubject(E.Actor))
			{
				int num = GetAvailableComputePowerEvent.AdjustUp(this, this.SpeedPercentBonus.RollCached(), this.ComputePowerFactor);
				if (num != 0)
				{
					int applied = E.GetApplied(this.SingleApplicationKey);
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
						E.SetApplied(this.SingleApplicationKey, num2);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A79C RID: 42908 RVA: 0x003D3A30 File Offset: 0x003D1C30
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				if (!string.IsNullOrEmpty(this.PercentBonus))
				{
					int num = Stat.RollMin(this.PercentBonus);
					int num2 = Stat.RollMax(this.PercentBonus);
					if (num != 0 || num2 != 0)
					{
						E.Postfix.Append("\n{{rules|Chance of becoming lost ");
						if (!string.IsNullOrEmpty(this.TravelClass))
						{
							Type type = ModManager.ResolveType("XRL.World.Parts.Skill." + this.TravelClass, false, false, true);
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
				if (!string.IsNullOrEmpty(this.EncounterPercentBonus))
				{
					int num3 = Stat.RollMin(this.EncounterPercentBonus);
					int num4 = Stat.RollMax(this.EncounterPercentBonus);
					if (num3 != 0 || num4 != 0)
					{
						E.Postfix.Append("\n{{rules|Chance of interesting encounters while traveling ");
						if (!string.IsNullOrEmpty(this.TravelClass))
						{
							Type type2 = ModManager.ResolveType("XRL.World.Parts.Skill." + this.TravelClass, false, false, true);
							BaseTerrainSurvivalSkill baseTerrainSurvivalSkill2 = ((type2 != null) ? (Activator.CreateInstance(type2) as BaseTerrainSurvivalSkill) : null);
							if (baseTerrainSurvivalSkill2 == null)
							{
								E.Postfix.Append("through certain terrain on the world map");
							}
							else
							{
								E.Postfix.Append("through ").Append(baseTerrainSurvivalSkill2.GetTerrainName()).Append(" on the world map ");
							}
						}
						if (num3 >= 0 == num4 >= 0)
						{
							E.Postfix.Append((num3 >= 0) ? "increased" : "reduced").Append(" by ");
							if (num3 == num4)
							{
								E.Postfix.Append((num3 > 0) ? num3 : (-num3));
							}
							else if (num3 <= 0)
							{
								E.Postfix.Append(-num3).Append('-').Append(num4);
							}
							else
							{
								E.Postfix.Append(-num4).Append('-').Append(num3);
							}
							E.Postfix.Append("%.");
						}
						else
						{
							E.Postfix.Append(" adjusted by ").Append(num3).Append("% to ");
							if (num3 < 0)
							{
								E.Postfix.Append('+');
							}
							E.Postfix.Append(num4).Append("%.");
						}
						base.AddStatusSummary(E.Postfix);
						E.Postfix.Append("}}");
					}
				}
				if (!string.IsNullOrEmpty(this.EncounterType) && !string.IsNullOrEmpty(this.EncounterTypePercentBonus))
				{
					int num5 = Stat.RollMin(this.EncounterTypePercentBonus);
					int num6 = Stat.RollMax(this.EncounterTypePercentBonus);
					if (num5 != 0 || num6 != 0)
					{
						E.Postfix.Append("\n{{rules|Chance of encountering ").Append(this.EncounterType).Append(" while traveling ");
						if (!string.IsNullOrEmpty(this.TravelClass))
						{
							E.Postfix.Append("through other terrain on the world map ");
						}
						else
						{
							E.Postfix.Append("on the world map ");
						}
						if (num5 >= 0 == num6 >= 0)
						{
							E.Postfix.Append((num5 >= 0) ? "increased" : "reduced").Append(" by ");
							if (num5 == num6)
							{
								E.Postfix.Append((num5 > 0) ? num5 : (-num5));
							}
							else if (num5 <= 0)
							{
								E.Postfix.Append(-num5).Append('-').Append(num6);
							}
							else
							{
								E.Postfix.Append(-num6).Append('-').Append(num5);
							}
							E.Postfix.Append("%.");
						}
						else
						{
							E.Postfix.Append(" adjusted by ").Append(num5).Append("% to ");
							if (num5 < 0)
							{
								E.Postfix.Append('+');
							}
							E.Postfix.Append(num6).Append("%.");
						}
						base.AddStatusSummary(E.Postfix);
						E.Postfix.Append("}}");
					}
				}
				if (!string.IsNullOrEmpty(this.SpeedPercentBonus))
				{
					int num7 = Stat.RollMin(this.SpeedPercentBonus);
					int num8 = Stat.RollMax(this.SpeedPercentBonus);
					if (num7 != 0 || num8 != 0)
					{
						E.Postfix.Append("\n{{rules|Speed while traveling on the world map ");
						if (!string.IsNullOrEmpty(this.TravelClass))
						{
							Type type3 = ModManager.ResolveType("XRL.World.Parts.Skill." + this.TravelClass, false, false, true);
							BaseTerrainSurvivalSkill baseTerrainSurvivalSkill3 = ((type3 != null) ? (Activator.CreateInstance(type3) as BaseTerrainSurvivalSkill) : null);
							if (baseTerrainSurvivalSkill3 == null)
							{
								E.Postfix.Append("in certain terrain ");
							}
							else
							{
								E.Postfix.Append("in ").Append(baseTerrainSurvivalSkill3.GetTerrainName()).Append(' ');
							}
						}
						if (num7 >= 0 == num8 >= 0)
						{
							E.Postfix.Append((num7 >= 0) ? "increased" : "reduced").Append(" by ");
							if (num7 == num8)
							{
								E.Postfix.Append((num7 > 0) ? num7 : (-num7));
							}
							else if (num7 <= 0)
							{
								E.Postfix.Append(-num7).Append('-').Append(num8);
							}
							else
							{
								E.Postfix.Append(-num8).Append('-').Append(num7);
							}
							E.Postfix.Append("%.");
						}
						else
						{
							E.Postfix.Append(" adjusted by ").Append(num7).Append("% to ");
							if (num7 < 0)
							{
								E.Postfix.Append('+');
							}
							E.Postfix.Append(num8).Append("%.");
						}
						base.AddStatusSummary(E.Postfix);
						E.Postfix.Append("}}");
					}
				}
				if (this.ComputePowerFactor > 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
				}
				else if (this.ComputePowerFactor < 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice decreases this item's effectiveness.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A79D RID: 42909 RVA: 0x003D41C8 File Offset: 0x003D23C8
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "PercentBonus", this.PercentBonus);
			E.AddEntry(this, "SpeedPercentBonus", this.SpeedPercentBonus);
			E.AddEntry(this, "EncounterPercentBonus", this.EncounterPercentBonus);
			E.AddEntry(this, "EncounterType", this.EncounterType);
			E.AddEntry(this, "EncounterTypePercentBonus", this.EncounterTypePercentBonus);
			E.AddEntry(this, "SingleApplicationKey", this.SingleApplicationKey);
			E.AddEntry(this, "TravelClass", this.TravelClass);
			E.AddEntry(this, "ShowInShortDescription", this.ShowInShortDescription);
			E.AddEntry(this, "ComputePowerFactor", this.ComputePowerFactor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A79E RID: 42910 RVA: 0x003D427E File Offset: 0x003D247E
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0400452B RID: 17707
		public string PercentBonus = "10";

		// Token: 0x0400452C RID: 17708
		public string SpeedPercentBonus;

		// Token: 0x0400452D RID: 17709
		public string EncounterPercentBonus;

		// Token: 0x0400452E RID: 17710
		public string EncounterType;

		// Token: 0x0400452F RID: 17711
		public string EncounterTypePercentBonus;

		// Token: 0x04004530 RID: 17712
		public string SingleApplicationKey;

		// Token: 0x04004531 RID: 17713
		public string TravelClass;

		// Token: 0x04004532 RID: 17714
		public bool ShowInShortDescription = true;

		// Token: 0x04004533 RID: 17715
		public float ComputePowerFactor;
	}
}
