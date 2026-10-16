using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200034B RID: 843
	internal class InvokableCallList
	{
		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x0004D540 File Offset: 0x0004B740
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x0004D569 File Offset: 0x0004B769
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002825 RID: 10277 RVA: 0x0004D580 File Offset: 0x0004B780
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
			this.m_NeedsUpdate = true;
		}

		// Token: 0x06002826 RID: 10278 RVA: 0x0004D598 File Offset: 0x0004B798
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				bool flag = this.m_RuntimeCalls[i].Find(targetObj, method);
				if (flag)
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
			List<BaseInvokableCall> list2 = new List<BaseInvokableCall>(this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count);
			list2.AddRange(this.m_PersistentCalls);
			list2.AddRange(this.m_RuntimeCalls);
			this.m_ExecutingCalls = list2;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x0004D654 File Offset: 0x0004B854
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
			List<BaseInvokableCall> list = new List<BaseInvokableCall>(this.m_PersistentCalls);
			this.m_ExecutingCalls = list;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x0004D688 File Offset: 0x0004B888
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
			List<BaseInvokableCall> list = new List<BaseInvokableCall>(this.m_RuntimeCalls);
			this.m_ExecutingCalls = list;
			this.m_NeedsUpdate = false;
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x0004D6BC File Offset: 0x0004B8BC
		public List<BaseInvokableCall> PrepareInvoke()
		{
			bool needsUpdate = this.m_NeedsUpdate;
			if (needsUpdate)
			{
				this.m_ExecutingCalls.Clear();
				this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
				this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
				this.m_NeedsUpdate = false;
			}
			return this.m_ExecutingCalls;
		}

		// Token: 0x04000B48 RID: 2888
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x04000B49 RID: 2889
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x04000B4A RID: 2890
		private List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();

		// Token: 0x04000B4B RID: 2891
		private bool m_NeedsUpdate = true;
	}
}
