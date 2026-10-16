using System;

namespace XRL.World
{
	// Token: 0x02000846 RID: 2118
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ReplaceInContextEvent : PooledEvent<ReplaceInContextEvent>
	{
		// Token: 0x0600540E RID: 21518 RVA: 0x0019103F File Offset: 0x0018F23F
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600540F RID: 21519 RVA: 0x00191048 File Offset: 0x0018F248
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Replacement = null;
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x0019105E File Offset: 0x0018F25E
		public static ReplaceInContextEvent FromPool(GameObject Object, GameObject Replacement)
		{
			ReplaceInContextEvent replaceInContextEvent = PooledEvent<ReplaceInContextEvent>.FromPool();
			replaceInContextEvent.Object = Object;
			replaceInContextEvent.Replacement = Replacement;
			return replaceInContextEvent;
		}

		// Token: 0x06005411 RID: 21521 RVA: 0x00191073 File Offset: 0x0018F273
		public static void Send(GameObject Object, GameObject Replacement)
		{
			if (GameObject.Validate(ref Object))
			{
				Object.HandleEvent(ReplaceInContextEvent.FromPool(Object, Replacement));
			}
		}

		// Token: 0x04002A3C RID: 10812
		public GameObject Object;

		// Token: 0x04002A3D RID: 10813
		public GameObject Replacement;
	}
}
