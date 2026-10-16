using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200005B RID: 91
	public class TestCaseData : TestCaseParameters
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00006CA1 File Offset: 0x00004EA1
		public TestCaseData(params object[] args)
			: base((args == null) ? new object[1] : args)
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006CB5 File Offset: 0x00004EB5
		public TestCaseData(object arg)
			: base(new object[] { arg })
		{
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00006CC7 File Offset: 0x00004EC7
		public TestCaseData(object arg1, object arg2)
			: base(new object[] { arg1, arg2 })
		{
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00006CDD File Offset: 0x00004EDD
		public TestCaseData(object arg1, object arg2, object arg3)
			: base(new object[] { arg1, arg2, arg3 })
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00006CF7 File Offset: 0x00004EF7
		public TestCaseData Returns(object result)
		{
			base.ExpectedResult = result;
			return this;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00006D01 File Offset: 0x00004F01
		public TestCaseData SetName(string name)
		{
			base.TestName = name;
			return this;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00006D0B File Offset: 0x00004F0B
		public TestCaseData SetDescription(string description)
		{
			base.Properties.Set("Description", description);
			return this;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006D1F File Offset: 0x00004F1F
		public TestCaseData SetCategory(string category)
		{
			base.Properties.Add("Category", category);
			return this;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00006D33 File Offset: 0x00004F33
		public TestCaseData SetProperty(string propName, string propValue)
		{
			base.Properties.Add(propName, propValue);
			return this;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00006D43 File Offset: 0x00004F43
		public TestCaseData SetProperty(string propName, int propValue)
		{
			base.Properties.Add(propName, propValue);
			return this;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00006D58 File Offset: 0x00004F58
		public TestCaseData SetProperty(string propName, double propValue)
		{
			base.Properties.Add(propName, propValue);
			return this;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00006D6D File Offset: 0x00004F6D
		public TestCaseData Explicit()
		{
			base.RunState = RunState.Explicit;
			return this;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00006D77 File Offset: 0x00004F77
		public TestCaseData Explicit(string reason)
		{
			base.RunState = RunState.Explicit;
			base.Properties.Set("_SKIPREASON", reason);
			return this;
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00006D92 File Offset: 0x00004F92
		public TestCaseData Ignore(string reason)
		{
			base.RunState = RunState.Ignored;
			base.Properties.Set("_SKIPREASON", reason);
			return this;
		}
	}
}
