using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013DB RID: 5083
	[Serializable]
	public class SociallyRepugnant : BaseMutation
	{
		// Token: 0x0600C467 RID: 50279 RVA: 0x00478357 File Offset: 0x00476557
		public SociallyRepugnant()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C468 RID: 50280 RVA: 0x0047836A File Offset: 0x0047656A
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C469 RID: 50281 RVA: 0x0047836D File Offset: 0x0047656D
		public override string GetDescription()
		{
			return "Others find it difficult to tolerate you in social settings.\n\n-50 reputation with every faction";
		}

		// Token: 0x0600C46A RID: 50282 RVA: 0x00478374 File Offset: 0x00476574
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C46B RID: 50283 RVA: 0x0047837B File Offset: 0x0047657B
		public override bool Mutate(GameObject GO, int Level)
		{
			GO.ModIntProperty("AllVisibleRepModifier", -50, false);
			this.appliedBonus = -50;
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C46C RID: 50284 RVA: 0x0047839C File Offset: 0x0047659C
		public override bool Unmutate(GameObject GO)
		{
			GO.ModIntProperty("AllVisibleRepModifier", -this.appliedBonus, false);
			this.appliedBonus = 0;
			return base.Unmutate(GO);
		}

		// Token: 0x04004DD4 RID: 19924
		[NonSerialized]
		public static string[] waterRitualOptions = new string[] { "Swap spittle?", "Let's get to it.", "{{emote|licks lips}}", "My thirst is mine, your water is yours.", "Thirst. Water. Drink.", "Slurp time.", "????", "My water is thirst, your water is yours" };

		// Token: 0x04004DD5 RID: 19925
		public int appliedBonus;
	}
}
