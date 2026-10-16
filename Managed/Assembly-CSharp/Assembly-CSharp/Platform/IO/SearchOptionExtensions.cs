using System;
using System.IO;

namespace Platform.IO
{
	// Token: 0x0200034A RID: 842
	public static class SearchOptionExtensions
	{
		// Token: 0x06001F74 RID: 8052 RVA: 0x000990C2 File Offset: 0x000972C2
		public static SearchOption ToSystemIOEquivalent(this SearchOption option)
		{
			if (option == SearchOption.TopDirectoryOnly)
			{
				return SearchOption.TopDirectoryOnly;
			}
			if (option != SearchOption.AllDirectories)
			{
				throw new ArgumentOutOfRangeException("option", option, null);
			}
			return SearchOption.AllDirectories;
		}
	}
}
