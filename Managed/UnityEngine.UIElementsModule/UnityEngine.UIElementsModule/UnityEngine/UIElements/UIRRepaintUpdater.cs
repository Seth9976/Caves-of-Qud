using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000330 RID: 816
	internal class UIRRepaintUpdater : BaseVisualTreeUpdater, IPanelRenderer
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x0005B471 File Offset: 0x00059671
		public UIRRepaintUpdater()
		{
			base.panelChanged += this.OnPanelChanged;
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600179A RID: 6042 RVA: 0x0005B48E File Offset: 0x0005968E
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRRepaintUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600179B RID: 6043 RVA: 0x0005B495 File Offset: 0x00059695
		// (set) Token: 0x0600179C RID: 6044 RVA: 0x0005B4A0 File Offset: 0x000596A0
		public bool forceGammaRendering
		{
			get
			{
				return this.m_ForceGammaRendering;
			}
			set
			{
				bool flag = this.m_ForceGammaRendering == value;
				if (!flag)
				{
					this.m_ForceGammaRendering = value;
					this.DestroyRenderChain();
				}
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600179D RID: 6045 RVA: 0x0005B4CB File Offset: 0x000596CB
		// (set) Token: 0x0600179E RID: 6046 RVA: 0x0005B4D4 File Offset: 0x000596D4
		public uint vertexBudget
		{
			get
			{
				return this.m_VertexBudget;
			}
			set
			{
				bool flag = this.m_VertexBudget == value;
				if (!flag)
				{
					this.m_VertexBudget = value;
					this.DestroyRenderChain();
				}
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x0600179F RID: 6047 RVA: 0x0005B4FF File Offset: 0x000596FF
		// (set) Token: 0x060017A0 RID: 6048 RVA: 0x0005B507 File Offset: 0x00059707
		public bool drawStats { get; set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x060017A1 RID: 6049 RVA: 0x0005B510 File Offset: 0x00059710
		// (set) Token: 0x060017A2 RID: 6050 RVA: 0x0005B518 File Offset: 0x00059718
		public bool breakBatches { get; set; }

		// Token: 0x060017A3 RID: 6051 RVA: 0x0005B524 File Offset: 0x00059724
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				bool flag2 = (versionChangeType & VersionChangeType.Transform) > (VersionChangeType)0;
				bool flag3 = (versionChangeType & VersionChangeType.Size) > (VersionChangeType)0;
				bool flag4 = (versionChangeType & VersionChangeType.Overflow) > (VersionChangeType)0;
				bool flag5 = (versionChangeType & VersionChangeType.BorderRadius) > (VersionChangeType)0;
				bool flag6 = (versionChangeType & VersionChangeType.BorderWidth) > (VersionChangeType)0;
				bool flag7 = (versionChangeType & VersionChangeType.RenderHints) > (VersionChangeType)0;
				bool flag8 = (versionChangeType & VersionChangeType.DisableRendering) > (VersionChangeType)0;
				bool flag9 = (versionChangeType & VersionChangeType.Repaint) > (VersionChangeType)0;
				bool flag10 = flag7;
				if (flag10)
				{
					this.renderChain.UIEOnRenderHintsChanged(ve);
				}
				bool flag11 = flag2 || flag3 || flag6;
				if (flag11)
				{
					this.renderChain.UIEOnTransformOrSizeChanged(ve, flag2, flag3 || flag6);
				}
				bool flag12 = flag4 || flag5;
				if (flag12)
				{
					this.renderChain.UIEOnClippingChanged(ve, false);
				}
				bool flag13 = (versionChangeType & VersionChangeType.Opacity) > (VersionChangeType)0;
				if (flag13)
				{
					this.renderChain.UIEOnOpacityChanged(ve, false);
				}
				bool flag14 = (versionChangeType & VersionChangeType.Color) > (VersionChangeType)0;
				if (flag14)
				{
					this.renderChain.UIEOnColorChanged(ve);
				}
				bool flag15 = flag9;
				if (flag15)
				{
					this.renderChain.UIEOnVisualsChanged(ve, false);
				}
				bool flag16 = flag8 && !flag9;
				if (flag16)
				{
					this.renderChain.UIEOnDisableRenderingChanged(ve);
				}
			}
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0005B65C File Offset: 0x0005985C
		public override void Update()
		{
			bool flag = this.renderChain == null;
			if (flag)
			{
				this.InitRenderChain();
			}
			bool flag2 = this.renderChain == null || this.renderChain.device == null;
			if (!flag2)
			{
				this.renderChain.ProcessChanges();
				this.renderChain.drawStats = this.drawStats;
				this.renderChain.device.breakBatches = this.breakBatches;
			}
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0005B6D4 File Offset: 0x000598D4
		public void Render()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				Debug.Assert(!this.renderChain.drawInCameras);
				this.renderChain.Render();
			}
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0005B714 File Offset: 0x00059914
		protected virtual RenderChain CreateRenderChain()
		{
			return new RenderChain(base.panel);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005B731 File Offset: 0x00059931
		static UIRRepaintUpdater()
		{
			Utility.GraphicsResourcesRecreate += UIRRepaintUpdater.OnGraphicsResourcesRecreate;
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0005B760 File Offset: 0x00059960
		private static void OnGraphicsResourcesRecreate(bool recreate)
		{
			bool flag = !recreate;
			if (flag)
			{
				UIRenderDevice.PrepareForGfxDeviceRecreate();
			}
			Dictionary<int, Panel>.Enumerator panelsIterator = UIElementsUtility.GetPanelsIterator();
			while (panelsIterator.MoveNext())
			{
				if (recreate)
				{
					KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
					AtlasBase atlas = keyValuePair.Value.atlas;
					if (atlas != null)
					{
						atlas.Reset();
					}
				}
				else
				{
					KeyValuePair<int, Panel> keyValuePair = panelsIterator.Current;
					keyValuePair.Value.panelRenderer.Reset();
				}
			}
			bool flag2 = !recreate;
			if (flag2)
			{
				UIRenderDevice.FlushAllPendingDeviceDisposes();
			}
			else
			{
				UIRenderDevice.WrapUpGfxDeviceRecreate();
			}
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0005B7EA File Offset: 0x000599EA
		private void OnPanelChanged(BaseVisualElementPanel obj)
		{
			this.DetachFromPanel();
			this.AttachToPanel();
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0005B7FC File Offset: 0x000599FC
		private void AttachToPanel()
		{
			Debug.Assert(this.attachedPanel == null);
			bool flag = base.panel == null;
			if (!flag)
			{
				this.attachedPanel = base.panel;
				this.attachedPanel.isFlatChanged += this.OnPanelIsFlatChanged;
				this.attachedPanel.atlasChanged += this.OnPanelAtlasChanged;
				this.attachedPanel.hierarchyChanged += this.OnPanelHierarchyChanged;
				Debug.Assert(this.attachedPanel.panelRenderer == null);
				this.attachedPanel.panelRenderer = this;
				BaseRuntimePanel baseRuntimePanel = base.panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					baseRuntimePanel.drawsInCamerasChanged += this.OnPanelDrawsInCamerasChanged;
				}
			}
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0005B8C8 File Offset: 0x00059AC8
		private void DetachFromPanel()
		{
			bool flag = this.attachedPanel == null;
			if (!flag)
			{
				this.DestroyRenderChain();
				BaseRuntimePanel baseRuntimePanel = base.panel as BaseRuntimePanel;
				bool flag2 = baseRuntimePanel != null;
				if (flag2)
				{
					baseRuntimePanel.drawsInCamerasChanged -= this.OnPanelDrawsInCamerasChanged;
				}
				this.attachedPanel.isFlatChanged -= this.OnPanelIsFlatChanged;
				this.attachedPanel.atlasChanged -= this.OnPanelAtlasChanged;
				this.attachedPanel.hierarchyChanged -= this.OnPanelHierarchyChanged;
				Debug.Assert(this.attachedPanel.panelRenderer == this);
				this.attachedPanel.panelRenderer = null;
				this.attachedPanel = null;
			}
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0005B985 File Offset: 0x00059B85
		private void InitRenderChain()
		{
			Debug.Assert(this.attachedPanel != null);
			this.renderChain = this.CreateRenderChain();
			this.renderChain.UIEOnChildAdded(this.attachedPanel.visualTree);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0005B9BA File Offset: 0x00059BBA
		public void Reset()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0005B9C4 File Offset: 0x00059BC4
		private void DestroyRenderChain()
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				this.renderChain.Dispose();
				this.renderChain = null;
				this.ResetAllElementsDataRecursive(this.attachedPanel.visualTree);
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0005BA06 File Offset: 0x00059C06
		private void OnPanelIsFlatChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0005BA06 File Offset: 0x00059C06
		private void OnPanelAtlasChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0005BA06 File Offset: 0x00059C06
		private void OnPanelDrawsInCamerasChanged()
		{
			this.DestroyRenderChain();
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0005BA10 File Offset: 0x00059C10
		private void OnPanelHierarchyChanged(VisualElement ve, HierarchyChangeType changeType, IReadOnlyList<VisualElement> additionalContext = null)
		{
			bool flag = this.renderChain == null;
			if (!flag)
			{
				switch (changeType)
				{
				case HierarchyChangeType.AddedToParent:
					this.renderChain.UIEOnChildAdded(ve);
					break;
				case HierarchyChangeType.RemovedFromParent:
					this.renderChain.UIEOnChildRemoving(ve);
					break;
				case HierarchyChangeType.ChildrenReordered:
					this.renderChain.UIEOnChildrenReordered(ve);
					break;
				}
			}
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0005BA74 File Offset: 0x00059C74
		private void ResetAllElementsDataRecursive(VisualElement ve)
		{
			ve.renderChainData = default(RenderChainVEData);
			int i = ve.hierarchy.childCount - 1;
			while (i >= 0)
			{
				this.ResetAllElementsDataRecursive(ve.hierarchy[i--]);
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x060017B4 RID: 6068 RVA: 0x0005BAC6 File Offset: 0x00059CC6
		// (set) Token: 0x060017B5 RID: 6069 RVA: 0x0005BACE File Offset: 0x00059CCE
		private protected bool disposed { protected get; private set; }

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005BAD8 File Offset: 0x00059CD8
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.DetachFromPanel();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000CEB RID: 3307
		private BaseVisualElementPanel attachedPanel;

		// Token: 0x04000CEC RID: 3308
		internal RenderChain renderChain;

		// Token: 0x04000CED RID: 3309
		private static readonly string s_Description = "Update Rendering";

		// Token: 0x04000CEE RID: 3310
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRRepaintUpdater.s_Description);

		// Token: 0x04000CEF RID: 3311
		private bool m_ForceGammaRendering;

		// Token: 0x04000CF0 RID: 3312
		private uint m_VertexBudget;
	}
}
