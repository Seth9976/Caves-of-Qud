using System;
using System.Collections.Generic;

namespace PlayFab
{
	// Token: 0x0200002B RID: 43
	public class PluginContractKeyComparator : EqualityComparer<PluginContractKey>
	{
		// Token: 0x0600030C RID: 780 RVA: 0x00010328 File Offset: 0x0000E528
		public override bool Equals(PluginContractKey x, PluginContractKey y)
		{
			return x._pluginContract == y._pluginContract && x._pluginName.Equals(y._pluginName);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0001034B File Offset: 0x0000E54B
		public override int GetHashCode(PluginContractKey obj)
		{
			return (int)(obj._pluginContract + obj._pluginName.GetHashCode());
		}
	}
}
