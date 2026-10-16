using System;
using Qud.API;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C59 RID: 3161
	[Serializable]
	public class CookingDomainAttributes_UnitPermanentAllStats_25Percent : ProceduralCookingEffectUnit
	{
		// Token: 0x060080BE RID: 32958 RVA: 0x002F5064 File Offset: 0x002F3264
		public override string GetDescription()
		{
			if (!this.Succeed)
			{
				return "Nothing happened.";
			}
			return this.Bonus.Signed(false) + " to all six attributes permanently";
		}

		// Token: 0x060080BF RID: 32959 RVA: 0x002F508A File Offset: 0x002F328A
		public override string GetTemplatedDescription()
		{
			return "25% chance to gain +1 Strength, Agility, Toughness, Intelligence, Willpower, and Ego permanently.";
		}

		// Token: 0x060080C0 RID: 32960 RVA: 0x002F5091 File Offset: 0x002F3291
		public override void Init(GameObject target)
		{
			target.WithSeededRandom(delegate(Random rng)
			{
				this.Succeed = 25.in100(rng);
			}, "CookingDomainAttributes_UnitPermanentAllStats_25Percent");
		}

		// Token: 0x060080C1 RID: 32961 RVA: 0x002F50AC File Offset: 0x002F32AC
		public override void Apply(GameObject Object, Effect parent)
		{
			Object.PermuteRandomMutationBuys();
			if (this.Succeed)
			{
				Object.GetStat("Strength").BaseValue += this.Bonus;
				Object.GetStat("Agility").BaseValue += this.Bonus;
				Object.GetStat("Toughness").BaseValue += this.Bonus;
				Object.GetStat("Intelligence").BaseValue += this.Bonus;
				Object.GetStat("Willpower").BaseValue += this.Bonus;
				Object.GetStat("Ego").BaseValue += this.Bonus;
				if (Object.IsPlayer())
				{
					JournalAPI.AddAccomplishment("You rejoiced in a drop of nectar.", "=name= the Eater supped on royal nectar and metamorphosed into Godhead.", string.Concat(new string[]
					{
						"While traveling through ",
						The.Player.CurrentZone.GetTerrainDisplayName(true),
						", =name= stopped at a tavern near ",
						JournalAPI.GetLandmarkNearestPlayer().Text,
						". There ",
						The.Player.GetPronounProvider(false).Subjective,
						" wisely declined to play a game of dice and instead supped on a nourishing serving of Eaters' nectar."
					}), null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Low, null, -1L, true);
				}
			}
			if (Object.IsPlayer())
			{
				Object.GetPart<Leveler>().SifrahInsights();
			}
		}

		// Token: 0x060080C2 RID: 32962 RVA: 0x002F520D File Offset: 0x002F340D
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x04003904 RID: 14596
		public const string RANDOM_SEED = "CookingDomainAttributes_UnitPermanentAllStats_25Percent";

		// Token: 0x04003905 RID: 14597
		public int Bonus = 1;

		// Token: 0x04003906 RID: 14598
		public bool Succeed;
	}
}
