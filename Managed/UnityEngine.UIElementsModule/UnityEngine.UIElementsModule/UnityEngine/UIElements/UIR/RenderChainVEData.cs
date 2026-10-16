using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000668 RID: 1640
	internal struct RenderChainVEData
	{
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000B3EEC File Offset: 0x000B20EC
		public RenderChainCommand lastTailOrHeadCommand
		{
			get
			{
				return this.lastTailCommand ?? this.lastHeadCommand;
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000B3F10 File Offset: 0x000B2110
		public static bool AllocatesID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Owned && alloc.IsValid();
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000B3F38 File Offset: 0x000B2138
		public static bool InheritsID(BMPAlloc alloc)
		{
			return alloc.ownedState == OwnedState.Inherited && alloc.IsValid();
		}

		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06002FFA RID: 12282 RVA: 0x000B3F5C File Offset: 0x000B215C
		public bool isInChain
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsInChain) == RenderDataFlags.IsInChain;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000B3F69 File Offset: 0x000B2169
		public bool isGroupTransform
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsGroupTransform) == RenderDataFlags.IsGroupTransform;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000B3F76 File Offset: 0x000B2176
		public bool isIgnoringDynamicColorHint
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.IsIgnoringDynamicColorHint) == RenderDataFlags.IsIgnoringDynamicColorHint;
			}
		}

		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000B3F83 File Offset: 0x000B2183
		public bool hasExtraData
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.HasExtraData) == RenderDataFlags.HasExtraData;
			}
		}

		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000B3F90 File Offset: 0x000B2190
		public bool hasExtraMeshes
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return (this.flags & RenderDataFlags.HasExtraMeshes) == RenderDataFlags.HasExtraMeshes;
			}
		}

		// Token: 0x04001749 RID: 5961
		public VisualElement prev;

		// Token: 0x0400174A RID: 5962
		public VisualElement next;

		// Token: 0x0400174B RID: 5963
		public VisualElement groupTransformAncestor;

		// Token: 0x0400174C RID: 5964
		public VisualElement boneTransformAncestor;

		// Token: 0x0400174D RID: 5965
		public VisualElement prevDirty;

		// Token: 0x0400174E RID: 5966
		public VisualElement nextDirty;

		// Token: 0x0400174F RID: 5967
		public RenderDataFlags flags;

		// Token: 0x04001750 RID: 5968
		public int hierarchyDepth;

		// Token: 0x04001751 RID: 5969
		public RenderDataDirtyTypes dirtiedValues;

		// Token: 0x04001752 RID: 5970
		public uint dirtyID;

		// Token: 0x04001753 RID: 5971
		public RenderChainCommand firstHeadCommand;

		// Token: 0x04001754 RID: 5972
		public RenderChainCommand lastHeadCommand;

		// Token: 0x04001755 RID: 5973
		public RenderChainCommand firstTailCommand;

		// Token: 0x04001756 RID: 5974
		public RenderChainCommand lastTailCommand;

		// Token: 0x04001757 RID: 5975
		public bool localFlipsWinding;

		// Token: 0x04001758 RID: 5976
		public bool localTransformScaleZero;

		// Token: 0x04001759 RID: 5977
		public bool worldFlipsWinding;

		// Token: 0x0400175A RID: 5978
		public bool worldTransformScaleZero;

		// Token: 0x0400175B RID: 5979
		public ClipMethod clipMethod;

		// Token: 0x0400175C RID: 5980
		public int childrenStencilRef;

		// Token: 0x0400175D RID: 5981
		public int childrenMaskDepth;

		// Token: 0x0400175E RID: 5982
		public MeshHandle headMesh;

		// Token: 0x0400175F RID: 5983
		public MeshHandle tailMesh;

		// Token: 0x04001760 RID: 5984
		public Matrix4x4 verticesSpace;

		// Token: 0x04001761 RID: 5985
		public BMPAlloc transformID;

		// Token: 0x04001762 RID: 5986
		public BMPAlloc clipRectID;

		// Token: 0x04001763 RID: 5987
		public BMPAlloc opacityID;

		// Token: 0x04001764 RID: 5988
		public BMPAlloc textCoreSettingsID;

		// Token: 0x04001765 RID: 5989
		public BMPAlloc colorID;

		// Token: 0x04001766 RID: 5990
		public BMPAlloc backgroundColorID;

		// Token: 0x04001767 RID: 5991
		public BMPAlloc borderLeftColorID;

		// Token: 0x04001768 RID: 5992
		public BMPAlloc borderTopColorID;

		// Token: 0x04001769 RID: 5993
		public BMPAlloc borderRightColorID;

		// Token: 0x0400176A RID: 5994
		public BMPAlloc borderBottomColorID;

		// Token: 0x0400176B RID: 5995
		public BMPAlloc tintColorID;

		// Token: 0x0400176C RID: 5996
		public float compositeOpacity;

		// Token: 0x0400176D RID: 5997
		public float backgroundAlpha;

		// Token: 0x0400176E RID: 5998
		public BasicNode<TextureEntry> textures;

		// Token: 0x0400176F RID: 5999
		public bool pendingRepaint;

		// Token: 0x04001770 RID: 6000
		public bool pendingHierarchicalRepaint;
	}
}
