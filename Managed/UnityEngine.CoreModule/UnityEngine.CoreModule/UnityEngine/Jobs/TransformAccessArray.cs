using System;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;

namespace UnityEngine.Jobs
{
	// Token: 0x02000309 RID: 777
	[NativeType(Header = "Runtime/Transform/ScriptBindings/TransformAccess.bindings.h", CodegenOptions = CodegenOptions.Custom)]
	public struct TransformAccessArray : IDisposable
	{
		// Token: 0x06002689 RID: 9865 RVA: 0x0004A4BE File Offset: 0x000486BE
		public TransformAccessArray(Transform[] transforms, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(transforms.Length, desiredJobCount, out this);
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x0600268A RID: 9866 RVA: 0x0004A4D9 File Offset: 0x000486D9
		public TransformAccessArray(int capacity, int desiredJobCount = -1)
		{
			TransformAccessArray.Allocate(capacity, desiredJobCount, out this);
		}

		// Token: 0x0600268B RID: 9867 RVA: 0x0004A4E5 File Offset: 0x000486E5
		public static void Allocate(int capacity, int desiredJobCount, out TransformAccessArray array)
		{
			array.m_TransformArray = TransformAccessArray.Create(capacity, desiredJobCount);
			UnsafeUtility.LeakRecord(array.m_TransformArray, LeakCategory.TransformAccessArray, 0);
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x0004A504 File Offset: 0x00048704
		public bool isCreated
		{
			get
			{
				return this.m_TransformArray != IntPtr.Zero;
			}
		}

		// Token: 0x0600268D RID: 9869 RVA: 0x0004A526 File Offset: 0x00048726
		public void Dispose()
		{
			UnsafeUtility.LeakErase(this.m_TransformArray, LeakCategory.TransformAccessArray);
			TransformAccessArray.DestroyTransformAccessArray(this.m_TransformArray);
			this.m_TransformArray = IntPtr.Zero;
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0004A550 File Offset: 0x00048750
		internal IntPtr GetTransformAccessArrayForSchedule()
		{
			return this.m_TransformArray;
		}

		// Token: 0x1700063E RID: 1598
		public Transform this[int index]
		{
			get
			{
				return TransformAccessArray.GetTransform(this.m_TransformArray, index);
			}
			set
			{
				TransformAccessArray.SetTransform(this.m_TransformArray, index, value);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x0004A598 File Offset: 0x00048798
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x0004A5B5 File Offset: 0x000487B5
		public int capacity
		{
			get
			{
				return TransformAccessArray.GetCapacity(this.m_TransformArray);
			}
			set
			{
				TransformAccessArray.SetCapacity(this.m_TransformArray, value);
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x0004A5C8 File Offset: 0x000487C8
		public int length
		{
			get
			{
				return TransformAccessArray.GetLength(this.m_TransformArray);
			}
		}

		// Token: 0x06002694 RID: 9876 RVA: 0x0004A5E5 File Offset: 0x000487E5
		public void Add(Transform transform)
		{
			TransformAccessArray.Add(this.m_TransformArray, transform);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x0004A5F5 File Offset: 0x000487F5
		public void Add(int instanceId)
		{
			TransformAccessArray.AddInstanceId(this.m_TransformArray, instanceId);
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x0004A605 File Offset: 0x00048805
		public void RemoveAtSwapBack(int index)
		{
			TransformAccessArray.RemoveAtSwapBack(this.m_TransformArray, index);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x0004A615 File Offset: 0x00048815
		public void SetTransforms(Transform[] transforms)
		{
			TransformAccessArray.SetTransforms(this.m_TransformArray, transforms);
		}

		// Token: 0x06002698 RID: 9880
		[NativeMethod(Name = "TransformAccessArrayBindings::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create(int capacity, int desiredJobCount);

		// Token: 0x06002699 RID: 9881
		[NativeMethod(Name = "DestroyTransformAccessArray", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyTransformAccessArray(IntPtr transformArray);

		// Token: 0x0600269A RID: 9882
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransforms", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransforms(IntPtr transformArrayIntPtr, Transform[] transforms);

		// Token: 0x0600269B RID: 9883 RVA: 0x0004A628 File Offset: 0x00048828
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransform", IsFreeFunction = true)]
		private static void Add(IntPtr transformArrayIntPtr, Transform transform)
		{
			TransformAccessArray.Add_Injected(transformArrayIntPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x0600269C RID: 9884
		[NativeMethod(Name = "TransformAccessArrayBindings::AddTransformInstanceId", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddInstanceId(IntPtr transformArrayIntPtr, int instanceId);

		// Token: 0x0600269D RID: 9885
		[NativeMethod(Name = "TransformAccessArrayBindings::RemoveAtSwapBack", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveAtSwapBack(IntPtr transformArrayIntPtr, int index);

		// Token: 0x0600269E RID: 9886
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedTransformAccess", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedTransformAccess(IntPtr transformArrayIntPtr);

		// Token: 0x0600269F RID: 9887
		[NativeMethod(Name = "TransformAccessArrayBindings::GetSortedToUserIndex", IsThreadSafe = true, IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetSortedToUserIndex(IntPtr transformArrayIntPtr);

		// Token: 0x060026A0 RID: 9888
		[NativeMethod(Name = "TransformAccessArrayBindings::GetLength", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetLength(IntPtr transformArrayIntPtr);

		// Token: 0x060026A1 RID: 9889
		[NativeMethod(Name = "TransformAccessArrayBindings::GetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetCapacity(IntPtr transformArrayIntPtr);

		// Token: 0x060026A2 RID: 9890
		[NativeMethod(Name = "TransformAccessArrayBindings::SetCapacity", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetCapacity(IntPtr transformArrayIntPtr, int capacity);

		// Token: 0x060026A3 RID: 9891 RVA: 0x0004A644 File Offset: 0x00048844
		[NativeMethod(Name = "TransformAccessArrayBindings::GetTransform", IsFreeFunction = true, ThrowsException = true)]
		internal static Transform GetTransform(IntPtr transformArrayIntPtr, int index)
		{
			return Unmarshal.UnmarshalUnityObject<Transform>(TransformAccessArray.GetTransform_Injected(transformArrayIntPtr, index));
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x0004A660 File Offset: 0x00048860
		[NativeMethod(Name = "TransformAccessArrayBindings::SetTransform", IsFreeFunction = true, ThrowsException = true)]
		internal static void SetTransform(IntPtr transformArrayIntPtr, int index, Transform transform)
		{
			TransformAccessArray.SetTransform_Injected(transformArrayIntPtr, index, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x060026A5 RID: 9893
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Add_Injected(IntPtr transformArrayIntPtr, IntPtr transform);

		// Token: 0x060026A6 RID: 9894
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTransform_Injected(IntPtr transformArrayIntPtr, int index);

		// Token: 0x060026A7 RID: 9895
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTransform_Injected(IntPtr transformArrayIntPtr, int index, IntPtr transform);

		// Token: 0x04000AC4 RID: 2756
		private IntPtr m_TransformArray;
	}
}
