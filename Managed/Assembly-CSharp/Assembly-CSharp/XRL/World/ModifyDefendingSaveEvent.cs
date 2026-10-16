using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000809 RID: 2057
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ModifyDefendingSaveEvent : ISaveEvent
	{
		// Token: 0x06005231 RID: 21041 RVA: 0x0018C058 File Offset: 0x0018A258
		public ModifyDefendingSaveEvent()
		{
			this.ID = ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x06005232 RID: 21042 RVA: 0x0018C06B File Offset: 0x0018A26B
		public static int CountPool()
		{
			if (ModifyDefendingSaveEvent.Pool != null)
			{
				return ModifyDefendingSaveEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005233 RID: 21043 RVA: 0x0018C080 File Offset: 0x0018A280
		public static void ResetPool()
		{
			while (ModifyDefendingSaveEvent.PoolCounter > 0)
			{
				ModifyDefendingSaveEvent.Pool[--ModifyDefendingSaveEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005234 RID: 21044 RVA: 0x0018C0A8 File Offset: 0x0018A2A8
		public static void ResetTo(ref ModifyDefendingSaveEvent E)
		{
			MinEvent.ResetTo<ModifyDefendingSaveEvent>(E, ModifyDefendingSaveEvent.Pool, ref ModifyDefendingSaveEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005235 RID: 21045 RVA: 0x0018C0BE File Offset: 0x0018A2BE
		public static ModifyDefendingSaveEvent FromPool()
		{
			return MinEvent.FromPool<ModifyDefendingSaveEvent>(ref ModifyDefendingSaveEvent.Pool, ref ModifyDefendingSaveEvent.PoolCounter, 8192);
		}

		// Token: 0x06005236 RID: 21046 RVA: 0x0018C0D4 File Offset: 0x0018A2D4
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005237 RID: 21047 RVA: 0x0018C0E8 File Offset: 0x0018A2E8
		public static ModifyDefendingSaveEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, int Roll, int BaseDifficulty, int Difficulty, bool IgnoreNatural1, bool IgnoreNatural20, bool Actual)
		{
			ModifyDefendingSaveEvent modifyDefendingSaveEvent = ModifyDefendingSaveEvent.FromPool();
			modifyDefendingSaveEvent.Attacker = Attacker;
			modifyDefendingSaveEvent.Defender = Defender;
			modifyDefendingSaveEvent.Source = Source;
			modifyDefendingSaveEvent.Stat = Stat;
			modifyDefendingSaveEvent.AttackerStat = AttackerStat;
			modifyDefendingSaveEvent.Vs = Vs;
			modifyDefendingSaveEvent.NaturalRoll = NaturalRoll;
			modifyDefendingSaveEvent.Roll = Roll;
			modifyDefendingSaveEvent.BaseDifficulty = BaseDifficulty;
			modifyDefendingSaveEvent.Difficulty = Difficulty;
			modifyDefendingSaveEvent.Actual = Actual;
			return modifyDefendingSaveEvent;
		}

		// Token: 0x06005238 RID: 21048 RVA: 0x0018C150 File Offset: 0x0018A350
		public static bool Process(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, ref int Roll, int BaseDifficulty, ref int Difficulty, ref bool IgnoreNatural1, ref bool IgnoreNatural20, bool Actual)
		{
			return ISaveEvent.Process(Defender, "ModifyDefendingSave", ModifyDefendingSaveEvent.ID, ISaveEvent.CascadeLevel, new ISaveEvent.ISaveEventGenerator(ModifyDefendingSaveEvent.FromPool), Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, Actual);
		}

		// Token: 0x0400294F RID: 10575
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ModifyDefendingSaveEvent), null, new MinEvent.EventPoolCount(ModifyDefendingSaveEvent.CountPool), new MinEvent.EventPoolReset(ModifyDefendingSaveEvent.ResetPool));

		// Token: 0x04002950 RID: 10576
		private static List<ModifyDefendingSaveEvent> Pool;

		// Token: 0x04002951 RID: 10577
		private static int PoolCounter;
	}
}
