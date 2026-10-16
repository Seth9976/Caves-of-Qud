using System;

namespace System.Configuration
{
	/// <summary>Specifies that an application settings property has a common value for all users of an application. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200019B RID: 411
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class ApplicationScopedSettingAttribute : SettingAttribute
	{
	}
}
