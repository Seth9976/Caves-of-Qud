using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005C0 RID: 1472
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		// Token: 0x06003F46 RID: 16198 RVA: 0x000A5473 File Offset: 0x000A3673
		public override bool CanValidate(Type t)
		{
			return t == typeof(TimeSpan);
		}

		// Token: 0x06003F47 RID: 16199 RVA: 0x000A5488 File Offset: 0x000A3688
		public override void Validate(object value)
		{
			if (((TimeSpan)value).Ticks <= 0L)
			{
				throw new ConfigurationErrorsException("TimeSpan value must be positive.");
			}
		}
	}
}
