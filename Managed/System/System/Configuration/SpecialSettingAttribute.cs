using System;

namespace System.Configuration
{
	/// <summary>Indicates that an application settings property has a special significance. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DE RID: 478
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
	public sealed class SpecialSettingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SpecialSettingAttribute" /> class.</summary>
		/// <param name="specialSetting">A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</param>
		// Token: 0x06000C62 RID: 3170 RVA: 0x000327BD File Offset: 0x000309BD
		public SpecialSettingAttribute(SpecialSetting specialSetting)
		{
			this.setting = specialSetting;
		}

		/// <summary>Gets the value describing the special setting category of the application settings property.</summary>
		/// <returns>A <see cref="T:System.Configuration.SpecialSetting" /> enumeration value defining the category of the application settings property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x000327CC File Offset: 0x000309CC
		public SpecialSetting SpecialSetting
		{
			get
			{
				return this.setting;
			}
		}

		// Token: 0x040007DE RID: 2014
		private SpecialSetting setting;
	}
}
