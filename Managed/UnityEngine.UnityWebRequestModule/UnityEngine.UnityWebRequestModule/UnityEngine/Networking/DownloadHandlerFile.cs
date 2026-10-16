using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine.Bindings;

namespace UnityEngine.Networking
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandlerVFS.h")]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DownloadHandlerFile : DownloadHandler
	{
		// Token: 0x06000066 RID: 102 RVA: 0x00003988 File Offset: 0x00001B88
		[NativeThrows]
		private unsafe static IntPtr Create([Unmarshalled] DownloadHandlerFile obj, string path, bool append)
		{
			IntPtr intPtr;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				intPtr = DownloadHandlerFile.Create_Injected(obj, ref managedSpanWrapper, append);
			}
			finally
			{
				char* ptr = null;
			}
			return intPtr;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000039E0 File Offset: 0x00001BE0
		private void InternalCreateVFS(string path, bool append)
		{
			string directoryName = Path.GetDirectoryName(path);
			bool flag = !Directory.Exists(directoryName);
			if (flag)
			{
				Directory.CreateDirectory(directoryName);
			}
			this.m_Ptr = DownloadHandlerFile.Create(this, path, append);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003A17 File Offset: 0x00001C17
		public DownloadHandlerFile(string path)
		{
			this.InternalCreateVFS(path, false);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A2A File Offset: 0x00001C2A
		public DownloadHandlerFile(string path, bool append)
		{
			this.InternalCreateVFS(path, append);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003A3D File Offset: 0x00001C3D
		protected override NativeArray<byte> GetNativeData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003A3D File Offset: 0x00001C3D
		protected override byte[] GetData()
		{
			throw new NotSupportedException("Raw data access is not supported");
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003A4A File Offset: 0x00001C4A
		protected override string GetText()
		{
			throw new NotSupportedException("String access is not supported");
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003A58 File Offset: 0x00001C58
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003A7C File Offset: 0x00001C7C
		public bool removeFileOnAbort
		{
			get
			{
				IntPtr intPtr = DownloadHandlerFile.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return DownloadHandlerFile.get_removeFileOnAbort_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = DownloadHandlerFile.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				DownloadHandlerFile.set_removeFileOnAbort_Injected(intPtr, value);
			}
		}

		// Token: 0x0600006F RID: 111
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(DownloadHandlerFile obj, ref ManagedSpanWrapper path, bool append);

		// Token: 0x06000070 RID: 112
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_removeFileOnAbort_Injected(IntPtr _unity_self);

		// Token: 0x06000071 RID: 113
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_removeFileOnAbort_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0200000E RID: 14
		internal new static class BindingsMarshaller
		{
			// Token: 0x06000072 RID: 114 RVA: 0x0000389D File Offset: 0x00001A9D
			public static IntPtr ConvertToNative(DownloadHandlerFile handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
