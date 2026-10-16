using System;

namespace System.Reflection
{
	// Token: 0x020008D8 RID: 2264
	public static class MethodInfoExtensions
	{
		// Token: 0x06004B7C RID: 19324 RVA: 0x000F089C File Offset: 0x000EEA9C
		public static MethodInfo GetBaseDefinition(MethodInfo method)
		{
			Requires.NotNull(method, "method");
			return method.GetBaseDefinition();
		}
	}
}
