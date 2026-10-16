using System;
using System.ComponentModel;
using System.Configuration;
using System.Web.UI;
using System.Xml;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the pages section of the configuration file. This class cannot be inherited.</summary>
	// Token: 0x020005BE RID: 1470
	public sealed class PagesSection : ConfigurationSection
	{
		// Token: 0x06003F0B RID: 16139 RVA: 0x000A4B54 File Offset: 0x000A2D54
		static PagesSection()
		{
			PagesSection.properties.Add(PagesSection.asyncTimeoutProp);
			PagesSection.properties.Add(PagesSection.autoEventWireupProp);
			PagesSection.properties.Add(PagesSection.bufferProp);
			PagesSection.properties.Add(PagesSection.controlsProp);
			PagesSection.properties.Add(PagesSection.enableEventValidationProp);
			PagesSection.properties.Add(PagesSection.enableSessionStateProp);
			PagesSection.properties.Add(PagesSection.enableViewStateProp);
			PagesSection.properties.Add(PagesSection.enableViewStateMacProp);
			PagesSection.properties.Add(PagesSection.maintainScrollPositionOnPostBackProp);
			PagesSection.properties.Add(PagesSection.masterPageFileProp);
			PagesSection.properties.Add(PagesSection.maxPageStateFieldLengthProp);
			PagesSection.properties.Add(PagesSection.modeProp);
			PagesSection.properties.Add(PagesSection.namespacesProp);
			PagesSection.properties.Add(PagesSection.pageBaseTypeProp);
			PagesSection.properties.Add(PagesSection.pageParserFilterTypeProp);
			PagesSection.properties.Add(PagesSection.smartNavigationProp);
			PagesSection.properties.Add(PagesSection.styleSheetThemeProp);
			PagesSection.properties.Add(PagesSection.tagMappingProp);
			PagesSection.properties.Add(PagesSection.themeProp);
			PagesSection.properties.Add(PagesSection.userControlBaseTypeProp);
			PagesSection.properties.Add(PagesSection.validateRequestProp);
			PagesSection.properties.Add(PagesSection.viewStateEncryptionModeProp);
			PagesSection.properties.Add(PagesSection.clientIDModeProp);
			PagesSection.properties.Add(PagesSection.controlRenderingCompatibilityVersionProp);
		}

		/// <summary>Gets or sets a value indicating the number of seconds to wait for an asynchronous handler to complete during asynchronous page processing.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the amount of time in seconds to wait for an asynchronous handler to complete during asynchronous page processing.</returns>
		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x06003F0D RID: 16141 RVA: 0x000A5018 File Offset: 0x000A3218
		// (set) Token: 0x06003F0E RID: 16142 RVA: 0x000A502A File Offset: 0x000A322A
		[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
		[TypeConverter(typeof(TimeSpanSecondsConverter))]
		[ConfigurationProperty("asyncTimeout", DefaultValue = "00:00:45")]
		public TimeSpan AsyncTimeout
		{
			get
			{
				return (TimeSpan)base[PagesSection.asyncTimeoutProp];
			}
			set
			{
				base[PagesSection.asyncTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether events for ASP.NET pages are automatically connected to event-handling functions.</summary>
		/// <returns>true if events for ASP.NET pages are automatically connected to event-handling functions; otherwise, false. The default is true.</returns>
		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000A503D File Offset: 0x000A323D
		// (set) Token: 0x06003F10 RID: 16144 RVA: 0x000A504F File Offset: 0x000A324F
		[ConfigurationProperty("autoEventWireup", DefaultValue = true)]
		public bool AutoEventWireup
		{
			get
			{
				return (bool)base[PagesSection.autoEventWireupProp];
			}
			set
			{
				base[PagesSection.autoEventWireupProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether .aspx pages and .ascx controls use response buffering.</summary>
		/// <returns>true if .aspx pages and .ascx controls use response buffering; otherwise, false. The default is true.</returns>
		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x000A5062 File Offset: 0x000A3262
		// (set) Token: 0x06003F12 RID: 16146 RVA: 0x000A5074 File Offset: 0x000A3274
		[ConfigurationProperty("buffer", DefaultValue = true)]
		public bool Buffer
		{
			get
			{
				return (bool)base[PagesSection.bufferProp];
			}
			set
			{
				base[PagesSection.bufferProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines how .aspx pages and .ascx controls are compiled.</summary>
		/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.CompilationMode" /> property, which specifies how .aspx pages and .ascx controls are compiled.</returns>
		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x000A5087 File Offset: 0x000A3287
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x000A5099 File Offset: 0x000A3299
		[ConfigurationProperty("compilationMode", DefaultValue = CompilationMode.Always)]
		public CompilationMode CompilationMode
		{
			get
			{
				return (CompilationMode)base[PagesSection.modeProp];
			}
			set
			{
				base[PagesSection.modeProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TagPrefixCollection" /> of <see cref="T:System.Web.Configuration.TagPrefixInfo" /> objects.</returns>
		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000A50AC File Offset: 0x000A32AC
		[ConfigurationProperty("controls")]
		public TagPrefixCollection Controls
		{
			get
			{
				return (TagPrefixCollection)base[PagesSection.controlsProp];
			}
		}

		/// <summary>Gets or sets a value that specifies whether event validation is enabled.</summary>
		/// <returns>true if event validation is enabled; otherwise, false.</returns>
		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000A50BE File Offset: 0x000A32BE
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x000A50D0 File Offset: 0x000A32D0
		[ConfigurationProperty("enableEventValidation", DefaultValue = true)]
		public bool EnableEventValidation
		{
			get
			{
				return (bool)base[PagesSection.enableEventValidationProp];
			}
			set
			{
				base[PagesSection.enableEventValidationProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the session state is enabled, disabled, or read-only.</summary>
		/// <returns>One of the values for the <see cref="P:System.Web.Configuration.PagesSection.EnableSessionState" /> property, which specifies whether the session state is enabled, disabled, or read-only. The default is <see cref="F:System.Web.Configuration.PagesEnableSessionState.True" />, which indicates that session state is enabled.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is not a valid <see cref="T:System.Web.Configuration.PagesEnableSessionState" /> enumeration value.</exception>
		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000A50E4 File Offset: 0x000A32E4
		// (set) Token: 0x06003F19 RID: 16153 RVA: 0x000A513B File Offset: 0x000A333B
		[ConfigurationProperty("enableSessionState", DefaultValue = "true")]
		public PagesEnableSessionState EnableSessionState
		{
			get
			{
				string text = (string)base[PagesSection.enableSessionStateProp];
				if (text == "true")
				{
					return PagesEnableSessionState.True;
				}
				if (text == "false")
				{
					return PagesEnableSessionState.False;
				}
				if (!(text == "ReadOnly"))
				{
					throw new ConfigurationErrorsException("The 'enableSessionState' attribute must be one of the following values: true,false, ReadOnly.");
				}
				return PagesEnableSessionState.ReadOnly;
			}
			set
			{
				if (value == PagesEnableSessionState.False)
				{
					base[PagesSection.enableSessionStateProp] = "false";
					return;
				}
				if (value != PagesEnableSessionState.ReadOnly)
				{
					base[PagesSection.enableSessionStateProp] = "true";
					return;
				}
				base[PagesSection.enableSessionStateProp] = "ReadOnly";
			}
		}

		/// <summary>Gets or sets a value indicating whether view state is enabled or disabled.</summary>
		/// <returns>true if view state is enabled; false if view state is disabled. The default is true.</returns>
		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000A5178 File Offset: 0x000A3378
		// (set) Token: 0x06003F1B RID: 16155 RVA: 0x000A518A File Offset: 0x000A338A
		[ConfigurationProperty("enableViewState", DefaultValue = true)]
		public bool EnableViewState
		{
			get
			{
				return (bool)base[PagesSection.enableViewStateProp];
			}
			set
			{
				base[PagesSection.enableViewStateProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client.</summary>
		/// <returns>true if ASP.NET should run a message authentication code (MAC) on the page's view state when the page is posted back from the client; otherwise, false. The default is false.</returns>
		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x000A519D File Offset: 0x000A339D
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x000A51AF File Offset: 0x000A33AF
		[ConfigurationProperty("enableViewStateMac", DefaultValue = true)]
		public bool EnableViewStateMac
		{
			get
			{
				return (bool)base[PagesSection.enableViewStateMacProp];
			}
			set
			{
				base[PagesSection.enableViewStateMacProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the page scroll position should be maintained upon returning from a postback from the server.</summary>
		/// <returns>true if the page-scroll position should be maintained after postback; otherwise, false. The default value is false.</returns>
		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x000A51C2 File Offset: 0x000A33C2
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x000A51D4 File Offset: 0x000A33D4
		[ConfigurationProperty("maintainScrollPositionOnPostBack", DefaultValue = false)]
		public bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return (bool)base[PagesSection.maintainScrollPositionOnPostBackProp];
			}
			set
			{
				base[PagesSection.maintainScrollPositionOnPostBackProp] = value;
			}
		}

		/// <summary>Gets or sets a reference to the master page for the application. </summary>
		/// <returns>A reference to the master page for the application.</returns>
		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x000A51E7 File Offset: 0x000A33E7
		// (set) Token: 0x06003F21 RID: 16161 RVA: 0x000A51F9 File Offset: 0x000A33F9
		[ConfigurationProperty("masterPageFile", DefaultValue = "")]
		public string MasterPageFile
		{
			get
			{
				return (string)base[PagesSection.masterPageFileProp];
			}
			set
			{
				base[PagesSection.masterPageFileProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum number of characters that a single view-state field can contain.</summary>
		/// <returns>The maximum number of characters that a single view-state field can contain.</returns>
		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x06003F22 RID: 16162 RVA: 0x000A5207 File Offset: 0x000A3407
		// (set) Token: 0x06003F23 RID: 16163 RVA: 0x000A5219 File Offset: 0x000A3419
		[ConfigurationProperty("maxPageStateFieldLength", DefaultValue = -1)]
		public int MaxPageStateFieldLength
		{
			get
			{
				return (int)base[PagesSection.maxPageStateFieldLengthProp];
			}
			set
			{
				base[PagesSection.maxPageStateFieldLengthProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.NamespaceCollection" /> of <see cref="T:System.Web.Configuration.NamespaceInfo" /> objects.</returns>
		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x000A522C File Offset: 0x000A342C
		[ConfigurationProperty("namespaces")]
		public NamespaceCollection Namespaces
		{
			get
			{
				return (NamespaceCollection)base[PagesSection.namespacesProp];
			}
		}

		/// <summary>Gets or sets a value that specifies a code-behind class that .aspx pages inherit by default.</summary>
		/// <returns>A string that specifies a code-behind class that .aspx pages inherit by default.</returns>
		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000A523E File Offset: 0x000A343E
		// (set) Token: 0x06003F26 RID: 16166 RVA: 0x000A5250 File Offset: 0x000A3450
		[ConfigurationProperty("pageBaseType", DefaultValue = "System.Web.UI.Page")]
		public string PageBaseType
		{
			get
			{
				return (string)base[PagesSection.pageBaseTypeProp];
			}
			set
			{
				base[PagesSection.pageBaseTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the parser filter type.</summary>
		/// <returns>A string that specifies the parser filter type.</returns>
		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000A525E File Offset: 0x000A345E
		// (set) Token: 0x06003F28 RID: 16168 RVA: 0x000A5270 File Offset: 0x000A3470
		[ConfigurationProperty("pageParserFilterType", DefaultValue = "")]
		public string PageParserFilterType
		{
			get
			{
				return (string)base[PagesSection.pageParserFilterTypeProp];
			}
			set
			{
				base[PagesSection.pageParserFilterTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether smart navigation is enabled.</summary>
		/// <returns>true if smart navigation is enabled; otherwise, false. The default value is false.</returns>
		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06003F29 RID: 16169 RVA: 0x000A527E File Offset: 0x000A347E
		// (set) Token: 0x06003F2A RID: 16170 RVA: 0x000A5290 File Offset: 0x000A3490
		[ConfigurationProperty("smartNavigation", DefaultValue = false)]
		public bool SmartNavigation
		{
			get
			{
				return (bool)base[PagesSection.smartNavigationProp];
			}
			set
			{
				base[PagesSection.smartNavigationProp] = value;
			}
		}

		/// <summary>Gets or sets the name of an ASP.NET style sheet theme.</summary>
		/// <returns>The name of an ASP.NET style sheet theme.</returns>
		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06003F2B RID: 16171 RVA: 0x000A52A3 File Offset: 0x000A34A3
		// (set) Token: 0x06003F2C RID: 16172 RVA: 0x000A52B5 File Offset: 0x000A34B5
		[ConfigurationProperty("styleSheetTheme", DefaultValue = "")]
		public string StyleSheetTheme
		{
			get
			{
				return (string)base[PagesSection.styleSheetThemeProp];
			}
			set
			{
				base[PagesSection.styleSheetThemeProp] = value;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TagMapCollection" /> of <see cref="T:System.Web.Configuration.TagMapInfo" /> objects.</returns>
		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000A52C3 File Offset: 0x000A34C3
		[ConfigurationProperty("tagMapping")]
		public TagMapCollection TagMapping
		{
			get
			{
				return (TagMapCollection)base[PagesSection.tagMappingProp];
			}
		}

		/// <summary>Gets or sets the name of an ASP.NET page theme.</summary>
		/// <returns>The name of an ASP.NET page theme.</returns>
		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x000A52D5 File Offset: 0x000A34D5
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x000A52E7 File Offset: 0x000A34E7
		[ConfigurationProperty("theme", DefaultValue = "")]
		public string Theme
		{
			get
			{
				return (string)base[PagesSection.themeProp];
			}
			set
			{
				base[PagesSection.themeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies a code-behind class that user controls inherit by default.</summary>
		/// <returns>A string that specifies a code-behind file that user controls inherit by default.</returns>
		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000A52F5 File Offset: 0x000A34F5
		// (set) Token: 0x06003F31 RID: 16177 RVA: 0x000A5307 File Offset: 0x000A3507
		[ConfigurationProperty("userControlBaseType", DefaultValue = "System.Web.UI.UserControl")]
		public string UserControlBaseType
		{
			get
			{
				return (string)base[PagesSection.userControlBaseTypeProp];
			}
			set
			{
				base[PagesSection.userControlBaseTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that determines whether ASP.NET examines input from the browser for dangerous values. For more information, see Script Exploits Overview.</summary>
		/// <returns>true if ASP.NET examines input from the browser for dangerous values; otherwise, false. The default value is true.</returns>
		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x000A5315 File Offset: 0x000A3515
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x000A5327 File Offset: 0x000A3527
		[ConfigurationProperty("validateRequest", DefaultValue = true)]
		public bool ValidateRequest
		{
			get
			{
				return (bool)base[PagesSection.validateRequestProp];
			}
			set
			{
				base[PagesSection.validateRequestProp] = value;
			}
		}

		/// <summary>Gets or sets the encryption mode that ASP.NET uses when maintaining ViewState values.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ViewStateEncryptionMode" /> enumeration value indicating when the ViewState values are encrypted.</returns>
		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x06003F34 RID: 16180 RVA: 0x000A533A File Offset: 0x000A353A
		// (set) Token: 0x06003F35 RID: 16181 RVA: 0x000A534C File Offset: 0x000A354C
		[ConfigurationProperty("viewStateEncryptionMode", DefaultValue = ViewStateEncryptionMode.Auto)]
		public ViewStateEncryptionMode ViewStateEncryptionMode
		{
			get
			{
				return (ViewStateEncryptionMode)base[PagesSection.viewStateEncryptionModeProp];
			}
			set
			{
				base[PagesSection.viewStateEncryptionModeProp] = value;
			}
		}

		/// <summary>Gets or sets the default algorithm that is used to generate a control's identifier.</summary>
		/// <returns>A value that indicates how the value in the <see cref="P:System.Web.UI.Control.ClientID" /> property is generated. The default value is <see cref="F:System.Web.UI.ClientIDMode.Predictable" />.</returns>
		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000A535F File Offset: 0x000A355F
		// (set) Token: 0x06003F37 RID: 16183 RVA: 0x000A5371 File Offset: 0x000A3571
		[ConfigurationProperty("clientIDMode", DefaultValue = ClientIDMode.Predictable)]
		public ClientIDMode ClientIDMode
		{
			get
			{
				return (ClientIDMode)base[PagesSection.clientIDModeProp];
			}
			set
			{
				base[PagesSection.clientIDModeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies the ASP.NET version that any rendered HTML will be compatible with.</summary>
		/// <returns>The ASP.NET version that any rendered HTML will be compatible with.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set this property to null.</exception>
		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003F38 RID: 16184 RVA: 0x000A5384 File Offset: 0x000A3584
		// (set) Token: 0x06003F39 RID: 16185 RVA: 0x000A5396 File Offset: 0x000A3596
		[ConfigurationProperty("controlRenderingCompatibilityVersion", DefaultValue = "4.0")]
		public Version ControlRenderingCompatibilityVersion
		{
			get
			{
				return (Version)base[PagesSection.controlRenderingCompatibilityVersionProp];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base[PagesSection.controlRenderingCompatibilityVersionProp] = value;
			}
		}

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003F3A RID: 16186 RVA: 0x000A53B8 File Offset: 0x000A35B8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PagesSection.properties;
			}
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x0009EE32 File Offset: 0x0009D032
		protected internal override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
		}

		/// <summary>Gets the collection of device tags that ASP.NET should ignore when it renders a page.</summary>
		/// <returns>The collection of device tags that ASP.NET should ignore.</returns>
		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06003F3C RID: 16188 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IgnoreDeviceFilterElementCollection IgnoreDeviceFilters
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets a value that indicates whether all system-generated hidden fields are rendered at the top of the form.</summary>
		/// <returns>true if system-generated hidden fields are rendered at the top of the form; otherwise, false. The default is true.</returns>
		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x000A53C0 File Offset: 0x000A35C0
		// (set) Token: 0x06003F3E RID: 16190 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool RenderAllHiddenFieldsAtTopOfForm
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002270 RID: 8816
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002271 RID: 8817
		private static ConfigurationProperty asyncTimeoutProp = new ConfigurationProperty("asyncTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(45.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002272 RID: 8818
		private static ConfigurationProperty autoEventWireupProp = new ConfigurationProperty("autoEventWireup", typeof(bool), true);

		// Token: 0x04002273 RID: 8819
		private static ConfigurationProperty bufferProp = new ConfigurationProperty("buffer", typeof(bool), true);

		// Token: 0x04002274 RID: 8820
		private static ConfigurationProperty controlsProp = new ConfigurationProperty("controls", typeof(TagPrefixCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002275 RID: 8821
		private static ConfigurationProperty enableEventValidationProp = new ConfigurationProperty("enableEventValidation", typeof(bool), true);

		// Token: 0x04002276 RID: 8822
		private static ConfigurationProperty enableSessionStateProp = new ConfigurationProperty("enableSessionState", typeof(string), "true");

		// Token: 0x04002277 RID: 8823
		private static ConfigurationProperty enableViewStateProp = new ConfigurationProperty("enableViewState", typeof(bool), true);

		// Token: 0x04002278 RID: 8824
		private static ConfigurationProperty enableViewStateMacProp = new ConfigurationProperty("enableViewStateMac", typeof(bool), true);

		// Token: 0x04002279 RID: 8825
		private static ConfigurationProperty maintainScrollPositionOnPostBackProp = new ConfigurationProperty("maintainScrollPositionOnPostBack", typeof(bool), false);

		// Token: 0x0400227A RID: 8826
		private static ConfigurationProperty masterPageFileProp = new ConfigurationProperty("masterPageFile", typeof(string), "");

		// Token: 0x0400227B RID: 8827
		private static ConfigurationProperty maxPageStateFieldLengthProp = new ConfigurationProperty("maxPageStateFieldLength", typeof(int), -1);

		// Token: 0x0400227C RID: 8828
		private static ConfigurationProperty modeProp = new ConfigurationProperty("compilationMode", typeof(CompilationMode), CompilationMode.Always, new GenericEnumConverter(typeof(CompilationMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400227D RID: 8829
		private static ConfigurationProperty namespacesProp = new ConfigurationProperty("namespaces", typeof(NamespaceCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x0400227E RID: 8830
		private static ConfigurationProperty pageBaseTypeProp = new ConfigurationProperty("pageBaseType", typeof(string), "System.Web.UI.Page");

		// Token: 0x0400227F RID: 8831
		private static ConfigurationProperty pageParserFilterTypeProp = new ConfigurationProperty("pageParserFilterType", typeof(string), "");

		// Token: 0x04002280 RID: 8832
		private static ConfigurationProperty smartNavigationProp = new ConfigurationProperty("smartNavigation", typeof(bool), false);

		// Token: 0x04002281 RID: 8833
		private static ConfigurationProperty styleSheetThemeProp = new ConfigurationProperty("styleSheetTheme", typeof(string), "");

		// Token: 0x04002282 RID: 8834
		private static ConfigurationProperty tagMappingProp = new ConfigurationProperty("tagMapping", typeof(TagMapCollection), null, null, null, ConfigurationPropertyOptions.None);

		// Token: 0x04002283 RID: 8835
		private static ConfigurationProperty themeProp = new ConfigurationProperty("theme", typeof(string), "");

		// Token: 0x04002284 RID: 8836
		private static ConfigurationProperty userControlBaseTypeProp = new ConfigurationProperty("userControlBaseType", typeof(string), "System.Web.UI.UserControl");

		// Token: 0x04002285 RID: 8837
		private static ConfigurationProperty validateRequestProp = new ConfigurationProperty("validateRequest", typeof(bool), true);

		// Token: 0x04002286 RID: 8838
		private static ConfigurationProperty viewStateEncryptionModeProp = new ConfigurationProperty("viewStateEncryptionMode", typeof(ViewStateEncryptionMode), ViewStateEncryptionMode.Auto, new GenericEnumConverter(typeof(ViewStateEncryptionMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002287 RID: 8839
		private static ConfigurationProperty clientIDModeProp = new ConfigurationProperty("clientIDMode", typeof(ClientIDMode), ClientIDMode.Predictable, new GenericEnumConverter(typeof(ClientIDMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002288 RID: 8840
		private static ConfigurationProperty controlRenderingCompatibilityVersionProp = new ConfigurationProperty("controlRenderingCompatibilityVersion", typeof(Version), new Version(4, 0), new VersionConverter(3, 5, "The value for the property 'controlRenderingCompatibilityVersion' is not valid. The error is: The control rendering compatibility version must not be less than {1}."), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
	}
}
