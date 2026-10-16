using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006A4 RID: 1700
	[RequiredByNativeCode]
	internal readonly struct LayoutDataAccess
	{
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000BBD34 File Offset: 0x000B9F34
		public bool IsValid
		{
			get
			{
				return this.m_Nodes.IsValid && this.m_Configs.IsValid;
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000BBD62 File Offset: 0x000B9F62
		internal LayoutDataAccess(int manager, LayoutDataStore nodes, LayoutDataStore configs)
		{
			this.m_Manager = manager;
			this.m_Nodes = nodes;
			this.m_Configs = configs;
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000BBD7A File Offset: 0x000B9F7A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe ref T GetTypedNodeDataRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(LayoutHandle handle, LayoutNodeDataType type) where T : struct, ValueType
		{
			return ref *(T*)this.m_Nodes.GetComponentDataPtr(handle.Index, (int)type);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000BBD8E File Offset: 0x000B9F8E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe ref T GetTypedConfigDataRef<[global::System.Runtime.CompilerServices.IsUnmanaged] T>(LayoutHandle handle, LayoutConfigDataType type) where T : struct, ValueType
		{
			return ref *(T*)this.m_Configs.GetComponentDataPtr(handle.Index, (int)type);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000BBDA2 File Offset: 0x000B9FA2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutNodeData GetNodeData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutNodeData>(handle, LayoutNodeDataType.Node);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000BBDAC File Offset: 0x000B9FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutStyleData GetStyleData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutStyleData>(handle, LayoutNodeDataType.Style);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000BBDB6 File Offset: 0x000B9FB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutComputedData GetComputedData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutComputedData>(handle, LayoutNodeDataType.Computed);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000BBDC0 File Offset: 0x000B9FC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutCacheData GetCacheData(LayoutHandle handle)
		{
			return this.GetTypedNodeDataRef<LayoutCacheData>(handle, LayoutNodeDataType.Cache);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000BBDCA File Offset: 0x000B9FCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref LayoutConfigData GetConfigData(LayoutHandle handle)
		{
			return this.GetTypedConfigDataRef<LayoutConfigData>(handle, LayoutConfigDataType.Config);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000BBDD4 File Offset: 0x000B9FD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LayoutMeasureFunction GetMeasureFunction(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetMeasureFunction(handle);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000BBDE7 File Offset: 0x000B9FE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetMeasureFunction(LayoutHandle handle, LayoutMeasureFunction value)
		{
			LayoutManager.GetManager(this.m_Manager).SetMeasureFunction(handle, value);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000BBDFC File Offset: 0x000B9FFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public VisualElement GetOwner(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetOwner(handle);
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000BBE0F File Offset: 0x000BA00F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetOwner(LayoutHandle handle, VisualElement value)
		{
			LayoutManager.GetManager(this.m_Manager).SetOwner(handle, value);
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000BBE24 File Offset: 0x000BA024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LayoutBaselineFunction GetBaselineFunction(LayoutHandle handle)
		{
			return LayoutManager.GetManager(this.m_Manager).GetBaselineFunction(handle);
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000BBE37 File Offset: 0x000BA037
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetBaselineFunction(LayoutHandle handle, LayoutBaselineFunction value)
		{
			LayoutManager.GetManager(this.m_Manager).SetBaselineFunction(handle, value);
		}

		// Token: 0x0400185E RID: 6238
		private readonly int m_Manager;

		// Token: 0x0400185F RID: 6239
		private readonly LayoutDataStore m_Nodes;

		// Token: 0x04001860 RID: 6240
		private readonly LayoutDataStore m_Configs;
	}
}
