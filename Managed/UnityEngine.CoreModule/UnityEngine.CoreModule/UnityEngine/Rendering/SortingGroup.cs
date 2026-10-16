using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000477 RID: 1143
	[NativeType(Header = "Runtime/2D/Sorting/SortingGroup.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class SortingGroup : Behaviour
	{
		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06002CFA RID: 11514
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static extern int invalidSortingGroupID
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06002CFB RID: 11515
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateAllSortingGroups();

		// Token: 0x06002CFC RID: 11516 RVA: 0x00055224 File Offset: 0x00053424
		[StaticAccessor("SortingGroup", StaticAccessorType.DoubleColon)]
		internal static SortingGroup GetSortingGroupByIndex(int index)
		{
			return Unmarshal.UnmarshalUnityObject<SortingGroup>(SortingGroup.GetSortingGroupByIndex_Injected(index));
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0005523C File Offset: 0x0005343C
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x0005527C File Offset: 0x0005347C
		public unsafe string sortingLayerName
		{
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					SortingGroup.get_sortingLayerName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					SortingGroup.set_sortingLayerName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x000552E0 File Offset: 0x000534E0
		// (set) Token: 0x06002D00 RID: 11520 RVA: 0x00055304 File Offset: 0x00053504
		public int sortingLayerID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingLayerID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SortingGroup.set_sortingLayerID_Injected(intPtr, value);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06002D01 RID: 11521 RVA: 0x00055328 File Offset: 0x00053528
		// (set) Token: 0x06002D02 RID: 11522 RVA: 0x0005534C File Offset: 0x0005354C
		public int sortingOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingOrder_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SortingGroup.set_sortingOrder_Injected(intPtr, value);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x00055370 File Offset: 0x00053570
		// (set) Token: 0x06002D04 RID: 11524 RVA: 0x00055394 File Offset: 0x00053594
		public bool sortAtRoot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortAtRoot_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				SortingGroup.set_sortAtRoot_Injected(intPtr, value);
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06002D05 RID: 11525 RVA: 0x000553B8 File Offset: 0x000535B8
		internal int sortingGroupID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingGroupID_Injected(intPtr);
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000553DC File Offset: 0x000535DC
		internal int sortingGroupOrder
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingGroupOrder_Injected(intPtr);
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06002D07 RID: 11527 RVA: 0x00055400 File Offset: 0x00053600
		internal int index
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_index_Injected(intPtr);
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06002D08 RID: 11528 RVA: 0x00055424 File Offset: 0x00053624
		internal uint sortingKey
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<SortingGroup>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return SortingGroup.get_sortingKey_Injected(intPtr);
			}
		}

		// Token: 0x06002D0A RID: 11530
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetSortingGroupByIndex_Injected(int index);

		// Token: 0x06002D0B RID: 11531
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_sortingLayerName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x06002D0C RID: 11532
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x06002D0D RID: 11533
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingLayerID_Injected(IntPtr _unity_self);

		// Token: 0x06002D0E RID: 11534
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingLayerID_Injected(IntPtr _unity_self, int value);

		// Token: 0x06002D0F RID: 11535
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingOrder_Injected(IntPtr _unity_self);

		// Token: 0x06002D10 RID: 11536
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortingOrder_Injected(IntPtr _unity_self, int value);

		// Token: 0x06002D11 RID: 11537
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_sortAtRoot_Injected(IntPtr _unity_self);

		// Token: 0x06002D12 RID: 11538
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_sortAtRoot_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06002D13 RID: 11539
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingGroupID_Injected(IntPtr _unity_self);

		// Token: 0x06002D14 RID: 11540
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_sortingGroupOrder_Injected(IntPtr _unity_self);

		// Token: 0x06002D15 RID: 11541
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_index_Injected(IntPtr _unity_self);

		// Token: 0x06002D16 RID: 11542
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint get_sortingKey_Injected(IntPtr _unity_self);
	}
}
