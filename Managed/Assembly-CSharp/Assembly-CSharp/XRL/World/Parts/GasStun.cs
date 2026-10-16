using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FCB RID: 4043
	[Serializable]
	public class GasStun : IGasBehavior
	{
		// Token: 0x0600A0C6 RID: 41158 RVA: 0x003A826F File Offset: 0x003A646F
		public override bool SameAs(IPart p)
		{
			return !((p as GasStun).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A0C7 RID: 41159 RVA: 0x003A8292 File Offset: 0x003A6492
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A0C8 RID: 41160 RVA: 0x003A82C0 File Offset: 0x003A64C0
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
						E.MinWeight(base.StepValue(part.Density, 5) / 2 + 1, 51);
					}
				}
				else
				{
					E.MinWeight(2);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0C9 RID: 41161 RVA: 0x003A8340 File Offset: 0x003A6540
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
						E.MinWeight(base.StepValue(part.Density, 5) / 10, 10);
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0CA RID: 41162 RVA: 0x003A83BD File Offset: 0x003A65BD
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject && E.Object.PhaseMatches(this.ParentObject))
			{
				this.ApplyStun(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0CB RID: 41163 RVA: 0x003A83F4 File Offset: 0x003A65F4
		public override bool HandleEvent(EndTurnEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				int i = 0;
				int count = currentCell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = currentCell.Objects[i];
					if (gameObject != this.ParentObject && gameObject.Brain != null && this.ParentObject.PhaseMatches(gameObject))
					{
						this.ApplyStun(gameObject);
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0CC RID: 41164 RVA: 0x003A8462 File Offset: 0x003A6662
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A0CD RID: 41165 RVA: 0x003A8477 File Offset: 0x003A6677
		public bool IsAffectable(GameObject Object, Gas Gas = null)
		{
			return CheckGasCanAffectEvent.Check(Object, this.ParentObject, Gas) && (Object == null || (Object.FireEvent("CanApplyStunGasStun") && CanApplyEffectEvent.Check<StunGasStun>(Object, 0) && Object.PhaseMatches(this.ParentObject)));
		}

		// Token: 0x0600A0CE RID: 41166 RVA: 0x003A84B4 File Offset: 0x003A66B4
		public void ApplyStun(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return;
			}
			if (!Object.Respires)
			{
				return;
			}
			if (!this.IsAffectable(Object, null))
			{
				return;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(Object, this.ParentObject, null, null, 0, 0, false);
			if (@for <= 0)
			{
				return;
			}
			Object.RemoveEffect<StunGasStun>();
			Object.ApplyEffect(new StunGasStun(@for), null);
		}

		// Token: 0x0600A0CF RID: 41167 RVA: 0x003A8514 File Offset: 0x003A6714
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040042BF RID: 17087
		public string GasType = "Stun";
	}
}
