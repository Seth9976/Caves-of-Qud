using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FBF RID: 4031
	[Serializable]
	public class GasAsh : IObjectGasBehavior
	{
		// Token: 0x0600A046 RID: 41030 RVA: 0x003A53F8 File Offset: 0x003A35F8
		public override bool SameAs(IPart p)
		{
			GasAsh gasAsh = p as GasAsh;
			return !(gasAsh.GasType != this.GasType) && gasAsh.GasLevel == this.GasLevel && base.SameAs(p);
		}

		// Token: 0x0600A047 RID: 41031 RVA: 0x003A5438 File Offset: 0x003A3638
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600A048 RID: 41032 RVA: 0x003A5460 File Offset: 0x003A3660
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyAshPoison") && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 2 + 5, 70);
					}
				}
				else
				{
					E.MinWeight(30);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A049 RID: 41033 RVA: 0x003A5504 File Offset: 0x003A3704
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyAshPoison") && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 10 + 1, 14);
					}
				}
				else
				{
					E.MinWeight(6);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A04A RID: 41034 RVA: 0x003A55A6 File Offset: 0x003A37A6
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.UpdateOpacity();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A04B RID: 41035 RVA: 0x003A55B5 File Offset: 0x003A37B5
		public override bool ApplyGas(Cell Cell)
		{
			bool flag = base.ApplyGas(Cell);
			this.UpdateOpacity();
			return flag;
		}

		// Token: 0x0600A04C RID: 41036 RVA: 0x003A55C4 File Offset: 0x003A37C4
		public override bool ApplyGas(GameObject Object)
		{
			if (Object == this.ParentObject)
			{
				return false;
			}
			if (!Object.Respires)
			{
				return false;
			}
			Gas baseGas = base.BaseGas;
			if (!CheckGasCanAffectEvent.Check(Object, this.ParentObject, baseGas))
			{
				return false;
			}
			if (!Object.FireEvent("CanApplyAshPoison"))
			{
				return false;
			}
			if (!CanApplyEffectEvent.Check<AshPoison>(Object, 0))
			{
				return false;
			}
			if (!Object.PhaseMatches(this.ParentObject))
			{
				return false;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(Object, this.ParentObject, baseGas, null, 0, 0, false);
			if (@for <= 0)
			{
				return false;
			}
			Object.RemoveEffect<AshPoison>();
			Object.ApplyEffect(new AshPoison(Stat.Random(1, 10), baseGas.Creator)
			{
				Damage = this.GasLevel * 2
			}, null);
			return Object.TakeDamage((int)Math.Max(Math.Floor((double)(@for + 1) / 10.0), 1.0), "from %t {{K|choking ash}}!", "InhaleDanger Asphyxiation Gas", null, null, null, baseGas.Creator, null, null, null, false, true, false, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x0600A04D RID: 41037 RVA: 0x003A56C4 File Offset: 0x003A38C4
		public void UpdateOpacity()
		{
			bool flag = base.GasDensity() >= 40;
			if (this.ParentObject.Render.Occluding != flag)
			{
				this.ParentObject.Render.Occluding = flag;
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell == null)
				{
					return;
				}
				currentCell.ClearOccludeCache();
			}
		}

		// Token: 0x0400429F RID: 17055
		public string GasType = "Ash";

		// Token: 0x040042A0 RID: 17056
		public int GasLevel = 1;
	}
}
