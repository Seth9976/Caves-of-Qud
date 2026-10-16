using System;
using System.Linq;

namespace XRL.World.Parts
{
	// Token: 0x02000FF3 RID: 4083
	[Serializable]
	public class AilingQuickness : IActivePart
	{
		// Token: 0x0600A2C3 RID: 41667 RVA: 0x003B77DB File Offset: 0x003B59DB
		public AilingQuickness()
		{
			this.WorksOnHolder = true;
			this.WorksOnCarrier = true;
		}

		// Token: 0x0600A2C4 RID: 41668 RVA: 0x003B77FF File Offset: 0x003B59FF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A2C5 RID: 41669 RVA: 0x003B7802 File Offset: 0x003B5A02
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CalculateBonus(true, Amount);
		}

		// Token: 0x0600A2C6 RID: 41670 RVA: 0x003B780C File Offset: 0x003B5A0C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == TakenEvent.ID || ID == DroppedEvent.ID;
		}

		// Token: 0x0600A2C7 RID: 41671 RVA: 0x003B7834 File Offset: 0x003B5A34
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.SpeedBonus > 0)
			{
				E.Postfix.Compound("{{rules|", '\n');
				Statistic.AppendStatAdjustDescription(E.Postfix, "Speed", this.SpeedBonus, false, false);
				E.Postfix.Append(" per negative status effect}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2C8 RID: 41672 RVA: 0x003B788D File Offset: 0x003B5A8D
		public override bool HandleEvent(TakenEvent E)
		{
			this.CalculateBonus(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2C9 RID: 41673 RVA: 0x003B789E File Offset: 0x003B5A9E
		public override bool HandleEvent(DroppedEvent E)
		{
			this.CalculateBonus(false, 1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2CA RID: 41674 RVA: 0x003B78B0 File Offset: 0x003B5AB0
		public void CalculateBonus(bool UseCharge = false, int MultipleCharge = 1)
		{
			if (!base.IsReady(UseCharge, false, false, false, false, false, false, false, false, false, MultipleCharge, null, false, 0L, null))
			{
				base.StatShifter.RemoveStatShifts();
				this.LastCount = -1;
				return;
			}
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			int num = activePartFirstSubject.Effects.Count<Effect>((Effect x) => x.IsOfType(33554432));
			if (this.LastCount == num)
			{
				return;
			}
			this.LastCount = num;
			if (num <= 0)
			{
				base.StatShifter.RemoveStatShifts(activePartFirstSubject);
				return;
			}
			base.StatShifter.SetStatShift(activePartFirstSubject, "Speed", this.SpeedBonus * num, false);
		}

		// Token: 0x04004373 RID: 17267
		public int SpeedBonus = 5;

		// Token: 0x04004374 RID: 17268
		[NonSerialized]
		private int LastCount = -1;
	}
}
