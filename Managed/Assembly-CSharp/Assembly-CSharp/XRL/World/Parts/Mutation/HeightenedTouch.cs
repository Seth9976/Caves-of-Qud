using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001449 RID: 5193
	[Serializable]
	public class HeightenedTouch : BaseMutation
	{
		// Token: 0x0600CACC RID: 51916 RVA: 0x0049B9FB File Offset: 0x00499BFB
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CACD RID: 51917 RVA: 0x0049BA05 File Offset: 0x00499C05
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CACE RID: 51918 RVA: 0x0049BA0E File Offset: 0x00499C0E
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CACF RID: 51919 RVA: 0x0049BA18 File Offset: 0x00499C18
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
