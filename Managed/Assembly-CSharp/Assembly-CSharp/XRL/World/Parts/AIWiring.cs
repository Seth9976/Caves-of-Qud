using System;
using Genkit;
using XRL.Collections;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E36 RID: 3638
	[Serializable]
	public class AIWiring : AIBehaviorPart
	{
		// Token: 0x060093AA RID: 37802 RVA: 0x003528B9 File Offset: 0x00350AB9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIBoredEvent>.ID || ID == TookEvent.ID || ID == DroppedEvent.ID || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID;
		}

		// Token: 0x060093AB RID: 37803 RVA: 0x003528E8 File Offset: 0x00350AE8
		public override bool HandleEvent(AIBoredEvent E)
		{
			int wait = this.Wait;
			this.Wait = wait - 1;
			if (wait <= 0)
			{
				this.Wait = 10;
				try
				{
					if (this.QueueAction())
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("AIWiring", ex, "game_exception");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093AC RID: 37804 RVA: 0x0035294C File Offset: 0x00350B4C
		public bool QueueAction()
		{
			if (this.Items == -1)
			{
				this.Items = this.ParentObject.Inventory.Count(this.Blueprint);
			}
			if (this.Items == 0 || !this.ParentObject.IsMobile())
			{
				return false;
			}
			if (this.Goal != null && this.ParentObject.Brain.Goals.Contains(this.Goal))
			{
				return false;
			}
			ElectricalPowerTransmission electricalPowerTransmission = null;
			ElectricalPowerTransmission electricalPowerTransmission2 = null;
			foreach (Cell cell in this.ParentObject.CurrentCell.IterateAdjacent(this.Radius, false, true, true))
			{
				using (Container<GameObject>.Enumerator enumerator2 = cell.Objects.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						ElectricalPowerTransmission electricalPowerTransmission3;
						if (enumerator2.Current.TryGetPart<ElectricalPowerTransmission>(out electricalPowerTransmission3))
						{
							if (electricalPowerTransmission3.IsConsumer && !electricalPowerTransmission3.AnyCharge(0L, false))
							{
								electricalPowerTransmission = electricalPowerTransmission3;
								if (electricalPowerTransmission2 != null)
								{
									return this.FindPath(electricalPowerTransmission, electricalPowerTransmission2);
								}
							}
							else if (electricalPowerTransmission3.IsProducer && electricalPowerTransmission3.AnyCharge(0L, false))
							{
								electricalPowerTransmission2 = electricalPowerTransmission3;
								if (electricalPowerTransmission != null)
								{
									return this.FindPath(electricalPowerTransmission, electricalPowerTransmission2);
								}
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x060093AD RID: 37805 RVA: 0x00352A94 File Offset: 0x00350C94
		public bool FindPath(ElectricalPowerTransmission Consumer, ElectricalPowerTransmission Producer)
		{
			if (Consumer == null || Producer == null)
			{
				return false;
			}
			long gridBit = Consumer.GetGridBit();
			if (gridBit != 0L && Producer.GetGridBit() == gridBit)
			{
				return false;
			}
			GameObject gameObject = null;
			GameObject gameObject2 = null;
			int num = int.MaxValue;
			foreach (GameObject gameObject3 in Consumer.GetGrid())
			{
				foreach (GameObject gameObject4 in Producer.GetGrid())
				{
					int num2 = gameObject3.DistanceTo(gameObject4);
					if (num2 < num)
					{
						gameObject = gameObject3;
						gameObject2 = gameObject4;
						num = num2;
					}
				}
			}
			if (gameObject == null || gameObject2 == null)
			{
				return false;
			}
			if (this.Pathfinder == null)
			{
				this.Pathfinder = new Pathfinder(80, 25);
			}
			if (this.Pathfinder.FindPath(gameObject.CurrentCell.Location, gameObject2.CurrentCell.Location, false, true, 50, false))
			{
				this.Step = 0;
				this.Pathfinder.Directions.Reverse();
				this.Pathfinder.Steps.Reverse();
				if (this.Goal == null)
				{
					this.Goal = new DelegateGoal(new Action<GoalHandler>(this.TakeAction), null, new Action<GoalHandler>(this.Failed));
				}
				this.ParentObject.Brain.PushGoal(this.Goal);
				return true;
			}
			return false;
		}

		// Token: 0x060093AE RID: 37806 RVA: 0x00352C14 File Offset: 0x00350E14
		public void TakeAction(GoalHandler Handler)
		{
			if (!this.ParentObject.IsMobile())
			{
				Handler.FailToParent();
				return;
			}
			int count = this.Pathfinder.Steps.Count;
			if (this.Step >= count)
			{
				Handler.Pop();
				return;
			}
			Cell cell = this.ParentObject.CurrentZone.GetCell(this.Pathfinder.Steps[this.Step].pos);
			if (cell.HasObject((GameObject x) => x.HasPart<ElectricalPowerTransmission>()))
			{
				this.Step++;
				return;
			}
			if (this.ParentObject.DistanceTo(cell) > 1)
			{
				Handler.PushChildGoal(new MoveTo(cell, false, false, 1, false, false, false, -1, -1, 95));
				return;
			}
			GameObject gameObject = this.ParentObject.Inventory.FindObjectByBlueprint(this.Blueprint);
			DeployableInfrastructure deployableInfrastructure;
			if (gameObject == null || !gameObject.TryGetPart<DeployableInfrastructure>(out deployableInfrastructure) || !deployableInfrastructure.DeployOne(this.ParentObject, cell, true, true))
			{
				Handler.FailToParent();
				return;
			}
			this.Step++;
			this.ParentObject.ForfeitTurn(false);
		}

		// Token: 0x060093AF RID: 37807 RVA: 0x00352D34 File Offset: 0x00350F34
		public void Failed(GoalHandler Handler)
		{
			Handler.FailToParent();
		}

		// Token: 0x060093B0 RID: 37808 RVA: 0x00352D3C File Offset: 0x00350F3C
		public override bool HandleEvent(TookEvent E)
		{
			this.Items = -1;
			return base.HandleEvent(E);
		}

		// Token: 0x060093B1 RID: 37809 RVA: 0x00352D4C File Offset: 0x00350F4C
		public override bool HandleEvent(DroppedEvent E)
		{
			this.Items = -1;
			return base.HandleEvent(E);
		}

		// Token: 0x060093B2 RID: 37810 RVA: 0x00352D5C File Offset: 0x00350F5C
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (this.Goal != null && E.Speaker == this.ParentObject && E.Speaker.Brain.Goals.Contains(this.Goal))
			{
				E.Message = E.Speaker.Does("ignore", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".";
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003D10 RID: 15632
		public string Blueprint = "Wire Strand";

		// Token: 0x04003D11 RID: 15633
		public int Radius = 5;

		// Token: 0x04003D12 RID: 15634
		[NonSerialized]
		private Pathfinder Pathfinder;

		// Token: 0x04003D13 RID: 15635
		[NonSerialized]
		private DelegateGoal Goal;

		// Token: 0x04003D14 RID: 15636
		[NonSerialized]
		private int Items = -1;

		// Token: 0x04003D15 RID: 15637
		[NonSerialized]
		private int Step;

		// Token: 0x04003D16 RID: 15638
		[NonSerialized]
		private int Wait;
	}
}
