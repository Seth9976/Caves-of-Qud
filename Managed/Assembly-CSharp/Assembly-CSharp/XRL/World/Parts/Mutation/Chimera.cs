using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142A RID: 5162
	[Serializable]
	public class Chimera : BaseMutation
	{
		// Token: 0x0600C92C RID: 51500 RVA: 0x004949E1 File Offset: 0x00492BE1
		public Chimera()
		{
			base.Type = "Chimera";
		}

		// Token: 0x0600C92D RID: 51501 RVA: 0x004949F4 File Offset: 0x00492BF4
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C92E RID: 51502 RVA: 0x004949F7 File Offset: 0x00492BF7
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C92F RID: 51503 RVA: 0x004949FA File Offset: 0x00492BFA
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C930 RID: 51504 RVA: 0x00494A04 File Offset: 0x00492C04
		public override string GetDescription()
		{
			return "You only manifest physical mutations, and all of your mutation choices when manifesting a new mutation are physical.\n\n" + "Whenever you manifest a new mutation, one of your choices will also cause you to grow a new limb at random.";
		}

		// Token: 0x0600C931 RID: 51505 RVA: 0x00494A15 File Offset: 0x00492C15
		public override string GetLevelText(int Level)
		{
			return "";
		}
	}
}
