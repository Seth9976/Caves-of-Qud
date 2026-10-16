using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001294 RID: 4756
	[Serializable]
	public class SultanRegionSurface : IPart
	{
		// Token: 0x0600BC28 RID: 48168 RVA: 0x0044C786 File Offset: 0x0044A986
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC29 RID: 48169 RVA: 0x0044C789 File Offset: 0x0044A989
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC2A RID: 48170 RVA: 0x0044C7A0 File Offset: 0x0044A9A0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (The.Game.HasQuest("Visit " + this.RegionName))
				{
					The.Game.CompleteQuest("Visit " + this.RegionName);
					if (The.Game.GetIntGameState("Visited_" + this.RegionName, 0) != 1)
					{
						JournalAPI.AddAccomplishment("You visited the historic site of " + this.RegionName + ".", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered " + this.region.ToString() + ", once thought lost to the sands of time.", string.Concat(new string[]
						{
							"Acting against the enfranchisement of ",
							Factions.GetMostHatedFormattedName(true),
							", =name= led an army to the historic site of ",
							this.RegionName,
							". =name= <spice.commonPhrases.liberated.!random> its citizens, and in ",
							The.Player.GetPronounProvider(false).PossessiveAdjective,
							" honor they <spice.history.gospels.Celebration.LateSultanate.!random>."
						}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
					}
					The.Game.SetIntGameState("Visited_" + this.RegionName, 1);
				}
				else if (The.Game.GetIntGameState("Visited_" + this.RegionName, 0) != 1)
				{
					int newTier = this.ParentObject.CurrentZone.NewTier;
					GameObject player = The.Player;
					if (player != null)
					{
						int num = 250 * newTier;
						int num2 = -1;
						int num3 = 0;
						int maxValue = int.MaxValue;
						GameObject gameObject = null;
						GameObject gameObject2 = null;
						GameObject gameObject3 = null;
						GameObject gameObject4 = null;
						Zone currentZone = this.ParentObject.GetCurrentZone();
						player.AwardXP(num, num2, num3, maxValue, gameObject, gameObject2, gameObject3, gameObject4, (currentZone != null) ? currentZone.ZoneID : null, null);
					}
					The.Game.SetIntGameState("Visited_" + this.RegionName, 1);
					JournalAPI.AddAccomplishment("You visited the historic site of " + this.RegionName + ".", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered " + this.region.ToString() + ", once thought lost to the sands of time.", string.Concat(new string[]
					{
						"Acting against the enfranchisement of ",
						Factions.GetMostHatedFormattedName(true),
						", =name= led an army to the historic site of ",
						this.RegionName,
						". =name= <spice.commonPhrases.liberated.!random> its citizens, and in ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" honor they <spice.history.gospels.Celebration.LateSultanate.!random>."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
				}
				if (The.Game.HasIntGameState(this.RevealKey))
				{
					return true;
				}
				The.Game.SetIntGameState(this.RevealKey, 1);
				if (this.RevealLocation != null && The.ZoneManager.GetZone("JoppaWorld").GetCell(this.RevealLocation.x, this.RevealLocation.y).FireEvent("SultanReveal") && this.RevealString != null)
				{
					Popup.Show(this.RevealString, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.RevealSecret);
				if (mapNote != null && !mapNote.Revealed)
				{
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
				Zone currentZone2 = this.ParentObject.CurrentZone;
				if (currentZone2 != null)
				{
					currentZone2.GetWorldTerrainObject().SetStringProperty("MinimapColor", "g", false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B65 RID: 19301
		public string RegionName;

		// Token: 0x04004B66 RID: 19302
		public string RevealString;

		// Token: 0x04004B67 RID: 19303
		public string RevealSecret;

		// Token: 0x04004B68 RID: 19304
		public string RevealKey;

		// Token: 0x04004B69 RID: 19305
		public Vector2i RevealLocation;

		// Token: 0x04004B6A RID: 19306
		public int region = -1;
	}
}
