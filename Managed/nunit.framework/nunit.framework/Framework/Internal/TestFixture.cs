using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200008B RID: 139
	public class TestFixture : TestSuite, IDisposableFixture
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0000C364 File Offset: 0x0000A564
		public TestFixture(ITypeInfo fixtureType)
			: base(fixtureType)
		{
			base.CheckSetUpTearDownMethods(typeof(OneTimeSetUpAttribute));
			base.CheckSetUpTearDownMethods(typeof(OneTimeTearDownAttribute));
			base.CheckSetUpTearDownMethods(typeof(SetUpAttribute));
			base.CheckSetUpTearDownMethods(typeof(TearDownAttribute));
		}
	}
}
