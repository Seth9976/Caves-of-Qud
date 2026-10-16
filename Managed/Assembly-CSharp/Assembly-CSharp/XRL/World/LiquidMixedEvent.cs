using System;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000800 RID: 2048
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class LiquidMixedEvent : PooledEvent<LiquidMixedEvent>
	{
		// Token: 0x060051F7 RID: 20983 RVA: 0x0018B505 File Offset: 0x00189705
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060051F8 RID: 20984 RVA: 0x0018B50E File Offset: 0x0018970E
		public override void Reset()
		{
			base.Reset();
			this.Liquid = null;
		}

		// Token: 0x060051F9 RID: 20985 RVA: 0x0018B51D File Offset: 0x0018971D
		public static LiquidMixedEvent FromPool(LiquidVolume Liquid)
		{
			LiquidMixedEvent liquidMixedEvent = PooledEvent<LiquidMixedEvent>.FromPool();
			liquidMixedEvent.Liquid = Liquid;
			return liquidMixedEvent;
		}

		// Token: 0x060051FA RID: 20986 RVA: 0x0018B52C File Offset: 0x0018972C
		public static void Send(LiquidVolume Liquid)
		{
			if (Liquid.ParentObject != null)
			{
				if (Liquid.ParentObject.HasRegisteredEvent("LiquidMixed"))
				{
					Liquid.ParentObject.FireEvent(Event.New("LiquidMixed", "Volume", Liquid));
				}
				if (Liquid.ParentObject.WantEvent(PooledEvent<LiquidMixedEvent>.ID, MinEvent.CascadeLevel))
				{
					Liquid.ParentObject.HandleEvent(LiquidMixedEvent.FromPool(Liquid));
				}
			}
		}

		// Token: 0x04002922 RID: 10530
		public LiquidVolume Liquid;
	}
}
