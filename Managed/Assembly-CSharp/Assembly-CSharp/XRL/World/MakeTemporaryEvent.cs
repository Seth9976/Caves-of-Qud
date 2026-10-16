using System;

namespace XRL.World
{
	// Token: 0x02000802 RID: 2050
	[GameEvent(Cascade = 271, Cache = MinEvent.Cache.Pool)]
	public class MakeTemporaryEvent : PooledEvent<MakeTemporaryEvent>
	{
		// Token: 0x06005201 RID: 20993 RVA: 0x0018B74E File Offset: 0x0018994E
		public override int GetCascadeLevel()
		{
			return MakeTemporaryEvent.CascadeLevel;
		}

		// Token: 0x06005202 RID: 20994 RVA: 0x0018B755 File Offset: 0x00189955
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005203 RID: 20995 RVA: 0x0018B75E File Offset: 0x0018995E
		public override void Reset()
		{
			base.Reset();
			this.RootObject = null;
			this.Duration = 0;
			this.TurnInto = null;
			this.DependsOn = null;
			this.RootObjectValidateEveryTurn = false;
		}

		// Token: 0x06005204 RID: 20996 RVA: 0x0018B78C File Offset: 0x0018998C
		public static bool Send(GameObject RootObject, int Duration = -1, string TurnInto = null, GameObject DependsOn = null, bool RootObjectValidateEveryTurn = false)
		{
			MakeTemporaryEvent makeTemporaryEvent = PooledEvent<MakeTemporaryEvent>.FromPool();
			makeTemporaryEvent.RootObject = RootObject;
			makeTemporaryEvent.Duration = Duration;
			makeTemporaryEvent.TurnInto = TurnInto;
			makeTemporaryEvent.DependsOn = DependsOn;
			makeTemporaryEvent.RootObjectValidateEveryTurn = RootObjectValidateEveryTurn;
			return RootObject.HandleEvent(makeTemporaryEvent);
		}

		// Token: 0x04002929 RID: 10537
		public new static readonly int CascadeLevel = 271;

		// Token: 0x0400292A RID: 10538
		public GameObject RootObject;

		// Token: 0x0400292B RID: 10539
		public int Duration;

		// Token: 0x0400292C RID: 10540
		public string TurnInto;

		// Token: 0x0400292D RID: 10541
		public GameObject DependsOn;

		// Token: 0x0400292E RID: 10542
		public bool RootObjectValidateEveryTurn;
	}
}
