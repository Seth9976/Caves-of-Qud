using System;

namespace System
{
	/// <summary>A customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
	// Token: 0x02000162 RID: 354
	public class FtpStyleUriParser : UriParser
	{
		/// <summary>Creates a customizable parser based on the File Transfer Protocol (FTP) scheme.</summary>
		// Token: 0x06000994 RID: 2452 RVA: 0x0002A2A3 File Offset: 0x000284A3
		public FtpStyleUriParser()
			: base(UriParser.FtpUri.Flags)
		{
		}
	}
}
