using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013E8 RID: 5096
	[Serializable]
	public class Empathy : BaseMutation
	{
		// Token: 0x0600C51B RID: 50459 RVA: 0x0047C29E File Offset: 0x0047A49E
		public Empathy()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C51C RID: 50460 RVA: 0x0047C2B1 File Offset: 0x0047A4B1
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C51D RID: 50461 RVA: 0x0047C2BB File Offset: 0x0047A4BB
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C51E RID: 50462 RVA: 0x0047C2C4 File Offset: 0x0047A4C4
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C51F RID: 50463 RVA: 0x0047C2CE File Offset: 0x0047A4CE
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
