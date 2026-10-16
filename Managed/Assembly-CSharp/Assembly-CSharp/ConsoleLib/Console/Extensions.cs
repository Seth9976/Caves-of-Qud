using System;
using System.Text;

namespace ConsoleLib.Console
{
	// Token: 0x0200153D RID: 5437
	public static class Extensions
	{
		// Token: 0x0600D3E5 RID: 54245 RVA: 0x004EBB43 File Offset: 0x004E9D43
		public static StringBuilder AppendMarkupNode(this StringBuilder SB, string shader, string text)
		{
			if (string.IsNullOrEmpty(text))
			{
				return SB;
			}
			return SB.Append("{{").Append(shader).Append("|")
				.Append(text)
				.Append("}}");
		}
	}
}
