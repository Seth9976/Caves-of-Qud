using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001054 RID: 4180
	[Serializable]
	public class Interactable : IPart
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600A675 RID: 42613 RVA: 0x003CE500 File Offset: 0x003CC700
		public string CommandID
		{
			get
			{
				if (this._CommandID.IsNullOrEmpty())
				{
					this._CommandID = Guid.NewGuid().ToString();
				}
				return this._CommandID;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600A676 RID: 42614 RVA: 0x003CE539 File Offset: 0x003CC739
		// (set) Token: 0x0600A677 RID: 42615 RVA: 0x003CE547 File Offset: 0x003CC747
		public bool CanSmartUse
		{
			get
			{
				return this.Flags.HasBit(1);
			}
			set
			{
				(ref this.Flags).SetBit(1, value);
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600A678 RID: 42616 RVA: 0x003CE556 File Offset: 0x003CC756
		// (set) Token: 0x0600A679 RID: 42617 RVA: 0x003CE564 File Offset: 0x003CC764
		public bool UsePopup
		{
			get
			{
				return this.Flags.HasBit(2);
			}
			set
			{
				(ref this.Flags).SetBit(2, value);
			}
		}

		// Token: 0x0600A67A RID: 42618 RVA: 0x003CE573 File Offset: 0x003CC773
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A67B RID: 42619 RVA: 0x003CE5A1 File Offset: 0x003CC7A1
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !this.CanSmartUse;
		}

		// Token: 0x0600A67C RID: 42620 RVA: 0x003CE5AC File Offset: 0x003CC7AC
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (this.CanSmartUse)
			{
				this.DoInteract(E.Actor);
				return false;
			}
			return true;
		}

		// Token: 0x0600A67D RID: 42621 RVA: 0x003CE5C8 File Offset: 0x003CC7C8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (!this.Action.IsNullOrEmpty())
			{
				E.AddAction(this.CommandID, this.Action, this.CommandID, null, char.ToLower(this.Action[0]), false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A67E RID: 42622 RVA: 0x003CE61E File Offset: 0x003CC81E
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == this.CommandID)
			{
				this.DoInteract(E.Actor);
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A67F RID: 42623 RVA: 0x003CE64C File Offset: 0x003CC84C
		public void DoInteract(GameObject Actor)
		{
			if (!this.Sound.IsNullOrEmpty())
			{
				base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
			if (!this.Message.IsNullOrEmpty())
			{
				string text = this.Message.StartReplace().AddObject(Actor, null).AddObject(this.ParentObject, null)
					.ToString();
				if (this.UsePopup)
				{
					Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
					return;
				}
				base.EmitMessage(text, ' ', false, false, false, null, null);
			}
		}

		// Token: 0x040044BB RID: 17595
		public const int FLAG_SMART_USE = 1;

		// Token: 0x040044BC RID: 17596
		public const int FLAG_POPUP = 2;

		// Token: 0x040044BD RID: 17597
		public string Action;

		// Token: 0x040044BE RID: 17598
		public string Message;

		// Token: 0x040044BF RID: 17599
		public string Sound;

		// Token: 0x040044C0 RID: 17600
		public int Flags = 1;

		// Token: 0x040044C1 RID: 17601
		[NonSerialized]
		private string _CommandID;
	}
}
