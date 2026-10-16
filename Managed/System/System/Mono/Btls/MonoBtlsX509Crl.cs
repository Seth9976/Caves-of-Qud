using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x020000F8 RID: 248
	internal class MonoBtlsX509Crl : MonoBtlsObject
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x000103A9 File Offset: 0x0000E5A9
		internal new MonoBtlsX509Crl.BoringX509CrlHandle Handle
		{
			get
			{
				return (MonoBtlsX509Crl.BoringX509CrlHandle)base.Handle;
			}
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Crl(MonoBtlsX509Crl.BoringX509CrlHandle handle)
			: base(handle)
		{
		}

		// Token: 0x060005A5 RID: 1445
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_crl_ref(IntPtr handle);

		// Token: 0x060005A6 RID: 1446
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_crl_from_data(IntPtr data, int len, MonoBtlsX509Format format);

		// Token: 0x060005A7 RID: 1447
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_crl_get_by_cert(IntPtr handle, IntPtr x509);

		// Token: 0x060005A8 RID: 1448
		[DllImport("libmono-btls-shared")]
		private unsafe static extern IntPtr mono_btls_x509_crl_get_by_serial(IntPtr handle, void* serial, int len);

		// Token: 0x060005A9 RID: 1449
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_crl_get_revoked_count(IntPtr handle);

		// Token: 0x060005AA RID: 1450
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_crl_get_revoked(IntPtr handle, int index);

		// Token: 0x060005AB RID: 1451
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_crl_get_last_update(IntPtr handle);

		// Token: 0x060005AC RID: 1452
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_crl_get_next_update(IntPtr handle);

		// Token: 0x060005AD RID: 1453
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_crl_get_version(IntPtr handle);

		// Token: 0x060005AE RID: 1454
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_crl_get_issuer(IntPtr handle);

		// Token: 0x060005AF RID: 1455
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_crl_free(IntPtr handle);

		// Token: 0x060005B0 RID: 1456 RVA: 0x000103B8 File Offset: 0x0000E5B8
		public static MonoBtlsX509Crl LoadFromData(byte[] buffer, MonoBtlsX509Format format)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(buffer.Length);
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			MonoBtlsX509Crl monoBtlsX509Crl;
			try
			{
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
				IntPtr intPtr2 = MonoBtlsX509Crl.mono_btls_x509_crl_from_data(intPtr, buffer.Length, format);
				if (intPtr2 == IntPtr.Zero)
				{
					throw new MonoBtlsException("Failed to read CRL from data.");
				}
				monoBtlsX509Crl = new MonoBtlsX509Crl(new MonoBtlsX509Crl.BoringX509CrlHandle(intPtr2));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return monoBtlsX509Crl;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00010434 File Offset: 0x0000E634
		public MonoBtlsX509Revoked GetByCert(MonoBtlsX509 x509)
		{
			IntPtr intPtr = MonoBtlsX509Crl.mono_btls_x509_crl_get_by_cert(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509Revoked(new MonoBtlsX509Revoked.BoringX509RevokedHandle(intPtr));
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x00010478 File Offset: 0x0000E678
		public unsafe MonoBtlsX509Revoked GetBySerial(byte[] serial)
		{
			void* ptr;
			if (serial == null || serial.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&serial[0]);
			}
			IntPtr intPtr = MonoBtlsX509Crl.mono_btls_x509_crl_get_by_serial(this.Handle.DangerousGetHandle(), ptr, serial.Length);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509Revoked(new MonoBtlsX509Revoked.BoringX509RevokedHandle(intPtr));
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x000104CC File Offset: 0x0000E6CC
		public int GetRevokedCount()
		{
			return MonoBtlsX509Crl.mono_btls_x509_crl_get_revoked_count(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000104E0 File Offset: 0x0000E6E0
		public MonoBtlsX509Revoked GetRevoked(int index)
		{
			if (index >= this.GetRevokedCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr = MonoBtlsX509Crl.mono_btls_x509_crl_get_revoked(this.Handle.DangerousGetHandle(), index);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509Revoked(new MonoBtlsX509Revoked.BoringX509RevokedHandle(intPtr));
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x00010528 File Offset: 0x0000E728
		public DateTime GetLastUpdate()
		{
			long num = MonoBtlsX509Crl.mono_btls_x509_crl_get_last_update(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00010560 File Offset: 0x0000E760
		public DateTime GetNextUpdate()
		{
			long num = MonoBtlsX509Crl.mono_btls_x509_crl_get_next_update(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00010598 File Offset: 0x0000E798
		public long GetVersion()
		{
			return MonoBtlsX509Crl.mono_btls_x509_crl_get_version(this.Handle.DangerousGetHandle());
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000105AC File Offset: 0x0000E7AC
		public MonoBtlsX509Name GetIssuerName()
		{
			IntPtr intPtr = MonoBtlsX509Crl.mono_btls_x509_crl_get_issuer(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "GetIssuerName");
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x020000F9 RID: 249
		internal class BoringX509CrlHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060005B9 RID: 1465 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509CrlHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060005BA RID: 1466 RVA: 0x000105EC File Offset: 0x0000E7EC
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsX509Crl.mono_btls_x509_crl_free(this.handle);
				}
				return true;
			}

			// Token: 0x060005BB RID: 1467 RVA: 0x00010229 File Offset: 0x0000E429
			public IntPtr StealHandle()
			{
				return Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			}
		}
	}
}
