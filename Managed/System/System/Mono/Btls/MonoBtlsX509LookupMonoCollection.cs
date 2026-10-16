using System;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x02000103 RID: 259
	internal class MonoBtlsX509LookupMonoCollection : MonoBtlsX509LookupMono
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x00010B5F File Offset: 0x0000ED5F
		internal MonoBtlsX509LookupMonoCollection(X509CertificateCollection collection, MonoBtlsX509TrustKind trust)
		{
			this.collection = collection;
			this.trust = trust;
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00010B78 File Offset: 0x0000ED78
		private void Initialize()
		{
			if (this.certificates != null)
			{
				return;
			}
			this.hashes = new long[this.collection.Count];
			this.certificates = new MonoBtlsX509[this.collection.Count];
			for (int i = 0; i < this.collection.Count; i++)
			{
				byte[] rawCertData = this.collection[i].GetRawCertData();
				this.certificates[i] = MonoBtlsX509.LoadFromData(rawCertData, MonoBtlsX509Format.DER);
				this.certificates[i].AddExplicitTrust(this.trust);
				this.hashes[i] = this.certificates[i].GetSubjectNameHash();
			}
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00010C1C File Offset: 0x0000EE1C
		protected override MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name)
		{
			this.Initialize();
			long hash = name.GetHash();
			MonoBtlsX509 monoBtlsX = null;
			for (int i = 0; i < this.certificates.Length; i++)
			{
				if (this.hashes[i] == hash)
				{
					monoBtlsX = this.certificates[i];
					base.AddCertificate(monoBtlsX);
				}
			}
			return monoBtlsX;
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00010C68 File Offset: 0x0000EE68
		protected override void Close()
		{
			try
			{
				if (this.certificates != null)
				{
					for (int i = 0; i < this.certificates.Length; i++)
					{
						if (this.certificates[i] != null)
						{
							this.certificates[i].Dispose();
							this.certificates[i] = null;
						}
					}
					this.certificates = null;
					this.hashes = null;
				}
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x0400044A RID: 1098
		private long[] hashes;

		// Token: 0x0400044B RID: 1099
		private MonoBtlsX509[] certificates;

		// Token: 0x0400044C RID: 1100
		private X509CertificateCollection collection;

		// Token: 0x0400044D RID: 1101
		private MonoBtlsX509TrustKind trust;
	}
}
