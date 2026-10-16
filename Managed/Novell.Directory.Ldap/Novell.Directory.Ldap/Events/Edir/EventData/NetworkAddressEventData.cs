using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000C3 RID: 195
	public class NetworkAddressEventData : BaseEdirEventData
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00015DBA File Offset: 0x00013FBA
		public int ValueType
		{
			get
			{
				return this.nType;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00015DC2 File Offset: 0x00013FC2
		public string Data
		{
			get
			{
				return this.strData;
			}
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00015DCC File Offset: 0x00013FCC
		public NetworkAddressEventData(EdirEventDataType eventDataType, Asn1Object message)
			: base(eventDataType, message)
		{
			int[] array = new int[1];
			this.nType = ((Asn1Integer)this.decoder.decode(this.decodedData, array)).intValue();
			this.strData = ((Asn1OctetString)this.decoder.decode(this.decodedData, array)).stringValue();
			base.DataInitDone();
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00015E34 File Offset: 0x00014034
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[NetworkAddress");
			stringBuilder.AppendFormat("(type={0})", this.nType);
			stringBuilder.AppendFormat("(Data={0})", this.strData);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0400045B RID: 1115
		protected int nType;

		// Token: 0x0400045C RID: 1116
		protected string strData;
	}
}
