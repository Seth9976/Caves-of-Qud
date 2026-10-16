using System;
using System.Collections.Generic;
using System.Net;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002C3 RID: 707
	public sealed class SubjectAlternativeNameBuilder
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x00057FED File Offset: 0x000561ED
		public void AddEmailAddress(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentOutOfRangeException("emailAddress", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeEmailAddress(emailAddress));
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0005801E File Offset: 0x0005621E
		public void AddDnsName(string dnsName)
		{
			if (string.IsNullOrEmpty(dnsName))
			{
				throw new ArgumentOutOfRangeException("dnsName", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeDnsName(dnsName));
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0005804F File Offset: 0x0005624F
		public void AddUri(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUri(uri));
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0005807C File Offset: 0x0005627C
		public void AddIpAddress(IPAddress ipAddress)
		{
			if (ipAddress == null)
			{
				throw new ArgumentNullException("ipAddress");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeIpAddress(ipAddress));
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x000580A3 File Offset: 0x000562A3
		public void AddUserPrincipalName(string upn)
		{
			if (string.IsNullOrEmpty(upn))
			{
				throw new ArgumentOutOfRangeException("upn", "String cannot be empty or null.");
			}
			this._encodedTlvs.Add(this._generalNameEncoder.EncodeUserPrincipalName(upn));
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x000580D4 File Offset: 0x000562D4
		public X509Extension Build(bool critical = false)
		{
			return new X509Extension("2.5.29.17", DerEncoder.ConstructSequence(this._encodedTlvs), critical);
		}

		// Token: 0x04000C8C RID: 3212
		private readonly List<byte[][]> _encodedTlvs = new List<byte[][]>();

		// Token: 0x04000C8D RID: 3213
		private readonly GeneralNameEncoder _generalNameEncoder = new GeneralNameEncoder();
	}
}
