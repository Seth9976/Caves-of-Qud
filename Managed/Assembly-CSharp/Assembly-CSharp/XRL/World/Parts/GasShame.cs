using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02000FC8 RID: 4040
	[Serializable]
	public class GasShame : IGasBehavior
	{
		// Token: 0x0600A0A0 RID: 41120 RVA: 0x003A783D File Offset: 0x003A5A3D
		public override bool SameAs(IPart p)
		{
			return !((p as GasShame).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A0A1 RID: 41121 RVA: 0x003A7860 File Offset: 0x003A5A60
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A0A2 RID: 41122 RVA: 0x003A7888 File Offset: 0x003A5A88
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
						E.MinWeight(base.StepValue(part.Density, 5) / 5 + 5, 60);
					}
				}
				else
				{
					E.MinWeight(2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0A3 RID: 41123 RVA: 0x003A7908 File Offset: 0x003A5B08
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
						E.MinWeight(base.StepValue(part.Density, 5) / 25 + 1, 12);
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0A4 RID: 41124 RVA: 0x003A7987 File Offset: 0x003A5B87
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ApplyShame(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0A5 RID: 41125 RVA: 0x003A799C File Offset: 0x003A5B9C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A0A6 RID: 41126 RVA: 0x003A799F File Offset: 0x003A5B9F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ApplyShame();
		}

		// Token: 0x0600A0A7 RID: 41127 RVA: 0x003A79A7 File Offset: 0x003A5BA7
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A0A8 RID: 41128 RVA: 0x003A79BC File Offset: 0x003A5BBC
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A0A9 RID: 41129 RVA: 0x003A7A10 File Offset: 0x003A5C10
		public void ApplyShame()
		{
			this.ApplyShame(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600A0AA RID: 41130 RVA: 0x003A7A24 File Offset: 0x003A5C24
		public void ApplyShame(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					this.ApplyShame(C.Objects[i]);
					i++;
				}
			}
		}

		// Token: 0x0600A0AB RID: 41131 RVA: 0x003A7A60 File Offset: 0x003A5C60
		public bool IsAffectable(GameObject Object, Gas Gas = null)
		{
			return CheckGasCanAffectEvent.Check(Object, this.ParentObject, Gas) && (Object == null || (Object.FireEvent("CanApplyShamed") && Object.FireEvent("CanApplyShameGas") && CanApplyEffectEvent.Check<Shamed>(Object, 0) && Object.PhaseMatches(this.ParentObject)));
		}

		// Token: 0x0600A0AC RID: 41132 RVA: 0x003A7AB4 File Offset: 0x003A5CB4
		public void ApplyShame(GameObject GO)
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
			if (@for > 0 && !GO.MakeSave("Willpower", 5 + part.Level + @for / 10, null, null, "Shame Inhaled Gas", false, false, false, false, this.ParentObject))
			{
				GO.ApplyEffect(new Shamed("2d6".RollCached() + part.Level * 2), null);
			}
		}

		// Token: 0x040042BC RID: 17084
		public string GasType = "Shame";
	}
}
