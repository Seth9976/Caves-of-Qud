using System;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x0200005D RID: 93
	public class TestFixtureData : TestFixtureParameters
	{
		// Token: 0x06000322 RID: 802 RVA: 0x000070C2 File Offset: 0x000052C2
		public TestFixtureData(params object[] args)
			: base((args == null) ? new object[1] : args)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000070D6 File Offset: 0x000052D6
		public TestFixtureData(object arg)
			: base(new object[] { arg })
		{
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000070E8 File Offset: 0x000052E8
		public TestFixtureData(object arg1, object arg2)
			: base(new object[] { arg1, arg2 })
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000070FE File Offset: 0x000052FE
		public TestFixtureData(object arg1, object arg2, object arg3)
			: base(new object[] { arg1, arg2, arg3 })
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00006D6D File Offset: 0x00004F6D
		public TestFixtureData Explicit()
		{
			base.RunState = RunState.Explicit;
			return this;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00006D77 File Offset: 0x00004F77
		public TestFixtureData Explicit(string reason)
		{
			base.RunState = RunState.Explicit;
			base.Properties.Set("_SKIPREASON", reason);
			return this;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00006D92 File Offset: 0x00004F92
		public TestFixtureData Ignore(string reason)
		{
			base.RunState = RunState.Ignored;
			base.Properties.Set("_SKIPREASON", reason);
			return this;
		}
	}
}
