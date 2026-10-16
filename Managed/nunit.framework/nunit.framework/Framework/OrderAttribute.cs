using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class OrderAttribute : NUnitAttribute, IApplyToTest
	{
		// Token: 0x06000156 RID: 342 RVA: 0x00003FD2 File Offset: 0x000021D2
		public OrderAttribute(int order)
		{
			this.Order = order;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00003FE1 File Offset: 0x000021E1
		public void ApplyToTest(Test test)
		{
			if (!test.Properties.ContainsKey("Order"))
			{
				test.Properties.Set("Order", this.Order);
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly int Order;
	}
}
