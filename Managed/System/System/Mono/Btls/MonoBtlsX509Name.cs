using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Mono.Btls
{
	// Token: 0x02000105 RID: 261
	internal class MonoBtlsX509Name : MonoBtlsObject
	{
		// Token: 0x060005EF RID: 1519
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_print_bio(IntPtr handle, IntPtr bio);

		// Token: 0x060005F0 RID: 1520
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_print_string(IntPtr handle, IntPtr buffer, int size);

		// Token: 0x060005F1 RID: 1521
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_raw_data(IntPtr handle, out IntPtr buffer, int use_canon_enc);

		// Token: 0x060005F2 RID: 1522
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_name_hash(IntPtr handle);

		// Token: 0x060005F3 RID: 1523
		[DllImport("libmono-btls-shared")]
		private static extern long mono_btls_x509_name_hash_old(IntPtr handle);

		// Token: 0x060005F4 RID: 1524
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_count(IntPtr handle);

		// Token: 0x060005F5 RID: 1525
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509NameEntryType mono_btls_x509_name_get_entry_type(IntPtr name, int index);

		// Token: 0x060005F6 RID: 1526
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid(IntPtr name, int index, IntPtr buffer, int size);

		// Token: 0x060005F7 RID: 1527
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_oid_data(IntPtr name, int index, out IntPtr data);

		// Token: 0x060005F8 RID: 1528
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_name_get_entry_value(IntPtr name, int index, out int tag, out IntPtr str);

		// Token: 0x060005F9 RID: 1529
		[DllImport("libmono-btls-shared")]
		private unsafe static extern IntPtr mono_btls_x509_name_from_data(void* data, int len, int use_canon_enc);

		// Token: 0x060005FA RID: 1530
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_name_free(IntPtr handle);

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x00010CD8 File Offset: 0x0000EED8
		internal new MonoBtlsX509Name.BoringX509NameHandle Handle
		{
			get
			{
				return (MonoBtlsX509Name.BoringX509NameHandle)base.Handle;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509Name(MonoBtlsX509Name.BoringX509NameHandle handle)
			: base(handle)
		{
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00010CE8 File Offset: 0x0000EEE8
		public string GetString()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string text;
			try
			{
				int num = MonoBtlsX509Name.mono_btls_x509_name_print_string(this.Handle.DangerousGetHandle(), intPtr, 4096);
				base.CheckError(num, "GetString");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00010D44 File Offset: 0x0000EF44
		public void PrintBio(MonoBtlsBio bio)
		{
			int num = MonoBtlsX509Name.mono_btls_x509_name_print_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
			base.CheckError(num, "PrintBio");
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00010D7C File Offset: 0x0000EF7C
		public byte[] GetRawData(bool use_canon_enc)
		{
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_raw_data(this.Handle.DangerousGetHandle(), out intPtr, use_canon_enc ? 1 : 0);
			base.CheckError(num > 0, "GetRawData");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			base.FreeDataPtr(intPtr);
			return array;
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00010DCA File Offset: 0x0000EFCA
		public long GetHash()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_hash(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public long GetHashOld()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_hash_old(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00010DEE File Offset: 0x0000EFEE
		public int GetEntryCount()
		{
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_count(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00010E00 File Offset: 0x0000F000
		public MonoBtlsX509NameEntryType GetEntryType(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			return MonoBtlsX509Name.mono_btls_x509_name_get_entry_type(this.Handle.DangerousGetHandle(), index);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00010E24 File Offset: 0x0000F024
		public string GetEntryOid(int index)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr = Marshal.AllocHGlobal(4096);
			string text;
			try
			{
				int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid(this.Handle.DangerousGetHandle(), index, intPtr, 4096);
				base.CheckError(num > 0, "GetEntryOid");
				text = Marshal.PtrToStringAnsi(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return text;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00010E94 File Offset: 0x0000F094
		public byte[] GetEntryOidData(int index)
		{
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_oid_data(this.Handle.DangerousGetHandle(), index, out intPtr);
			base.CheckError(num > 0, "GetEntryOidData");
			byte[] array = new byte[num];
			Marshal.Copy(intPtr, array, 0, num);
			return array;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00010ED8 File Offset: 0x0000F0D8
		public unsafe string GetEntryValue(int index, out int tag)
		{
			if (index >= this.GetEntryCount())
			{
				throw new ArgumentOutOfRangeException();
			}
			IntPtr intPtr;
			int num = MonoBtlsX509Name.mono_btls_x509_name_get_entry_value(this.Handle.DangerousGetHandle(), index, out tag, out intPtr);
			if (num <= 0)
			{
				return null;
			}
			string @string;
			try
			{
				@string = new UTF8Encoding().GetString((byte*)(void*)intPtr, num);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					base.FreeDataPtr(intPtr);
				}
			}
			return @string;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00010F4C File Offset: 0x0000F14C
		public unsafe static MonoBtlsX509Name CreateFromData(byte[] data, bool use_canon_enc)
		{
			void* ptr;
			if (data == null || data.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = (void*)(&data[0]);
			}
			IntPtr intPtr = MonoBtlsX509Name.mono_btls_x509_name_from_data(ptr, data.Length, use_canon_enc ? 1 : 0);
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException("mono_btls_x509_name_from_data() failed.");
			}
			return new MonoBtlsX509Name(new MonoBtlsX509Name.BoringX509NameHandle(intPtr, false));
		}

		// Token: 0x02000106 RID: 262
		internal class BoringX509NameHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000608 RID: 1544 RVA: 0x00010FA4 File Offset: 0x0000F1A4
			internal BoringX509NameHandle(IntPtr handle, bool ownsHandle)
				: base(handle, ownsHandle)
			{
				this.dontFree = !ownsHandle;
			}

			// Token: 0x06000609 RID: 1545 RVA: 0x00010FB8 File Offset: 0x0000F1B8
			protected override bool ReleaseHandle()
			{
				if (!this.dontFree)
				{
					MonoBtlsX509Name.mono_btls_x509_name_free(this.handle);
				}
				return true;
			}

			// Token: 0x04000453 RID: 1107
			private bool dontFree;
		}
	}
}
