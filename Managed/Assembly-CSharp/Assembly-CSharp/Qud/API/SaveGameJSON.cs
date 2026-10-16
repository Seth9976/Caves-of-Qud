using System;
using System.Collections.Generic;

namespace Qud.API
{
	// Token: 0x0200040C RID: 1036
	[Serializable]
	public class SaveGameJSON
	{
		// Token: 0x04001361 RID: 4961
		public int InfoVersion = 1;

		// Token: 0x04001362 RID: 4962
		public int SaveVersion;

		// Token: 0x04001363 RID: 4963
		public string GameVersion;

		// Token: 0x04001364 RID: 4964
		public string ID;

		// Token: 0x04001365 RID: 4965
		public string Name;

		// Token: 0x04001366 RID: 4966
		public int Level;

		// Token: 0x04001367 RID: 4967
		public string GenoSubType;

		// Token: 0x04001368 RID: 4968
		public string GameMode;

		// Token: 0x04001369 RID: 4969
		public string CharIcon;

		// Token: 0x0400136A RID: 4970
		public char FColor;

		// Token: 0x0400136B RID: 4971
		public char DColor;

		// Token: 0x0400136C RID: 4972
		public string Location;

		// Token: 0x0400136D RID: 4973
		public string InGameTime;

		// Token: 0x0400136E RID: 4974
		public long Turn;

		// Token: 0x0400136F RID: 4975
		public string SaveTime;

		// Token: 0x04001370 RID: 4976
		public List<string> ModsEnabled;
	}
}
