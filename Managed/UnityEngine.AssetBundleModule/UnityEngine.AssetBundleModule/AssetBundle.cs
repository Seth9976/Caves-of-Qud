using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromMemoryAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromManagedStreamAsyncOperation.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetOperation.h")]
	[NativeHeader("Runtime/Scripting/ScriptingExportUtility.h")]
	[NativeHeader("Runtime/Scripting/ScriptingUtility.h")]
	[NativeHeader("AssetBundleScriptingClasses.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleSaveAndLoadHelper.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleUtility.h")]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadAssetUtility.h")]
	[ExcludeFromPreset]
	[NativeHeader("Modules/AssetBundle/Public/AssetBundleLoadFromFileAsyncOperation.h")]
	public class AssetBundle : Object
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private AssetBundle()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		[Obsolete("mainAsset has been made obsolete. Please use the new AssetBundle build system introduced in 5.0 and check BuildAssetBundles documentation for details.")]
		public Object mainAsset
		{
			get
			{
				return AssetBundle.returnMainAsset(this);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002074 File Offset: 0x00000274
		[FreeFunction("LoadMainObjectFromAssetBundle", true)]
		internal static Object returnMainAsset([NotNull] AssetBundle bundle)
		{
			if (bundle == null)
			{
				ThrowHelper.ThrowArgumentNullException(bundle, "bundle");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(bundle);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(bundle, "bundle");
			}
			return Unmarshal.UnmarshalUnityObject<Object>(AssetBundle.returnMainAsset_Injected(intPtr));
		}

		// Token: 0x06000004 RID: 4
		[FreeFunction("UnloadAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnloadAllAssetBundles(bool unloadAllObjects);

		// Token: 0x06000005 RID: 5
		[FreeFunction("GetAllAssetBundles")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssetBundle[] GetAllLoadedAssetBundles_Native();

		// Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		public static IEnumerable<AssetBundle> GetAllLoadedAssetBundles()
		{
			return AssetBundle.GetAllLoadedAssetBundles_Native();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		[FreeFunction("LoadFromFileAsync")]
		internal unsafe static AssetBundleCreateRequest LoadFromFileAsync_Internal(string path, uint crc, ulong offset)
		{
			AssetBundleCreateRequest assetBundleCreateRequest;
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
				IntPtr intPtr = AssetBundle.LoadFromFileAsync_Internal_Injected(ref managedSpanWrapper, crc, offset);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				assetBundleCreateRequest = ((intPtr2 == 0) ? null : AssetBundleCreateRequest.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
			}
			return assetBundleCreateRequest;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002134 File Offset: 0x00000334
		public static AssetBundleCreateRequest LoadFromFileAsync(string path)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, 0U, 0UL);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, 0UL);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000216C File Offset: 0x0000036C
		public static AssetBundleCreateRequest LoadFromFileAsync(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFileAsync_Internal(path, crc, offset);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002188 File Offset: 0x00000388
		[FreeFunction("LoadFromFile")]
		internal unsafe static AssetBundle LoadFromFile_Internal(string path, uint crc, ulong offset)
		{
			AssetBundle assetBundle;
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
				IntPtr intPtr = AssetBundle.LoadFromFile_Internal_Injected(ref managedSpanWrapper, crc, offset);
			}
			finally
			{
				IntPtr intPtr;
				assetBundle = Unmarshal.UnmarshalUnityObject<AssetBundle>(intPtr);
				char* ptr = null;
			}
			return assetBundle;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		public static AssetBundle LoadFromFile(string path)
		{
			return AssetBundle.LoadFromFile_Internal(path, 0U, 0UL);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002208 File Offset: 0x00000408
		public static AssetBundle LoadFromFile(string path, uint crc)
		{
			return AssetBundle.LoadFromFile_Internal(path, crc, 0UL);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002224 File Offset: 0x00000424
		public static AssetBundle LoadFromFile(string path, uint crc, ulong offset)
		{
			return AssetBundle.LoadFromFile_Internal(path, crc, offset);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002240 File Offset: 0x00000440
		[FreeFunction("LoadFromMemoryAsync")]
		internal unsafe static AssetBundleCreateRequest LoadFromMemoryAsync_Internal(byte[] binary, uint crc)
		{
			Span<byte> span = new Span<byte>(binary);
			AssetBundleCreateRequest assetBundleCreateRequest;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				IntPtr intPtr = AssetBundle.LoadFromMemoryAsync_Internal_Injected(ref managedSpanWrapper, crc);
				assetBundleCreateRequest = ((intPtr == 0) ? null : AssetBundleCreateRequest.BindingsMarshaller.ConvertToManaged(intPtr));
			}
			return assetBundleCreateRequest;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002288 File Offset: 0x00000488
		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary)
		{
			return AssetBundle.LoadFromMemoryAsync_Internal(binary, 0U);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022A4 File Offset: 0x000004A4
		public static AssetBundleCreateRequest LoadFromMemoryAsync(byte[] binary, uint crc)
		{
			return AssetBundle.LoadFromMemoryAsync_Internal(binary, crc);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022C0 File Offset: 0x000004C0
		[FreeFunction("LoadFromMemory")]
		internal unsafe static AssetBundle LoadFromMemory_Internal(byte[] binary, uint crc)
		{
			Span<byte> span = new Span<byte>(binary);
			AssetBundle assetBundle;
			fixed (byte* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				assetBundle = Unmarshal.UnmarshalUnityObject<AssetBundle>(AssetBundle.LoadFromMemory_Internal_Injected(ref managedSpanWrapper, crc));
			}
			return assetBundle;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002300 File Offset: 0x00000500
		public static AssetBundle LoadFromMemory(byte[] binary)
		{
			return AssetBundle.LoadFromMemory_Internal(binary, 0U);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000231C File Offset: 0x0000051C
		public static AssetBundle LoadFromMemory(byte[] binary, uint crc)
		{
			return AssetBundle.LoadFromMemory_Internal(binary, crc);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002338 File Offset: 0x00000538
		internal static void ValidateLoadFromStream(Stream stream)
		{
			bool flag = stream == null;
			if (flag)
			{
				throw new ArgumentNullException("ManagedStream object must be non-null", "stream");
			}
			bool flag2 = !stream.CanRead;
			if (flag2)
			{
				throw new ArgumentException("ManagedStream object must be readable (stream.CanRead must return true)", "stream");
			}
			bool flag3 = !stream.CanSeek;
			if (flag3)
			{
				throw new ArgumentException("ManagedStream object must be seekable (stream.CanSeek must return true)", "stream");
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002398 File Offset: 0x00000598
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc, uint managedReadBufferSize)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, crc, managedReadBufferSize);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023BC File Offset: 0x000005BC
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream, uint crc)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, crc, 0U);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023E0 File Offset: 0x000005E0
		public static AssetBundleCreateRequest LoadFromStreamAsync(Stream stream)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamAsyncInternal(stream, 0U, 0U);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002404 File Offset: 0x00000604
		public static AssetBundle LoadFromStream(Stream stream, uint crc, uint managedReadBufferSize)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, crc, managedReadBufferSize);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002428 File Offset: 0x00000628
		public static AssetBundle LoadFromStream(Stream stream, uint crc)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, crc, 0U);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000244C File Offset: 0x0000064C
		public static AssetBundle LoadFromStream(Stream stream)
		{
			AssetBundle.ValidateLoadFromStream(stream);
			return AssetBundle.LoadFromStreamInternal(stream, 0U, 0U);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002470 File Offset: 0x00000670
		[FreeFunction("LoadFromStreamAsyncInternal")]
		internal static AssetBundleCreateRequest LoadFromStreamAsyncInternal(Stream stream, uint crc, uint managedReadBufferSize)
		{
			IntPtr intPtr = AssetBundle.LoadFromStreamAsyncInternal_Injected(stream, crc, managedReadBufferSize);
			return (intPtr == 0) ? null : AssetBundleCreateRequest.BindingsMarshaller.ConvertToManaged(intPtr);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002494 File Offset: 0x00000694
		[FreeFunction("LoadFromStreamInternal")]
		internal static AssetBundle LoadFromStreamInternal(Stream stream, uint crc, uint managedReadBufferSize)
		{
			return Unmarshal.UnmarshalUnityObject<AssetBundle>(AssetBundle.LoadFromStreamInternal_Injected(stream, crc, managedReadBufferSize));
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024B0 File Offset: 0x000006B0
		public bool isStreamedSceneAssetBundle
		{
			[NativeMethod("GetIsStreamedSceneAssetBundle")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return AssetBundle.get_isStreamedSceneAssetBundle_Injected(intPtr);
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024D4 File Offset: 0x000006D4
		[NativeMethod("Contains")]
		public unsafe bool Contains(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = AssetBundle.Contains_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000253C File Offset: 0x0000073C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		public Object Load(string name)
		{
			return null;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002550 File Offset: 0x00000750
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Object Load<T>(string name)
		{
			return null;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002564 File Offset: 0x00000764
		[Obsolete("Method Load has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAsset instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private Object Load(string name, Type type)
		{
			return null;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002578 File Offset: 0x00000778
		[Obsolete("Method LoadAsync has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAssetAsync instead and check the documentation for details.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private AssetBundleRequest LoadAsync(string name, Type type)
		{
			return null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000258C File Offset: 0x0000078C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		private Object[] LoadAll(Type type)
		{
			return null;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025A0 File Offset: 0x000007A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public Object[] LoadAll()
		{
			return null;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025B4 File Offset: 0x000007B4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Method LoadAll has been deprecated. Script updater cannot update it as the loading behaviour has changed. Please use LoadAllAssets instead and check the documentation for details.", true)]
		public T[] LoadAll<T>() where T : Object
		{
			return null;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C8 File Offset: 0x000007C8
		public Object LoadAsset(string name)
		{
			return this.LoadAsset(name, typeof(Object));
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025EC File Offset: 0x000007EC
		public T LoadAsset<T>(string name) where T : Object
		{
			return (T)((object)this.LoadAsset(name, typeof(T)));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002614 File Offset: 0x00000814
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		public Object LoadAsset(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAsset_Internal(name, type);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002674 File Offset: 0x00000874
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeMethod("LoadAsset_Internal")]
		[NativeThrows]
		private unsafe Object LoadAsset_Internal(string name, Type type)
		{
			Object @object;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAsset_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				@object = Unmarshal.UnmarshalUnityObject<Object>(intPtr2);
				char* ptr = null;
			}
			return @object;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000026E4 File Offset: 0x000008E4
		public AssetBundleRequest LoadAssetAsync(string name)
		{
			return this.LoadAssetAsync(name, typeof(Object));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002708 File Offset: 0x00000908
		public AssetBundleRequest LoadAssetAsync<T>(string name)
		{
			return this.LoadAssetAsync(name, typeof(T));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000272C File Offset: 0x0000092C
		public AssetBundleRequest LoadAssetAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetAsync_Internal(name, type);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000278C File Offset: 0x0000098C
		public Object[] LoadAssetWithSubAssets(string name)
		{
			return this.LoadAssetWithSubAssets(name, typeof(Object));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000027B0 File Offset: 0x000009B0
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

		// Token: 0x06000030 RID: 48 RVA: 0x000027FC File Offset: 0x000009FC
		public T[] LoadAssetWithSubAssets<T>(string name) where T : Object
		{
			return AssetBundle.ConvertObjects<T>(this.LoadAssetWithSubAssets(name, typeof(T)));
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002824 File Offset: 0x00000A24
		public Object[] LoadAssetWithSubAssets(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal(name, type);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002884 File Offset: 0x00000A84
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(Object));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028A8 File Offset: 0x00000AA8
		public AssetBundleRequest LoadAssetWithSubAssetsAsync<T>(string name)
		{
			return this.LoadAssetWithSubAssetsAsync(name, typeof(T));
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028CC File Offset: 0x00000ACC
		public AssetBundleRequest LoadAssetWithSubAssetsAsync(string name, Type type)
		{
			bool flag = name == null;
			if (flag)
			{
				throw new NullReferenceException("The input asset name cannot be null.");
			}
			bool flag2 = name.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("The input asset name cannot be empty.");
			}
			bool flag3 = type == null;
			if (flag3)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal(name, type);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000292C File Offset: 0x00000B2C
		public Object[] LoadAllAssets()
		{
			return this.LoadAllAssets(typeof(Object));
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002950 File Offset: 0x00000B50
		public T[] LoadAllAssets<T>() where T : Object
		{
			return AssetBundle.ConvertObjects<T>(this.LoadAllAssets(typeof(T)));
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002978 File Offset: 0x00000B78
		public Object[] LoadAllAssets(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssets_Internal("", type);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000029B0 File Offset: 0x00000BB0
		public AssetBundleRequest LoadAllAssetsAsync()
		{
			return this.LoadAllAssetsAsync(typeof(Object));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029D4 File Offset: 0x00000BD4
		public AssetBundleRequest LoadAllAssetsAsync<T>()
		{
			return this.LoadAllAssetsAsync(typeof(T));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000029F8 File Offset: 0x00000BF8
		public AssetBundleRequest LoadAllAssetsAsync(Type type)
		{
			bool flag = type == null;
			if (flag)
			{
				throw new NullReferenceException("The input type cannot be null.");
			}
			return this.LoadAssetWithSubAssetsAsync_Internal("", type);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002A30 File Offset: 0x00000C30
		[Obsolete("This method is deprecated.Use GetAllAssetNames() instead.", false)]
		public string[] AllAssetNames()
		{
			return this.GetAllAssetNames();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A48 File Offset: 0x00000C48
		[NativeMethod("LoadAssetAsync_Internal")]
		[NativeThrows]
		private unsafe AssetBundleRequest LoadAssetAsync_Internal(string name, Type type)
		{
			AssetBundleRequest assetBundleRequest;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAssetAsync_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				IntPtr intPtr3 = intPtr2;
				assetBundleRequest = ((intPtr3 == 0) ? null : AssetBundleRequest.BindingsMarshaller.ConvertToManaged(intPtr3));
				char* ptr = null;
			}
			return assetBundleRequest;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AC4 File Offset: 0x00000CC4
		[NativeMethod("Unload")]
		[NativeThrows]
		public void Unload(bool unloadAllLoadedObjects)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			AssetBundle.Unload_Injected(intPtr, unloadAllLoadedObjects);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AE8 File Offset: 0x00000CE8
		[NativeMethod("UnloadAsync")]
		[NativeThrows]
		public AssetBundleUnloadOperation UnloadAsync(bool unloadAllLoadedObjects)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = AssetBundle.UnloadAsync_Injected(intPtr, unloadAllLoadedObjects);
			return (intPtr2 == 0) ? null : AssetBundleUnloadOperation.BindingsMarshaller.ConvertToManaged(intPtr2);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B1C File Offset: 0x00000D1C
		[NativeMethod("GetAllAssetNames")]
		public string[] GetAllAssetNames()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AssetBundle.GetAllAssetNames_Injected(intPtr);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B40 File Offset: 0x00000D40
		[NativeMethod("GetAllScenePaths")]
		public string[] GetAllScenePaths()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return AssetBundle.GetAllScenePaths_Injected(intPtr);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B64 File Offset: 0x00000D64
		[NativeThrows]
		[NativeMethod("LoadAssetWithSubAssets_Internal")]
		internal unsafe Object[] LoadAssetWithSubAssets_Internal(string name, Type type)
		{
			Object[] array;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				array = AssetBundle.LoadAssetWithSubAssets_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				char* ptr = null;
			}
			return array;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BCC File Offset: 0x00000DCC
		[NativeMethod("LoadAssetWithSubAssetsAsync_Internal")]
		[NativeThrows]
		private unsafe AssetBundleRequest LoadAssetWithSubAssetsAsync_Internal(string name, Type type)
		{
			AssetBundleRequest assetBundleRequest;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<AssetBundle>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = AssetBundle.LoadAssetWithSubAssetsAsync_Internal_Injected(intPtr, ref managedSpanWrapper, type);
			}
			finally
			{
				IntPtr intPtr2;
				IntPtr intPtr3 = intPtr2;
				assetBundleRequest = ((intPtr3 == 0) ? null : AssetBundleRequest.BindingsMarshaller.ConvertToManaged(intPtr3));
				char* ptr = null;
			}
			return assetBundleRequest;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C48 File Offset: 0x00000E48
		public static AssetBundleRecompressOperation RecompressAssetBundleAsync(string inputPath, string outputPath, BuildCompression method, uint expectedCRC = 0U, ThreadPriority priority = ThreadPriority.Low)
		{
			return AssetBundle.RecompressAssetBundleAsync_Internal(inputPath, outputPath, method, expectedCRC, priority);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C68 File Offset: 0x00000E68
		[NativeThrows]
		[FreeFunction("RecompressAssetBundleAsync_Internal")]
		internal unsafe static AssetBundleRecompressOperation RecompressAssetBundleAsync_Internal(string inputPath, string outputPath, BuildCompression method, uint expectedCRC, ThreadPriority priority)
		{
			AssetBundleRecompressOperation assetBundleRecompressOperation;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(inputPath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = inputPath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(outputPath, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = outputPath.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				IntPtr intPtr = AssetBundle.RecompressAssetBundleAsync_Internal_Injected(ref managedSpanWrapper, ref managedSpanWrapper2, ref method, expectedCRC, priority);
			}
			finally
			{
				IntPtr intPtr;
				IntPtr intPtr2 = intPtr;
				assetBundleRecompressOperation = ((intPtr2 == 0) ? null : AssetBundleRecompressOperation.BindingsMarshaller.ConvertToManaged(intPtr2));
				char* ptr = null;
				char* ptr2 = null;
			}
			return assetBundleRecompressOperation;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002D0C File Offset: 0x00000F0C
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002D23 File Offset: 0x00000F23
		public static uint memoryBudgetKB
		{
			get
			{
				return AssetBundleLoadingCache.memoryBudgetKB;
			}
			set
			{
				AssetBundleLoadingCache.memoryBudgetKB = value;
			}
		}

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr returnMainAsset_Injected(IntPtr bundle);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromFileAsync_Internal_Injected(ref ManagedSpanWrapper path, uint crc, ulong offset);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromFile_Internal_Injected(ref ManagedSpanWrapper path, uint crc, ulong offset);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromMemoryAsync_Internal_Injected(ref ManagedSpanWrapper binary, uint crc);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromMemory_Internal_Injected(ref ManagedSpanWrapper binary, uint crc);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromStreamAsyncInternal_Injected(Stream stream, uint crc, uint managedReadBufferSize);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadFromStreamInternal_Injected(Stream stream, uint crc, uint managedReadBufferSize);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStreamedSceneAssetBundle_Injected(IntPtr _unity_self);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAsset_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAssetAsync_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Unload_Injected(IntPtr _unity_self, bool unloadAllLoadedObjects);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr UnloadAsync_Injected(IntPtr _unity_self, bool unloadAllLoadedObjects);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetAllAssetNames_Injected(IntPtr _unity_self);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetAllScenePaths_Injected(IntPtr _unity_self);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object[] LoadAssetWithSubAssets_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr LoadAssetWithSubAssetsAsync_Internal_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, Type type);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RecompressAssetBundleAsync_Internal_Injected(ref ManagedSpanWrapper inputPath, ref ManagedSpanWrapper outputPath, [In] ref BuildCompression method, uint expectedCRC, ThreadPriority priority);
	}
}
