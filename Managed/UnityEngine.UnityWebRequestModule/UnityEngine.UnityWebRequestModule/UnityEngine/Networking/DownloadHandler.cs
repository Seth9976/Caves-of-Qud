using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking
{
	// Token: 0x02000007 RID: 7
	[NativeHeader("Modules/UnityWebRequest/Public/DownloadHandler/DownloadHandler.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class DownloadHandler : IDisposable
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000033BC File Offset: 0x000015BC
		[NativeMethod(IsThreadSafe = true)]
		private void ReleaseFromScripting()
		{
			IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			DownloadHandler.ReleaseFromScripting_Injected(intPtr);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000033DE File Offset: 0x000015DE
		[VisibleToOtherModules]
		internal DownloadHandler()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000033E8 File Offset: 0x000015E8
		~DownloadHandler()
		{
			this.Dispose();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003418 File Offset: 0x00001618
		public virtual void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.ReleaseFromScripting();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00003450 File Offset: 0x00001650
		public bool isDone
		{
			get
			{
				return this.IsDone();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003468 File Offset: 0x00001668
		private bool IsDone()
		{
			IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return DownloadHandler.IsDone_Injected(intPtr);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x0000348C File Offset: 0x0000168C
		public string error
		{
			get
			{
				return this.GetErrorMsg();
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034A4 File Offset: 0x000016A4
		private string GetErrorMsg()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				DownloadHandler.GetErrorMsg_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000034E4 File Offset: 0x000016E4
		public NativeArray<byte>.ReadOnly nativeData
		{
			get
			{
				return this.GetNativeData().AsReadOnly();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00003504 File Offset: 0x00001704
		public byte[] data
		{
			get
			{
				return this.GetData();
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000351C File Offset: 0x0000171C
		public string text
		{
			get
			{
				return this.GetText();
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003534 File Offset: 0x00001734
		protected virtual NativeArray<byte> GetNativeData()
		{
			return default(NativeArray<byte>);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003550 File Offset: 0x00001750
		protected virtual byte[] GetData()
		{
			return DownloadHandler.InternalGetByteArray(this);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003568 File Offset: 0x00001768
		protected unsafe virtual string GetText()
		{
			NativeArray<byte> nativeData = this.GetNativeData();
			bool flag = nativeData.IsCreated && nativeData.Length > 0;
			string text;
			if (flag)
			{
				text = new string((sbyte*)nativeData.GetUnsafeReadOnlyPtr<byte>(), 0, nativeData.Length, this.GetTextEncoder());
			}
			else
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000035C0 File Offset: 0x000017C0
		private Encoding GetTextEncoder()
		{
			string contentType = this.GetContentType();
			bool flag = !string.IsNullOrEmpty(contentType);
			if (flag)
			{
				int num = contentType.IndexOf("charset", StringComparison.OrdinalIgnoreCase);
				bool flag2 = num > -1;
				if (flag2)
				{
					int num2 = contentType.IndexOf('=', num);
					bool flag3 = num2 > -1;
					if (flag3)
					{
						string text = contentType.Substring(num2 + 1).Trim().Trim(new char[] { '\'', '"' })
							.Trim();
						int num3 = text.IndexOf(';');
						bool flag4 = num3 > -1;
						if (flag4)
						{
							text = text.Substring(0, num3);
						}
						try
						{
							return Encoding.GetEncoding(text);
						}
						catch (ArgumentException ex)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex.Message));
						}
						catch (NotSupportedException ex2)
						{
							Debug.LogWarning(string.Format("Unsupported encoding '{0}': {1}", text, ex2.Message));
						}
					}
				}
			}
			return Encoding.UTF8;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036DC File Offset: 0x000018DC
		private string GetContentType()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = DownloadHandler.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				DownloadHandler.GetContentType_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000371C File Offset: 0x0000191C
		[RequiredByNativeCode]
		protected virtual bool ReceiveData(byte[] data, int dataLength)
		{
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000372F File Offset: 0x0000192F
		[RequiredByNativeCode]
		protected virtual void ReceiveContentLengthHeader(ulong contentLength)
		{
			this.ReceiveContentLength((int)contentLength);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000373B File Offset: 0x0000193B
		[Obsolete("Use ReceiveContentLengthHeader")]
		protected virtual void ReceiveContentLength(int contentLength)
		{
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000373B File Offset: 0x0000193B
		[RequiredByNativeCode]
		protected virtual void CompleteContent()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003740 File Offset: 0x00001940
		[RequiredByNativeCode]
		protected virtual float GetProgress()
		{
			return 0f;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003758 File Offset: 0x00001958
		protected static T GetCheckedDownloader<T>(UnityWebRequest www) where T : DownloadHandler
		{
			bool flag = www == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get content from a null UnityWebRequest object");
			}
			bool flag2 = !www.isDone;
			if (flag2)
			{
				throw new InvalidOperationException("Cannot get content from an unfinished UnityWebRequest object");
			}
			bool flag3 = www.result == UnityWebRequest.Result.ProtocolError;
			if (flag3)
			{
				throw new InvalidOperationException(www.error);
			}
			return (T)((object)www.downloadHandler);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037BC File Offset: 0x000019BC
		[VisibleToOtherModules]
		[NativeThrows]
		internal unsafe static byte* InternalGetByteArray(DownloadHandler dh, out int length)
		{
			return DownloadHandler.InternalGetByteArray_Injected((dh == null) ? ((IntPtr)0) : DownloadHandler.BindingsMarshaller.ConvertToNative(dh), out length);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000037E0 File Offset: 0x000019E0
		internal static byte[] InternalGetByteArray(DownloadHandler dh)
		{
			NativeArray<byte> nativeData = dh.GetNativeData();
			bool isCreated = nativeData.IsCreated;
			byte[] array;
			if (isCreated)
			{
				array = nativeData.ToArray();
			}
			else
			{
				array = null;
			}
			return array;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003810 File Offset: 0x00001A10
		[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestAudioModule", "UnityEngine.UnityWebRequestTextureModule" })]
		internal unsafe static NativeArray<byte> InternalGetNativeArray(DownloadHandler dh, ref NativeArray<byte> nativeArray)
		{
			int num;
			byte* ptr = DownloadHandler.InternalGetByteArray(dh, out num);
			bool isCreated = nativeArray.IsCreated;
			if (isCreated)
			{
				bool flag = nativeArray.Length == num;
				if (flag)
				{
					return nativeArray;
				}
				DownloadHandler.DisposeNativeArray(ref nativeArray);
			}
			DownloadHandler.CreateNativeArrayForNativeData(ref nativeArray, ptr, num);
			return nativeArray;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003868 File Offset: 0x00001A68
		[VisibleToOtherModules(new string[] { "UnityEngine.UnityWebRequestAudioModule", "UnityEngine.UnityWebRequestTextureModule" })]
		internal static void DisposeNativeArray(ref NativeArray<byte> data)
		{
			bool flag = !data.IsCreated;
			if (!flag)
			{
				data = default(NativeArray<byte>);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000388C File Offset: 0x00001A8C
		internal unsafe static void CreateNativeArrayForNativeData(ref NativeArray<byte> data, byte* bytes, int length)
		{
			data = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)bytes, length, Allocator.Persistent);
		}

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReleaseFromScripting_Injected(IntPtr _unity_self);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDone_Injected(IntPtr _unity_self);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetErrorMsg_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetContentType_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern byte* InternalGetByteArray_Injected(IntPtr dh, out int length);

		// Token: 0x0400001A RID: 26
		[VisibleToOtherModules]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x02000008 RID: 8
		internal static class BindingsMarshaller
		{
			// Token: 0x06000057 RID: 87 RVA: 0x0000389D File Offset: 0x00001A9D
			public static IntPtr ConvertToNative(DownloadHandler handler)
			{
				return handler.m_Ptr;
			}
		}
	}
}
