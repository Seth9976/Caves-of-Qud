using System;
using Qud.API;
using XRL.Core;

namespace XRL.World.Parts
{
	// Token: 0x02001271 RID: 4721
	[Serializable]
	public class HydroponSurface : IPart
	{
		// Token: 0x0600BB06 RID: 47878 RVA: 0x0044511E File Offset: 0x0044331E
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB07 RID: 47879 RVA: 0x00445121 File Offset: 0x00443321
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BB08 RID: 47880 RVA: 0x00445138 File Offset: 0x00443338
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !XRLCore.Core.Game.HasIntGameState(this.RevealKey))
			{
				XRLCore.Core.Game.SetIntGameState(this.RevealKey, 1);
				ZoneManager.instance.GetZone("JoppaWorld").BroadcastEvent("HydroponReveal");
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.RevealSecret);
				if (mapNote != null && !mapNote.Revealed)
				{
					JournalAPI.AddAccomplishment("You discovered the Hydropon.", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered the Hydropon.", string.Concat(new string[]
					{
						"Acting against labor laws restricting the rights of newly sentient plant-people, =name= led an army to the gates of the Hydropon. ",
						The.Player.GetPronounProvider(false).CapitalizedSubjective,
						" <spice.commonPhrases.liberated.!random> its citizens, and in ",
						The.Player.GetPronounProvider(false).PossessiveAdjective,
						" honor they <spice.history.gospels.Celebration.LateSultanate.!random>."
					}), null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B15 RID: 19221
		public string RegionName = "Hydropon";

		// Token: 0x04004B16 RID: 19222
		public string RevealString;

		// Token: 0x04004B17 RID: 19223
		public string RevealSecret = "$hydropon";

		// Token: 0x04004B18 RID: 19224
		public string RevealKey = "Hydropon_LocationKnown";
	}
}
