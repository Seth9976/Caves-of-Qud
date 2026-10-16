using System;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine.Bindings;
using UnityEngine.UIElements.Layout;

namespace UnityEngine.UIElements
{
	// Token: 0x02000321 RID: 801
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UIRLayoutUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0005958E File Offset: 0x0005778E
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return UIRLayoutUpdater.s_ProfilerMarker;
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00059598 File Offset: 0x00057798
		public unsafe override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & (VersionChangeType.Hierarchy | VersionChangeType.Layout)) == (VersionChangeType)0;
			if (!flag)
			{
				LayoutNode layoutNode = *ve.layoutNode;
				bool isMeasureDefined = layoutNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					layoutNode.MarkDirty();
				}
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000595D4 File Offset: 0x000577D4
		public override void Update()
		{
			int num = 0;
			bool isDirty = base.visualTree.layoutNode.IsDirty;
			if (isDirty)
			{
				this.missedHierarchyChangeEventsList.Clear();
				while (base.visualTree.layoutNode.IsDirty)
				{
					this.changeEventsList.Clear();
					bool flag = num > 0;
					if (flag)
					{
						base.panel.ApplyStyles();
					}
					base.panel.duringLayoutPhase = true;
					base.visualTree.layoutNode.CalculateLayout(float.NaN, float.NaN);
					base.panel.duringLayoutPhase = false;
					this.UpdateSubTree(base.visualTree, this.changeEventsList);
					this.DispatchChangeEvents(this.changeEventsList, num);
					bool flag2 = !base.visualTree.layoutNode.IsDirty;
					if (flag2)
					{
						this.DispatchMissedHierarchyChangeEvents(this.missedHierarchyChangeEventsList, num);
						this.missedHierarchyChangeEventsList.Clear();
					}
					bool flag3 = num++ >= 10;
					if (flag3)
					{
						string text = "Layout update is struggling to process current layout (consider simplifying to avoid recursive layout): ";
						VisualElement visualTree = base.visualTree;
						Debug.LogError(text + ((visualTree != null) ? visualTree.ToString() : null));
						break;
					}
				}
			}
			base.visualTree.focusController.ReevaluateFocus();
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0005971C File Offset: 0x0005791C
		private static bool UpdateHierarchyDisplayed(VisualElement ve, List<ValueTuple<Rect, Rect, VisualElement>> changeEvents, bool inheritedDisplayed = true)
		{
			bool flag = inheritedDisplayed & (ve.resolvedStyle.display != DisplayStyle.None);
			bool flag2 = inheritedDisplayed && !flag;
			if (flag2)
			{
				ve.disableRendering = true;
			}
			else
			{
				bool flag3 = flag;
				if (flag3)
				{
					ve.disableRendering = false;
				}
			}
			bool flag4 = ve.areAncestorsAndSelfDisplayed == flag;
			bool flag5;
			if (flag4)
			{
				flag5 = false;
			}
			else
			{
				ve.areAncestorsAndSelfDisplayed = flag;
				bool flag6 = !flag;
				if (flag6)
				{
					if (inheritedDisplayed)
					{
						ve.IncrementVersion(VersionChangeType.Size);
					}
					bool flag7 = ve.HasSelfEventInterests(EventBase<GeometryChangedEvent>.EventCategory);
					if (flag7)
					{
						changeEvents.Add(new ValueTuple<Rect, Rect, VisualElement>(ve.lastLayout, Rect.zero, ve));
					}
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						UIRLayoutUpdater.UpdateHierarchyDisplayed(ve.hierarchy[i], changeEvents, flag);
					}
				}
				flag5 = true;
			}
			return flag5;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00059818 File Offset: 0x00057A18
		private void UpdateSubTree(VisualElement ve, List<ValueTuple<Rect, Rect, VisualElement>> changeEvents)
		{
			bool flag = UIRLayoutUpdater.UpdateHierarchyDisplayed(ve, changeEvents, true);
			bool flag2 = !ve.areAncestorsAndSelfDisplayed;
			if (!flag2)
			{
				Rect rect = new Rect(ve.layoutNode.LayoutX, ve.layoutNode.LayoutY, ve.layoutNode.LayoutWidth, ve.layoutNode.LayoutHeight);
				Rect rect2 = new Rect(ve.layoutNode.LayoutPaddingLeft, ve.layoutNode.LayoutPaddingLeft, ve.layoutNode.LayoutPaddingRight, ve.layoutNode.LayoutPaddingBottom);
				Rect rect3 = new Rect(rect2.x, rect2.y, rect.width - (rect2.x + rect2.width), rect.height - (rect2.y + rect2.height));
				Rect lastLayout = ve.lastLayout;
				Rect lastPseudoPadding = ve.lastPseudoPadding;
				VersionChangeType versionChangeType = (VersionChangeType)0;
				bool flag3 = lastLayout.size != rect.size;
				bool flag4 = lastPseudoPadding.size != rect3.size;
				bool flag5 = flag3 || flag4;
				if (flag5)
				{
					versionChangeType |= VersionChangeType.Size | VersionChangeType.Repaint;
				}
				bool flag6 = rect.position != lastLayout.position;
				bool flag7 = rect3.position != lastPseudoPadding.position;
				bool flag8 = flag6 || flag7 || flag;
				if (flag8)
				{
					versionChangeType |= VersionChangeType.Transform;
				}
				bool flag9 = flag;
				if (flag9)
				{
					versionChangeType |= VersionChangeType.Size;
				}
				bool flag10 = (versionChangeType & (VersionChangeType.Transform | VersionChangeType.Size)) == VersionChangeType.Size;
				if (flag10)
				{
					bool flag11 = !ve.hasDefaultRotationAndScale;
					if (flag11)
					{
						bool flag12 = !Mathf.Approximately(ve.resolvedStyle.transformOrigin.x, 0f) || !Mathf.Approximately(ve.resolvedStyle.transformOrigin.y, 0f);
						if (flag12)
						{
							versionChangeType |= VersionChangeType.Transform;
						}
					}
				}
				bool flag13 = versionChangeType > (VersionChangeType)0;
				if (flag13)
				{
					ve.IncrementVersion(versionChangeType);
				}
				ve.lastLayout = rect;
				ve.lastPseudoPadding = rect3;
				bool hasNewLayout = ve.layoutNode.HasNewLayout;
				bool flag14 = hasNewLayout;
				if (flag14)
				{
					int childCount = ve.hierarchy.childCount;
					for (int i = 0; i < childCount; i++)
					{
						VisualElement visualElement = ve.hierarchy[i];
						bool hasNewLayout2 = visualElement.layoutNode.HasNewLayout;
						if (hasNewLayout2)
						{
							this.UpdateSubTree(visualElement, changeEvents);
						}
					}
				}
				bool flag15 = ve.HasSelfEventInterests(EventBase<GeometryChangedEvent>.EventCategory);
				if (flag15)
				{
					bool flag16 = flag3 || flag6 || flag;
					if (flag16)
					{
						changeEvents.Add(new ValueTuple<Rect, Rect, VisualElement>(flag ? Rect.zero : lastLayout, rect, ve));
						bool receivesHierarchyGeometryChangedEvents = ve.receivesHierarchyGeometryChangedEvents;
						if (receivesHierarchyGeometryChangedEvents)
						{
							this.missedHierarchyChangeEventsList.Remove(ve);
						}
					}
					else
					{
						bool flag17 = ve.receivesHierarchyGeometryChangedEvents && ve.boundingBoxDirtiedSinceLastLayoutPass;
						if (flag17)
						{
							this.missedHierarchyChangeEventsList.Add(ve);
						}
					}
				}
				ve.boundingBoxDirtiedSinceLastLayoutPass = false;
				bool flag18 = hasNewLayout;
				if (flag18)
				{
					ve.layoutNode.MarkLayoutSeen();
				}
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00059B34 File Offset: 0x00057D34
		private void DispatchChangeEvents(List<ValueTuple<Rect, Rect, VisualElement>> changeEvents, int currentLayoutPass)
		{
			foreach (ValueTuple<Rect, Rect, VisualElement> valueTuple in changeEvents)
			{
				Rect item = valueTuple.Item1;
				Rect item2 = valueTuple.Item2;
				VisualElement item3 = valueTuple.Item3;
				using (GeometryChangedEvent pooled = GeometryChangedEvent.GetPooled(item, item2))
				{
					pooled.layoutPass = currentLayoutPass;
					pooled.elementTarget = item3;
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(pooled, base.panel, item3);
				}
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00059BDC File Offset: 0x00057DDC
		private void DispatchMissedHierarchyChangeEvents(List<VisualElement> missedHierarchyChangeEvents, int currentLayoutPass)
		{
			foreach (VisualElement visualElement in missedHierarchyChangeEvents)
			{
				using (GeometryChangedEvent pooled = GeometryChangedEvent.GetPooled(Rect.zero, visualElement.layout))
				{
					pooled.layoutPass = currentLayoutPass;
					pooled.elementTarget = visualElement;
					EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(pooled, base.panel, visualElement);
				}
			}
		}

		// Token: 0x04000CA4 RID: 3236
		public const int kMaxValidateLayoutCount = 10;

		// Token: 0x04000CA5 RID: 3237
		private static readonly string s_Description = "Update Layout";

		// Token: 0x04000CA6 RID: 3238
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(UIRLayoutUpdater.s_Description);

		// Token: 0x04000CA7 RID: 3239
		private static readonly ProfilerMarker k_ComputeLayoutMarker = new ProfilerMarker("LayoutUpdater.ComputeLayout");

		// Token: 0x04000CA8 RID: 3240
		private static readonly ProfilerMarker k_UpdateSubTreeMarker = new ProfilerMarker("LayoutUpdater.UpdateSubTree");

		// Token: 0x04000CA9 RID: 3241
		private static readonly ProfilerMarker k_DispatchChangeEventsMarker = new ProfilerMarker("LayoutUpdater.DispatchChangeEvents");

		// Token: 0x04000CAA RID: 3242
		private List<ValueTuple<Rect, Rect, VisualElement>> changeEventsList = new List<ValueTuple<Rect, Rect, VisualElement>>();

		// Token: 0x04000CAB RID: 3243
		private List<VisualElement> missedHierarchyChangeEventsList = new List<VisualElement>();
	}
}
