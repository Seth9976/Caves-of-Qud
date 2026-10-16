using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200001D RID: 29
	[NativeHeader("Modules/UnityWebRequest/Public/UploadHandler/UploadHandlerFile.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class UploadHandlerFile : UploadHandler
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00005E24 File Offset: 0x00004024
		[NativeThrows]
		private unsafe static IntPtr Create([Unmarshalled] UploadHandlerFile self, string filePath)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = UploadHandlerFile.Create_Injected(self, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005E7C File Offset: 0x0000407C
		public UploadHandlerFile(string filePath)
		{
			this.m_Ptr = UploadHandlerFile.Create(this, filePath);
		}

		// Token: 0x06000160 RID: 352
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(UploadHandlerFile self, ref ManagedSpanWrapper filePath);

		// Token: 0x0200001E RID: 30
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000161 RID: 353 RVA: 0x00005CA2 File Offset: 0x00003EA2
			public static IntPtr ConvertToNative(UploadHandlerFile uploadHandler)
			{
				return uploadHandler.m_Ptr;
			}
		}
	}
}
