using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x0200080A RID: 2058
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ModifyOriginatingSaveEvent : ISaveEvent
	{
		// Token: 0x0600523A RID: 21050 RVA: 0x0018C1C3 File Offset: 0x0018A3C3
		public ModifyOriginatingSaveEvent()
		{
			this.ID = ModifyOriginatingSaveEvent.ID;
		}

		// Token: 0x0600523B RID: 21051 RVA: 0x0018C1D6 File Offset: 0x0018A3D6
		public static int CountPool()
		{
			if (ModifyOriginatingSaveEvent.Pool != null)
			{
				return ModifyOriginatingSaveEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x0600523C RID: 21052 RVA: 0x0018C1EB File Offset: 0x0018A3EB
		public static void ResetPool()
		{
			while (ModifyOriginatingSaveEvent.PoolCounter > 0)
			{
				ModifyOriginatingSaveEvent.Pool[--ModifyOriginatingSaveEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x0600523D RID: 21053 RVA: 0x0018C213 File Offset: 0x0018A413
		public static void ResetTo(ref ModifyOriginatingSaveEvent E)
		{
			MinEvent.ResetTo<ModifyOriginatingSaveEvent>(E, ModifyOriginatingSaveEvent.Pool, ref ModifyOriginatingSaveEvent.PoolCounter);
			E = null;
		}

		// Token: 0x0600523E RID: 21054 RVA: 0x0018C229 File Offset: 0x0018A429
		public static ModifyOriginatingSaveEvent FromPool()
		{
			return MinEvent.FromPool<ModifyOriginatingSaveEvent>(ref ModifyOriginatingSaveEvent.Pool, ref ModifyOriginatingSaveEvent.PoolCounter, 8192);
		}

		// Token: 0x0600523F RID: 21055 RVA: 0x0018C23F File Offset: 0x0018A43F
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005240 RID: 21056 RVA: 0x0018C254 File Offset: 0x0018A454
		public static ModifyOriginatingSaveEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, int Roll, int BaseDifficulty, int Difficulty, bool IgnoreNatural1, bool IgnoreNatural20, bool Actual)
		{
			ModifyOriginatingSaveEvent modifyOriginatingSaveEvent = ModifyOriginatingSaveEvent.FromPool();
			modifyOriginatingSaveEvent.Attacker = Attacker;
			modifyOriginatingSaveEvent.Defender = Defender;
			modifyOriginatingSaveEvent.Source = Source;
			modifyOriginatingSaveEvent.Stat = Stat;
			modifyOriginatingSaveEvent.AttackerStat = AttackerStat;
			modifyOriginatingSaveEvent.Vs = Vs;
			modifyOriginatingSaveEvent.NaturalRoll = NaturalRoll;
			modifyOriginatingSaveEvent.Roll = Roll;
			modifyOriginatingSaveEvent.BaseDifficulty = BaseDifficulty;
			modifyOriginatingSaveEvent.Difficulty = Difficulty;
			modifyOriginatingSaveEvent.Actual = Actual;
			return modifyOriginatingSaveEvent;
		}

		// Token: 0x06005241 RID: 21057 RVA: 0x0018C2BC File Offset: 0x0018A4BC
		public static bool Process(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, ref int Roll, int BaseDifficulty, ref int Difficulty, ref bool IgnoreNatural1, ref bool IgnoreNatural20, bool Actual)
		{
			return ISaveEvent.Process(Attacker, "ModifyOriginatingSave", ModifyOriginatingSaveEvent.ID, ISaveEvent.CascadeLevel, new ISaveEvent.ISaveEventGenerator(ModifyOriginatingSaveEvent.FromPool), Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, Actual);
		}

		// Token: 0x04002952 RID: 10578
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ModifyOriginatingSaveEvent), null, new MinEvent.EventPoolCount(ModifyOriginatingSaveEvent.CountPool), new MinEvent.EventPoolReset(ModifyOriginatingSaveEvent.ResetPool));

		// Token: 0x04002953 RID: 10579
		private static List<ModifyOriginatingSaveEvent> Pool;

		// Token: 0x04002954 RID: 10580
		private static int PoolCounter;
	}
}
