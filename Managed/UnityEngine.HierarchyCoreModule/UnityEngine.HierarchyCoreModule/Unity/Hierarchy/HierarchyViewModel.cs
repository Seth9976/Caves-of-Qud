using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.Hierarchy
{
	// Token: 0x02000034 RID: 52
	[NativeHeader("Modules/HierarchyCore/Public/HierarchyViewModel.h")]
	[NativeHeader("Modules/HierarchyCore/HierarchyViewModelBindings.h")]
	[RequiredByNativeCode(GenerateProxy = true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class HierarchyViewModel : IDisposable
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00006565 File Offset: 0x00004765
		public bool IsCreated
		{
			get
			{
				return this.m_Ptr != IntPtr.Zero;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00006577 File Offset: 0x00004777
		public int Count
		{
			get
			{
				return this.m_NodesCount;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00006580 File Offset: 0x00004780
		public bool Updating
		{
			[NativeMethod("Updating", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_Updating_Injected(intPtr);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000065A4 File Offset: 0x000047A4
		public bool UpdateNeeded
		{
			[NativeMethod("UpdateNeeded", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_UpdateNeeded_Injected(intPtr);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000065C6 File Offset: 0x000047C6
		public HierarchyFlattened HierarchyFlattened
		{
			get
			{
				return this.m_HierarchyFlattened;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000065CE File Offset: 0x000047CE
		public Hierarchy Hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000065D6 File Offset: 0x000047D6
		internal unsafe int* NodesPtr
		{
			get
			{
				return (int*)(void*)this.m_NodesPtr;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000065E3 File Offset: 0x000047E3
		internal int Version
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600021F RID: 543 RVA: 0x000065EC File Offset: 0x000047EC
		internal float UpdateProgress
		{
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			[NativeMethod("UpdateProgress", IsThreadSafe = true)]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_UpdateProgress_Injected(intPtr);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000660E File Offset: 0x0000480E
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00006616 File Offset: 0x00004816
		internal IHierarchySearchQueryParser QueryParser
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
			get;
			[VisibleToOtherModules(new string[] { "UnityEditor.HierarchyModule" })]
			set;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006620 File Offset: 0x00004820
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00006644 File Offset: 0x00004844
		internal HierarchySearchQueryDescriptor Query
		{
			[NativeMethod(IsThreadSafe = true)]
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			get
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return HierarchyViewModel.get_Query_Injected(intPtr);
			}
			[VisibleToOtherModules(new string[] { "UnityEngine.HierarchyModule" })]
			[NativeMethod(IsThreadSafe = true)]
			set
			{
				IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				HierarchyViewModel.set_Query_Injected(intPtr, value);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00006668 File Offset: 0x00004868
		public HierarchyViewModel(HierarchyFlattened hierarchyFlattened, HierarchyNodeFlags defaultFlags = HierarchyNodeFlags.None)
		{
			IntPtr intPtr;
			int num;
			int num2;
			this.m_Ptr = HierarchyViewModel.Create(GCHandle.ToIntPtr(GCHandle.Alloc(this)), hierarchyFlattened, defaultFlags, out intPtr, out num, out num2);
			this.m_Hierarchy = hierarchyFlattened.Hierarchy;
			this.m_HierarchyFlattened = hierarchyFlattened;
			this.m_NodesPtr = intPtr;
			this.m_NodesCount = num;
			this.m_Version = num2;
			this.m_IsOwner = true;
			this.QueryParser = new DefaultHierarchySearchQueryParser();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000066D8 File Offset: 0x000048D8
		private HierarchyViewModel(IntPtr nativePtr, HierarchyFlattened hierarchyFlattened, IntPtr nodesPtr, int nodesCount, int version)
		{
			this.m_Ptr = nativePtr;
			this.m_Hierarchy = hierarchyFlattened.Hierarchy;
			this.m_HierarchyFlattened = hierarchyFlattened;
			this.m_NodesPtr = nodesPtr;
			this.m_NodesCount = nodesCount;
			this.m_Version = version;
			this.m_IsOwner = false;
			this.QueryParser = new DefaultHierarchySearchQueryParser();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00006734 File Offset: 0x00004934
		~HierarchyViewModel()
		{
			this.Dispose(false);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00006768 File Offset: 0x00004968
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000677C File Offset: 0x0000497C
		private void Dispose(bool disposing)
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				bool isOwner = this.m_IsOwner;
				if (isOwner)
				{
					HierarchyViewModel.Destroy(this.m_Ptr);
				}
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x17000059 RID: 89
		public unsafe ref HierarchyNode this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				bool flag = index < 0 || index >= this.m_NodesCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return HierarchyFlattenedNode.GetNodeByRef(this.m_HierarchyFlattened[*(int*)((byte*)(void*)this.m_NodesPtr + (IntPtr)index * 4)]);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000681C File Offset: 0x00004A1C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int IndexOf(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.IndexOf_Injected(intPtr, in node);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006840 File Offset: 0x00004A40
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public bool Contains(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.Contains_Injected(intPtr, in node);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006864 File Offset: 0x00004A64
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetParent(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			HierarchyViewModel.GetParent_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000688C File Offset: 0x00004A8C
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNode GetNextSibling(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyNode hierarchyNode;
			HierarchyViewModel.GetNextSibling_Injected(intPtr, in node, out hierarchyNode);
			return hierarchyNode;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000068B4 File Offset: 0x00004AB4
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCount(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.GetChildrenCount_Injected(intPtr, in node);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000068D8 File Offset: 0x00004AD8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetChildrenCountRecursive(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.GetChildrenCountRecursive_Injected(intPtr, in node);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000068FC File Offset: 0x00004AFC
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int GetDepth(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.GetDepth_Injected(intPtr, in node);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006920 File Offset: 0x00004B20
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public HierarchyNodeFlags GetFlags(in HierarchyNode node)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.GetFlags_Injected(intPtr, in node);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006943 File Offset: 0x00004B43
		public void SetFlags(HierarchyNodeFlags flags)
		{
			this.SetFlagsAll(flags);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000694D File Offset: 0x00004B4D
		public void SetFlags(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			this.SetFlagsNode(in node, flags, recurse);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00006959 File Offset: 0x00004B59
		public int SetFlags(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			return this.SetFlagsNodes(nodes, flags);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00006963 File Offset: 0x00004B63
		public int SetFlags(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			return this.SetFlagsIndices(indices, flags);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000696D File Offset: 0x00004B6D
		public bool HasAllFlags(HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsAny(flags);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00006976 File Offset: 0x00004B76
		public bool HasAnyFlags(HierarchyNodeFlags flags)
		{
			return this.HasAnyFlagsAny(flags);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000697F File Offset: 0x00004B7F
		public bool HasAllFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsNode(in node, flags);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00006989 File Offset: 0x00004B89
		public bool HasAnyFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.HasAnyFlagsNode(in node, flags);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00006994 File Offset: 0x00004B94
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int HasAllFlagsCount(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAllFlagsCount_Injected(intPtr, flags);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000069B8 File Offset: 0x00004BB8
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int HasAnyFlagsCount(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAnyFlagsCount_Injected(intPtr, flags);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000069DB File Offset: 0x00004BDB
		public bool DoesNotHaveAllFlags(HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAllFlagsAny(flags);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000069E4 File Offset: 0x00004BE4
		public bool DoesNotHaveAnyFlags(HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAnyFlagsAny(flags);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000069ED File Offset: 0x00004BED
		public bool DoesNotHaveAllFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAllFlagsNode(in node, flags);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000069F7 File Offset: 0x00004BF7
		public bool DoesNotHaveAnyFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAnyFlagsNode(in node, flags);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00006A04 File Offset: 0x00004C04
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int DoesNotHaveAllFlagsCount(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAllFlagsCount_Injected(intPtr, flags);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00006A28 File Offset: 0x00004C28
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		public int DoesNotHaveAnyFlagsCount(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAnyFlagsCount_Injected(intPtr, flags);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00006A4B File Offset: 0x00004C4B
		public void ClearFlags(HierarchyNodeFlags flags)
		{
			this.ClearFlagsAll(flags);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00006A55 File Offset: 0x00004C55
		public void ClearFlags(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			this.ClearFlagsNode(in node, flags, recurse);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006A61 File Offset: 0x00004C61
		public int ClearFlags(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			return this.ClearFlagsNodes(nodes, flags);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006A6B File Offset: 0x00004C6B
		public int ClearFlags(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			return this.ClearFlagsIndices(indices, flags);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006A75 File Offset: 0x00004C75
		public void ToggleFlags(HierarchyNodeFlags flags)
		{
			this.ToggleFlagsAll(flags);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006A7F File Offset: 0x00004C7F
		public void ToggleFlags(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			this.ToggleFlagsNode(in node, flags, recurse);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006A8B File Offset: 0x00004C8B
		public int ToggleFlags(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			return this.ToggleFlagsNodes(nodes, flags);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00006A95 File Offset: 0x00004C95
		public int ToggleFlags(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			return this.ToggleFlagsIndices(indices, flags);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00006A9F File Offset: 0x00004C9F
		public int GetNodesWithAllFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithAllFlagsSpan(flags, outNodes);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00006AA9 File Offset: 0x00004CA9
		public int GetNodesWithAnyFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithAnyFlagsSpan(flags, outNodes);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public HierarchyNode[] GetNodesWithAllFlags(HierarchyNodeFlags flags)
		{
			int num = this.HasAllFlagsCount(flags);
			bool flag = num == 0;
			HierarchyNode[] array;
			if (flag)
			{
				array = Array.Empty<HierarchyNode>();
			}
			else
			{
				HierarchyNode[] array2 = new HierarchyNode[num];
				this.GetNodesWithAllFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x00006AF4 File Offset: 0x00004CF4
		public HierarchyNode[] GetNodesWithAnyFlags(HierarchyNodeFlags flags)
		{
			int num = this.HasAnyFlagsCount(flags);
			bool flag = num == 0;
			HierarchyNode[] array;
			if (flag)
			{
				array = Array.Empty<HierarchyNode>();
			}
			else
			{
				HierarchyNode[] array2 = new HierarchyNode[num];
				this.GetNodesWithAnyFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00006B34 File Offset: 0x00004D34
		public HierarchyViewNodesEnumerable EnumerateNodesWithAllFlags(HierarchyNodeFlags flags)
		{
			return new HierarchyViewNodesEnumerable(this, flags, new HierarchyViewNodesEnumerable.Predicate(this.HasAllFlags));
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00006B49 File Offset: 0x00004D49
		public HierarchyViewNodesEnumerable EnumerateNodesWithAnyFlags(HierarchyNodeFlags flags)
		{
			return new HierarchyViewNodesEnumerable(this, flags, new HierarchyViewNodesEnumerable.Predicate(this.HasAnyFlags));
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00006B5E File Offset: 0x00004D5E
		public int GetIndicesWithAllFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithAllFlagsSpan(flags, outIndices);
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00006B68 File Offset: 0x00004D68
		public int GetIndicesWithAnyFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithAnyFlagsSpan(flags, outIndices);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00006B74 File Offset: 0x00004D74
		public int[] GetIndicesWithAllFlags(HierarchyNodeFlags flags)
		{
			int num = this.HasAllFlagsCount(flags);
			bool flag = num == 0;
			int[] array;
			if (flag)
			{
				array = Array.Empty<int>();
			}
			else
			{
				int[] array2 = new int[num];
				this.GetIndicesWithAllFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00006BB4 File Offset: 0x00004DB4
		public int[] GetIndicesWithAnyFlags(HierarchyNodeFlags flags)
		{
			int num = this.HasAnyFlagsCount(flags);
			bool flag = num == 0;
			int[] array;
			if (flag)
			{
				array = Array.Empty<int>();
			}
			else
			{
				int[] array2 = new int[num];
				this.GetIndicesWithAnyFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public int GetNodesWithoutAllFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithoutAllFlagsSpan(flags, outNodes);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00006BFE File Offset: 0x00004DFE
		public int GetNodesWithoutAnyFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithoutAnyFlagsSpan(flags, outNodes);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00006C08 File Offset: 0x00004E08
		public HierarchyNode[] GetNodesWithoutAllFlags(HierarchyNodeFlags flags)
		{
			int num = this.DoesNotHaveAllFlagsCount(flags);
			bool flag = num == 0;
			HierarchyNode[] array;
			if (flag)
			{
				array = Array.Empty<HierarchyNode>();
			}
			else
			{
				HierarchyNode[] array2 = new HierarchyNode[num];
				this.GetNodesWithoutAllFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006C48 File Offset: 0x00004E48
		public HierarchyNode[] GetNodesWithoutAnyFlags(HierarchyNodeFlags flags)
		{
			int num = this.DoesNotHaveAnyFlagsCount(flags);
			bool flag = num == 0;
			HierarchyNode[] array;
			if (flag)
			{
				array = Array.Empty<HierarchyNode>();
			}
			else
			{
				HierarchyNode[] array2 = new HierarchyNode[num];
				this.GetNodesWithoutAnyFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006C88 File Offset: 0x00004E88
		public HierarchyViewNodesEnumerable EnumerateNodesWithoutAllFlags(HierarchyNodeFlags flags)
		{
			return new HierarchyViewNodesEnumerable(this, flags, new HierarchyViewNodesEnumerable.Predicate(this.DoesNotHaveAllFlags));
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006C9D File Offset: 0x00004E9D
		public HierarchyViewNodesEnumerable EnumerateNodesWithoutAnyFlags(HierarchyNodeFlags flags)
		{
			return new HierarchyViewNodesEnumerable(this, flags, new HierarchyViewNodesEnumerable.Predicate(this.DoesNotHaveAnyFlags));
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public int GetIndicesWithoutAllFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithoutAllFlagsSpan(flags, outIndices);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00006CBC File Offset: 0x00004EBC
		public int GetIndicesWithoutAnyFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithoutAnyFlagsSpan(flags, outIndices);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00006CC8 File Offset: 0x00004EC8
		public int[] GetIndicesWithoutAllFlags(HierarchyNodeFlags flags)
		{
			int num = this.DoesNotHaveAllFlagsCount(flags);
			bool flag = num == 0;
			int[] array;
			if (flag)
			{
				array = Array.Empty<int>();
			}
			else
			{
				int[] array2 = new int[num];
				this.GetIndicesWithoutAllFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00006D08 File Offset: 0x00004F08
		public int[] GetIndicesWithoutAnyFlags(HierarchyNodeFlags flags)
		{
			int num = this.DoesNotHaveAnyFlagsCount(flags);
			bool flag = num == 0;
			int[] array;
			if (flag)
			{
				array = Array.Empty<int>();
			}
			else
			{
				int[] array2 = new int[num];
				this.GetIndicesWithoutAnyFlagsSpan(flags, array2);
				array = array2;
			}
			return array;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00006D48 File Offset: 0x00004F48
		public void SetQuery(string query)
		{
			HierarchySearchQueryDescriptor hierarchySearchQueryDescriptor = this.QueryParser.ParseQuery(query);
			bool flag = hierarchySearchQueryDescriptor == this.Query;
			if (!flag)
			{
				this.Query = hierarchySearchQueryDescriptor;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00006D7C File Offset: 0x00004F7C
		[NativeMethod(IsThreadSafe = true)]
		public void Update()
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.Update_Injected(intPtr);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00006DA0 File Offset: 0x00004FA0
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncremental()
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.UpdateIncremental_Injected(intPtr);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00006DC4 File Offset: 0x00004FC4
		[NativeMethod(IsThreadSafe = true)]
		public bool UpdateIncrementalTimed(double milliseconds)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.UpdateIncrementalTimed_Injected(intPtr, milliseconds);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00006DE7 File Offset: 0x00004FE7
		public HierarchyViewModel.Enumerator GetEnumerator()
		{
			return new HierarchyViewModel.Enumerator(this);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00006DF0 File Offset: 0x00004FF0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static HierarchyViewModel FromIntPtr(IntPtr handlePtr)
		{
			return (handlePtr != IntPtr.Zero) ? ((HierarchyViewModel)GCHandle.FromIntPtr(handlePtr).Target) : null;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00006E20 File Offset: 0x00005020
		[FreeFunction("HierarchyViewModelBindings::Create", IsThreadSafe = true)]
		private static IntPtr Create(IntPtr handlePtr, HierarchyFlattened hierarchyFlattened, HierarchyNodeFlags defaultFlags, out IntPtr nodesPtr, out int nodesCount, out int version)
		{
			return HierarchyViewModel.Create_Injected(handlePtr, (hierarchyFlattened == null) ? ((IntPtr)0) : HierarchyFlattened.BindingsMarshaller.ConvertToNative(hierarchyFlattened), defaultFlags, out nodesPtr, out nodesCount, out version);
		}

		// Token: 0x06000265 RID: 613
		[FreeFunction("HierarchyViewModelBindings::Destroy", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy(IntPtr nativePtr);

		// Token: 0x06000266 RID: 614 RVA: 0x00006E48 File Offset: 0x00005048
		[FreeFunction("HierarchyViewModelBindings::SetFlagsAll", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void SetFlagsAll(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.SetFlagsAll_Injected(intPtr, flags);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00006E6C File Offset: 0x0000506C
		[FreeFunction("HierarchyViewModelBindings::SetFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void SetFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.SetFlagsNode_Injected(intPtr, in node, flags, recurse);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00006E94 File Offset: 0x00005094
		[FreeFunction("HierarchyViewModelBindings::SetFlagsNodes", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int SetFlagsNodes(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<HierarchyNode> readOnlySpan = nodes;
			int num;
			fixed (HierarchyNode* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.SetFlagsNodes_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00006ED8 File Offset: 0x000050D8
		[FreeFunction("HierarchyViewModelBindings::SetFlagsIndices", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int SetFlagsIndices(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = indices;
			int num;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.SetFlagsIndices_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00006F1C File Offset: 0x0000511C
		[FreeFunction("HierarchyViewModelBindings::HasAllFlagsAny", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool HasAllFlagsAny(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAllFlagsAny_Injected(intPtr, flags);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00006F40 File Offset: 0x00005140
		[FreeFunction("HierarchyViewModelBindings::HasAnyFlagsAny", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool HasAnyFlagsAny(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAnyFlagsAny_Injected(intPtr, flags);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006F64 File Offset: 0x00005164
		[FreeFunction("HierarchyViewModelBindings::HasAllFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool HasAllFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAllFlagsNode_Injected(intPtr, in node, flags);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00006F88 File Offset: 0x00005188
		[FreeFunction("HierarchyViewModelBindings::HasAnyFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool HasAnyFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.HasAnyFlagsNode_Injected(intPtr, in node, flags);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00006FAC File Offset: 0x000051AC
		[FreeFunction("HierarchyViewModelBindings::DoesNotHaveAllFlagsAny", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool DoesNotHaveAllFlagsAny(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAllFlagsAny_Injected(intPtr, flags);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00006FD0 File Offset: 0x000051D0
		[FreeFunction("HierarchyViewModelBindings::DoesNotHaveAnyFlagsAny", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool DoesNotHaveAnyFlagsAny(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAnyFlagsAny_Injected(intPtr, flags);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00006FF4 File Offset: 0x000051F4
		[FreeFunction("HierarchyViewModelBindings::DoesNotHaveAllFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool DoesNotHaveAllFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAllFlagsNode_Injected(intPtr, in node, flags);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00007018 File Offset: 0x00005218
		[FreeFunction("HierarchyViewModelBindings::DoesNotHaveAnyFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private bool DoesNotHaveAnyFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return HierarchyViewModel.DoesNotHaveAnyFlagsNode_Injected(intPtr, in node, flags);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000703C File Offset: 0x0000523C
		[FreeFunction("HierarchyViewModelBindings::ClearFlagsAll", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void ClearFlagsAll(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.ClearFlagsAll_Injected(intPtr, flags);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00007060 File Offset: 0x00005260
		[FreeFunction("HierarchyViewModelBindings::ClearFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void ClearFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.ClearFlagsNode_Injected(intPtr, in node, flags, recurse);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00007088 File Offset: 0x00005288
		[FreeFunction("HierarchyViewModelBindings::ClearFlagsNodes", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int ClearFlagsNodes(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<HierarchyNode> readOnlySpan = nodes;
			int num;
			fixed (HierarchyNode* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.ClearFlagsNodes_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000070CC File Offset: 0x000052CC
		[FreeFunction("HierarchyViewModelBindings::ClearFlagsIndices", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int ClearFlagsIndices(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = indices;
			int num;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.ClearFlagsIndices_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00007110 File Offset: 0x00005310
		[FreeFunction("HierarchyViewModelBindings::ToggleFlagsAll", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void ToggleFlagsAll(HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.ToggleFlagsAll_Injected(intPtr, flags);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00007134 File Offset: 0x00005334
		[FreeFunction("HierarchyViewModelBindings::ToggleFlagsNode", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private void ToggleFlagsNode(in HierarchyNode node, HierarchyNodeFlags flags, bool recurse = false)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			HierarchyViewModel.ToggleFlagsNode_Injected(intPtr, in node, flags, recurse);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000715C File Offset: 0x0000535C
		[FreeFunction("HierarchyViewModelBindings::ToggleFlagsNodes", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int ToggleFlagsNodes(ReadOnlySpan<HierarchyNode> nodes, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<HierarchyNode> readOnlySpan = nodes;
			int num;
			fixed (HierarchyNode* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.ToggleFlagsNodes_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x000071A0 File Offset: 0x000053A0
		[FreeFunction("HierarchyViewModelBindings::ToggleFlagsIndices", HasExplicitThis = true, IsThreadSafe = true)]
		private unsafe int ToggleFlagsIndices(ReadOnlySpan<int> indices, HierarchyNodeFlags flags)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ReadOnlySpan<int> readOnlySpan = indices;
			int num;
			fixed (int* pinnableReference = readOnlySpan.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, readOnlySpan.Length);
				num = HierarchyViewModel.ToggleFlagsIndices_Injected(intPtr, ref managedSpanWrapper, flags);
			}
			return num;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x000071E4 File Offset: 0x000053E4
		[FreeFunction("HierarchyViewModelBindings::GetNodesWithAllFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetNodesWithAllFlagsSpan(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outNodes;
			int nodesWithAllFlagsSpan_Injected;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodesWithAllFlagsSpan_Injected = HierarchyViewModel.GetNodesWithAllFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return nodesWithAllFlagsSpan_Injected;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007228 File Offset: 0x00005428
		[FreeFunction("HierarchyViewModelBindings::GetNodesWithAnyFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetNodesWithAnyFlagsSpan(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outNodes;
			int nodesWithAnyFlagsSpan_Injected;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodesWithAnyFlagsSpan_Injected = HierarchyViewModel.GetNodesWithAnyFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return nodesWithAnyFlagsSpan_Injected;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000726C File Offset: 0x0000546C
		[FreeFunction("HierarchyViewModelBindings::GetIndicesWithAllFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetIndicesWithAllFlagsSpan(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = outIndices;
			int indicesWithAllFlagsSpan_Injected;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				indicesWithAllFlagsSpan_Injected = HierarchyViewModel.GetIndicesWithAllFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return indicesWithAllFlagsSpan_Injected;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x000072B0 File Offset: 0x000054B0
		[FreeFunction("HierarchyViewModelBindings::GetIndicesWithAnyFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetIndicesWithAnyFlagsSpan(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = outIndices;
			int indicesWithAnyFlagsSpan_Injected;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				indicesWithAnyFlagsSpan_Injected = HierarchyViewModel.GetIndicesWithAnyFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return indicesWithAnyFlagsSpan_Injected;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000072F4 File Offset: 0x000054F4
		[FreeFunction("HierarchyViewModelBindings::GetNodesWithoutAllFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetNodesWithoutAllFlagsSpan(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outNodes;
			int nodesWithoutAllFlagsSpan_Injected;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodesWithoutAllFlagsSpan_Injected = HierarchyViewModel.GetNodesWithoutAllFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return nodesWithoutAllFlagsSpan_Injected;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00007338 File Offset: 0x00005538
		[FreeFunction("HierarchyViewModelBindings::GetNodesWithoutAnyFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetNodesWithoutAnyFlagsSpan(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<HierarchyNode> span = outNodes;
			int nodesWithoutAnyFlagsSpan_Injected;
			fixed (HierarchyNode* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				nodesWithoutAnyFlagsSpan_Injected = HierarchyViewModel.GetNodesWithoutAnyFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return nodesWithoutAnyFlagsSpan_Injected;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000737C File Offset: 0x0000557C
		[FreeFunction("HierarchyViewModelBindings::GetIndicesWithoutAllFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetIndicesWithoutAllFlagsSpan(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = outIndices;
			int indicesWithoutAllFlagsSpan_Injected;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				indicesWithoutAllFlagsSpan_Injected = HierarchyViewModel.GetIndicesWithoutAllFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return indicesWithoutAllFlagsSpan_Injected;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x000073C0 File Offset: 0x000055C0
		[FreeFunction("HierarchyViewModelBindings::GetIndicesWithoutAnyFlagsSpan", HasExplicitThis = true, IsThreadSafe = true, ThrowsException = true)]
		private unsafe int GetIndicesWithoutAnyFlagsSpan(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			IntPtr intPtr = HierarchyViewModel.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = outIndices;
			int indicesWithoutAnyFlagsSpan_Injected;
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				indicesWithoutAnyFlagsSpan_Injected = HierarchyViewModel.GetIndicesWithoutAnyFlagsSpan_Injected(intPtr, flags, ref managedSpanWrapper);
			}
			return indicesWithoutAnyFlagsSpan_Injected;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00007403 File Offset: 0x00005603
		[RequiredByNativeCode]
		private static IntPtr CreateHierarchyViewModel(IntPtr nativePtr, IntPtr flattenedPtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			return GCHandle.ToIntPtr(GCHandle.Alloc(new HierarchyViewModel(nativePtr, HierarchyFlattened.FromIntPtr(flattenedPtr), nodesPtr, nodesCount, version)));
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007420 File Offset: 0x00005620
		[RequiredByNativeCode]
		private static void UpdateHierarchyViewModel(IntPtr handlePtr, IntPtr nodesPtr, int nodesCount, int version)
		{
			HierarchyViewModel hierarchyViewModel = HierarchyViewModel.FromIntPtr(handlePtr);
			hierarchyViewModel.m_NodesPtr = nodesPtr;
			hierarchyViewModel.m_NodesCount = nodesCount;
			hierarchyViewModel.m_Version = version;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000744C File Offset: 0x0000564C
		[RequiredByNativeCode]
		private static void SearchBegin(IntPtr handlePtr)
		{
			HierarchyViewModel hierarchyViewModel = HierarchyViewModel.FromIntPtr(handlePtr);
			foreach (HierarchyNodeTypeHandlerBase hierarchyNodeTypeHandlerBase in hierarchyViewModel.m_Hierarchy.EnumerateNodeTypeHandlersBase())
			{
				hierarchyNodeTypeHandlerBase.Internal_SearchBegin(hierarchyViewModel.Query);
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000696D File Offset: 0x00004B6D
		[Obsolete("HasFlags is obsolete, please use HasAllFlags or HasAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool HasFlags(HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsAny(flags);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000697F File Offset: 0x00004B7F
		[Obsolete("HasFlags is obsolete, please use HasAllFlags or HasAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool HasFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsNode(in node, flags);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x000074B8 File Offset: 0x000056B8
		[Obsolete("HasFlagsCount is obsolete, please use HasAllFlagsCount or HasAnyFlagsCount instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int HasFlagsCount(HierarchyNodeFlags flags)
		{
			return this.HasAllFlagsCount(flags);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x000069DB File Offset: 0x00004BDB
		[Obsolete("DoesNotHaveFlags is obsolete, please use DoesNotHaveAllFlags or DoesNotHaveAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool DoesNotHaveFlags(HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAllFlagsAny(flags);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x000069ED File Offset: 0x00004BED
		[Obsolete("DoesNotHaveFlags is obsolete, please use DoesNotHaveAllFlags or DoesNotHaveAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool DoesNotHaveFlags(in HierarchyNode node, HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAllFlagsNode(in node, flags);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x000074C1 File Offset: 0x000056C1
		[Obsolete("DoesNotHaveFlagsCount is obsolete, please use DoesNotHaveAllFlagsCount or DoesNotHaveAnyFlagsCount instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int DoesNotHaveFlagsCount(HierarchyNodeFlags flags)
		{
			return this.DoesNotHaveAllFlagsCount(flags);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x00006A9F File Offset: 0x00004C9F
		[Obsolete("GetNodesWithFlags is obsolete, please use GetNodesWithAllFlags or GetNodesWithAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetNodesWithFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithAllFlagsSpan(flags, outNodes);
		}

		// Token: 0x0600028C RID: 652 RVA: 0x000074CA File Offset: 0x000056CA
		[Obsolete("GetNodesWithFlags is obsolete, please use GetNodesWithAllFlags or GetNodesWithAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HierarchyNode[] GetNodesWithFlags(HierarchyNodeFlags flags)
		{
			return this.GetNodesWithAllFlags(flags);
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000074D3 File Offset: 0x000056D3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("EnumerateNodesWithFlags is obsolete, please use EnumerateNodesWithAllFlags or EnumerateNodesWithAnyFlags instead", false)]
		public HierarchyViewNodesEnumerable EnumerateNodesWithFlags(HierarchyNodeFlags flags)
		{
			return this.EnumerateNodesWithAllFlags(flags);
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00006B5E File Offset: 0x00004D5E
		[Obsolete("GetIndicesWithFlags is obsolete, please use GetIndicesWithAllFlags or GetIndicesWithAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetIndicesWithFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithAllFlagsSpan(flags, outIndices);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x000074DC File Offset: 0x000056DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("GetIndicesWithFlags is obsolete, please use GetIndicesWithAllFlags or GetIndicesWithAnyFlags instead", false)]
		public int[] GetIndicesWithFlags(HierarchyNodeFlags flags)
		{
			return this.GetIndicesWithAllFlags(flags);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00006BF4 File Offset: 0x00004DF4
		[Obsolete("GetNodesWithoutFlags is obsolete, please use GetNodesWithoutAllFlags or GetNodesWithoutAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int GetNodesWithoutFlags(HierarchyNodeFlags flags, Span<HierarchyNode> outNodes)
		{
			return this.GetNodesWithoutAllFlagsSpan(flags, outNodes);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000074E5 File Offset: 0x000056E5
		[Obsolete("GetNodesWithoutFlags is obsolete, please use GetNodesWithoutAllFlags or GetNodesWithoutAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HierarchyNode[] GetNodesWithoutFlags(HierarchyNodeFlags flags)
		{
			return this.GetNodesWithoutAllFlags(flags);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000074EE File Offset: 0x000056EE
		[Obsolete("EnumerateNodesWithoutFlags is obsolete, please use EnumerateNodesWithoutAllFlags or EnumerateNodesWithoutAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public HierarchyViewNodesEnumerable EnumerateNodesWithoutFlags(HierarchyNodeFlags flags)
		{
			return this.EnumerateNodesWithoutAllFlags(flags);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00006CB2 File Offset: 0x00004EB2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("GetIndicesWithoutFlags is obsolete, please use GetIndicesWithoutAllFlags or GetIndicesWithoutAnyFlags instead", false)]
		public int GetIndicesWithoutFlags(HierarchyNodeFlags flags, Span<int> outIndices)
		{
			return this.GetIndicesWithoutAllFlagsSpan(flags, outIndices);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000074F7 File Offset: 0x000056F7
		[Obsolete("GetIndicesWithoutFlags is obsolete, please use GetIndicesWithoutAllFlags or GetIndicesWithoutAnyFlags instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public int[] GetIndicesWithoutFlags(HierarchyNodeFlags flags)
		{
			return this.GetIndicesWithoutAllFlags(flags);
		}

		// Token: 0x06000295 RID: 661
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_Updating_Injected(IntPtr _unity_self);

		// Token: 0x06000296 RID: 662
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_UpdateNeeded_Injected(IntPtr _unity_self);

		// Token: 0x06000297 RID: 663
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_UpdateProgress_Injected(IntPtr _unity_self);

		// Token: 0x06000298 RID: 664
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HierarchySearchQueryDescriptor get_Query_Injected(IntPtr _unity_self);

		// Token: 0x06000299 RID: 665
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_Query_Injected(IntPtr _unity_self, HierarchySearchQueryDescriptor value);

		// Token: 0x0600029A RID: 666
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int IndexOf_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600029B RID: 667
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Contains_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600029C RID: 668
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetParent_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x0600029D RID: 669
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextSibling_Injected(IntPtr _unity_self, in HierarchyNode node, out HierarchyNode ret);

		// Token: 0x0600029E RID: 670
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCount_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x0600029F RID: 671
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetChildrenCountRecursive_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x060002A0 RID: 672
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetDepth_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x060002A1 RID: 673
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern HierarchyNodeFlags GetFlags_Injected(IntPtr _unity_self, in HierarchyNode node);

		// Token: 0x060002A2 RID: 674
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int HasAllFlagsCount_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002A3 RID: 675
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int HasAnyFlagsCount_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002A4 RID: 676
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int DoesNotHaveAllFlagsCount_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002A5 RID: 677
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int DoesNotHaveAnyFlagsCount_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002A6 RID: 678
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self);

		// Token: 0x060002A7 RID: 679
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncremental_Injected(IntPtr _unity_self);

		// Token: 0x060002A8 RID: 680
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UpdateIncrementalTimed_Injected(IntPtr _unity_self, double milliseconds);

		// Token: 0x060002A9 RID: 681
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected(IntPtr handlePtr, IntPtr hierarchyFlattened, HierarchyNodeFlags defaultFlags, out IntPtr nodesPtr, out int nodesCount, out int version);

		// Token: 0x060002AA RID: 682
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFlagsAll_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002AB RID: 683
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags, bool recurse);

		// Token: 0x060002AC RID: 684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetFlagsNodes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper nodes, HierarchyNodeFlags flags);

		// Token: 0x060002AD RID: 685
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SetFlagsIndices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper indices, HierarchyNodeFlags flags);

		// Token: 0x060002AE RID: 686
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAllFlagsAny_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002AF RID: 687
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAnyFlagsAny_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002B0 RID: 688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAllFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x060002B1 RID: 689
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasAnyFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x060002B2 RID: 690
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoesNotHaveAllFlagsAny_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002B3 RID: 691
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoesNotHaveAnyFlagsAny_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002B4 RID: 692
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoesNotHaveAllFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x060002B5 RID: 693
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool DoesNotHaveAnyFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags);

		// Token: 0x060002B6 RID: 694
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearFlagsAll_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002B7 RID: 695
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags, bool recurse);

		// Token: 0x060002B8 RID: 696
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ClearFlagsNodes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper nodes, HierarchyNodeFlags flags);

		// Token: 0x060002B9 RID: 697
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ClearFlagsIndices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper indices, HierarchyNodeFlags flags);

		// Token: 0x060002BA RID: 698
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ToggleFlagsAll_Injected(IntPtr _unity_self, HierarchyNodeFlags flags);

		// Token: 0x060002BB RID: 699
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ToggleFlagsNode_Injected(IntPtr _unity_self, in HierarchyNode node, HierarchyNodeFlags flags, bool recurse);

		// Token: 0x060002BC RID: 700
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ToggleFlagsNodes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper nodes, HierarchyNodeFlags flags);

		// Token: 0x060002BD RID: 701
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ToggleFlagsIndices_Injected(IntPtr _unity_self, ref ManagedSpanWrapper indices, HierarchyNodeFlags flags);

		// Token: 0x060002BE RID: 702
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodesWithAllFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outNodes);

		// Token: 0x060002BF RID: 703
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodesWithAnyFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outNodes);

		// Token: 0x060002C0 RID: 704
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIndicesWithAllFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outIndices);

		// Token: 0x060002C1 RID: 705
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIndicesWithAnyFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outIndices);

		// Token: 0x060002C2 RID: 706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodesWithoutAllFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outNodes);

		// Token: 0x060002C3 RID: 707
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNodesWithoutAnyFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outNodes);

		// Token: 0x060002C4 RID: 708
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIndicesWithoutAllFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outIndices);

		// Token: 0x060002C5 RID: 709
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIndicesWithoutAnyFlagsSpan_Injected(IntPtr _unity_self, HierarchyNodeFlags flags, ref ManagedSpanWrapper outIndices);

		// Token: 0x04000094 RID: 148
		private IntPtr m_Ptr;

		// Token: 0x04000095 RID: 149
		private readonly Hierarchy m_Hierarchy;

		// Token: 0x04000096 RID: 150
		private readonly HierarchyFlattened m_HierarchyFlattened;

		// Token: 0x04000097 RID: 151
		private IntPtr m_NodesPtr;

		// Token: 0x04000098 RID: 152
		private int m_NodesCount;

		// Token: 0x04000099 RID: 153
		private int m_Version;

		// Token: 0x0400009A RID: 154
		private readonly bool m_IsOwner;

		// Token: 0x02000035 RID: 53
		internal static class BindingsMarshaller
		{
			// Token: 0x060002C6 RID: 710 RVA: 0x00007500 File Offset: 0x00005700
			public static IntPtr ConvertToNative(HierarchyViewModel viewModel)
			{
				return viewModel.m_Ptr;
			}
		}

		// Token: 0x02000036 RID: 54
		public struct Enumerator
		{
			// Token: 0x060002C7 RID: 711 RVA: 0x00007508 File Offset: 0x00005708
			internal unsafe Enumerator(HierarchyViewModel hierarchyViewModel)
			{
				this.m_ViewModel = hierarchyViewModel;
				this.m_HierarchyFlattened = hierarchyViewModel.HierarchyFlattened;
				this.m_NodesPtr = (int*)(void*)hierarchyViewModel.m_NodesPtr;
				this.m_NodesCount = hierarchyViewModel.Count;
				this.m_Version = hierarchyViewModel.Version;
				this.m_Index = -1;
			}

			// Token: 0x1700005A RID: 90
			// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000755C File Offset: 0x0000575C
			public unsafe readonly ref HierarchyNode Current
			{
				[MethodImpl(MethodImplOptions.AggressiveInlining)]
				get
				{
					bool flag = this.m_Version != this.m_ViewModel.m_Version;
					if (flag)
					{
						throw new InvalidOperationException("HierarchyViewModel was modified.");
					}
					return HierarchyFlattenedNode.GetNodeByRef(this.m_HierarchyFlattened[this.m_NodesPtr[this.m_Index]]);
				}
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x000075B4 File Offset: 0x000057B4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				int num = this.m_Index + 1;
				this.m_Index = num;
				return num < this.m_NodesCount;
			}

			// Token: 0x0400009C RID: 156
			private readonly HierarchyViewModel m_ViewModel;

			// Token: 0x0400009D RID: 157
			private readonly HierarchyFlattened m_HierarchyFlattened;

			// Token: 0x0400009E RID: 158
			private unsafe readonly int* m_NodesPtr;

			// Token: 0x0400009F RID: 159
			private readonly int m_NodesCount;

			// Token: 0x040000A0 RID: 160
			private readonly int m_Version;

			// Token: 0x040000A1 RID: 161
			private int m_Index;
		}
	}
}
