using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000E09 RID: 3593
	[Serializable]
	public class AttackerElementalAmplifier : IPart
	{
		// Token: 0x06009118 RID: 37144 RVA: 0x003452FE File Offset: 0x003434FE
		public AttackerElementalAmplifier()
		{
		}

		// Token: 0x06009119 RID: 37145 RVA: 0x00345311 File Offset: 0x00343511
		public AttackerElementalAmplifier(string Attribute, int Amplification)
		{
			this.Attribute = Attribute;
			this.Amplification = Amplification;
		}

		// Token: 0x0600911A RID: 37146 RVA: 0x00345334 File Offset: 0x00343534
		public override bool SameAs(IPart Part)
		{
			AttackerElementalAmplifier attackerElementalAmplifier = Part as AttackerElementalAmplifier;
			return attackerElementalAmplifier != null && attackerElementalAmplifier.Attribute == this.Attribute && attackerElementalAmplifier.Amplification == this.Amplification;
		}

		// Token: 0x0600911B RID: 37147 RVA: 0x0034536E File Offset: 0x0034356E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AttackerDealingDamageEvent.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600911C RID: 37148 RVA: 0x0034538C File Offset: 0x0034358C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendAmplification));
			return base.HandleEvent(E);
		}

		// Token: 0x0600911D RID: 37149 RVA: 0x003453AD File Offset: 0x003435AD
		public void AppendAmplification(StringBuilder SB)
		{
			SB.AppendSigned(this.Amplification).Append("% ").Append(this.Attribute.ToLowerInvariant())
				.Append(" damage dealt");
		}

		// Token: 0x0600911E RID: 37150 RVA: 0x003453E0 File Offset: 0x003435E0
		public override bool HandleEvent(AttackerDealingDamageEvent E)
		{
			if (E.Damage.HasAttribute(this.Attribute))
			{
				E.Damage.Amount = (int)((float)E.Damage.Amount * (1f + (float)this.Amplification * 0.01f));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003C80 RID: 15488
		public string Attribute = "Heat";

		// Token: 0x04003C81 RID: 15489
		public int Amplification;
	}
}
