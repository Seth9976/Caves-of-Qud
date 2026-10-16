using System;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02001016 RID: 4118
	[Serializable]
	public class DestroyMe : IActivePart
	{
		// Token: 0x0600A418 RID: 42008 RVA: 0x003C04FF File Offset: 0x003BE6FF
		public DestroyMe()
		{
		}

		// Token: 0x0600A419 RID: 42009 RVA: 0x003C0507 File Offset: 0x003BE707
		public DestroyMe(string who)
			: this()
		{
			this.who = who;
		}

		// Token: 0x0600A41A RID: 42010 RVA: 0x003C0516 File Offset: 0x003BE716
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A41B RID: 42011 RVA: 0x003C0519 File Offset: 0x003BE719
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IdleQueryEvent>.ID;
		}

		// Token: 0x0600A41C RID: 42012 RVA: 0x003C052F File Offset: 0x003BE72F
		public override bool HandleEvent(IdleQueryEvent E)
		{
			return !this.TryDestroyMe(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x0600A41D RID: 42013 RVA: 0x003C0548 File Offset: 0x003BE748
		public bool TryDestroyMe(GameObject actor)
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
			actor.Brain.PushGoal(new Kill(this.ParentObject, false));
			return true;
		}

		// Token: 0x04004400 RID: 17408
		public string who;
	}
}
