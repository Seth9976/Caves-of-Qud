using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC0 RID: 4032
	[Serializable]
	public class GasConfusion : IGasBehavior
	{
		// Token: 0x0600A04F RID: 41039 RVA: 0x003A5732 File Offset: 0x003A3932
		public override bool SameAs(IPart p)
		{
			return !((p as GasConfusion).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A050 RID: 41040 RVA: 0x003A5755 File Offset: 0x003A3955
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A051 RID: 41041 RVA: 0x003A577C File Offset: 0x003A397C
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					Gas part = this.ParentObject.GetPart<Gas>();
					if (this.IsAffectable(E.Actor, part))
					{
						E.MinWeight(base.StepValue(part.Density, 5) / 2 + 20, 60);
					}
				}
				else
				{
					E.MinWeight(3);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A052 RID: 41042 RVA: 0x003A57FC File Offset: 0x003A39FC
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					Gas part = this.ParentObject.GetPart<Gas>();
					if (this.IsAffectable(E.Actor, part))
					{
						E.MinWeight(base.StepValue(part.Density, 5) / 10 + 4, 12);
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A053 RID: 41043 RVA: 0x003A587B File Offset: 0x003A3A7B
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ApplyConfusion(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A054 RID: 41044 RVA: 0x003A5890 File Offset: 0x003A3A90
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A055 RID: 41045 RVA: 0x003A5893 File Offset: 0x003A3A93
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ApplyConfusion();
		}

		// Token: 0x0600A056 RID: 41046 RVA: 0x003A589B File Offset: 0x003A3A9B
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A057 RID: 41047 RVA: 0x003A58B0 File Offset: 0x003A3AB0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A058 RID: 41048 RVA: 0x003A5904 File Offset: 0x003A3B04
		public void ApplyConfusion()
		{
			this.ApplyConfusion(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600A059 RID: 41049 RVA: 0x003A5918 File Offset: 0x003A3B18
		public void ApplyConfusion(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					this.ApplyConfusion(C.Objects[i]);
					i++;
				}
			}
		}

		// Token: 0x0600A05A RID: 41050 RVA: 0x003A5954 File Offset: 0x003A3B54
		public bool IsAffectable(GameObject Object, Gas Gas = null)
		{
			return Object != null && CheckGasCanAffectEvent.Check(Object, this.ParentObject, Gas) && (Object.FireEvent("CanApplyConfusion") && Object.FireEvent("CanApplyConfusionGas") && CanApplyEffectEvent.Check(Object, "Confusion", 0)) && Object.PhaseMatches(this.ParentObject);
		}

		// Token: 0x0600A05B RID: 41051 RVA: 0x003A59B0 File Offset: 0x003A3BB0
		public void ApplyConfusion(GameObject GO)
		{
			if (GO == this.ParentObject)
			{
				return;
			}
			if (GO.Brain == null)
			{
				return;
			}
			if (!GO.Respires)
			{
				return;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!this.IsAffectable(GO, part))
			{
				return;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(GO, this.ParentObject, part, null, 0, 0, true);
			if (@for > 0 && !GO.MakeSave("Toughness", 5 + part.Level + @for / 10, null, null, "Confusion Inhaled Gas", false, false, false, false, this.ParentObject))
			{
				GO.ApplyEffect(new Confused(Stat.Roll("4d6", null) + part.Level, part.Level, part.Level + 2, "ToxicConfusion"), null);
			}
		}

		// Token: 0x040042A1 RID: 17057
		public string GasType = "Confusion";
	}
}
