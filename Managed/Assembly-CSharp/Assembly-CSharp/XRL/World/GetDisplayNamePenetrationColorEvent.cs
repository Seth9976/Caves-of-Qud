using System;

namespace XRL.World
{
	// Token: 0x02000744 RID: 1860
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDisplayNamePenetrationColorEvent : PooledEvent<GetDisplayNamePenetrationColorEvent>
	{
		// Token: 0x06004D11 RID: 19729 RVA: 0x0017BE9A File Offset: 0x0017A09A
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0017BEA3 File Offset: 0x0017A0A3
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Color = null;
		}

		// Token: 0x06004D13 RID: 19731 RVA: 0x0017BEBC File Offset: 0x0017A0BC
		public static string GetFor(GameObject Object, string Default = "c")
		{
			string text = Default;
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetDisplayNamePenetrationColor"))
			{
				Event @event = Event.New("GetDisplayNamePenetrationColor", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Color", text);
				flag = Object.FireEvent(@event);
				text = @event.GetStringParameter("Color", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetDisplayNamePenetrationColorEvent>.ID, MinEvent.CascadeLevel))
			{
				GetDisplayNamePenetrationColorEvent getDisplayNamePenetrationColorEvent = PooledEvent<GetDisplayNamePenetrationColorEvent>.FromPool();
				getDisplayNamePenetrationColorEvent.Object = Object;
				getDisplayNamePenetrationColorEvent.Color = text;
				flag = Object.HandleEvent(getDisplayNamePenetrationColorEvent);
				text = getDisplayNamePenetrationColorEvent.Color;
			}
			return text;
		}

		// Token: 0x04002551 RID: 9553
		public GameObject Object;

		// Token: 0x04002552 RID: 9554
		public string Color;
	}
}
