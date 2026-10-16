using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FFC RID: 4092
	[Serializable]
	public class AttributeMultiplier : IPart
	{
		// Token: 0x0600A311 RID: 41745 RVA: 0x003B99EC File Offset: 0x003B7BEC
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A312 RID: 41746 RVA: 0x003B99F0 File Offset: 0x003B7BF0
		public override void TurnTick(long TimeTick, int Amount)
		{
			float num = (float)this.ParentObject.GetStat(this.Attribute).BaseValue * ((float)this.Percent / 100f);
			base.StatShifter.SetStatShift(this.Attribute, (int)num, false);
		}

		// Token: 0x0600A313 RID: 41747 RVA: 0x003B9A38 File Offset: 0x003B7C38
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A314 RID: 41748 RVA: 0x003B9A50 File Offset: 0x003B7C50
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int num = (Statistic.IsInverseBenefit(this.Attribute) ? (-this.Percent) : this.Percent);
			E.Postfix.AppendSigned(num).Append("% ").Append(Statistic.GetStatDisplayName(this.Attribute));
			return base.HandleEvent(E);
		}

		// Token: 0x0400438E RID: 17294
		public string Attribute;

		// Token: 0x0400438F RID: 17295
		public int Percent;
	}
}
