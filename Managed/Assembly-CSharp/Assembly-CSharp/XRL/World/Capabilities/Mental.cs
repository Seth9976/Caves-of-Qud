using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Capabilities
{
	// Token: 0x020009EF RID: 2543
	public static class Mental
	{
		// Token: 0x06006C82 RID: 27778 RVA: 0x002339A0 File Offset: 0x00231BA0
		public static bool PerformAttack(Mental.Attack Handler, GameObject Attacker, GameObject Defender, GameObject Source = null, string Command = null, string Dice = null, int Type = 0, int Magnitude = -2147483648, int Penetrations = -2147483648, int AttackModifier = 0, int DefenseModifier = 0)
		{
			if (Source == null)
			{
				Source = Attacker;
			}
			if (!Defender.HasStat("MA"))
			{
				return false;
			}
			MentalAttackEvent mentalAttackEvent = MentalAttackEvent.FromPool(Attacker, Defender, Source, Command, Dice, Type, Magnitude);
			mentalAttackEvent.BaseDifficulty = Stats.GetCombatMA(Defender);
			mentalAttackEvent.Difficulty = mentalAttackEvent.BaseDifficulty + DefenseModifier;
			mentalAttackEvent.Modifier = AttackModifier;
			if (!BeginMentalAttackEvent.Check(mentalAttackEvent))
			{
				return false;
			}
			if (!BeginMentalDefendEvent.Check(mentalAttackEvent))
			{
				return false;
			}
			if (Penetrations >= 0)
			{
				mentalAttackEvent.Penetrations = Penetrations;
			}
			else if (mentalAttackEvent.Dice != null)
			{
				mentalAttackEvent.Penetrations = Stat.RollPenetratingSuccesses(mentalAttackEvent.Dice, mentalAttackEvent.Difficulty, mentalAttackEvent.Modifier, 1, null);
			}
			else
			{
				mentalAttackEvent.Penetrations = Stat.RollDamagePenetrations(mentalAttackEvent.Difficulty, mentalAttackEvent.Modifier, mentalAttackEvent.Modifier);
			}
			if (!BeforeMentalAttackEvent.Check(mentalAttackEvent))
			{
				mentalAttackEvent.Penetrations = 0;
			}
			if (!BeforeMentalDefendEvent.Check(mentalAttackEvent))
			{
				mentalAttackEvent.Penetrations = 0;
			}
			if (!Handler(mentalAttackEvent))
			{
				mentalAttackEvent.Penetrations = 0;
			}
			AfterMentalAttackEvent.Send(mentalAttackEvent);
			AfterMentalDefendEvent.Send(mentalAttackEvent);
			return mentalAttackEvent.Penetrations > 0;
		}

		// Token: 0x06006C83 RID: 27779 RVA: 0x00233AA4 File Offset: 0x00231CA4
		public static int TypeExpansion(string Types)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(Types))
			{
				foreach (string text in Types.ToUpper().CachedCommaExpansion())
				{
					int num2;
					if (Mental.StringToType.TryGetValue(text, out num2))
					{
						num |= num2;
					}
				}
			}
			return num;
		}

		// Token: 0x040031BB RID: 12731
		public const int TYPE_PSIONIC = 1;

		// Token: 0x040031BC RID: 12732
		public const int TYPE_SONIC = 2;

		// Token: 0x040031BD RID: 12733
		public const int TYPE_OPTIC = 4;

		// Token: 0x040031BE RID: 12734
		public const int TYPE_METABOLIC = 8;

		// Token: 0x040031BF RID: 12735
		public const int TYPE_REFLECTABLE = 8388608;

		// Token: 0x040031C0 RID: 12736
		public const int TYPE_REFLECTED = 16777216;

		// Token: 0x040031C1 RID: 12737
		public static readonly Dictionary<string, int> StringToType = new Dictionary<string, int>
		{
			{ "PSIONIC", 1 },
			{ "SONIC", 2 },
			{ "OPTIC", 4 },
			{ "METABOLIC", 8 },
			{ "REFLECTABLE", 8388608 },
			{ "REFLECTED", 16777216 }
		};

		// Token: 0x02001AD3 RID: 6867
		// (Invoke) Token: 0x0600E940 RID: 59712
		public delegate bool Attack(MentalAttackEvent E);
	}
}
