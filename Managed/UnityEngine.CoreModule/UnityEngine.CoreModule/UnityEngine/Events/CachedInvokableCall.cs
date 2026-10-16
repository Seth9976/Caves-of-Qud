using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000347 RID: 839
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x060027FE RID: 10238 RVA: 0x0004CE2C File Offset: 0x0004B02C
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument)
			: base(target, theFunction)
		{
			this.m_Arg1 = argument;
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0004CE3F File Offset: 0x0004B03F
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0004CE3F File Offset: 0x0004B03F
		public override void Invoke(T arg0)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x04000B3C RID: 2876
		private readonly T m_Arg1;
	}
}
