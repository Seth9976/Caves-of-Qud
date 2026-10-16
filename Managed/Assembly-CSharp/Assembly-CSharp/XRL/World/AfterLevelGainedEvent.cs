using System;

namespace XRL.World
{
	// Token: 0x02000662 RID: 1634
	[GameEvent(Cascade = 17)]
	public class AfterLevelGainedEvent : PooledEvent<AfterLevelGainedEvent>
	{
		// Token: 0x0600468B RID: 18059 RVA: 0x0016719D File Offset: 0x0016539D
		public override int GetCascadeLevel()
		{
			return AfterLevelGainedEvent.CascadeLevel;
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x001671A4 File Offset: 0x001653A4
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x001671AD File Offset: 0x001653AD
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Kill = null;
			this.InfluencedBy = null;
			this.Level = 0;
			this.Detail = false;
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x001671D8 File Offset: 0x001653D8
		public static void Send(BeforeLevelGainedEvent E)
		{
			AfterLevelGainedEvent afterLevelGainedEvent = PooledEvent<AfterLevelGainedEvent>.FromPool();
			GameObject actor = E.Actor;
			afterLevelGainedEvent.Actor = actor;
			afterLevelGainedEvent.Kill = E.Kill;
			afterLevelGainedEvent.InfluencedBy = E.InfluencedBy;
			afterLevelGainedEvent.Level = E.Level;
			afterLevelGainedEvent.Detail = E.Detail;
			actor.HandleEvent(afterLevelGainedEvent);
			The.Game.HandleEvent(afterLevelGainedEvent);
			actor.FireEvent("AfterLevelGained", afterLevelGainedEvent);
			E.Kill = afterLevelGainedEvent.Kill;
			E.InfluencedBy = afterLevelGainedEvent.InfluencedBy;
			E.Detail = afterLevelGainedEvent.Detail;
			E.ProcessChildEvent(afterLevelGainedEvent);
		}

		// Token: 0x0400218C RID: 8588
		public new static readonly int CascadeLevel = 17;

		// Token: 0x0400218D RID: 8589
		public GameObject Actor;

		// Token: 0x0400218E RID: 8590
		public GameObject Kill;

		// Token: 0x0400218F RID: 8591
		public GameObject InfluencedBy;

		// Token: 0x04002190 RID: 8592
		public int Level;

		// Token: 0x04002191 RID: 8593
		public bool Detail;
	}
}
