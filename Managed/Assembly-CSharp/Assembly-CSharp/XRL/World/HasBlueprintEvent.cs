using System;

namespace XRL.World
{
	// Token: 0x020007B2 RID: 1970
	[GameEvent(Cascade = 3, Cache = MinEvent.Cache.Pool)]
	public class HasBlueprintEvent : PooledEvent<HasBlueprintEvent>
	{
		// Token: 0x06005011 RID: 20497 RVA: 0x00185E26 File Offset: 0x00184026
		public override int GetCascadeLevel()
		{
			return HasBlueprintEvent.CascadeLevel;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00185E2D File Offset: 0x0018402D
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00185E36 File Offset: 0x00184036
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Blueprint = null;
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00185E4C File Offset: 0x0018404C
		public static bool Check(GameObject Object, string Blueprint)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("HasBlueprint"))
			{
				Event @event = Event.New("HasBlueprint", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Blueprint", Blueprint);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<HasBlueprintEvent>.ID, HasBlueprintEvent.CascadeLevel))
			{
				HasBlueprintEvent hasBlueprintEvent = PooledEvent<HasBlueprintEvent>.FromPool();
				hasBlueprintEvent.Object = Object;
				hasBlueprintEvent.Blueprint = Blueprint;
				flag = Object.HandleEvent(hasBlueprintEvent);
			}
			return !flag;
		}

		// Token: 0x040027A2 RID: 10146
		public new static readonly int CascadeLevel = 3;

		// Token: 0x040027A3 RID: 10147
		public GameObject Object;

		// Token: 0x040027A4 RID: 10148
		public string Blueprint;
	}
}
