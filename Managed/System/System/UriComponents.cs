using System;

namespace System
{
	/// <summary>Specifies the parts of a <see cref="T:System.Uri" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015A RID: 346
	[Flags]
	public enum UriComponents
	{
		/// <summary>The <see cref="P:System.Uri.Scheme" /> data.</summary>
		// Token: 0x04000634 RID: 1588
		Scheme = 1,
		/// <summary>The <see cref="P:System.Uri.UserInfo" /> data.</summary>
		// Token: 0x04000635 RID: 1589
		UserInfo = 2,
		/// <summary>The <see cref="P:System.Uri.Host" /> data.</summary>
		// Token: 0x04000636 RID: 1590
		Host = 4,
		/// <summary>The <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000637 RID: 1591
		Port = 8,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> data.</summary>
		// Token: 0x04000638 RID: 1592
		Path = 16,
		/// <summary>The <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000639 RID: 1593
		Query = 32,
		/// <summary>The <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x0400063A RID: 1594
		Fragment = 64,
		/// <summary>The <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x0400063B RID: 1595
		StrongPort = 128,
		/// <summary>The normalized form of the <see cref="P:System.Uri.Host" />.</summary>
		// Token: 0x0400063C RID: 1596
		NormalizedHost = 256,
		/// <summary>Specifies that the delimiter should be included.</summary>
		// Token: 0x0400063D RID: 1597
		KeepDelimiter = 1073741824,
		/// <summary>The complete <see cref="T:System.Uri" /> context that is needed for Uri Serializers. The context includes the IPv6 scope.</summary>
		// Token: 0x0400063E RID: 1598
		SerializationInfoString = -2147483648,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, <see cref="P:System.Uri.Query" />, and <see cref="P:System.Uri.Fragment" /> data.</summary>
		// Token: 0x0400063F RID: 1599
		AbsoluteUri = 127,
		/// <summary>The <see cref="P:System.Uri.Host" /> and <see cref="P:System.Uri.Port" /> data. If no port data is in the Uri and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000640 RID: 1600
		HostAndPort = 132,
		/// <summary>The <see cref="P:System.Uri.UserInfo" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data. If no port data is in the <see cref="T:System.Uri" /> and a default port has been assigned to the <see cref="P:System.Uri.Scheme" />, the default port is returned. If there is no default port, -1 is returned.</summary>
		// Token: 0x04000641 RID: 1601
		StrongAuthority = 134,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, and <see cref="P:System.Uri.Port" /> data.</summary>
		// Token: 0x04000642 RID: 1602
		SchemeAndServer = 13,
		/// <summary>The <see cref="P:System.Uri.Scheme" />, <see cref="P:System.Uri.Host" />, <see cref="P:System.Uri.Port" />, <see cref="P:System.Uri.LocalPath" />, and <see cref="P:System.Uri.Query" /> data.</summary>
		// Token: 0x04000643 RID: 1603
		HttpRequestUrl = 61,
		/// <summary>The <see cref="P:System.Uri.LocalPath" /> and <see cref="P:System.Uri.Query" /> data. Also see <see cref="P:System.Uri.PathAndQuery" />. </summary>
		// Token: 0x04000644 RID: 1604
		PathAndQuery = 48
	}
}
