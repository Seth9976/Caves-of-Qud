using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x0200101E RID: 4126
	[Serializable]
	public class DoubleEdge : IPart
	{
		// Token: 0x0600A452 RID: 42066 RVA: 0x003C1690 File Offset: 0x003BF890
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AttackerDealingDamageEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600A453 RID: 42067 RVA: 0x003C16B6 File Offset: 0x003BF8B6
		public override bool HandleEvent(AttackerDealingDamageEvent E)
		{
			E.Damage.Amount += (int)((float)E.Damage.Amount * ((float)this.Dealt / 100f));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A454 RID: 42068 RVA: 0x003C16EC File Offset: 0x003BF8EC
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			E.Damage.Amount += (int)((float)E.Damage.Amount * ((float)this.Received / 100f));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A455 RID: 42069 RVA: 0x003C1722 File Offset: 0x003BF922
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600A456 RID: 42070 RVA: 0x003C1744 File Offset: 0x003BF944
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Received != 0)
			{
				SB.Append("Take ").Append(this.Received).Append((this.Received > 0) ? "% more" : "% less")
					.Append(" damage");
				if (this.Dealt != 0)
				{
					SB.Append((Math.Sign(this.Received) != Math.Sign(this.Dealt)) ? " but " : " and ");
				}
			}
			if (this.Dealt != 0)
			{
				SB.Append((this.Received != 0) ? "deal " : "Deal ").Append(this.Dealt).Append((this.Dealt > 0) ? "% more" : "% less")
					.Append(" damage");
			}
		}

		// Token: 0x04004420 RID: 17440
		public int Dealt;

		// Token: 0x04004421 RID: 17441
		public int Received;
	}
}
