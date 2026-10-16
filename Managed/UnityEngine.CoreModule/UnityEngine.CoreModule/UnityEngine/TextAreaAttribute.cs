using System;

namespace UnityEngine
{
	// Token: 0x02000238 RID: 568
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class TextAreaAttribute : PropertyAttribute
	{
		// Token: 0x06001E68 RID: 7784 RVA: 0x0003A8BC File Offset: 0x00038ABC
		public TextAreaAttribute()
		{
			this.minLines = 3;
			this.maxLines = 3;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0003A8D4 File Offset: 0x00038AD4
		public TextAreaAttribute(int minLines, int maxLines)
		{
			this.minLines = minLines;
			this.maxLines = maxLines;
		}

		// Token: 0x04000908 RID: 2312
		public readonly int minLines;

		// Token: 0x04000909 RID: 2313
		public readonly int maxLines;
	}
}
