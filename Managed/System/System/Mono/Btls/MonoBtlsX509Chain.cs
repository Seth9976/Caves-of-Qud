using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x020000F6 RID: 246
	internal class MonoBtlsX509Chain : MonoBtlsObject
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0001023B File Offset: 0x0000E43B
		internal new MonoBtlsX509Chain.BoringX509ChainHandle Handle
		{
			get
			{
				return (MonoBtlsX509Chain.BoringX509ChainHandle)base.Handle;
			}
		}

		// Token: 0x06000594 RID: 1428
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_new();

		// Token: 0x06000595 RID: 1429
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_get_count(IntPtr handle);

		// Token: 0x06000596 RID: 1430
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_get_cert(IntPtr Handle, int index);

		// Token: 0x06000597 RID: 1431
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_chain_add_cert(IntPtr chain, IntPtr x509);

		// Token: 0x06000598 RID: 1432
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_chain_up_ref(IntPtr handle);

		// Token: 0x06000599 RID: 1433
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_chain_free(IntPtr handle);

		// Token: 0x0600059A RID: 1434 RVA: 0x00010248 File Offset: 0x0000E448
		public MonoBtlsX509Chain()
			: base(new MonoBtlsX509Chain.BoringX509ChainHandle(MonoBtlsX509Chain.mono_btls_x509_chain_new()))
		{
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Chain(MonoBtlsX509Chain.BoringX509ChainHandle handle)
			: base(handle)
		{
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0001025A File Offset: 0x0000E45A
		public int Count
		{
			get
			{
				return MonoBtlsX509Chain.mono_btls_x509_chain_get_count(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001026C File Offset: 0x0000E46C
		public MonoBtlsX509 GetCertificate(int index)
		{
			if (index >= this.Count)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_get_cert(this.Handle.DangerousGetHandle(), index);
			base.CheckError(intPtr != IntPtr.Zero, "GetCertificate");
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x000102BC File Offset: 0x0000E4BC
		public void Dump()
		{
			Console.Error.WriteLine("CHAIN: {0:x} {1}", this.Handle, this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				using (MonoBtlsX509 certificate = this.GetCertificate(i))
				{
					Console.Error.WriteLine("  CERT #{0}: {1}", i, certificate.GetSubjectNameString());
				}
			}
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0001033C File Offset: 0x0000E53C
		public void AddCertificate(MonoBtlsX509 x509)
		{
			MonoBtlsX509Chain.mono_btls_x509_chain_add_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0001035C File Offset: 0x0000E55C
		internal MonoBtlsX509Chain Copy()
		{
			IntPtr intPtr = MonoBtlsX509Chain.mono_btls_x509_chain_up_ref(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509Chain(new MonoBtlsX509Chain.BoringX509ChainHandle(intPtr));
		}

		// Token: 0x020000F7 RID: 247
		internal class BoringX509ChainHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060005A1 RID: 1441 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509ChainHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060005A2 RID: 1442 RVA: 0x0001039B File Offset: 0x0000E59B
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509Chain.mono_btls_x509_chain_free(this.handle);
				return true;
			}
		}
	}
}
