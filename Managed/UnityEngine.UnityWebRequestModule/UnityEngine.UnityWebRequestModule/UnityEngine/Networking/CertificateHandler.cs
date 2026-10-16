using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Modules/UnityWebRequest/Public/CertificateHandler/CertificateHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class CertificateHandler : IDisposable
	{
		// Token: 0x0600002E RID: 46
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create([Unmarshalled] CertificateHandler obj);

		// Token: 0x0600002F RID: 47 RVA: 0x000032E4 File Offset: 0x000014E4
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = CertificateHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			CertificateHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00003306 File Offset: 0x00001506
		protected CertificateHandler()
		{
			this.m_Ptr = CertificateHandler.Create(this);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000331C File Offset: 0x0000151C
		~CertificateHandler()
		{
			this.Dispose();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000334C File Offset: 0x0000154C
		protected virtual bool ValidateCertificate(byte[] certificateData)
		{
			return false;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003360 File Offset: 0x00001560
		[RequiredByNativeCode]
		internal bool ValidateCertificateNative(byte[] certificateData)
		{
			return this.ValidateCertificate(certificateData);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000337C File Offset: 0x0000157C
		public void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x04000019 RID: 25
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000006 RID: 6
		internal static class BindingsMarshaller
		{
			// Token: 0x06000036 RID: 54 RVA: 0x000033B2 File Offset: 0x000015B2
			public static IntPtr ConvertToNative(CertificateHandler handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
