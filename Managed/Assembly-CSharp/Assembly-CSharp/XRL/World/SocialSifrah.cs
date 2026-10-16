using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008E0 RID: 2272
	public abstract class SocialSifrah : SifrahGame
	{
		// Token: 0x060057CE RID: 22478 RVA: 0x001AA02E File Offset: 0x001A822E
		public SocialSifrah()
		{
			this.CorrectTokenSound = "Sounds/Interact/sfx_artifact_examination_success_total";
			this.CorrectTokenSoundDelay = 300;
			this.IncorrectTokenSound = "Sounds/Creatures/VO/sfx_creature_animal_grazingAnimal_vo_die";
			this.IncorrectTokenSoundDelay = 300;
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x001AA062 File Offset: 0x001A8262
		public override string GetSifrahCategory()
		{
			return SocialSifrah.CATEGORY;
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x060057D0 RID: 22480 RVA: 0x001AA069 File Offset: 0x001A8269
		public static bool AnyEnabled
		{
			get
			{
				return Options.SifrahHaggling || Options.SifrahRecruitment;
			}
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x001AA079 File Offset: 0x001A8279
		public static void AwardInsight()
		{
			if (SocialSifrah.AnyEnabled)
			{
				SifrahGame.AwardInsight(SocialSifrah.CATEGORY, "social interaction");
			}
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x001AA094 File Offset: 0x001A8294
		public static void AddTokenTokens(List<SifrahPrioritizableToken> List, GameObject ContextObject)
		{
			if (ContextObject.IsMemberOfFaction("Farmers") || ContextObject.IsMemberOfFaction("Wardens"))
			{
				List.Add(new SocialSifrahTokenDisplayAFarmersToken());
			}
			if (ContextObject.IsMemberOfFaction("Merchants") || ContextObject.IsMemberOfFaction("Dromad") || ContextObject.IsMemberOfFaction("Consortium"))
			{
				List.Add(new SocialSifrahTokenDisplayAMerchantsToken());
			}
			if (ContextObject.IsMemberOfFaction("Birds") || ContextObject.IsMemberOfFaction("Prey") || ContextObject.IsMemberOfFaction("Antelopes") || ContextObject.IsMemberOfFaction("Equines") || ContextObject.IsMemberOfFaction("Water"))
			{
				List.Add(new SocialSifrahTokenDisplayAMinstrelsToken());
			}
			if (ContextObject.IsMemberOfFaction("Barathrumites"))
			{
				List.Add(new SocialSifrahTokenDisplayABarathrumiteToken());
			}
		}

		// Token: 0x04002C12 RID: 11282
		public static readonly string CATEGORY = "Social";
	}
}
