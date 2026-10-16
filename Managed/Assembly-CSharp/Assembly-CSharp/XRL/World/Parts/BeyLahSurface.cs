using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x0200124B RID: 4683
	[Serializable]
	public class BeyLahSurface : IPart
	{
		// Token: 0x0600BA24 RID: 47652 RVA: 0x0043F951 File Offset: 0x0043DB51
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BA25 RID: 47653 RVA: 0x0043F954 File Offset: 0x0043DB54
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BA26 RID: 47654 RVA: 0x0043F96C File Offset: 0x0043DB6C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell" && !The.Game.HasIntGameState(this.RevealKey))
			{
				The.Game.SetIntGameState(this.RevealKey, 1);
				ZoneManager.instance.GetZone("JoppaWorld").BroadcastEvent("BeyLahReveal");
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.RevealSecret);
				if (mapNote != null && !mapNote.Revealed)
				{
					JournalAPI.AddAccomplishment("You discovered the hidden village of Bey Lah.", "<spice.commonPhrases.intrepid.!random.capitalize> =name= discovered Bey Lah, once thought lost to the sands of time.", "<spice.commonPhrases.allThroughout.!random.capitalize> =year=, =name= <spice.commonPhrases.ravaged.!random> the flower fields and brought turmoil to the troubled village of Bey Lah. " + The.Player.GetPronounProvider(false).CapitalizedSubjective + " became known as the Hindren <spice.commonPhrases.scourge.!random.capitalize>.", null, "general", MuralCategory.VisitsLocation, MuralWeight.Medium, null, -1L, true);
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004ACE RID: 19150
		public string RegionName = "Bey Lah";

		// Token: 0x04004ACF RID: 19151
		public string RevealString;

		// Token: 0x04004AD0 RID: 19152
		public string RevealSecret = "$beylah";

		// Token: 0x04004AD1 RID: 19153
		public string RevealKey = "BeyLah_LocationKnown";
	}
}
