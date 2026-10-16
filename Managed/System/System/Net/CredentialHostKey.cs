using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020005CB RID: 1483
	internal class CredentialHostKey
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x000A5780 File Offset: 0x000A3980
		internal CredentialHostKey(string host, int port, string authenticationType)
		{
			this.Host = host;
			this.Port = port;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000A579D File Offset: 0x000A399D
		internal bool Match(string host, int port, string authenticationType)
		{
			return host != null && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, host, StringComparison.OrdinalIgnoreCase) == 0 && port == this.Port;
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000A57D8 File Offset: 0x000A39D8
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.Host.ToUpperInvariant().GetHashCode() + this.Port.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000A5830 File Offset: 0x000A3A30
		public override bool Equals(object comparand)
		{
			CredentialHostKey credentialHostKey = comparand as CredentialHostKey;
			return comparand != null && (string.Compare(this.AuthenticationType, credentialHostKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, credentialHostKey.Host, StringComparison.OrdinalIgnoreCase) == 0) && this.Port == credentialHostKey.Port;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000A5884 File Offset: 0x000A3A84
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Host.Length.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				this.Host,
				":",
				this.Port.ToString(NumberFormatInfo.InvariantInfo),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001A8C RID: 6796
		internal string Host;

		// Token: 0x04001A8D RID: 6797
		internal string AuthenticationType;

		// Token: 0x04001A8E RID: 6798
		internal int Port;

		// Token: 0x04001A8F RID: 6799
		private int m_HashCode;

		// Token: 0x04001A90 RID: 6800
		private bool m_ComputedHashCode;
	}
}
