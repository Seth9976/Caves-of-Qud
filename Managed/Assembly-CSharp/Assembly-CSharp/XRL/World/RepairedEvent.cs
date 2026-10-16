using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x02000845 RID: 2117
	[GameEvent(Cascade = 256, Cache = MinEvent.Cache.Pool)]
	public class RepairedEvent : PooledEvent<RepairedEvent>
	{
		// Token: 0x06005408 RID: 21512 RVA: 0x00190FA0 File Offset: 0x0018F1A0
		public override int GetCascadeLevel()
		{
			return RepairedEvent.CascadeLevel;
		}

		// Token: 0x06005409 RID: 21513 RVA: 0x00190FA7 File Offset: 0x0018F1A7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600540A RID: 21514 RVA: 0x00190FB0 File Offset: 0x0018F1B0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Tool = null;
			this.Skill = null;
			this.MaxRepairTier = 0;
		}

		// Token: 0x0600540B RID: 21515 RVA: 0x00190FDC File Offset: 0x0018F1DC
		public static void Send(GameObject Actor = null, GameObject Subject = null, GameObject Tool = null, BaseSkill Skill = null, int MaxRepairTier = 0)
		{
			if (GameObject.Validate(ref Subject))
			{
				RepairedEvent repairedEvent = PooledEvent<RepairedEvent>.FromPool();
				repairedEvent.Actor = Actor;
				repairedEvent.Subject = Subject;
				repairedEvent.Tool = Tool;
				repairedEvent.Skill = Skill;
				repairedEvent.MaxRepairTier = MaxRepairTier;
				Subject.HandleEvent(repairedEvent);
				PooledEvent<RepairedEvent>.ResetTo(ref repairedEvent);
			}
		}

		// Token: 0x04002A36 RID: 10806
		public new static readonly int CascadeLevel = 256;

		// Token: 0x04002A37 RID: 10807
		public GameObject Actor;

		// Token: 0x04002A38 RID: 10808
		public GameObject Subject;

		// Token: 0x04002A39 RID: 10809
		public GameObject Tool;

		// Token: 0x04002A3A RID: 10810
		public BaseSkill Skill;

		// Token: 0x04002A3B RID: 10811
		public int MaxRepairTier;
	}
}
