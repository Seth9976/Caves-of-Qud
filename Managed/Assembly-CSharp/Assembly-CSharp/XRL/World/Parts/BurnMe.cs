using System;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02001003 RID: 4099
	[Serializable]
	public class BurnMe : IActivePart
	{
		// Token: 0x0600A35F RID: 41823 RVA: 0x003BB3BF File Offset: 0x003B95BF
		public BurnMe()
		{
		}

		// Token: 0x0600A360 RID: 41824 RVA: 0x003BB3C7 File Offset: 0x003B95C7
		public BurnMe(string who)
			: this()
		{
			this.who = who;
		}

		// Token: 0x0600A361 RID: 41825 RVA: 0x003BB3D6 File Offset: 0x003B95D6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A362 RID: 41826 RVA: 0x003BB3D9 File Offset: 0x003B95D9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IdleQueryEvent>.ID;
		}

		// Token: 0x0600A363 RID: 41827 RVA: 0x003BB3EF File Offset: 0x003B95EF
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return !this.TryBurnMe(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x0600A364 RID: 41828 RVA: 0x003BB408 File Offset: 0x003B9608
		public bool TryBurnMe(GameObject actor)
		{
			if (this.ParentObject.GetIntProperty("DroppedByPlayer", 0) != 0)
			{
				return false;
			}
			if (!actor.BelongsToFaction(this.who))
			{
				return false;
			}
			if (this.ParentObject.IsAflame())
			{
				return false;
			}
			if (actor.Brain == null)
			{
				return false;
			}
			if (this.ParentObject.CurrentCell == null)
			{
				return false;
			}
			actor.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
			{
				if (actor.DistanceTo(this.ParentObject) <= 1)
				{
					this.ParentObject.Physics.Temperature = this.ParentObject.Physics.FlameTemperature * 2;
					actor.UseEnergy(1000, "Item Ignite", null, null, false);
				}
				h.FailToParent();
			}, null, null));
			actor.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
			return true;
		}

		// Token: 0x040043B3 RID: 17331
		public string who;
	}
}
