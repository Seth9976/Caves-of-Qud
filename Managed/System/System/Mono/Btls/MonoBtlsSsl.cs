using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x020000E8 RID: 232
	internal class MonoBtlsSsl : MonoBtlsObject
	{
		// Token: 0x060004DF RID: 1247
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_destroy(IntPtr handle);

		// Token: 0x060004E0 RID: 1248
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_new(IntPtr handle);

		// Token: 0x060004E1 RID: 1249
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060004E2 RID: 1250
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_use_private_key(IntPtr handle, IntPtr key);

		// Token: 0x060004E3 RID: 1251
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_add_chain_certificate(IntPtr handle, IntPtr x509);

		// Token: 0x060004E4 RID: 1252
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_accept(IntPtr handle);

		// Token: 0x060004E5 RID: 1253
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_connect(IntPtr handle);

		// Token: 0x060004E6 RID: 1254
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_handshake(IntPtr handle);

		// Token: 0x060004E7 RID: 1255
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_close(IntPtr handle);

		// Token: 0x060004E8 RID: 1256
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_shutdown(IntPtr handle);

		// Token: 0x060004E9 RID: 1257
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_quiet_shutdown(IntPtr handle, int mode);

		// Token: 0x060004EA RID: 1258
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_bio(IntPtr handle, IntPtr bio);

		// Token: 0x060004EB RID: 1259
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_read(IntPtr handle, IntPtr data, int len);

		// Token: 0x060004EC RID: 1260
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_write(IntPtr handle, IntPtr data, int len);

		// Token: 0x060004ED RID: 1261
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_error(IntPtr handle, int ret_code);

		// Token: 0x060004EE RID: 1262
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_version(IntPtr handle);

		// Token: 0x060004EF RID: 1263
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_min_version(IntPtr handle, int version);

		// Token: 0x060004F0 RID: 1264
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_max_version(IntPtr handle, int version);

		// Token: 0x060004F1 RID: 1265
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_cipher(IntPtr handle);

		// Token: 0x060004F2 RID: 1266
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_get_ciphers(IntPtr handle, out IntPtr data);

		// Token: 0x060004F3 RID: 1267
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_peer_certificate(IntPtr handle);

		// Token: 0x060004F4 RID: 1268
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_cipher_list(IntPtr handle, IntPtr str);

		// Token: 0x060004F5 RID: 1269
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_print_errors_cb(IntPtr func, IntPtr ctx);

		// Token: 0x060004F6 RID: 1270
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_verify_param(IntPtr handle, IntPtr param);

		// Token: 0x060004F7 RID: 1271
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_set_server_name(IntPtr handle, IntPtr name);

		// Token: 0x060004F8 RID: 1272
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_ssl_get_server_name(IntPtr handle);

		// Token: 0x060004F9 RID: 1273
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_ssl_set_renegotiate_mode(IntPtr handle, int mode);

		// Token: 0x060004FA RID: 1274
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_ssl_renegotiate_pending(IntPtr handle);

		// Token: 0x060004FB RID: 1275 RVA: 0x0000E950 File Offset: 0x0000CB50
		private static MonoBtlsSsl.BoringSslHandle Create_internal(MonoBtlsSslCtx ctx)
		{
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_new(ctx.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				throw new MonoBtlsException();
			}
			return new MonoBtlsSsl.BoringSslHandle(intPtr);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000E97A File Offset: 0x0000CB7A
		public MonoBtlsSsl(MonoBtlsSslCtx ctx)
			: base(MonoBtlsSsl.Create_internal(ctx))
		{
			this.printErrorsFunc = new MonoBtlsSsl.PrintErrorsCallbackFunc(MonoBtlsSsl.PrintErrorsCallback);
			this.printErrorsFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsSsl.PrintErrorsCallbackFunc>(this.printErrorsFunc);
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000E9AB File Offset: 0x0000CBAB
		internal new MonoBtlsSsl.BoringSslHandle Handle
		{
			get
			{
				return (MonoBtlsSsl.BoringSslHandle)base.Handle;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		public void SetBio(MonoBtlsBio bio)
		{
			base.CheckThrow();
			this.bio = bio;
			MonoBtlsSsl.mono_btls_ssl_set_bio(this.Handle.DangerousGetHandle(), bio.Handle.DangerousGetHandle());
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		private Exception ThrowError([CallerMemberName] string callerName = null)
		{
			string text;
			try
			{
				if (callerName == null)
				{
					callerName = base.GetType().Name;
				}
				text = this.GetErrors();
			}
			catch
			{
				text = null;
			}
			if (text != null)
			{
				throw new MonoBtlsException("{0} failed: {1}.", new object[] { callerName, text });
			}
			throw new MonoBtlsException("{0} failed.", new object[] { callerName });
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0000EA50 File Offset: 0x0000CC50
		private MonoBtlsSslError GetError(int ret_code)
		{
			base.CheckThrow();
			this.bio.CheckLastError("GetError");
			return (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), ret_code);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0000EA79 File Offset: 0x0000CC79
		public void SetCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetCertificate");
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000EAAB File Offset: 0x0000CCAB
		public void SetPrivateKey(MonoBtlsKey key)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_use_private_key(this.Handle.DangerousGetHandle(), key.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("SetPrivateKey");
			}
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0000EADD File Offset: 0x0000CCDD
		public void AddIntermediateCertificate(MonoBtlsX509 x509)
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_add_chain_certificate(this.Handle.DangerousGetHandle(), x509.Handle.DangerousGetHandle()) <= 0)
			{
				throw this.ThrowError("AddIntermediateCertificate");
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000EB10 File Offset: 0x0000CD10
		public MonoBtlsSslError Accept()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_accept(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		public MonoBtlsSslError Connect()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_connect(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000EB68 File Offset: 0x0000CD68
		public MonoBtlsSslError Handshake()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_handshake(this.Handle.DangerousGetHandle());
			return this.GetError(num);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000EB94 File Offset: 0x0000CD94
		[MonoPInvokeCallback(typeof(MonoBtlsSsl.PrintErrorsCallbackFunc))]
		private static int PrintErrorsCallback(IntPtr str, IntPtr len, IntPtr ctx)
		{
			StringBuilder stringBuilder = (StringBuilder)GCHandle.FromIntPtr(ctx).Target;
			int num;
			try
			{
				string text = Marshal.PtrToStringAnsi(str, (int)len);
				stringBuilder.Append(text);
				num = 1;
			}
			catch
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		public string GetErrors()
		{
			StringBuilder stringBuilder = new StringBuilder();
			GCHandle gchandle = GCHandle.Alloc(stringBuilder);
			string text;
			try
			{
				MonoBtlsSsl.mono_btls_ssl_print_errors_cb(this.printErrorsFuncPtr, GCHandle.ToIntPtr(gchandle));
				text = stringBuilder.ToString();
			}
			finally
			{
				if (gchandle.IsAllocated)
				{
					gchandle.Free();
				}
			}
			return text;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000EC3C File Offset: 0x0000CE3C
		public void PrintErrors()
		{
			string errors = this.GetErrors();
			if (string.IsNullOrEmpty(errors))
			{
				return;
			}
			Console.Error.WriteLine(errors);
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000EC64 File Offset: 0x0000CE64
		public MonoBtlsSslError Read(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_read(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num > 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError error = this.GetError(num);
			if (num == 0 && error == MonoBtlsSslError.Syscall)
			{
				dataSize = 0;
				return MonoBtlsSslError.None;
			}
			dataSize = 0;
			return error;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public MonoBtlsSslError Write(IntPtr data, ref int dataSize)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_write(this.Handle.DangerousGetHandle(), data, dataSize);
			if (num >= 0)
			{
				dataSize = num;
				return MonoBtlsSslError.None;
			}
			MonoBtlsSslError monoBtlsSslError = (MonoBtlsSslError)MonoBtlsSsl.mono_btls_ssl_get_error(this.Handle.DangerousGetHandle(), num);
			dataSize = 0;
			return monoBtlsSslError;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0000ECF0 File Offset: 0x0000CEF0
		public int GetVersion()
		{
			base.CheckThrow();
			return MonoBtlsSsl.mono_btls_ssl_get_version(this.Handle.DangerousGetHandle());
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0000ED08 File Offset: 0x0000CF08
		public void SetMinVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_min_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000ED21 File Offset: 0x0000CF21
		public void SetMaxVersion(int version)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_max_version(this.Handle.DangerousGetHandle(), version);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public int GetCipher()
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_get_cipher(this.Handle.DangerousGetHandle());
			base.CheckError(num > 0, "GetCipher");
			return num;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0000ED70 File Offset: 0x0000CF70
		public short[] GetCiphers()
		{
			base.CheckThrow();
			IntPtr intPtr;
			int num = MonoBtlsSsl.mono_btls_ssl_get_ciphers(this.Handle.DangerousGetHandle(), out intPtr);
			base.CheckError(num > 0, "GetCiphers");
			short[] array2;
			try
			{
				short[] array = new short[num];
				Marshal.Copy(intPtr, array, 0, num);
				array2 = array;
			}
			finally
			{
				base.FreeDataPtr(intPtr);
			}
			return array2;
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000EDD4 File Offset: 0x0000CFD4
		public void SetCipherList(string str)
		{
			base.CheckThrow();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(str);
				int num = MonoBtlsSsl.mono_btls_ssl_set_cipher_list(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetCipherList");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0000EE38 File Offset: 0x0000D038
		public MonoBtlsX509 GetPeerCertificate()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_peer_certificate(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return new MonoBtlsX509(new MonoBtlsX509.BoringX509Handle(intPtr));
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0000EE78 File Offset: 0x0000D078
		public void SetVerifyParam(MonoBtlsX509VerifyParam param)
		{
			base.CheckThrow();
			int num = MonoBtlsSsl.mono_btls_ssl_set_verify_param(this.Handle.DangerousGetHandle(), param.Handle.DangerousGetHandle());
			base.CheckError(num, "SetVerifyParam");
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		public void SetServerName(string name)
		{
			base.CheckThrow();
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalAnsi(name);
				int num = MonoBtlsSsl.mono_btls_ssl_set_server_name(this.Handle.DangerousGetHandle(), intPtr);
				base.CheckError(num, "SetServerName");
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0000EF18 File Offset: 0x0000D118
		public string GetServerName()
		{
			base.CheckThrow();
			IntPtr intPtr = MonoBtlsSsl.mono_btls_ssl_get_server_name(this.Handle.DangerousGetHandle());
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Marshal.PtrToStringAnsi(intPtr);
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0000EF51 File Offset: 0x0000D151
		public void Shutdown()
		{
			base.CheckThrow();
			if (MonoBtlsSsl.mono_btls_ssl_shutdown(this.Handle.DangerousGetHandle()) < 0)
			{
				throw this.ThrowError("Shutdown");
			}
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0000EF78 File Offset: 0x0000D178
		public void SetQuietShutdown()
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_quiet_shutdown(this.Handle.DangerousGetHandle(), 1);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0000EF91 File Offset: 0x0000D191
		protected override void Close()
		{
			if (!this.Handle.IsInvalid)
			{
				MonoBtlsSsl.mono_btls_ssl_close(this.Handle.DangerousGetHandle());
			}
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public void SetRenegotiateMode(MonoBtlsSslRenegotiateMode mode)
		{
			base.CheckThrow();
			MonoBtlsSsl.mono_btls_ssl_set_renegotiate_mode(this.Handle.DangerousGetHandle(), (int)mode);
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0000EFC9 File Offset: 0x0000D1C9
		public bool RenegotiatePending()
		{
			return MonoBtlsSsl.mono_btls_ssl_renegotiate_pending(this.Handle.DangerousGetHandle()) != 0;
		}

		// Token: 0x040003D5 RID: 981
		private MonoBtlsBio bio;

		// Token: 0x040003D6 RID: 982
		private MonoBtlsSsl.PrintErrorsCallbackFunc printErrorsFunc;

		// Token: 0x040003D7 RID: 983
		private IntPtr printErrorsFuncPtr;

		// Token: 0x020000E9 RID: 233
		internal class BoringSslHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x0600051B RID: 1307 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringSslHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x0600051C RID: 1308 RVA: 0x0000EFDE File Offset: 0x0000D1DE
			protected override bool ReleaseHandle()
			{
				MonoBtlsSsl.mono_btls_ssl_destroy(this.handle);
				this.handle = IntPtr.Zero;
				return true;
			}
		}

		// Token: 0x020000EA RID: 234
		// (Invoke) Token: 0x0600051E RID: 1310
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int PrintErrorsCallbackFunc(IntPtr str, IntPtr len, IntPtr ctx);
	}
}
