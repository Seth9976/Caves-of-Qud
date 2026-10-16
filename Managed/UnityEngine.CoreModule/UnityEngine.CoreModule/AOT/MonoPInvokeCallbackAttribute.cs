using System;

namespace AOT
{
	// Token: 0x02000006 RID: 6
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class MonoPInvokeCallbackAttribute : Attribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002082 File Offset: 0x00000282
		public MonoPInvokeCallbackAttribute(Type type)
		{
		}
	}
}
