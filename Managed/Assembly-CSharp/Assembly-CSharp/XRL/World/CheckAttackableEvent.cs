using System;

namespace XRL.World
{
	// Token: 0x020006D9 RID: 1753
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class CheckAttackableEvent : PooledEvent<CheckAttackableEvent>
	{
		// Token: 0x060049ED RID: 18925 RVA: 0x00172E78 File Offset: 0x00171078
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x00172E81 File Offset: 0x00171081
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.Attacker = null;
		}

		// Token: 0x060049EF RID: 18927 RVA: 0x00172E97 File Offset: 0x00171097
		public static CheckAttackableEvent FromPool(GameObject Object, GameObject Attacker)
		{
			CheckAttackableEvent checkAttackableEvent = PooledEvent<CheckAttackableEvent>.FromPool();
			checkAttackableEvent.Object = Object;
			checkAttackableEvent.Attacker = Attacker;
			return checkAttackableEvent;
		}

		// Token: 0x060049F0 RID: 18928 RVA: 0x00172EAC File Offset: 0x001710AC
		public static bool Check(GameObject Object, GameObject Attacker)
		{
			return !GameObject.Validate(ref Object) || !Object.WantEvent(PooledEvent<CheckAttackableEvent>.ID, MinEvent.CascadeLevel) || Object.HandleEvent(CheckAttackableEvent.FromPool(Object, Attacker));
		}

		// Token: 0x04002383 RID: 9091
		public GameObject Object;

		// Token: 0x04002384 RID: 9092
		public GameObject Attacker;
	}
}
