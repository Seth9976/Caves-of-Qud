using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000EDA RID: 3802
	[Serializable]
	public class Memorial : IPart
	{
		// Token: 0x06009828 RID: 38952 RVA: 0x003743A8 File Offset: 0x003725A8
		public override bool SameAs(IPart Part)
		{
			Memorial memorial = Part as Memorial;
			return !(memorial.Eulogies != this.Eulogies) && !(memorial.UseDefaultEulogyIfPrimaryFaction != this.UseDefaultEulogyIfPrimaryFaction) && memorial.OnlyIfNamed == this.OnlyIfNamed && memorial.GenerateNameIfNotNamed == this.GenerateNameIfNotNamed && base.SameAs(Part);
		}

		// Token: 0x06009829 RID: 38953 RVA: 0x0037440D File Offset: 0x0037260D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == OnDeathRemovalEvent.ID;
		}

		// Token: 0x0600982A RID: 38954 RVA: 0x00374424 File Offset: 0x00372624
		public override bool HandleEvent(OnDeathRemovalEvent E)
		{
			if (!this.OnlyIfNamed || this.ParentObject.HasProperName)
			{
				string text = this.ParentObject.GetPrimaryFaction(false);
				if (!this.Eulogies.IsNullOrEmpty())
				{
					foreach (KeyValuePair<string, string> keyValuePair in this.Eulogies.CachedDictionaryExpansion())
					{
						Faction ifExists = Factions.GetIfExists(keyValuePair.Key);
						if (ifExists != null)
						{
							if (ifExists.QueueMemorial(this.ParentObject, this.GenerateNameIfNotNamed, keyValuePair.Value, E.Reason, E.ThirdPersonReason) != null && ifExists.Name == text)
							{
								text = null;
							}
						}
						else
						{
							MetricsManager.LogError("Memorial had nonexistent faction " + keyValuePair.Key);
						}
					}
				}
				if (!text.IsNullOrEmpty() && this.UseDefaultEulogyIfPrimaryFaction == text)
				{
					Factions.GetIfExists(text).QueueMemorial(this.ParentObject, this.GenerateNameIfNotNamed, null, E.Reason, E.ThirdPersonReason);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600982B RID: 38955 RVA: 0x0037454C File Offset: 0x0037274C
		public void AddEulogy(string Faction, string Eulogy)
		{
			if (Faction.IsNullOrEmpty())
			{
				MetricsManager.LogError("faction for Memorial.AddEulogy() was null/empty");
				return;
			}
			if (Factions.GetIfExists(Faction) == null)
			{
				MetricsManager.LogError("no such faction: " + Faction);
				return;
			}
			if (Eulogy.Contains(";;"))
			{
				MetricsManager.LogError("cannot track eulogy containing double semicolon, replacing with emdash");
				Eulogy = Eulogy.Replace(";;", "--");
			}
			if (Eulogy.Contains("::"))
			{
				MetricsManager.LogError("cannot track eulogy containing double colon, replacing with ellipsis");
				Eulogy = Eulogy.Replace("::", "...");
			}
			if (this.Eulogies.IsNullOrEmpty())
			{
				this.Eulogies = Faction + "::" + Eulogy;
				return;
			}
			Dictionary<string, string> dictionary = this.Eulogies.CachedDictionaryExpansion();
			string text;
			if (dictionary.TryGetValue(Faction, out text))
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(dictionary);
				dictionary2[Faction] = Eulogy;
				this.Eulogies = dictionary2.ToStringForCachedDictionaryExpansion();
				return;
			}
			this.Eulogies = string.Concat(new string[] { this.Eulogies, ";;", Faction, "::", Eulogy });
		}

		// Token: 0x04003F5F RID: 16223
		public string Eulogies;

		// Token: 0x04003F60 RID: 16224
		public string UseDefaultEulogyIfPrimaryFaction;

		// Token: 0x04003F61 RID: 16225
		public bool OnlyIfNamed;

		// Token: 0x04003F62 RID: 16226
		public bool GenerateNameIfNotNamed;
	}
}
