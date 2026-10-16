using System;

namespace HarmonyLib
{
	// Token: 0x02000051 RID: 81
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
	public class HarmonyBefore : HarmonyAttribute
	{
		// Token: 0x0600018B RID: 395 RVA: 0x0000AFA4 File Offset: 0x000091A4
		public HarmonyBefore(params string[] before)
		{
			this.info.before = before;
		}
	}
}
