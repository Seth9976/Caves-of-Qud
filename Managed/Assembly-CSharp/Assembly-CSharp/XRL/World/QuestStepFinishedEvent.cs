using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200083A RID: 2106
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class QuestStepFinishedEvent : IQuestEvent
	{
		// Token: 0x060053BA RID: 21434 RVA: 0x001903CC File Offset: 0x0018E5CC
		public QuestStepFinishedEvent()
		{
			this.ID = QuestStepFinishedEvent.ID;
		}

		// Token: 0x060053BB RID: 21435 RVA: 0x001903DF File Offset: 0x0018E5DF
		public override int GetCascadeLevel()
		{
			return QuestStepFinishedEvent.CascadeLevel;
		}

		// Token: 0x060053BC RID: 21436 RVA: 0x001903E6 File Offset: 0x0018E5E6
		public static int CountPool()
		{
			if (QuestStepFinishedEvent.Pool != null)
			{
				return QuestStepFinishedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060053BD RID: 21437 RVA: 0x001903FB File Offset: 0x0018E5FB
		public static void ResetPool()
		{
			while (QuestStepFinishedEvent.PoolCounter > 0)
			{
				QuestStepFinishedEvent.Pool[--QuestStepFinishedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060053BE RID: 21438 RVA: 0x00190423 File Offset: 0x0018E623
		public static void ResetTo(ref QuestStepFinishedEvent E)
		{
			MinEvent.ResetTo<QuestStepFinishedEvent>(E, QuestStepFinishedEvent.Pool, ref QuestStepFinishedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x00190439 File Offset: 0x0018E639
		public static QuestStepFinishedEvent FromPool()
		{
			return MinEvent.FromPool<QuestStepFinishedEvent>(ref QuestStepFinishedEvent.Pool, ref QuestStepFinishedEvent.PoolCounter, 8192);
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0019044F File Offset: 0x0018E64F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x00190463 File Offset: 0x0018E663
		public override void Reset()
		{
			base.Reset();
			this.Step = null;
		}

		// Token: 0x060053C2 RID: 21442 RVA: 0x00190474 File Offset: 0x0018E674
		public static void Send(Quest Quest, QuestStep Step)
		{
			QuestStepFinishedEvent questStepFinishedEvent = QuestStepFinishedEvent.FromPool();
			questStepFinishedEvent.Quest = Quest;
			questStepFinishedEvent.Step = Step;
			The.Game.HandleEvent(questStepFinishedEvent, true);
			QuestStepFinishedEvent.ResetTo(ref questStepFinishedEvent);
		}

		// Token: 0x04002A1C RID: 10780
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(QuestStepFinishedEvent), null, new MinEvent.EventPoolCount(QuestStepFinishedEvent.CountPool), new MinEvent.EventPoolReset(QuestStepFinishedEvent.ResetPool));

		// Token: 0x04002A1D RID: 10781
		public new static readonly int CascadeLevel = 15;

		// Token: 0x04002A1E RID: 10782
		private static List<QuestStepFinishedEvent> Pool;

		// Token: 0x04002A1F RID: 10783
		private static int PoolCounter;

		// Token: 0x04002A20 RID: 10784
		public QuestStep Step;
	}
}
