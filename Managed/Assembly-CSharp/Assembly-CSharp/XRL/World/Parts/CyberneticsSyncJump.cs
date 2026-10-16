using System;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000F4B RID: 3915
	[Serializable]
	public class CyberneticsSyncJump : IPart
	{
		// Token: 0x06009C4D RID: 40013 RVA: 0x0038B1A9 File Offset: 0x003893A9
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C4E RID: 40014 RVA: 0x0038B1AC File Offset: 0x003893AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009C4F RID: 40015 RVA: 0x0038B1CA File Offset: 0x003893CA
		public override bool HandleEvent(ImplantedEvent E)
		{
			Acrobatics_Jump.SyncAbility(E.Implantee, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C50 RID: 40016 RVA: 0x0038B1DF File Offset: 0x003893DF
		public override bool HandleEvent(UnimplantedEvent E)
		{
			Acrobatics_Jump.SyncAbility(E.Implantee, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C51 RID: 40017 RVA: 0x0038B1F4 File Offset: 0x003893F4
		public override bool AllowStaticRegistration()
		{
			return true;
		}
	}
}
