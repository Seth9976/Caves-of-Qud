using System;

namespace XRL.World
{
	// Token: 0x02000857 RID: 2135
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ShotCompleteEvent : PooledEvent<ShotCompleteEvent>
	{
		// Token: 0x06005541 RID: 21825 RVA: 0x0019805B File Offset: 0x0019625B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005542 RID: 21826 RVA: 0x00198064 File Offset: 0x00196264
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Actor = null;
			this.LoadedAmmo = null;
		}

		// Token: 0x06005543 RID: 21827 RVA: 0x00198084 File Offset: 0x00196284
		public static void Send(GameObject Object, GameObject Actor, GameObject LoadedAmmo)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("ShotComplete"))
			{
				Event @event = Event.New("ShotComplete", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("LoadedAmmo", LoadedAmmo);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<ShotCompleteEvent>.ID, MinEvent.CascadeLevel))
			{
				ShotCompleteEvent shotCompleteEvent = PooledEvent<ShotCompleteEvent>.FromPool();
				shotCompleteEvent.Object = Object;
				shotCompleteEvent.Actor = Actor;
				shotCompleteEvent.LoadedAmmo = LoadedAmmo;
				flag = Object.HandleEvent(shotCompleteEvent);
			}
		}

		// Token: 0x04002AE4 RID: 10980
		public GameObject Object;

		// Token: 0x04002AE5 RID: 10981
		public GameObject Actor;

		// Token: 0x04002AE6 RID: 10982
		public GameObject LoadedAmmo;
	}
}
