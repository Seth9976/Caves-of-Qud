using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000012 RID: 18
	[UsedByNativeCode]
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
	[NativeHeader("UnityWebRequestScriptingClasses.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class UnityWebRequestAsyncOperation : AsyncOperation
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003DBC File Offset: 0x00001FBC
		public UnityWebRequestAsyncOperation()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003DC6 File Offset: 0x00001FC6
		private UnityWebRequestAsyncOperation(IntPtr ptr)
			: base(ptr)
		{
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003DD1 File Offset: 0x00001FD1
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00003DD9 File Offset: 0x00001FD9
		public UnityWebRequest webRequest { get; internal set; }

		// Token: 0x02000013 RID: 19
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000091 RID: 145 RVA: 0x00003DE2 File Offset: 0x00001FE2
			public static UnityWebRequestAsyncOperation ConvertToManaged(IntPtr ptr)
			{
				return new UnityWebRequestAsyncOperation(ptr);
			}
		}
	}
}
