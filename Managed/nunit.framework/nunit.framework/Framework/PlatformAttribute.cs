using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000029 RID: 41
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	public class PlatformAttribute : IncludeExcludeAttribute, IApplyToTest
	{
		// Token: 0x0600015C RID: 348 RVA: 0x00004061 File Offset: 0x00002261
		public PlatformAttribute()
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004074 File Offset: 0x00002274
		public PlatformAttribute(string platforms)
			: base(platforms)
		{
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004088 File Offset: 0x00002288
		public void ApplyToTest(Test test)
		{
			if (test.RunState != RunState.NotRunnable && test.RunState != RunState.Ignored && !this.platformHelper.IsPlatformSupported(this))
			{
				test.RunState = RunState.Skipped;
				test.Properties.Add("_SKIPREASON", this.platformHelper.Reason);
			}
		}

		// Token: 0x0400002B RID: 43
		private PlatformHelper platformHelper = new PlatformHelper();
	}
}
