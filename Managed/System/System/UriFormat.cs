using System;

namespace System
{
	/// <summary>Controls how URI information is escaped.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015B RID: 347
	public enum UriFormat
	{
		/// <summary>Escaping is performed according to the rules in RFC 2396.</summary>
		// Token: 0x04000646 RID: 1606
		UriEscaped = 1,
		/// <summary>No escaping is performed.</summary>
		// Token: 0x04000647 RID: 1607
		Unescaped,
		/// <summary>Characters that have a reserved meaning in the requested URI components remain escaped. All others are not escaped. See Remarks.</summary>
		// Token: 0x04000648 RID: 1608
		SafeUnescaped
	}
}
