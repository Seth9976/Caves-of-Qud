using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059E RID: 1438
	public class QudChooseStartingLocationModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003D86 RID: 15750 RVA: 0x0012D8BA File Offset: 0x0012BABA
		public QudChooseStartingLocationModuleData()
		{
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x0012D8C2 File Offset: 0x0012BAC2
		public QudChooseStartingLocationModuleData(string startingLocation)
		{
			this.StartingLocation = startingLocation;
		}

		// Token: 0x04001E80 RID: 7808
		public string StartingLocation;
	}
}
