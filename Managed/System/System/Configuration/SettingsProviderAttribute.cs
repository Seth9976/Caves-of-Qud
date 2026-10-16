using System;

namespace System.Configuration
{
	/// <summary>Specifies the settings provider used to provide storage for the current application settings class or property. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001D7 RID: 471
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SettingsProviderAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class.</summary>
		/// <param name="providerTypeName">A <see cref="T:System.String" /> containing the name of the settings provider.</param>
		// Token: 0x06000C54 RID: 3156 RVA: 0x000326A3 File Offset: 0x000308A3
		public SettingsProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this.providerTypeName = providerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SettingsProviderAttribute" /> class. </summary>
		/// <param name="providerType">A <see cref="T:System.Type" /> containing the settings provider type.</param>
		// Token: 0x06000C55 RID: 3157 RVA: 0x000326C0 File Offset: 0x000308C0
		public SettingsProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this.providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Gets the type name of the settings provider.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the settings provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000326E8 File Offset: 0x000308E8
		public string ProviderTypeName
		{
			get
			{
				return this.providerTypeName;
			}
		}

		// Token: 0x040007D4 RID: 2004
		private string providerTypeName;
	}
}
