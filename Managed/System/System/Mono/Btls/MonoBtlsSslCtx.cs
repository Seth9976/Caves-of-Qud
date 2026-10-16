using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000EB RID: 235
	internal class MonoBtlsSslCtx : MonoBtlsObject
	{
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000EFF7 File Offset: 0x0000D1F7
		internal new MonoBtlsSslCtx.BoringSslCtxHandle Handle
		{
			get
			{
				return (MonoBtlsSslCtx.BoringSslCtxHandle)base.Handle;
			}
		}

		// Token: 0x06000522 RID: 1314
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_ctx_new();

		// Token: 0x06000523 RID: 1315
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_free(IntPtr handle);

		// Token: 0x06000524 RID: 1316
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_ctx_up_ref(IntPtr handle);

		// Token: 0x06000525 RID: 1317
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_initialize(IntPtr handle, IntPtr instance);

		// Token: 0x06000526 RID: 1318
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_debug_bio(IntPtr handle, IntPtr bio);

		// Token: 0x06000527 RID: 1319
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_cert_verify_callback(IntPtr handle, IntPtr func, int cert_required);

		// Token: 0x06000528 RID: 1320
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_cert_select_callback(IntPtr handle, IntPtr func);

		// Token: 0x06000529 RID: 1321
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_min_version(IntPtr handle, int version);

		// Token: 0x0600052A RID: 1322
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_max_version(IntPtr handle, int version);

		// Token: 0x0600052B RID: 1323
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_is_cipher_supported(IntPtr handle, short value);

		// Token: 0x0600052C RID: 1324
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_ciphers(IntPtr handle, int count, IntPtr data, int allow_unsupported);

		// Token: 0x0600052D RID: 1325
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_verify_param(IntPtr handle, IntPtr param);

		// Token: 0x0600052E RID: 1326
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_ctx_set_client_ca_list(IntPtr handle, int count, IntPtr sizes, IntPtr data);

		// Token: 0x0600052F RID: 1327
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_ctx_set_server_name_callback(IntPtr handle, IntPtr func);

		// Token: 0x06000530 RID: 1328 RVA: 0x0000F004 File Offset: 0x0000D204
		public MonoBtlsSslCtx()
			: this(new MonoBtlsSslCtx.BoringSslCtxHandle(MonoBtlsSslCtx.mono_btls_ssl_ctx_new()))
		{
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0000F018 File Offset: 0x0000D218
		internal MonoBtlsSslCtx(MonoBtlsSslCtx.BoringSslCtxHandle handle)
			: base(handle)
		{
			this.instance = GCHandle.Alloc(this);
			this.instancePtr = GCHandle.ToIntPtr(this.instance);
			MonoBtlsSslCtx.mono_btls_ssl_ctx_initialize(handle.DangerousGetHandle(), this.instancePtr);
			this.verifyFunc = new MonoBtlsSslCtx.NativeVerifyFunc(MonoBtlsSslCtx.NativeVerifyCallback);
			this.selectFunc = new MonoBtlsSslCtx.NativeSelectFunc(MonoBtlsSslCtx.NativeSelectCallback);
			this.serverNameFunc = new MonoBtlsSslCtx.NativeServerNameFunc(MonoBtlsSslCtx.NativeServerNameCallback);
			this.verifyFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeVerifyFunc>(this.verifyFunc);
			this.selectFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeSelectFunc>(this.selectFunc);
			this.serverNameFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSslCtx.NativeServerNameFunc>(this.serverNameFunc);
			this.store = new MonoBtlsX509Store(this.Handle);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000F0D4 File Offset: 0x0000D2D4
		internal MonoBtlsSslCtx Copy()
		{
			return new MonoBtlsSslCtx(new MonoBtlsSslCtx.BoringSslCtxHandle(MonoBtlsSslCtx.mono_btls_ssl_ctx_up_ref(this.Handle.DangerousGetHandle())));
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		public MonoBtlsX509Store CertificateStore
		{
			get
			{
				return this.store;
			}
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		private int VerifyCallback(bool preverify_ok, MonoBtlsX509StoreCtx ctx)
		{
			if (this.verifyCallback != null)
			{
				return this.verifyCallback(ctx);
			}
			return 0;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0000F110 File Offset: 0x0000D310
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeVerifyFunc))]
		private static int NativeVerifyCallback(IntPtr instance, int preverify_ok, IntPtr store_ctx)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			using (MonoBtlsX509StoreCtx monoBtlsX509StoreCtx = new MonoBtlsX509StoreCtx(preverify_ok, store_ctx))
			{
				try
				{
					return monoBtlsSslCtx.VerifyCallback(preverify_ok != 0, monoBtlsX509StoreCtx);
				}
				catch (Exception ex)
				{
					monoBtlsSslCtx.SetException(ex);
				}
			}
			return 0;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0000F180 File Offset: 0x0000D380
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeSelectFunc))]
		private static int NativeSelectCallback(IntPtr instance, int count, IntPtr sizes, IntPtr data)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				string[] array = MonoBtlsSslCtx.CopyIssuers(count, sizes, data);
				if (monoBtlsSslCtx.selectCallback != null)
				{
					num = monoBtlsSslCtx.selectCallback(array);
				}
				else
				{
					num = 1;
				}
			}
			catch (Exception ex)
			{
				monoBtlsSslCtx.SetException(ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		private static string[] CopyIssuers(int count, IntPtr sizesPtr, IntPtr dataPtr)
		{
			if (count == 0 || sizesPtr == IntPtr.Zero || dataPtr == IntPtr.Zero)
			{
				return null;
			}
			int[] array = new int[count];
			Marshal.Copy(sizesPtr, array, 0, count);
			IntPtr[] array2 = new IntPtr[count];
			Marshal.Copy(dataPtr, array2, 0, count);
			string[] array3 = new string[count];
			for (int i = 0; i < count; i++)
			{
				byte[] array4 = new byte[array[i]];
				Marshal.Copy(array2[i], array4, 0, array4.Length);
				using (MonoBtlsX509Name monoBtlsX509Name = MonoBtlsX509Name.CreateFromData(array4, false))
				{
					array3[i] = MonoBtlsUtils.FormatName(monoBtlsX509Name, true, ", ", true);
				}
			}
			return array3;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0000F29C File Offset: 0x0000D49C
		public void SetDebugBio(MonoBtlsBio bio)
		{
			base.CheckThrow();
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_debug_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000F2BF File Offset: 0x0000D4BF
		public void SetVerifyCallback(MonoBtlsVerifyCallback callback, bool client_cert_required)
		{
			base.CheckThrow();
			this.verifyCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_cert_verify_callback(this.Handle.DangerousGetHandle(), this.verifyFuncPtr, client_cert_required ? 1 : 0);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000F2EB File Offset: 0x0000D4EB
		public void SetSelectCallback(MonoBtlsSelectCallback callback)
		{
			base.CheckThrow();
			this.selectCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_cert_select_callback(this.Handle.DangerousGetHandle(), this.selectFuncPtr);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000F310 File Offset: 0x0000D510
		public void SetMinVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_min_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000F329 File Offset: 0x0000D529
		public void SetMaxVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_max_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000F342 File Offset: 0x0000D542
		public bool IsCipherSupported(short value)
		{
			base.CheckThrow();
			return MonoBtlsSslCtx.mono_btls_ssl_ctx_is_cipher_supported(this.Handle.DangerousGetHandle(), value) != 0;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0000F360 File Offset: 0x0000D560
		public void SetCiphers(short[] ciphers, bool allow_unsupported)
		{
			base.CheckThrow();
			IntPtr intPtr = Marshal.AllocHGlobal(ciphers.Length * 2);
			try
			{
				Marshal.Copy(ciphers, 0, intPtr, ciphers.Length);
				int num = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_ciphers(this.Handle.DangerousGetHandle(), ciphers.Length, intPtr, allow_unsupported ? 1 : 0);
				base.CheckError(num > 0, "SetCiphers");
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			base.CheckThrow();
			int num = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_verify_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(num, "SetVerifyParam");
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0000F40C File Offset: 0x0000D60C
		public void SetClientCertificateIssuers(string[] acceptableIssuers)
		{
			base.CheckThrow();
			if (acceptableIssuers == null || acceptableIssuers.Length == 0)
			{
				return;
			}
			int num = acceptableIssuers.Length;
			new byte[num][];
			int[] array = new int[num];
			IntPtr[] array2 = new IntPtr[num];
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				for (int i = 0; i < num; i++)
				{
					byte[] rawData = new X500DistinguishedName(acceptableIssuers[i]).RawData;
					array[i] = rawData.Length;
					array2[i] = Marshal.AllocHGlobal(rawData.Length);
					Marshal.Copy(rawData, 0, array2[i], rawData.Length);
				}
				intPtr = Marshal.AllocHGlobal(num * 4);
				Marshal.Copy(array, 0, intPtr, num);
				intPtr2 = Marshal.AllocHGlobal(num * 8);
				Marshal.Copy(array2, 0, intPtr2, num);
				int num2 = MonoBtlsSslCtx.mono_btls_ssl_ctx_set_client_ca_list(this.Handle.DangerousGetHandle(), num, intPtr, intPtr2);
				base.CheckError(num2, "SetClientCertificateIssuers");
			}
			finally
			{
				for (int j = 0; j < num; j++)
				{
					if (array2[j] != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(array2[j]);
					}
				}
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0000F540 File Offset: 0x0000D740
		public void SetServerNameCallback(MonoBtlsServerNameCallback callback)
		{
			base.CheckThrow();
			this.serverNameCallback = callback;
			MonoBtlsSslCtx.mono_btls_ssl_ctx_set_server_name_callback(this.Handle.DangerousGetHandle(), this.serverNameFuncPtr);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0000F568 File Offset: 0x0000D768
		[MonoPInvokeCallback(typeof(MonoBtlsSslCtx.NativeServerNameFunc))]
		private static int NativeServerNameCallback(IntPtr instance)
		{
			MonoBtlsSslCtx monoBtlsSslCtx = (MonoBtlsSslCtx)GCHandle.FromIntPtr(instance).Target;
			int num;
			try
			{
				num = monoBtlsSslCtx.serverNameCallback();
			}
			catch (Exception ex)
			{
				monoBtlsSslCtx.SetException(ex);
				num = 0;
			}
			return num;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		protected override void Close()
		{
			if (this.store != null)
			{
				this.store.Dispose();
				this.store = null;
			}
			if (this.instance.IsAllocated)
			{
				this.instance.Free();
			}
			base.Close();
		}

		// Token: 0x040003D8 RID: 984
		private MonoBtlsSslCtx.NativeVerifyFunc verifyFunc;

		// Token: 0x040003D9 RID: 985
		private MonoBtlsSslCtx.NativeSelectFunc selectFunc;

		// Token: 0x040003DA RID: 986
		private MonoBtlsSslCtx.NativeServerNameFunc serverNameFunc;

		// Token: 0x040003DB RID: 987
		private IntPtr verifyFuncPtr;

		// Token: 0x040003DC RID: 988
		private IntPtr selectFuncPtr;

		// Token: 0x040003DD RID: 989
		private IntPtr serverNameFuncPtr;

		// Token: 0x040003DE RID: 990
		private MonoBtlsVerifyCallback verifyCallback;

		// Token: 0x040003DF RID: 991
		private MonoBtlsSelectCallback selectCallback;

		// Token: 0x040003E0 RID: 992
		private MonoBtlsServerNameCallback serverNameCallback;

		// Token: 0x040003E1 RID: 993
		private MonoBtlsX509Store store;

		// Token: 0x040003E2 RID: 994
		private GCHandle instance;

		// Token: 0x040003E3 RID: 995
		private IntPtr instancePtr;

		// Token: 0x020000EC RID: 236
		internal class BoringSslCtxHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x06000544 RID: 1348 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringSslCtxHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x06000545 RID: 1349 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
			protected override bool ReleaseHandle()
			{
				MonoBtlsSslCtx.mono_btls_ssl_ctx_free(this.handle);
				return true;
			}
		}

		// Token: 0x020000ED RID: 237
		// (Invoke) Token: 0x06000547 RID: 1351
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeVerifyFunc(IntPtr instance, int preverify_ok, IntPtr ctx);

		// Token: 0x020000EE RID: 238
		// (Invoke) Token: 0x0600054B RID: 1355
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeSelectFunc(IntPtr instance, int count, IntPtr sizes, IntPtr data);

		// Token: 0x020000EF RID: 239
		// (Invoke) Token: 0x0600054F RID: 1359
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int NativeServerNameFunc(IntPtr instance);
	}
}
