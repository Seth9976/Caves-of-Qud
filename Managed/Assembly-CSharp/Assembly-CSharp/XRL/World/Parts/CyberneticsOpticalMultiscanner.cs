using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F3B RID: 3899
	[Serializable]
	public class CyberneticsOpticalMultiscanner : IPart
	{
		// Token: 0x06009B86 RID: 39814 RVA: 0x003869EA File Offset: 0x00384BEA
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009B87 RID: 39815 RVA: 0x003869ED File Offset: 0x00384BED
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID;
		}

		// Token: 0x06009B88 RID: 39816 RVA: 0x00386A03 File Offset: 0x00384C03
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (Options.AnySifrah)
			{
				if (Options.SifrahExamine)
				{
					E.Description = "You gain access to the precise hit point, armor, and dodge values of robotic creatures, biological creatures, and structures.\nStaircases and other up/down map transitions are always revealed to you.";
				}
				E.Add("Adds a bonus turn, and is otherwise useful, in most tinkering Sifrah games, and is useful in many social Sifrah games.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009B89 RID: 39817 RVA: 0x00386A30 File Offset: 0x00384C30
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
