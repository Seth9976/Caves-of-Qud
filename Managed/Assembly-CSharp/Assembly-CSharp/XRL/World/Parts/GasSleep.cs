using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC9 RID: 4041
	[Serializable]
	public class GasSleep : IGasBehavior
	{
		// Token: 0x0600A0AE RID: 41134 RVA: 0x003A7B70 File Offset: 0x003A5D70
		public override bool SameAs(IPart p)
		{
			return !((p as GasSleep).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A0AF RID: 41135 RVA: 0x003A7B93 File Offset: 0x003A5D93
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A0B0 RID: 41136 RVA: 0x003A7BBC File Offset: 0x003A5DBC
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
						int num = part.Level * 5;
						E.MinWeight(base.StepValue(part.Density, 5) / 2 + num, Math.Min(50 + num, 70));
					}
				}
				else
				{
					E.MinWeight(2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0B1 RID: 41137 RVA: 0x003A7C4C File Offset: 0x003A5E4C
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
						int num = part.Level * 5;
						E.MinWeight(base.StepValue(part.Density, 5) / 10 + num / 5, Math.Min(10 + num / 5, 14));
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0B2 RID: 41138 RVA: 0x003A7CE1 File Offset: 0x003A5EE1
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ApplySleep(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0B3 RID: 41139 RVA: 0x003A7CF7 File Offset: 0x003A5EF7
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A0B4 RID: 41140 RVA: 0x003A7CFA File Offset: 0x003A5EFA
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ApplySleep();
		}

		// Token: 0x0600A0B5 RID: 41141 RVA: 0x003A7D02 File Offset: 0x003A5F02
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A0B6 RID: 41142 RVA: 0x003A7D18 File Offset: 0x003A5F18
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A0B7 RID: 41143 RVA: 0x003A7D6C File Offset: 0x003A5F6C
		public void ApplySleep()
		{
			this.ApplySleep(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600A0B8 RID: 41144 RVA: 0x003A7D80 File Offset: 0x003A5F80
		public void ApplySleep(Cell C)
		{
			if (C != null)
			{
				for (int i = 0; i < C.Objects.Count; i++)
				{
					this.ApplySleep(C.Objects[i]);
				}
			}
		}

		// Token: 0x0600A0B9 RID: 41145 RVA: 0x003A7DBC File Offset: 0x003A5FBC
		public bool IsAffectable(GameObject Object, Gas Gas = null)
		{
			return CheckGasCanAffectEvent.Check(Object, this.ParentObject, Gas) && (Object == null || (Object.FireEvent("CanApplySleep") && Object.FireEvent("CanApplySleegas") && Object.FireEvent("CanApplyInvoluntarySleep") && CanApplyEffectEvent.Check(Object, "Sleep", 0) && Object.PhaseMatches(this.ParentObject)));
		}

		// Token: 0x0600A0BA RID: 41146 RVA: 0x003A7E24 File Offset: 0x003A6024
		public bool ApplySleep(GameObject Object)
		{
			if (Object == this.ParentObject)
			{
				return false;
			}
			if (Object.Brain == null)
			{
				return false;
			}
			if (!Object.Respires)
			{
				return false;
			}
			if (Object.HasEffect<Asleep>())
			{
				return false;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!this.IsAffectable(Object, part))
			{
				return false;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(Object, this.ParentObject, part, null, 0, 0, true);
			return @for > 0 && !Object.MakeSave("Toughness", 5 + part.Level + @for / 10, null, null, "Sleep Inhaled Gas", false, false, false, false, this.ParentObject) && Object.ApplyEffect(new Asleep("4d6".RollCached() + part.Level, false, false, false, true), null);
		}

		// Token: 0x040042BD RID: 17085
		public string GasType = "Sleep";
	}
}
