using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001434 RID: 5172
	[Serializable]
	public class EnergyMetamorphosis : BaseMutation
	{
		// Token: 0x0600C9E3 RID: 51683 RVA: 0x00497B70 File Offset: 0x00495D70
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C9E4 RID: 51684 RVA: 0x00497B7A File Offset: 0x00495D7A
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C9E5 RID: 51685 RVA: 0x00497B83 File Offset: 0x00495D83
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C9E6 RID: 51686 RVA: 0x00497B8D File Offset: 0x00495D8D
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
