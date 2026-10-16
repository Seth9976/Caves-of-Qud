using System;

namespace System.Web.Compilation
{
	// Token: 0x0200061F RID: 1567
	internal sealed class TextBlock
	{
		// Token: 0x0600431D RID: 17181 RVA: 0x000B1299 File Offset: 0x000AF499
		public TextBlock(TextBlockType type, string content)
		{
			this.Content = content;
			this.Type = type;
			this.Length = content.Length;
		}

		// Token: 0x0600431E RID: 17182 RVA: 0x000B12BB File Offset: 0x000AF4BB
		public override string ToString()
		{
			return base.GetType().FullName + " [" + this.Type.ToString() + "]";
		}

		// Token: 0x040023FD RID: 9213
		public string Content;

		// Token: 0x040023FE RID: 9214
		public readonly TextBlockType Type;

		// Token: 0x040023FF RID: 9215
		public readonly int Length;
	}
}
