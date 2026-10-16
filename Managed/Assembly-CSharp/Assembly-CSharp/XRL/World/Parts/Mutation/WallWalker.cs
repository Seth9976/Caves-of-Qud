using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001428 RID: 5160
	[Serializable]
	public class WallWalker : BaseMutation
	{
		// Token: 0x0600C917 RID: 51479 RVA: 0x004943AB File Offset: 0x004925AB
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C918 RID: 51480 RVA: 0x004943AE File Offset: 0x004925AE
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C919 RID: 51481 RVA: 0x004943B8 File Offset: 0x004925B8
		public override string GetDescription()
		{
			return "You can move across walls, and only across walls.";
		}

		// Token: 0x0600C91A RID: 51482 RVA: 0x004943BF File Offset: 0x004925BF
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C91B RID: 51483 RVA: 0x004943C6 File Offset: 0x004925C6
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C91C RID: 51484 RVA: 0x004943CF File Offset: 0x004925CF
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C91D RID: 51485 RVA: 0x004943D8 File Offset: 0x004925D8
		public override bool Mutate(GameObject GO, int Level)
		{
			if (GO.Brain != null)
			{
				GO.Brain.WallWalker = true;
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C91E RID: 51486 RVA: 0x004943F6 File Offset: 0x004925F6
		public override bool Unmutate(GameObject GO)
		{
			if (GO.Brain != null)
			{
				GO.Brain.WallWalker = false;
			}
			return base.Unmutate(GO);
		}
	}
}
