using System;

namespace XRL.World
{
	// Token: 0x02000706 RID: 1798
	[GameEvent(Cascade = 352, Cache = MinEvent.Cache.Singleton)]
	public class EndTurnEvent : SingletonEvent<EndTurnEvent>
	{
		// Token: 0x06004B15 RID: 19221 RVA: 0x00175F0B File Offset: 0x0017410B
		public override int GetCascadeLevel()
		{
			return EndTurnEvent.CascadeLevel;
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x00175F12 File Offset: 0x00174112
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x00175F1B File Offset: 0x0017411B
		public static void Send(GameObject Object)
		{
			Object.HandleEvent(SingletonEvent<EndTurnEvent>.Instance);
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x00175F29 File Offset: 0x00174129
		public static void Send(XRLGame Game)
		{
			Game.HandleEvent(SingletonEvent<EndTurnEvent>.Instance);
		}

		// Token: 0x06004B19 RID: 19225 RVA: 0x00175F37 File Offset: 0x00174137
		public static void Send(Zone Zone)
		{
			Zone.HandleEvent(SingletonEvent<EndTurnEvent>.Instance);
		}

		// Token: 0x04002440 RID: 9280
		public new static readonly int CascadeLevel = 352;

		// Token: 0x04002441 RID: 9281
		public static ImmutableEvent registeredInstance = new ImmutableEvent("EndTurn");
	}
}
