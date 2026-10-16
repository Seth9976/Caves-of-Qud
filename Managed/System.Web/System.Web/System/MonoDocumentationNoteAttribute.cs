using System;

namespace System
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoDocumentationNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002C49 File Offset: 0x00000E49
		public MonoDocumentationNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
