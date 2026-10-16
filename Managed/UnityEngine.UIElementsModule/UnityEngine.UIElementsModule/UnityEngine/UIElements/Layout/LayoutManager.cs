using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.Collections;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x02000691 RID: 1681
	internal class LayoutManager : IDisposable
	{
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000B9783 File Offset: 0x000B7983
		public static LayoutManager SharedManager
		{
			get
			{
				return LayoutManager.s_SharedInstance;
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000B978A File Offset: 0x000B798A
		static LayoutManager()
		{
			LayoutManager.Initialize();
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000B97A0 File Offset: 0x000B79A0
		private static void Initialize()
		{
			bool flag = LayoutManager.s_Initialized;
			if (!flag)
			{
				LayoutManager.s_Initialized = true;
				bool flag2 = !LayoutManager.s_AppDomainUnloadRegistered;
				if (flag2)
				{
					AppDomain.CurrentDomain.DomainUnload += delegate(object _, EventArgs __)
					{
						bool flag3 = LayoutManager.s_Initialized;
						if (flag3)
						{
							LayoutManager.Shutdown();
						}
					};
					LayoutManager.s_AppDomainUnloadRegistered = true;
				}
				LayoutManager.s_SharedInstance = new LayoutManager(Allocator.Persistent);
			}
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x000B9808 File Offset: 0x000B7A08
		private static void Shutdown()
		{
			bool flag = !LayoutManager.s_Initialized;
			if (!flag)
			{
				LayoutManager.s_Initialized = false;
				LayoutManager.s_SharedInstance.Dispose();
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000B9835 File Offset: 0x000B7A35
		private static int DefaultCapacity
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000B9839 File Offset: 0x000B7A39
		public int NodeCapacity
		{
			get
			{
				return this.m_Nodes.Capacity;
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000B9846 File Offset: 0x000B7A46
		internal static LayoutManager GetManager(int index)
		{
			return ((ulong)index < (ulong)((long)LayoutManager.s_Managers.Count)) ? LayoutManager.s_Managers[index] : null;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000B9865 File Offset: 0x000B7A65
		public LayoutManager(Allocator allocator)
			: this(allocator, LayoutManager.DefaultCapacity)
		{
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000B9878 File Offset: 0x000B7A78
		public LayoutManager(Allocator allocator, int initialNodeCapacity)
		{
			this.m_Index = LayoutManager.s_Managers.Count;
			LayoutManager.s_Managers.Add(this);
			ComponentType[] array = new ComponentType[]
			{
				ComponentType.Create<LayoutNodeData>(),
				ComponentType.Create<LayoutStyleData>(),
				ComponentType.Create<LayoutComputedData>(),
				ComponentType.Create<LayoutCacheData>()
			};
			ComponentType[] array2 = new ComponentType[] { ComponentType.Create<LayoutConfigData>() };
			this.m_Nodes = new LayoutDataStore(array, initialNodeCapacity, allocator);
			this.m_Configs = new LayoutDataStore(array2, 32, allocator);
			this.m_DefaultConfig = this.CreateConfig().Handle;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000B9964 File Offset: 0x000B7B64
		public unsafe void Dispose()
		{
			LayoutManager.s_Managers[this.m_Index] = null;
			for (int i = 0; i <= this.m_HighMark; i++)
			{
				LayoutNodeData* componentDataPtr = (LayoutNodeData*)this.m_Nodes.GetComponentDataPtr(i, 0);
				bool flag = !componentDataPtr->Children.IsCreated;
				if (!flag)
				{
					componentDataPtr->Children.Dispose();
					componentDataPtr->Children = new LayoutList<LayoutHandle>();
				}
			}
			this.m_Nodes.Dispose();
			this.m_Configs.Dispose();
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000B99F4 File Offset: 0x000B7BF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private LayoutDataAccess GetAccess()
		{
			return new LayoutDataAccess(this.m_Index, this.m_Nodes, this.m_Configs);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000B9A20 File Offset: 0x000B7C20
		public LayoutConfig GetDefaultConfig()
		{
			return new LayoutConfig(this.GetAccess(), this.m_DefaultConfig);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000B9A44 File Offset: 0x000B7C44
		public LayoutConfig CreateConfig()
		{
			LayoutDataAccess access = this.GetAccess();
			LayoutConfigData @default = LayoutConfigData.Default;
			return new LayoutConfig(access, this.m_Configs.Allocate<LayoutConfigData>(in @default));
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000B9A74 File Offset: 0x000B7C74
		public void DestroyConfig(ref LayoutConfig config)
		{
			LayoutHandle handle = config.Handle;
			this.m_Configs.Free(in handle);
			config = LayoutConfig.Undefined;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000B9AA4 File Offset: 0x000B7CA4
		public LayoutNode CreateNode()
		{
			return this.CreateNodeInternal(this.m_DefaultConfig);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		public LayoutNode CreateNode(LayoutConfig config)
		{
			return this.CreateNodeInternal(config.Handle);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000B9AE4 File Offset: 0x000B7CE4
		public LayoutNode CreateNode(LayoutNode source)
		{
			LayoutNode layoutNode = this.CreateNodeInternal(source.Config.Handle);
			layoutNode.CopyStyle(source);
			return layoutNode;
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000B9B18 File Offset: 0x000B7D18
		private LayoutNode CreateNodeInternal(LayoutHandle configHandle)
		{
			this.TryFreeNodes();
			LayoutNodeData layoutNodeData = default(LayoutNodeData);
			layoutNodeData.Config = configHandle;
			layoutNodeData.Children = new LayoutList<LayoutHandle>();
			LayoutComputedData @default = LayoutComputedData.Default;
			LayoutHandle layoutHandle = this.m_Nodes.Allocate<LayoutNodeData, LayoutStyleData, LayoutComputedData, LayoutCacheData>(in layoutNodeData, in LayoutStyleData.Default, in @default, in LayoutCacheData.Default);
			bool flag = layoutHandle.Index > this.m_HighMark;
			if (flag)
			{
				this.m_HighMark = layoutHandle.Index;
			}
			LayoutNode layoutNode = new LayoutNode(this.GetAccess(), layoutHandle);
			Debug.Assert(!this.GetAccess().GetNodeData(layoutHandle).Children.IsCreated, "memory is not initialized");
			return layoutNode;
		}

		// Token: 0x060030E0 RID: 12512 RVA: 0x000B9BC8 File Offset: 0x000B7DC8
		private void TryFreeNodes()
		{
			bool flag = false;
			try
			{
				Monitor.TryEnter(this.m_SyncRoot, ref flag);
				bool flag2 = flag;
				if (flag2)
				{
					while (this.m_NodesToFree.Count > 0)
					{
						this.FreeNode(this.m_NodesToFree.Pop());
					}
				}
			}
			finally
			{
				bool flag3 = flag;
				if (flag3)
				{
					Monitor.Exit(this.m_SyncRoot);
				}
			}
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000B9C3C File Offset: 0x000B7E3C
		public void DestroyNode(ref LayoutNode node)
		{
			bool isUndefined = node.IsUndefined;
			if (!isUndefined)
			{
				LayoutDataAccess access = this.GetAccess();
				bool flag = !access.IsValid;
				if (!flag)
				{
					ref LayoutNodeData nodeData = ref access.GetNodeData(node.Handle);
					bool isCreated = nodeData.Children.IsCreated;
					if (isCreated)
					{
						nodeData.Children.Dispose();
						nodeData.Children = new LayoutList<LayoutHandle>();
					}
					object syncRoot = this.m_SyncRoot;
					lock (syncRoot)
					{
						this.m_NodesToFree.Push(node.Handle);
					}
					node = LayoutNode.Undefined;
				}
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000B9CFC File Offset: 0x000B7EFC
		private void FreeNode(LayoutHandle handle)
		{
			ref LayoutNodeData nodeData = ref this.GetAccess().GetNodeData(handle);
			this.m_ManagedMeasureFunctions.UpdateValue(ref nodeData.ManagedMeasureFunctionIndex, null);
			this.m_ManagedBaselineFunctions.UpdateValue(ref nodeData.ManagedBaselineFunctionIndex, null);
			this.m_ManagedOwners.UpdateValue(ref nodeData.ManagedOwnerIndex, null);
			this.m_Nodes.Free(in handle);
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000B9D64 File Offset: 0x000B7F64
		public void Collect()
		{
			object syncRoot = this.m_SyncRoot;
			lock (syncRoot)
			{
				while (this.m_NodesToFree.Count > 0)
				{
					this.FreeNode(this.m_NodesToFree.Pop());
				}
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000B9DC8 File Offset: 0x000B7FC8
		public LayoutMeasureFunction GetMeasureFunction(LayoutHandle handle)
		{
			return this.m_ManagedMeasureFunctions.GetValue(this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex);
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000B9DFC File Offset: 0x000B7FFC
		public void SetMeasureFunction(LayoutHandle handle, LayoutMeasureFunction value)
		{
			ref int ptr = ref this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex;
			this.m_ManagedMeasureFunctions.UpdateValue(ref ptr, value);
		}

		// Token: 0x060030E6 RID: 12518 RVA: 0x000B9E30 File Offset: 0x000B8030
		public VisualElement GetOwner(LayoutHandle handle)
		{
			bool flag = this.GetAccess().GetNodeData(handle).ManagedOwnerIndex == 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = null;
			}
			else
			{
				VisualElement visualElement2;
				this.m_ManagedOwners.GetValue(this.GetAccess().GetNodeData(handle).ManagedOwnerIndex).TryGetTarget(out visualElement2);
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x000B9E8C File Offset: 0x000B808C
		public void SetOwner(LayoutHandle handle, VisualElement value)
		{
			ref int ptr = ref this.GetAccess().GetNodeData(handle).ManagedOwnerIndex;
			this.m_ManagedOwners.UpdateValue(ref ptr, new WeakReference<VisualElement>(value));
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x000B9EC4 File Offset: 0x000B80C4
		public LayoutBaselineFunction GetBaselineFunction(LayoutHandle handle)
		{
			return this.m_ManagedBaselineFunctions.GetValue(this.GetAccess().GetNodeData(handle).ManagedMeasureFunctionIndex);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000B9EF8 File Offset: 0x000B80F8
		public void SetBaselineFunction(LayoutHandle handle, LayoutBaselineFunction value)
		{
			ref int ptr = ref this.GetAccess().GetNodeData(handle).ManagedBaselineFunctionIndex;
			this.m_ManagedBaselineFunctions.UpdateValue(ref ptr, value);
		}

		// Token: 0x0400181D RID: 6173
		private static bool s_Initialized;

		// Token: 0x0400181E RID: 6174
		private static bool s_AppDomainUnloadRegistered;

		// Token: 0x0400181F RID: 6175
		private static LayoutManager s_SharedInstance;

		// Token: 0x04001820 RID: 6176
		private static readonly List<LayoutManager> s_Managers = new List<LayoutManager>();

		// Token: 0x04001821 RID: 6177
		public const int k_CapacityBig = 65536;

		// Token: 0x04001822 RID: 6178
		public const int k_CapacitySmall = 16;

		// Token: 0x04001823 RID: 6179
		private const int k_InitialConfigCapacity = 32;

		// Token: 0x04001824 RID: 6180
		private readonly int m_Index;

		// Token: 0x04001825 RID: 6181
		private LayoutDataStore m_Nodes;

		// Token: 0x04001826 RID: 6182
		private LayoutDataStore m_Configs;

		// Token: 0x04001827 RID: 6183
		private readonly object m_SyncRoot = new object();

		// Token: 0x04001828 RID: 6184
		private readonly Stack<LayoutHandle> m_NodesToFree = new Stack<LayoutHandle>();

		// Token: 0x04001829 RID: 6185
		private readonly LayoutHandle m_DefaultConfig;

		// Token: 0x0400182A RID: 6186
		private readonly ManagedObjectStore<LayoutMeasureFunction> m_ManagedMeasureFunctions = new ManagedObjectStore<LayoutMeasureFunction>();

		// Token: 0x0400182B RID: 6187
		private readonly ManagedObjectStore<LayoutBaselineFunction> m_ManagedBaselineFunctions = new ManagedObjectStore<LayoutBaselineFunction>();

		// Token: 0x0400182C RID: 6188
		private readonly ManagedObjectStore<WeakReference<VisualElement>> m_ManagedOwners = new ManagedObjectStore<WeakReference<VisualElement>>();

		// Token: 0x0400182D RID: 6189
		private int m_HighMark = -1;
	}
}
