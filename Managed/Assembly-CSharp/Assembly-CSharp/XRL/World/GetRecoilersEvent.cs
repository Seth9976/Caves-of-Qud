using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000781 RID: 1921
	[GameEvent(Cascade = 143, Cache = MinEvent.Cache.Pool)]
	public class GetRecoilersEvent : PooledEvent<GetRecoilersEvent>
	{
		// Token: 0x06004EC4 RID: 20164 RVA: 0x001815F2 File Offset: 0x0017F7F2
		public override int GetCascadeLevel()
		{
			return GetRecoilersEvent.CascadeLevel;
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x001815F9 File Offset: 0x0017F7F9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x00181602 File Offset: 0x0017F802
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Objects = null;
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x00181618 File Offset: 0x0017F818
		public static List<GameObject> GetFor(GameObject Actor)
		{
			bool flag = true;
			List<GameObject> list = Event.NewGameObjectList();
			if (flag && GameObject.Validate(ref Actor) && Actor.HasRegisteredEvent("GetRecoilers"))
			{
				Event @event = Event.New("GetRecoilers", 0, 0, 0);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Objects", list);
				flag = Actor.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<GetRecoilersEvent>.ID, GetRecoilersEvent.CascadeLevel))
			{
				GetRecoilersEvent getRecoilersEvent = PooledEvent<GetRecoilersEvent>.FromPool();
				getRecoilersEvent.Actor = Actor;
				getRecoilersEvent.Objects = list;
				flag = Actor.HandleEvent(getRecoilersEvent);
			}
			return list;
		}

		// Token: 0x04002680 RID: 9856
		public new static readonly int CascadeLevel = 143;

		// Token: 0x04002681 RID: 9857
		public GameObject Actor;

		// Token: 0x04002682 RID: 9858
		public List<GameObject> Objects;
	}
}
