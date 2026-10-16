using System;

namespace PlayFab.SharedModels
{
	// Token: 0x0200002E RID: 46
	public class PlayFabBaseModel
	{
		// Token: 0x06000318 RID: 792 RVA: 0x000104AF File Offset: 0x0000E6AF
		public string ToJson()
		{
			return PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "").SerializeObject(this);
		}
	}
}
