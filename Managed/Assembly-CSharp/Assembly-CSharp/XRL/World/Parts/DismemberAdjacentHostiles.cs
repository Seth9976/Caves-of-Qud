using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, ChanceToActivate and ChancePerHostile will,
	/// if less than 100, be increased by a relative percentage of
	/// ((power load - 100) / 10), i.e. 30% for the standard overload power
	/// load of 400.
	/// </remarks>
	// Token: 0x0200101C RID: 4124
	[Serializable]
	public class DismemberAdjacentHostiles : IPoweredPart
	{
		// Token: 0x0600A43B RID: 42043 RVA: 0x003C0E24 File Offset: 0x003BF024
		public DismemberAdjacentHostiles()
		{
			this.ChargeUse = 1000;
			this.IsPowerLoadSensitive = true;
			this.WorksOnEquipper = true;
			this.NameForStatus = "AntipersonnelSystems";
		}

		// Token: 0x0600A43C RID: 42044 RVA: 0x003C0E74 File Offset: 0x003BF074
		public override bool SameAs(IPart p)
		{
			DismemberAdjacentHostiles dismemberAdjacentHostiles = p as DismemberAdjacentHostiles;
			return dismemberAdjacentHostiles.ChanceToActivate == this.ChanceToActivate && dismemberAdjacentHostiles.ChancePerHostile == this.ChancePerHostile && dismemberAdjacentHostiles.CanAlwaysDecapitate == this.CanAlwaysDecapitate && dismemberAdjacentHostiles.UsesChargeToActivate == this.UsesChargeToActivate && dismemberAdjacentHostiles.UsesChargePerHostile == this.UsesChargePerHostile && dismemberAdjacentHostiles.UsesChargePerDismemberment == this.UsesChargePerDismemberment && base.SameAs(p);
		}

		// Token: 0x0600A43D RID: 42045 RVA: 0x003C0EEF File Offset: 0x003BF0EF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A43E RID: 42046 RVA: 0x003C0EF2 File Offset: 0x003BF0F2
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckDismemberment();
		}

		// Token: 0x0600A43F RID: 42047 RVA: 0x003C0EFA File Offset: 0x003BF0FA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == PooledEvent<GenericQueryEvent>.ID;
		}

		// Token: 0x0600A440 RID: 42048 RVA: 0x003C0F18 File Offset: 0x003BF118
		public override bool HandleEvent(GenericQueryEvent E)
		{
			if (E.Query == "PhaseHarmonicEligible" && base.IsTechScannable)
			{
				E.Result = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A441 RID: 42049 RVA: 0x003C0F42 File Offset: 0x003BF142
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 25) && base.HandleEvent(E);
		}

		// Token: 0x0600A442 RID: 42050 RVA: 0x003C0F58 File Offset: 0x003BF158
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && this.CheckDismemberment(E.Actor, E.Actor, null, false, false, true, true))
			{
				E.Identify = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600A443 RID: 42051 RVA: 0x003C0FB0 File Offset: 0x003BF1B0
		public void CheckDismemberment()
		{
			GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
			if (gameObject == null)
			{
				return;
			}
			Cell currentCell = gameObject.CurrentCell;
			if (currentCell == null || currentCell.OnWorldMap())
			{
				return;
			}
			int num = this.MyPowerLoadLevel();
			int num2 = this.ChanceToActivate;
			if (num2 < 100 && this.IsPowerLoadSensitive)
			{
				num2 = num2 * (100 + IComponent<GameObject>.PowerLoadBonus(num, 100, 10)) / 100;
			}
			if (!num2.in100())
			{
				return;
			}
			bool usesChargeToActivate = this.UsesChargeToActivate;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			bool flag8 = false;
			bool flag9 = false;
			int num3 = 1;
			int? num4 = new int?(num);
			if (!base.IsReady(usesChargeToActivate, flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, num3, null, false, 0L, num4))
			{
				return;
			}
			this.CheckDismemberment(currentCell, gameObject, new int?(num));
			foreach (Cell cell in currentCell.GetLocalAdjacentCells())
			{
				this.CheckDismemberment(cell, gameObject, new int?(num));
			}
		}

		// Token: 0x0600A444 RID: 42052 RVA: 0x003C10B4 File Offset: 0x003BF2B4
		public void CheckDismemberment(Cell C, GameObject user = null, int? load = null)
		{
			int i = 0;
			int num = C.Objects.Count;
			while (i < num)
			{
				GameObject gameObject = C.Objects[i];
				if (gameObject.Brain != null)
				{
					this.CheckDismemberment(gameObject, user, load, true, true, false, false);
					if (num != C.Objects.Count)
					{
						num = C.Objects.Count;
						if (i < num && C.Objects[i] != gameObject)
						{
							i--;
						}
					}
				}
				i++;
			}
		}

		// Token: 0x0600A445 RID: 42053 RVA: 0x003C1130 File Offset: 0x003BF330
		public bool CheckDismemberment(GameObject Target, GameObject Actor = null, int? PowerLoad = null, bool RequireHostility = true, bool CheckChance = true, bool IgnoreSubject = false, bool UsePopups = false)
		{
			if (!GameObject.Validate(ref Target))
			{
				return false;
			}
			if (Actor == null)
			{
				Actor = this.ParentObject.Equipped ?? this.ParentObject.Implantee;
			}
			if (RequireHostility && !Target.IsHostileTowards(Actor))
			{
				return false;
			}
			if (Actor.IsInStasis())
			{
				return false;
			}
			if (Target.Body == null)
			{
				return false;
			}
			if (!Actor.FlightMatches(Target))
			{
				return false;
			}
			int? num;
			if (this.UsesChargePerHostile)
			{
				bool flag = true;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				num = PowerLoad;
				if (!base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, IgnoreSubject, false, 1, null, false, 0L, num))
				{
					return false;
				}
			}
			if (CheckChance)
			{
				int num2 = this.ChancePerHostile;
				if (num2 < 100 && this.IsPowerLoadSensitive)
				{
					num2 = num2 * (100 + IComponent<GameObject>.PowerLoadBonus(PowerLoad.Value, 100, 10)) / 100;
				}
				if (!num2.in100())
				{
					return false;
				}
			}
			int @for = GetActivationPhaseEvent.GetFor(this.ParentObject, Actor.GetPhase());
			if (!Target.PhaseMatches(@for))
			{
				return false;
			}
			if (PowerLoad == null)
			{
				PowerLoad = new int?(this.MyPowerLoadLevel());
			}
			bool usesChargePerDismemberment = this.UsesChargePerDismemberment;
			bool flag9 = false;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			num = PowerLoad;
			return base.IsReady(usesChargePerDismemberment, flag9, flag10, flag11, flag12, flag13, flag14, flag15, IgnoreSubject, false, 1, null, false, 0L, num) && Axe_Dismember.Dismember(Actor, Target, null, null, this.ParentObject, null, "sfx_characterTrigger_dismember", this.CanAlwaysDecapitate, false, true, UsePopups);
		}

		// Token: 0x04004417 RID: 17431
		public int ChanceToActivate = 100;

		// Token: 0x04004418 RID: 17432
		public int ChancePerHostile = 10;

		// Token: 0x04004419 RID: 17433
		public bool CanAlwaysDecapitate;

		// Token: 0x0400441A RID: 17434
		public bool UsesChargeToActivate;

		// Token: 0x0400441B RID: 17435
		public bool UsesChargePerHostile;

		// Token: 0x0400441C RID: 17436
		public bool UsesChargePerDismemberment = true;
	}
}
