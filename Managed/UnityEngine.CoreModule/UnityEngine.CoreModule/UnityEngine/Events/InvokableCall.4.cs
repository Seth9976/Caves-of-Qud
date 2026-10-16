using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000345 RID: 837
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x060027F0 RID: 10224 RVA: 0x0004CB1C File Offset: 0x0004AD1C
		// (remove) Token: 0x060027F1 RID: 10225 RVA: 0x0004CB54 File Offset: 0x0004AD54
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x060027F2 RID: 10226 RVA: 0x0004CB89 File Offset: 0x0004AD89
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0004CBB1 File Offset: 0x0004ADB1
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate += action;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 3;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x0004CC38 File Offset: 0x0004AE38
		public void Invoke(T1 args0, T2 args1, T3 args2)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1, args2);
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x0004CC64 File Offset: 0x0004AE64
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
