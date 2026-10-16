using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000342 RID: 834
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060027DB RID: 10203 RVA: 0x0004C720 File Offset: 0x0004A920
		// (remove) Token: 0x060027DC RID: 10204 RVA: 0x0004C758 File Offset: 0x0004A958
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private event UnityAction Delegate;

		// Token: 0x060027DD RID: 10205 RVA: 0x0004C78D File Offset: 0x0004A98D
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction)global::System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction);
		}

		// Token: 0x060027DE RID: 10206 RVA: 0x0004C7B6 File Offset: 0x0004A9B6
		public InvokableCall(UnityAction action)
		{
			this.Delegate += action;
		}

		// Token: 0x060027DF RID: 10207 RVA: 0x0004C7C8 File Offset: 0x0004A9C8
		public override void Invoke(object[] args)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x060027E0 RID: 10208 RVA: 0x0004C7F4 File Offset: 0x0004A9F4
		public void Invoke()
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate();
			}
		}

		// Token: 0x060027E1 RID: 10209 RVA: 0x0004C820 File Offset: 0x0004AA20
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
