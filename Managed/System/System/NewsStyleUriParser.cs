using System;

namespace System
{
	/// <summary>A customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
	// Token: 0x02000164 RID: 356
	public class NewsStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the news scheme using the Network News Transfer Protocol (NNTP).</summary>
		// Token: 0x06000996 RID: 2454 RVA: 0x0002A2C7 File Offset: 0x000284C7
		public NewsStyleUriParser()
			: base(UriParser.NewsUri.Flags)
		{
		}
	}
}
