using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000344 RID: 836
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060027E9 RID: 10217 RVA: 0x0004C9B0 File Offset: 0x0004ABB0
		// (remove) Token: 0x060027EA RID: 10218 RVA: 0x0004C9E8 File Offset: 0x0004ABE8
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1, T2> Delegate;

		// Token: 0x060027EB RID: 10219 RVA: 0x0004CA1D File Offset: 0x0004AC1D
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction);
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x0004CA45 File Offset: 0x0004AC45
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate += action;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x0004CA58 File Offset: 0x0004AC58
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 2;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0004CABC File Offset: 0x0004ACBC
		public void Invoke(T1 args0, T2 args1)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0, args1);
			}
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x0004CAE8 File Offset: 0x0004ACE8
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
