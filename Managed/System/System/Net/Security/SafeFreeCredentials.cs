using System;
using System.Runtime.InteropServices;

namespace System.Net.Security
{
	// Token: 0x02000849 RID: 2121
	internal abstract class SafeFreeCredentials : SafeHandle
	{
		// Token: 0x0600437F RID: 17279 RVA: 0x000EB747 File Offset: 0x000E9947
		protected SafeFreeCredentials()
			: base(IntPtr.Zero, true)
		{
			this._handle = default(global::Interop.SspiCli.CredHandle);
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06004380 RID: 17280 RVA: 0x000EB761 File Offset: 0x000E9961
		public override bool IsInvalid
		{
			get
			{
				return base.IsClosed || this._handle.IsZero;
			}
		}

		// Token: 0x06004381 RID: 17281 RVA: 0x000EB778 File Offset: 0x000E9978
		public static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref global::Interop.SspiCli.SEC_WINNT_AUTH_IDENTITY_W authdata, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, authdata, "AcquireCredentialsHandle");
			}
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004382 RID: 17282 RVA: 0x000EB7D4 File Offset: 0x000E99D4
		public static int AcquireDefaultCredential(string package, global::Interop.SspiCli.CredentialUse intent, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, "AcquireDefaultCredential");
			}
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, IntPtr.Zero, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004383 RID: 17283 RVA: 0x000EB828 File Offset: 0x000E9A28
		public static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref SafeSspiAuthDataHandle authdata, out SafeFreeCredentials outCredential)
		{
			outCredential = new SafeFreeCredential_SECURITY();
			long num2;
			int num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, authdata, null, null, ref outCredential._handle, out num2);
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x06004384 RID: 17284 RVA: 0x000EB860 File Offset: 0x000E9A60
		public unsafe static int AcquireCredentialsHandle(string package, global::Interop.SspiCli.CredentialUse intent, ref global::Interop.SspiCli.SCHANNEL_CRED authdata, out SafeFreeCredentials outCredential)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(null, package, intent, authdata, "AcquireCredentialsHandle");
			}
			int num = -1;
			IntPtr paCred = authdata.paCred;
			try
			{
				IntPtr intPtr = new IntPtr((void*)(&paCred));
				if (paCred != IntPtr.Zero)
				{
					authdata.paCred = intPtr;
				}
				outCredential = new SafeFreeCredential_SECURITY();
				long num2;
				num = global::Interop.SspiCli.AcquireCredentialsHandleW(null, package, (int)intent, null, ref authdata, null, null, ref outCredential._handle, out num2);
			}
			finally
			{
				authdata.paCred = paCred;
			}
			if (num != 0)
			{
				outCredential.SetHandleAsInvalid();
			}
			return num;
		}

		// Token: 0x040028EF RID: 10479
		internal global::Interop.SspiCli.CredHandle _handle;
	}
}
