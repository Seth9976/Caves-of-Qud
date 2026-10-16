using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x0200024B RID: 587
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	public sealed class Resources
	{
		// Token: 0x06001EE7 RID: 7911 RVA: 0x0003B624 File Offset: 0x00039824
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			bool flag = rawObjects == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] array2 = new T[rawObjects.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (T)((object)rawObjects[i]);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0003B670 File Offset: 0x00039870
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return ResourcesAPI.ActiveAPI.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0003B690 File Offset: 0x00039890
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0003B6B8 File Offset: 0x000398B8
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0003B6DC File Offset: 0x000398DC
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0003B704 File Offset: 0x00039904
		public static Object Load(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.Load(path, systemTypeInstance);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0003B724 File Offset: 0x00039924
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0003B748 File Offset: 0x00039948
		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0003B76C File Offset: 0x0003996C
		public static ResourceRequest LoadAsync(string path, Type type)
		{
			return ResourcesAPI.ActiveAPI.LoadAsync(path, type);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0003B78C File Offset: 0x0003998C
		public static Object[] LoadAll(string path, Type systemTypeInstance)
		{
			return ResourcesAPI.ActiveAPI.LoadAll(path, systemTypeInstance);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0003B7AC File Offset: 0x000399AC
		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0003B7D0 File Offset: 0x000399D0
		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0003B7F8 File Offset: 0x000399F8
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction("GetScriptingBuiltinResource", ThrowsException = true)]
		public unsafe static Object GetBuiltinResource([NotNull] Type type, string path)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
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
				IntPtr builtinResource_Injected = Resources.GetBuiltinResource_Injected(type, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr builtinResource_Injected;
				@object = Unmarshal.UnmarshalUnityObject<Object>(builtinResource_Injected);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0003B868 File Offset: 0x00039A68
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0003B88F File Offset: 0x00039A8F
		public static void UnloadAsset(Object assetToUnload)
		{
			ResourcesAPI.ActiveAPI.UnloadAsset(assetToUnload);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0003B8A0 File Offset: 0x00039AA0
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		private static void UnloadAssetImplResourceManager(Object assetToUnload)
		{
			Resources.UnloadAssetImplResourceManager_Injected(Object.MarshalledUnityObject.Marshal<Object>(assetToUnload));
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x0003B8B8 File Offset: 0x00039AB8
		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		public static AsyncOperation UnloadUnusedAssets()
		{
			IntPtr intPtr = Resources.UnloadUnusedAssets_Injected();
			return (intPtr == 0) ? null : AsyncOperation.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0003B8D8 File Offset: 0x00039AD8
		[FreeFunction("Resources_Bindings::InstanceIDToObject")]
		public static Object InstanceIDToObject(int instanceID)
		{
			return Unmarshal.UnmarshalUnityObject<Object>(Resources.InstanceIDToObject_Injected(instanceID));
		}

		// Token: 0x06001EF9 RID: 7929
		[FreeFunction("Resources_Bindings::IsInstanceLoaded")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsInstanceLoaded(int instanceID);

		// Token: 0x06001EFA RID: 7930
		[FreeFunction("Resources_Bindings::InstanceIDToObjectList", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstanceIDToObjectList(IntPtr instanceIDs, int instanceCount, List<Object> objects);

		// Token: 0x06001EFB RID: 7931 RVA: 0x0003B8F0 File Offset: 0x00039AF0
		public static void InstanceIDToObjectList(NativeArray<int> instanceIDs, List<Object> objects)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = objects == null;
			if (flag2)
			{
				throw new ArgumentNullException("objects");
			}
			bool flag3 = instanceIDs.Length == 0;
			if (flag3)
			{
				objects.Clear();
			}
			else
			{
				Resources.InstanceIDToObjectList((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, objects);
			}
		}

		// Token: 0x06001EFC RID: 7932
		[FreeFunction("Resources_Bindings::InstanceIDsToValidArray", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InstanceIDsToValidArray_Internal(IntPtr instanceIDs, int instanceCount, IntPtr validArray, int validArrayCount);

		// Token: 0x06001EFD RID: 7933
		[FreeFunction("Resources_Bindings::DoesObjectWithInstanceIDExist", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool InstanceIDIsValid(int instanceId);

		// Token: 0x06001EFE RID: 7934 RVA: 0x0003B960 File Offset: 0x00039B60
		public static void InstanceIDsToValidArray(NativeArray<int> instanceIDs, NativeArray<bool> validArray)
		{
			bool flag = !instanceIDs.IsCreated;
			if (flag)
			{
				throw new ArgumentException("NativeArray is uninitialized", "instanceIDs");
			}
			bool flag2 = !validArray.IsCreated;
			if (flag2)
			{
				throw new ArgumentException("NativeArray is uninitialized", "validArray");
			}
			bool flag3 = instanceIDs.Length != validArray.Length;
			if (flag3)
			{
				throw new ArgumentException("Size mismatch! Both arrays must be the same length.");
			}
			bool flag4 = instanceIDs.Length == 0;
			if (!flag4)
			{
				Resources.InstanceIDsToValidArray_Internal((IntPtr)instanceIDs.GetUnsafeReadOnlyPtr<int>(), instanceIDs.Length, (IntPtr)validArray.GetUnsafePtr<bool>(), validArray.Length);
			}
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0003BA08 File Offset: 0x00039C08
		public unsafe static void InstanceIDsToValidArray(ReadOnlySpan<int> instanceIDs, Span<bool> validArray)
		{
			bool flag = instanceIDs.Length != validArray.Length;
			if (flag)
			{
				throw new ArgumentException("Size mismatch! Both arrays must be the same length.");
			}
			bool flag2 = instanceIDs.Length == 0;
			if (!flag2)
			{
				fixed (int* pinnableReference = instanceIDs.GetPinnableReference())
				{
					int* ptr = pinnableReference;
					fixed (bool* pinnableReference2 = validArray.GetPinnableReference())
					{
						bool* ptr2 = pinnableReference2;
						Resources.InstanceIDsToValidArray_Internal((IntPtr)((void*)ptr), instanceIDs.Length, (IntPtr)((void*)ptr2), validArray.Length);
					}
				}
			}
		}

		// Token: 0x06001F01 RID: 7937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBuiltinResource_Injected(Type type, ref ManagedSpanWrapper path);

		// Token: 0x06001F02 RID: 7938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UnloadAssetImplResourceManager_Injected(IntPtr assetToUnload);

		// Token: 0x06001F03 RID: 7939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadUnusedAssets_Injected();

		// Token: 0x06001F04 RID: 7940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr InstanceIDToObject_Injected(int instanceID);
	}
}
