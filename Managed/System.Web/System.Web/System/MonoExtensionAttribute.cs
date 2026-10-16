using System;

namespace System
{
	// Token: 0x02000011 RID: 17
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoExtensionAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002C49 File Offset: 0x00000E49
		public MonoExtensionAttribute(string comment)
			: base(comment)
		{
		}
	}
}
