using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E7F RID: 3711
	public class MakClamSpawner : IPart
	{
		// Token: 0x060095BD RID: 38333 RVA: 0x0036297F File Offset: 0x00360B7F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeObjectCreatedEvent.ID;
		}

		// Token: 0x060095BE RID: 38334 RVA: 0x00362995 File Offset: 0x00360B95
		public override bool HandleEvent(BeforeObjectCreatedEvent E)
		{
			E.ReplacementObject = GameObject.Create("Giant Clam");
			E.ReplacementObject.SetIntProperty("MakClam", 1, false);
			return base.HandleEvent(E);
		}
	}
}
