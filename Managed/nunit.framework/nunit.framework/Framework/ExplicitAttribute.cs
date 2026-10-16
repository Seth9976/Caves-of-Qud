using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200001B RID: 27
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class ExplicitAttribute : NUnitAttribute, IApplyToTest
	{
		// Token: 0x0600013C RID: 316 RVA: 0x00003DE1 File Offset: 0x00001FE1
		public ExplicitAttribute()
		{
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003DFF File Offset: 0x00001FFF
		public ExplicitAttribute(string reason)
		{
			this._reason = reason;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003E0E File Offset: 0x0000200E
		public void ApplyToTest(Test test)
		{
			if (test.RunState != RunState.NotRunnable && test.RunState != RunState.Ignored)
			{
				test.RunState = RunState.Explicit;
				if (this._reason != null)
				{
					test.Properties.Set("_SKIPREASON", this._reason);
				}
			}
		}

		// Token: 0x0400001C RID: 28
		private string _reason;
	}
}
