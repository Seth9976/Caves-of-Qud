using System;

namespace XRL.World
{
	// Token: 0x02000874 RID: 2164
	[GameEvent(Cache = MinEvent.Cache.Singleton)]
	public class UseEnergyEvent : SingletonEvent<UseEnergyEvent>
	{
		// Token: 0x060055F9 RID: 22009 RVA: 0x00199F92 File Offset: 0x00198192
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x00199F9B File Offset: 0x0019819B
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Amount = 0;
			this.Type = null;
			this.Passive = false;
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x00199FC0 File Offset: 0x001981C0
		public static void Send(GameObject Actor, int Amount, string Type, bool Passive = false)
		{
			if (!Actor.WantEvent(SingletonEvent<UseEnergyEvent>.ID, MinEvent.CascadeLevel))
			{
				return;
			}
			SingletonEvent<UseEnergyEvent>.Instance.Actor = Actor;
			SingletonEvent<UseEnergyEvent>.Instance.Amount = Amount;
			SingletonEvent<UseEnergyEvent>.Instance.Type = Type;
			SingletonEvent<UseEnergyEvent>.Instance.Passive = Passive;
			Actor.HandleEvent(SingletonEvent<UseEnergyEvent>.Instance);
			SingletonEvent<UseEnergyEvent>.Instance.Reset();
		}

		// Token: 0x04002B50 RID: 11088
		public GameObject Actor;

		// Token: 0x04002B51 RID: 11089
		public int Amount;

		// Token: 0x04002B52 RID: 11090
		public string Type;

		// Token: 0x04002B53 RID: 11091
		public bool Passive;
	}
}
