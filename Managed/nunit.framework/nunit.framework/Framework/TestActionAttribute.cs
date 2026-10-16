using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public abstract class TestActionAttribute : Attribute, ITestAction
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00003E5D File Offset: 0x0000205D
		public virtual void BeforeTest(ITest test)
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00003E5D File Offset: 0x0000205D
		public virtual void AfterTest(ITest test)
		{
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00004B9A File Offset: 0x00002D9A
		public virtual ActionTargets Targets
		{
			get
			{
				return ActionTargets.Default;
			}
		}
	}
}
