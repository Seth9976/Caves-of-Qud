using System;

namespace XRL.World.Parts
{
	// Token: 0x0200100E RID: 4110
	[Serializable]
	public class Cursed : IActivePart
	{
		// Token: 0x0600A3B6 RID: 41910 RVA: 0x003BD128 File Offset: 0x003BB328
		public Cursed()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A3B7 RID: 41911 RVA: 0x003BD142 File Offset: 0x003BB342
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A3B8 RID: 41912 RVA: 0x003BD148 File Offset: 0x003BB348
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}

		// Token: 0x0600A3B9 RID: 41913 RVA: 0x003BD17B File Offset: 0x003BB37B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == PooledEvent<CanBeUnequippedEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.RevealInDescription) || ID == PooledEvent<GetSlotsRequiredEvent>.ID || ID == PooledEvent<IsAfflictionEvent>.ID;
		}

		// Token: 0x0600A3BA RID: 41914 RVA: 0x003BD1BC File Offset: 0x003BB3BC
		public override bool HandleEvent(CanBeUnequippedEvent E)
		{
			return (E.Forced || !base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null)) && base.HandleEvent(E);
		}

		// Token: 0x0600A3BB RID: 41915 RVA: 0x003BD204 File Offset: 0x003BB404
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (!E.Forced && this.ParentObject.Equipped != null && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AddFailureMessage("You can't remove " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3BC RID: 41916 RVA: 0x003BD296 File Offset: 0x003BB496
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.AllowReduction = false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3BD RID: 41917 RVA: 0x003BD2B4 File Offset: 0x003BB4B4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.RevealInDescription)
			{
				E.Postfix.AppendRules(this.DescriptionPostfix);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3BE RID: 41918 RVA: 0x003BD2D7 File Offset: 0x003BB4D7
		public override bool HandleEvent(IsAfflictionEvent E)
		{
			return false;
		}

		// Token: 0x0600A3BF RID: 41919 RVA: 0x003BD2DA File Offset: 0x003BB4DA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040043CA RID: 17354
		public bool RevealInDescription;

		// Token: 0x040043CB RID: 17355
		public string DescriptionPostfix = "Cannot be removed once equipped.";
	}
}
