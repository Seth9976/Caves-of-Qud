using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005AD RID: 1453
	public class QudPregenModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003DED RID: 15853 RVA: 0x00130043 File Offset: 0x0012E243
		public QudPregenModuleData()
		{
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x0013004B File Offset: 0x0012E24B
		public QudPregenModuleData(string pregen)
		{
			this.Pregen = pregen;
		}

		// Token: 0x04001EB6 RID: 7862
		public string Pregen;
	}
}
