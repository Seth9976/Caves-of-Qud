using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000071 RID: 113
	[Serializable]
	public class StoreID
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00008772 File Offset: 0x00006972
		public StoreID(string store_, string id_)
		{
			this.store = store_;
			this.id = id_;
		}

		// Token: 0x04000133 RID: 307
		public string store;

		// Token: 0x04000134 RID: 308
		public string id;
	}
}
