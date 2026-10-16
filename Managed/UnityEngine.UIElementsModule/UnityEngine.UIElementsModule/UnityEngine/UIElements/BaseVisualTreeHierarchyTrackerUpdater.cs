using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D0 RID: 1488
	internal abstract class BaseVisualTreeHierarchyTrackerUpdater : BaseVisualTreeUpdater
	{
		// Token: 0x06002BE5 RID: 11237
		protected abstract void OnHierarchyChange(VisualElement ve, HierarchyChangeType type);

		// Token: 0x06002BE6 RID: 11238
		internal abstract void PollElementsWithBindings(Action<VisualElement, IBinding> callback);

		// Token: 0x06002BE7 RID: 11239 RVA: 0x0009E7E4 File Offset: 0x0009C9E4
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			bool flag = (versionChangeType & VersionChangeType.Hierarchy) == VersionChangeType.Hierarchy;
			if (flag)
			{
				switch (this.m_State)
				{
				case BaseVisualTreeHierarchyTrackerUpdater.State.Waiting:
					this.ProcessNewChange(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove:
					this.ProcessAddOrMove(ve);
					break;
				case BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove:
					this.ProcessRemove(ve);
					break;
				}
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x0009E83C File Offset: 0x0009CA3C
		public override void Update()
		{
			Debug.Assert(this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove || this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.Waiting);
			bool flag = this.m_State == BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.ChildrenReordered);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			this.m_CurrentChangeElement = null;
			this.m_CurrentChangeParent = null;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x0009E898 File Offset: 0x0009CA98
		private void ProcessNewChange(VisualElement ve)
		{
			this.m_CurrentChangeElement = ve;
			this.m_CurrentChangeParent = ve.parent;
			bool flag = this.m_CurrentChangeParent == null && ve.panel != null;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.ChildrenReordered);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_State = ((this.m_CurrentChangeParent == null) ? BaseVisualTreeHierarchyTrackerUpdater.State.TrackingRemove : BaseVisualTreeHierarchyTrackerUpdater.State.TrackingAddOrMove);
			}
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x0009E900 File Offset: 0x0009CB00
		private void ProcessAddOrMove(VisualElement ve)
		{
			Debug.Assert(this.m_CurrentChangeParent != null);
			bool flag = this.m_CurrentChangeParent == ve;
			if (flag)
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.AddedToParent);
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.ChildrenReordered);
				this.ProcessNewChange(ve);
			}
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x0009E95C File Offset: 0x0009CB5C
		private void ProcessRemove(VisualElement ve)
		{
			this.OnHierarchyChange(this.m_CurrentChangeElement, HierarchyChangeType.RemovedFromParent);
			bool flag = ve.panel != null;
			if (flag)
			{
				this.m_CurrentChangeParent = null;
				this.m_CurrentChangeElement = null;
				this.m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;
			}
			else
			{
				this.m_CurrentChangeElement = ve;
			}
		}

		// Token: 0x0400140C RID: 5132
		private BaseVisualTreeHierarchyTrackerUpdater.State m_State = BaseVisualTreeHierarchyTrackerUpdater.State.Waiting;

		// Token: 0x0400140D RID: 5133
		private VisualElement m_CurrentChangeElement;

		// Token: 0x0400140E RID: 5134
		private VisualElement m_CurrentChangeParent;

		// Token: 0x020005D1 RID: 1489
		private enum State
		{
			// Token: 0x04001410 RID: 5136
			Waiting,
			// Token: 0x04001411 RID: 5137
			TrackingAddOrMove,
			// Token: 0x04001412 RID: 5138
			TrackingRemove
		}
	}
}
