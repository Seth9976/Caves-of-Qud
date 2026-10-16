using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000214 RID: 532
	internal class AssertSection : ConfigurationElement
	{
		// Token: 0x06000F4A RID: 3914 RVA: 0x00044A24 File Offset: 0x00042C24
		static AssertSection()
		{
			AssertSection._properties.Add(AssertSection._propAssertUIEnabled);
			AssertSection._properties.Add(AssertSection._propLogFile);
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00044A98 File Offset: 0x00042C98
		[ConfigurationProperty("assertuienabled", DefaultValue = true)]
		public bool AssertUIEnabled
		{
			get
			{
				return (bool)base[AssertSection._propAssertUIEnabled];
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x00044AAA File Offset: 0x00042CAA
		[ConfigurationProperty("logfilename", DefaultValue = "")]
		public string LogFileName
		{
			get
			{
				return (string)base[AssertSection._propLogFile];
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00044ABC File Offset: 0x00042CBC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AssertSection._properties;
			}
		}

		// Token: 0x040009AE RID: 2478
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040009AF RID: 2479
		private static readonly ConfigurationProperty _propAssertUIEnabled = new ConfigurationProperty("assertuienabled", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x040009B0 RID: 2480
		private static readonly ConfigurationProperty _propLogFile = new ConfigurationProperty("logfilename", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
