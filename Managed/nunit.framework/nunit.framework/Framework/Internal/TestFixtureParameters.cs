using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000081 RID: 129
	public class TestFixtureParameters : TestParameters, ITestFixtureData, ITestData
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x0000AB6E File Offset: 0x00008D6E
		public TestFixtureParameters()
		{
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0000AB76 File Offset: 0x00008D76
		public TestFixtureParameters(Exception exception)
			: base(exception)
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0000AB7F File Offset: 0x00008D7F
		public TestFixtureParameters(params object[] args)
			: base(args)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000B49B File Offset: 0x0000969B
		public TestFixtureParameters(ITestFixtureData data)
			: base(data)
		{
			this.TypeArgs = data.TypeArgs;
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000B4B0 File Offset: 0x000096B0
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000B4B8 File Offset: 0x000096B8
		public Type[] TypeArgs { get; internal set; }
	}
}
