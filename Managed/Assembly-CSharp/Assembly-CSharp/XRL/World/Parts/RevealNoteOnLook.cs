using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001214 RID: 4628
	[Serializable]
	public class RevealNoteOnLook : IPart
	{
		// Token: 0x0600B7F3 RID: 47091 RVA: 0x0042EACA File Offset: 0x0042CCCA
		public RevealNoteOnLook()
		{
		}

		// Token: 0x0600B7F4 RID: 47092 RVA: 0x0042EAD2 File Offset: 0x0042CCD2
		public RevealNoteOnLook(string NoteID)
			: this()
		{
			this.NoteID = NoteID;
		}

		// Token: 0x0600B7F5 RID: 47093 RVA: 0x0042EAE4 File Offset: 0x0042CCE4
		public override bool SameAs(IPart Part)
		{
			RevealNoteOnLook revealNoteOnLook = Part as RevealNoteOnLook;
			return revealNoteOnLook != null && revealNoteOnLook.NoteID == this.NoteID;
		}

		// Token: 0x0600B7F6 RID: 47094 RVA: 0x0042EB0E File Offset: 0x0042CD0E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AutoexploreObjectEvent.ID;
		}

		// Token: 0x0600B7F7 RID: 47095 RVA: 0x0042EB24 File Offset: 0x0042CD24
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command != "Look" && !this.LookedAt && this.HasUnrevealedSecret())
			{
				E.Command = "Look";
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B7F8 RID: 47096 RVA: 0x0042EB62 File Offset: 0x0042CD62
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
		}

		// Token: 0x0600B7F9 RID: 47097 RVA: 0x0042EB6F File Offset: 0x0042CD6F
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AfterLookedAt" && !this.LookedAt && JournalAPI.TryRevealNote(this.NoteID, null, false))
			{
				this.LookedAt = true;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B7FA RID: 47098 RVA: 0x0042EBA8 File Offset: 0x0042CDA8
		public bool HasUnrevealedSecret()
		{
			return this.NoteID != null && JournalAPI.HasUnrevealedNote(this.NoteID);
		}

		// Token: 0x04004A05 RID: 18949
		public string NoteID;

		// Token: 0x04004A06 RID: 18950
		public bool LookedAt;
	}
}
