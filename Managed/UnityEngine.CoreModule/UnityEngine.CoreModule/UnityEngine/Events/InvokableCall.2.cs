using System;
using System.Diagnostics;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000343 RID: 835
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060027E2 RID: 10210 RVA: 0x0004C854 File Offset: 0x0004AA54
		// (remove) Token: 0x060027E3 RID: 10211 RVA: 0x0004C88C File Offset: 0x0004AA8C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		protected event UnityAction<T1> Delegate;

		// Token: 0x060027E4 RID: 10212 RVA: 0x0004C8C1 File Offset: 0x0004AAC1
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate += (UnityAction<T1>)global::System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x0004C8EA File Offset: 0x0004AAEA
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate += action;
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x0004C8FC File Offset: 0x0004AAFC
		public override void Invoke(object[] args)
		{
			bool flag = args.Length != 1;
			if (flag)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			bool flag2 = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag2)
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0004C950 File Offset: 0x0004AB50
		public virtual void Invoke(T1 args0)
		{
			bool flag = BaseInvokableCall.AllowInvoke(this.Delegate);
			if (flag)
			{
				this.Delegate(args0);
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0004C97C File Offset: 0x0004AB7C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method.Equals(method);
		}
	}
}
