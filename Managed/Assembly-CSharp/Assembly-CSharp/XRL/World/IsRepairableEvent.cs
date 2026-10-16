using System;
using XRL.World.Parts.Skill;

namespace XRL.World
{
	// Token: 0x020007F2 RID: 2034
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class IsRepairableEvent : PooledEvent<IsRepairableEvent>
	{
		// Token: 0x06005197 RID: 20887 RVA: 0x0018A1C7 File Offset: 0x001883C7
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06005198 RID: 20888 RVA: 0x0018A1D0 File Offset: 0x001883D0
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Subject = null;
			this.Tool = null;
			this.Skill = null;
			this.MaxRepairTier = null;
		}

		// Token: 0x06005199 RID: 20889 RVA: 0x0018A200 File Offset: 0x00188400
		public static bool Check(GameObject Actor = null, GameObject Subject = null, GameObject Tool = null, BaseSkill Skill = null, int? MaxRepairTier = null)
		{
			bool flag = false;
			if (GameObject.Validate(ref Subject))
			{
				IsRepairableEvent isRepairableEvent = PooledEvent<IsRepairableEvent>.FromPool();
				isRepairableEvent.Actor = Actor;
				isRepairableEvent.Subject = Subject;
				isRepairableEvent.Tool = Tool;
				isRepairableEvent.Skill = Skill;
				isRepairableEvent.MaxRepairTier = MaxRepairTier;
				flag = !Subject.HandleEvent(isRepairableEvent);
				PooledEvent<IsRepairableEvent>.ResetTo(ref isRepairableEvent);
			}
			return flag;
		}

		// Token: 0x040028E9 RID: 10473
		public GameObject Actor;

		// Token: 0x040028EA RID: 10474
		public GameObject Subject;

		// Token: 0x040028EB RID: 10475
		public GameObject Tool;

		// Token: 0x040028EC RID: 10476
		public BaseSkill Skill;

		// Token: 0x040028ED RID: 10477
		public int? MaxRepairTier;
	}
}
