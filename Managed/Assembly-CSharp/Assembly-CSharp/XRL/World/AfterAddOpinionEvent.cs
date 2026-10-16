using System;
using System.Diagnostics.CodeAnalysis;
using XRL.World.AI;

namespace XRL.World
{
	// Token: 0x02000653 RID: 1619
	[GameEvent(Cascade = 17, Cache = MinEvent.Cache.Pool)]
	public class AfterAddOpinionEvent : PooledEvent<AfterAddOpinionEvent>
	{
		// Token: 0x06004619 RID: 17945 RVA: 0x00165A6D File Offset: 0x00163C6D
		public override int GetCascadeLevel()
		{
			return AfterAddOpinionEvent.CascadeLevel;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x00165A74 File Offset: 0x00163C74
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x00165A7D File Offset: 0x00163C7D
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Object = null;
			this.Opinion = null;
			this.Renew = false;
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x00165AA8 File Offset: 0x00163CA8
		public static void Send(IOpinion Opinion, GameObject Actor, GameObject Subject, GameObject Object = null, bool Renew = false)
		{
			AfterAddOpinionEvent afterAddOpinionEvent = PooledEvent<AfterAddOpinionEvent>.FromPool();
			afterAddOpinionEvent.Opinion = Opinion;
			afterAddOpinionEvent.Actor = Actor;
			afterAddOpinionEvent.Subject = Subject;
			afterAddOpinionEvent.Object = Object;
			afterAddOpinionEvent.Renew = Renew;
			if (Opinion.HandleEvent(afterAddOpinionEvent) && Actor.HandleEvent(afterAddOpinionEvent))
			{
				Subject.HandleEvent(afterAddOpinionEvent);
			}
			PooledEvent<AfterAddOpinionEvent>.ResetTo(ref afterAddOpinionEvent);
		}

		// Token: 0x04002142 RID: 8514
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002143 RID: 8515
		[NotNull]
		public GameObject Actor;

		// Token: 0x04002144 RID: 8516
		[NotNull]
		public GameObject Subject;

		// Token: 0x04002145 RID: 8517
		public GameObject Object;

		// Token: 0x04002146 RID: 8518
		[NotNull]
		public IOpinion Opinion;

		// Token: 0x04002147 RID: 8519
		public bool Renew;
	}
}
