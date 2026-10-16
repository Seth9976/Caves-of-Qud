using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class TimeoutAttribute : PropertyAttribute, IApplyToContext
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00005DDE File Offset: 0x00003FDE
		public TimeoutAttribute(int timeout)
			: base(timeout)
		{
			this._timeout = timeout;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005DF3 File Offset: 0x00003FF3
		void IApplyToContext.ApplyToContext(ITestExecutionContext context)
		{
			context.TestCaseTimeout = this._timeout;
		}

		// Token: 0x04000057 RID: 87
		private int _timeout;
	}
}
