using System;
using System.Collections.Generic;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000692 RID: 1682
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class BeforeFireMissileWeaponsEvent : PooledEvent<BeforeFireMissileWeaponsEvent>
	{
		// Token: 0x060047EF RID: 18415 RVA: 0x0016AFA9 File Offset: 0x001691A9
		public override int GetCascadeLevel()
		{
			return BeforeFireMissileWeaponsEvent.CascadeLevel;
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0016AFB0 File Offset: 0x001691B0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0016AFB9 File Offset: 0x001691B9
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.ApparentTarget = null;
			this.TargetCell = null;
			this.Path = null;
			this.MissileWeapons = null;
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0016AFE4 File Offset: 0x001691E4
		public static bool Check(GameObject Actor, GameObject ApparentTarget = null, Cell TargetCell = null, MissilePath Path = null, List<MissileWeapon> MissileWeapons = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("BeforeFireMissileWeapons"))
			{
				Event @event = Event.New("BeforeFireMissileWeapons", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("ApparentTarget", ApparentTarget);
				@event.SetParameter("TargetCell", TargetCell);
				@event.SetParameter("Path", Path);
				@event.SetParameter("MissileWeapons", MissileWeapons);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<BeforeFireMissileWeaponsEvent>.ID, BeforeFireMissileWeaponsEvent.CascadeLevel))
			{
				BeforeFireMissileWeaponsEvent beforeFireMissileWeaponsEvent = PooledEvent<BeforeFireMissileWeaponsEvent>.FromPool();
				beforeFireMissileWeaponsEvent.Actor = Actor;
				beforeFireMissileWeaponsEvent.ApparentTarget = ApparentTarget;
				beforeFireMissileWeaponsEvent.TargetCell = TargetCell;
				beforeFireMissileWeaponsEvent.Path = Path;
				beforeFireMissileWeaponsEvent.MissileWeapons = MissileWeapons;
				flag = Actor.HandleEvent(beforeFireMissileWeaponsEvent);
			}
			return flag;
		}

		// Token: 0x0400224D RID: 8781
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400224E RID: 8782
		public GameObject Actor;

		// Token: 0x0400224F RID: 8783
		public GameObject ApparentTarget;

		// Token: 0x04002250 RID: 8784
		public Cell TargetCell;

		// Token: 0x04002251 RID: 8785
		public MissilePath Path;

		// Token: 0x04002252 RID: 8786
		public List<MissileWeapon> MissileWeapons;
	}
}
