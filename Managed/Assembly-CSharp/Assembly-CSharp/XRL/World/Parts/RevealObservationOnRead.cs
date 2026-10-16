using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000E19 RID: 3609
	[Serializable]
	public class RevealObservationOnRead : IPart
	{
		// Token: 0x06009301 RID: 37633 RVA: 0x0034FB84 File Offset: 0x0034DD84
		public RevealObservationOnRead()
		{
		}

		// Token: 0x06009302 RID: 37634 RVA: 0x0034FB8C File Offset: 0x0034DD8C
		public RevealObservationOnRead(string ObservationId)
		{
			this.ObservationId = ObservationId;
		}

		// Token: 0x06009303 RID: 37635 RVA: 0x0034FB9B File Offset: 0x0034DD9B
		public override bool SameAs(IPart p)
		{
			return !((p as RevealObservationOnRead).ObservationId != this.ObservationId) && base.SameAs(p);
		}

		// Token: 0x06009304 RID: 37636 RVA: 0x0034FBBE File Offset: 0x0034DDBE
		public override void Initialize()
		{
			base.Initialize();
			this.ParentObject.SetStringProperty("BookID", this.ParentObject.ID, false);
		}

		// Token: 0x06009305 RID: 37637 RVA: 0x0034FBE2 File Offset: 0x0034DDE2
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<HasBeenReadEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x06009306 RID: 37638 RVA: 0x0034FC08 File Offset: 0x0034DE08
		public override bool HandleEvent(HasBeenReadEvent E)
		{
			return (E.Actor != The.Player || (!this.ObservationId.IsNullOrEmpty() && !JournalAPI.IsObservationRevealed(this.ObservationId))) && base.HandleEvent(E);
		}

		// Token: 0x06009307 RID: 37639 RVA: 0x0034FC3C File Offset: 0x0034DE3C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			this.CheckObservation();
			E.AddAction("Read", "read", "Read", null, 'r', false, (!this.ObservationId.IsNullOrEmpty() && !JournalAPI.IsObservationRevealed(this.ObservationId)) ? 100 : 15, 0, true, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009308 RID: 37640 RVA: 0x0034FC98 File Offset: 0x0034DE98
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Read" && E.Actor.IsPlayer())
			{
				this.CheckObservation();
				if (this.ObservationId != null)
				{
					JournalAPI.RevealObservation(this.ObservationId, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009309 RID: 37641 RVA: 0x0034FCE8 File Offset: 0x0034DEE8
		private void CheckObservation()
		{
			if (this.ObservationId == null)
			{
				AddObservation part = this.ParentObject.GetPart<AddObservation>();
				if (part != null)
				{
					this.ObservationId = part.ID;
				}
			}
		}

		// Token: 0x04003CD6 RID: 15574
		public string ObservationId;
	}
}
