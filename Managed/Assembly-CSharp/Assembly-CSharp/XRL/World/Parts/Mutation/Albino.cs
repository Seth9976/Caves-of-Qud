using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C5 RID: 5061
	[Serializable]
	public class Albino : BaseMutation
	{
		// Token: 0x0600C38B RID: 50059 RVA: 0x004760BC File Offset: 0x004742BC
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C38C RID: 50060 RVA: 0x004760BF File Offset: 0x004742BF
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Regenerating2");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C38D RID: 50061 RVA: 0x004760D4 File Offset: 0x004742D4
		public override string GetDescription()
		{
			return "Your skin, hair, and eyes are absent of pigment.\n\nYou regenerate hit points at one-fifth the usual rate in the daylight.";
		}

		// Token: 0x0600C38E RID: 50062 RVA: 0x004760DB File Offset: 0x004742DB
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C38F RID: 50063 RVA: 0x004760E4 File Offset: 0x004742E4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating2")
			{
				if (!base.IsDay())
				{
					return true;
				}
				Zone currentZone = this.ParentObject.CurrentZone;
				if (currentZone == null || !currentZone.IsOutside())
				{
					return true;
				}
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) / 5);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C390 RID: 50064 RVA: 0x00476147 File Offset: 0x00474347
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C391 RID: 50065 RVA: 0x00476150 File Offset: 0x00474350
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C392 RID: 50066 RVA: 0x0047615A File Offset: 0x0047435A
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
