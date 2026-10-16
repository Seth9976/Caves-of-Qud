using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000087 RID: 135
	public class GetBindDNResponse : LdapExtendedResponse
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0001278E File Offset: 0x0001098E
		public virtual string Identity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00012798 File Offset: 0x00010998
		public GetBindDNResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode == 0)
			{
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
				Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(value);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.identity = asn1OctetString.stringValue();
				if (this.identity == null)
				{
					throw new IOException("Decoding error");
				}
			}
			else
			{
				this.identity = "";
			}
		}

		// Token: 0x04000255 RID: 597
		private string identity;
	}
}
