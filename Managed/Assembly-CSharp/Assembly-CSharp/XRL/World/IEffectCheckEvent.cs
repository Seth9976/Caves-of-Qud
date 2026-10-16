using System;

namespace XRL.World
{
	// Token: 0x020007C8 RID: 1992
	[GameEvent(Base = true, Cascade = 0)]
	public abstract class IEffectCheckEvent : MinEvent
	{
		// Token: 0x06005079 RID: 20601 RVA: 0x00186B40 File Offset: 0x00184D40
		public override int GetCascadeLevel()
		{
			return IEffectCheckEvent.CascadeLevel;
		}

		// Token: 0x0600507A RID: 20602 RVA: 0x00186B47 File Offset: 0x00184D47
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600507B RID: 20603 RVA: 0x00186B50 File Offset: 0x00184D50
		public override void Reset()
		{
			base.Reset();
			this.Name = null;
			this.Duration = 0;
		}

		// Token: 0x040027FE RID: 10238
		public new static readonly int CascadeLevel;

		// Token: 0x040027FF RID: 10239
		public string Name;

		// Token: 0x04002800 RID: 10240
		public int Duration;
	}
}
