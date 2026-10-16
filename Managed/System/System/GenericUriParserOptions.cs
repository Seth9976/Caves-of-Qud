using System;

namespace System
{
	/// <summary>Specifies options for a <see cref="T:System.UriParser" />.</summary>
	// Token: 0x0200014E RID: 334
	[Flags]
	public enum GenericUriParserOptions
	{
		/// <summary>The parser:</summary>
		// Token: 0x040005A3 RID: 1443
		Default = 0,
		/// <summary>The parser allows a registry-based authority.</summary>
		// Token: 0x040005A4 RID: 1444
		GenericAuthority = 1,
		/// <summary>The parser allows a URI with no authority.</summary>
		// Token: 0x040005A5 RID: 1445
		AllowEmptyAuthority = 2,
		/// <summary>The scheme does not define a user information part.</summary>
		// Token: 0x040005A6 RID: 1446
		NoUserInfo = 4,
		/// <summary>The scheme does not define a port.</summary>
		// Token: 0x040005A7 RID: 1447
		NoPort = 8,
		/// <summary>The scheme does not define a query part.</summary>
		// Token: 0x040005A8 RID: 1448
		NoQuery = 16,
		/// <summary>The scheme does not define a fragment part.</summary>
		// Token: 0x040005A9 RID: 1449
		NoFragment = 32,
		/// <summary>The parser does not convert back slashes into forward slashes.</summary>
		// Token: 0x040005AA RID: 1450
		DontConvertPathBackslashes = 64,
		/// <summary>The parser does not canonicalize the URI.</summary>
		// Token: 0x040005AB RID: 1451
		DontCompressPath = 128,
		/// <summary>The parser does not unescape path dots, forward slashes, or back slashes.</summary>
		// Token: 0x040005AC RID: 1452
		DontUnescapePathDotsAndSlashes = 256,
		/// <summary>The parser supports Internationalized Domain Name (IDN) parsing (IDN) of host names. Whether IDN is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x040005AD RID: 1453
		Idn = 512,
		/// <summary>The parser supports the parsing rules specified in RFC 3987 for International Resource Identifiers (IRI). Whether IRI is used is dictated by configuration values. See the Remarks for more information.</summary>
		// Token: 0x040005AE RID: 1454
		IriParsing = 1024
	}
}
