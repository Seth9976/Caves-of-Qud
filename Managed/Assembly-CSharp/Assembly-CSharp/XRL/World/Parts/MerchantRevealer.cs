using System;
using System.Collections.Generic;
using System.Linq;
using HistoryKit;
using Qud.API;
using XRL.Language;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E18 RID: 3608
	[Serializable]
	public class MerchantRevealer : IPart
	{
		// Token: 0x060092F3 RID: 37619 RVA: 0x0034F86F File Offset: 0x0034DA6F
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x060092F4 RID: 37620 RVA: 0x0034F874 File Offset: 0x0034DA74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AddedToInventoryEvent.ID || ID == BeforeObjectCreatedEvent.ID || ID == EnteredCellEvent.ID || ID == EquippedEvent.ID || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060092F5 RID: 37621 RVA: 0x0034F8C5 File Offset: 0x0034DAC5
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			this.CheckMerchantLocation(E);
			return base.HandleEvent(E);
		}

		// Token: 0x060092F6 RID: 37622 RVA: 0x0034F8D5 File Offset: 0x0034DAD5
		public override bool HandleEvent(AddedToInventoryEvent E)
		{
			this.CheckMerchantLocation(null);
			return base.HandleEvent(E);
		}

		// Token: 0x060092F7 RID: 37623 RVA: 0x0034F8E5 File Offset: 0x0034DAE5
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.CheckMerchantLocation(null);
			return base.HandleEvent(E);
		}

		// Token: 0x060092F8 RID: 37624 RVA: 0x0034F8F5 File Offset: 0x0034DAF5
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckMerchantLocation(null);
			return base.HandleEvent(E);
		}

		// Token: 0x060092F9 RID: 37625 RVA: 0x0034F905 File Offset: 0x0034DB05
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return (E.Actor != The.Player || (this.merchantLoc != null && !this.merchantLoc.Revealed)) && base.HandleEvent(E);
		}

		// Token: 0x060092FA RID: 37626 RVA: 0x0034F934 File Offset: 0x0034DB34
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, (this.merchantLoc != null && !this.merchantLoc.Revealed) ? 100 : 15, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060092FB RID: 37627 RVA: 0x0034F988 File Offset: 0x0034DB88
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read" && E.Actor.IsPlayer())
			{
				BookUI.ShowBook(this.bookText, this.bookTitle, "Sounds/Interact/sfx_interact_book_read", null, null);
				AfterReadBookEvent.Send(E.Actor, this.ParentObject, this, E);
				if (this.merchantLoc != null)
				{
					JournalMapNote journalMapNote = JournalAPI.GetMapNotes((JournalMapNote n) => n.SameAs(this.merchantLoc)).FirstOrDefault<JournalMapNote>();
					if (journalMapNote != null)
					{
						this.merchantLoc = journalMapNote;
					}
					else
					{
						JournalAPI.AddMapNote(this.merchantLoc);
					}
					JournalMapNote journalMapNote2 = this.merchantLoc;
					if (journalMapNote2 != null)
					{
						journalMapNote2.Reveal(null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092FC RID: 37628 RVA: 0x0034FA32 File Offset: 0x0034DC32
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060092FD RID: 37629 RVA: 0x0034FA38 File Offset: 0x0034DC38
		private void CheckMerchantLocation(IObjectCreationEvent E = null)
		{
			if (this.merchantLoc == null)
			{
				this.GenerateMerchantLocation();
				if (this.merchantLoc == null)
				{
					GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom("Books", null, null).Blueprint);
					if (E != null)
					{
						E.ReplacementObject = gameObject;
						return;
					}
					this.ParentObject.ReplaceWith(gameObject);
				}
			}
		}

		// Token: 0x060092FE RID: 37630 RVA: 0x0034FA8C File Offset: 0x0034DC8C
		private void GenerateMerchantLocation()
		{
			if (The.Game != null)
			{
				this.merchantLoc = JournalAPI.GetMapNotes((JournalMapNote mapnote) => mapnote.Has("merchant") && mapnote.Has("humanoid")).GetRandomElement<JournalMapNote>(null);
				if (this.merchantLoc != null)
				{
					this.bookTitle = "advertisement for " + Grammar.InitLower(this.merchantLoc.Text);
					this.ParentObject.DisplayName = this.bookTitle;
					Dictionary<string, string> dictionary = new Dictionary<string, string>
					{
						{
							"$workshop",
							"{{|" + Grammar.InitLower(this.merchantLoc.Text) + "}}"
						},
						{
							"$location",
							LoreGenerator.GenerateLandmarkDirectionsTo(this.merchantLoc.ZoneID)
						}
					};
					this.bookText = HistoricStringExpander.ExpandString("<spice.advertisements.merchants.!random>", null, null, dictionary, null);
				}
			}
		}

		// Token: 0x04003CD3 RID: 15571
		public JournalMapNote merchantLoc;

		// Token: 0x04003CD4 RID: 15572
		public string bookTitle;

		// Token: 0x04003CD5 RID: 15573
		public string bookText;
	}
}
