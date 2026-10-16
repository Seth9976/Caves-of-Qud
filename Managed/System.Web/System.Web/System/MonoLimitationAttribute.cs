using System;

namespace System
{
	// Token: 0x02000013 RID: 19
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoLimitationAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600003B RID: 59 RVA: 0x00002C49 File Offset: 0x00000E49
		public MonoLimitationAttribute(string comment)
			: base(comment)
		{
		}
	}
}
