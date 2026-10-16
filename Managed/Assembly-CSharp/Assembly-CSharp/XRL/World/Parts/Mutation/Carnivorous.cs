using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CC RID: 5068
	[Serializable]
	public class Carnivorous : BaseMutation
	{
		// Token: 0x0600C3CD RID: 50125 RVA: 0x00476853 File Offset: 0x00474A53
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3CE RID: 50126 RVA: 0x00476856 File Offset: 0x00474A56
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C3CF RID: 50127 RVA: 0x00476860 File Offset: 0x00474A60
		public override string GetDescription()
		{
			return "You eat meat exclusively.\n\nYou get no satiation from foods that aren't meat.\nIf you eat raw food that isn't meat, there's a 50% chance you become ill for 2 hours.\nYou can't cook with plant or fungus ingredients.\nYou don't get ill when you eat raw meat.\nYou can eat raw meat without being famished.\n";
		}

		// Token: 0x0600C3D0 RID: 50128 RVA: 0x00476867 File Offset: 0x00474A67
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3D1 RID: 50129 RVA: 0x0047686E File Offset: 0x00474A6E
		public override bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x0600C3D2 RID: 50130 RVA: 0x00476871 File Offset: 0x00474A71
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3D3 RID: 50131 RVA: 0x0047687A File Offset: 0x00474A7A
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3D4 RID: 50132 RVA: 0x00476884 File Offset: 0x00474A84
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
