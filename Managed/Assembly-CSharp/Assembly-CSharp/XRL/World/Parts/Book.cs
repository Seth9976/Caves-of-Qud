using System;
using Qud.API;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000E13 RID: 3603
	[Serializable]
	public class Book : IPart
	{
		// Token: 0x060092CD RID: 37581 RVA: 0x0034E2D6 File Offset: 0x0034C4D6
		public override bool SameAs(IPart p)
		{
			return !((p as Book).ID != this.ID) && base.SameAs(p);
		}

		// Token: 0x060092CE RID: 37582 RVA: 0x0034E2F9 File Offset: 0x0034C4F9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x060092CF RID: 37583 RVA: 0x0034E31F File Offset: 0x0034C51F
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return (E.Actor != The.Player || !this.GetHasBeenRead()) && base.HandleEvent(E);
		}

		// Token: 0x060092D0 RID: 37584 RVA: 0x0034E340 File Offset: 0x0034C540
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Read", "read", "Read", null, 'r', false, this.GetHasBeenRead() ? 15 : 100, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x060092D1 RID: 37585 RVA: 0x0034E384 File Offset: 0x0034C584
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read" && E.Actor.IsPlayer())
			{
				BookUI.ShowBookByID(this.ID, "Sounds/Interact/sfx_interact_book_read", null);
				AfterReadBookEvent.Send(E.Actor, this.ParentObject, this, E);
				if (!this.GetHasBeenRead())
				{
					this.SetHasBeenRead(true);
					string referenceDisplayName = this.ParentObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false);
					JournalAPI.AddAccomplishment("You read " + referenceDisplayName + ".", string.Concat(new string[]
					{
						"In the month of ",
						Calendar.GetMonth(),
						" of ",
						Calendar.GetYear().ToString(),
						", =name= penned the influential book, ",
						referenceDisplayName,
						"."
					}), string.Concat(new string[]
					{
						"At a remote library near ",
						JournalAPI.GetLandmarkNearestPlayer().Text,
						", =name= met with a group of blind scribes and together they penned the beloved codex ",
						referenceDisplayName,
						"."
					}), null, "general", MuralCategory.CreatesSomething, MuralWeight.VeryLow, null, -1L, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060092D2 RID: 37586 RVA: 0x0034E4AC File Offset: 0x0034C6AC
		public string GetBookKey()
		{
			return "AlreadyRead_" + this.ID;
		}

		// Token: 0x060092D3 RID: 37587 RVA: 0x0034E4BE File Offset: 0x0034C6BE
		public bool GetHasBeenRead()
		{
			return The.Game.GetStringGameState(this.GetBookKey(), "") == "Yes";
		}

		// Token: 0x060092D4 RID: 37588 RVA: 0x0034E4DF File Offset: 0x0034C6DF
		public void SetHasBeenRead(bool flag)
		{
			if (flag)
			{
				The.Game.SetStringGameState(this.GetBookKey(), "Yes");
				return;
			}
			The.Game.SetStringGameState(this.GetBookKey(), "");
		}

		// Token: 0x04003CC5 RID: 15557
		public const string DEFAULT_OPEN_SOUND = "Sounds/Interact/sfx_interact_book_read";

		// Token: 0x04003CC6 RID: 15558
		public const string DEFAULT_PAGE_SOUND = "Sounds/Interact/sfx_interact_book_pageTurn";

		// Token: 0x04003CC7 RID: 15559
		public string ID = "";
	}
}
