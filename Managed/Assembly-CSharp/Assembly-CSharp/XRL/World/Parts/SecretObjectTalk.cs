using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001224 RID: 4644
	[Serializable]
	public class SecretObjectTalk : IPart
	{
		// Token: 0x0600B854 RID: 47188 RVA: 0x00430FFB File Offset: 0x0042F1FB
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B855 RID: 47189 RVA: 0x00430FFE File Offset: 0x0042F1FE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterConversationEvent.ID;
		}

		// Token: 0x0600B856 RID: 47190 RVA: 0x00431014 File Offset: 0x0042F214
		public override bool HandleEvent(AfterConversationEvent E)
		{
			if (!this.revealed && E.SpeakingWith == this.ParentObject && E.Actor.IsPlayer())
			{
				this.revealed = true;
				if (!string.IsNullOrEmpty(this.achieve))
				{
					AchievementManager.SetAchievement(this.achieve);
				}
				JournalMapNote mapNote = JournalAPI.GetMapNote(this.id);
				if (mapNote != null)
				{
					if (!mapNote.Revealed)
					{
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

		// Token: 0x04004A38 RID: 19000
		public string id = "";

		// Token: 0x04004A39 RID: 19001
		public string text;

		// Token: 0x04004A3A RID: 19002
		public string message;

		// Token: 0x04004A3B RID: 19003
		public string adjectives;

		// Token: 0x04004A3C RID: 19004
		public string category;

		// Token: 0x04004A3D RID: 19005
		public string achieve = "";

		// Token: 0x04004A3E RID: 19006
		public bool revealed;
	}
}
