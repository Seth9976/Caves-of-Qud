using System;

namespace System
{
	/// <summary>A customizable parser based on the File scheme.</summary>
	// Token: 0x02000163 RID: 355
	public class FileStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File scheme.</summary>
		// Token: 0x06000995 RID: 2453 RVA: 0x0002A2B5 File Offset: 0x000284B5
		public FileStyleUriParser()
			: base(UriParser.FileUri.Flags)
		{
		}
	}
}
