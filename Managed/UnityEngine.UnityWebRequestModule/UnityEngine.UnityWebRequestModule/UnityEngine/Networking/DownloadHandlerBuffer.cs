using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerBuffer.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerBuffer : DownloadHandler
	{
		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create([Unmarshalled] DownloadHandlerBuffer obj);

		// Token: 0x06000059 RID: 89 RVA: 0x000038A5 File Offset: 0x00001AA5
		private void InternalCreateBuffer()
		{
			this.m_Ptr = DownloadHandlerBuffer.Create(this);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000038B4 File Offset: 0x00001AB4
		public DownloadHandlerBuffer()
		{
			this.InternalCreateBuffer();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000038C8 File Offset: 0x00001AC8
		protected override NativeArray<byte> GetNativeData()
		{
			return DownloadHandler.InternalGetNativeArray(this, ref this.m_NativeData);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000038E6 File Offset: 0x00001AE6
		public override void Dispose()
		{
			DownloadHandler.DisposeNativeArray(ref this.m_NativeData);
			base.Dispose();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000038FC File Offset: 0x00001AFC
		public static string GetContent(UnityWebRequest www)
		{
			return DownloadHandler.GetCheckedDownloader<DownloadHandlerBuffer>(www).text;
		}

		// Token: 0x0400001B RID: 27
		private NativeArray<byte> m_NativeData;

		// Token: 0x0200000A RID: 10
		internal new static class BindingsMarshaller
		{
			// Token: 0x0600005E RID: 94 RVA: 0x0000389D File Offset: 0x00001A9D
			public static IntPtr ConvertToNative(DownloadHandlerBuffer handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
