using System;

namespace Unity.Services.Core.Environments
{
	// Token: 0x02000003 RID: 3
	public static class EnvironmentsOptionsExtensions
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020B8 File Offset: 0x000002B8
		public static InitializationOptions SetEnvironmentName(this InitializationOptions self, string environmentName)
		{
			if (string.IsNullOrEmpty(environmentName))
			{
				throw new ArgumentException("Environment name cannot be null or empty.", "environmentName");
			}
			self.SetOption("com.unity.services.core.environment-name", environmentName);
			return self;
		}

		// Token: 0x04000001 RID: 1
		internal const string EnvironmentNameKey = "com.unity.services.core.environment-name";

		// Token: 0x04000002 RID: 2
		internal const string EnvironmentDefaultNameValue = "production";
	}
}
