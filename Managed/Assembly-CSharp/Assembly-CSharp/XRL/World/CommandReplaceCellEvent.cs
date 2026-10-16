using System;

namespace XRL.World
{
	// Token: 0x020006EA RID: 1770
	[GameEvent(Cascade = 256, Cache = MinEvent.Cache.Pool)]
	public class CommandReplaceCellEvent : PooledEvent<CommandReplaceCellEvent>
	{
		// Token: 0x06004A4B RID: 19019 RVA: 0x001740E9 File Offset: 0x001722E9
		public override int GetCascadeLevel()
		{
			return CommandReplaceCellEvent.CascadeLevel;
		}

		// Token: 0x06004A4C RID: 19020 RVA: 0x001740F0 File Offset: 0x001722F0
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004A4D RID: 19021 RVA: 0x001740F9 File Offset: 0x001722F9
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
		}

		// Token: 0x06004A4E RID: 19022 RVA: 0x00174108 File Offset: 0x00172308
		public static void Execute(GameObject Actor)
		{
			if (GameObject.Validate(ref Actor) && Actor.WantEvent(PooledEvent<CommandReplaceCellEvent>.ID, CommandReplaceCellEvent.CascadeLevel))
			{
				CommandReplaceCellEvent commandReplaceCellEvent = PooledEvent<CommandReplaceCellEvent>.FromPool();
				commandReplaceCellEvent.Actor = Actor;
				Actor.HandleEvent(commandReplaceCellEvent);
			}
		}

		// Token: 0x040023D0 RID: 9168
		public new static readonly int CascadeLevel = 256;

		// Token: 0x040023D1 RID: 9169
		public GameObject Actor;
	}
}
