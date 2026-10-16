using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000803 RID: 2051
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class MentalAttackEvent : IMentalAttackEvent
	{
		// Token: 0x06005207 RID: 20999 RVA: 0x0018B7E3 File Offset: 0x001899E3
		public MentalAttackEvent()
		{
			this.ID = MentalAttackEvent.ID;
		}

		// Token: 0x06005208 RID: 21000 RVA: 0x0018B7F6 File Offset: 0x001899F6
		public static int CountPool()
		{
			if (MentalAttackEvent.Pool != null)
			{
				return MentalAttackEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005209 RID: 21001 RVA: 0x0018B80B File Offset: 0x00189A0B
		public static void ResetPool()
		{
			while (MentalAttackEvent.PoolCounter > 0)
			{
				MentalAttackEvent.Pool[--MentalAttackEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600520A RID: 21002 RVA: 0x0018B833 File Offset: 0x00189A33
		public static void ResetTo(ref MentalAttackEvent E)
		{
			MinEvent.ResetTo<MentalAttackEvent>(E, MentalAttackEvent.Pool, ref MentalAttackEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600520B RID: 21003 RVA: 0x0018B849 File Offset: 0x00189A49
		public static MentalAttackEvent FromPool()
		{
			return MinEvent.FromPool<MentalAttackEvent>(ref MentalAttackEvent.Pool, ref MentalAttackEvent.PoolCounter, 8192);
		}

		// Token: 0x0600520C RID: 21004 RVA: 0x0018B85F File Offset: 0x00189A5F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x0600520D RID: 21005 RVA: 0x0018B873 File Offset: 0x00189A73
		public static MentalAttackEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Source = null, string Command = null, string Dice = null, int Type = 0, int Magnitude = -2147483648)
		{
			MentalAttackEvent mentalAttackEvent = MentalAttackEvent.FromPool();
			mentalAttackEvent.Attacker = Attacker;
			mentalAttackEvent.Defender = Defender;
			mentalAttackEvent.Source = Source;
			mentalAttackEvent.Command = Command;
			mentalAttackEvent.Dice = Dice;
			mentalAttackEvent.Type = Type;
			mentalAttackEvent.Magnitude = Magnitude;
			return mentalAttackEvent;
		}

		// Token: 0x0400292F RID: 10543
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(MentalAttackEvent), null, new MinEvent.EventPoolCount(MentalAttackEvent.CountPool), new MinEvent.EventPoolReset(MentalAttackEvent.ResetPool));

		// Token: 0x04002930 RID: 10544
		private static List<MentalAttackEvent> Pool;

		// Token: 0x04002931 RID: 10545
		private static int PoolCounter;
	}
}
