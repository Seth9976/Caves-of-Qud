using System;
using System.Collections.Generic;

namespace XRL.World
{
	// Token: 0x02000807 RID: 2055
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class ModifyAttackingSaveEvent : ISaveEvent
	{
		// Token: 0x06005221 RID: 21025 RVA: 0x0018BDFF File Offset: 0x00189FFF
		public ModifyAttackingSaveEvent()
		{
			this.ID = ModifyAttackingSaveEvent.ID;
		}

		// Token: 0x06005222 RID: 21026 RVA: 0x0018BE12 File Offset: 0x0018A012
		public static int CountPool()
		{
			if (ModifyAttackingSaveEvent.Pool != null)
			{
				return ModifyAttackingSaveEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x06005223 RID: 21027 RVA: 0x0018BE27 File Offset: 0x0018A027
		public static void ResetPool()
		{
			while (ModifyAttackingSaveEvent.PoolCounter > 0)
			{
				ModifyAttackingSaveEvent.Pool[--ModifyAttackingSaveEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x06005224 RID: 21028 RVA: 0x0018BE4F File Offset: 0x0018A04F
		public static void ResetTo(ref ModifyAttackingSaveEvent E)
		{
			MinEvent.ResetTo<ModifyAttackingSaveEvent>(E, ModifyAttackingSaveEvent.Pool, ref ModifyAttackingSaveEvent.PoolCounter);
			E = null;
		}

		// Token: 0x06005225 RID: 21029 RVA: 0x0018BE65 File Offset: 0x0018A065
		public static ModifyAttackingSaveEvent FromPool()
		{
			return MinEvent.FromPool<ModifyAttackingSaveEvent>(ref ModifyAttackingSaveEvent.Pool, ref ModifyAttackingSaveEvent.PoolCounter, 8192);
		}

		// Token: 0x06005226 RID: 21030 RVA: 0x0018BE7B File Offset: 0x0018A07B
		public override bool Dispatch(IEventHandler Handler)
		{
			return base.Dispatch(Handler) && Handler.HandleEvent(this);
		}

		// Token: 0x06005227 RID: 21031 RVA: 0x0018BE90 File Offset: 0x0018A090
		public static ModifyAttackingSaveEvent FromPool(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, int Roll, int BaseDifficulty, int Difficulty, bool IgnoreNatural1, bool IgnoreNatural20, bool Actual)
		{
			ModifyAttackingSaveEvent modifyAttackingSaveEvent = ModifyAttackingSaveEvent.FromPool();
			modifyAttackingSaveEvent.Attacker = Attacker;
			modifyAttackingSaveEvent.Defender = Defender;
			modifyAttackingSaveEvent.Source = Source;
			modifyAttackingSaveEvent.Stat = Stat;
			modifyAttackingSaveEvent.AttackerStat = AttackerStat;
			modifyAttackingSaveEvent.Vs = Vs;
			modifyAttackingSaveEvent.NaturalRoll = NaturalRoll;
			modifyAttackingSaveEvent.Roll = Roll;
			modifyAttackingSaveEvent.BaseDifficulty = BaseDifficulty;
			modifyAttackingSaveEvent.Difficulty = Difficulty;
			modifyAttackingSaveEvent.Actual = Actual;
			return modifyAttackingSaveEvent;
		}

		// Token: 0x06005228 RID: 21032 RVA: 0x0018BEF8 File Offset: 0x0018A0F8
		public static bool Process(GameObject Attacker, GameObject Defender, GameObject Source, string Stat, string AttackerStat, string Vs, int NaturalRoll, ref int Roll, int BaseDifficulty, ref int Difficulty, ref bool IgnoreNatural1, ref bool IgnoreNatural20, bool Actual)
		{
			return ISaveEvent.Process(Attacker, "ModifyAttackingSave", ModifyAttackingSaveEvent.ID, ISaveEvent.CascadeLevel, new ISaveEvent.ISaveEventGenerator(ModifyAttackingSaveEvent.FromPool), Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, Actual);
		}

		// Token: 0x04002948 RID: 10568
		public new static readonly int ID = MinEvent.RegisterEvent(typeof(ModifyAttackingSaveEvent), null, new MinEvent.EventPoolCount(ModifyAttackingSaveEvent.CountPool), new MinEvent.EventPoolReset(ModifyAttackingSaveEvent.ResetPool));

		// Token: 0x04002949 RID: 10569
		private static List<ModifyAttackingSaveEvent> Pool;

		// Token: 0x0400294A RID: 10570
		private static int PoolCounter;
	}
}
