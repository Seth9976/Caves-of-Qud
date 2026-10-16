using System;
using System.Text;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x020012A9 RID: 4777
	[Serializable]
	public class TrashOracle : IActivePart
	{
		// Token: 0x0600BCB3 RID: 48307 RVA: 0x00451348 File Offset: 0x0044F548
		public TrashOracle()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600BCB4 RID: 48308 RVA: 0x00451357 File Offset: 0x0044F557
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetSkillEffectChanceEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600BCB5 RID: 48309 RVA: 0x00451378 File Offset: 0x0044F578
		public override bool HandleEvent(GetSkillEffectChanceEvent E)
		{
			if (E.Skill is Customs_TrashDivining && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.Bonus != 0)
				{
					E.Chance += this.Bonus;
				}
				if (this.Magnitude != 0)
				{
					if (this.Magnitude > 0)
					{
						E.Chance *= this.Magnitude;
					}
					else
					{
						E.Chance /= this.Magnitude;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCB6 RID: 48310 RVA: 0x00451426 File Offset: 0x0044F626
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCB7 RID: 48311 RVA: 0x00451448 File Offset: 0x0044F648
		public void AppendEffect(StringBuilder SB)
		{
			SB.Append("Chance to reveal secrets via Trash Divining ");
			bool flag = false;
			if (this.Bonus != 0)
			{
				SB.Append((this.Bonus > 0) ? "increased " : "decreased ").Append("by ").Append(this.Bonus)
					.Append('%');
				if (this.Magnitude != 0)
				{
					flag = Math.Sign(this.Magnitude) != Math.Sign(this.Bonus);
					SB.Append(flag ? " but " : " and ");
				}
			}
			if (this.Magnitude != 0)
			{
				if (this.Bonus == 0 || flag)
				{
					SB.Append((this.Magnitude > 0) ? "increased " : "decreased ");
				}
				SB.Append("by ").Append(this.Magnitude).Append('x');
			}
		}

		// Token: 0x04004BA5 RID: 19365
		public int Bonus;

		// Token: 0x04004BA6 RID: 19366
		public int Magnitude;
	}
}
