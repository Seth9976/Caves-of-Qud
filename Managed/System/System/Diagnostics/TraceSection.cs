using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x02000237 RID: 567
	internal class TraceSection : ConfigurationElement
	{
		// Token: 0x060010E5 RID: 4325 RVA: 0x000495C0 File Offset: 0x000477C0
		static TraceSection()
		{
			TraceSection._properties.Add(TraceSection._propListeners);
			TraceSection._properties.Add(TraceSection._propAutoFlush);
			TraceSection._properties.Add(TraceSection._propIndentSize);
			TraceSection._properties.Add(TraceSection._propUseGlobalLock);
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x060010E6 RID: 4326 RVA: 0x00049692 File Offset: 0x00047892
		[ConfigurationProperty("autoflush", DefaultValue = false)]
		public bool AutoFlush
		{
			get
			{
				return (bool)base[TraceSection._propAutoFlush];
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000496A4 File Offset: 0x000478A4
		[ConfigurationProperty("indentsize", DefaultValue = 4)]
		public int IndentSize
		{
			get
			{
				return (int)base[TraceSection._propIndentSize];
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x000496B6 File Offset: 0x000478B6
		[ConfigurationProperty("listeners")]
		public ListenerElementsCollection Listeners
		{
			get
			{
				return (ListenerElementsCollection)base[TraceSection._propListeners];
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x000496C8 File Offset: 0x000478C8
		[ConfigurationProperty("useGlobalLock", DefaultValue = true)]
		public bool UseGlobalLock
		{
			get
			{
				return (bool)base[TraceSection._propUseGlobalLock];
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060010EA RID: 4330 RVA: 0x000496DA File Offset: 0x000478DA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection._properties;
			}
		}

		// Token: 0x04000A26 RID: 2598
		private static readonly ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04000A27 RID: 2599
		private static readonly ConfigurationProperty _propListeners = new ConfigurationProperty("listeners", typeof(ListenerElementsCollection), new ListenerElementsCollection(), ConfigurationPropertyOptions.None);

		// Token: 0x04000A28 RID: 2600
		private static readonly ConfigurationProperty _propAutoFlush = new ConfigurationProperty("autoflush", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04000A29 RID: 2601
		private static readonly ConfigurationProperty _propIndentSize = new ConfigurationProperty("indentsize", typeof(int), 4, ConfigurationPropertyOptions.None);

		// Token: 0x04000A2A RID: 2602
		private static readonly ConfigurationProperty _propUseGlobalLock = new ConfigurationProperty("useGlobalLock", typeof(bool), true, ConfigurationPropertyOptions.None);
	}
}
