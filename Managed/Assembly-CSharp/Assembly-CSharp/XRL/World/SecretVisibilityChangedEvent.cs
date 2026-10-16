using System;
using Qud.API;

namespace XRL.World
{
	// Token: 0x0200084B RID: 2123
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class SecretVisibilityChangedEvent : PooledEvent<SecretVisibilityChangedEvent>
	{
		// Token: 0x06005432 RID: 21554 RVA: 0x00191720 File Offset: 0x0018F920
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005433 RID: 21555 RVA: 0x00191729 File Offset: 0x0018F929
		public override void Reset()
		{
			base.Reset();
			this.Entry = null;
		}

		// Token: 0x06005434 RID: 21556 RVA: 0x00191738 File Offset: 0x0018F938
		public static void Send(IBaseJournalEntry Entry)
		{
			SecretVisibilityChangedEvent secretVisibilityChangedEvent = PooledEvent<SecretVisibilityChangedEvent>.FromPool();
			secretVisibilityChangedEvent.Entry = Entry;
			The.Game.HandleEvent(secretVisibilityChangedEvent, true);
			PooledEvent<SecretVisibilityChangedEvent>.ResetTo(ref secretVisibilityChangedEvent);
		}

		// Token: 0x04002A52 RID: 10834
		public IBaseJournalEntry Entry;
	}
}
