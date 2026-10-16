using System;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000AD RID: 173
	public class DSETimeStamp
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x000145B9 File Offset: 0x000127B9
		public int Seconds
		{
			get
			{
				return this.nSeconds;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x000145C1 File Offset: 0x000127C1
		public int ReplicaNumber
		{
			get
			{
				return this.replica_number;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x000145C9 File Offset: 0x000127C9
		public int Event
		{
			get
			{
				return this.nEvent;
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000145D4 File Offset: 0x000127D4
		public DSETimeStamp(Asn1Sequence dseObject)
		{
			this.nSeconds = ((Asn1Integer)dseObject.get_Renamed(0)).intValue();
			this.replica_number = ((Asn1Integer)dseObject.get_Renamed(1)).intValue();
			this.nEvent = ((Asn1Integer)dseObject.get_Renamed(2)).intValue();
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001462C File Offset: 0x0001282C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[TimeStamp (seconds={0})", this.nSeconds);
			stringBuilder.AppendFormat("(replicaNumber={0})", this.replica_number);
			stringBuilder.AppendFormat("(event={0})", this.nEvent);
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04000322 RID: 802
		protected int nSeconds;

		// Token: 0x04000323 RID: 803
		protected int replica_number;

		// Token: 0x04000324 RID: 804
		protected int nEvent;
	}
}
