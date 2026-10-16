using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200056F RID: 1391
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class RegexMatchTimeoutValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x0009C768 File Offset: 0x0009A968
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new RegexMatchTimeoutValidator();
			}
		}
	}
}
