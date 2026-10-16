using System;

namespace XRL.World
{
	// Token: 0x0200071C RID: 1820
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GenericCommandEvent : PooledEvent<GenericCommandEvent>
	{
		// Token: 0x06004BC0 RID: 19392 RVA: 0x00177A78 File Offset: 0x00175C78
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x00177A81 File Offset: 0x00175C81
		public override void Reset()
		{
			base.Reset();
			this.Command = null;
			this.Type = null;
			this.Object = null;
			this.Source = null;
			this.Level = 0;
			this.Flags = 0;
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x00177AB4 File Offset: 0x00175CB4
		public static void Send(IEventSource EventSource, string Command, string Type = null, object Object = null, object Source = null, int Level = 0, int Flags = 0)
		{
			GenericCommandEvent genericCommandEvent = PooledEvent<GenericCommandEvent>.FromPool();
			genericCommandEvent.Command = Command;
			genericCommandEvent.Type = Type;
			genericCommandEvent.Object = Object;
			genericCommandEvent.Source = Source;
			genericCommandEvent.Level = Level;
			genericCommandEvent.Flags = Flags;
			EventSource.HandleEvent(genericCommandEvent);
			PooledEvent<GenericCommandEvent>.ResetTo(ref genericCommandEvent);
		}

		// Token: 0x04002486 RID: 9350
		public string Command;

		// Token: 0x04002487 RID: 9351
		public string Type;

		// Token: 0x04002488 RID: 9352
		public object Object;

		// Token: 0x04002489 RID: 9353
		public object Source;

		// Token: 0x0400248A RID: 9354
		public int Level;

		// Token: 0x0400248B RID: 9355
		public int Flags;
	}
}
