using System;

namespace System
{
	/// <summary>Defines the parts of a URI for the <see cref="M:System.Uri.GetLeftPart(System.UriPartial)" /> method.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000158 RID: 344
	public enum UriPartial
	{
		/// <summary>The scheme segment of the URI.</summary>
		// Token: 0x0400062B RID: 1579
		Scheme,
		/// <summary>The scheme and authority segments of the URI.</summary>
		// Token: 0x0400062C RID: 1580
		Authority,
		/// <summary>The scheme, authority, and path segments of the URI.</summary>
		// Token: 0x0400062D RID: 1581
		Path,
		/// <summary>The scheme, authority, path, and query segments of the URI.</summary>
		// Token: 0x0400062E RID: 1582
		Query
	}
}
