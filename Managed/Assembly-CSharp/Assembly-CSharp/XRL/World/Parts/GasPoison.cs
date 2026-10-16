using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC7 RID: 4039
	[Serializable]
	public class GasPoison : IObjectGasBehavior
	{
		// Token: 0x0600A099 RID: 41113 RVA: 0x003A7594 File Offset: 0x003A5794
		public override bool SameAs(IPart p)
		{
			GasPoison gasPoison = p as GasPoison;
			return !(gasPoison.GasType != this.GasType) && gasPoison.GasLevel == this.GasLevel && base.SameAs(p);
		}

		// Token: 0x0600A09A RID: 41114 RVA: 0x003A75D4 File Offset: 0x003A57D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600A09B RID: 41115 RVA: 0x003A75F4 File Offset: 0x003A57F4
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (this.IsAffectable(E.Actor, null))
					{
						E.MinWeight(base.GasDensityStepped(5) / 2 + this.GasLevel * 10, Math.Min(50 + this.GasLevel * 10, 80));
					}
				}
				else
				{
					E.MinWeight(8);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A09C RID: 41116 RVA: 0x003A767C File Offset: 0x003A587C
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (this.IsAffectable(E.Actor, null))
					{
						E.MinWeight(base.GasDensityStepped(5) / 10 + this.GasLevel * 2, Math.Min(10 + this.GasLevel * 2, 16));
					}
				}
				else
				{
					E.MinWeight(2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A09D RID: 41117 RVA: 0x003A7700 File Offset: 0x003A5900
		public bool IsAffectable(GameObject Object, Gas Gas = null)
		{
			return CheckGasCanAffectEvent.Check(Object, this.ParentObject, Gas) && (Object == null || (Object.FireEvent("CanApplyPoisonGasPoison") && CanApplyEffectEvent.Check<PoisonGasPoison>(Object, 0) && Object.PhaseMatches(this.ParentObject)));
		}

		// Token: 0x0600A09E RID: 41118 RVA: 0x003A773C File Offset: 0x003A593C
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
			if (!Object.HasTag("Creature"))
			{
				return false;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!this.IsAffectable(Object, part))
			{
				return false;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(Object, this.ParentObject, part, null, 0, 0, false);
			if (@for <= 0)
			{
				return false;
			}
			Object.RemoveEffect<PoisonGasPoison>();
			Object.ApplyEffect(new PoisonGasPoison(Stat.Random(1, 10), part.Creator)
			{
				Damage = this.GasLevel * 2
			}, null);
			int num = (int)Math.Max(Math.Floor((double)(@for + 1) / 20.0), 1.0);
			return Object.TakeDamage(num, "from %t {{g|poison}}!", "InhaleDanger Poison Gas", null, null, null, part.Creator, null, null, null, false, true, false, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x040042BA RID: 17082
		public string GasType = "Poison";

		// Token: 0x040042BB RID: 17083
		public int GasLevel = 1;
	}
}
