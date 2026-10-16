using System;

namespace XRL.World
{
	// Token: 0x020007D7 RID: 2007
	[GameEvent(Base = true)]
	public abstract class IQuestEvent : MinEvent
	{
		// Token: 0x060050FA RID: 20730 RVA: 0x0018819B File Offset: 0x0018639B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060050FB RID: 20731 RVA: 0x001881A4 File Offset: 0x001863A4
		public override void Reset()
		{
			base.Reset();
			this.Quest = null;
		}

		// Token: 0x0400286B RID: 10347
		public Quest Quest;
	}
}
