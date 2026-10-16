using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F4C RID: 3916
	[Serializable]
	public class CyberneticsTechIndexer : IPart
	{
		// Token: 0x06009C53 RID: 40019 RVA: 0x0038B1FF File Offset: 0x003893FF
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009C54 RID: 40020 RVA: 0x0038B202 File Offset: 0x00389402
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009C55 RID: 40021 RVA: 0x0038B228 File Offset: 0x00389428
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (Options.AnySifrah)
			{
				if (Options.SifrahExamine)
				{
					E.Description = "You gain access to the precise hit point, armor, and dodge values of robotic creatures.";
				}
				E.Add("Adds a bonus turn, and is otherwise useful, in many tinkering Sifrah games, and is useful in some social Sifrah games involving robots.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C56 RID: 40022 RVA: 0x0038B255 File Offset: 0x00389455
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.ModIntProperty(this.AffectedProperty, 1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C57 RID: 40023 RVA: 0x0038B272 File Offset: 0x00389472
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.ModIntProperty(this.AffectedProperty, -1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009C58 RID: 40024 RVA: 0x0038B28F File Offset: 0x0038948F
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x040040AE RID: 16558
		public string AffectedProperty = "TechScannerEquipped";
	}
}
