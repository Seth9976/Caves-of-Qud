using System;

namespace XRL.World
{
	// Token: 0x0200071E RID: 1822
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GenericDeepQueryEvent : PooledEvent<GenericDeepQueryEvent>
	{
		// Token: 0x06004BCA RID: 19402 RVA: 0x00177C29 File Offset: 0x00175E29
		public override int GetCascadeLevel()
		{
			return GenericDeepQueryEvent.CascadeLevel;
		}

		// Token: 0x06004BCB RID: 19403 RVA: 0x00177C30 File Offset: 0x00175E30
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BCC RID: 19404 RVA: 0x00177C39 File Offset: 0x00175E39
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Subject = null;
			this.Source = null;
			this.Query = null;
			this.Level = 0;
			this.Result = false;
		}

		// Token: 0x06004BCD RID: 19405 RVA: 0x00177C6C File Offset: 0x00175E6C
		public static bool Check(GameObject Object, string Query, GameObject Subject = null, GameObject Source = null, int Level = 0)
		{
			bool flag = true;
			bool flag2 = false;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GenericDeepQuery"))
			{
				Event @event = Event.New("GenericDeepQuery", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Subject", Subject);
				@event.SetParameter("Source", Source);
				@event.SetParameter("Query", Query);
				@event.SetParameter("Level", Level);
				@event.SetFlag("Result", flag2);
				flag = Object.FireEvent(@event);
				flag2 = @event.HasFlag("Result");
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GenericDeepQueryEvent>.ID, GenericDeepQueryEvent.CascadeLevel))
			{
				GenericDeepQueryEvent genericDeepQueryEvent = PooledEvent<GenericDeepQueryEvent>.FromPool();
				genericDeepQueryEvent.Object = Object;
				genericDeepQueryEvent.Subject = Subject;
				genericDeepQueryEvent.Source = Source;
				genericDeepQueryEvent.Query = Query;
				genericDeepQueryEvent.Level = Level;
				genericDeepQueryEvent.Result = flag2;
				flag = Object.HandleEvent(genericDeepQueryEvent);
				flag2 = genericDeepQueryEvent.Result;
			}
			return flag2;
		}

		// Token: 0x04002492 RID: 9362
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002493 RID: 9363
		public GameObject Object;

		// Token: 0x04002494 RID: 9364
		public GameObject Subject;

		// Token: 0x04002495 RID: 9365
		public GameObject Source;

		// Token: 0x04002496 RID: 9366
		public string Query;

		// Token: 0x04002497 RID: 9367
		public int Level;

		// Token: 0x04002498 RID: 9368
		public bool Result;
	}
}
