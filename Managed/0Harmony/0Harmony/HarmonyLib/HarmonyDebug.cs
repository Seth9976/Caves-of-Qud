using System;

namespace HarmonyLib
{
	// Token: 0x02000053 RID: 83
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyDebug : HarmonyAttribute
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000AFCC File Offset: 0x000091CC
		public HarmonyDebug()
		{
			this.info.debug = new bool?(true);
		}
	}
}
