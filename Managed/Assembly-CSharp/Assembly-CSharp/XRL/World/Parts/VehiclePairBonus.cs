using System;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x020012B6 RID: 4790
	[Serializable]
	public class VehiclePairBonus : IPart
	{
		// Token: 0x0600BD52 RID: 48466 RVA: 0x00454CA9 File Offset: 0x00452EA9
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BD53 RID: 48467 RVA: 0x00454CAC File Offset: 0x00452EAC
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ParentObject.IsPlayer())
			{
				if (this.Paired == 0)
				{
					base.StatShifter.RemoveStatShifts();
					return;
				}
				if (this.Paired != 0)
				{
					base.StatShifter.SetStatShift(this.Stat, this.Paired, false);
					return;
				}
			}
			else
			{
				if (this.Unpaired == 0)
				{
					base.StatShifter.RemoveStatShifts();
					return;
				}
				if (this.Unpaired != 0)
				{
					base.StatShifter.SetStatShift(this.Stat, this.Unpaired, false);
				}
			}
		}

		// Token: 0x0600BD54 RID: 48468 RVA: 0x00454D31 File Offset: 0x00452F31
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600BD55 RID: 48469 RVA: 0x00454D47 File Offset: 0x00452F47
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD56 RID: 48470 RVA: 0x00454D68 File Offset: 0x00452F68
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Paired != 0)
			{
				Statistic.AppendStatAdjustDescription(SB, this.Stat, this.Paired, false, false);
				StringBuilder stringBuilder = SB.Append(" when you are paired with ");
				GameObject parentObject = this.ParentObject;
				stringBuilder.Append(((parentObject != null) ? parentObject.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false) : null) ?? "the golem");
				if (this.Unpaired != 0)
				{
					SB.Append(" and ");
				}
			}
			if (this.Unpaired != 0)
			{
				Statistic.AppendStatAdjustDescription(SB, this.Stat, this.Unpaired, false, false);
				StringBuilder stringBuilder2 = SB.Append(" when you are unpaired with ");
				GameObject parentObject2 = this.ParentObject;
				stringBuilder2.Append(((parentObject2 != null) ? parentObject2.t(int.MaxValue, null, null, false, false, false, false, true, true, true, true, null, false, true, false, null, true, null, false) : null) ?? "the golem");
			}
		}

		// Token: 0x04004BE0 RID: 19424
		public string Stat;

		// Token: 0x04004BE1 RID: 19425
		public int Paired;

		// Token: 0x04004BE2 RID: 19426
		public int Unpaired;
	}
}
