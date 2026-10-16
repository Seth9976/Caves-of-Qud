using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000086 RID: 134
	public class ParameterizedFixtureSuite : TestSuite
	{
		// Token: 0x0600052B RID: 1323 RVA: 0x0000BE1A File Offset: 0x0000A01A
		public ParameterizedFixtureSuite(ITypeInfo typeInfo)
			: base(typeInfo.Namespace, typeInfo.GetDisplayName())
		{
			this._genericFixture = typeInfo.ContainsGenericParameters;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000BE3A File Offset: 0x0000A03A
		public override string TestType
		{
			get
			{
				if (!this._genericFixture)
				{
					return "ParameterizedFixture";
				}
				return "GenericFixture";
			}
		}

		// Token: 0x04000101 RID: 257
		private bool _genericFixture;
	}
}
