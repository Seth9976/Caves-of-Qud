using System;

namespace XRL.World
{
	// Token: 0x02000767 RID: 1895
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMissileStatusColorEvent : PooledEvent<GetMissileStatusColorEvent>
	{
		// Token: 0x06004E10 RID: 19984 RVA: 0x0017EDB4 File Offset: 0x0017CFB4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E11 RID: 19985 RVA: 0x0017EDBD File Offset: 0x0017CFBD
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Color = null;
		}

		// Token: 0x06004E12 RID: 19986 RVA: 0x0017EDD3 File Offset: 0x0017CFD3
		public static GetMissileStatusColorEvent FromPool(GameObject Object, string Color = null)
		{
			GetMissileStatusColorEvent getMissileStatusColorEvent = PooledEvent<GetMissileStatusColorEvent>.FromPool();
			getMissileStatusColorEvent.Object = Object;
			getMissileStatusColorEvent.Color = Color;
			return getMissileStatusColorEvent;
		}

		// Token: 0x06004E13 RID: 19987 RVA: 0x0017EDE8 File Offset: 0x0017CFE8
		public static string GetFor(GameObject Object, string Color = null)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetMissileStatusColor"))
			{
				Event @event = Event.New("GetMissileStatusColor", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Color", Color);
				flag = Object.FireEvent(@event);
				Color = @event.GetStringParameter("Color", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetMissileStatusColorEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMissileStatusColorEvent getMissileStatusColorEvent = GetMissileStatusColorEvent.FromPool(Object, Color);
				flag = Object.HandleEvent(getMissileStatusColorEvent);
				Color = getMissileStatusColorEvent.Color;
			}
			return Color ?? Object.DisplayNameColor;
		}

		// Token: 0x04002602 RID: 9730
		public GameObject Object;

		// Token: 0x04002603 RID: 9731
		public string Color;
	}
}
