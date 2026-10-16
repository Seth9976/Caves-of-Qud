using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02000F1B RID: 3867
	[Serializable]
	public class CyberneticsBiologicalIndexer : IPart
	{
		// Token: 0x06009A27 RID: 39463 RVA: 0x0037F4BB File Offset: 0x0037D6BB
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A28 RID: 39464 RVA: 0x0037F4BE File Offset: 0x0037D6BE
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A29 RID: 39465 RVA: 0x0037F4E4 File Offset: 0x0037D6E4
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (Options.AnySifrah)
			{
				E.Add("Adds a bonus turn, and is otherwise useful, in some tinkering Sifrah games involving biotech artifacts and is useful in many social Sifrah games.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A2A RID: 39466 RVA: 0x0037F4FF File Offset: 0x0037D6FF
		public override bool HandleEvent(ImplantedEvent E)
		{
			E.Implantee.ModIntProperty(this.AffectedProperty, 1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A2B RID: 39467 RVA: 0x0037F51C File Offset: 0x0037D71C
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.ModIntProperty(this.AffectedProperty, -1, true);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A2C RID: 39468 RVA: 0x0037F539 File Offset: 0x0037D739
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004034 RID: 16436
		public string AffectedProperty = "BioScannerEquipped";
	}
}
