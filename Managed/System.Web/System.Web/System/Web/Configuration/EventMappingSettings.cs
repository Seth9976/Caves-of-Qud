using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the ASP.NET event mapping settings for event providers. This class cannot be inherited.</summary>
	// Token: 0x02000595 RID: 1429
	public sealed class EventMappingSettings : ConfigurationElement
	{
		// Token: 0x06003C83 RID: 15491 RVA: 0x0009EF44 File Offset: 0x0009D144
		static EventMappingSettings()
		{
			EventMappingSettings.properties.Add(EventMappingSettings.endEventCodeProp);
			EventMappingSettings.properties.Add(EventMappingSettings.nameProp);
			EventMappingSettings.properties.Add(EventMappingSettings.startEventCodeProp);
			EventMappingSettings.properties.Add(EventMappingSettings.typeProp);
		}

		// Token: 0x06003C84 RID: 15492 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal EventMappingSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> class using the specified name and type.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
		/// <param name="type">The fully qualified type of the event class to use.</param>
		// Token: 0x06003C85 RID: 15493 RVA: 0x0009F055 File Offset: 0x0009D255
		public EventMappingSettings(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.BufferModeSettings" /> class using the specified values.</summary>
		/// <param name="name">The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object being created.</param>
		/// <param name="type">The fully qualified type of the event class to use.</param>
		/// <param name="startEventCode">The starting event code range.</param>
		/// <param name="endEventCode">The ending event code range.</param>
		// Token: 0x06003C86 RID: 15494 RVA: 0x0009F06B File Offset: 0x0009D26B
		public EventMappingSettings(string name, string type, int startEventCode, int endEventCode)
		{
			this.Name = name;
			this.Type = type;
			this.StartEventCode = startEventCode;
			this.EndEventCode = endEventCode;
		}

		/// <summary>Gets or sets the ending event code of the range.</summary>
		/// <returns>The ending event code of the range. The default is <see cref="F:System.Int32.MaxValue" />.</returns>
		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x0009F090 File Offset: 0x0009D290
		// (set) Token: 0x06003C88 RID: 15496 RVA: 0x0009F0A2 File Offset: 0x0009D2A2
		[ConfigurationProperty("endEventCode", DefaultValue = "2147483647")]
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		public int EndEventCode
		{
			get
			{
				return (int)base[EventMappingSettings.endEventCodeProp];
			}
			set
			{
				base[EventMappingSettings.endEventCodeProp] = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Web.Configuration.EventMappingSettings" /> object. The default is an empty string ("").</returns>
		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003C89 RID: 15497 RVA: 0x0009F0B5 File Offset: 0x0009D2B5
		// (set) Token: 0x06003C8A RID: 15498 RVA: 0x0009F0C7 File Offset: 0x0009D2C7
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[EventMappingSettings.nameProp];
			}
			set
			{
				base[EventMappingSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the starting event code of the range.</summary>
		/// <returns>The starting event code of the range. The default is 0.</returns>
		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003C8B RID: 15499 RVA: 0x0009F0D5 File Offset: 0x0009D2D5
		// (set) Token: 0x06003C8C RID: 15500 RVA: 0x0009F0E7 File Offset: 0x0009D2E7
		[IntegerValidator(MinValue = 0, MaxValue = 2147483647)]
		[ConfigurationProperty("startEventCode", DefaultValue = "0")]
		public int StartEventCode
		{
			get
			{
				return (int)base[EventMappingSettings.startEventCodeProp];
			}
			set
			{
				base[EventMappingSettings.startEventCodeProp] = value;
			}
		}

		/// <summary>Gets or sets a custom event type.</summary>
		/// <returns>A valid type reference or an empty string (""). The default is an empty string.</returns>
		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x0009F0FA File Offset: 0x0009D2FA
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x0009F10C File Offset: 0x0009D30C
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[EventMappingSettings.typeProp];
			}
			set
			{
				base[EventMappingSettings.typeProp] = value;
			}
		}

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003C8F RID: 15503 RVA: 0x0009F11A File Offset: 0x0009D31A
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return EventMappingSettings.properties;
			}
		}

		// Token: 0x040020D2 RID: 8402
		private static ConfigurationProperty endEventCodeProp = new ConfigurationProperty("endEventCode", typeof(int), int.MaxValue, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020D3 RID: 8403
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), "", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020D4 RID: 8404
		private static ConfigurationProperty startEventCodeProp = new ConfigurationProperty("startEventCode", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020D5 RID: 8405
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), "", ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020D6 RID: 8406
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
