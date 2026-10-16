using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001223 RID: 4643
	[Serializable]
	public class SecretObjectLook : IPart
	{
		// Token: 0x0600B850 RID: 47184 RVA: 0x00430F1F File Offset: 0x0042F11F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B851 RID: 47185 RVA: 0x00430F22 File Offset: 0x0042F122
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterLookedAt");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B852 RID: 47186 RVA: 0x00430F38 File Offset: 0x0042F138
		public override bool FireEvent(Event E)
		{
			if (!this.revealed && E.ID == "AfterLookedAt")
			{
				this.revealed = true;
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.id);
				if (mapNote != null)
				{
					if (mapNote.Revealed)
					{
						return true;
					}
					JournalAPI.RevealMapNote(mapNote, false, null);
				}
				else
				{
					Popup.Show(this.message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.AddMapNote(this.ParentObject.GetCurrentCell().ParentZone.ZoneID, this.text, this.category, this.adjectives.Split(',', StringSplitOptions.None), this.id, true, false, -1L, false);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004A32 RID: 18994
		public string id = "";

		// Token: 0x04004A33 RID: 18995
		public string text;

		// Token: 0x04004A34 RID: 18996
		public string message;

		// Token: 0x04004A35 RID: 18997
		public string adjectives;

		// Token: 0x04004A36 RID: 18998
		public string category;

		// Token: 0x04004A37 RID: 18999
		public bool revealed;
	}
}
