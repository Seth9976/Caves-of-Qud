using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Genkit;
using XRL.Core;
using XRL.Messages;
using XRL.UI;
using XRL.World;

namespace XRL.Rules
{
	// Token: 0x020005DF RID: 1503
	public static class Stat
	{
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06004074 RID: 16500 RVA: 0x0013E851 File Offset: 0x0013CA51
		// (set) Token: 0x06004075 RID: 16501 RVA: 0x0013E858 File Offset: 0x0013CA58
		public static Random Rnd
		{
			get
			{
				return Stat._Rnd;
			}
			set
			{
				Stat._Rnd = value;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06004076 RID: 16502 RVA: 0x0013E860 File Offset: 0x0013CA60
		// (set) Token: 0x06004077 RID: 16503 RVA: 0x0013E870 File Offset: 0x0013CA70
		public static bool logRndAccess
		{
			get
			{
				Stat.rndAccessCount = 0;
				return Stat._logRndAccess;
			}
			set
			{
				if (value)
				{
					Stat.rndAccessLog = "c:\\logs\\rnglog" + Stat.rndAccessIndex.ToString() + ".txt";
					File.WriteAllText(Stat.rndAccessLog, "start\n");
					Stat.rndAccessIndex++;
					Stat.rndLogLines.Clear();
				}
				else
				{
					File.WriteAllLines(Stat.rndAccessLog, Stat.rndLogLines.ToArray());
				}
				Stat._logRndAccess = value;
				Stat.rndLogLines.Add("RND ACCESS COUNT:" + Stat._logRndAccess.ToString() + Environment.StackTrace.ToString() + "\n");
			}
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x0013E90C File Offset: 0x0013CB0C
		public static void ReseedFrom(string Seed, bool includeLifetimeSeeds = false)
		{
			if (XRLCore.Core != null && XRLCore.Core.Game != null)
			{
				Stat.ReseedFrom(XRLCore.Core.Game.GetWorldSeed(Seed), includeLifetimeSeeds);
			}
		}

		// Token: 0x06004079 RID: 16505 RVA: 0x0013E938 File Offset: 0x0013CB38
		public static void ReseedFrom(int Seed, bool includeLifetimeSeeds = false)
		{
			Stat.Rand = new Random(Hash.String("Seed0" + Seed.ToString()));
			Stat.Rnd = new Random(Hash.String("Seed1" + Seed.ToString()));
			Stat.Rnd2 = new Random(Hash.String("Seed2" + Seed.ToString()));
			if (includeLifetimeSeeds)
			{
				Stat.SeedLevelUpRandom(new Random(Hash.String("Seed3" + Seed.ToString())).Next());
				Stat.NamingRnd = new Random(Hash.String("Seed6" + Seed.ToString()));
			}
			Stat.Rnd4 = new Random(Hash.String("Seed4" + Seed.ToString()));
			Stat.Rnd5 = new Random(Hash.String("Seed5" + Seed.ToString()));
			Calc.Reseed(Hash.String("CalcSeed" + Seed.ToString()));
		}

		// Token: 0x0600407A RID: 16506 RVA: 0x0013EA48 File Offset: 0x0013CC48
		public static void PushState(string Seed, bool IncludeLifetime = false)
		{
			Stat.State.Push(new ValueTuple<Random[], int>(new Random[]
			{
				Stat.Rand,
				Stat.Rnd,
				Stat.Rnd2,
				IncludeLifetime ? Stat.NamingRnd : null,
				Stat.Rnd4,
				Stat.Rnd5,
				Calc.R
			}, IncludeLifetime ? The.Game.GetIntGameState("RandomSeed3", 0) : int.MinValue));
			Stat.ReseedFrom(Seed, IncludeLifetime);
		}

		// Token: 0x0600407B RID: 16507 RVA: 0x0013EACC File Offset: 0x0013CCCC
		public static void PopState()
		{
			if (Stat.State.IsReadOnlyNullOrEmpty<ValueTuple<Random[], int>>())
			{
				return;
			}
			ValueTuple<Random[], int> valueTuple = Stat.State.Pop();
			Random[] item = valueTuple.Item1;
			int item2 = valueTuple.Item2;
			Stat.Rand = item[0];
			Stat.Rnd = item[1];
			Stat.Rnd2 = item[2];
			if (item2 != -2147483648)
			{
				The.Game.SetIntGameState("RandomSeed3", item2);
			}
			Stat.NamingRnd = item[3] ?? Stat.NamingRnd;
			Stat.Rnd4 = item[4];
			Stat.Rnd5 = item[5];
			Calc.R = item[6];
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x0013EB58 File Offset: 0x0013CD58
		public static int RollDamagePenetrations(int TargetInclusive, int Bonus, int MaxBonus)
		{
			int num = 0;
			int num2 = 3;
			bool debugDamagePenetrations = Options.DebugDamagePenetrations;
			while (num2 == 3)
			{
				num2 = 0;
				for (int i = 0; i < 3; i++)
				{
					int num3 = Stat.Random(1, 10) - 2;
					int num4 = 0;
					while (num3 == 8)
					{
						num4 += 8;
						num3 = Stat.Random(1, 10) - 2;
					}
					num4 += num3;
					int num5 = num4 + Math.Min(Bonus, MaxBonus);
					if (num5 > TargetInclusive)
					{
						if (debugDamagePenetrations)
						{
							MessageQueue.AddPlayerMessage("Penned with Roll:" + num4.ToString() + " Final:" + num5.ToString(), null, true);
						}
						num2++;
					}
					else if (debugDamagePenetrations)
					{
						MessageQueue.AddPlayerMessage("Didn't pen with " + num4.ToString() + " Final:" + num5.ToString(), null, true);
					}
				}
				if (debugDamagePenetrations)
				{
					MessageQueue.AddPlayerMessage(string.Concat(new string[]
					{
						"{{K|Penning Bonus: ",
						Bonus.ToString(),
						" Max: ",
						MaxBonus.ToString(),
						" Used: ",
						Math.Min(Bonus, MaxBonus).ToString(),
						" Target: ",
						TargetInclusive.ToString(),
						"(Penned ",
						num2.ToString(),
						" times)}}"
					}), null, true);
				}
				if (num2 >= 1)
				{
					num++;
				}
				Bonus -= 2;
			}
			return num;
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x0013ECB7 File Offset: 0x0013CEB7
		public static int RandomLevelUpChoice(int Low, int High)
		{
			return Stat.WithLevelUpRandom<int>((Random rng) => rng.Next(Low, High + 1));
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x0013ECDC File Offset: 0x0013CEDC
		public static T WithLevelUpRandom<T>(Func<Random, T> proc)
		{
			Random random = null;
			if (XRLCore.Core.Game != null)
			{
				random = new Random(XRLCore.Core.Game.GetIntGameState("RandomSeed3", 0));
			}
			T t = proc(random);
			if (XRLCore.Core.Game != null)
			{
				XRLCore.Core.Game.SetIntGameState("RandomSeed3", random.Next());
			}
			return t;
		}

		/// <summary>This effectively pulls off a single use System.Random from RandomSeed3, then sets the next seed
		/// meerly getting this property will mutate it!</summary>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x0600407F RID: 16511 RVA: 0x0013ED3F File Offset: 0x0013CF3F
		public static Random LevelUpRandom
		{
			get
			{
				return Stat.WithLevelUpRandom<Random>((Random rng) => rng);
			}
		}

		// Token: 0x06004080 RID: 16512 RVA: 0x0013ED65 File Offset: 0x0013CF65
		private static void SeedLevelUpRandom(int seed)
		{
			if (XRLCore.Core.Game != null)
			{
				XRLCore.Core.Game.SetIntGameState("RandomSeed3", seed);
			}
		}

		// Token: 0x06004081 RID: 16513 RVA: 0x0013ED88 File Offset: 0x0013CF88
		public static int TinkerRandom(int Low, int High)
		{
			return Stat.Rnd4.Next(Low, High + 1);
		}

		// Token: 0x06004082 RID: 16514 RVA: 0x0013ED98 File Offset: 0x0013CF98
		public static int Random5(int Low, int High)
		{
			return Stat.Rnd5.Next(Low, High + 1);
		}

		// Token: 0x06004083 RID: 16515 RVA: 0x0013EDA8 File Offset: 0x0013CFA8
		public static int NamingRandom(int Low, int High)
		{
			return Stat.NamingRnd.Next(Low, High + 1);
		}

		// Token: 0x06004084 RID: 16516 RVA: 0x0013EDB8 File Offset: 0x0013CFB8
		public static int RandomCosmetic(int Low, int High)
		{
			return Stat.Rnd2.Next(Low, High + 1);
		}

		// Token: 0x06004085 RID: 16517 RVA: 0x0013EDC8 File Offset: 0x0013CFC8
		public static Random GetSeededRandomGenerator(string Seed)
		{
			if (XRLCore.Core.Game == null)
			{
				return new Random(Hash.String(Seed));
			}
			return new Random(Hash.String(XRLCore.Core.Game.GetWorldSeed(null).ToString() + Seed));
		}

		// Token: 0x06004086 RID: 16518 RVA: 0x0013EE15 File Offset: 0x0013D015
		public static Random GetStableRandomGenerator(string Seed)
		{
			return new Random((int)Seed.GetStableHashCode32());
		}

		// Token: 0x06004087 RID: 16519 RVA: 0x0013EE24 File Offset: 0x0013D024
		public static int SeededRandom(string Seed, int Low, int High)
		{
			if (Seed == null)
			{
				Seed = "none";
			}
			return new Random(Hash.String(XRLCore.Core.Game.GetWorldSeed(null).ToString() + Seed)).Next(Low, High + 1);
		}

		// Token: 0x06004088 RID: 16520 RVA: 0x0013EE6C File Offset: 0x0013D06C
		public static double GaussianRandom(float Mean, float StandardDeviation)
		{
			double num = Stat.Rnd.NextDouble();
			double num2 = Stat.Rnd.NextDouble();
			double num3 = Math.Sqrt(-2.0 * Math.Log(num)) * Math.Sin(6.283185307179586 * num2);
			return (double)Mean + (double)StandardDeviation * num3;
		}

		// Token: 0x06004089 RID: 16521 RVA: 0x0013EEBD File Offset: 0x0013D0BD
		public static int Random(int Low, int High)
		{
			return Stat.Rnd.Next(Low, High + 1);
		}

		// Token: 0x0600408A RID: 16522 RVA: 0x0013EECD File Offset: 0x0013D0CD
		public static int Random(long Low, long High)
		{
			return Stat.Rnd.Next((int)Low, (int)High + 1);
		}

		// Token: 0x0600408B RID: 16523 RVA: 0x0013EEDF File Offset: 0x0013D0DF
		public static int Random(int Low, long High)
		{
			return Stat.Rnd.Next(Low, (int)High + 1);
		}

		// Token: 0x0600408C RID: 16524 RVA: 0x0013EEF0 File Offset: 0x0013D0F0
		public static int Random(long Low, int High)
		{
			return Stat.Rnd.Next((int)Low, High + 1);
		}

		// Token: 0x0600408D RID: 16525 RVA: 0x0013EF01 File Offset: 0x0013D101
		public static float Random(float Low, float High)
		{
			return (float)Stat.Rnd.Next((int)(Low * 10f), (int)(High * 10f) + 1) / 10f;
		}

		// Token: 0x0600408E RID: 16526 RVA: 0x0013EF28 File Offset: 0x0013D128
		public unsafe static ulong NextULong(ulong Low, ulong High)
		{
			if (Low >= High)
			{
				return Low;
			}
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)8], 8);
			ulong num = High - Low;
			ulong num2 = ulong.MaxValue - (ulong.MaxValue % num + 1UL) % num;
			ulong num3;
			do
			{
				Stat.Rnd.NextBytes(span);
				num3 = BitConverter.ToUInt64(span);
			}
			while (num3 > num2);
			return num3 % num + Low;
		}

		// Token: 0x0600408F RID: 16527 RVA: 0x0013EF7C File Offset: 0x0013D17C
		public static void RollSave(out int NaturalRoll, out int Roll, out int Difficulty, ref bool IgnoreNatural1, ref bool IgnoreNatural20, GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool LogRoll = false, GameObject Source = null)
		{
			NaturalRoll = Stat.Random(1, 20);
			Roll = NaturalRoll;
			Difficulty = BaseDifficulty;
			Roll += Defender.StatMod(Stat, 0);
			if (Attacker != null)
			{
				Difficulty += Attacker.StatMod(AttackerStat ?? Stat, 0);
				ModifyAttackingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, true);
			}
			if (Source != null)
			{
				ModifyOriginatingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, true);
			}
			ModifyDefendingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, NaturalRoll, ref Roll, BaseDifficulty, ref Difficulty, ref IgnoreNatural1, ref IgnoreNatural20, true);
			if (Defender.IsPlayer())
			{
				if (Stat == "Intelligence" || Stat == "Ego" || Stat == "Willpower")
				{
					Defender.PlayWorldSound("sfx_ability_mutation_mental_generic_save", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				else
				{
					Defender.PlayWorldSound("sfx_ability_mutation_physical_generic_save", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			if (LogRoll && Options.DebugSavingThrows)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(Defender.IsPlayer() ? "Player" : Defender.Blueprint).Append(" rolled ").Append(NaturalRoll);
				if (Roll != NaturalRoll)
				{
					stringBuilder.Append(" modified to ").Append(Roll);
				}
				stringBuilder.Append(" on ").Append(Stat).Append(" save");
				if (Vs != null)
				{
					stringBuilder.Append(" vs. ").Append(Vs);
				}
				if (Attacker != null)
				{
					stringBuilder.Append(" from ").Append(Attacker.IsPlayer() ? "player" : Attacker.Blueprint);
					if (AttackerStat != null && AttackerStat != Stat)
					{
						stringBuilder.Append(" (using ").Append(AttackerStat).Append(')');
					}
				}
				stringBuilder.Append(" with difficulty ").Append(BaseDifficulty);
				if (Difficulty != BaseDifficulty)
				{
					stringBuilder.Append(" modified to ").Append(Difficulty);
				}
				MessageQueue.AddPlayerMessage(stringBuilder.ToString(), null, true);
			}
		}

		// Token: 0x06004090 RID: 16528 RVA: 0x0013F1CC File Offset: 0x0013D3CC
		public static void RollSave(out int NaturalRoll, out int Roll, out int Difficulty, GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool LogRoll = false)
		{
			bool flag = false;
			bool flag2 = false;
			Stat.RollSave(out NaturalRoll, out Roll, out Difficulty, ref flag, ref flag2, Defender, Stat, BaseDifficulty, Attacker, AttackerStat, Vs, LogRoll, null);
		}

		// Token: 0x06004091 RID: 16529 RVA: 0x0013F1F8 File Offset: 0x0013D3F8
		public static void RollSave(out int Roll, out int Difficulty, GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool LogRoll = false)
		{
			int num;
			Stat.RollSave(out num, out Roll, out Difficulty, Defender, Stat, BaseDifficulty, Attacker, AttackerStat, Vs, LogRoll);
		}

		// Token: 0x06004092 RID: 16530 RVA: 0x0013F21C File Offset: 0x0013D41C
		public static int RollSave(GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool LogRoll = false)
		{
			int num;
			int num2;
			int num3;
			Stat.RollSave(out num, out num2, out num3, Defender, Stat, BaseDifficulty, Attacker, AttackerStat, Vs, LogRoll);
			return num2;
		}

		// Token: 0x06004093 RID: 16531 RVA: 0x0013F240 File Offset: 0x0013D440
		public static bool MakeSave(out int SuccessMargin, out int FailureMargin, GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false, GameObject Source = null)
		{
			if (IgnoreNaturals)
			{
				IgnoreNatural1 = true;
				IgnoreNatural20 = true;
			}
			SuccessMargin = 0;
			FailureMargin = 0;
			int num;
			int num2;
			int num3;
			Stat.RollSave(out num, out num2, out num3, ref IgnoreNatural1, ref IgnoreNatural20, Defender, Stat, BaseDifficulty, Attacker, AttackerStat, Vs, false, Source);
			bool flag = (Defender.IsPlayer() && The.Core.IDKFA && !IgnoreGodmode) || (num == 20 && !IgnoreNatural20) || ((num != 1 || IgnoreNatural1) && num2 >= num3);
			if (flag)
			{
				if (num2 > num3)
				{
					SuccessMargin = num2 - num3;
				}
			}
			else if (num2 < num3)
			{
				FailureMargin = num3 - num2;
			}
			if (Options.DebugSavingThrows)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				stringBuilder.Append(Defender.DebugName).Append(flag ? " made " : " failed ").Append(Stat)
					.Append(" save");
				if (Vs != null)
				{
					stringBuilder.Append(" vs. ").Append(Vs);
				}
				if (Attacker != null)
				{
					stringBuilder.Append(" from ").Append(Attacker.DebugName);
					if (AttackerStat != null && AttackerStat != Stat)
					{
						stringBuilder.Append(" (using ").Append(AttackerStat).Append(')');
					}
				}
				if (Source != null)
				{
					stringBuilder.Append(" via ").Append(Source.DebugName);
				}
				stringBuilder.Append(" on ");
				if ((num == 1 && !IgnoreNatural1) || (num == 20 && !IgnoreNatural20))
				{
					stringBuilder.Append("natural ");
				}
				stringBuilder.Append(num);
				if (num2 != num)
				{
					stringBuilder.Append(" modified to ").Append(num2);
				}
				stringBuilder.Append(" with difficulty ").Append(BaseDifficulty);
				if (num3 != BaseDifficulty)
				{
					stringBuilder.Append(" modified to ").Append(num3);
				}
				if (Defender.IsPlayer() && The.Core.IDKFA && !IgnoreGodmode)
				{
					stringBuilder.Append(" (godmode)");
				}
				MessageQueue.AddPlayerMessage(stringBuilder.ToString(), null, true);
			}
			return flag;
		}

		// Token: 0x06004094 RID: 16532 RVA: 0x0013F438 File Offset: 0x0013D638
		public static bool MakeSave(GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false)
		{
			int num;
			int num2;
			return Stat.MakeSave(out num, out num2, Defender, Stat, BaseDifficulty, Attacker, AttackerStat, Vs, IgnoreNaturals, IgnoreNatural1, IgnoreNatural20, IgnoreGodmode, null);
		}

		// Token: 0x06004095 RID: 16533 RVA: 0x0013F460 File Offset: 0x0013D660
		public static int SaveChance(GameObject Defender, string Stat, int BaseDifficulty, GameObject Attacker = null, string AttackerStat = null, string Vs = null, bool IgnoreNaturals = false, bool IgnoreNatural1 = false, bool IgnoreNatural20 = false, bool IgnoreGodmode = false, GameObject Source = null)
		{
			if (IgnoreNaturals)
			{
				IgnoreNatural1 = true;
				IgnoreNatural20 = true;
			}
			int num = BaseDifficulty;
			int num2 = Defender.StatMod(Stat, 0);
			if (Attacker != null)
			{
				num += Attacker.StatMod(AttackerStat ?? Stat, 0);
				ModifyAttackingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, 0, ref num2, BaseDifficulty, ref num, ref IgnoreNatural1, ref IgnoreNatural20, false);
			}
			if (Source != null)
			{
				ModifyOriginatingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, 0, ref num2, BaseDifficulty, ref num, ref IgnoreNatural1, ref IgnoreNatural20, false);
			}
			ModifyDefendingSaveEvent.Process(Attacker, Defender, Source, Stat, AttackerStat, Vs, 0, ref num2, BaseDifficulty, ref num, ref IgnoreNatural1, ref IgnoreNatural20, false);
			if (Defender.IsPlayer() && The.Core.IDKFA && !IgnoreGodmode)
			{
				return 100;
			}
			int num3 = 21 - num + num2;
			int num4 = (IgnoreNatural1 ? 0 : 1);
			int num5 = (IgnoreNatural20 ? 20 : 19);
			if (num3 < num4)
			{
				num3 = num4;
			}
			else if (num3 > num5)
			{
				num3 = num5;
			}
			return num3 * 5;
		}

		// Token: 0x06004096 RID: 16534 RVA: 0x0013F532 File Offset: 0x0013D732
		public static char GetResultColorChar(int Result)
		{
			if (Result == -1)
			{
				return 'K';
			}
			if (Result == 0)
			{
				return 'r';
			}
			if (Result == 1)
			{
				return 'w';
			}
			if (Result == 2)
			{
				return 'W';
			}
			if (Result == 3)
			{
				return 'r';
			}
			if (Result == 4)
			{
				return 'R';
			}
			if (Result >= 5)
			{
				return 'M';
			}
			return 'y';
		}

		// Token: 0x06004097 RID: 16535 RVA: 0x0013F568 File Offset: 0x0013D768
		public static string GetResultColor(int Result)
		{
			if (Result == -1)
			{
				return "&K";
			}
			if (Result == 0)
			{
				return "&r";
			}
			if (Result == 1)
			{
				return "&w";
			}
			if (Result == 2)
			{
				return "&W";
			}
			if (Result == 3)
			{
				return "&r";
			}
			if (Result == 4)
			{
				return "&R";
			}
			if (Result >= 5)
			{
				return "&M";
			}
			return "&y";
		}

		// Token: 0x06004098 RID: 16536 RVA: 0x0013F5BF File Offset: 0x0013D7BF
		public static char GetChanceColorChar(int Chance)
		{
			if (Chance >= 100)
			{
				return 'M';
			}
			if (Chance >= 70)
			{
				return 'G';
			}
			if (Chance >= 40)
			{
				return 'W';
			}
			if (Chance >= 1)
			{
				return 'R';
			}
			return 'K';
		}

		// Token: 0x06004099 RID: 16537 RVA: 0x0013F5E2 File Offset: 0x0013D7E2
		public static string GetChanceColor(int Chance)
		{
			if (Chance >= 100)
			{
				return "&M";
			}
			if (Chance >= 70)
			{
				return "&G";
			}
			if (Chance >= 40)
			{
				return "&W";
			}
			if (Chance >= 1)
			{
				return "&R";
			}
			return "&K";
		}

		// Token: 0x0600409A RID: 16538 RVA: 0x0013F614 File Offset: 0x0013D814
		public static int RollResult(int Rating)
		{
			int num = Stat.Rnd.Next(1, 101);
			int num2 = Math.Min(Math.Max(Rating, -5), 35);
			if (num == 1)
			{
				return -1;
			}
			if ((double)num >= 100.0 - 0.26 * (double)num2)
			{
				return 10;
			}
			if ((double)num >= 99.0 - 0.27 * (double)num2)
			{
				return 9;
			}
			if (num >= 89 - num2)
			{
				return 8;
			}
			if ((double)num >= 86.0 - 1.31 * (double)num2)
			{
				return 7;
			}
			if ((double)num >= 78.0 - 1.74 * (double)num2)
			{
				return 6;
			}
			if ((double)num >= 74.0 - 1.92 * (double)num2)
			{
				return 5;
			}
			if ((double)num >= 67.0 - 3.1 * (double)num2)
			{
				return 4;
			}
			if ((double)num >= 55.0 - 1.5 * (double)num2)
			{
				return 3;
			}
			if ((double)num >= 44.0 - 1.4 * (double)num2)
			{
				return 2;
			}
			if ((double)num >= 15.0 - 0.5 * (double)num2)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x0600409B RID: 16539 RVA: 0x0013F74D File Offset: 0x0013D94D
		public static int MixRandom(int Low, int High)
		{
			if (High < Low)
			{
				return Stat.Rand.Next(High, Low + 1);
			}
			return Stat.Rand.Next(Low, High + 1);
		}

		// Token: 0x0600409C RID: 16540 RVA: 0x0013F770 File Offset: 0x0013D970
		public static int Roll(int Low, int High)
		{
			return Stat.Rand.Next(Low, High + 1);
		}

		// Token: 0x0600409D RID: 16541 RVA: 0x0013F780 File Offset: 0x0013D980
		public static int Roll(DieRoll Roll)
		{
			return Roll.Resolve();
		}

		// Token: 0x0600409E RID: 16542 RVA: 0x0013F788 File Offset: 0x0013D988
		public static bool Chance(string chance)
		{
			if (chance.Contains(":"))
			{
				string[] array = chance.Split(':', StringSplitOptions.None);
				return Stat.Random(1, Convert.ToInt32(array[1])) <= Convert.ToInt32(array[0]);
			}
			return Convert.ToInt32(chance).in100();
		}

		// Token: 0x0600409F RID: 16543 RVA: 0x0013F7D3 File Offset: 0x0013D9D3
		public static bool Chance(int chance)
		{
			return chance.in100();
		}

		/// <summary>
		///     Convert an attribute score to a roll modifier. 
		///     An attribute of 0 will have a -8 roll modifier, and will get +1 for every 2 points of attribute.
		///     Floor((Score - 16) * 0.5) 
		/// </summary>
		/// <param name="Score">The raw attribute score</param>
		/// <returns>A modifier to be used for a dice roll.</returns>
		// Token: 0x060040A0 RID: 16544 RVA: 0x0013F7DB File Offset: 0x0013D9DB
		public static int GetScoreModifier(int Score)
		{
			return (int)Math.Floor((double)(Score - 16) * 0.5);
		}

		// Token: 0x060040A1 RID: 16545 RVA: 0x0013F7F4 File Offset: 0x0013D9F4
		public static int RollLevelupChoice(string Dice)
		{
			int num = 1;
			if (Dice.Length == 0)
			{
				return 0;
			}
			if (Dice[0] == '-')
			{
				num = -1;
				Dice = Dice.Substring(1);
			}
			if (Dice.Contains("d"))
			{
				string[] array = Dice.Split('d', StringSplitOptions.None);
				int num2 = 0;
				int num3;
				if (array[1].Contains("+"))
				{
					string[] array2 = array[1].Split('+', StringSplitOptions.None);
					num3 = Convert.ToInt32(array2[0]);
					num2 = Convert.ToInt32(array2[1]);
				}
				else if (array[1].Contains("-"))
				{
					string[] array3 = array[1].Split('-', StringSplitOptions.None);
					num3 = Convert.ToInt32(array3[0]);
					num2 = -Convert.ToInt32(array3[1]);
				}
				else
				{
					num3 = Convert.ToInt32(array[1]);
				}
				int num4 = 0;
				int i = 0;
				int num5 = Convert.ToInt32(array[0]);
				while (i < num5)
				{
					num4 += Stat.RandomLevelUpChoice(1, num3);
					i++;
				}
				return num * (num4 + num2);
			}
			if (Dice.Contains("-"))
			{
				string[] array4 = Dice.Split('-', StringSplitOptions.None);
				return Stat.RandomLevelUpChoice(Convert.ToInt32(array4[0]), Convert.ToInt32(array4[1])) * num;
			}
			return Convert.ToInt32(Dice) * num;
		}

		// Token: 0x060040A2 RID: 16546 RVA: 0x0013F910 File Offset: 0x0013DB10
		public static int Roll(string Dice, string Channel = null)
		{
			if (string.IsNullOrEmpty(Dice))
			{
				return 0;
			}
			Stat.TryExtractChannel(ref Dice, ref Channel);
			if (Dice[0] == '-')
			{
				return -Stat.Roll(Dice.Substring(1), Channel);
			}
			if (Dice.Contains("|"))
			{
				string[] array = Dice.Split('|', StringSplitOptions.None);
				int num = Stat.GlobalChannelRandom(Channel, 0, array.Length - 1);
				return Stat.Roll(array[num], Channel);
			}
			if (Dice.Contains("+"))
			{
				string[] array2 = Dice.Split(Stat.splitterPlus, 2);
				return Stat.Roll(array2[0], Channel) + Stat.Roll(array2[1], Channel);
			}
			if (Dice.Contains("d") && Dice.Contains("-"))
			{
				string[] array3 = Dice.Split(Stat.splitterMinus, 2);
				return Stat.Roll(array3[0], Channel) - Stat.Roll(array3[1], Channel);
			}
			if (Dice.Contains("x"))
			{
				string[] array4 = Dice.Split(Stat.splitterX, 2);
				return Stat.Roll(array4[0], Channel) * Stat.Roll(array4[1], Channel);
			}
			if (Dice.Contains("/"))
			{
				string[] array5 = Dice.Split(Stat.splitterSlash, 2);
				return Stat.Roll(array5[0], Channel) / Stat.Roll(array5[1], Channel);
			}
			if (Dice.Contains("d"))
			{
				string[] array6 = Dice.Split(Stat.splitterD, 2);
				int num2 = Convert.ToInt32(array6[0]);
				int num3 = Convert.ToInt32(array6[1]);
				int num4 = 0;
				for (int i = 0; i < num2; i++)
				{
					num4 += Stat.GlobalChannelRandom(Channel, 1, num3);
				}
				return num4;
			}
			if (Dice.Contains("-"))
			{
				string[] array7 = Dice.Split(Stat.splitterMinus, 2);
				return Stat.GlobalChannelRandom(Channel, Stat.Roll(array7[0], Channel), Stat.Roll(array7[1], Channel));
			}
			return Convert.ToInt32(Dice);
		}

		// Token: 0x060040A3 RID: 16547 RVA: 0x0013FAD0 File Offset: 0x0013DCD0
		public static int Roll(this Random R, string Dice)
		{
			if (string.IsNullOrEmpty(Dice))
			{
				return 0;
			}
			if (Dice[0] == '-')
			{
				return -R.Roll(Dice.Substring(1));
			}
			if (Dice.Contains("|"))
			{
				string[] array = Dice.Split('|', StringSplitOptions.None);
				return R.Roll(array[R.Next(array.Length)]);
			}
			if (Dice.Contains("+"))
			{
				string[] array2 = Dice.Split(Stat.splitterPlus, 2);
				return R.Roll(array2[0]) + R.Roll(array2[1]);
			}
			if (Dice.Contains("d") && Dice.Contains("-"))
			{
				string[] array3 = Dice.Split(Stat.splitterMinus, 2);
				return R.Roll(array3[0]) - R.Roll(array3[1]);
			}
			if (Dice.Contains("x"))
			{
				string[] array4 = Dice.Split(Stat.splitterX, 2);
				return R.Roll(array4[0]) * R.Roll(array4[1]);
			}
			if (Dice.Contains("/"))
			{
				string[] array5 = Dice.Split(Stat.splitterSlash, 2);
				return R.Roll(array5[0]) / R.Roll(array5[1]);
			}
			if (Dice.Contains("d"))
			{
				string[] array6 = Dice.Split(Stat.splitterD, 2);
				int num = Convert.ToInt32(array6[0]);
				int num2 = Convert.ToInt32(array6[1]);
				int num3 = 0;
				for (int i = 0; i < num; i++)
				{
					num3 += R.Next(1, num2 + 1);
				}
				return num3;
			}
			if (Dice.Contains("-"))
			{
				string[] array7 = Dice.Split(Stat.splitterMinus, 2);
				return R.Next(R.Roll(array7[0]), R.Roll(array7[1]) + 1);
			}
			return Convert.ToInt32(Dice);
		}

		// Token: 0x060040A4 RID: 16548 RVA: 0x0013FC84 File Offset: 0x0013DE84
		public static int RollMax(string Dice)
		{
			if (string.IsNullOrEmpty(Dice))
			{
				return 0;
			}
			Stat.TryExtractChannel(ref Dice);
			if (Dice[0] == '-')
			{
				return -Stat.RollMin(Dice.Substring(1));
			}
			if (Dice.Contains("|"))
			{
				int num = int.MinValue;
				bool flag = false;
				string[] array = Dice.Split('|', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = Stat.RollMax(array[i]);
					if (num2 > num)
					{
						num = num2;
						flag = true;
					}
				}
				if (!flag)
				{
					return 0;
				}
				return num;
			}
			else
			{
				if (Dice.Contains("+"))
				{
					string[] array2 = Dice.Split(Stat.splitterPlus, 2);
					return Stat.RollMax(array2[0]) + Stat.RollMax(array2[1]);
				}
				if (Dice.Contains("d") && Dice.Contains("-"))
				{
					string[] array3 = Dice.Split(Stat.splitterMinus, 2);
					return Stat.RollMax(array3[0]) - Stat.RollMin(array3[1]);
				}
				if (Dice.Contains("x"))
				{
					string[] array4 = Dice.Split(Stat.splitterX, 2);
					return Stat.RollMax(array4[0]) * Stat.RollMax(array4[1]);
				}
				if (Dice.Contains("/"))
				{
					string[] array5 = Dice.Split(Stat.splitterSlash, 2);
					return Stat.RollMax(array5[0]) / Stat.RollMax(array5[1]);
				}
				if (Dice.Contains("d"))
				{
					string[] array6 = Dice.Split(Stat.splitterD, 2);
					int num3 = Convert.ToInt32(array6[0]);
					int num4 = Convert.ToInt32(array6[1]);
					return num3 * num4;
				}
				if (Dice.Contains("-"))
				{
					return Stat.RollMax(Dice.Split(Stat.splitterMinus, 2)[1]);
				}
				return Convert.ToInt32(Dice);
			}
		}

		// Token: 0x060040A5 RID: 16549 RVA: 0x0013FE25 File Offset: 0x0013E025
		public static int RollMax(DieRoll Roll)
		{
			return Roll.Max();
		}

		// Token: 0x060040A6 RID: 16550 RVA: 0x0013FE30 File Offset: 0x0013E030
		public static int RollMin(string Dice)
		{
			if (string.IsNullOrEmpty(Dice))
			{
				return 0;
			}
			Stat.TryExtractChannel(ref Dice);
			if (Dice[0] == '-')
			{
				return -Stat.RollMax(Dice.Substring(1));
			}
			if (Dice.Contains("|"))
			{
				int num = int.MaxValue;
				bool flag = false;
				string[] array = Dice.Split('|', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = Stat.RollMin(array[i]);
					if (num2 < num)
					{
						num = num2;
						flag = true;
					}
				}
				if (!flag)
				{
					return 0;
				}
				return num;
			}
			else
			{
				if (Dice.Contains("+"))
				{
					string[] array2 = Dice.Split(Stat.splitterPlus, 2);
					return Stat.RollMin(array2[0]) + Stat.RollMin(array2[1]);
				}
				if (Dice.Contains("d") && Dice.Contains("-"))
				{
					string[] array3 = Dice.Split(Stat.splitterMinus, 2);
					return Stat.RollMin(array3[0]) - Stat.RollMax(array3[1]);
				}
				if (Dice.Contains("x"))
				{
					string[] array4 = Dice.Split(Stat.splitterX, 2);
					return Stat.RollMin(array4[0]) * Stat.RollMin(array4[1]);
				}
				if (Dice.Contains("/"))
				{
					string[] array5 = Dice.Split(Stat.splitterSlash, 2);
					return Stat.RollMin(array5[0]) / Stat.RollMin(array5[1]);
				}
				if (Dice.Contains("d"))
				{
					return Convert.ToInt32(Dice.Split(Stat.splitterD, 2)[0]);
				}
				if (Dice.Contains("-"))
				{
					return Stat.RollMin(Dice.Split(Stat.splitterMinus, 2)[0]);
				}
				return Convert.ToInt32(Dice);
			}
		}

		// Token: 0x060040A7 RID: 16551 RVA: 0x0013FFC0 File Offset: 0x0013E1C0
		public static int RollMin(DieRoll Roll)
		{
			return Roll.Min();
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x0013FFC8 File Offset: 0x0013E1C8
		public static DieRoll GetCachedDieRoll(string Dice)
		{
			DieRoll dieRoll;
			if (Stat.DieRolls == null)
			{
				Stat.DieRolls = new Dictionary<string, DieRoll>(128);
				dieRoll = new DieRoll(Dice);
				Stat.DieRolls.Add(Dice, dieRoll);
			}
			else if (!Stat.DieRolls.TryGetValue(Dice, out dieRoll))
			{
				dieRoll = new DieRoll(Dice);
				Stat.DieRolls.Add(Dice, dieRoll);
			}
			return dieRoll;
		}

		// Token: 0x060040A9 RID: 16553 RVA: 0x00140023 File Offset: 0x0013E223
		public static int RollCached(string Dice)
		{
			return Stat.GetCachedDieRoll(Dice).Resolve();
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x00140030 File Offset: 0x0013E230
		public static int RollMinCached(string Dice)
		{
			return Stat.GetCachedDieRoll(Dice).Min();
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x0014003D File Offset: 0x0013E23D
		public static int RollMaxCached(string Dice)
		{
			return Stat.GetCachedDieRoll(Dice).Max();
		}

		// Token: 0x060040AC RID: 16556 RVA: 0x0014004A File Offset: 0x0013E24A
		public static double RollAverageCached(string Dice)
		{
			return Stat.GetCachedDieRoll(Dice).Average();
		}

		// Token: 0x060040AD RID: 16557 RVA: 0x00140058 File Offset: 0x0013E258
		public static int rollMentalAttackPenetrations(GameObject attacker, GameObject defender)
		{
			string text = "1d8";
			int num = attacker.StatMod("Ego", 0);
			if (num >= 0)
			{
				text = text + "+" + num.ToString();
			}
			else if (num < 0)
			{
				text += num.ToString();
			}
			int combatMA = Stats.GetCombatMA(defender);
			return Stat.RollPenetratingSuccesses(text, combatMA, 0, 1, null);
		}

		// Token: 0x060040AE RID: 16558 RVA: 0x001400B4 File Offset: 0x0013E2B4
		public static void TryExtractChannel(ref string Dice, ref string Channel)
		{
			Match match = Stat.channelPattern.Match(Dice);
			if (match.Success)
			{
				Channel = match.Groups[1].Value;
				Dice = Stat.channelPattern.Replace(Dice, "");
			}
		}

		// Token: 0x060040AF RID: 16559 RVA: 0x001400FC File Offset: 0x0013E2FC
		public static void TryExtractChannel(ref string Dice)
		{
			string text = null;
			Stat.TryExtractChannel(ref Dice, ref text);
		}

		// Token: 0x060040B0 RID: 16560 RVA: 0x00140114 File Offset: 0x0013E314
		public static int RollPenetratingSuccesses(string Dice, int TargetInclusive, int Bonus = 0, int Multiplier = 1, string Channel = null)
		{
			if (string.IsNullOrEmpty(Dice))
			{
				return 0;
			}
			Stat.TryExtractChannel(ref Dice, ref Channel);
			if (Dice.Contains("|"))
			{
				string[] array = Dice.Split('|', StringSplitOptions.None);
				int num = Stat.GlobalChannelRandom(Channel, 0, array.Length - 1);
				return Stat.RollPenetratingSuccesses(array[num], TargetInclusive, Bonus, Multiplier, Channel);
			}
			if (Dice[0] == '-')
			{
				Multiplier *= -1;
				Dice = Dice.Substring(1);
			}
			if (Dice.Contains("+"))
			{
				string[] array2 = Dice.Split(Stat.splitterPlus, 2);
				return Stat.RollPenetratingSuccesses(array2[0], TargetInclusive, Bonus + Stat.Roll(array2[1], Channel), Multiplier, Channel);
			}
			if (Dice.Contains("d") && Dice.Contains("-"))
			{
				string[] array3 = Dice.Split(Stat.splitterMinus, 2);
				return Stat.RollPenetratingSuccesses(array3[0], TargetInclusive, Bonus - Stat.Roll(array3[1], Channel), Multiplier, Channel);
			}
			if (Dice.Contains("x"))
			{
				string[] array4 = Dice.Split(Stat.splitterX, 2);
				return Stat.RollPenetratingSuccesses(array4[0], TargetInclusive, Bonus, Multiplier * Stat.Roll(array4[1], Channel), Channel);
			}
			if (Dice.Contains("/"))
			{
				string[] array5 = Dice.Split(Stat.splitterSlash, 2);
				return Stat.RollPenetratingSuccesses(array5[0], TargetInclusive, Bonus, Multiplier / Stat.Roll(array5[1], Channel), Channel);
			}
			if (Dice.Contains("d"))
			{
				string[] array6 = Dice.Split(Stat.splitterD, 2);
				int num2 = Convert.ToInt32(array6[0]);
				int num3 = Convert.ToInt32(array6[1]);
				int num4 = 0;
				int i = 0;
				while (i < num2)
				{
					int num5 = 0;
					bool flag = true;
					int num6;
					do
					{
						num6 = Stat.GlobalChannelRandom(Channel, 1, num3);
						num5 += num6;
						if (flag)
						{
							flag = false;
						}
						else
						{
							num5--;
						}
						if (num5 * Multiplier + Bonus >= TargetInclusive)
						{
							goto Block_11;
						}
					}
					while (num6 == num3 && num6 != 1);
					IL_01B9:
					i++;
					continue;
					Block_11:
					num4++;
					goto IL_01B9;
				}
				return num4;
			}
			if (Dice.Contains("-"))
			{
				string[] array7 = Dice.Split(Stat.splitterMinus, 2);
				if (Stat.GlobalChannelRandom(Channel, Stat.Roll(array7[0], Channel), Stat.Roll(array7[1], Channel)) * Multiplier + Bonus >= TargetInclusive)
				{
					return 1;
				}
			}
			else if (Stat.Roll(Dice, Channel) * Multiplier + Bonus >= TargetInclusive)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x060040B1 RID: 16561 RVA: 0x0014033C File Offset: 0x0013E53C
		public static int GlobalChannelRandom(string Channel, int Low, int High)
		{
			if (Channel == null)
			{
				return Stat.Random(Low, High);
			}
			string text = "RandomSeed:" + Channel;
			int num;
			if (XRLCore.Core.Game.HasIntGameState(text))
			{
				num = XRLCore.Core.Game.GetIntGameState(text, 0);
			}
			else
			{
				num = Hash.String(XRLCore.Core.Game.GetWorldSeed(null).ToString() + text);
			}
			Random random = new Random(num);
			int num2 = random.Next(Low, High + 1);
			XRLCore.Core.Game.SetIntGameState(text, random.Next());
			return num2;
		}

		// Token: 0x04001FAB RID: 8107
		public static string rndAccessLog = null;

		// Token: 0x04001FAC RID: 8108
		public static int rndAccessIndex = 0;

		// Token: 0x04001FAD RID: 8109
		public static int rndAccessCount = 0;

		// Token: 0x04001FAE RID: 8110
		public static bool _logRndAccess = false;

		// Token: 0x04001FAF RID: 8111
		public static List<string> rndLogLines = new List<string>();

		// Token: 0x04001FB0 RID: 8112
		public static Random Rand = new Random();

		// Token: 0x04001FB1 RID: 8113
		public static Random _Rnd = new Random();

		// Token: 0x04001FB2 RID: 8114
		public static Random Rnd2 = new Random();

		// Token: 0x04001FB3 RID: 8115
		public static Random Rnd4 = new Random();

		// Token: 0x04001FB4 RID: 8116
		public static Random Rnd5 = new Random();

		// Token: 0x04001FB5 RID: 8117
		public static Random NamingRnd = new Random();

		// Token: 0x04001FB6 RID: 8118
		private static Stack<ValueTuple<Random[], int>> State = new Stack<ValueTuple<Random[], int>>();

		// Token: 0x04001FB7 RID: 8119
		[NonSerialized]
		public static char[] splitterD = new char[] { 'd' };

		// Token: 0x04001FB8 RID: 8120
		[NonSerialized]
		public static char[] splitterX = new char[] { 'x' };

		// Token: 0x04001FB9 RID: 8121
		[NonSerialized]
		public static char[] splitterSlash = new char[] { '/' };

		// Token: 0x04001FBA RID: 8122
		[NonSerialized]
		public static char[] splitterMinus = new char[] { '-' };

		// Token: 0x04001FBB RID: 8123
		[NonSerialized]
		public static char[] splitterPlus = new char[] { '+' };

		// Token: 0x04001FBC RID: 8124
		[NonSerialized]
		public static Dictionary<string, DieRoll> DieRolls = null;

		// Token: 0x04001FBD RID: 8125
		private static Regex channelPattern = new Regex("\\[(\\w+)\\]");
	}
}
