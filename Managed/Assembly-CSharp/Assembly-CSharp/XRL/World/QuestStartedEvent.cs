using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000839 RID: 2105
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class QuestStartedEvent : OnQuestAddedEvent
	{
		// Token: 0x060053B1 RID: 21425 RVA: 0x001902D0 File Offset: 0x0018E4D0
		public QuestStartedEvent()
		{
			this.ID = QuestStartedEvent.ID;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x001902E3 File Offset: 0x0018E4E3
		public override int GetCascadeLevel()
		{
			return QuestStartedEvent.CascadeLevel;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x001902EA File Offset: 0x0018E4EA
		public static int CountPool()
		{
			if (QuestStartedEvent.Pool != null)
			{
				return QuestStartedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x001902FF File Offset: 0x0018E4FF
		public static void ResetPool()
		{
			while (QuestStartedEvent.PoolCounter > 0)
			{
				QuestStartedEvent.Pool[--QuestStartedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x00190327 File Offset: 0x0018E527
		public static void ResetTo(ref QuestStartedEvent E)
		{
			MinEvent.ResetTo<QuestStartedEvent>(E, QuestStartedEvent.Pool, ref QuestStartedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0019033D File Offset: 0x0018E53D
		public static QuestStartedEvent FromPool()
		{
			return MinEvent.FromPool<QuestStartedEvent>(ref QuestStartedEvent.Pool, ref QuestStartedEvent.PoolCounter, 8192);
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x00190353 File Offset: 0x0018E553
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053B8 RID: 21432 RVA: 0x00190368 File Offset: 0x0018E568
		public static void Send(Quest Quest)
		{
			QuestStartedEvent questStartedEvent = QuestStartedEvent.FromPool();
			questStartedEvent.Quest = Quest;
			The.Game.HandleEvent(questStartedEvent, true);
			QuestStartedEvent.ResetTo(ref questStartedEvent);
		}

		// Token: 0x04002A18 RID: 10776
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QuestStartedEvent), null, new MinEvent.EventPoolCount(QuestStartedEvent.CountPool), new MinEvent.EventPoolReset(QuestStartedEvent.ResetPool));

		// Token: 0x04002A19 RID: 10777
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002A1A RID: 10778
		private static List<QuestStartedEvent> Pool;

		// Token: 0x04002A1B RID: 10779
		private static int PoolCounter;
	}
}
