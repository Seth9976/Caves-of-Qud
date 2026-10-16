using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F1 RID: 2545
	public static class Phase
	{
		// Token: 0x06006C8D RID: 27789 RVA: 0x00234F88 File Offset: 0x00233188
		public static string getName(int phase)
		{
			switch (phase)
			{
			case 0:
				return "unknown";
			case 1:
				return "in-phase";
			case 2:
				return "out-of-phase";
			case 3:
				return "omniphase";
			case 4:
				return "nullphase";
			case 5:
				return "phase insensitive";
			default:
				return null;
			}
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x00234FDC File Offset: 0x002331DC
		public static int getPhase(GameObject obj)
		{
			if (obj == null)
			{
				return 0;
			}
			if (obj.HasTag("Nullphase") || obj.GetIntProperty("Nullphase", 0) >= 1)
			{
				return 4;
			}
			if (obj.HasEffect(typeof(Omniphase)))
			{
				return 3;
			}
			if (obj.HasEffect(typeof(Phased)))
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x00235035 File Offset: 0x00233235
		public static bool phaseMatches(int phase1, int phase2)
		{
			if (phase1 == 5 || phase2 == 5)
			{
				return true;
			}
			if (phase1 == 4 || phase2 == 4)
			{
				return false;
			}
			if (phase1 == 3 || phase2 == 3)
			{
				return true;
			}
			if (phase1 == 0)
			{
				phase1 = 1;
			}
			if (phase2 == 0)
			{
				phase2 = 1;
			}
			return phase1 == phase2;
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x00235068 File Offset: 0x00233268
		public static bool phaseMatches(GameObject obj, int phase2)
		{
			if (phase2 == 5)
			{
				return true;
			}
			if (phase2 == 4)
			{
				return false;
			}
			if (obj == null)
			{
				return false;
			}
			int num = Phase.getPhase(obj);
			if (num == 4)
			{
				return false;
			}
			if (num == 3 || phase2 == 3)
			{
				return true;
			}
			if (num == 0)
			{
				num = 1;
			}
			if (phase2 == 0)
			{
				phase2 = 1;
			}
			return num == phase2;
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x002350AC File Offset: 0x002332AC
		public static bool phaseMatches(int phase1, GameObject obj)
		{
			return Phase.phaseMatches(obj, phase1);
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x002350B5 File Offset: 0x002332B5
		public static bool phaseMatches(GameObject obj1, GameObject obj2)
		{
			return obj2 != null && Phase.phaseMatches(obj1, Phase.getPhase(obj2));
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x002350C8 File Offset: 0x002332C8
		public static bool IsTemporarilyPhasedOut(GameObject Object)
		{
			if (Object.GetPhase() != 2)
			{
				return false;
			}
			Phased effect = Object.GetEffect<Phased>();
			return effect != null && effect.Duration != 9999;
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x002350FC File Offset: 0x002332FC
		public static int getWeaponPhase(int actorPhase, int weaponPhase)
		{
			if (actorPhase == 3 || weaponPhase == 3)
			{
				return 3;
			}
			return actorPhase;
		}

		// Token: 0x06006C95 RID: 27797 RVA: 0x00235109 File Offset: 0x00233309
		public static int getWeaponPhase(GameObject actor, GameObject weapon)
		{
			return Phase.getWeaponPhase(actor.GetPhase(), weapon.GetPhase());
		}

		// Token: 0x06006C96 RID: 27798 RVA: 0x0023511C File Offset: 0x0023331C
		public static int getWeaponPhase(int actorPhase, GameObject weapon)
		{
			return Phase.getWeaponPhase(actorPhase, weapon.GetPhase());
		}

		// Token: 0x06006C97 RID: 27799 RVA: 0x0023512A File Offset: 0x0023332A
		public static int getWeaponPhase(GameObject actor, int weaponPhase)
		{
			return Phase.getWeaponPhase(actor.GetPhase(), weaponPhase);
		}

		// Token: 0x06006C98 RID: 27800 RVA: 0x00235138 File Offset: 0x00233338
		public static void carryOver(int Phase, GameObject Object, int Duration = 9999)
		{
			if (Object == null)
			{
				return;
			}
			if (Phase == 2)
			{
				Object.ForceApplyEffect(new Phased(Duration), null);
				return;
			}
			if (Phase == 3)
			{
				Object.ForceApplyEffect(new Omniphase(Duration), null);
			}
		}

		// Token: 0x06006C99 RID: 27801 RVA: 0x00235164 File Offset: 0x00233364
		public static void carryOver(GameObject src, GameObject dest)
		{
			if (src == null || dest == null)
			{
				return;
			}
			Phased phased;
			if (src.TryGetEffect<Phased>(out phased))
			{
				dest.ForceApplyEffect(new Phased(phased.Duration), null);
			}
			Omniphase omniphase;
			if (src.TryGetEffect<Omniphase>(out omniphase))
			{
				dest.ForceApplyEffect(new Omniphase(omniphase.Duration), null);
			}
		}

		// Token: 0x06006C9A RID: 27802 RVA: 0x002351B4 File Offset: 0x002333B4
		public static void carryOver(GameObject src, GameObject dest, int maxDuration)
		{
			Phased phased;
			if (src.TryGetEffect<Phased>(out phased))
			{
				dest.ForceApplyEffect(new Phased(Math.Min(phased.Duration, maxDuration)), null);
			}
			Omniphase omniphase;
			if (src.TryGetEffect<Omniphase>(out omniphase))
			{
				dest.ForceApplyEffect(new Omniphase(Math.Min(omniphase.Duration, maxDuration)), null);
			}
		}

		// Token: 0x06006C9B RID: 27803 RVA: 0x00235207 File Offset: 0x00233407
		public static void carryOverPrep(GameObject src, out Phased FX1, out Omniphase FX2)
		{
			FX1 = src.GetEffect<Phased>();
			FX2 = src.GetEffect<Omniphase>();
		}

		// Token: 0x06006C9C RID: 27804 RVA: 0x00235219 File Offset: 0x00233419
		public static void carryOver(GameObject src, GameObject dest, Phased FX1, Omniphase FX2)
		{
			if (FX1 != null)
			{
				dest.ForceApplyEffect(new Phased(FX1.Duration), null);
			}
			if (FX2 != null)
			{
				dest.ForceApplyEffect(new Omniphase(FX2.Duration), null);
			}
		}

		// Token: 0x06006C9D RID: 27805 RVA: 0x00235247 File Offset: 0x00233447
		public static void syncPrep(GameObject src, out Phased FX1, out Omniphase FX2)
		{
			FX1 = src.GetEffect<Phased>();
			FX2 = src.GetEffect<Omniphase>();
		}

		// Token: 0x06006C9E RID: 27806 RVA: 0x0023525C File Offset: 0x0023345C
		public static bool sync(GameObject src, GameObject dest, Phased FX1, Omniphase FX2)
		{
			bool flag = false;
			Phased effect = dest.GetEffect<Phased>();
			if (FX1 != null && effect != null)
			{
				if (effect.Duration != FX1.Duration)
				{
					effect.Duration = FX1.Duration;
					flag = true;
				}
			}
			else if (FX1 != null)
			{
				dest.ForceApplyEffect(new Phased(FX1.Duration), null);
				flag = true;
			}
			else if (effect != null)
			{
				dest.RemoveEffect(effect, true);
				flag = true;
			}
			Omniphase effect2 = dest.GetEffect<Omniphase>();
			if (FX2 != null && effect2 != null)
			{
				if (effect2.Duration != FX2.Duration)
				{
					effect2.Duration = FX2.Duration;
					flag = true;
				}
			}
			else if (FX2 != null)
			{
				dest.ForceApplyEffect(new Omniphase(FX2.Duration), null);
				flag = true;
			}
			else if (effect2 != null)
			{
				dest.RemoveEffect(effect2, true);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06006C9F RID: 27807 RVA: 0x00235312 File Offset: 0x00233512
		public static bool sync(GameObject src, GameObject dest)
		{
			return Phase.sync(src, dest, src.GetEffect<Phased>(), src.GetEffect<Omniphase>());
		}

		// Token: 0x06006CA0 RID: 27808 RVA: 0x00235327 File Offset: 0x00233527
		public static char getColor(int phase)
		{
			switch (phase)
			{
			case 1:
				return 'Y';
			case 2:
				return 'b';
			case 3:
				return 'M';
			case 4:
				return 'K';
			default:
				return 'y';
			}
		}

		// Token: 0x06006CA1 RID: 27809 RVA: 0x00235354 File Offset: 0x00233554
		public static char getRandomExplosionColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'Y';
				}
				if (num != 2)
				{
					return 'W';
				}
				return 'R';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'b';
			case 3:
				if (num == 1)
				{
					return 'M';
				}
				if (num != 2)
				{
					return 'm';
				}
				return 'G';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'k';
				}
				return 'y';
			default:
				return 'y';
			}
		}

		// Token: 0x06006CA2 RID: 27810 RVA: 0x002353CC File Offset: 0x002335CC
		public static char getRandomElectricArcColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'Y';
				}
				if (num != 2)
				{
					return 'W';
				}
				return 'W';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'b';
			case 3:
				if (num == 1)
				{
					return 'M';
				}
				if (num != 2)
				{
					return 'm';
				}
				return 'G';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'y';
			default:
				return 'y';
			}
		}

		// Token: 0x06006CA3 RID: 27811 RVA: 0x00235444 File Offset: 0x00233644
		public static char getRandomHeatColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'Y';
				}
				if (num != 2)
				{
					return 'R';
				}
				return 'W';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'y';
				}
				return 'B';
			case 3:
				if (num == 1)
				{
					return 'M';
				}
				if (num != 2)
				{
					return 'W';
				}
				return 'G';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'y';
			default:
				return 'R';
			}
		}

		// Token: 0x06006CA4 RID: 27812 RVA: 0x002354BC File Offset: 0x002336BC
		public static char getRandomColdColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'Y';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'C';
			case 2:
				if (num == 1)
				{
					return 'y';
				}
				if (num != 2)
				{
					return 'b';
				}
				return 'B';
			case 3:
				if (num == 1)
				{
					return 'M';
				}
				if (num != 2)
				{
					return 'B';
				}
				return 'G';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'y';
			default:
				return 'C';
			}
		}

		// Token: 0x06006CA5 RID: 27813 RVA: 0x00235534 File Offset: 0x00233734
		public static char getRandomElectromagneticPulseColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'W';
				}
				if (num != 2)
				{
					return 'C';
				}
				return 'Y';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'b';
			case 3:
				if (num == 1)
				{
					return 'm';
				}
				if (num != 2)
				{
					return 'C';
				}
				return 'c';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'y';
			default:
				return 'W';
			}
		}

		// Token: 0x06006CA6 RID: 27814 RVA: 0x002355AC File Offset: 0x002337AC
		public static char getRandomFlashColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'B';
				}
				if (num != 2)
				{
					return 'B';
				}
				return 'Y';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'B';
			case 3:
				if (num == 1)
				{
					return 'm';
				}
				if (num != 2)
				{
					return 'B';
				}
				return 'c';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'B';
			default:
				return 'Y';
			}
		}

		// Token: 0x06006CA7 RID: 27815 RVA: 0x00235624 File Offset: 0x00233824
		public static char getRandomDisintegrationColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 3);
			switch (phase)
			{
			case 1:
				if (num == 1)
				{
					return 'r';
				}
				if (num != 2)
				{
					return 'w';
				}
				return 'R';
			case 2:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'c';
				}
				return 'b';
			case 3:
				if (num == 1)
				{
					return 'M';
				}
				if (num != 2)
				{
					return 'm';
				}
				return 'G';
			case 4:
				if (num == 1)
				{
					return 'K';
				}
				if (num != 2)
				{
					return 'Y';
				}
				return 'y';
			default:
				return 'K';
			}
		}

		// Token: 0x06006CA8 RID: 27816 RVA: 0x0023569C File Offset: 0x0023389C
		public static char getRandomStunningForceColor(int phase)
		{
			int num = Stat.RandomCosmetic(1, 2);
			switch (phase)
			{
			case 1:
				if (num != 1)
				{
					return 'y';
				}
				return 'Y';
			case 2:
				if (num != 1)
				{
					return 'b';
				}
				return 'K';
			case 3:
				if (num != 1)
				{
					return 'G';
				}
				return 'M';
			case 4:
				if (num != 1)
				{
					return 'y';
				}
				return 'K';
			default:
				return 'y';
			}
		}

		// Token: 0x040031C2 RID: 12738
		public const int UNKNOWN = 0;

		// Token: 0x040031C3 RID: 12739
		public const int IN_PHASE = 1;

		// Token: 0x040031C4 RID: 12740
		public const int OUT_OF_PHASE = 2;

		// Token: 0x040031C5 RID: 12741
		public const int OMNIPHASE = 3;

		// Token: 0x040031C6 RID: 12742
		public const int NULLPHASE = 4;

		// Token: 0x040031C7 RID: 12743
		public const int PHASE_INSENSITIVE = 5;
	}
}
