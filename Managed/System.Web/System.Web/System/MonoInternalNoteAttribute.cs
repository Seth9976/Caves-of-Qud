using System;

namespace System
{
	// Token: 0x02000012 RID: 18
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
	internal class MonoInternalNoteAttribute : global::System.MonoTODOAttribute
	{
		// Token: 0x0600003A RID: 58 RVA: 0x00002C49 File Offset: 0x00000E49
		public MonoInternalNoteAttribute(string comment)
			: base(comment)
		{
		}
	}
}
