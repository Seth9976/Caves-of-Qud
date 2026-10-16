using System;

namespace System
{
	/// <summary>A customizable parser based on the HTTP scheme.</summary>
	// Token: 0x02000161 RID: 353
	public class HttpStyleUriParser : UriParser
	{
		/// <summary>Create a customizable parser based on the HTTP scheme.</summary>
		// Token: 0x06000993 RID: 2451 RVA: 0x0002A291 File Offset: 0x00028491
		public HttpStyleUriParser()
			: base(UriParser.HttpUri.Flags)
		{
		}
	}
}
