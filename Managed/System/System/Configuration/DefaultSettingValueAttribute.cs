using System;

namespace System.Configuration
{
	/// <summary>Specifies the default value for an application settings property.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001B0 RID: 432
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		/// <summary>Initializes an instance of the <see cref="T:System.Configuration.DefaultSettingValueAttribute" /> class.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that represents the default value for the property. </param>
		// Token: 0x06000B7A RID: 2938 RVA: 0x000311A2 File Offset: 0x0002F3A2
		public DefaultSettingValueAttribute(string value)
		{
			this.value = value;
		}

		/// <summary>Gets the default value for the application settings property.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the default value for the property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000311B1 File Offset: 0x0002F3B1
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x040007A1 RID: 1953
		private string value;
	}
}
