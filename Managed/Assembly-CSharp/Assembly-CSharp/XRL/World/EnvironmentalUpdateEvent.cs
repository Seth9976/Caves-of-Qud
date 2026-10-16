using System;

namespace XRL.World
{
	// Token: 0x0200070B RID: 1803
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class EnvironmentalUpdateEvent : PooledEvent<EnvironmentalUpdateEvent>
	{
		// Token: 0x06004B3E RID: 19262 RVA: 0x00176448 File Offset: 0x00174648
		public override int GetCascadeLevel()
		{
			return EnvironmentalUpdateEvent.CascadeLevel;
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x0017644F File Offset: 0x0017464F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x00176458 File Offset: 0x00174658
		public static void Send(GameObject Object)
		{
			if (Object.WantEvent(PooledEvent<EnvironmentalUpdateEvent>.ID, EnvironmentalUpdateEvent.CascadeLevel))
			{
				EnvironmentalUpdateEvent environmentalUpdateEvent = PooledEvent<EnvironmentalUpdateEvent>.FromPool();
				Object.HandleEvent(environmentalUpdateEvent);
				PooledEvent<EnvironmentalUpdateEvent>.ResetTo(ref environmentalUpdateEvent);
			}
		}

		// Token: 0x04002450 RID: 9296
		public new static readonly int CascadeLevel = 15;
	}
}
