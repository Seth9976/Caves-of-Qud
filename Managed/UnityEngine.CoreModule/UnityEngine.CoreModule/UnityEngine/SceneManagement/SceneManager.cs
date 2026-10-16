using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200037F RID: 895
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	public class SceneManager
	{
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06002922 RID: 10530
		public static extern int sceneCount
		{
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[NativeMethod("GetSceneCount")]
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06002923 RID: 10531
		public static extern int loadedSceneCount
		{
			[NativeMethod("GetLoadedSceneCount")]
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06002924 RID: 10532 RVA: 0x0004F49C File Offset: 0x0004D69C
		public static int sceneCountInBuildSettings
		{
			get
			{
				return SceneManagerAPI.ActiveAPI.GetNumScenesInBuildSettings();
			}
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0004F4B8 File Offset: 0x0004D6B8
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		internal static bool CanSetAsActiveScene(Scene scene)
		{
			return SceneManager.CanSetAsActiveScene_Injected(ref scene);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x0004F4CC File Offset: 0x0004D6CC
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene scene;
			SceneManager.GetActiveScene_Injected(out scene);
			return scene;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.SetActiveScene_Injected(ref scene);
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0004F4F8 File Offset: 0x0004D6F8
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public unsafe static Scene GetSceneByPath(string scenePath)
		{
			Scene scene2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(scenePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = scenePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Scene scene;
				SceneManager.GetSceneByPath_Injected(ref managedSpanWrapper, out scene);
			}
			finally
			{
				char* ptr = null;
				Scene scene;
				scene2 = scene;
			}
			return scene2;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0004F554 File Offset: 0x0004D754
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public unsafe static Scene GetSceneByName(string name)
		{
			Scene scene2;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Scene scene;
				SceneManager.GetSceneByName_Injected(ref managedSpanWrapper, out scene);
			}
			finally
			{
				char* ptr = null;
				Scene scene;
				scene2 = scene;
			}
			return scene2;
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x0004F5B0 File Offset: 0x0004D7B0
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			return SceneManagerAPI.ActiveAPI.GetSceneByBuildIndex(buildIndex);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x0004F5D0 File Offset: 0x0004D7D0
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static Scene GetSceneAt(int index)
		{
			Scene scene;
			SceneManager.GetSceneAt_Injected(index, out scene);
			return scene;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0004F5E8 File Offset: 0x0004D7E8
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public unsafe static Scene CreateScene([NotNull] string sceneName, CreateSceneParameters parameters)
		{
			if (sceneName == null)
			{
				ThrowHelper.ThrowArgumentNullException(sceneName, "sceneName");
			}
			Scene scene2;
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
				Scene scene;
				SceneManager.CreateScene_Injected(ref managedSpanWrapper, ref parameters, out scene);
			}
			finally
			{
				char* ptr = null;
				Scene scene;
				scene2 = scene;
			}
			return scene2;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0004F654 File Offset: 0x0004D854
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneInternal_Injected(ref scene, options);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x0004F66C File Offset: 0x0004D86C
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			IntPtr intPtr = SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x0004F690 File Offset: 0x0004D890
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation asyncOperation;
			if (flag)
			{
				asyncOperation = null;
			}
			else
			{
				asyncOperation = SceneManagerAPI.ActiveAPI.LoadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, parameters, mustCompleteNextFrame);
			}
			return asyncOperation;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x0004F6C0 File Offset: 0x0004D8C0
		private static AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess)
		{
			bool flag = !SceneManager.s_AllowLoadScene;
			AsyncOperation asyncOperation;
			if (flag)
			{
				outSuccess = false;
				asyncOperation = null;
			}
			else
			{
				asyncOperation = SceneManagerAPI.ActiveAPI.UnloadSceneAsyncByNameOrIndex(sceneName, sceneBuildIndex, immediately, options, out outSuccess);
			}
			return asyncOperation;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x0004F6F8 File Offset: 0x0004D8F8
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x0004F710 File Offset: 0x0004D910
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MoveGameObjectToScene([NotNull] GameObject go, Scene scene)
		{
			if (go == null)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GameObject>(go);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(go, "go");
			}
			SceneManager.MoveGameObjectToScene_Injected(intPtr, ref scene);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x0004F748 File Offset: 0x0004D948
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static void MoveGameObjectsToSceneByInstanceId(IntPtr instanceIds, int instanceCount, Scene scene)
		{
			SceneManager.MoveGameObjectsToSceneByInstanceId_Injected(instanceIds, instanceCount, ref scene);
		}

		// Token: 0x06002934 RID: 10548 RVA: 0x0004F760 File Offset: 0x0004D960
		public static void MoveGameObjectsToScene(NativeArray<int> instanceIDs, Scene scene)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = instanceIDs.Length == 0;
			if (!flag2)
			{
				SceneManager.MoveGameObjectsToSceneByInstanceId((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, scene);
			}
		}

		// Token: 0x06002935 RID: 10549 RVA: 0x0004F7B8 File Offset: 0x0004D9B8
		[RequiredByNativeCode]
		internal static AsyncOperation LoadFirstScene_Internal(bool async)
		{
			return SceneManagerAPI.ActiveAPI.LoadFirstScene(async);
		}

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06002936 RID: 10550 RVA: 0x0004F7D8 File Offset: 0x0004D9D8
		// (remove) Token: 0x06002937 RID: 10551 RVA: 0x0004F80C File Offset: 0x0004DA0C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		// Token: 0x1400002B RID: 43
		// (add) Token: 0x06002938 RID: 10552 RVA: 0x0004F840 File Offset: 0x0004DA40
		// (remove) Token: 0x06002939 RID: 10553 RVA: 0x0004F874 File Offset: 0x0004DA74
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event UnityAction<Scene> sceneUnloaded;

		// Token: 0x1400002C RID: 44
		// (add) Token: 0x0600293A RID: 10554 RVA: 0x0004F8A8 File Offset: 0x0004DAA8
		// (remove) Token: 0x0600293B RID: 10555 RVA: 0x0004F8DC File Offset: 0x0004DADC
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event UnityAction<Scene, Scene> activeSceneChanged;

		// Token: 0x0600293C RID: 10556 RVA: 0x0004F910 File Offset: 0x0004DB10
		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		// Token: 0x0600293D RID: 10557 RVA: 0x0004F954 File Offset: 0x0004DB54
		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return SceneManager.CreateScene(sceneName, createSceneParameters);
		}

		// Token: 0x0600293E RID: 10558 RVA: 0x0004F978 File Offset: 0x0004DB78
		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x0600293F RID: 10559 RVA: 0x0004F998 File Offset: 0x0004DB98
		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x06002940 RID: 10560 RVA: 0x0004F9B8 File Offset: 0x0004DBB8
		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x0004F9E0 File Offset: 0x0004DBE0
		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06002942 RID: 10562 RVA: 0x0004FA00 File Offset: 0x0004DC00
		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x0004FA20 File Offset: 0x0004DC20
		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x0004FA48 File Offset: 0x0004DC48
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x0004FA6C File Offset: 0x0004DC6C
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x0004FA90 File Offset: 0x0004DC90
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, false);
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x0004FAAC File Offset: 0x0004DCAC
		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0004FAD0 File Offset: 0x0004DCD0
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0004FAF4 File Offset: 0x0004DCF4
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x0004FB10 File Offset: 0x0004DD10
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return SceneManager.UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x0600294B RID: 10571 RVA: 0x0004FB2C File Offset: 0x0004DD2C
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x0004FB50 File Offset: 0x0004DD50
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x0600294D RID: 10573 RVA: 0x0004FB70 File Offset: 0x0004DD70
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x0004FB94 File Offset: 0x0004DD94
		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x0004FBB4 File Offset: 0x0004DDB4
		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x0004FBD0 File Offset: 0x0004DDD0
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, options, out flag);
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x0004FBF4 File Offset: 0x0004DDF4
		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, options, out flag);
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x0004FC14 File Offset: 0x0004DE14
		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x0004FC30 File Offset: 0x0004DE30
		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x0004FC5C File Offset: 0x0004DE5C
		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0004FC84 File Offset: 0x0004DE84
		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		// Token: 0x06002958 RID: 10584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CanSetAsActiveScene_Injected([In] ref Scene scene);

		// Token: 0x06002959 RID: 10585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		// Token: 0x0600295A RID: 10586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetActiveScene_Injected([In] ref Scene scene);

		// Token: 0x0600295B RID: 10587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByPath_Injected(ref ManagedSpanWrapper scenePath, out Scene ret);

		// Token: 0x0600295C RID: 10588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneByName_Injected(ref ManagedSpanWrapper name, out Scene ret);

		// Token: 0x0600295D RID: 10589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		// Token: 0x0600295E RID: 10590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScene_Injected(ref ManagedSpanWrapper sceneName, [In] ref CreateSceneParameters parameters, out Scene ret);

		// Token: 0x0600295F RID: 10591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnloadSceneInternal_Injected([In] ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06002960 RID: 10592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadSceneAsyncInternal_Injected([In] ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06002961 RID: 10593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MergeScenes_Injected([In] ref Scene sourceScene, [In] ref Scene destinationScene);

		// Token: 0x06002962 RID: 10594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveGameObjectToScene_Injected(IntPtr go, [In] ref Scene scene);

		// Token: 0x06002963 RID: 10595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MoveGameObjectsToSceneByInstanceId_Injected(IntPtr instanceIds, int instanceCount, [In] ref Scene scene);

		// Token: 0x04000BA6 RID: 2982
		internal static bool s_AllowLoadScene = true;
	}
}
