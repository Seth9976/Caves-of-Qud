using System;

namespace XRL.World
{
	// Token: 0x020006D6 RID: 1750
	[GameEvent(Cascade = 15, Cache = MinEvent.Cache.Pool)]
	public class CheckAnythingToCleanEvent : PooledEvent<CheckAnythingToCleanEvent>
	{
		// Token: 0x060049DA RID: 18906 RVA: 0x00172C81 File Offset: 0x00170E81
		public override int GetCascadeLevel()
		{
			return CheckAnythingToCleanEvent.CascadeLevel;
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x00172C88 File Offset: 0x00170E88
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049DC RID: 18908 RVA: 0x00172C91 File Offset: 0x00170E91
		public override void Reset()
		{
			base.Reset();
			this.CascadeFrom = null;
			this.Using = null;
		}

		// Token: 0x060049DD RID: 18909 RVA: 0x00172CA7 File Offset: 0x00170EA7
		public static CheckAnythingToCleanEvent FromPool(GameObject CascadeFrom, GameObject Using = null)
		{
			CheckAnythingToCleanEvent checkAnythingToCleanEvent = PooledEvent<CheckAnythingToCleanEvent>.FromPool();
			checkAnythingToCleanEvent.CascadeFrom = CascadeFrom;
			checkAnythingToCleanEvent.Using = Using;
			return checkAnythingToCleanEvent;
		}

		// Token: 0x060049DE RID: 18910 RVA: 0x00172CBC File Offset: 0x00170EBC
		public static bool Check(GameObject CascadeFrom, GameObject Using = null)
		{
			return GameObject.Validate(ref CascadeFrom) && CascadeFrom.WantEvent(PooledEvent<CheckAnythingToCleanEvent>.ID, CheckAnythingToCleanEvent.CascadeLevel) && !CascadeFrom.HandleEvent(CheckAnythingToCleanEvent.FromPool(CascadeFrom, Using));
		}

		// Token: 0x0400237B RID: 9083
		public new static readonly int CascadeLevel = 15;

		// Token: 0x0400237C RID: 9084
		public GameObject CascadeFrom;

		// Token: 0x0400237D RID: 9085
		public GameObject Using;
	}
}
