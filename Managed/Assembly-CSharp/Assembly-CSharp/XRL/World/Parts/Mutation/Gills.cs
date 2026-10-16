using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001419 RID: 5145
	[Serializable]
	public class Gills : BaseMutation
	{
		// Token: 0x0600C805 RID: 51205 RVA: 0x0048CFCC File Offset: 0x0048B1CC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C806 RID: 51206 RVA: 0x0048CFD6 File Offset: 0x0048B1D6
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C807 RID: 51207 RVA: 0x0048CFDF File Offset: 0x0048B1DF
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C808 RID: 51208 RVA: 0x0048CFE9 File Offset: 0x0048B1E9
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
