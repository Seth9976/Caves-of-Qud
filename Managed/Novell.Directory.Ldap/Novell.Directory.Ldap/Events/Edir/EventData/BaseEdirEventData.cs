using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir.EventData
{
	// Token: 0x020000BA RID: 186
	public class BaseEdirEventData
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00014CBE File Offset: 0x00012EBE
		public EdirEventDataType EventDataType
		{
			get
			{
				return this.event_data_type;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00014CC8 File Offset: 0x00012EC8
		public BaseEdirEventData(EdirEventDataType eventDataType, Asn1Object message)
		{
			this.event_data_type = eventDataType;
			byte[] array = SupportClass.ToByteArray(((Asn1OctetString)message).byteValue());
			this.decodedData = new MemoryStream(array);
			this.decoder = new LBERDecoder();
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00014D0A File Offset: 0x00012F0A
		protected void DataInitDone()
		{
			this.decodedData = null;
			this.decoder = null;
		}

		// Token: 0x0400042D RID: 1069
		protected MemoryStream decodedData;

		// Token: 0x0400042E RID: 1070
		protected LBERDecoder decoder;

		// Token: 0x0400042F RID: 1071
		protected EdirEventDataType event_data_type;
	}
}
