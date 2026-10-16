using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerScript.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandlerScript : DownloadHandler
	{
		// Token: 0x0600005F RID: 95
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create([Unmarshalled] DownloadHandlerScript obj);

		// Token: 0x06000060 RID: 96
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreatePreallocated([Unmarshalled] DownloadHandlerScript obj, [Unmarshalled] byte[] preallocatedBuffer);

		// Token: 0x06000061 RID: 97 RVA: 0x00003919 File Offset: 0x00001B19
		private void InternalCreateScript()
		{
			this.m_Ptr = DownloadHandlerScript.Create(this);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003928 File Offset: 0x00001B28
		private void InternalCreateScript(byte[] preallocatedBuffer)
		{
			this.m_Ptr = DownloadHandlerScript.CreatePreallocated(this, preallocatedBuffer);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003938 File Offset: 0x00001B38
		public DownloadHandlerScript()
		{
			this.InternalCreateScript();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000394C File Offset: 0x00001B4C
		public DownloadHandlerScript(byte[] preallocatedBuffer)
		{
			bool flag = preallocatedBuffer == null || preallocatedBuffer.Length < 1;
			if (flag)
			{
				throw new ArgumentException("Cannot create a preallocated-buffer DownloadHandlerScript backed by a null or zero-length array");
			}
			this.InternalCreateScript(preallocatedBuffer);
		}

		// Token: 0x0200000C RID: 12
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000065 RID: 101 RVA: 0x0000389D File Offset: 0x00001A9D
			public static IntPtr ConvertToNative(DownloadHandlerScript handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
