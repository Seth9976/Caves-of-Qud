using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.SceneManagement
{
	// Token: 0x0200037B RID: 891
	[NativeHeader("Runtime/Export/SceneManager/Scene.bindings.h")]
	[Serializable]
	public struct Scene
	{
		// Token: 0x060028E7 RID: 10471
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValidInternal(int sceneHandle);

		// Token: 0x060028E8 RID: 10472 RVA: 0x0004EED4 File Offset: 0x0004D0D4
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static string GetPathInternal(int sceneHandle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Scene.GetPathInternal_Injected(sceneHandle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060028E9 RID: 10473 RVA: 0x0004EF04 File Offset: 0x0004D104
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private unsafe static void SetPathAndGUIDInternal(int sceneHandle, string path, string guid)
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
				ManagedSpanWrapper managedSpanWrapper2;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(guid, ref managedSpanWrapper2))
				{
					ReadOnlySpan<char> readOnlySpan2 = guid.AsSpan();
					fixed (char* ptr2 = readOnlySpan2.GetPinnableReference())
					{
						managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, readOnlySpan2.Length);
					}
				}
				Scene.SetPathAndGUIDInternal_Injected(sceneHandle, ref managedSpanWrapper, ref managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x060028EA RID: 10474 RVA: 0x0004EF8C File Offset: 0x0004D18C
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static string GetNameInternal(int sceneHandle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Scene.GetNameInternal_Injected(sceneHandle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060028EB RID: 10475 RVA: 0x0004EFBC File Offset: 0x0004D1BC
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private unsafe static void SetNameInternal(int sceneHandle, string name)
		{
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
				Scene.SetNameInternal_Injected(sceneHandle, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060028EC RID: 10476 RVA: 0x0004F014 File Offset: 0x0004D214
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		private static string GetGUIDInternal(int sceneHandle)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Scene.GetGUIDInternal_Injected(sceneHandle, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060028ED RID: 10477
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSubScene(int sceneHandle);

		// Token: 0x060028EE RID: 10478
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIsSubScene(int sceneHandle, bool value);

		// Token: 0x060028EF RID: 10479
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsLoadedInternal(int sceneHandle);

		// Token: 0x060028F0 RID: 10480
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Scene.LoadingState GetLoadingStateInternal(int sceneHandle);

		// Token: 0x060028F1 RID: 10481
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetIsDirtyInternal(int sceneHandle);

		// Token: 0x060028F2 RID: 10482
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDirtyID(int sceneHandle);

		// Token: 0x060028F3 RID: 10483
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBuildIndexInternal(int sceneHandle);

		// Token: 0x060028F4 RID: 10484
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRootCountInternal(int sceneHandle);

		// Token: 0x060028F5 RID: 10485
		[StaticAccessor("SceneBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetRootGameObjectsInternal(int sceneHandle, object resultRootList);

		// Token: 0x060028F6 RID: 10486 RVA: 0x0004F044 File Offset: 0x0004D244
		internal Scene(int handle)
		{
			this.m_Handle = handle;
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x0004F050 File Offset: 0x0004D250
		public int handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060028F8 RID: 10488 RVA: 0x0004F068 File Offset: 0x0004D268
		internal Scene.LoadingState loadingState
		{
			get
			{
				return Scene.GetLoadingStateInternal(this.handle);
			}
		}

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x0004F088 File Offset: 0x0004D288
		internal string guid
		{
			get
			{
				return Scene.GetGUIDInternal(this.handle);
			}
		}

		// Token: 0x060028FA RID: 10490 RVA: 0x0004F0A8 File Offset: 0x0004D2A8
		public bool IsValid()
		{
			return Scene.IsValidInternal(this.handle);
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x0004F0C8 File Offset: 0x0004D2C8
		public string path
		{
			get
			{
				return Scene.GetPathInternal(this.handle);
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060028FC RID: 10492 RVA: 0x0004F0E8 File Offset: 0x0004D2E8
		// (set) Token: 0x060028FD RID: 10493 RVA: 0x0004F105 File Offset: 0x0004D305
		public string name
		{
			get
			{
				return Scene.GetNameInternal(this.handle);
			}
			set
			{
				Scene.SetNameInternal(this.handle, value);
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060028FE RID: 10494 RVA: 0x0004F118 File Offset: 0x0004D318
		public bool isLoaded
		{
			get
			{
				return Scene.GetIsLoadedInternal(this.handle);
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x0004F138 File Offset: 0x0004D338
		public int buildIndex
		{
			get
			{
				return Scene.GetBuildIndexInternal(this.handle);
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x0004F158 File Offset: 0x0004D358
		public bool isDirty
		{
			get
			{
				return Scene.GetIsDirtyInternal(this.handle);
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x0004F178 File Offset: 0x0004D378
		internal int dirtyID
		{
			get
			{
				return Scene.GetDirtyID(this.handle);
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06002902 RID: 10498 RVA: 0x0004F198 File Offset: 0x0004D398
		public int rootCount
		{
			get
			{
				return Scene.GetRootCountInternal(this.handle);
			}
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x0004F1B8 File Offset: 0x0004D3B8
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x0004F1D5 File Offset: 0x0004D3D5
		public bool isSubScene
		{
			get
			{
				return Scene.IsSubScene(this.handle);
			}
			set
			{
				Scene.SetIsSubScene(this.handle, value);
			}
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0004F1E8 File Offset: 0x0004D3E8
		public GameObject[] GetRootGameObjects()
		{
			List<GameObject> list = new List<GameObject>(this.rootCount);
			this.GetRootGameObjects(list);
			return list.ToArray();
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0004F214 File Offset: 0x0004D414
		public void GetRootGameObjects(List<GameObject> rootGameObjects)
		{
			bool flag = rootGameObjects.Capacity < this.rootCount;
			if (flag)
			{
				rootGameObjects.Capacity = this.rootCount;
			}
			rootGameObjects.Clear();
			bool flag2 = !this.IsValid();
			if (flag2)
			{
				throw new ArgumentException("The scene is invalid.");
			}
			bool flag3 = !Application.isPlaying && !this.isLoaded;
			if (flag3)
			{
				throw new ArgumentException("The scene is not loaded.");
			}
			bool flag4 = this.rootCount == 0;
			if (!flag4)
			{
				Scene.GetRootGameObjectsInternal(this.handle, rootGameObjects);
			}
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0004F2A0 File Offset: 0x0004D4A0
		public static bool operator ==(Scene lhs, Scene rhs)
		{
			return lhs.handle == rhs.handle;
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0004F2C4 File Offset: 0x0004D4C4
		public static bool operator !=(Scene lhs, Scene rhs)
		{
			return lhs.handle != rhs.handle;
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0004F2EC File Offset: 0x0004D4EC
		public override int GetHashCode()
		{
			return this.m_Handle;
		}

		// Token: 0x0600290A RID: 10506 RVA: 0x0004F304 File Offset: 0x0004D504
		public override bool Equals(object other)
		{
			bool flag = !(other is Scene);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				Scene scene = (Scene)other;
				flag2 = this.handle == scene.handle;
			}
			return flag2;
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0004F340 File Offset: 0x0004D540
		internal void SetPathAndGuid(string path, string guid)
		{
			Scene.SetPathAndGUIDInternal(this.m_Handle, path, guid);
		}

		// Token: 0x0600290C RID: 10508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPathInternal_Injected(int sceneHandle, out ManagedSpanWrapper ret);

		// Token: 0x0600290D RID: 10509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetPathAndGUIDInternal_Injected(int sceneHandle, ref ManagedSpanWrapper path, ref ManagedSpanWrapper guid);

		// Token: 0x0600290E RID: 10510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNameInternal_Injected(int sceneHandle, out ManagedSpanWrapper ret);

		// Token: 0x0600290F RID: 10511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNameInternal_Injected(int sceneHandle, ref ManagedSpanWrapper name);

		// Token: 0x06002910 RID: 10512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGUIDInternal_Injected(int sceneHandle, out ManagedSpanWrapper ret);

		// Token: 0x04000B9E RID: 2974
		[HideInInspector]
		[SerializeField]
		private int m_Handle;

		// Token: 0x0200037C RID: 892
		internal enum LoadingState
		{
			// Token: 0x04000BA0 RID: 2976
			NotLoaded,
			// Token: 0x04000BA1 RID: 2977
			Loading,
			// Token: 0x04000BA2 RID: 2978
			Loaded,
			// Token: 0x04000BA3 RID: 2979
			Unloading
		}
	}
}
