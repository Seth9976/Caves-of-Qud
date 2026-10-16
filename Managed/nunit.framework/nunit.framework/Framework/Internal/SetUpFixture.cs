using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000088 RID: 136
	public class SetUpFixture : TestSuite, IDisposableFixture
	{
		// Token: 0x0600052F RID: 1327 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		public SetUpFixture(ITypeInfo type)
			: base(type)
		{
			base.Name = type.Namespace;
			if (base.Name == null)
			{
				base.Name = "[default namespace]";
			}
			int num = base.Name.LastIndexOf('.');
			if (num > 0)
			{
				base.Name = base.Name.Substring(num + 1);
			}
			base.CheckSetUpTearDownMethods(typeof(OneTimeSetUpAttribute));
			base.CheckSetUpTearDownMethods(typeof(OneTimeTearDownAttribute));
		}
	}
}
