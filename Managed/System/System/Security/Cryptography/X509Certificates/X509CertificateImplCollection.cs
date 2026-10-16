using System;
using System.Collections.Generic;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002DB RID: 731
	internal class X509CertificateImplCollection : IDisposable
	{
		// Token: 0x060016EF RID: 5871 RVA: 0x0005B10D File Offset: 0x0005930D
		public X509CertificateImplCollection()
		{
			this.list = new List<X509CertificateImpl>();
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x0005B120 File Offset: 0x00059320
		private X509CertificateImplCollection(X509CertificateImplCollection other)
		{
			this.list = new List<X509CertificateImpl>();
			foreach (X509CertificateImpl x509CertificateImpl in other.list)
			{
				this.list.Add(x509CertificateImpl.Clone());
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x060016F1 RID: 5873 RVA: 0x0005B190 File Offset: 0x00059390
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x17000465 RID: 1125
		public X509CertificateImpl this[int index]
		{
			get
			{
				return this.list[index];
			}
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x0005B1AB File Offset: 0x000593AB
		public void Add(X509CertificateImpl impl, bool takeOwnership)
		{
			if (!takeOwnership)
			{
				impl = impl.Clone();
			}
			this.list.Add(impl);
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x0005B1C4 File Offset: 0x000593C4
		public X509CertificateImplCollection Clone()
		{
			return new X509CertificateImplCollection(this);
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x0005B1CC File Offset: 0x000593CC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x0005B1DC File Offset: 0x000593DC
		protected virtual void Dispose(bool disposing)
		{
			foreach (X509CertificateImpl x509CertificateImpl in this.list)
			{
				try
				{
					x509CertificateImpl.Dispose();
				}
				catch
				{
				}
			}
			this.list.Clear();
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x0005B24C File Offset: 0x0005944C
		~X509CertificateImplCollection()
		{
			this.Dispose(false);
		}

		// Token: 0x04000D23 RID: 3363
		private List<X509CertificateImpl> list;
	}
}
