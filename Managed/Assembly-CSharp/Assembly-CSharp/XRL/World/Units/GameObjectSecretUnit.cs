using System;
using System.Collections.Generic;
using Qud.API;

namespace XRL.World.Units
{
	// Token: 0x020009B4 RID: 2484
	[Serializable]
	public class GameObjectSecretUnit : GameObjectUnit
	{
		// Token: 0x06006B01 RID: 27393 RVA: 0x00227490 File Offset: 0x00225690
		public override void Apply(GameObject Object)
		{
			List<IBaseJournalEntry> unrevealedNotes = JournalAPI.GetUnrevealedNotes(null);
			for (int i = 0; i < this.Amount; i++)
			{
				IBaseJournalEntry baseJournalEntry = unrevealedNotes.RemoveRandomElement<IBaseJournalEntry>(null);
				if (baseJournalEntry != null)
				{
					baseJournalEntry.Reveal(null, false);
				}
			}
		}

		// Token: 0x06006B02 RID: 27394 RVA: 0x002274C9 File Offset: 0x002256C9
		public override void Reset()
		{
			base.Reset();
			this.Amount = 0;
		}

		// Token: 0x06006B03 RID: 27395 RVA: 0x002274D8 File Offset: 0x002256D8
		public override string GetDescription(bool Inscription = false)
		{
			return string.Format("Reveals {0} secrets on creation", this.Amount);
		}

		// Token: 0x04003101 RID: 12545
		public int Amount;
	}
}
