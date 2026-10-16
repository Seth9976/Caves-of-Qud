using System;
using System.Collections.Generic;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000015 RID: 21
	internal class AccessibilityHierarchyService : IService
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00004E78 File Offset: 0x00003078
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00004E30 File Offset: 0x00003030
		internal AccessibilityHierarchy hierarchy
		{
			get
			{
				return this.m_Hierarchy;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					this.RemoveActiveHierarchy(true);
				}
				else
				{
					this.RemoveActiveHierarchy(false);
					this.m_Hierarchy = value;
					this.m_Hierarchy.AllocateNative();
					AssistiveSupport.notificationDispatcher.SendScreenChanged(null);
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004E80 File Offset: 0x00003080
		public void Start()
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004E84 File Offset: 0x00003084
		public void Stop()
		{
			bool flag = this.m_Hierarchy == null;
			if (!flag)
			{
				this.RemoveActiveHierarchy(true);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004EAC File Offset: 0x000030AC
		private void RemoveActiveHierarchy(bool notifyScreenChanged)
		{
			bool flag = this.m_Hierarchy == null;
			if (!flag)
			{
				this.m_Hierarchy.FreeNative();
				this.m_Hierarchy = null;
				if (notifyScreenChanged)
				{
					AssistiveSupport.notificationDispatcher.SendScreenChanged(null);
				}
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004EF0 File Offset: 0x000030F0
		internal bool TryGetNode(int id, out AccessibilityNode node)
		{
			node = null;
			return this.m_Hierarchy != null && this.m_Hierarchy.TryGetNode(id, out node);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004F20 File Offset: 0x00003120
		internal List<AccessibilityNode> GetRootNodes()
		{
			AccessibilityHierarchy hierarchy = this.m_Hierarchy;
			return (hierarchy != null) ? hierarchy.m_RootNodes : null;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004F44 File Offset: 0x00003144
		internal bool TryGetNodeAt(float x, float y, out AccessibilityNode node)
		{
			node = null;
			return this.m_Hierarchy != null && this.m_Hierarchy.TryGetNodeAt(x, y, out node);
		}

		// Token: 0x0400006B RID: 107
		private AccessibilityHierarchy m_Hierarchy;
	}
}
