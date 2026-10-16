using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Mono.Btls
{
	// Token: 0x02000109 RID: 265
	internal class MonoBtlsX509Revoked : MonoBtlsObject
	{
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00010FCE File Offset: 0x0000F1CE
		internal new MonoBtlsX509Revoked.BoringX509RevokedHandle Handle
		{
			get
			{
				return (MonoBtlsX509Revoked.BoringX509RevokedHandle)base.Handle;
			}
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Revoked(MonoBtlsX509Revoked.BoringX509RevokedHandle handle)
			: base(handle)
		{
		}

		// Token: 0x0600060C RID: 1548
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_serial_number(IntPtr handle, IntPtr data, int size);

		// Token: 0x0600060D RID: 1549
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_revoked_get_revocation_date(IntPtr handle);

		// Token: 0x0600060E RID: 1550
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_reason(IntPtr handle);

		// Token: 0x0600060F RID: 1551
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_revoked_get_sequence(IntPtr handle);

		// Token: 0x06000610 RID: 1552
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_revoked_free(IntPtr handle);

		// Token: 0x06000611 RID: 1553 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public byte[] GetSerialNumber()
		{
			int num = 256;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			byte[] array2;
			try
			{
				int num2 = MonoBtlsX509Revoked.mono_btls_x509_revoked_get_serial_number(this.Handle.DangerousGetHandle(), intPtr, num);
				base.CheckError(num2 > 0, "GetSerialNumber");
				byte[] array = new byte[num2];
				Marshal.Copy(intPtr, array, 0, num2);
				array2 = array;
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return array2;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00011054 File Offset: 0x0000F254
		public DateTime GetRevocationDate()
		{
			long num = MonoBtlsX509Revoked.mono_btls_x509_revoked_get_revocation_date(this.Handle.DangerousGetHandle());
			return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double)num);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001108C File Offset: 0x0000F28C
		public int GetReason()
		{
			return MonoBtlsX509Revoked.mono_btls_x509_revoked_get_reason(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001109E File Offset: 0x0000F29E
		public int GetSequence()
		{
			return MonoBtlsX509Revoked.mono_btls_x509_revoked_get_sequence(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0200010A RID: 266
		internal class BoringX509RevokedHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000615 RID: 1557 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509RevokedHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000616 RID: 1558 RVA: 0x000110B0 File Offset: 0x0000F2B0
			protected override bool ReleaseHandle()
			{
				if (this.handle != IntPtr.Zero)
				{
					MonoBtlsX509Revoked.mono_btls_x509_revoked_free(this.handle);
				}
				return true;
			}

			// Token: 0x06000617 RID: 1559 RVA: 0x00010229 File Offset: 0x0000E429
			public IntPtr StealHandle()
			{
				return Interlocked.Exchange(ref this.handle, IntPtr.Zero);
			}
		}
	}
}
