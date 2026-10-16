using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET trace service. This class cannot be inherited.</summary>
	// Token: 0x020005DD RID: 1501
	public sealed class TraceSection : ConfigurationSection
	{
		// Token: 0x06004110 RID: 16656 RVA: 0x000A8A24 File Offset: 0x000A6C24
		static TraceSection()
		{
			TraceSection.properties.Add(TraceSection.enabledProp);
			TraceSection.properties.Add(TraceSection.localOnlyProp);
			TraceSection.properties.Add(TraceSection.mostRecentProp);
			TraceSection.properties.Add(TraceSection.pageOutputProp);
			TraceSection.properties.Add(TraceSection.requestLimitProp);
			TraceSection.properties.Add(TraceSection.traceModeProp);
			TraceSection.properties.Add(TraceSection.writeToDiagnosticsTraceProp);
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace service is enabled.</summary>
		/// <returns>true if trace is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x170014C6 RID: 5318
		// (get) Token: 0x06004111 RID: 16657 RVA: 0x000A8BA4 File Offset: 0x000A6DA4
		// (set) Token: 0x06004112 RID: 16658 RVA: 0x000A8BB6 File Offset: 0x000A6DB6
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[TraceSection.enabledProp];
			}
			set
			{
				base[TraceSection.enabledProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server.</summary>
		/// <returns>true if the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server; otherwise, false. The default is true.</returns>
		// Token: 0x170014C7 RID: 5319
		// (get) Token: 0x06004113 RID: 16659 RVA: 0x000A8BC9 File Offset: 0x000A6DC9
		// (set) Token: 0x06004114 RID: 16660 RVA: 0x000A8BDB File Offset: 0x000A6DDB
		[ConfigurationProperty("localOnly", DefaultValue = "True")]
		public bool LocalOnly
		{
			get
			{
				return (bool)base[TraceSection.localOnlyProp];
			}
			set
			{
				base[TraceSection.localOnlyProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the most recent requests are always stored on the server.</summary>
		/// <returns>true if the most recent requests are always stored in the trace log; otherwise, false. The default is false.</returns>
		// Token: 0x170014C8 RID: 5320
		// (get) Token: 0x06004115 RID: 16661 RVA: 0x000A8BEE File Offset: 0x000A6DEE
		// (set) Token: 0x06004116 RID: 16662 RVA: 0x000A8C00 File Offset: 0x000A6E00
		[ConfigurationProperty("mostRecent", DefaultValue = "False")]
		public bool MostRecent
		{
			get
			{
				return (bool)base[TraceSection.mostRecentProp];
			}
			set
			{
				base[TraceSection.mostRecentProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the ASP.NET trace information is appended to the output of each page.</summary>
		/// <returns>true if the trace information is appended to each page; otherwise, false. The default is false.</returns>
		// Token: 0x170014C9 RID: 5321
		// (get) Token: 0x06004117 RID: 16663 RVA: 0x000A8C13 File Offset: 0x000A6E13
		// (set) Token: 0x06004118 RID: 16664 RVA: 0x000A8C25 File Offset: 0x000A6E25
		[ConfigurationProperty("pageOutput", DefaultValue = "False")]
		public bool PageOutput
		{
			get
			{
				return (bool)base[TraceSection.pageOutputProp];
			}
			set
			{
				base[TraceSection.pageOutputProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating the maximum number of requests to the application for which ASP.NET stores trace information. </summary>
		/// <returns>The maximum number of requests to store on the server. The default is 10.</returns>
		// Token: 0x170014CA RID: 5322
		// (get) Token: 0x06004119 RID: 16665 RVA: 0x000A8C38 File Offset: 0x000A6E38
		// (set) Token: 0x0600411A RID: 16666 RVA: 0x000A8C4A File Offset: 0x000A6E4A
		[ConfigurationProperty("requestLimit", DefaultValue = "10")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int RequestLimit
		{
			get
			{
				return (int)base[TraceSection.requestLimitProp];
			}
			set
			{
				base[TraceSection.requestLimitProp] = value;
			}
		}

		/// <summary>Gets or sets the order in which ASP.NET trace information is displayed.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.TraceDisplayMode" /> values, indicating the order in which trace information is displayed.</returns>
		// Token: 0x170014CB RID: 5323
		// (get) Token: 0x0600411B RID: 16667 RVA: 0x000A8C5D File Offset: 0x000A6E5D
		// (set) Token: 0x0600411C RID: 16668 RVA: 0x000A8C6F File Offset: 0x000A6E6F
		[ConfigurationProperty("traceMode", DefaultValue = "SortByTime")]
		public TraceDisplayMode TraceMode
		{
			get
			{
				return (TraceDisplayMode)base[TraceSection.traceModeProp];
			}
			set
			{
				base[TraceSection.traceModeProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the messages emitted through the page trace are forwarded to an instance of the <see cref="T:System.Diagnostics.Trace" /> class.</summary>
		/// <returns>true if the trace messages are sent to the <see cref="T:System.Diagnostics.Trace" /> class; otherwise, false. The default is false.</returns>
		// Token: 0x170014CC RID: 5324
		// (get) Token: 0x0600411D RID: 16669 RVA: 0x000A8C82 File Offset: 0x000A6E82
		// (set) Token: 0x0600411E RID: 16670 RVA: 0x000A8C94 File Offset: 0x000A6E94
		[ConfigurationProperty("writeToDiagnosticsTrace", DefaultValue = "False")]
		public bool WriteToDiagnosticsTrace
		{
			get
			{
				return (bool)base[TraceSection.writeToDiagnosticsTraceProp];
			}
			set
			{
				base[TraceSection.writeToDiagnosticsTraceProp] = value;
			}
		}

		// Token: 0x170014CD RID: 5325
		// (get) Token: 0x0600411F RID: 16671 RVA: 0x000A8CA7 File Offset: 0x000A6EA7
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TraceSection.properties;
			}
		}

		// Token: 0x04002321 RID: 8993
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04002322 RID: 8994
		private static ConfigurationProperty localOnlyProp = new ConfigurationProperty("localOnly", typeof(bool), true);

		// Token: 0x04002323 RID: 8995
		private static ConfigurationProperty mostRecentProp = new ConfigurationProperty("mostRecent", typeof(bool), false);

		// Token: 0x04002324 RID: 8996
		private static ConfigurationProperty pageOutputProp = new ConfigurationProperty("pageOutput", typeof(bool), false);

		// Token: 0x04002325 RID: 8997
		private static ConfigurationProperty requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), 10, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002326 RID: 8998
		private static ConfigurationProperty traceModeProp = new ConfigurationProperty("traceMode", typeof(TraceDisplayMode), TraceDisplayMode.SortByTime, new GenericEnumConverter(typeof(TraceDisplayMode)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002327 RID: 8999
		private static ConfigurationProperty writeToDiagnosticsTraceProp = new ConfigurationProperty("writeToDiagnosticsTrace", typeof(bool), false);

		// Token: 0x04002328 RID: 9000
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
