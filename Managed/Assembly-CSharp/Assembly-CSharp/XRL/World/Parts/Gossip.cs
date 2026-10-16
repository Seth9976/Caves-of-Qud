using System;
using System.Collections.Generic;
using HistoryKit;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000EC1 RID: 3777
	public static class Gossip
	{
		// Token: 0x06009781 RID: 38785 RVA: 0x00370508 File Offset: 0x0036E708
		public static string GenerateGossip_OneFaction(string faction)
		{
			History sultanHistory = The.Game.sultanHistory;
			if (50.in100())
			{
				return Gossip.GenerateGossip_TwoFactions(faction, HistoricStringExpander.ExpandString("some <spice.commonPhrases.group.!random>", null, sultanHistory, null, null));
			}
			return Gossip.GenerateGossip_TwoFactions(HistoricStringExpander.ExpandString("<spice.commonPhrases.someone.!random>", null, sultanHistory, null, null), faction);
		}

		// Token: 0x06009782 RID: 38786 RVA: 0x00370554 File Offset: 0x0036E754
		public static string GenerateGossip_TwoFactions(string actor, string actee)
		{
			History sultanHistory = The.Game.sultanHistory;
			string text = HistoricStringExpander.ExpandString("<spice.gossip.twoFaction.!random>", null, sultanHistory, null, null);
			if (text.Contains("@item"))
			{
				GameObject gameObject = GameObject.Create(EncountersAPI.GetARandomDescendantOf("Item"));
				text = text.Replace("@item.a", gameObject.a).Replace("@item.name", gameObject.DisplayNameOnlyStripped);
			}
			List<string> factionNames = Factions.GetFactionNames();
			if (factionNames.Contains(actor))
			{
				if (40.in100())
				{
					List<GameObjectBlueprint> factionMembers = GameObjectFactory.Factory.GetFactionMembers(actor, false, true);
					if (factionMembers.Count > 0)
					{
						actor = GameObject.Create(factionMembers.GetRandomElement<GameObjectBlueprint>(null).Name).an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false);
					}
					else
					{
						actor = ((!Faction.GetFormattedName(actor).IsNullOrEmpty()) ? Faction.GetFormattedName(actor) : actor);
					}
				}
				else
				{
					string formattedName = Faction.GetFormattedName(actor);
					actor = ((!formattedName.IsNullOrEmpty()) ? formattedName : actor);
				}
			}
			if (factionNames.Contains(actee))
			{
				if (40.in100())
				{
					List<GameObjectBlueprint> factionMembers2 = GameObjectFactory.Factory.GetFactionMembers(actee, false, true);
					if (factionMembers2.Count > 0)
					{
						actee = GameObject.Create(factionMembers2.GetRandomElement<GameObjectBlueprint>(null).Name).an(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, true, false, null, false, null, false);
					}
					else
					{
						actee = ((!Faction.GetFormattedName(actee).IsNullOrEmpty()) ? Faction.GetFormattedName(actee) : actee);
					}
				}
				else
				{
					actee = ((!Faction.GetFormattedName(actee).IsNullOrEmpty()) ? Faction.GetFormattedName(actee) : actee);
				}
			}
			return text.Replace("*f1*", actor).Replace("*f2*", actee);
		}
	}
}
