using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E35 RID: 3637
	[Serializable]
	public class AIWanderingJuggernaut : AIBehaviorPart
	{
		// Token: 0x060093A2 RID: 37794 RVA: 0x0035266C File Offset: 0x0035086C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x060093A3 RID: 37795 RVA: 0x0035268A File Offset: 0x0035088A
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			if (this.Active)
			{
				this.WanderJuggernautStyle();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060093A4 RID: 37796 RVA: 0x003526A1 File Offset: 0x003508A1
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.Angle = Stat.Random(0, 359);
			return base.HandleEvent(E);
		}

		// Token: 0x060093A5 RID: 37797 RVA: 0x003526BB File Offset: 0x003508BB
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060093A6 RID: 37798 RVA: 0x003526BE File Offset: 0x003508BE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
			base.Register(Object, Registrar);
		}

		// Token: 0x060093A7 RID: 37799 RVA: 0x003526D3 File Offset: 0x003508D3
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.Active = false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x060093A8 RID: 37800 RVA: 0x003526F8 File Offset: 0x003508F8
		private void WanderJuggernautStyle()
		{
			if (this.ParentObject.IsPlayer())
			{
				return;
			}
			if (this.ParentObject.Brain == null)
			{
				return;
			}
			if (this.ParentObject.PartyLeader != null)
			{
				return;
			}
			if (!this.ParentObject.FireEvent("CanAIDoIndependentBehavior"))
			{
				return;
			}
			if (this.ParentObject.IsFleeing())
			{
				return;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			Cell cell = this.ParentObject.MovingTo();
			GameObject target = this.ParentObject.Target;
			if (target == null)
			{
				if (cell == null)
				{
					List<string> list = Directions.DirectionsFromAngle(this.Angle, 64);
					int num = 0;
					while (currentCell.GetCellFromDirection(list[0], false) == null && ++num < 100)
					{
						this.Angle = Stat.Random(0, 359);
						list = Directions.DirectionsFromAngle(this.Angle, 64);
					}
					for (int i = list.Count - 1; i >= 0; i--)
					{
						GameObject parentObject = this.ParentObject;
						if (parentObject != null)
						{
							Brain brain = parentObject.Brain;
							if (brain != null)
							{
								brain.PushGoal(new Step(list[i], false, false, true, true, null, true));
							}
						}
					}
				}
				return;
			}
			if (!this.ParentObject.HasPart<Consumer>())
			{
				return;
			}
			Cell currentCell2 = target.CurrentCell;
			if (currentCell2 == null)
			{
				return;
			}
			if (currentCell == currentCell2)
			{
				this.ParentObject.Brain.PushGoal(new FleeLocation(currentCell, 1, false, false));
				return;
			}
			if (cell == currentCell2)
			{
				return;
			}
			if (currentCell.IsAdjacentTo(currentCell2, true))
			{
				string directionFromCell = currentCell.GetDirectionFromCell(currentCell2, false);
				if (directionFromCell != null)
				{
					this.ParentObject.Target = null;
					this.ParentObject.Target = target;
					this.ParentObject.Brain.PushGoal(new Step(directionFromCell, false, true, false, true, target, false));
					return;
				}
			}
		}

		// Token: 0x04003D0E RID: 15630
		public int Angle;

		// Token: 0x04003D0F RID: 15631
		public bool Active = true;
	}
}
