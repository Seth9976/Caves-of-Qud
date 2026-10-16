using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the xhtmlConformance section. This class cannot be inherited. </summary>
	// Token: 0x020005F3 RID: 1523
	public sealed class XhtmlConformanceSection : ConfigurationSection
	{
		// Token: 0x06004216 RID: 16918 RVA: 0x000AAC84 File Offset: 0x000A8E84
		static XhtmlConformanceSection()
		{
			XhtmlConformanceSection.properties.Add(XhtmlConformanceSection.modeProp);
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.XhtmlConformanceSection.Mode" /> property. </summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.XhtmlConformanceMode" /> values. The default is <see cref="F:System.Web.Configuration.XhtmlConformanceMode.Transitional" />.</returns>
		// Token: 0x1700150F RID: 5391
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x000AACDE File Offset: 0x000A8EDE
		// (set) Token: 0x06004218 RID: 16920 RVA: 0x000AACF0 File Offset: 0x000A8EF0
		[ConfigurationProperty("mode", DefaultValue = "Transitional")]
		public XhtmlConformanceMode Mode
		{
			get
			{
				return (XhtmlConformanceMode)base[XhtmlConformanceSection.modeProp];
			}
			set
			{
				base[XhtmlConformanceSection.modeProp] = value;
			}
		}

		// Token: 0x17001510 RID: 5392
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x000AAD03 File Offset: 0x000A8F03
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return XhtmlConformanceSection.properties;
			}
		}

		// Token: 0x04002370 RID: 9072
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(XhtmlConformanceMode), XhtmlConformanceMode.Transitional, new GenericEnumConverter(typeof(XhtmlConformanceMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002371 RID: 9073
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
