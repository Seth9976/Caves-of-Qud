using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the sessionPageState section. This class cannot be inherited. </summary>
	// Token: 0x020005D2 RID: 1490
	public sealed class SessionPageStateSection : ConfigurationSection
	{
		// Token: 0x06004055 RID: 16469 RVA: 0x000A7618 File Offset: 0x000A5818
		static SessionPageStateSection()
		{
			SessionPageStateSection.properties.Add(SessionPageStateSection.historySizeProp);
		}

		/// <summary>Gets or sets the size of the page history.</summary>
		/// <returns>The size of the page history.</returns>
		// Token: 0x17001467 RID: 5223
		// (get) Token: 0x06004056 RID: 16470 RVA: 0x000A7679 File Offset: 0x000A5879
		// (set) Token: 0x06004057 RID: 16471 RVA: 0x000A768B File Offset: 0x000A588B
		[IntegerValidator(MinValue = 1, MaxValue = 2147483647)]
		[ConfigurationProperty("historySize", DefaultValue = "9")]
		public int HistorySize
		{
			get
			{
				return (int)base[SessionPageStateSection.historySizeProp];
			}
			set
			{
				base[SessionPageStateSection.historySizeProp] = value;
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06004058 RID: 16472 RVA: 0x000A769E File Offset: 0x000A589E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return SessionPageStateSection.properties;
			}
		}

		// Token: 0x040022EF RID: 8943
		private static ConfigurationProperty historySizeProp = new ConfigurationProperty("historySize", typeof(int), 9, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);

		// Token: 0x040022F0 RID: 8944
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		/// <summary>Defines the size of the page history.</summary>
		// Token: 0x040022F1 RID: 8945
		public const int DefaultHistorySize = 9;
	}
}
