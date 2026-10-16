using System;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x02000597 RID: 1431
	public class QudBuildLibraryModuleData : AbstractEmbarkBuilderModuleData
	{
		// Token: 0x06003D54 RID: 15700 RVA: 0x0012C847 File Offset: 0x0012AA47
		public QudBuildLibraryModuleData()
		{
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x0012C84F File Offset: 0x0012AA4F
		public QudBuildLibraryModuleData(string buildID)
		{
			this.BuildID = buildID;
		}

		// Token: 0x04001E7A RID: 7802
		public string BuildID;
	}
}
