using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000170 RID: 368
	[Serializable]
	public class ObjectResult : PlayFabBaseModel
	{
		// Token: 0x0400073F RID: 1855
		public object DataObject;

		// Token: 0x04000740 RID: 1856
		public string EscapedDataObject;

		// Token: 0x04000741 RID: 1857
		public string ObjectName;
	}
}
