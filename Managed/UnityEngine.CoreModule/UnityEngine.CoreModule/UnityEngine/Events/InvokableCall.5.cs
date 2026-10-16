using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000346 RID: 838
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060027F7 RID: 10231 RVA: 0x0004CC98 File Offset: 0x0004AE98
		// (remove) Token: 0x060027F8 RID: 10232 RVA: 0x0004CCD0 File Offset: 0x0004AED0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		// Token: 0x060027F9 RID: 10233 RVA: 0x0004CD05 File Offset: 0x0004AF05
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction);
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0004CD2D File Offset: 0x0004AF2D
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate += action;
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0004CD40 File Offset: 0x0004AF40
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 4;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0004CDC8 File Offset: 0x0004AFC8
		public void Invoke(T1 args0, T2 args1, T3 args2, T4 args3)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2, args3);
			}
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0004CDF8 File Offset: 0x0004AFF8
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
