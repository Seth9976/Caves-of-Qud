using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001431 RID: 5169
	[Serializable]
	public class NightVision : BaseMutation
	{
		// Token: 0x0600C99A RID: 51610 RVA: 0x00496611 File Offset: 0x00494811
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
		}

		// Token: 0x0600C99B RID: 51611 RVA: 0x00496613 File Offset: 0x00494813
		public override string GetDescription()
		{
			return "";
		}

		// Token: 0x0600C99C RID: 51612 RVA: 0x0049661A File Offset: 0x0049481A
		public override string GetLevelText(int Level)
		{
			return "You see in the dark.\n";
		}

		// Token: 0x0600C99D RID: 51613 RVA: 0x00496621 File Offset: 0x00494821
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600C99E RID: 51614 RVA: 0x00496637 File Offset: 0x00494837
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.ParentObject.IsPlayer())
			{
				base.AddLight(base.Level, LightLevel.Darkvision, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C99F RID: 51615 RVA: 0x0049665C File Offset: 0x0049485C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C9A0 RID: 51616 RVA: 0x00496665 File Offset: 0x00494865
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C9A1 RID: 51617 RVA: 0x0049666F File Offset: 0x0049486F
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
