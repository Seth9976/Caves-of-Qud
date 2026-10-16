using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02001222 RID: 4642
	[Serializable]
	public class SecretObject : IPart
	{
		// Token: 0x0600B84C RID: 47180 RVA: 0x00430E29 File Offset: 0x0042F029
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B84D RID: 47181 RVA: 0x00430E2C File Offset: 0x0042F02C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			if (this.id == "")
			{
				this.id = Guid.NewGuid().ToString();
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B84E RID: 47182 RVA: 0x00430E6C File Offset: 0x0042F06C
		public override bool Render(RenderEvent E)
		{
			if (!this.revealed && !The.Player.IsConfused)
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
					IBaseJournalEntry.DisplayMessage(this.message, null);
					JournalAPI.AddMapNote(this.ParentObject.GetCurrentCell().ParentZone.ZoneID, this.text, this.category, this.adjectives.Split(',', StringSplitOptions.None), this.id, true, false, -1L, false);
				}
			}
			return base.Render(E);
		}

		// Token: 0x04004A2C RID: 18988
		public string id = "";

		// Token: 0x04004A2D RID: 18989
		public string text;

		// Token: 0x04004A2E RID: 18990
		public string message;

		// Token: 0x04004A2F RID: 18991
		public string adjectives;

		// Token: 0x04004A30 RID: 18992
		public string category;

		// Token: 0x04004A31 RID: 18993
		public bool revealed;
	}
}
