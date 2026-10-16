using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B9 RID: 185
	public class MonitorEventResponse : LdapExtendedResponse
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00014BF8 File Offset: 0x00012DF8
		public EdirEventSpecifier[] SpecifierList
		{
			get
			{
				return this.specifier_list;
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014C00 File Offset: 0x00012E00
		public MonitorEventResponse(RfcLdapMessage message)
			: base(message)
		{
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, null);
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(value);
			int num = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
			this.specifier_list = new EdirEventSpecifier[num];
			for (int i = 0; i < num; i++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
				int num2 = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
				int num3 = ((Asn1Enumerated)asn1Sequence2.get_Renamed(1)).intValue();
				this.specifier_list[i] = new EdirEventSpecifier((EdirEventType)num2, (EdirEventResultType)num3);
			}
		}

		// Token: 0x0400042C RID: 1068
		protected EdirEventSpecifier[] specifier_list;
	}
}
