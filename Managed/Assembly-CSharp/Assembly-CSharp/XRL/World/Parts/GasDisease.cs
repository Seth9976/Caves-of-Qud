using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC3 RID: 4035
	[Serializable]
	public class GasDisease : IGasBehavior
	{
		// Token: 0x0600A07C RID: 41084 RVA: 0x003A6A64 File Offset: 0x003A4C64
		public override bool SameAs(IPart p)
		{
			return !((p as GasDisease).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A07D RID: 41085 RVA: 0x003A6A87 File Offset: 0x003A4C87
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A07E RID: 41086 RVA: 0x003A6AB0 File Offset: 0x003A4CB0
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyDisease") && CanApplyEffectEvent.Check(E.Actor, "Disease", 0) && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 2 + 20, 60);
					}
				}
				else
				{
					E.MinWeight(3);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A07F RID: 41087 RVA: 0x003A6B6C File Offset: 0x003A4D6C
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && !E.Unbreathing && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || (E.Actor.FireEvent("CanApplyDisease") && CanApplyEffectEvent.Check(E.Actor, "Disease", 0) && E.Actor.PhaseMatches(this.ParentObject))))
					{
						E.MinWeight(base.GasDensityStepped(5) / 10 + 4, 12);
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A080 RID: 41088 RVA: 0x003A6C27 File Offset: 0x003A4E27
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			this.ApplyDisease(E.Object);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A081 RID: 41089 RVA: 0x003A6C3C File Offset: 0x003A4E3C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A082 RID: 41090 RVA: 0x003A6C3F File Offset: 0x003A4E3F
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ApplyDisease();
		}

		// Token: 0x0600A083 RID: 41091 RVA: 0x003A6C47 File Offset: 0x003A4E47
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A084 RID: 41092 RVA: 0x003A6C5C File Offset: 0x003A4E5C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A085 RID: 41093 RVA: 0x003A6CB0 File Offset: 0x003A4EB0
		public void ApplyDisease()
		{
			this.ApplyDisease(this.ParentObject.CurrentCell);
		}

		// Token: 0x0600A086 RID: 41094 RVA: 0x003A6CC4 File Offset: 0x003A4EC4
		public void ApplyDisease(Cell C)
		{
			if (C != null)
			{
				int i = 0;
				int count = C.Objects.Count;
				while (i < count)
				{
					this.ApplyDisease(C.Objects[i]);
					i++;
				}
			}
		}

		// Token: 0x0600A087 RID: 41095 RVA: 0x003A6D00 File Offset: 0x003A4F00
		public void ApplyDisease(GameObject GO)
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
			if (!CheckGasCanAffectEvent.Check(GO, this.ParentObject, part))
			{
				return;
			}
			if (!GO.FireEvent("CanApplyDisease"))
			{
				return;
			}
			if (!CanApplyEffectEvent.Check(GO, "Disease", 0))
			{
				return;
			}
			if (!GO.PhaseMatches(this.ParentObject))
			{
				return;
			}
			int @for = GetRespiratoryAgentPerformanceEvent.GetFor(GO, this.ParentObject, part, null, 0, 0, true);
			if (@for > 0 && !GO.MakeSave("Toughness", 5 + part.Level + @for / 10, null, null, "Disease Inhaled Gas", false, false, false, false, this.ParentObject))
			{
				bool flag;
				if (GO.CurrentZone.Z % 2 == 0)
				{
					flag = GO.ApplyEffect(new IronshankOnset(), null);
				}
				else
				{
					flag = GO.ApplyEffect(new GlotrotOnset(), null);
				}
				if (flag && GO.IsPlayer())
				{
					Popup.Show("You feel sick.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x040042B3 RID: 17075
		public string GasType = "Disease";
	}
}
