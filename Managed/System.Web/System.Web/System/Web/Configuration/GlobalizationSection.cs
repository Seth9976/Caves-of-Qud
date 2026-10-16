using System;
using System.Collections;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Defines configuration settings that are used to support the globalization infrastructure of Web applications. This class cannot be inherited.</summary>
	// Token: 0x0200059D RID: 1437
	public sealed class GlobalizationSection : ConfigurationSection
	{
		// Token: 0x06003CF5 RID: 15605 RVA: 0x0009FA5C File Offset: 0x0009DC5C
		static GlobalizationSection()
		{
			GlobalizationSection.properties.Add(GlobalizationSection.cultureProp);
			GlobalizationSection.properties.Add(GlobalizationSection.enableBestFitResponseEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.enableClientBasedCultureProp);
			GlobalizationSection.properties.Add(GlobalizationSection.fileEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.requestEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.resourceProviderFactoryTypeProp);
			GlobalizationSection.properties.Add(GlobalizationSection.responseEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.responseHeaderEncodingProp);
			GlobalizationSection.properties.Add(GlobalizationSection.uiCultureProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.GlobalizationSection" /> class by using default settings.</summary>
		// Token: 0x06003CF6 RID: 15606 RVA: 0x0009FC05 File Offset: 0x0009DE05
		public GlobalizationSection()
		{
			this.encodingHash = new Hashtable();
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x0009FC18 File Offset: 0x0009DE18
		private void VerifyData()
		{
			bool flag = false;
			try
			{
				this.GetSanitizedCulture(this.Culture, ref flag);
			}
			catch
			{
				throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'culture' attribute");
			}
			try
			{
				this.GetSanitizedCulture(this.UICulture, ref flag);
			}
			catch
			{
				throw new ConfigurationErrorsException("the <globalization> tag contains an invalid value for the 'uiCulture' attribute");
			}
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x0009FC80 File Offset: 0x0009DE80
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
			this.VerifyData();
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x0009FC8E File Offset: 0x0009DE8E
		protected override void PreSerialize(XmlWriter writer)
		{
			base.PreSerialize(writer);
			this.VerifyData();
		}

		/// <summary>Gets or sets a value specifying the default culture for processing incoming Web requests.</summary>
		/// <returns>The default culture for processing incoming Web requests.</returns>
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003CFA RID: 15610 RVA: 0x0009FC9D File Offset: 0x0009DE9D
		// (set) Token: 0x06003CFB RID: 15611 RVA: 0x0009FCAF File Offset: 0x0009DEAF
		[ConfigurationProperty("culture", DefaultValue = "")]
		public string Culture
		{
			get
			{
				return (string)base[GlobalizationSection.cultureProp];
			}
			set
			{
				base[GlobalizationSection.cultureProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the best-fit character encoding for a response is enabled.</summary>
		/// <returns>true if the best-fit character encoding for a response is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003CFC RID: 15612 RVA: 0x0009FCBD File Offset: 0x0009DEBD
		// (set) Token: 0x06003CFD RID: 15613 RVA: 0x0009FCCF File Offset: 0x0009DECF
		[ConfigurationProperty("enableBestFitResponseEncoding", DefaultValue = "False")]
		public bool EnableBestFitResponseEncoding
		{
			get
			{
				return (bool)base[GlobalizationSection.enableBestFitResponseEncodingProp];
			}
			set
			{
				base[GlobalizationSection.enableBestFitResponseEncodingProp] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> properties should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value that is sent by the client browser.</summary>
		/// <returns>true if the <see cref="P:System.Web.Configuration.GlobalizationSection.Culture" /> and <see cref="P:System.Web.Configuration.GlobalizationSection.UICulture" /> should be based on the <see cref="F:System.Net.HttpRequestHeader.AcceptLanguage" /> header field value sent by the client browser; otherwise, false. The default is false.</returns>
		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06003CFE RID: 15614 RVA: 0x0009FCE2 File Offset: 0x0009DEE2
		// (set) Token: 0x06003CFF RID: 15615 RVA: 0x0009FCF4 File Offset: 0x0009DEF4
		[ConfigurationProperty("enableClientBasedCulture", DefaultValue = "False")]
		public bool EnableClientBasedCulture
		{
			get
			{
				return (bool)base[GlobalizationSection.enableClientBasedCultureProp];
			}
			set
			{
				base[GlobalizationSection.enableClientBasedCultureProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the default encoding for .aspx, .asmx, and .asax file parsing.</summary>
		/// <returns>The default encoding value.</returns>
		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06003D00 RID: 15616 RVA: 0x0009FD07 File Offset: 0x0009DF07
		// (set) Token: 0x06003D01 RID: 15617 RVA: 0x0009FD1A File Offset: 0x0009DF1A
		[ConfigurationProperty("fileEncoding")]
		public Encoding FileEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.fileEncodingProp, ref this.cached_fileencoding);
			}
			set
			{
				base[GlobalizationSection.fileEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the content encoding of HTTP requests.</summary>
		/// <returns>The content encoding of HTTP requests. The default is UTF-8.</returns>
		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06003D02 RID: 15618 RVA: 0x0009FD2D File Offset: 0x0009DF2D
		// (set) Token: 0x06003D03 RID: 15619 RVA: 0x0009FD40 File Offset: 0x0009DF40
		[ConfigurationProperty("requestEncoding", DefaultValue = "utf-8")]
		public Encoding RequestEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.requestEncodingProp, ref this.cached_requestencoding);
			}
			set
			{
				base[GlobalizationSection.requestEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets the factory type of the resource provider.</summary>
		/// <returns>The factory type of the resource provider.</returns>
		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003D04 RID: 15620 RVA: 0x0009FD53 File Offset: 0x0009DF53
		// (set) Token: 0x06003D05 RID: 15621 RVA: 0x0009FD65 File Offset: 0x0009DF65
		[ConfigurationProperty("resourceProviderFactoryType", DefaultValue = "")]
		public string ResourceProviderFactoryType
		{
			get
			{
				return (string)base[GlobalizationSection.resourceProviderFactoryTypeProp];
			}
			set
			{
				base[GlobalizationSection.resourceProviderFactoryTypeProp] = value;
			}
		}

		/// <summary>Gets or sets a value specifying the content encoding of HTTP responses.</summary>
		/// <returns>The content encoding of HTTP responses. The default is UTF-8.</returns>
		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003D06 RID: 15622 RVA: 0x0009FD73 File Offset: 0x0009DF73
		// (set) Token: 0x06003D07 RID: 15623 RVA: 0x0009FD86 File Offset: 0x0009DF86
		[ConfigurationProperty("responseEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.responseEncodingProp, ref this.cached_responseencoding);
			}
			set
			{
				base[GlobalizationSection.responseEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the header encoding of HTTP responses.</summary>
		/// <returns>The header encoding of HTTP responses. The default is UTF-8.</returns>
		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x0009FD99 File Offset: 0x0009DF99
		// (set) Token: 0x06003D09 RID: 15625 RVA: 0x0009FDAC File Offset: 0x0009DFAC
		[ConfigurationProperty("responseHeaderEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseHeaderEncoding
		{
			get
			{
				return this.GetEncoding(GlobalizationSection.responseHeaderEncodingProp, ref this.cached_responseheaderencoding);
			}
			set
			{
				base[GlobalizationSection.responseHeaderEncodingProp] = value.WebName;
			}
		}

		/// <summary>Gets or sets a value specifying the default culture for processing locale-dependent resource searches.</summary>
		/// <returns>The default culture for processing locale-dependent resource searches.</returns>
		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x0009FDBF File Offset: 0x0009DFBF
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x0009FDD1 File Offset: 0x0009DFD1
		[ConfigurationProperty("uiCulture", DefaultValue = "")]
		public string UICulture
		{
			get
			{
				return (string)base[GlobalizationSection.uiCultureProp];
			}
			set
			{
				base[GlobalizationSection.uiCultureProp] = value;
			}
		}

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x0009FDDF File Offset: 0x0009DFDF
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return GlobalizationSection.properties;
			}
		}

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06003D0D RID: 15629 RVA: 0x0009FDE6 File Offset: 0x0009DFE6
		internal bool IsAutoCulture
		{
			get
			{
				return this.autoCulture;
			}
		}

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06003D0E RID: 15630 RVA: 0x0009FDEE File Offset: 0x0009DFEE
		internal bool IsAutoUICulture
		{
			get
			{
				return this.autoUICulture;
			}
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x0009FDF8 File Offset: 0x0009DFF8
		private CultureInfo GetSanitizedCulture(string culture, ref bool auto)
		{
			auto = false;
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			if (culture.Length <= 3)
			{
				return new CultureInfo(culture);
			}
			if (!culture.StartsWith("auto"))
			{
				return new CultureInfo(culture);
			}
			auto = true;
			if (culture.Length > 5 && culture[4] == ':')
			{
				return new CultureInfo(culture.Substring(5));
			}
			return Helpers.InvariantCulture;
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x0009FE64 File Offset: 0x0009E064
		internal CultureInfo GetUICulture()
		{
			string uiculture = this.UICulture;
			if (this.cached_uiculture != uiculture)
			{
				try
				{
					this.cached_uicultureinfo = this.GetSanitizedCulture(uiculture, ref this.autoUICulture);
					this.cached_uiculture = uiculture;
				}
				catch
				{
					GlobalizationSection.CultureFailed("UICulture", uiculture);
					this.cached_uicultureinfo = new CultureInfo(127);
					this.cached_uiculture = null;
				}
			}
			return this.cached_uicultureinfo;
		}

		// Token: 0x06003D11 RID: 15633 RVA: 0x0009FEDC File Offset: 0x0009E0DC
		internal CultureInfo GetCulture()
		{
			string culture = this.Culture;
			if (this.cached_culture != culture)
			{
				try
				{
					this.cached_cultureinfo = this.GetSanitizedCulture(culture, ref this.autoCulture);
					this.cached_culture = culture;
				}
				catch
				{
					GlobalizationSection.CultureFailed("Culture", culture);
					this.cached_cultureinfo = new CultureInfo(127);
					this.cached_culture = null;
				}
			}
			return this.cached_cultureinfo;
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x0009FF54 File Offset: 0x0009E154
		private Encoding GetEncoding(ConfigurationProperty prop, ref string cached_encoding_name)
		{
			string text = (string)base[prop];
			if (cached_encoding_name == null)
			{
				cached_encoding_name = ((text == null) ? "utf-8" : text);
			}
			Encoding encoding = (Encoding)this.encodingHash[prop];
			if (encoding == null || encoding.WebName != cached_encoding_name)
			{
				try
				{
					string text2 = cached_encoding_name.ToLower(Helpers.InvariantCulture);
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text2);
					if (num <= 1769188890U)
					{
						if (num <= 662872037U)
						{
							if (num != 102844035U)
							{
								if (num != 103516576U)
								{
									if (num != 662872037U)
									{
										goto IL_01EC;
									}
									if (!(text2 == "utf-16le"))
									{
										goto IL_01EC;
									}
								}
								else if (!(text2 == "unicode"))
								{
									goto IL_01EC;
								}
							}
							else
							{
								if (!(text2 == "utf-8"))
								{
									goto IL_01EC;
								}
								goto IL_01E4;
							}
						}
						else if (num != 864497655U)
						{
							if (num != 998087601U)
							{
								if (num != 1769188890U)
								{
									goto IL_01EC;
								}
								if (!(text2 == "utf-16"))
								{
									goto IL_01EC;
								}
							}
							else if (!(text2 == "ucs-2"))
							{
								goto IL_01EC;
							}
						}
						else
						{
							if (!(text2 == "utf-16be"))
							{
								goto IL_01EC;
							}
							goto IL_01DA;
						}
					}
					else if (num <= 2579641873U)
					{
						if (num != 2342213115U)
						{
							if (num != 2421957552U)
							{
								if (num != 2579641873U)
								{
									goto IL_01EC;
								}
								if (!(text2 == "unicodefffe"))
								{
									goto IL_01EC;
								}
								goto IL_01DA;
							}
							else
							{
								if (!(text2 == "x-unicode-1-1-utf-8"))
								{
									goto IL_01EC;
								}
								goto IL_01E4;
							}
						}
						else
						{
							if (!(text2 == "unicode-2-0-utf-8"))
							{
								goto IL_01EC;
							}
							goto IL_01E4;
						}
					}
					else if (num != 2831744022U)
					{
						if (num != 3097952761U)
						{
							if (num != 3859930375U)
							{
								goto IL_01EC;
							}
							if (!(text2 == "iso-10646-ucs-2"))
							{
								goto IL_01EC;
							}
						}
						else
						{
							if (!(text2 == "unicode-1-1-utf-8"))
							{
								goto IL_01EC;
							}
							goto IL_01E4;
						}
					}
					else
					{
						if (!(text2 == "x-unicode-2-0-utf-8"))
						{
							goto IL_01EC;
						}
						goto IL_01E4;
					}
					encoding = new UnicodeEncoding(false, true);
					goto IL_01F4;
					IL_01DA:
					encoding = new UnicodeEncoding(true, true);
					goto IL_01F4;
					IL_01E4:
					encoding = Encoding.UTF8;
					goto IL_01F4;
					IL_01EC:
					encoding = Encoding.GetEncoding(cached_encoding_name);
					IL_01F4:;
				}
				catch
				{
					GlobalizationSection.EncodingFailed(prop.Name, cached_encoding_name);
					encoding = new UTF8Encoding(false, false);
				}
			}
			this.encodingHash[prop] = encoding;
			cached_encoding_name = encoding.WebName;
			return encoding;
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x000A01A4 File Offset: 0x0009E3A4
		private static void EncodingFailed(string att, string enc)
		{
			if (GlobalizationSection.encoding_warning)
			{
				return;
			}
			GlobalizationSection.encoding_warning = true;
			Console.WriteLine("Encoding {1} cannot be loaded.\n{0}=\"{1}\"\n", att, enc);
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x000A01C0 File Offset: 0x0009E3C0
		private static void CultureFailed(string att, string cul)
		{
			if (GlobalizationSection.culture_warning)
			{
				return;
			}
			GlobalizationSection.culture_warning = true;
			Console.WriteLine("Culture {1} cannot be loaded. Perhaps your runtime \ndon't have ICU support?\n{0}=\"{1}\"\n", att, cul);
		}

		// Token: 0x040020F1 RID: 8433
		private static ConfigurationProperty cultureProp = new ConfigurationProperty("culture", typeof(string), "");

		// Token: 0x040020F2 RID: 8434
		private static ConfigurationProperty enableBestFitResponseEncodingProp = new ConfigurationProperty("enableBestFitResponseEncoding", typeof(bool), false);

		// Token: 0x040020F3 RID: 8435
		private static ConfigurationProperty enableClientBasedCultureProp = new ConfigurationProperty("enableClientBasedCulture", typeof(bool), false);

		// Token: 0x040020F4 RID: 8436
		private static ConfigurationProperty fileEncodingProp = new ConfigurationProperty("fileEncoding", typeof(string));

		// Token: 0x040020F5 RID: 8437
		private static ConfigurationProperty requestEncodingProp = new ConfigurationProperty("requestEncoding", typeof(string), "utf-8");

		// Token: 0x040020F6 RID: 8438
		private static ConfigurationProperty resourceProviderFactoryTypeProp = new ConfigurationProperty("resourceProviderFactoryType", typeof(string), "");

		// Token: 0x040020F7 RID: 8439
		private static ConfigurationProperty responseEncodingProp = new ConfigurationProperty("responseEncoding", typeof(string), "utf-8");

		// Token: 0x040020F8 RID: 8440
		private static ConfigurationProperty responseHeaderEncodingProp = new ConfigurationProperty("responseHeaderEncoding", typeof(string), "utf-8");

		// Token: 0x040020F9 RID: 8441
		private static ConfigurationProperty uiCultureProp = new ConfigurationProperty("uiCulture", typeof(string), "");

		// Token: 0x040020FA RID: 8442
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040020FB RID: 8443
		private string cached_fileencoding;

		// Token: 0x040020FC RID: 8444
		private string cached_requestencoding;

		// Token: 0x040020FD RID: 8445
		private string cached_responseencoding;

		// Token: 0x040020FE RID: 8446
		private string cached_responseheaderencoding;

		// Token: 0x040020FF RID: 8447
		private Hashtable encodingHash;

		// Token: 0x04002100 RID: 8448
		private string cached_culture;

		// Token: 0x04002101 RID: 8449
		private CultureInfo cached_cultureinfo;

		// Token: 0x04002102 RID: 8450
		private string cached_uiculture;

		// Token: 0x04002103 RID: 8451
		private CultureInfo cached_uicultureinfo;

		// Token: 0x04002104 RID: 8452
		private static bool encoding_warning;

		// Token: 0x04002105 RID: 8453
		private static bool culture_warning;

		// Token: 0x04002106 RID: 8454
		private bool autoCulture;

		// Token: 0x04002107 RID: 8455
		private bool autoUICulture;
	}
}
