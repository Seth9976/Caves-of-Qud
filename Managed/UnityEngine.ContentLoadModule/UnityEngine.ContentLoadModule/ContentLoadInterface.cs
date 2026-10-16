using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Content;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.SceneManagement;

namespace Unity.Loading
{
	// Token: 0x02000009 RID: 9
	[StaticAccessor("GetContentLoadFrontend()", StaticAccessorType.Dot)]
	[NativeHeader("Modules/ContentLoad/Public/ContentLoadFrontend.h")]
	public static class ContentLoadInterface
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000236C File Offset: 0x0000056C
		[NativeThrows]
		internal unsafe static ContentFile LoadContentFileAsync(ContentNamespace nameSpace, string filename, void* dependencies, int dependencyCount, JobHandle dependentFence, bool useUnsafe = false)
		{
			ContentFile contentFile2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ContentFile contentFile;
				ContentLoadInterface.LoadContentFileAsync_Injected(ref nameSpace, ref managedSpanWrapper, dependencies, dependencyCount, ref dependentFence, useUnsafe, out contentFile);
			}
			finally
			{
				char* ptr = null;
				ContentFile contentFile;
				contentFile2 = contentFile;
			}
			return contentFile2;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023D0 File Offset: 0x000005D0
		[NativeThrows]
		internal static void ContentFile_UnloadAsync(ContentFile handle)
		{
			ContentLoadInterface.ContentFile_UnloadAsync_Injected(ref handle);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E4 File Offset: 0x000005E4
		internal static Object ContentFile_GetObject(ContentFile handle, ulong localIdentifierInFile)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(ContentLoadInterface.ContentFile_GetObject_Injected(ref handle, localIdentifierInFile));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002400 File Offset: 0x00000600
		internal static Object[] ContentFile_GetObjects(ContentFile handle)
		{
			return ContentLoadInterface.ContentFile_GetObjects_Injected(ref handle);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002414 File Offset: 0x00000614
		internal static LoadingStatus ContentFile_GetLoadingStatus(ContentFile handle)
		{
			return ContentLoadInterface.ContentFile_GetLoadingStatus_Injected(ref handle);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002428 File Offset: 0x00000628
		internal static bool ContentFile_IsHandleValid(ContentFile handle)
		{
			return ContentLoadInterface.ContentFile_IsHandleValid_Injected(ref handle);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31
		// (set) Token: 0x06000020 RID: 32
		internal static extern float IntegrationTimeMS
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000243C File Offset: 0x0000063C
		internal static bool WaitForLoadCompletion(ContentFile handle, int timeoutMs)
		{
			return ContentLoadInterface.WaitForLoadCompletion_Injected(ref handle, timeoutMs);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002454 File Offset: 0x00000654
		internal static bool WaitForUnloadCompletion(ContentFile handle, int timeoutMs)
		{
			return ContentLoadInterface.WaitForUnloadCompletion_Injected(ref handle, timeoutMs);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000246C File Offset: 0x0000066C
		internal static bool ContentFile_IsUnloadComplete(ContentFile handle)
		{
			return ContentLoadInterface.ContentFile_IsUnloadComplete_Injected(ref handle);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002480 File Offset: 0x00000680
		[NativeThrows]
		internal unsafe static ContentSceneFile LoadSceneAsync(ContentNamespace nameSpace, string filename, string sceneName, ContentSceneParameters sceneParams, ContentFile* dependencies, int dependencyCount, JobHandle dependentFence)
		{
			ContentSceneFile contentSceneFile2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filename, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filename.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sceneName, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = sceneName.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				ContentSceneFile contentSceneFile;
				ContentLoadInterface.LoadSceneAsync_Injected(ref nameSpace, ref managedSpanWrapper, ref managedSpanWrapper2, ref sceneParams, dependencies, dependencyCount, ref dependentFence, out contentSceneFile);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
				ContentSceneFile contentSceneFile;
				contentSceneFile2 = contentSceneFile;
			}
			return contentSceneFile2;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002518 File Offset: 0x00000718
		internal static Scene ContentSceneFile_GetScene(ContentSceneFile handle)
		{
			Scene scene;
			ContentLoadInterface.ContentSceneFile_GetScene_Injected(ref handle, out scene);
			return scene;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002530 File Offset: 0x00000730
		internal static SceneLoadingStatus ContentSceneFile_GetStatus(ContentSceneFile handle)
		{
			return ContentLoadInterface.ContentSceneFile_GetStatus_Injected(ref handle);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002544 File Offset: 0x00000744
		[NativeThrows]
		internal static void ContentSceneFile_IntegrateAtEndOfFrame(ContentSceneFile handle)
		{
			ContentLoadInterface.ContentSceneFile_IntegrateAtEndOfFrame_Injected(ref handle);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002558 File Offset: 0x00000758
		internal static bool ContentSceneFile_UnloadAtEndOfFrame(ContentSceneFile handle)
		{
			return ContentLoadInterface.ContentSceneFile_UnloadAtEndOfFrame_Injected(ref handle);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000256C File Offset: 0x0000076C
		internal static bool ContentSceneFile_IsHandleValid(ContentSceneFile handle)
		{
			return ContentLoadInterface.ContentSceneFile_IsHandleValid_Injected(ref handle);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002580 File Offset: 0x00000780
		internal static bool ContentSceneFile_WaitForCompletion(ContentSceneFile handle, int timeoutMs)
		{
			return ContentLoadInterface.ContentSceneFile_WaitForCompletion_Injected(ref handle, timeoutMs);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002598 File Offset: 0x00000798
		public unsafe static ContentSceneFile LoadSceneAsync(ContentNamespace nameSpace, string filename, string sceneName, ContentSceneParameters sceneParams, NativeArray<ContentFile> dependencies, JobHandle dependentFence = default(JobHandle))
		{
			return ContentLoadInterface.LoadSceneAsync(nameSpace, filename, sceneName, sceneParams, (ContentFile*)dependencies.m_Buffer, dependencies.Length, dependentFence);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000025C4 File Offset: 0x000007C4
		public static ContentFile LoadContentFileAsync(ContentNamespace nameSpace, string filename, NativeArray<ContentFile> dependencies, JobHandle dependentFence = default(JobHandle))
		{
			return ContentLoadInterface.LoadContentFileAsync(nameSpace, filename, dependencies.m_Buffer, dependencies.Length, dependentFence, false);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025F0 File Offset: 0x000007F0
		public static ContentFile[] GetContentFiles(ContentNamespace nameSpace)
		{
			ContentFile[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentLoadInterface.GetContentFiles_Injected(ref nameSpace, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentFile[] array;
				blittableArrayWrapper.Unmarshal<ContentFile>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002628 File Offset: 0x00000828
		public static ContentSceneFile[] GetSceneFiles(ContentNamespace nameSpace)
		{
			ContentSceneFile[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentLoadInterface.GetSceneFiles_Injected(ref nameSpace, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				ContentSceneFile[] array;
				blittableArrayWrapper.Unmarshal<ContentSceneFile>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002660 File Offset: 0x00000860
		public static float GetIntegrationTimeMS()
		{
			return ContentLoadInterface.IntegrationTimeMS;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002678 File Offset: 0x00000878
		public static void SetIntegrationTimeMS(float integrationTimeMS)
		{
			bool flag = integrationTimeMS <= 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("integrationTimeMS", "integrationTimeMS was out of range. Must be greater than zero.");
			}
			ContentLoadInterface.IntegrationTimeMS = integrationTimeMS;
		}

		// Token: 0x06000031 RID: 49
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void LoadContentFileAsync_Injected([In] ref ContentNamespace nameSpace, ref ManagedSpanWrapper filename, void* dependencies, int dependencyCount, [In] ref JobHandle dependentFence, bool useUnsafe, out ContentFile ret);

		// Token: 0x06000032 RID: 50
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ContentFile_UnloadAsync_Injected([In] ref ContentFile handle);

		// Token: 0x06000033 RID: 51
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr ContentFile_GetObject_Injected([In] ref ContentFile handle, ulong localIdentifierInFile);

		// Token: 0x06000034 RID: 52
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] ContentFile_GetObjects_Injected([In] ref ContentFile handle);

		// Token: 0x06000035 RID: 53
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LoadingStatus ContentFile_GetLoadingStatus_Injected([In] ref ContentFile handle);

		// Token: 0x06000036 RID: 54
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContentFile_IsHandleValid_Injected([In] ref ContentFile handle);

		// Token: 0x06000037 RID: 55
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool WaitForLoadCompletion_Injected([In] ref ContentFile handle, int timeoutMs);

		// Token: 0x06000038 RID: 56
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool WaitForUnloadCompletion_Injected([In] ref ContentFile handle, int timeoutMs);

		// Token: 0x06000039 RID: 57
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContentFile_IsUnloadComplete_Injected([In] ref ContentFile handle);

		// Token: 0x0600003A RID: 58
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void LoadSceneAsync_Injected([In] ref ContentNamespace nameSpace, ref ManagedSpanWrapper filename, ref ManagedSpanWrapper sceneName, [In] ref ContentSceneParameters sceneParams, ContentFile* dependencies, int dependencyCount, [In] ref JobHandle dependentFence, out ContentSceneFile ret);

		// Token: 0x0600003B RID: 59
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ContentSceneFile_GetScene_Injected([In] ref ContentSceneFile handle, out Scene ret);

		// Token: 0x0600003C RID: 60
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern SceneLoadingStatus ContentSceneFile_GetStatus_Injected([In] ref ContentSceneFile handle);

		// Token: 0x0600003D RID: 61
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ContentSceneFile_IntegrateAtEndOfFrame_Injected([In] ref ContentSceneFile handle);

		// Token: 0x0600003E RID: 62
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContentSceneFile_UnloadAtEndOfFrame_Injected([In] ref ContentSceneFile handle);

		// Token: 0x0600003F RID: 63
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContentSceneFile_IsHandleValid_Injected([In] ref ContentSceneFile handle);

		// Token: 0x06000040 RID: 64
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContentSceneFile_WaitForCompletion_Injected([In] ref ContentSceneFile handle, int timeoutMs);

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetContentFiles_Injected([In] ref ContentNamespace nameSpace, out BlittableArrayWrapper ret);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneFiles_Injected([In] ref ContentNamespace nameSpace, out BlittableArrayWrapper ret);
	}
}
