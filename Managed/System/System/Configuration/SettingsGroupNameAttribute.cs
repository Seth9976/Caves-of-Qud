using System;

namespace System.Configuration
{
	/// <summary>Specifies a name for application settings property group. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CA RID: 458
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SettingsGroupNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsGroupNameAttribute" /> class.</summary>
		/// <param name="groupName">A <see cref="T:System.String" /> containing the name of the application settings property group.</param>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x00031E0E File Offset: 0x0003000E
		public SettingsGroupNameAttribute(string groupName)
		{
			this.group_name = groupName;
		}

		/// <summary>Gets the name of the application settings property group.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the application settings property group.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x00031E1D File Offset: 0x0003001D
		public string GroupName
		{
			get
			{
				return this.group_name;
			}
		}

		// Token: 0x040007BA RID: 1978
		private string group_name;
	}
}
