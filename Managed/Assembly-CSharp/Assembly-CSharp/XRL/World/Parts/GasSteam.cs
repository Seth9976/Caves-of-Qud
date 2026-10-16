using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000FCA RID: 4042
	[Serializable]
	public class GasSteam : IGasBehavior
	{
		// Token: 0x0600A0BC RID: 41148 RVA: 0x003A7EF1 File Offset: 0x003A60F1
		public override bool SameAs(IPart p)
		{
			return !((p as GasSteam).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A0BD RID: 41149 RVA: 0x003A7F14 File Offset: 0x003A6114
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A0BE RID: 41150 RVA: 0x003A7F4C File Offset: 0x003A614C
		public override bool HandleEvent(EndTurnEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				List<GameObject> list = Event.NewGameObjectList(currentCell.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					GameObject gameObject = list[i];
					if (gameObject != this.ParentObject && !gameObject.IsScenery)
					{
						this.ApplySteam(gameObject);
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0BF RID: 41151 RVA: 0x003A7FB4 File Offset: 0x003A61B4
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || E.Actor.PhaseMatches(this.ParentObject)))
					{
						int num = base.GasDensityStepped(5) / 2 + 15;
						if (E.Actor != null)
						{
							int num2 = E.Actor.Stat("HeatResistance", 0);
							if (num2 != 0)
							{
								num = num * (100 - num2) / 100;
							}
						}
						E.MinWeight(num, 65);
					}
				}
				else
				{
					E.MinWeight(5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0C0 RID: 41152 RVA: 0x003A8068 File Offset: 0x003A6268
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || E.Actor.PhaseMatches(this.ParentObject)))
					{
						int num = base.GasDensityStepped(5) / 10 + 3;
						if (E.Actor != null)
						{
							int num2 = E.Actor.Stat("HeatResistance", 0);
							if (num2 != 0)
							{
								num = num * (100 - num2) / 100;
							}
						}
						E.MinWeight(num, 13);
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0C1 RID: 41153 RVA: 0x003A811C File Offset: 0x003A631C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Type != "Thrown")
			{
				this.ApplySteam(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A0C2 RID: 41154 RVA: 0x003A8143 File Offset: 0x003A6343
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A0C3 RID: 41155 RVA: 0x003A8158 File Offset: 0x003A6358
		public void ApplySteam(GameObject Object)
		{
			if (!GameObject.Validate(ref Object))
			{
				return;
			}
			if (Object == this.ParentObject)
			{
				return;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!CheckGasCanAffectEvent.Check(Object, this.ParentObject, part))
			{
				return;
			}
			if (!Object.PhaseAndFlightMatches(this.ParentObject))
			{
				return;
			}
			if (!Object.IsOrganic)
			{
				return;
			}
			if (!Object.IsCreature && !Object.HasPart<Food>())
			{
				return;
			}
			Object.TakeDamage((int)Math.Max(Math.Ceiling((double)(0.18f * (float)part.Density)), 1.0), "from %t scalding steam!", "Heat Steam NoBurn", null, null, null, part.Creator, null, null, null, false, true, false, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x0600A0C4 RID: 41156 RVA: 0x003A8208 File Offset: 0x003A6408
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040042BE RID: 17086
		public string GasType = "Steam";
	}
}
