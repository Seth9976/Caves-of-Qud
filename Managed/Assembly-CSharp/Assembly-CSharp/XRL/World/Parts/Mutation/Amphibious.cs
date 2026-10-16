using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C7 RID: 5063
	[Serializable]
	public class Amphibious : BaseMutation
	{
		// Token: 0x0600C3A1 RID: 50081 RVA: 0x004763E9 File Offset: 0x004745E9
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3A2 RID: 50082 RVA: 0x004763EC File Offset: 0x004745EC
		public override string GetDescription()
		{
			return "Your skin must be kept moist with fresh water.\n\nYou pour water on yourself rather than drinking it to quench your thirst.\nYou require about two-thirds more water than usual.\n+100 reputation with {{w|frogs}}";
		}

		// Token: 0x0600C3A3 RID: 50083 RVA: 0x004763F3 File Offset: 0x004745F3
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3A4 RID: 50084 RVA: 0x004763FA File Offset: 0x004745FA
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C3A5 RID: 50085 RVA: 0x00476403 File Offset: 0x00474603
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3A6 RID: 50086 RVA: 0x0047640C File Offset: 0x0047460C
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3A7 RID: 50087 RVA: 0x00476416 File Offset: 0x00474616
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
