using System;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200028F RID: 655
	public interface INamedItemFactory<TInstanceType, TDefinitionType> where TInstanceType : class
	{
		// Token: 0x06001740 RID: 5952
		void RegisterDefinition(string itemName, TDefinitionType itemDefinition);

		// Token: 0x06001741 RID: 5953
		bool TryGetDefinition(string itemName, out TDefinitionType result);

		// Token: 0x06001742 RID: 5954
		TInstanceType CreateInstance(string itemName);

		// Token: 0x06001743 RID: 5955
		bool TryCreateInstance(string itemName, out TInstanceType result);
	}
}
