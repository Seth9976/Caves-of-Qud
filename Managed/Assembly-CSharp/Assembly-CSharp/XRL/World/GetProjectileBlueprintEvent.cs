using System;

namespace XRL.World
{
	// Token: 0x02000778 RID: 1912
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetProjectileBlueprintEvent : PooledEvent<GetProjectileBlueprintEvent>
	{
		// Token: 0x06004E92 RID: 20114 RVA: 0x00180E76 File Offset: 0x0017F076
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x00180E7F File Offset: 0x0017F07F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Blueprint = null;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x00180E98 File Offset: 0x0017F098
		public static string GetFor(GameObject Object)
		{
			bool flag = true;
			string text = null;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetProjectileBlueprint"))
			{
				Event @event = Event.New("GetProjectileBlueprint", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Blueprint", text);
				flag = Object.FireEvent(@event);
				text = @event.GetStringParameter("Blueprint", null);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetProjectileBlueprintEvent>.ID, MinEvent.CascadeLevel))
			{
				GetProjectileBlueprintEvent getProjectileBlueprintEvent = PooledEvent<GetProjectileBlueprintEvent>.FromPool();
				getProjectileBlueprintEvent.Object = Object;
				getProjectileBlueprintEvent.Blueprint = text;
				flag = Object.HandleEvent(getProjectileBlueprintEvent);
				text = getProjectileBlueprintEvent.Blueprint;
			}
			return text;
		}

		// Token: 0x0400265B RID: 9819
		public GameObject Object;

		// Token: 0x0400265C RID: 9820
		public string Blueprint;
	}
}
