using System;

namespace AiUnity.NLog.Core.Config
{
	// Token: 0x0200028E RID: 654
	internal interface IFactory
	{
		// Token: 0x0600173D RID: 5949
		void Clear();

		// Token: 0x0600173E RID: 5950
		void ScanTypes(Type[] type, string prefix);

		// Token: 0x0600173F RID: 5951
		void RegisterType(Type type, string itemNamePrefix);
	}
}
