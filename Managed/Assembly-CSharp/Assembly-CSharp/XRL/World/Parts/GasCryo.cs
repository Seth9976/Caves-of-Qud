using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000FC1 RID: 4033
	[Serializable]
	public class GasCryo : IGasBehavior
	{
		// Token: 0x0600A05D RID: 41053 RVA: 0x003A5A7E File Offset: 0x003A3C7E
		public override bool SameAs(IPart p)
		{
			return !((p as GasCryo).GasType != this.GasType) && base.SameAs(p);
		}

		// Token: 0x0600A05E RID: 41054 RVA: 0x003A5AA1 File Offset: 0x003A3CA1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A05F RID: 41055 RVA: 0x003A5AD0 File Offset: 0x003A3CD0
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || E.Actor.PhaseMatches(this.ParentObject)))
					{
						int num = base.GasDensityStepped(5) / 2 + 3;
						if (E.Actor != null)
						{
							int num2 = E.Actor.Stat("ColdResistance", 0);
							if (num2 != 0)
							{
								num = Math.Max(num * (100 - num2) / 100, 0);
							}
						}
						if (num > 0)
						{
							E.MinWeight(num, 53);
						}
					}
				}
				else
				{
					E.MinWeight(3);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A060 RID: 41056 RVA: 0x003A5B90 File Offset: 0x003A3D90
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && (E.Actor == null || E.Actor.PhaseMatches(this.ParentObject)))
					{
						int num = base.GasDensityStepped(5) / 10 + 1;
						if (E.Actor != null)
						{
							int num2 = E.Actor.Stat("ColdResistance", 0);
							if (num2 != 0)
							{
								num = Math.Max(num * (100 - num2) / 100, 0);
							}
						}
						if (num > 0)
						{
							E.MinWeight(num, 11);
						}
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A061 RID: 41057 RVA: 0x003A5C50 File Offset: 0x003A3E50
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
						this.ApplyCryo(gameObject);
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A062 RID: 41058 RVA: 0x003A5CB6 File Offset: 0x003A3EB6
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject && E.Type != "Thrown")
			{
				this.ApplyCryo(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A063 RID: 41059 RVA: 0x003A5CEB File Offset: 0x003A3EEB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A064 RID: 41060 RVA: 0x003A5D00 File Offset: 0x003A3F00
		public void ApplyCryo(GameObject GO)
		{
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!CheckGasCanAffectEvent.Check(GO, this.ParentObject, part))
			{
				return;
			}
			if (!GO.PhaseMatches(this.ParentObject))
			{
				return;
			}
			Event.PinCurrentPool();
			int num = (int)Math.Ceiling((double)(2.5f * (float)part.Density));
			if (GO.Physics.Temperature > -num)
			{
				GO.TemperatureChange(-num, this.ParentObject, false, false, false, false, this.ParentObject.GetPhase(), null, null);
			}
			if (GO.IsPlayer() || !GO.IsFrozen())
			{
				GO.TakeDamage(1, "from the {{icy|cryogenic mist}}.", "Cold", null, null, part.Creator, null, this.ParentObject, null, null, false, true, false, false, false, false, false, false, false, 5, null);
			}
			Event.ResetToPin();
		}

		// Token: 0x0600A065 RID: 41061 RVA: 0x003A5DD4 File Offset: 0x003A3FD4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040042A2 RID: 17058
		public string GasType = "Cryo";
	}
}
