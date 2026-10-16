using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000748 RID: 1864
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class GetExtraPhysicalFeaturesEvent : PooledEvent<GetExtraPhysicalFeaturesEvent>
	{
		// Token: 0x06004D28 RID: 19752 RVA: 0x0017C478 File Offset: 0x0017A678
		public override int GetCascadeLevel()
		{
			return GetExtraPhysicalFeaturesEvent.CascadeLevel;
		}

		// Token: 0x06004D29 RID: 19753 RVA: 0x0017C47F File Offset: 0x0017A67F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004D2A RID: 19754 RVA: 0x0017C488 File Offset: 0x0017A688
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Features = null;
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0017C49E File Offset: 0x0017A69E
		public static GetExtraPhysicalFeaturesEvent FromPool(GameObject Object, List<string> Features)
		{
			GetExtraPhysicalFeaturesEvent getExtraPhysicalFeaturesEvent = PooledEvent<GetExtraPhysicalFeaturesEvent>.FromPool();
			getExtraPhysicalFeaturesEvent.Object = Object;
			getExtraPhysicalFeaturesEvent.Features = Features;
			return getExtraPhysicalFeaturesEvent;
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0017C4B4 File Offset: 0x0017A6B4
		public static void Send(GameObject Object, List<string> Features)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetExtraPhysicalFeatures"))
			{
				Event @event = Event.New("GetExtraPhysicalFeatures", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Features", Features);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetExtraPhysicalFeaturesEvent>.ID, GetExtraPhysicalFeaturesEvent.CascadeLevel))
			{
				flag = Object.HandleEvent(GetExtraPhysicalFeaturesEvent.FromPool(Object, Features));
			}
		}

		// Token: 0x0400256A RID: 9578
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400256B RID: 9579
		public GameObject Object;

		// Token: 0x0400256C RID: 9580
		public List<string> Features;
	}
}
