using System;

namespace XRL.World
{
	// Token: 0x0200071D RID: 1821
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GenericDeepNotifyEvent : PooledEvent<GenericDeepNotifyEvent>
	{
		// Token: 0x06004BC4 RID: 19396 RVA: 0x00177B0B File Offset: 0x00175D0B
		public override int GetCascadeLevel()
		{
			return GenericDeepNotifyEvent.CascadeLevel;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x00177B12 File Offset: 0x00175D12
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x00177B1B File Offset: 0x00175D1B
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Source = null;
			this.Notify = null;
			this.Level = 0;
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x00177B48 File Offset: 0x00175D48
		public static void Send(GameObject Object, string Notify, GameObject Subject = null, GameObject Source = null, int Level = 0)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GenericDeepNotify"))
			{
				Event @event = Event.New("GenericDeepNotify", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Notify", Notify);
				@event.SetParameter("Level", Level);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GenericDeepNotifyEvent>.ID, GenericDeepNotifyEvent.CascadeLevel))
			{
				GenericDeepNotifyEvent genericDeepNotifyEvent = PooledEvent<GenericDeepNotifyEvent>.FromPool();
				genericDeepNotifyEvent.Object = Object;
				genericDeepNotifyEvent.Subject = Subject;
				genericDeepNotifyEvent.Source = Source;
				genericDeepNotifyEvent.Notify = Notify;
				genericDeepNotifyEvent.Level = Level;
				flag = Object.HandleEvent(genericDeepNotifyEvent);
			}
		}

		// Token: 0x0400248C RID: 9356
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400248D RID: 9357
		public GameObject Object;

		// Token: 0x0400248E RID: 9358
		public GameObject Subject;

		// Token: 0x0400248F RID: 9359
		public GameObject Source;

		// Token: 0x04002490 RID: 9360
		public string Notify;

		// Token: 0x04002491 RID: 9361
		public int Level;
	}
}
