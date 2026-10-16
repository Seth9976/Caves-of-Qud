using System;

namespace System
{
	/// <summary>A customizable parser based on the Gopher scheme.</summary>
	// Token: 0x02000165 RID: 357
	public class GopherStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the Gopher scheme.</summary>
		// Token: 0x06000997 RID: 2455 RVA: 0x0002A2D9 File Offset: 0x000284D9
		public GopherStyleUriParser()
			: base(UriParser.GopherUri.Flags)
		{
		}
	}
}
