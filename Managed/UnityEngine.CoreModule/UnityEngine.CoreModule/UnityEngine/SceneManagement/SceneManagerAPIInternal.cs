using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200037D RID: 893
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
	internal static class SceneManagerAPIInternal
	{
		// Token: 0x06002911 RID: 10513
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetNumScenesInBuildSettings();

		// Token: 0x06002912 RID: 10514 RVA: 0x0004F354 File Offset: 0x0004D554
		[NativeThrows]
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			Scene scene;
			SceneManagerAPIInternal.GetSceneByBuildIndex_Injected(buildIndex, out scene);
			return scene;
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0004F36C File Offset: 0x0004D56C
		[NativeThrows]
		public unsafe static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			AsyncOperation asyncOperation;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sceneName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = sceneName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = SceneManagerAPIInternal.LoadSceneAsyncNameIndexInternal_Injected(ref managedSpanWrapper, sceneBuildIndex, ref parameters, mustCompleteNextFrame);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				asyncOperation = ((intPtr2 == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return asyncOperation;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0004F3D8 File Offset: 0x0004D5D8
		[NativeThrows]
		public unsafe static AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			AsyncOperation asyncOperation;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(sceneName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = sceneName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = SceneManagerAPIInternal.UnloadSceneNameIndexInternal_Injected(ref managedSpanWrapper, sceneBuildIndex, immediately, options, out outSuccess);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				asyncOperation = ((intPtr2 == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return asyncOperation;
		}

		// Token: 0x06002915 RID: 10517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByBuildIndex_Injected(int buildIndex, out Scene ret);

		// Token: 0x06002916 RID: 10518
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadSceneAsyncNameIndexInternal_Injected(ref ManagedSpanWrapper sceneName, int sceneBuildIndex, [In] ref LoadSceneParameters parameters, bool mustCompleteNextFrame);

		// Token: 0x06002917 RID: 10519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadSceneNameIndexInternal_Injected(ref ManagedSpanWrapper sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess);
	}
}
