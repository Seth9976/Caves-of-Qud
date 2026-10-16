using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C8 RID: 5064
	[Serializable]
	public class Analgesia : BaseMutation
	{
		// Token: 0x0600C3A9 RID: 50089 RVA: 0x00476427 File Offset: 0x00474627
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3AA RID: 50090 RVA: 0x0047642A File Offset: 0x0047462A
		public override string GetDescription()
		{
			return "You lack a developed sense of pain.\n\nYou only know your general state of health and not your precise number of hit points.";
		}

		// Token: 0x0600C3AB RID: 50091 RVA: 0x00476431 File Offset: 0x00474631
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3AC RID: 50092 RVA: 0x00476438 File Offset: 0x00474638
		public override bool Mutate(GameObject GO, int Level)
		{
			GO.ModIntProperty("Analgesia", 1, false);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3AD RID: 50093 RVA: 0x00476450 File Offset: 0x00474650
		public override bool Unmutate(GameObject GO)
		{
			GO.ModIntProperty("Analgesia", -1, true);
			return base.Unmutate(GO);
		}
	}
}
