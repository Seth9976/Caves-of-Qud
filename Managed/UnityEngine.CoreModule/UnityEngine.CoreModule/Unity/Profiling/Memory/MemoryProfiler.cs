using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Scripting;

namespace Unity.Profiling.Memory
{
	// Token: 0x0200007E RID: 126
	[NativeHeader("Runtime/Profiler/Runtime/MemorySnapshotManager.h")]
	public static class MemoryProfiler
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600024C RID: 588 RVA: 0x00004D6C File Offset: 0x00002F6C
		// (remove) Token: 0x0600024D RID: 589 RVA: 0x00004DA0 File Offset: 0x00002FA0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static event Action<string, bool> m_SnapshotFinished;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600024E RID: 590 RVA: 0x00004DD4 File Offset: 0x00002FD4
		// (remove) Token: 0x0600024F RID: 591 RVA: 0x00004E08 File Offset: 0x00003008
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static event Action<string, bool, DebugScreenCapture> m_SaveScreenshotToDisk;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000250 RID: 592 RVA: 0x00004E3C File Offset: 0x0000303C
		// (remove) Token: 0x06000251 RID: 593 RVA: 0x00004E70 File Offset: 0x00003070
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event Action<MemorySnapshotMetadata> CreatingMetadata;

		// Token: 0x06000252 RID: 594 RVA: 0x00004EA4 File Offset: 0x000030A4
		[NativeMethod("StartOperation")]
		[StaticAccessor("profiling::memory::GetMemorySnapshotManager()", StaticAccessorType.Dot)]
		[NativeConditional("ENABLE_PROFILER")]
		private unsafe static void StartOperation(uint captureFlags, bool requestScreenshot, string path, bool isRemote)
		{
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
				MemoryProfiler.StartOperation_Injected(captureFlags, requestScreenshot, ref managedSpanWrapper, isRemote);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000253 RID: 595
		[StaticAccessor("profiling::memory::GetMemorySnapshotManager()", StaticAccessorType.Dot)]
		[NativeMethod("RequestEditorTakeSnapshotOfPlayer")]
		[NativeConditional("ENABLE_PROFILER")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RequestEditorTakeSnapshotOfPlayer(uint captureFlags, bool requestScreenshot);

		// Token: 0x06000254 RID: 596 RVA: 0x00004EFC File Offset: 0x000030FC
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			MemoryProfiler.TakeSnapshot(path, finishCallback, null, captureFlags);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00004F0C File Offset: 0x0000310C
		public static void TakeSnapshot(string path, Action<string, bool> finishCallback, Action<string, bool, DebugScreenCapture> screenshotCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Debug.LogWarning("Canceling snapshot, there is another snapshot in progress.");
				finishCallback(path, false);
			}
			else
			{
				MemoryProfiler.m_SnapshotFinished += finishCallback;
				MemoryProfiler.m_SaveScreenshotToDisk += screenshotCallback;
				bool flag2 = string.IsNullOrEmpty(path) && PlayerConnection.instance.isConnected;
				if (flag2)
				{
					MemoryProfiler.RequestEditorTakeSnapshotOfPlayer((uint)captureFlags, MemoryProfiler.m_SaveScreenshotToDisk != null);
				}
				else
				{
					MemoryProfiler.StartOperation((uint)captureFlags, MemoryProfiler.m_SaveScreenshotToDisk != null, path, false);
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00004F8C File Offset: 0x0000318C
		public static void TakeTempSnapshot(Action<string, bool> finishCallback, CaptureFlags captureFlags = CaptureFlags.ManagedObjects | CaptureFlags.NativeObjects)
		{
			string[] array = Application.dataPath.Split('/', StringSplitOptions.None);
			string text = array[array.Length - 2];
			string text2 = Application.temporaryCachePath + "/" + text + ".snap";
			MemoryProfiler.TakeSnapshot(text2, finishCallback, captureFlags);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00004FD0 File Offset: 0x000031D0
		[RequiredByNativeCode]
		private unsafe static byte[] PrepareMetadata()
		{
			bool flag = MemoryProfiler.CreatingMetadata == null;
			byte[] array;
			if (flag)
			{
				array = new byte[0];
			}
			else
			{
				MemorySnapshotMetadata memorySnapshotMetadata = new MemorySnapshotMetadata();
				memorySnapshotMetadata.Description = string.Empty;
				MemoryProfiler.CreatingMetadata(memorySnapshotMetadata);
				bool flag2 = memorySnapshotMetadata.Description == null;
				if (flag2)
				{
					memorySnapshotMetadata.Description = "";
				}
				int num = 2 * memorySnapshotMetadata.Description.Length;
				int num2 = ((memorySnapshotMetadata.Data == null) ? 0 : memorySnapshotMetadata.Data.Length);
				int num3 = num + num2 + 12;
				byte[] array2 = new byte[num3];
				int num4 = 0;
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, memorySnapshotMetadata.Description.Length);
				num4 = MemoryProfiler.WriteStringToByteArray(array2, num4, memorySnapshotMetadata.Description);
				num4 = MemoryProfiler.WriteIntToByteArray(array2, num4, num2);
				byte[] array3;
				byte* ptr;
				if ((array3 = memorySnapshotMetadata.Data) == null || array3.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array3[0];
				}
				byte[] array4;
				byte* ptr2;
				if ((array4 = array2) == null || array4.Length == 0)
				{
					ptr2 = null;
				}
				else
				{
					ptr2 = &array4[0];
				}
				byte* ptr3 = ptr2 + num4;
				UnsafeUtility.MemCpy((void*)ptr3, (void*)ptr, (long)num2);
				array3 = null;
				array4 = null;
				array = array2;
			}
			return array;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00005100 File Offset: 0x00003300
		internal unsafe static int WriteIntToByteArray(byte[] array, int offset, int value)
		{
			byte* ptr = (byte*)(&value);
			array[offset++] = *ptr;
			array[offset++] = ptr[1];
			array[offset++] = ptr[2];
			array[offset++] = ptr[3];
			return offset;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00005148 File Offset: 0x00003348
		internal unsafe static int WriteStringToByteArray(byte[] array, int offset, string value)
		{
			bool flag = value.Length != 0;
			if (flag)
			{
				fixed (string text = value)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					char* ptr3 = ptr + value.Length;
					while (ptr2 != ptr3)
					{
						for (int i = 0; i < 2; i++)
						{
							array[offset++] = *(byte*)(ptr2 + i / 2);
						}
						ptr2++;
					}
				}
			}
			return offset;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000051CC File Offset: 0x000033CC
		[RequiredByNativeCode]
		private static void FinalizeSnapshot(string path, bool result)
		{
			bool flag = MemoryProfiler.m_SnapshotFinished != null;
			if (flag)
			{
				Action<string, bool> snapshotFinished = MemoryProfiler.m_SnapshotFinished;
				MemoryProfiler.m_SnapshotFinished = null;
				snapshotFinished(path, result);
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00005200 File Offset: 0x00003400
		[RequiredByNativeCode]
		private static void SaveScreenshotToDisk(string path, bool result, IntPtr pixelsPtr, int pixelsCount, TextureFormat format, int width, int height)
		{
			bool flag = MemoryProfiler.m_SaveScreenshotToDisk != null;
			if (flag)
			{
				Action<string, bool, DebugScreenCapture> saveScreenshotToDisk = MemoryProfiler.m_SaveScreenshotToDisk;
				MemoryProfiler.m_SaveScreenshotToDisk = null;
				DebugScreenCapture debugScreenCapture = default(DebugScreenCapture);
				if (result)
				{
					NativeArray<byte> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(pixelsPtr.ToPointer(), pixelsCount, Allocator.Persistent);
					debugScreenCapture.RawImageDataReference = nativeArray;
					debugScreenCapture.Height = height;
					debugScreenCapture.Width = width;
					debugScreenCapture.ImageFormat = format;
				}
				saveScreenshotToDisk(path, result, debugScreenCapture);
			}
		}

		// Token: 0x0600025C RID: 604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartOperation_Injected(uint captureFlags, bool requestScreenshot, ref ManagedSpanWrapper path, bool isRemote);
	}
}
