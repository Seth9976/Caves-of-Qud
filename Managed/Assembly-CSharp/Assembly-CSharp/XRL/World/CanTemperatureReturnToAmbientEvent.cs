using System;

namespace XRL.World
{
	// Token: 0x020006CE RID: 1742
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class CanTemperatureReturnToAmbientEvent : PooledEvent<CanTemperatureReturnToAmbientEvent>
	{
		// Token: 0x0600499C RID: 18844 RVA: 0x001721F4 File Offset: 0x001703F4
		public override int GetCascadeLevel()
		{
			return CanTemperatureReturnToAmbientEvent.CascadeLevel;
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001721FB File Offset: 0x001703FB
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00172204 File Offset: 0x00170404
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Amount = 0;
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x0017221C File Offset: 0x0017041C
		public static bool Check(GameObject Object, int Amount)
		{
			bool flag = true;
			if (flag && GameObject.Validate(ref Object) && Object.HasRegisteredEvent("CanTemperatureReturnToAmbient"))
			{
				Event @event = Event.New("CanTemperatureReturnToAmbient", 0, 0, 0);
				@event.SetParameter("Object", Object);
				@event.SetParameter("Amount", Amount);
				flag = Object.FireEvent(@event);
			}
			if (flag && GameObject.Validate(ref Object) && Object.WantEvent(PooledEvent<CanTemperatureReturnToAmbientEvent>.ID, CanTemperatureReturnToAmbientEvent.CascadeLevel))
			{
				CanTemperatureReturnToAmbientEvent canTemperatureReturnToAmbientEvent = PooledEvent<CanTemperatureReturnToAmbientEvent>.FromPool();
				canTemperatureReturnToAmbientEvent.Object = Object;
				canTemperatureReturnToAmbientEvent.Amount = Amount;
				flag = Object.HandleEvent(canTemperatureReturnToAmbientEvent);
			}
			return flag;
		}

		// Token: 0x04002362 RID: 9058
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002363 RID: 9059
		public GameObject Object;

		// Token: 0x04002364 RID: 9060
		public int Amount;
	}
}
