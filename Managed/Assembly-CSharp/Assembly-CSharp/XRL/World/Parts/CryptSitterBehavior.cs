using System;
using System.Linq;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000E6E RID: 3694
	[Serializable]
	public class CryptSitterBehavior : IPart
	{
		// Token: 0x06009541 RID: 38209 RVA: 0x0035FDC4 File Offset: 0x0035DFC4
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009542 RID: 38210 RVA: 0x0035FDC7 File Offset: 0x0035DFC7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009543 RID: 38211 RVA: 0x0035FDCA File Offset: 0x0035DFCA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x06009544 RID: 38212 RVA: 0x0035FDE8 File Offset: 0x0035DFE8
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			Brain brain = this.ParentObject.Brain;
			if (brain.PartyLeader != null)
			{
				this.ParentObject.RemovePart(this);
				return base.HandleEvent(E);
			}
			if (this.ParentObject.IsPlayer())
			{
				return base.HandleEvent(E);
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			Brain brain2 = brain;
			GlobalLocation globalLocation;
			if ((globalLocation = brain2.StartingCell) == null)
			{
				globalLocation = (brain2.StartingCell = currentCell.GetGlobalLocation());
			}
			GlobalLocation globalLocation2 = globalLocation;
			if (this.Alerted)
			{
				if (globalLocation2.PathDistanceTo(currentCell) > 10)
				{
					this.Unalert();
					brain.Goals.Clear();
					brain.PushGoal(new MoveTo(globalLocation2, false, false, 0, false, false, false, -1, -1, 95));
				}
				return base.HandleEvent(E);
			}
			if (!globalLocation2.Equals(currentCell))
			{
				if (!brain.Goals.Items.Any<GoalHandler>((GoalHandler g) => g is IMovementGoal))
				{
					brain.Goals.Clear();
					brain.PushGoal(new MoveTo(globalLocation2, false, false, 0, false, false, false, -1, -1, 95));
				}
				return true;
			}
			this.ParentObject.UseEnergy(1000);
			return false;
		}

		// Token: 0x06009545 RID: 38213 RVA: 0x0035FF0E File Offset: 0x0035E10E
		public override bool HandleEvent(GetFeelingEvent E)
		{
			if (!this.Alerted)
			{
				E.Feeling = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009546 RID: 38214 RVA: 0x0035FF28 File Offset: 0x0035E128
		public override bool HandleEvent(TookDamageEvent E)
		{
			this.Alert();
			return base.HandleEvent(E);
		}

		// Token: 0x06009547 RID: 38215 RVA: 0x0035FF37 File Offset: 0x0035E137
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AICryptHelpBroadcast");
			Registrar.Register(PooledEvent<GetFeelingEvent>.ID, 0, false);
		}

		// Token: 0x06009548 RID: 38216 RVA: 0x0035FF54 File Offset: 0x0035E154
		public void Alert()
		{
			this.Alerted = true;
			this.ParentObject.Brain.Allegiance.Hostile = true;
			this.ParentObject.Render.DetailColor = "R";
			base.DidX("stir", null, null, null, null, null, IComponent<GameObject>.ThePlayer, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x06009549 RID: 38217 RVA: 0x0035FFB4 File Offset: 0x0035E1B4
		public void Unalert()
		{
			this.Alerted = false;
			this.ParentObject.Brain.Allegiance.Hostile = false;
			this.ParentObject.Render.DetailColor = "Y";
			base.DidX("sleep", null, null, null, null, IComponent<GameObject>.ThePlayer, null, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0600954A RID: 38218 RVA: 0x00360012 File Offset: 0x0035E212
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AICryptHelpBroadcast")
			{
				this.Alert();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003DD2 RID: 15826
		public bool Alerted;
	}
}
