using System;
using System.Collections.Generic;

namespace UnityEngine.Purchasing
{
	// Token: 0x0200007B RID: 123
	internal class StoreConfiguration
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000098DB File Offset: 0x00007ADB
		// (set) Token: 0x060002F1 RID: 753 RVA: 0x000098E3 File Offset: 0x00007AE3
		public AppStore androidStore { get; private set; }

		// Token: 0x060002F2 RID: 754 RVA: 0x000098EC File Offset: 0x00007AEC
		public StoreConfiguration(AppStore store)
		{
			this.androidStore = store;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000098FC File Offset: 0x00007AFC
		public static string Serialize(StoreConfiguration store)
		{
			return MiniJson.JsonEncode(new Dictionary<string, object> { 
			{
				"androidStore",
				store.androidStore.ToString()
			} });
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00009934 File Offset: 0x00007B34
		public static StoreConfiguration Deserialize(string json)
		{
			Dictionary<string, object> dictionary = (Dictionary<string, object>)MiniJson.JsonDecode(json);
			string text = (string)dictionary["androidStore"];
			return new StoreConfiguration((!Enum.IsDefined(typeof(AppStore), text)) ? AppStore.GooglePlay : ((AppStore)Enum.Parse(typeof(AppStore), (string)dictionary["androidStore"], true)));
		}
	}
}
