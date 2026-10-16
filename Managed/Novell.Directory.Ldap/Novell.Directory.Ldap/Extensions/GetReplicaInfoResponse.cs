using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008B RID: 139
	public class GetReplicaInfoResponse : LdapExtendedResponse
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00012A40 File Offset: 0x00010C40
		public GetReplicaInfoResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.partitionID = 0;
				this.replicaState = 0;
				this.modificationTime = 0;
				this.purgeTime = 0;
				this.localPartitionID = 0;
				this.partitionDN = "";
				this.replicaType = 0;
				this.flags = 0;
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
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(value));
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionID = asn1Integer.intValue();
			Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer2 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaState = asn1Integer2.intValue();
			Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer3 == null)
			{
				throw new IOException("Decoding error");
			}
			this.modificationTime = asn1Integer3.intValue();
			Asn1Integer asn1Integer4 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer4 == null)
			{
				throw new IOException("Decoding error");
			}
			this.purgeTime = asn1Integer4.intValue();
			Asn1Integer asn1Integer5 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer5 == null)
			{
				throw new IOException("Decoding error");
			}
			this.localPartitionID = asn1Integer5.intValue();
			Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(memoryStream);
			if (asn1OctetString == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionDN = asn1OctetString.stringValue();
			if (this.partitionDN == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer6 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer6 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaType = asn1Integer6.intValue();
			Asn1Integer asn1Integer7 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer7 == null)
			{
				throw new IOException("Decoding error");
			}
			this.flags = asn1Integer7.intValue();
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012C2C File Offset: 0x00010E2C
		public virtual int getpartitionID()
		{
			return this.partitionID;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00012C34 File Offset: 0x00010E34
		public virtual int getreplicaState()
		{
			return this.replicaState;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00012C3C File Offset: 0x00010E3C
		public virtual int getmodificationTime()
		{
			return this.modificationTime;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012C44 File Offset: 0x00010E44
		public virtual int getpurgeTime()
		{
			return this.purgeTime;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00012C4C File Offset: 0x00010E4C
		public virtual int getlocalPartitionID()
		{
			return this.localPartitionID;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012C54 File Offset: 0x00010E54
		public virtual string getpartitionDN()
		{
			return this.partitionDN;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012C5C File Offset: 0x00010E5C
		public virtual int getreplicaType()
		{
			return this.replicaType;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00012C64 File Offset: 0x00010E64
		public virtual int getflags()
		{
			return this.flags;
		}

		// Token: 0x04000257 RID: 599
		private int partitionID;

		// Token: 0x04000258 RID: 600
		private int replicaState;

		// Token: 0x04000259 RID: 601
		private int modificationTime;

		// Token: 0x0400025A RID: 602
		private int purgeTime;

		// Token: 0x0400025B RID: 603
		private int localPartitionID;

		// Token: 0x0400025C RID: 604
		private string partitionDN;

		// Token: 0x0400025D RID: 605
		private int replicaType;

		// Token: 0x0400025E RID: 606
		private int flags;
	}
}
