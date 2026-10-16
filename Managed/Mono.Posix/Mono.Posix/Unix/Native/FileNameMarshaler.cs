using System;
using System.Runtime.InteropServices;

namespace Mono.Unix.Native
{
	// Token: 0x02000025 RID: 37
	internal class FileNameMarshaler : ICustomMarshaler
	{
		// Token: 0x06000202 RID: 514 RVA: 0x000079D5 File Offset: 0x00005BD5
		public static ICustomMarshaler GetInstance(string s)
		{
			return FileNameMarshaler.Instance;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000079DC File Offset: 0x00005BDC
		public void CleanUpManagedData(object o)
		{
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000079DE File Offset: 0x00005BDE
		public void CleanUpNativeData(IntPtr pNativeData)
		{
			UnixMarshal.FreeHeap(pNativeData);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000079E6 File Offset: 0x00005BE6
		public int GetNativeDataSize()
		{
			return IntPtr.Size;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000079F0 File Offset: 0x00005BF0
		public IntPtr MarshalManagedToNative(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				return IntPtr.Zero;
			}
			return UnixMarshal.StringToHeap(text, UnixEncoding.Instance);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007A18 File Offset: 0x00005C18
		public object MarshalNativeToManaged(IntPtr pNativeData)
		{
			return UnixMarshal.PtrToString(pNativeData, UnixEncoding.Instance);
		}

		// Token: 0x04000099 RID: 153
		private static FileNameMarshaler Instance = new FileNameMarshaler();
	}
}
