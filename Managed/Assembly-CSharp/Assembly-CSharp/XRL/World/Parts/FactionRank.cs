using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EB4 RID: 3764
	[Serializable]
	public class FactionRank : IPart
	{
		// Token: 0x0600970A RID: 38666 RVA: 0x0036CF0F File Offset: 0x0036B10F
		public override bool SameAs(IPart Part)
		{
			return !((Part as FactionRank).Ranks != this.Ranks) && base.SameAs(Part);
		}

		// Token: 0x0600970B RID: 38667 RVA: 0x0036CF32 File Offset: 0x0036B132
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetFactionRankEvent.ID;
		}

		// Token: 0x0600970C RID: 38668 RVA: 0x0036CF48 File Offset: 0x0036B148
		public override bool HandleEvent(GetFactionRankEvent E)
		{
			string text;
			if (!this.Ranks.IsNullOrEmpty() && GameObject.Validate(E.Object) && !E.Object.IsPlayer() && this.Ranks.CachedDictionaryExpansion().TryGetValue(E.Faction, out text) && (E.Rank == null || Faction.GetRankStanding(E.Faction, text) > Faction.GetRankStanding(E.Faction, E.Rank)))
			{
				E.Rank = text;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600970D RID: 38669 RVA: 0x0036CFCC File Offset: 0x0036B1CC
		public bool PromoteIfBelow(string Faction, string Rank, bool Message = false, bool IgnoreVisibility = false, bool Capitalize = true)
		{
			if (Rank.IsNullOrEmpty())
			{
				MetricsManager.LogError("rank was empty");
				return false;
			}
			if (Rank.Contains("::"))
			{
				MetricsManager.LogError("cannot track rank containing double colon, had " + Rank);
				return false;
			}
			if (Rank.Contains(";;"))
			{
				MetricsManager.LogError("cannot track rank containing double semicolon, had " + Rank);
				return false;
			}
			bool flag = false;
			Faction faction = null;
			if (this.Ranks.IsNullOrEmpty())
			{
				this.Ranks = Faction + "::" + Rank;
				flag = true;
			}
			else
			{
				Dictionary<string, string> dictionary = this.Ranks.CachedDictionaryExpansion();
				string text;
				if (dictionary.TryGetValue(Faction, out text))
				{
					if (text != Rank)
					{
						if (faction == null)
						{
							faction = Factions.Get(Faction);
						}
						if (faction.GetRankStanding(text) < faction.GetRankStanding(Rank))
						{
							Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dictionary);
							dictionary2[Faction] = Rank;
							this.Ranks = dictionary2.ToStringForCachedDictionaryExpansion();
							flag = true;
						}
					}
				}
				else
				{
					this.Ranks = string.Concat(new string[] { this.Ranks, ";;", Faction, "::", Rank });
					flag = true;
				}
			}
			if (flag && Message && (IgnoreVisibility || base.Visible()))
			{
				if (faction == null)
				{
					faction = Factions.Get(Faction);
				}
				base.DidX("are", "promoted to the " + faction.GetRankTerm() + " of " + (Capitalize ? Rank.Capitalize() : Rank), null, null, null, null, null, false, false, null, null, false, false, true, false, false, null);
			}
			return flag;
		}

		// Token: 0x04003EC3 RID: 16067
		public string Ranks;
	}
}
