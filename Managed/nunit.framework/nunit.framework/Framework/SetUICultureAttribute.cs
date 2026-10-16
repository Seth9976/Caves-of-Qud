using System;
using System.Globalization;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000034 RID: 52
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class SetUICultureAttribute : PropertyAttribute, IApplyToContext
	{
		// Token: 0x06000189 RID: 393 RVA: 0x00004A70 File Offset: 0x00002C70
		public SetUICultureAttribute(string culture)
			: base("SetUICulture", culture)
		{
			this._culture = culture;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004A85 File Offset: 0x00002C85
		void IApplyToContext.ApplyToContext(ITestExecutionContext context)
		{
			context.CurrentUICulture = new CultureInfo(this._culture);
		}

		// Token: 0x04000032 RID: 50
		private string _culture;
	}
}
