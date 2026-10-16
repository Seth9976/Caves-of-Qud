using System;
using System.Collections.Generic;
using System.Text;
using ConsoleLib.Console;

namespace XRL.World.Parts
{
	// Token: 0x02000E12 RID: 3602
	public class IBookContents
	{
		// Token: 0x060092C9 RID: 37577 RVA: 0x0034E274 File Offset: 0x0034C474
		public void AppendCatalogLine(string Left, string Right, string Separator, int Width, StringBuilder SB)
		{
			int num = Width - ColorUtility.LengthExceptFormatting(Left) - ColorUtility.LengthExceptFormatting(Right);
			SB.Append(Left);
			int num2 = ColorUtility.LengthExceptFormatting(Separator);
			for (int i = 0; i < num; i += num2)
			{
				SB.Append(Separator);
			}
			SB.Append(Right);
		}

		// Token: 0x060092CA RID: 37578 RVA: 0x0034E2C0 File Offset: 0x0034C4C0
		public virtual string GetTitle()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060092CB RID: 37579 RVA: 0x0034E2C7 File Offset: 0x0034C4C7
		public virtual List<IBookContents.BookPageInfo> GetContents()
		{
			throw new NotImplementedException();
		}

		// Token: 0x02001C61 RID: 7265
		[Serializable]
		public class BookPageInfo : IComposite
		{
			// Token: 0x04006FFC RID: 28668
			public string Title;

			// Token: 0x04006FFD RID: 28669
			public string Text;

			// Token: 0x04006FFE RID: 28670
			public string Format;

			// Token: 0x04006FFF RID: 28671
			public string Margins = "2,2,2,2";
		}
	}
}
