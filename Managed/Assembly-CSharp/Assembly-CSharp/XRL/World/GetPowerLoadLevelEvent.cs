using System;

namespace XRL.World
{
	// Token: 0x02000775 RID: 1909
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetPowerLoadLevelEvent : PooledEvent<GetPowerLoadLevelEvent>
	{
		// Token: 0x06004E7D RID: 20093 RVA: 0x00180B26 File Offset: 0x0017ED26
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E7E RID: 20094 RVA: 0x00180B2F File Offset: 0x0017ED2F
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Level = 0;
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x00180B48 File Offset: 0x0017ED48
		public static int GetFor(GameObject Object, int Level = 100)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("GetPowerLoadLevel"))
			{
				Event @event = Event.New("GetPowerLoadLevel", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Level", Level);
				flag = Object.FireEvent(@event);
				Level = @event.GetIntParameter("Level", 0);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<GetPowerLoadLevelEvent>.ID, MinEvent.CascadeLevel))
			{
				GetPowerLoadLevelEvent getPowerLoadLevelEvent = PooledEvent<GetPowerLoadLevelEvent>.FromPool();
				getPowerLoadLevelEvent.Object = Object;
				getPowerLoadLevelEvent.Level = Level;
				flag = Object.HandleEvent(getPowerLoadLevelEvent);
				Level = getPowerLoadLevelEvent.Level;
			}
			return Level;
		}

		// Token: 0x04002651 RID: 9809
		public GameObject Object;

		// Token: 0x04002652 RID: 9810
		public int Level;
	}
}
