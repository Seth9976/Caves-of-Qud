using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005CC RID: 1484
	internal class CredentialKey
	{
		// Token: 0x06003000 RID: 12288 RVA: 0x000A5901 File Offset: 0x000A3B01
		internal CredentialKey(Uri uriPrefix, string authenticationType)
		{
			this.UriPrefix = uriPrefix;
			this.UriPrefixLength = this.UriPrefix.ToString().Length;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000A5934 File Offset: 0x000A3B34
		internal bool Match(Uri uri, string authenticationType)
		{
			return !(uri == null) && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.IsPrefix(uri, this.UriPrefix);
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000A5964 File Offset: 0x000A3B64
		internal bool IsPrefix(Uri uri, Uri prefixUri)
		{
			if (prefixUri.Scheme != uri.Scheme || prefixUri.Host != uri.Host || prefixUri.Port != uri.Port)
			{
				return false;
			}
			int num = prefixUri.AbsolutePath.LastIndexOf('/');
			return num <= uri.AbsolutePath.LastIndexOf('/') && string.Compare(uri.AbsolutePath, 0, prefixUri.AbsolutePath, 0, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000A59DF File Offset: 0x000A3BDF
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.UriPrefixLength + this.UriPrefix.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000A5A20 File Offset: 0x000A3C20
		public override bool Equals(object comparand)
		{
			CredentialKey credentialKey = comparand as CredentialKey;
			return comparand != null && string.Compare(this.AuthenticationType, credentialKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.UriPrefix.Equals(credentialKey.UriPrefix);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000A5A60 File Offset: 0x000A3C60
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.UriPrefixLength.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				ValidationHelper.ToString(this.UriPrefix),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001A91 RID: 6801
		internal Uri UriPrefix;

		// Token: 0x04001A92 RID: 6802
		internal int UriPrefixLength = -1;

		// Token: 0x04001A93 RID: 6803
		internal string AuthenticationType;

		// Token: 0x04001A94 RID: 6804
		private int m_HashCode;

		// Token: 0x04001A95 RID: 6805
		private bool m_ComputedHashCode;
	}
}
