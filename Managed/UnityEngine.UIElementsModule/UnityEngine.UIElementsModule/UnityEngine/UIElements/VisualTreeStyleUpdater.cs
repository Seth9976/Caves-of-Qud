using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D3 RID: 1491
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class VisualTreeStyleUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x0009EAEC File Offset: 0x0009CCEC
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x0009EAF4 File Offset: 0x0009CCF4
		public VisualTreeStyleUpdaterTraversal traversal
		{
			get
			{
				return this.m_StyleContextHierarchyTraversal;
			}
			set
			{
				this.m_StyleContextHierarchyTraversal = value;
				BaseVisualElementPanel panel = base.panel;
				if (panel != null)
				{
					panel.visualTree.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Styles | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x0009EB1A File Offset: 0x0009CD1A
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualTreeStyleUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x0009EB24 File Offset: 0x0009CD24
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.StyleSheet | VersionChangeType.TransitionProperty)) == (VersionChangeType)0;
			if (!flag)
			{
				this.m_Version += 1U;
				bool flag2 = (versionChangeType & VersionChangeType.StyleSheet) > (VersionChangeType)0;
				if (flag2)
				{
					bool isApplyingStyles = this.m_IsApplyingStyles;
					if (isApplyingStyles)
					{
						this.m_ApplyStyleUpdateList.Add(ve);
					}
					else
					{
						this.m_StyleContextHierarchyTraversal.AddChangedElement(ve, versionChangeType);
					}
				}
				bool flag3 = (versionChangeType & VersionChangeType.TransitionProperty) > (VersionChangeType)0;
				if (flag3)
				{
					this.m_TransitionPropertyUpdateList.Add(ve);
				}
			}
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x0009EBA8 File Offset: 0x0009CDA8
		public override void Update()
		{
			bool flag = this.m_Version == this.m_LastVersion;
			if (!flag)
			{
				this.m_LastVersion = this.m_Version;
				this.ApplyStyles();
				this.m_StyleContextHierarchyTraversal.Clear();
				foreach (VisualElement visualElement in this.m_ApplyStyleUpdateList)
				{
					this.m_StyleContextHierarchyTraversal.AddChangedElement(visualElement, VersionChangeType.StyleSheet);
				}
				this.m_ApplyStyleUpdateList.Clear();
				foreach (VisualElement visualElement2 in this.m_TransitionPropertyUpdateList)
				{
					bool flag2 = visualElement2.hasRunningAnimations || visualElement2.hasCompletedAnimations;
					if (flag2)
					{
						ComputedTransitionUtils.UpdateComputedTransitions(visualElement2.computedStyle);
						this.m_StyleContextHierarchyTraversal.CancelAnimationsWithNoTransitionProperty(visualElement2, visualElement2.computedStyle);
					}
				}
				this.m_TransitionPropertyUpdateList.Clear();
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x0009ECD4 File Offset: 0x0009CED4
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x0009ECDC File Offset: 0x0009CEDC
		private protected bool disposed { protected get; private set; }

		// Token: 0x06002BFC RID: 11260 RVA: 0x0009ECE8 File Offset: 0x0009CEE8
		protected override void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					this.m_StyleContextHierarchyTraversal.Clear();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x0009ED1C File Offset: 0x0009CF1C
		private void ApplyStyles()
		{
			Debug.Assert(base.visualTree.panel != null);
			this.m_IsApplyingStyles = true;
			this.m_StyleContextHierarchyTraversal.PrepareTraversal(base.panel, base.panel.scaledPixelsPerPoint);
			this.m_StyleContextHierarchyTraversal.Traverse(base.visualTree);
			this.m_IsApplyingStyles = false;
		}

		// Token: 0x04001416 RID: 5142
		private HashSet<VisualElement> m_ApplyStyleUpdateList = new HashSet<VisualElement>();

		// Token: 0x04001417 RID: 5143
		private HashSet<VisualElement> m_TransitionPropertyUpdateList = new HashSet<VisualElement>();

		// Token: 0x04001418 RID: 5144
		private bool m_IsApplyingStyles = false;

		// Token: 0x04001419 RID: 5145
		private uint m_Version = 0U;

		// Token: 0x0400141A RID: 5146
		private uint m_LastVersion = 0U;

		// Token: 0x0400141B RID: 5147
		private VisualTreeStyleUpdaterTraversal m_StyleContextHierarchyTraversal = new VisualTreeStyleUpdaterTraversal();

		// Token: 0x0400141C RID: 5148
		private static readonly string s_Description = "Update Style";

		// Token: 0x0400141D RID: 5149
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualTreeStyleUpdater.s_Description);
	}
}
