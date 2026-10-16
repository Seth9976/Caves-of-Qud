using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000838 RID: 2104
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class QuestFinishedEvent : IQuestEvent
	{
		// Token: 0x060053A8 RID: 21416 RVA: 0x001901D3 File Offset: 0x0018E3D3
		public QuestFinishedEvent()
		{
			this.ID = QuestFinishedEvent.ID;
		}

		// Token: 0x060053A9 RID: 21417 RVA: 0x001901E6 File Offset: 0x0018E3E6
		public override int GetCascadeLevel()
		{
			return QuestFinishedEvent.CascadeLevel;
		}

		// Token: 0x060053AA RID: 21418 RVA: 0x001901ED File Offset: 0x0018E3ED
		public static int CountPool()
		{
			if (QuestFinishedEvent.Pool != null)
			{
				return QuestFinishedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053AB RID: 21419 RVA: 0x00190202 File Offset: 0x0018E402
		public static void ResetPool()
		{
			while (QuestFinishedEvent.PoolCounter > 0)
			{
				QuestFinishedEvent.Pool[--QuestFinishedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053AC RID: 21420 RVA: 0x0019022A File Offset: 0x0018E42A
		public static void ResetTo(ref QuestFinishedEvent E)
		{
			MinEvent.ResetTo<QuestFinishedEvent>(E, QuestFinishedEvent.Pool, ref QuestFinishedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053AD RID: 21421 RVA: 0x00190240 File Offset: 0x0018E440
		public static QuestFinishedEvent FromPool()
		{
			return MinEvent.FromPool<QuestFinishedEvent>(ref QuestFinishedEvent.Pool, ref QuestFinishedEvent.PoolCounter, 8192);
		}

		// Token: 0x060053AE RID: 21422 RVA: 0x00190256 File Offset: 0x0018E456
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053AF RID: 21423 RVA: 0x0019026C File Offset: 0x0018E46C
		public static void Send(Quest Quest)
		{
			QuestFinishedEvent questFinishedEvent = QuestFinishedEvent.FromPool();
			questFinishedEvent.Quest = Quest;
			The.Game.HandleEvent(questFinishedEvent, true);
			QuestFinishedEvent.ResetTo(ref questFinishedEvent);
		}

		// Token: 0x04002A14 RID: 10772
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QuestFinishedEvent), null, new MinEvent.EventPoolCount(QuestFinishedEvent.CountPool), new MinEvent.EventPoolReset(QuestFinishedEvent.ResetPool));

		// Token: 0x04002A15 RID: 10773
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002A16 RID: 10774
		private static List<QuestFinishedEvent> Pool;

		// Token: 0x04002A17 RID: 10775
		private static int PoolCounter;
	}
}
