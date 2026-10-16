using System;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142B RID: 5163
	[Serializable]
	public class Esper : BaseMutation
	{
		// Token: 0x0600C932 RID: 51506 RVA: 0x00494A1C File Offset: 0x00492C1C
		public Esper()
		{
			base.Type = "Esper";
		}

		// Token: 0x0600C933 RID: 51507 RVA: 0x00494A2F File Offset: 0x00492C2F
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C934 RID: 51508 RVA: 0x00494A32 File Offset: 0x00492C32
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetPsionicSifrahSetupEvent>.ID;
		}

		// Token: 0x0600C935 RID: 51509 RVA: 0x00494A48 File Offset: 0x00492C48
		public override bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			E.Rating++;
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x0600C936 RID: 51510 RVA: 0x00494A6D File Offset: 0x00492C6D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C937 RID: 51511 RVA: 0x00494A70 File Offset: 0x00492C70
		public override string GetDescription()
		{
			string text = "You only manifest mental mutations, and all of your mutation choices when manifesting a new mutation are mental.";
			if (Options.AnySifrah)
			{
				text += "\nAdds a bonus turn and improves performance in psionic Sifrah games.";
			}
			return text;
		}

		// Token: 0x0600C938 RID: 51512 RVA: 0x00494A97 File Offset: 0x00492C97
		public override string GetLevelText(int Level)
		{
			return "";
		}
	}
}
