using System;
using System.Collections;

namespace System.Net.Configuration
{
	// Token: 0x02000765 RID: 1893
	internal class ConnectionManagementData
	{
		// Token: 0x06003BAE RID: 15278 RVA: 0x000CC680 File Offset: 0x000CA880
		public ConnectionManagementData(object parent)
		{
			this.data = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			if (parent != null && parent is ConnectionManagementData)
			{
				ConnectionManagementData connectionManagementData = (ConnectionManagementData)parent;
				foreach (object obj in connectionManagementData.data.Keys)
				{
					string text = (string)obj;
					this.data[text] = connectionManagementData.data[text];
				}
			}
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x000CC71C File Offset: 0x000CA91C
		public void Add(string address, string nconns)
		{
			if (nconns == null || nconns == "")
			{
				nconns = "2";
			}
			this.data[address] = uint.Parse(nconns);
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000CC74C File Offset: 0x000CA94C
		public void Add(string address, int nconns)
		{
			this.data[address] = (uint)nconns;
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x000CC760 File Offset: 0x000CA960
		public void Remove(string address)
		{
			this.data.Remove(address);
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x000CC76E File Offset: 0x000CA96E
		public void Clear()
		{
			this.data.Clear();
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x000CC77C File Offset: 0x000CA97C
		public uint GetMaxConnections(string hostOrIP)
		{
			object obj = this.data[hostOrIP];
			if (obj == null)
			{
				obj = this.data["*"];
			}
			if (obj == null)
			{
				return 2U;
			}
			return (uint)obj;
		}

		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000CC7B5 File Offset: 0x000CA9B5
		public Hashtable Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x040023A5 RID: 9125
		private Hashtable data;

		// Token: 0x040023A6 RID: 9126
		private const int defaultMaxConnections = 2;
	}
}
