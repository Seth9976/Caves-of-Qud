using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC6 RID: 4038
	[Serializable]
	public class GasPlasma : IObjectGasBehavior
	{
		// Token: 0x0600A093 RID: 41107 RVA: 0x003A7308 File Offset: 0x003A5508
		public override bool SameAs(IPart p)
		{
			GasPlasma gasPlasma = p as GasPlasma;
			return !(gasPlasma.GasType != this.GasType) && gasPlasma.GasLevel == this.GasLevel && base.SameAs(p);
		}

		// Token: 0x0600A094 RID: 41108 RVA: 0x003A7348 File Offset: 0x003A5548
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600A095 RID: 41109 RVA: 0x003A7368 File Offset: 0x003A5568
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyCoatedInPlasma") && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 3 + 40, 90);
					}
				}
				else
				{
					E.MinWeight(70);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A096 RID: 41110 RVA: 0x003A7400 File Offset: 0x003A5600
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyCoatedInPlasma") && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 15 + 8, 18);
					}
				}
				else
				{
					E.MinWeight(14);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A097 RID: 41111 RVA: 0x003A7498 File Offset: 0x003A5698
		public override bool ApplyGas(GameObject Object)
		{
			if (Object == this.ParentObject)
			{
				return false;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!CheckGasCanAffectEvent.Check(Object, this.ParentObject, part))
			{
				return false;
			}
			if (!Object.FireEvent("CanApplyCoatedInPlasma"))
			{
				return false;
			}
			if (!CanApplyEffectEvent.Check<CoatedInPlasma>(Object, 0))
			{
				return false;
			}
			if (!Object.PhaseMatches(this.ParentObject))
			{
				return false;
			}
			if (part.Density <= 0)
			{
				return false;
			}
			int num = Stat.Random(part.Density * 2 / 5, part.Density * 3 / 5);
			if (num <= 0)
			{
				return false;
			}
			CoatedInPlasma coatedInPlasma = Object.GetEffect<CoatedInPlasma>();
			if (coatedInPlasma != null)
			{
				if (coatedInPlasma.Duration < num)
				{
					coatedInPlasma.Duration = num;
				}
				if (!GameObject.Validate(ref coatedInPlasma.Owner) && GameObject.Validate(part.Creator))
				{
					coatedInPlasma.Owner = part.Creator;
				}
				return true;
			}
			coatedInPlasma = new CoatedInPlasma(num, part.Creator);
			return Object.ApplyEffect(coatedInPlasma, null);
		}

		// Token: 0x040042B8 RID: 17080
		public string GasType = "Plasma";

		// Token: 0x040042B9 RID: 17081
		public int GasLevel = 1;
	}
}
