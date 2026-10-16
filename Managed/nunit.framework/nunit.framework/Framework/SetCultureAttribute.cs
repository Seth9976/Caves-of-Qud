using System;
using System.Globalization;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000033 RID: 51
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class SetCultureAttribute : PropertyAttribute, IApplyToContext
	{
		// Token: 0x06000187 RID: 391 RVA: 0x00004A48 File Offset: 0x00002C48
		public SetCultureAttribute(string culture)
			: base("SetCulture", culture)
		{
			this._culture = culture;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004A5D File Offset: 0x00002C5D
		void IApplyToContext.ApplyToContext(ITestExecutionContext context)
		{
			context.CurrentCulture = new CultureInfo(this._culture);
		}

		// Token: 0x04000031 RID: 49
		private string _culture;
	}
}
