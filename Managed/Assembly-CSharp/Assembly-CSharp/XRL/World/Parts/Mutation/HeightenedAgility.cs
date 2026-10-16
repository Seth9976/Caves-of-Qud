using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001444 RID: 5188
	[Serializable]
	public class HeightenedAgility : BaseMutation
	{
		// Token: 0x0600CA8E RID: 51854 RVA: 0x0049AF7D File Offset: 0x0049917D
		public int GetCooldownCancelChance(int Level)
		{
			return 7 + Level * 3;
		}

		// Token: 0x0600CA8F RID: 51855 RVA: 0x0049AF84 File Offset: 0x00499184
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeCooldownActivatedAbility");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA90 RID: 51856 RVA: 0x0049AF9C File Offset: 0x0049919C
		public override bool FireEvent(Event E)
		{
			if (!(E.ID == "BeforeCooldownActivatedAbility"))
			{
				return base.FireEvent(E);
			}
			if (E.GetStringParameter("Tags", null) == "Agility" && Stat.Random(1, 100) <= this.GetCooldownCancelChance(base.Level))
			{
				E.SetParameter("Turns", 0);
				if (this.ParentObject.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("You do that with ease.", null, true);
					this.ParentObject.ParticleText("No cooldown!", 'G', false, 1.5f, -8f);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600CA91 RID: 51857 RVA: 0x0049B037 File Offset: 0x00499237
		public override string GetDescription()
		{
			return "Your joints stretch much further than usual.";
		}

		// Token: 0x0600CA92 RID: 51858 RVA: 0x0049B040 File Offset: 0x00499240
		public override string GetLevelText(int Level)
		{
			return "+{{rules|" + (2 + (Level - 1) / 2).ToString() + "}} Agility\n" + "{{rules|" + this.GetCooldownCancelChance(Level).ToString() + "%}} chance that Sprint and skills with Agility prerequisites don't go on cooldown after use";
		}

		// Token: 0x0600CA93 RID: 51859 RVA: 0x0049B089 File Offset: 0x00499289
		public int GetAgilityBonus(int Level)
		{
			return 2 + (Level - 1) / 2;
		}

		// Token: 0x0600CA94 RID: 51860 RVA: 0x0049B092 File Offset: 0x00499292
		public int GetAgilityBonusAttributableToTemporaryLevels()
		{
			return this.GetAgilityBonus(base.Level) - this.GetAgilityBonus(base.Level - base.GetTemporaryLevels());
		}

		// Token: 0x0600CA95 RID: 51861 RVA: 0x0049B0B4 File Offset: 0x004992B4
		public override bool ChangeLevel(int NewLevel)
		{
			int agilityBonusAttributableToTemporaryLevels = this.GetAgilityBonusAttributableToTemporaryLevels();
			int num = this.GetAgilityBonus(base.Level) - agilityBonusAttributableToTemporaryLevels;
			base.StatShifter.SetStatShift(this.ParentObject, "Agility", num, true);
			base.StatShifter.SetStatShift(this.ParentObject, "Agility", agilityBonusAttributableToTemporaryLevels, false);
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA96 RID: 51862 RVA: 0x0049B110 File Offset: 0x00499310
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA97 RID: 51863 RVA: 0x0049B11A File Offset: 0x0049931A
		public override bool Unmutate(GameObject GO)
		{
			base.StatShifter.RemoveStatShifts(GO);
			return base.Unmutate(GO);
		}
	}
}
