using System;
using System.Threading;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000030 RID: 48
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class RequiresThreadAttribute : PropertyAttribute, IApplyToTest
	{
		// Token: 0x06000181 RID: 385 RVA: 0x000049D1 File Offset: 0x00002BD1
		public RequiresThreadAttribute()
			: base(true)
		{
		}

		// Token: 0x06000182 RID: 386 RVA: 0x000049DF File Offset: 0x00002BDF
		public RequiresThreadAttribute(ApartmentState apartment)
			: base(true)
		{
			base.Properties.Add("ApartmentState", apartment);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004A03 File Offset: 0x00002C03
		void IApplyToTest.ApplyToTest(Test test)
		{
			test.RequiresThread = true;
			base.ApplyToTest(test);
		}
	}
}
