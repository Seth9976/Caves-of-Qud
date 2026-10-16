using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000856 RID: 2134
	[GameEvent(Cache = MinEvent.Cache.Pool, Cascade = 17)]
	public class ShieldSlamPerformedEvent : MinEvent
	{
		// Token: 0x06005537 RID: 21815 RVA: 0x00197F10 File Offset: 0x00196110
		public ShieldSlamPerformedEvent()
		{
			this.ID = ShieldSlamPerformedEvent.ID;
		}

		// Token: 0x06005538 RID: 21816 RVA: 0x00197F23 File Offset: 0x00196123
		public override int GetCascadeLevel()
		{
			return ShieldSlamPerformedEvent.CascadeLevel;
		}

		// Token: 0x06005539 RID: 21817 RVA: 0x00197F2A File Offset: 0x0019612A
		public static int CountPool()
		{
			if (ShieldSlamPerformedEvent.Pool != null)
			{
				return ShieldSlamPerformedEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600553A RID: 21818 RVA: 0x00197F3F File Offset: 0x0019613F
		public static void ResetPool()
		{
			while (ShieldSlamPerformedEvent.PoolCounter > 0)
			{
				ShieldSlamPerformedEvent.Pool[--ShieldSlamPerformedEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600553B RID: 21819 RVA: 0x00197F67 File Offset: 0x00196167
		public static void ResetTo(ref ShieldSlamPerformedEvent E)
		{
			MinEvent.ResetTo<ShieldSlamPerformedEvent>(E, ShieldSlamPerformedEvent.Pool, ref ShieldSlamPerformedEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600553C RID: 21820 RVA: 0x00197F7D File Offset: 0x0019617D
		public static ShieldSlamPerformedEvent FromPool()
		{
			return MinEvent.FromPool<ShieldSlamPerformedEvent>(ref ShieldSlamPerformedEvent.Pool, ref ShieldSlamPerformedEvent.PoolCounter, 8192);
		}

		// Token: 0x0600553D RID: 21821 RVA: 0x00197F93 File Offset: 0x00196193
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x0600553E RID: 21822 RVA: 0x00197F9C File Offset: 0x0019619C
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Shield = null;
			this.ShieldAV = 0;
			this.Damage = 0;
		}

		// Token: 0x0600553F RID: 21823 RVA: 0x00197FC8 File Offset: 0x001961C8
		public static void Send(GameObject Actor, GameObject Target, GameObject Shield, int ShieldAV, int Damage = 0)
		{
			if (true && GameObject.Validate(ref Actor) && Actor.WantEvent(ShieldSlamPerformedEvent.ID, ShieldSlamPerformedEvent.CascadeLevel))
			{
				ShieldSlamPerformedEvent shieldSlamPerformedEvent = ShieldSlamPerformedEvent.FromPool();
				shieldSlamPerformedEvent.Actor = Actor;
				shieldSlamPerformedEvent.Target = Target;
				shieldSlamPerformedEvent.Shield = Shield;
				shieldSlamPerformedEvent.ShieldAV = ShieldAV;
				shieldSlamPerformedEvent.Damage = Damage;
				Actor.HandleEvent(shieldSlamPerformedEvent);
			}
		}

		// Token: 0x04002ADB RID: 10971
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ShieldSlamPerformedEvent), null, new MinEvent.EventPoolCount(ShieldSlamPerformedEvent.CountPool), new MinEvent.EventPoolReset(ShieldSlamPerformedEvent.ResetPool));

		// Token: 0x04002ADC RID: 10972
		public new static readonly int CascadeLevel = 17;

		// Token: 0x04002ADD RID: 10973
		private static List<ShieldSlamPerformedEvent> Pool;

		// Token: 0x04002ADE RID: 10974
		private static int PoolCounter;

		// Token: 0x04002ADF RID: 10975
		public GameObject Actor;

		// Token: 0x04002AE0 RID: 10976
		public GameObject Target;

		// Token: 0x04002AE1 RID: 10977
		public GameObject Shield;

		// Token: 0x04002AE2 RID: 10978
		public int ShieldAV;

		// Token: 0x04002AE3 RID: 10979
		public int Damage;
	}
}
