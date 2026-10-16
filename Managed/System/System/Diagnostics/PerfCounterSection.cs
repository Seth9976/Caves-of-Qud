using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000220 RID: 544
	internal class PerfCounterSection : ConfigurationElement
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x00046048 File Offset: 0x00044248
		static PerfCounterSection()
		{
			PerfCounterSection._properties.Add(PerfCounterSection._propFileMappingSize);
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00046087 File Offset: 0x00044287
		[ConfigurationProperty("filemappingsize", DefaultValue = 524288)]
		public int FileMappingSize
		{
			get
			{
				return (int)base[PerfCounterSection._propFileMappingSize];
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00046099 File Offset: 0x00044299
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerfCounterSection._properties;
			}
		}

		// Token: 0x040009C3 RID: 2499
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040009C4 RID: 2500
		private static readonly ConfigurationProperty _propFileMappingSize = new ConfigurationProperty("filemappingsize", typeof(int), 524288, ConfigurationPropertyOptions.None);
	}
}
