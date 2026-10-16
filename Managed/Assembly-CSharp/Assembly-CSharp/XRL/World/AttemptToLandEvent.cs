using System;

namespace XRL.World
{
	// Token: 0x02000680 RID: 1664
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class AttemptToLandEvent : PooledEvent<AttemptToLandEvent>
	{
		// Token: 0x06004762 RID: 18274 RVA: 0x00169595 File Offset: 0x00167795
		public override int GetCascadeLevel()
		{
			return AttemptToLandEvent.CascadeLevel;
		}

		// Token: 0x06004763 RID: 18275 RVA: 0x0016959C File Offset: 0x0016779C
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004764 RID: 18276 RVA: 0x001695A5 File Offset: 0x001677A5
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
		}

		// Token: 0x06004765 RID: 18277 RVA: 0x001695B4 File Offset: 0x001677B4
		public static AttemptToLandEvent FromPool(GameObject Actor)
		{
			AttemptToLandEvent attemptToLandEvent = PooledEvent<AttemptToLandEvent>.FromPool();
			attemptToLandEvent.Actor = Actor;
			return attemptToLandEvent;
		}

		// Token: 0x06004766 RID: 18278 RVA: 0x001695C4 File Offset: 0x001677C4
		public static bool Check(GameObject Actor)
		{
			return (GameObject.Validate(Actor) && Actor.HasRegisteredEvent("AttemptToLand") && !Actor.FireEvent(Event.New("AttemptToLand", "Actor", Actor))) || (GameObject.Validate(Actor) && Actor.WantEvent(PooledEvent<AttemptToLandEvent>.ID, AttemptToLandEvent.CascadeLevel) && !Actor.HandleEvent(AttemptToLandEvent.FromPool(Actor)));
		}

		// Token: 0x0400220C RID: 8716
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400220D RID: 8717
		public GameObject Actor;
	}
}
