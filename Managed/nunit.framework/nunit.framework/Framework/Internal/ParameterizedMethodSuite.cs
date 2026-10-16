using System;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000087 RID: 135
	public class ParameterizedMethodSuite : TestSuite
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0000BE4F File Offset: 0x0000A04F
		public ParameterizedMethodSuite(IMethodInfo method)
			: base(method.TypeInfo.FullName, method.Name)
		{
			base.Method = method;
			this._isTheory = method.IsDefined<TheoryAttribute>(true);
			base.MaintainTestOrder = true;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000BE83 File Offset: 0x0000A083
		public override string TestType
		{
			get
			{
				if (this._isTheory)
				{
					return "Theory";
				}
				if (base.Method.ContainsGenericParameters)
				{
					return "GenericMethod";
				}
				return "ParameterizedMethod";
			}
		}

		// Token: 0x04000102 RID: 258
		private bool _isTheory;
	}
}
