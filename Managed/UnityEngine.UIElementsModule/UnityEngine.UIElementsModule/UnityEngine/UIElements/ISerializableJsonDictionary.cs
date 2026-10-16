using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E2 RID: 738
	internal interface ISerializableJsonDictionary
	{
		// Token: 0x0600151A RID: 5402
		void Set<T>(string key, T value) where T : class;

		// Token: 0x0600151B RID: 5403
		T Get<T>(string key) where T : class;

		// Token: 0x0600151C RID: 5404
		T GetScriptable<T>(string key) where T : ScriptableObject;

		// Token: 0x0600151D RID: 5405
		void Overwrite(object obj, string key);

		// Token: 0x0600151E RID: 5406
		bool ContainsKey(string key);

		// Token: 0x0600151F RID: 5407
		void OnBeforeSerialize();

		// Token: 0x06001520 RID: 5408
		void OnAfterDeserialize();
	}
}
