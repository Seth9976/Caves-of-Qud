using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000221 RID: 545
	[StaticAccessor("GetObjectDispatcher()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Misc/ObjectDispatcher.h")]
	internal sealed class ObjectDispatcher : IDisposable
	{
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x0003924C File Offset: 0x0003744C
		public bool valid
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x00039270 File Offset: 0x00037470
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x00039294 File Offset: 0x00037494
		public int maxDispatchHistoryFramesCount
		{
			get
			{
				this.ValidateSystemHandleAndThrow();
				return ObjectDispatcher.GetMaxDispatchHistoryFramesCount(this.m_Ptr);
			}
			set
			{
				this.ValidateSystemHandleAndThrow();
				ObjectDispatcher.SetMaxDispatchHistoryFramesCount(this.m_Ptr, value);
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000392AC File Offset: 0x000374AC
		public ObjectDispatcher()
		{
			this.m_Ptr = ObjectDispatcher.CreateDispatchSystemHandle();
			this.m_TypeDataCallback = new Action<TypeDispatchData>(this.DispatchCallback);
			this.m_TransformDataCallback = new Action<TransformDispatchData>(this.DispatchCallback);
			this.m_TransformComponentCallback = new Action<Component[]>(this.DispatchCallback);
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x00039310 File Offset: 0x00037510
		~ObjectDispatcher()
		{
			this.Dispose(false);
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x00039344 File Offset: 0x00037544
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00039358 File Offset: 0x00037558
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				ObjectDispatcher.DestroyDispatchSystemHandle(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00039394 File Offset: 0x00037594
		private void ValidateSystemHandleAndThrow()
		{
			bool flag = !this.valid;
			if (flag)
			{
				throw new Exception("The ObjectDispatcher is invalid or has been disposed.");
			}
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000393BC File Offset: 0x000375BC
		private void ValidateTypeAndThrow(Type type)
		{
			bool flag = !type.IsSubclassOf(typeof(Object));
			if (flag)
			{
				throw new Exception("Only types inherited from UnityEngine.Object are supported.");
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x000393EC File Offset: 0x000375EC
		private void ValidateComponentTypeAndThrow(Type type)
		{
			bool flag = !type.IsSubclassOf(typeof(Component));
			if (flag)
			{
				throw new Exception("Only types inherited from UnityEngine.Component are supported.");
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0003941C File Offset: 0x0003761C
		private void DispatchCallback(TypeDispatchData data)
		{
			this.m_TypeDispatchData = default(TypeDispatchData);
			this.m_TypeDispatchData.changed = data.changed;
			this.m_TypeDispatchData.changedID = new NativeArray<int>(data.changedID, this.m_DispatchAllocator);
			this.m_TypeDispatchData.destroyedID = new NativeArray<int>(data.destroyedID, this.m_DispatchAllocator);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x00039480 File Offset: 0x00037680
		private void DispatchCallback(TransformDispatchData data)
		{
			this.m_TransformDispatchData = default(TransformDispatchData);
			this.m_TransformDispatchData.transformedID = new NativeArray<int>(data.transformedID, this.m_DispatchAllocator);
			this.m_TransformDispatchData.parentID = new NativeArray<int>(data.parentID, this.m_DispatchAllocator);
			this.m_TransformDispatchData.localToWorldMatrices = new NativeArray<Matrix4x4>(data.localToWorldMatrices, this.m_DispatchAllocator);
			this.m_TransformDispatchData.positions = new NativeArray<Vector3>(data.positions, this.m_DispatchAllocator);
			this.m_TransformDispatchData.rotations = new NativeArray<Quaternion>(data.rotations, this.m_DispatchAllocator);
			this.m_TransformDispatchData.scales = new NativeArray<Vector3>(data.scales, this.m_DispatchAllocator);
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00039542 File Offset: 0x00037742
		private void DispatchCallback(Component[] components)
		{
			this.m_TransformedComponents = components;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0003954C File Offset: 0x0003774C
		public void DispatchTypeChangesAndClear(Type type, Action<TypeDispatchData> callback, bool sortByInstanceID = false, bool noScriptingArray = false)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateTypeAndThrow(type);
			ObjectDispatcher.DispatchTypeChangesAndClear(this.m_Ptr, type, ObjectDispatcher.s_TypeDispatch, sortByInstanceID, noScriptingArray, callback);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x00039574 File Offset: 0x00037774
		public void DispatchTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<Component[]> callback, bool sortByInstanceID = false)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateComponentTypeAndThrow(type);
			ObjectDispatcher.DispatchTransformChangesAndClear(this.m_Ptr, type, trackingType, callback, sortByInstanceID);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00039597 File Offset: 0x00037797
		public void DispatchTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<TransformDispatchData> callback)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateComponentTypeAndThrow(type);
			ObjectDispatcher.DispatchTransformDataChangesAndClear(this.m_Ptr, type, trackingType, ObjectDispatcher.s_TransformDispatch, callback);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000395BD File Offset: 0x000377BD
		public void ClearTypeChanges(Type type)
		{
			this.ValidateSystemHandleAndThrow();
			this.ValidateTypeAndThrow(type);
			ObjectDispatcher.DispatchTypeChangesAndClear(this.m_Ptr, type, null, false, false, null);
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x000395E0 File Offset: 0x000377E0
		public TypeDispatchData GetTypeChangesAndClear(Type type, Allocator allocator, bool sortByInstanceID = false, bool noScriptingArray = false)
		{
			this.m_DispatchAllocator = allocator;
			this.DispatchTypeChangesAndClear(type, this.m_TypeDataCallback, sortByInstanceID, noScriptingArray);
			return this.m_TypeDispatchData;
		}

		// Token: 0x06001DD9 RID: 7641 RVA: 0x00039610 File Offset: 0x00037810
		public void GetTypeChangesAndClear(Type type, List<Object> changed, out NativeArray<int> changedID, out NativeArray<int> destroyedID, Allocator allocator, bool sortByInstanceID = false)
		{
			this.m_DispatchAllocator = allocator;
			this.DispatchTypeChangesAndClear(type, this.m_TypeDataCallback, sortByInstanceID, true);
			changedID = this.m_TypeDispatchData.changedID;
			destroyedID = this.m_TypeDispatchData.destroyedID;
			Resources.InstanceIDToObjectList(this.m_TypeDispatchData.changedID, changed);
		}

		// Token: 0x06001DDA RID: 7642 RVA: 0x0003966C File Offset: 0x0003786C
		public Component[] GetTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, bool sortByInstanceID = false)
		{
			this.DispatchTransformChangesAndClear(type, trackingType, this.m_TransformComponentCallback, sortByInstanceID);
			return this.m_TransformedComponents;
		}

		// Token: 0x06001DDB RID: 7643 RVA: 0x00039694 File Offset: 0x00037894
		public TransformDispatchData GetTransformChangesAndClear(Type type, ObjectDispatcher.TransformTrackingType trackingType, Allocator allocator)
		{
			this.m_DispatchAllocator = allocator;
			this.DispatchTransformChangesAndClear(type, trackingType, this.m_TransformDataCallback);
			return this.m_TransformDispatchData;
		}

		// Token: 0x06001DDC RID: 7644 RVA: 0x000396C4 File Offset: 0x000378C4
		public void EnableTypeTracking(ObjectDispatcher.TypeTrackingFlags typeTrackingMask, params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateTypeAndThrow(type);
				ObjectDispatcher.EnableTypeTracking(this.m_Ptr, type, typeTrackingMask);
			}
		}

		// Token: 0x06001DDD RID: 7645 RVA: 0x00039706 File Offset: 0x00037906
		public void EnableTypeTracking(params Type[] types)
		{
			this.EnableTypeTracking(ObjectDispatcher.TypeTrackingFlags.Default, types);
		}

		// Token: 0x06001DDE RID: 7646 RVA: 0x00039706 File Offset: 0x00037906
		[Obsolete("EnableTypeTrackingIncludingAssets is deprecated, please use EnableTypeTracking and provide the flag that specifies whether you need assets or not.", false)]
		public void EnableTypeTrackingIncludingAssets(params Type[] types)
		{
			this.EnableTypeTracking(ObjectDispatcher.TypeTrackingFlags.Default, types);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x00039714 File Offset: 0x00037914
		public void DisableTypeTracking(params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateTypeAndThrow(type);
				ObjectDispatcher.DisableTypeTracking(this.m_Ptr, type);
			}
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x00039758 File Offset: 0x00037958
		public void EnableTransformTracking(ObjectDispatcher.TransformTrackingType trackingType, params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateComponentTypeAndThrow(type);
				ObjectDispatcher.EnableTransformTracking(this.m_Ptr, type, trackingType);
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x0003979C File Offset: 0x0003799C
		public void DisableTransformTracking(ObjectDispatcher.TransformTrackingType trackingType, params Type[] types)
		{
			this.ValidateSystemHandleAndThrow();
			foreach (Type type in types)
			{
				this.ValidateComponentTypeAndThrow(type);
				ObjectDispatcher.DisableTransformTracking(this.m_Ptr, type, trackingType);
			}
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x000397DE File Offset: 0x000379DE
		public void DispatchTypeChangesAndClear<T>(Action<TypeDispatchData> callback, bool sortByInstanceID = false, bool noScriptingArray = false) where T : Object
		{
			this.DispatchTypeChangesAndClear(typeof(T), callback, sortByInstanceID, noScriptingArray);
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x000397F5 File Offset: 0x000379F5
		public void DispatchTransformChangesAndClear<T>(ObjectDispatcher.TransformTrackingType trackingType, Action<Component[]> callback, bool sortByInstanceID = false) where T : Object
		{
			this.DispatchTransformChangesAndClear(typeof(T), trackingType, callback, sortByInstanceID);
		}

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0003980C File Offset: 0x00037A0C
		public void DispatchTransformChangesAndClear<T>(ObjectDispatcher.TransformTrackingType trackingType, Action<TransformDispatchData> callback) where T : Object
		{
			this.DispatchTransformChangesAndClear(typeof(T), trackingType, callback);
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x00039822 File Offset: 0x00037A22
		public void ClearTypeChanges<T>() where T : Object
		{
			this.ClearTypeChanges(typeof(T));
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x00039838 File Offset: 0x00037A38
		public TypeDispatchData GetTypeChangesAndClear<T>(Allocator allocator, bool sortByInstanceID = false, bool noScriptingArray = false) where T : Object
		{
			return this.GetTypeChangesAndClear(typeof(T), allocator, sortByInstanceID, noScriptingArray);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0003985D File Offset: 0x00037A5D
		public void GetTypeChangesAndClear<T>(List<Object> changed, out NativeArray<int> changedID, out NativeArray<int> destroyedID, Allocator allocator, bool sortByInstanceID = false) where T : Object
		{
			this.GetTypeChangesAndClear(typeof(T), changed, out changedID, out destroyedID, allocator, sortByInstanceID);
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x00039878 File Offset: 0x00037A78
		public Component[] GetTransformChangesAndClear<T>(ObjectDispatcher.TransformTrackingType trackingType, bool sortByInstanceID = false) where T : Object
		{
			return this.GetTransformChangesAndClear(typeof(T), trackingType, sortByInstanceID);
		}

		// Token: 0x06001DE9 RID: 7657 RVA: 0x0003989C File Offset: 0x00037A9C
		public TransformDispatchData GetTransformChangesAndClear<T>(ObjectDispatcher.TransformTrackingType trackingType, Allocator allocator) where T : Object
		{
			return this.GetTransformChangesAndClear(typeof(T), trackingType, allocator);
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x000398C0 File Offset: 0x00037AC0
		public void EnableTypeTracking<T>(ObjectDispatcher.TypeTrackingFlags typeTrackingMask = ObjectDispatcher.TypeTrackingFlags.Default) where T : Object
		{
			this.EnableTypeTracking(typeTrackingMask, new Type[] { typeof(T) });
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x000398DE File Offset: 0x00037ADE
		public void DisableTypeTracking<T>() where T : Object
		{
			this.DisableTypeTracking(new Type[] { typeof(T) });
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x000398FB File Offset: 0x00037AFB
		public void EnableTransformTracking<T>(ObjectDispatcher.TransformTrackingType trackingType) where T : Object
		{
			this.EnableTransformTracking(trackingType, new Type[] { typeof(T) });
		}

		// Token: 0x06001DED RID: 7661 RVA: 0x00039919 File Offset: 0x00037B19
		public void DisableTransformTracking<T>(ObjectDispatcher.TransformTrackingType trackingType) where T : Object
		{
			this.DisableTransformTracking(trackingType, new Type[] { typeof(T) });
		}

		// Token: 0x06001DEE RID: 7662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateDispatchSystemHandle();

		// Token: 0x06001DEF RID: 7663
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyDispatchSystemHandle(IntPtr ptr);

		// Token: 0x06001DF0 RID: 7664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMaxDispatchHistoryFramesCount(IntPtr ptr);

		// Token: 0x06001DF1 RID: 7665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMaxDispatchHistoryFramesCount(IntPtr ptr, int count);

		// Token: 0x06001DF2 RID: 7666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableTypeTracking(IntPtr ptr, Type type, ObjectDispatcher.TypeTrackingFlags typeTrackingMask);

		// Token: 0x06001DF3 RID: 7667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableTypeTracking(IntPtr ptr, Type type);

		// Token: 0x06001DF4 RID: 7668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableTransformTracking(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType);

		// Token: 0x06001DF5 RID: 7669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableTransformTracking(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType);

		// Token: 0x06001DF6 RID: 7670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchTypeChangesAndClear(IntPtr ptr, Type type, Action<Object[], IntPtr, IntPtr, int, int, Action<TypeDispatchData>> callback, bool sortByInstanceID, bool noScriptingArray, Action<TypeDispatchData> param);

		// Token: 0x06001DF7 RID: 7671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchTransformDataChangesAndClear(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, int, Action<TransformDispatchData>> callback, Action<TransformDispatchData> param);

		// Token: 0x06001DF8 RID: 7672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchTransformChangesAndClear(IntPtr ptr, Type type, ObjectDispatcher.TransformTrackingType trackingType, Action<Component[]> callback, bool sortByInstanceID);

		// Token: 0x040008DF RID: 2271
		private IntPtr m_Ptr = IntPtr.Zero;

		// Token: 0x040008E0 RID: 2272
		private Allocator m_DispatchAllocator;

		// Token: 0x040008E1 RID: 2273
		private TypeDispatchData m_TypeDispatchData;

		// Token: 0x040008E2 RID: 2274
		private TransformDispatchData m_TransformDispatchData;

		// Token: 0x040008E3 RID: 2275
		private Component[] m_TransformedComponents;

		// Token: 0x040008E4 RID: 2276
		private Action<TypeDispatchData> m_TypeDataCallback;

		// Token: 0x040008E5 RID: 2277
		private Action<TransformDispatchData> m_TransformDataCallback;

		// Token: 0x040008E6 RID: 2278
		private Action<Component[]> m_TransformComponentCallback;

		// Token: 0x040008E7 RID: 2279
		private static Action<Object[], IntPtr, IntPtr, int, int, Action<TypeDispatchData>> s_TypeDispatch = delegate(Object[] changed, IntPtr changedID, IntPtr destroyedID, int changedCount, int destroyedCount, Action<TypeDispatchData> callback)
		{
			NativeArray<int> nativeArray = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(changedID.ToPointer(), changedCount, Allocator.Invalid);
			NativeArray<int> nativeArray2 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(destroyedID.ToPointer(), destroyedCount, Allocator.Invalid);
			TypeDispatchData typeDispatchData = new TypeDispatchData
			{
				changed = changed,
				changedID = nativeArray,
				destroyedID = nativeArray2
			};
			callback(typeDispatchData);
		};

		// Token: 0x040008E8 RID: 2280
		private static Action<IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, IntPtr, int, Action<TransformDispatchData>> s_TransformDispatch = delegate(IntPtr transformed, IntPtr parents, IntPtr localToWorldMatrices, IntPtr positions, IntPtr rotations, IntPtr scales, int count, Action<TransformDispatchData> callback)
		{
			NativeArray<int> nativeArray3 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(transformed.ToPointer(), count, Allocator.Invalid);
			NativeArray<int> nativeArray4 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<int>(parents.ToPointer(), (parents != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Matrix4x4> nativeArray5 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>(localToWorldMatrices.ToPointer(), (localToWorldMatrices != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Vector3> nativeArray6 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(positions.ToPointer(), (positions != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Quaternion> nativeArray7 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Quaternion>(rotations.ToPointer(), (rotations != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			NativeArray<Vector3> nativeArray8 = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Vector3>(scales.ToPointer(), (scales != IntPtr.Zero) ? count : 0, Allocator.Invalid);
			TransformDispatchData transformDispatchData = new TransformDispatchData
			{
				transformedID = nativeArray3,
				parentID = nativeArray4,
				localToWorldMatrices = nativeArray5,
				positions = nativeArray6,
				rotations = nativeArray7,
				scales = nativeArray8
			};
			callback(transformDispatchData);
		};

		// Token: 0x02000222 RID: 546
		public enum TransformTrackingType
		{
			// Token: 0x040008EA RID: 2282
			GlobalTRS,
			// Token: 0x040008EB RID: 2283
			LocalTRS,
			// Token: 0x040008EC RID: 2284
			Hierarchy
		}

		// Token: 0x02000223 RID: 547
		[Flags]
		public enum TypeTrackingFlags
		{
			// Token: 0x040008EE RID: 2286
			SceneObjects = 1,
			// Token: 0x040008EF RID: 2287
			Assets = 2,
			// Token: 0x040008F0 RID: 2288
			EditorOnlyObjects = 4,
			// Token: 0x040008F1 RID: 2289
			Default = 3,
			// Token: 0x040008F2 RID: 2290
			All = 7
		}
	}
}
