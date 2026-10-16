using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E1C RID: 3612
	[Serializable]
	public class VillageHistoryBook : IPart
	{
		// Token: 0x0600931C RID: 37660 RVA: 0x003503E6 File Offset: 0x0034E5E6
		public VillageHistoryBook()
		{
		}

		// Token: 0x0600931D RID: 37661 RVA: 0x003503F9 File Offset: 0x0034E5F9
		public VillageHistoryBook(string Title, string Text, string Events)
		{
			this.SetContents(Title, Text, Events);
		}

		// Token: 0x0600931E RID: 37662 RVA: 0x00350415 File Offset: 0x0034E615
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600931F RID: 37663 RVA: 0x00350418 File Offset: 0x0034E618
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009320 RID: 37664 RVA: 0x0035043E File Offset: 0x0034E63E
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return (E.Actor != The.Player || !this.GetHasBeenRead()) && base.HandleEvent(E);
		}

		// Token: 0x06009321 RID: 37665 RVA: 0x00350460 File Offset: 0x0034E660
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, this.GetHasBeenRead() ? 15 : 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009322 RID: 37666 RVA: 0x003504A4 File Offset: 0x0034E6A4
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read" && E.Actor.IsPlayer())
			{
				BookUI.ShowBook(this.Text, this.Title, "Sounds/Interact/sfx_interact_book_read", null, null);
				foreach (string text in this.Events.Split(',', StringSplitOptions.None))
				{
					if (!text.IsNullOrEmpty())
					{
						JournalAPI.RevealVillageNote(text, null);
					}
				}
				AfterReadBookEvent.Send(E.Actor, this.ParentObject, this, E);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009323 RID: 37667 RVA: 0x00350533 File Offset: 0x0034E733
		public void SetContents(string Title, string Text, string Events)
		{
			this.Title = Title;
			this.Text = Text;
			this.Events = Events;
		}

		// Token: 0x06009324 RID: 37668 RVA: 0x0035054C File Offset: 0x0034E74C
		public bool GetHasBeenRead()
		{
			foreach (string text in this.Events.Split(',', StringSplitOptions.None))
			{
				if (!string.IsNullOrEmpty(text) && !JournalAPI.IsMapOrVillageNoteRevealed(text))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003CDB RID: 15579
		public string Title;

		// Token: 0x04003CDC RID: 15580
		public string Text;

		// Token: 0x04003CDD RID: 15581
		public string Events = "";
	}
}
