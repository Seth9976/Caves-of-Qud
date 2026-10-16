using System;
using System.Runtime.InteropServices;
using Mono.Util;

namespace Mono.Btls
{
	// Token: 0x02000100 RID: 256
	internal abstract class MonoBtlsX509LookupMono : MonoBtlsObject
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x000109AA File Offset: 0x0000EBAA
		internal new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle Handle
		{
			get
			{
				return (MonoBtlsX509LookupMono.BoringX509LookupMonoHandle)base.Handle;
			}
		}

		// Token: 0x060005DC RID: 1500
		[DllImport("libmono-btls-shared")]
		private static extern IntPtr mono_btls_x509_lookup_mono_new();

		// Token: 0x060005DD RID: 1501
		[DllImport("libmono-btls-shared")]
		private static extern void mono_btls_x509_lookup_mono_init(IntPtr handle, IntPtr instance, IntPtr by_subject_func);

		// Token: 0x060005DE RID: 1502
		[DllImport("libmono-btls-shared")]
		private static extern int mono_btls_x509_lookup_mono_free(IntPtr handle);

		// Token: 0x060005DF RID: 1503 RVA: 0x000109B8 File Offset: 0x0000EBB8
		internal MonoBtlsX509LookupMono()
			: base(new MonoBtlsX509LookupMono.BoringX509LookupMonoHandle(MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_new()))
		{
			this.gch = GCHandle.Alloc(this);
			this.instance = GCHandle.ToIntPtr(this.gch);
			this.bySubjectFunc = new MonoBtlsX509LookupMono.BySubjectFunc(MonoBtlsX509LookupMono.OnGetBySubject);
			this.bySubjectFuncPtr = Marshal.GetFunctionPointerForDelegate<MonoBtlsX509LookupMono.BySubjectFunc>(this.bySubjectFunc);
			MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_init(this.Handle.DangerousGetHandle(), this.instance, this.bySubjectFuncPtr);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00010A31 File Offset: 0x0000EC31
		internal void Install(MonoBtlsX509Lookup lookup)
		{
			if (this.lookup != null)
			{
				throw new InvalidOperationException();
			}
			this.lookup = lookup;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00010A48 File Offset: 0x0000EC48
		protected void AddCertificate(MonoBtlsX509 certificate)
		{
			this.lookup.AddCertificate(certificate);
		}

		// Token: 0x060005E2 RID: 1506
		protected abstract MonoBtlsX509 OnGetBySubject(MonoBtlsX509Name name);

		// Token: 0x060005E3 RID: 1507 RVA: 0x00010A58 File Offset: 0x0000EC58
		[MonoPInvokeCallback(typeof(MonoBtlsX509LookupMono.BySubjectFunc))]
		private static int OnGetBySubject(IntPtr instance, IntPtr name_ptr, out IntPtr x509_ptr)
		{
			int num;
			try
			{
				MonoBtlsX509Name.BoringX509NameHandle boringX509NameHandle = null;
				try
				{
					MonoBtlsX509LookupMono monoBtlsX509LookupMono = (MonoBtlsX509LookupMono)GCHandle.FromIntPtr(instance).Target;
					boringX509NameHandle = new MonoBtlsX509Name.BoringX509NameHandle(name_ptr, false);
					MonoBtlsX509Name monoBtlsX509Name = new MonoBtlsX509Name(boringX509NameHandle);
					MonoBtlsX509 monoBtlsX = monoBtlsX509LookupMono.OnGetBySubject(monoBtlsX509Name);
					if (monoBtlsX != null)
					{
						x509_ptr = monoBtlsX.Handle.StealHandle();
						num = 1;
					}
					else
					{
						x509_ptr = IntPtr.Zero;
						num = 0;
					}
				}
				finally
				{
					if (boringX509NameHandle != null)
					{
						boringX509NameHandle.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("LOOKUP METHOD - GET BY SUBJECT EX: {0}", ex);
				x509_ptr = IntPtr.Zero;
				num = 0;
			}
			return num;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010AF4 File Offset: 0x0000ECF4
		protected override void Close()
		{
			try
			{
				if (this.gch.IsAllocated)
				{
					this.gch.Free();
				}
			}
			finally
			{
				this.instance = IntPtr.Zero;
				this.bySubjectFunc = null;
				this.bySubjectFuncPtr = IntPtr.Zero;
				base.Close();
			}
		}

		// Token: 0x04000445 RID: 1093
		private GCHandle gch;

		// Token: 0x04000446 RID: 1094
		private IntPtr instance;

		// Token: 0x04000447 RID: 1095
		private MonoBtlsX509LookupMono.BySubjectFunc bySubjectFunc;

		// Token: 0x04000448 RID: 1096
		private IntPtr bySubjectFuncPtr;

		// Token: 0x04000449 RID: 1097
		private MonoBtlsX509Lookup lookup;

		// Token: 0x02000101 RID: 257
		internal class BoringX509LookupMonoHandle : MonoBtlsObject.MonoBtlsHandle
		{
			// Token: 0x060005E5 RID: 1509 RVA: 0x0000CD48 File Offset: 0x0000AF48
			public BoringX509LookupMonoHandle(IntPtr handle)
				: base(handle, true)
			{
			}

			// Token: 0x060005E6 RID: 1510 RVA: 0x00010B50 File Offset: 0x0000ED50
			protected override bool ReleaseHandle()
			{
				MonoBtlsX509LookupMono.mono_btls_x509_lookup_mono_free(this.handle);
				return true;
			}
		}

		// Token: 0x02000102 RID: 258
		// (Invoke) Token: 0x060005E8 RID: 1512
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int BySubjectFunc(IntPtr instance, IntPtr name, out IntPtr x509_ptr);
	}
}
