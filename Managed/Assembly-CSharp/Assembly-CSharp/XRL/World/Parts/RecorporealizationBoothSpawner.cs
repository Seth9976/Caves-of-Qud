using System;

namespace XRL.World.Parts
{
	// Token: 0x02000FB2 RID: 4018
	[Serializable]
	public class RecorporealizationBoothSpawner : IPart
	{
		// Token: 0x06009FBF RID: 40895 RVA: 0x003A0F60 File Offset: 0x0039F160
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009FC0 RID: 40896 RVA: 0x003A0F63 File Offset: 0x0039F163
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009FC1 RID: 40897 RVA: 0x003A0F78 File Offset: 0x0039F178
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				Cell parameter = E.GetParameter<Cell>("Cell");
				new RecorporealizationBoothSpawnerBuilder().BuildZone(parameter.ParentZone);
				this.ParentObject.Destroy(null, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400424E RID: 16974
		public string Group = "A";

		// Token: 0x0400424F RID: 16975
		public int Period = 1;
	}
}
