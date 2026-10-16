using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001225 RID: 4645
	[Serializable]
	public class SecretRevealer : IPart
	{
		// Token: 0x0600B858 RID: 47192 RVA: 0x00431107 File Offset: 0x0042F307
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B859 RID: 47193 RVA: 0x0043110A File Offset: 0x0042F30A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600B85A RID: 47194 RVA: 0x00431120 File Offset: 0x0042F320
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (!this.revealed)
			{
				this.revealed = true;
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.id);
				if (mapNote != null)
				{
					if (!mapNote.Revealed)
					{
						Popup.Show(this.message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						JournalAPI.RevealMapNote(mapNote, false, null);
					}
				}
				else
				{
					Popup.Show(this.message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					JournalAPI.AddMapNote(this.ParentObject.GetCurrentCell().ParentZone.ZoneID, this.text, this.category, this.adjectives.Split(',', StringSplitOptions.None), this.id, true, false, -1L, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004A3F RID: 19007
		public string id;

		// Token: 0x04004A40 RID: 19008
		public string text;

		// Token: 0x04004A41 RID: 19009
		public string message;

		// Token: 0x04004A42 RID: 19010
		public string adjectives;

		// Token: 0x04004A43 RID: 19011
		public string category;

		// Token: 0x04004A44 RID: 19012
		public string extraprepopup;

		// Token: 0x04004A45 RID: 19013
		public bool revealed;
	}
}
