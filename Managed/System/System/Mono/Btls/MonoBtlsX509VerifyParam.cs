using System;
using System.Runtime.InteropServices;

namespace Mono.Btls
{
	// Token: 0x02000113 RID: 275
	internal class MonoBtlsX509VerifyParam : MonoBtlsObject
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x00011879 File Offset: 0x0000FA79
		internal new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle Handle
		{
			get
			{
				return (MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle)base.Handle;
			}
		}

		// Token: 0x0600065C RID: 1628
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_new();

		// Token: 0x0600065D RID: 1629
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_copy(IntPtr handle);

		// Token: 0x0600065E RID: 1630
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_lookup(IntPtr name);

		// Token: 0x0600065F RID: 1631
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_can_modify(IntPtr param);

		// Token: 0x06000660 RID: 1632
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_name(IntPtr handle, IntPtr name);

		// Token: 0x06000661 RID: 1633
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x06000662 RID: 1634
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_add_host(IntPtr handle, IntPtr name, int namelen);

		// Token: 0x06000663 RID: 1635
		[DllImport("libmono-btls-shared")]
		private static extern ulong mono_btls_x509_verify_param_get_flags(IntPtr handle);

		// Token: 0x06000664 RID: 1636
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_flags(IntPtr handle, ulong flags);

		// Token: 0x06000665 RID: 1637
		[DllImport("libmono-btls-shared")]
		private static extern MonoBtlsX509VerifyFlags mono_btls_x509_verify_param_get_mono_flags(IntPtr handle);

		// Token: 0x06000666 RID: 1638
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_mono_flags(IntPtr handle, MonoBtlsX509VerifyFlags flags);

		// Token: 0x06000667 RID: 1639
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_purpose(IntPtr handle, MonoBtlsX509Purpose purpose);

		// Token: 0x06000668 RID: 1640
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_get_depth(IntPtr handle);

		// Token: 0x06000669 RID: 1641
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_depth(IntPtr handle, int depth);

		// Token: 0x0600066A RID: 1642
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_verify_param_set_time(IntPtr handle, long time);

		// Token: 0x0600066B RID: 1643
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_verify_param_get_peername(IntPtr handle);

		// Token: 0x0600066C RID: 1644
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_verify_param_free(IntPtr handle);

		// Token: 0x0600066D RID: 1645 RVA: 0x00011886 File Offset: 0x0000FA86
		internal MonoBtlsX509VerifyParam()
			: base(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_new()))
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000CA92 File Offset: 0x0000AC92
		internal MonoBtlsX509VerifyParam(MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle handle)
			: base(handle)
		{
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00011898 File Offset: 0x0000FA98
		public MonoBtlsX509VerifyParam Copy()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_copy(this.Handle.DangerousGetHandle());
			base.CheckError(intPtr != IntPtr.Zero, "Copy");
			return new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr));
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000118D7 File Offset: 0x0000FAD7
		public static MonoBtlsX509VerifyParam GetSslClient()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_client", true);
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x000118E4 File Offset: 0x0000FAE4
		public static MonoBtlsX509VerifyParam GetSslServer()
		{
			return MonoBtlsX509VerifyParam.Lookup("ssl_server", true);
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x000118F4 File Offset: 0x0000FAF4
		public static MonoBtlsX509VerifyParam Lookup(string name, bool fail = false)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			MonoBtlsX509VerifyParam monoBtlsX509VerifyParam;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				intPtr2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_lookup(intPtr);
				if (intPtr2 == IntPtr.Zero)
				{
					if (fail)
					{
						throw new MonoBtlsException("X509_VERIFY_PARAM_lookup() could not find '{0}'.", new object[] { name });
					}
					monoBtlsX509VerifyParam = null;
				}
				else
				{
					monoBtlsX509VerifyParam = new MonoBtlsX509VerifyParam(new MonoBtlsX509VerifyParam.BoringX509VerifyParamHandle(intPtr2));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return monoBtlsX509VerifyParam;
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x00011978 File Offset: 0x0000FB78
		public bool CanModify
		{
			get
			{
				return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_can_modify(this.Handle.DangerousGetHandle()) != 0;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001198D File Offset: 0x0000FB8D
		private void WantToModify()
		{
			if (!this.CanModify)
			{
				throw new MonoBtlsException("Attempting to modify read-only MonoBtlsX509VerifyParam instance.");
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public void SetName(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00011A08 File Offset: 0x0000FC08
		public void SetHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(num, "SetHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00011A74 File Offset: 0x0000FC74
		public void AddHost(string name)
		{
			this.WantToModify();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_add_host(this.Handle.DangerousGetHandle(), intPtr, name.Length);
				base.CheckError(num, "AddHost");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00011AE0 File Offset: 0x0000FCE0
		public ulong GetFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00011AF4 File Offset: 0x0000FCF4
		public void SetFlags(ulong flags)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(num, "SetFlags");
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00011B25 File Offset: 0x0000FD25
		public MonoBtlsX509VerifyFlags GetMonoFlags()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_mono_flags(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00011B38 File Offset: 0x0000FD38
		public void SetMonoFlags(MonoBtlsX509VerifyFlags flags)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_mono_flags(this.Handle.DangerousGetHandle(), flags);
			base.CheckError(num, "SetMonoFlags");
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00011B6C File Offset: 0x0000FD6C
		public void SetPurpose(MonoBtlsX509Purpose purpose)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_purpose(this.Handle.DangerousGetHandle(), purpose);
			base.CheckError(num, "SetPurpose");
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00011B9D File Offset: 0x0000FD9D
		public int GetDepth()
		{
			return MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_depth(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00011BB0 File Offset: 0x0000FDB0
		public void SetDepth(int depth)
		{
			this.WantToModify();
			int num = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_depth(this.Handle.DangerousGetHandle(), depth);
			base.CheckError(num, "SetDepth");
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00011BE4 File Offset: 0x0000FDE4
		public void SetTime(DateTime time)
		{
			this.WantToModify();
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			long num = (long)time.Subtract(dateTime).TotalSeconds;
			int num2 = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_set_time(this.Handle.DangerousGetHandle(), num);
			base.CheckError(num2, "SetTime");
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00011C3C File Offset: 0x0000FE3C
		public string GetPeerName()
		{
			IntPtr intPtr = MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_get_peername(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x02000114 RID: 276
		internal class BoringX509VerifyParamHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509VerifyParamHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x00011C6F File Offset: 0x0000FE6F
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509VerifyParam.mono_btls_x509_verify_param_free(this.handle);
				return true;
			}
		}
	}
}
