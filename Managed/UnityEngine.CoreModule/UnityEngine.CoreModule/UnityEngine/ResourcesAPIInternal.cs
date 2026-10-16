using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000247 RID: 583
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	internal static class ResourcesAPIInternal
	{
		// Token: 0x06001EC9 RID: 7881
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x06001ECA RID: 7882 RVA: 0x0003B378 File Offset: 0x00039578
		[FreeFunction("GetShaderNameRegistry().FindShader")]
		public unsafe static Shader FindShaderByName(string name)
		{
			Shader shader;
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
				IntPtr intPtr = ResourcesAPIInternal.FindShaderByName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(intPtr);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0003B3D8 File Offset: 0x000395D8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeThrows]
		[FreeFunction("Resources_Bindings::Load")]
		public unsafe static Object Load(string path, [NotNull] Type systemTypeInstance)
		{
			if (systemTypeInstance == null)
			{
				ThrowHelper.ThrowArgumentNullException(systemTypeInstance, "systemTypeInstance");
			}
			Object @object;
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
				IntPtr intPtr = ResourcesAPIInternal.Load_Injected(ref managedSpanWrapper, systemTypeInstance);
			}
			finally
			{
				IntPtr intPtr;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0003B448 File Offset: 0x00039648
		[NativeThrows]
		[FreeFunction("Resources_Bindings::LoadAll")]
		public unsafe static Object[] LoadAll([NotNull] string path, [NotNull] Type systemTypeInstance)
		{
			if (path == null)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			if (systemTypeInstance == null)
			{
				ThrowHelper.ThrowArgumentNullException(systemTypeInstance, "systemTypeInstance");
			}
			Object[] array;
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
				array = ResourcesAPIInternal.LoadAll_Injected(ref managedSpanWrapper, systemTypeInstance);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0003B4C0 File Offset: 0x000396C0
		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		internal unsafe static ResourceRequest LoadAsyncInternal(string path, Type type)
		{
			ResourceRequest resourceRequest;
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
				IntPtr intPtr = ResourcesAPIInternal.LoadAsyncInternal_Injected(ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				resourceRequest = ((intPtr2 == 0) ? null : ResourceRequest.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return resourceRequest;
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x0003B52C File Offset: 0x0003972C
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		public static void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPIInternal.UnloadAsset_Injected(Object.MarshalledUnityObject.Marshal<Object>(assetToUnload));
		}

		// Token: 0x06001ECF RID: 7887
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindShaderByName_Injected(ref ManagedSpanWrapper name);

		// Token: 0x06001ED0 RID: 7888
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Load_Injected(ref ManagedSpanWrapper path, Type systemTypeInstance);

		// Token: 0x06001ED1 RID: 7889
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] LoadAll_Injected(ref ManagedSpanWrapper path, Type systemTypeInstance);

		// Token: 0x06001ED2 RID: 7890
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAsyncInternal_Injected(ref ManagedSpanWrapper path, Type type);

		// Token: 0x06001ED3 RID: 7891
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadAsset_Injected(IntPtr assetToUnload);

		// Token: 0x02000248 RID: 584
		internal static class EntitiesAssetGC
		{
			// Token: 0x06001ED4 RID: 7892
			[FreeFunction("Resources_Bindings::MarkInstanceIDsAsRoot")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void MarkInstanceIDsAsRoot(IntPtr instanceIDs, int count, IntPtr state);

			// Token: 0x06001ED5 RID: 7893
			[FreeFunction("Resources_Bindings::EnableEntitiesAssetGCCallback")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			internal static extern void EnableEntitiesAssetGCCallback();

			// Token: 0x06001ED6 RID: 7894 RVA: 0x0003B544 File Offset: 0x00039744
			internal static void RegisterAdditionalRootsHandler(ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandlerDelegate newAdditionalRootsHandler)
			{
				bool flag = ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandler == null;
				if (flag)
				{
					ResourcesAPIInternal.EntitiesAssetGC.EnableEntitiesAssetGCCallback();
					ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandler = newAdditionalRootsHandler;
				}
				else
				{
					Debug.LogWarning("Attempting to register more than one AdditionalRootsHandlerDelegate! Only one may be registered at a time.");
				}
			}

			// Token: 0x06001ED7 RID: 7895 RVA: 0x0003B57C File Offset: 0x0003977C
			[UsedByNativeCode]
			private static void GetAdditionalRoots(IntPtr state)
			{
				bool flag = ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandler != null;
				if (flag)
				{
					ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandler(state);
				}
			}

			// Token: 0x0400091E RID: 2334
			internal static ResourcesAPIInternal.EntitiesAssetGC.AdditionalRootsHandlerDelegate AdditionalRootsHandler;

			// Token: 0x02000249 RID: 585
			// (Invoke) Token: 0x06001ED9 RID: 7897
			internal delegate void AdditionalRootsHandlerDelegate(IntPtr state);
		}
	}
}
