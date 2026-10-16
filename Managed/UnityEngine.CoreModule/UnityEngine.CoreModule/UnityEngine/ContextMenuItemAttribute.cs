using System;

namespace UnityEngine
{
	// Token: 0x02000230 RID: 560
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class ContextMenuItemAttribute : PropertyAttribute
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x0003A7FC File Offset: 0x000389FC
		public ContextMenuItemAttribute(string name, string function)
		{
			this.name = name;
			this.function = function;
		}

		// Token: 0x040008FE RID: 2302
		public readonly string name;

		// Token: 0x040008FF RID: 2303
		public readonly string function;
	}
}
