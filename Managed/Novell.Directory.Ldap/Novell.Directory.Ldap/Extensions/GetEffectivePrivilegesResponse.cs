using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000089 RID: 137
	public class GetEffectivePrivilegesResponse : LdapExtendedResponse
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x000128F8 File Offset: 0x00010AF8
		public virtual int Privileges
		{
			get
			{
				return this.privileges;
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00012900 File Offset: 0x00010B00
		public GetEffectivePrivilegesResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.privileges = 0;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(value);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.privileges = asn1Integer.intValue();
		}

		// Token: 0x04000256 RID: 598
		private int privileges;
	}
}
