using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001430 RID: 5168
	[Serializable]
	public class DarkVision : BaseMutation
	{
		// Token: 0x0600C98F RID: 51599 RVA: 0x0049658E File Offset: 0x0049478E
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C990 RID: 51600 RVA: 0x00496591 File Offset: 0x00494791
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C991 RID: 51601 RVA: 0x0049659B File Offset: 0x0049479B
		public override string GetDescription()
		{
			return "You see in the dark.";
		}

		// Token: 0x0600C992 RID: 51602 RVA: 0x004965A2 File Offset: 0x004947A2
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C993 RID: 51603 RVA: 0x004965A9 File Offset: 0x004947A9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600C994 RID: 51604 RVA: 0x004965BF File Offset: 0x004947BF
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.ParentObject.IsPlayer())
			{
				base.AddLight(this.Radius, LightLevel.Dimvision, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C995 RID: 51605 RVA: 0x004965E4 File Offset: 0x004947E4
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x0600C996 RID: 51606 RVA: 0x004965ED File Offset: 0x004947ED
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C997 RID: 51607 RVA: 0x004965F6 File Offset: 0x004947F6
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C998 RID: 51608 RVA: 0x00496600 File Offset: 0x00494800
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004EC0 RID: 20160
		public int Radius = 5;
	}
}
