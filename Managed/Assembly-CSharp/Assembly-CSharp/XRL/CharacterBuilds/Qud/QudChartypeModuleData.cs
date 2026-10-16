using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059C RID: 1436
	public class QudChartypeModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003D71 RID: 15729 RVA: 0x0012D16A File Offset: 0x0012B36A
		public QudChartypeModuleData()
		{
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x0012D172 File Offset: 0x0012B372
		public QudChartypeModuleData(string type)
		{
			this.type = type;
		}

		// Token: 0x04001E7D RID: 7805
		public string type;
	}
}
