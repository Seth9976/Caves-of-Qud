using System;

namespace System
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoNotSupportedAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002C49 File Offset: 0x00000E49
		public MonoNotSupportedAttribute(string comment)
			: base(comment)
		{
		}
	}
}
