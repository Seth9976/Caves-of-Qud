using System;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D5 RID: 5077
	[Serializable]
	public class Myopia : BaseMutation
	{
		// Token: 0x0600C430 RID: 50224 RVA: 0x00477AAC File Offset: 0x00475CAC
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C431 RID: 50225 RVA: 0x00477AAF File Offset: 0x00475CAF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C432 RID: 50226 RVA: 0x00477AB9 File Offset: 0x00475CB9
		public override string GetDescription()
		{
			return "You are nearsighted.\n\nYou can only see up to a radius of 10.";
		}

		// Token: 0x0600C433 RID: 50227 RVA: 0x00477AC0 File Offset: 0x00475CC0
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C434 RID: 50228 RVA: 0x00477AC7 File Offset: 0x00475CC7
		public override bool FireEvent(Event E)
		{
			return true;
		}

		// Token: 0x0600C435 RID: 50229 RVA: 0x00477ACA File Offset: 0x00475CCA
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C436 RID: 50230 RVA: 0x00477AD3 File Offset: 0x00475CD3
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C437 RID: 50231 RVA: 0x00477ADD File Offset: 0x00475CDD
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x0600C438 RID: 50232 RVA: 0x00477AE6 File Offset: 0x00475CE6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AdjustVisibilityRadiusEvent>.ID;
		}

		// Token: 0x0600C439 RID: 50233 RVA: 0x00477AFC File Offset: 0x00475CFC
		public override bool HandleEvent(AdjustVisibilityRadiusEvent E)
		{
			if (!this.ParentObject.HasEffect<Spectacles>())
			{
				E.Radius = 10;
			}
			return base.HandleEvent(E);
		}
	}
}
