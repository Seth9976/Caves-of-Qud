using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class ParallelizableAttribute : PropertyAttribute, IApplyToContext
	{
		// Token: 0x06000159 RID: 345 RVA: 0x00004022 File Offset: 0x00002222
		public ParallelizableAttribute()
			: this(ParallelScope.Self)
		{
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000402B File Offset: 0x0000222B
		public ParallelizableAttribute(ParallelScope scope)
		{
			this._scope = scope;
			base.Properties.Add("ParallelScope", scope);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004050 File Offset: 0x00002250
		public void ApplyToContext(ITestExecutionContext context)
		{
			context.ParallelScope = this._scope & ~ParallelScope.Self;
		}

		// Token: 0x04000025 RID: 37
		private ParallelScope _scope;
	}
}
