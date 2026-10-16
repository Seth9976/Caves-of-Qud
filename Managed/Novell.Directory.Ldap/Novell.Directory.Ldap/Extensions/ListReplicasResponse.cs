using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000092 RID: 146
	public class ListReplicasResponse : LdapExtendedResponse
	{
		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000135B8 File Offset: 0x000117B8
		public virtual string[] ReplicaList
		{
			get
			{
				return this.replicaList;
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000135C0 File Offset: 0x000117C0
		public ListReplicasResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.replicaList = new string[0];
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
			Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(value);
			if (asn1Sequence == null)
			{
				throw new IOException("Decoding error");
			}
			int num = asn1Sequence.size();
			this.replicaList = new string[num];
			for (int i = 0; i < num; i++)
			{
				Asn1OctetString asn1OctetString = (Asn1OctetString)asn1Sequence.get_Renamed(i);
				if (asn1OctetString == null)
				{
					throw new IOException("Decoding error");
				}
				this.replicaList[i] = asn1OctetString.stringValue();
				if (this.replicaList[i] == null)
				{
					throw new IOException("Decoding error");
				}
			}
		}

		// Token: 0x04000264 RID: 612
		private string[] replicaList;
	}
}
