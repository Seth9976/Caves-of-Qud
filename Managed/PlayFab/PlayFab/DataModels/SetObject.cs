using System;
using PlayFab.SharedModels;

namespace PlayFab.DataModels
{
	// Token: 0x02000172 RID: 370
	[Serializable]
	public class SetObject : PlayFabBaseModel
	{
		// Token: 0x04000747 RID: 1863
		public object DataObject;

		// Token: 0x04000748 RID: 1864
		public bool? DeleteObject;

		// Token: 0x04000749 RID: 1865
		public string EscapedDataObject;

		// Token: 0x0400074A RID: 1866
		public string ObjectName;
	}
}
