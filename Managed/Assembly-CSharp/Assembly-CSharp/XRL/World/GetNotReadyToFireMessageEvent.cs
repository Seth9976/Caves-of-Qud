using System;

namespace XRL.World
{
	// Token: 0x02000771 RID: 1905
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetNotReadyToFireMessageEvent : PooledEvent<GetNotReadyToFireMessageEvent>
	{
		// Token: 0x06004E60 RID: 20064 RVA: 0x00180424 File Offset: 0x0017E624
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x0018042D File Offset: 0x0017E62D
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Message = null;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x00180444 File Offset: 0x0017E644
		public static string GetFor(GameObject Object)
		{
			bool flag = true;
			string text = null;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetNotReadyToFireMessage"))
			{
				Event @event = Event.New("GetNotReadyToFireMessage", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Message", text);
				flag = Object.FireEvent(@event);
				text = @event.GetStringParameter("Message", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetNotReadyToFireMessageEvent>.ID, MinEvent.CascadeLevel))
			{
				GetNotReadyToFireMessageEvent getNotReadyToFireMessageEvent = PooledEvent<GetNotReadyToFireMessageEvent>.FromPool();
				getNotReadyToFireMessageEvent.Object = Object;
				getNotReadyToFireMessageEvent.Message = text;
				flag = Object.HandleEvent(getNotReadyToFireMessageEvent);
				text = getNotReadyToFireMessageEvent.Message;
			}
			return text;
		}

		// Token: 0x04002643 RID: 9795
		public GameObject Object;

		// Token: 0x04002644 RID: 9796
		public string Message;
	}
}
