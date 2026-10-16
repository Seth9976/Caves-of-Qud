using System;

namespace System.Configuration
{
	/// <summary>Specifies special services for application settings properties. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001CE RID: 462
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsManageabilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsManageabilityAttribute" /> class.</summary>
		/// <param name="manageability">A <see cref="T:System.Configuration.SettingsManageability" /> value that enumerates the services being requested. </param>
		// Token: 0x06000BFF RID: 3071 RVA: 0x00031E3C File Offset: 0x0003003C
		public SettingsManageabilityAttribute(SettingsManageability manageability)
		{
			this.manageability = manageability;
		}

		/// <summary>Gets the set of special services that have been requested.</summary>
		/// <returns>A value that results from using the logical OR operator to combine all the <see cref="T:System.Configuration.SettingsManageability" /> enumeration values corresponding to the requested services.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00031E4B File Offset: 0x0003004B
		public SettingsManageability Manageability
		{
			get
			{
				return this.manageability;
			}
		}

		// Token: 0x040007BE RID: 1982
		private SettingsManageability manageability;
	}
}
