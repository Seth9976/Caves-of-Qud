using System;

namespace XRL.World.Parts
{
	// Token: 0x02001069 RID: 4201
	[Serializable]
	public class MoveCostMultiplier : IPart
	{
		// Token: 0x0600A749 RID: 42825 RVA: 0x003D211E File Offset: 0x003D031E
		public MoveCostMultiplier()
		{
		}

		// Token: 0x0600A74A RID: 42826 RVA: 0x003D212E File Offset: 0x003D032E
		public MoveCostMultiplier(int Amount)
			: this()
		{
			this.Amount = Amount;
		}

		// Token: 0x0600A74B RID: 42827 RVA: 0x003D213D File Offset: 0x003D033D
		public override bool SameAs(IPart p)
		{
			return (p as MoveCostMultiplier).Amount == this.Amount && base.SameAs(p);
		}

		// Token: 0x0600A74C RID: 42828 RVA: 0x003D215B File Offset: 0x003D035B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A74D RID: 42829 RVA: 0x003D2184 File Offset: 0x003D0384
		public override bool HandleEvent(EquippedEvent E)
		{
			GameObject actor = E.Actor;
			base.StatShifter.SetStatShift(actor, "MoveSpeed", this.Amount, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A74E RID: 42830 RVA: 0x003D21B8 File Offset: 0x003D03B8
		public override bool HandleEvent(UnequippedEvent E)
		{
			base.StatShifter.RemoveStatShifts(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A74F RID: 42831 RVA: 0x003D21D2 File Offset: 0x003D03D2
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Amount != 0)
			{
				E.Postfix.Append("\n{{rules|").Append((-this.Amount).Signed(false)).Append(" move speed}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004511 RID: 17681
		public int Amount = -5;
	}
}
