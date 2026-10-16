using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E9C RID: 3740
	[Serializable]
	public class DischargeOnStep : IActivePart
	{
		// Token: 0x0600965C RID: 38492 RVA: 0x00367C58 File Offset: 0x00365E58
		public DischargeOnStep()
		{
			this.WorksOnCellContents = true;
		}

		// Token: 0x0600965D RID: 38493 RVA: 0x00367CEC File Offset: 0x00365EEC
		public override bool SameAs(IPart p)
		{
			DischargeOnStep dischargeOnStep = p as DischargeOnStep;
			return dischargeOnStep.Chance == this.Chance && !(dischargeOnStep.SaveStat != this.SaveStat) && !(dischargeOnStep.SaveDifficultyStat != this.SaveDifficultyStat) && dischargeOnStep.SaveTarget == this.SaveTarget && !(dischargeOnStep.SaveVs != this.SaveVs) && dischargeOnStep.CooldownLeft == this.CooldownLeft && dischargeOnStep.TriggerOnSaveSuccess == this.TriggerOnSaveSuccess && !(dischargeOnStep.Cooldown != this.Cooldown) && !(dischargeOnStep.Voltage != this.Voltage) && !(dischargeOnStep.DamageRange != this.DamageRange) && base.SameAs(p);
		}

		// Token: 0x0600965E RID: 38494 RVA: 0x00367DC5 File Offset: 0x00365FC5
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600965F RID: 38495 RVA: 0x00367DC8 File Offset: 0x00365FC8
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.CooldownLeft > 0)
			{
				this.CooldownLeft--;
				if (this.CooldownLeft <= 0)
				{
					this.SyncColor();
				}
			}
			this.CheckActivate();
		}

		// Token: 0x06009660 RID: 38496 RVA: 0x00367DF6 File Offset: 0x00365FF6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetNavigationWeightEvent.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009661 RID: 38497 RVA: 0x00367E24 File Offset: 0x00366024
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if ((!E.Flying || this.ParentObject.IsFlying) && E.PhaseMatches(this.ParentObject))
			{
				E.MinWeight(98);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009662 RID: 38498 RVA: 0x00367E58 File Offset: 0x00366058
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			E.IndicateObject = this.ParentObject;
			return false;
		}

		// Token: 0x06009663 RID: 38499 RVA: 0x00367E67 File Offset: 0x00366067
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.SyncColor();
			return base.HandleEvent(E);
		}

		// Token: 0x06009664 RID: 38500 RVA: 0x00367E76 File Offset: 0x00366076
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.CheckActivate();
			return base.HandleEvent(E);
		}

		// Token: 0x06009665 RID: 38501 RVA: 0x00367E88 File Offset: 0x00366088
		public void CheckActivate()
		{
			if (this.CooldownLeft <= 0 && this.GetActivePartFirstSubject(new Predicate<GameObject>(this.ValidStepTarget)) != null && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && this.Chance.in100())
			{
				foreach (GameObject gameObject in this.GetActivePartSubjects(new Predicate<GameObject>(this.ValidStepTarget)))
				{
					if (string.IsNullOrEmpty(this.SaveStat) || gameObject.MakeSave(this.SaveStat, this.SaveTarget, this.ParentObject, this.SaveDifficultyStat, null, false, false, false, false, null) == this.TriggerOnSaveSuccess)
					{
						this.Discharge();
						break;
					}
				}
			}
		}

		// Token: 0x06009666 RID: 38502 RVA: 0x00367F7C File Offset: 0x0036617C
		public void SyncColor()
		{
			if (this.CooldownLeft > 0)
			{
				if (!string.IsNullOrEmpty(this.CooldownColorString))
				{
					this.ParentObject.Render.ColorString = this.CooldownColorString;
				}
				if (!string.IsNullOrEmpty(this.CooldownTileColor))
				{
					this.ParentObject.Render.TileColor = this.CooldownTileColor;
				}
				if (!string.IsNullOrEmpty(this.CooldownDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.CooldownDetailColor;
					return;
				}
			}
			else
			{
				if (!string.IsNullOrEmpty(this.ReadyColorString))
				{
					this.ParentObject.Render.ColorString = this.ReadyColorString;
				}
				if (!string.IsNullOrEmpty(this.ReadyTileColor))
				{
					this.ParentObject.Render.TileColor = this.ReadyTileColor;
				}
				if (!string.IsNullOrEmpty(this.ReadyDetailColor))
				{
					this.ParentObject.Render.DetailColor = this.ReadyDetailColor;
				}
			}
		}

		// Token: 0x06009667 RID: 38503 RVA: 0x00368068 File Offset: 0x00366268
		public bool ValidStepTarget(GameObject obj)
		{
			return obj.IsCombatObject() && obj.PhaseAndFlightMatches(this.ParentObject);
		}

		// Token: 0x06009668 RID: 38504 RVA: 0x00368080 File Offset: 0x00366280
		public void Discharge()
		{
			if (this.CooldownLeft > 0)
			{
				return;
			}
			this.CooldownLeft = Stat.Roll(this.Cooldown, null);
			List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
			adjacentCells.Add(this.ParentObject.CurrentCell);
			adjacentCells.ForEach(delegate(Cell c)
			{
				this.ParentObject.Discharge(c, this.Voltage.RollCached(), 0, this.DamageRange, null, this.ParentObject, this.ParentObject, null, null, null, null, null, 0, false, false, false);
			});
			adjacentCells.ForEach(delegate(Cell c)
			{
				c.GetObjectsWithPart("DischargeOnStep").ForEach(delegate(GameObject o)
				{
					o.GetPart<DischargeOnStep>().Discharge();
				});
			});
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			this.SyncColor();
		}

		// Token: 0x04003E47 RID: 15943
		public int CooldownLeft;

		// Token: 0x04003E48 RID: 15944
		public string Cooldown = "1d6";

		// Token: 0x04003E49 RID: 15945
		public int Chance = 100;

		// Token: 0x04003E4A RID: 15946
		public string SaveStat;

		// Token: 0x04003E4B RID: 15947
		public string SaveDifficultyStat;

		// Token: 0x04003E4C RID: 15948
		public int SaveTarget = 15;

		// Token: 0x04003E4D RID: 15949
		public string SaveVs;

		// Token: 0x04003E4E RID: 15950
		public bool TriggerOnSaveSuccess = true;

		// Token: 0x04003E4F RID: 15951
		public string Voltage = "1d2";

		// Token: 0x04003E50 RID: 15952
		public string DamageRange = "4d4";

		// Token: 0x04003E51 RID: 15953
		public string CooldownColorString = "&w";

		// Token: 0x04003E52 RID: 15954
		public new string ReadyColorString = "&W";

		// Token: 0x04003E53 RID: 15955
		public string CooldownTileColor = "&w";

		// Token: 0x04003E54 RID: 15956
		public string ReadyTileColor = "&W";

		// Token: 0x04003E55 RID: 15957
		public string CooldownDetailColor = "w";

		// Token: 0x04003E56 RID: 15958
		public new string ReadyDetailColor = "w";
	}
}
