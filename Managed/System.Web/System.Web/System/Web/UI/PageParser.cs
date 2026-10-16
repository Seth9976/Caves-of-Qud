using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI
{
	/// <summary>Implements a parser for .aspx files. This class cannot be inherited.</summary>
	// Token: 0x0200020C RID: 524
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PageParser : TemplateControlParser
	{
		/// <summary>Gets or sets a value that indicates whether ASP.NET should optimize its internal handling of long strings that are encountered when ASP.NET parses a page or control.</summary>
		/// <returns>true if ASP.NET should optimize its internal handling of long strings; otherwise false.</returns>
		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00037A46 File Offset: 0x00035C46
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x00037A4D File Offset: 0x00035C4D
		public static bool EnableLongStringsAsResources
		{
			get
			{
				return PageParser.enableLongStringsAsResources;
			}
			set
			{
				BuildManager.AssertPreStartMethodsRunning();
				PageParser.enableLongStringsAsResources = value;
			}
		}

		/// <summary>Gets or sets the type from which all pages are derived.</summary>
		/// <returns>The type.</returns>
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00037A5A File Offset: 0x00035C5A
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x00037A61 File Offset: 0x00035C61
		public static Type DefaultPageBaseType
		{
			get
			{
				return PageParser.defaultPageBaseType;
			}
			set
			{
				BuildManager.AssertPreStartMethodsRunning();
				if (value != null && !typeof(Page).IsAssignableFrom(value))
				{
					throw new ArgumentException(string.Format("The value assigned to property '{0}' is invalid.", "DefaultPageBaseType"));
				}
				PageParser.defaultPageBaseType = value;
			}
		}

		/// <summary>Gets or sets the type from which the <see cref="T:System.Web.HttpApplication" /> instance is derived.</summary>
		/// <returns>The type from which the <see cref="T:System.Web.HttpApplication" /> instance is derived.</returns>
		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00037A9E File Offset: 0x00035C9E
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00037AA5 File Offset: 0x00035CA5
		public static Type DefaultApplicationBaseType
		{
			get
			{
				return PageParser.defaultApplicationBaseType;
			}
			set
			{
				BuildManager.AssertPreStartMethodsRunning();
				if (value != null && !typeof(HttpApplication).IsAssignableFrom(value))
				{
					throw new ArgumentException(string.Format("The value assigned to property '{0}' is invalid.", "DefaultApplicationBaseType"));
				}
				PageParser.defaultApplicationBaseType = value;
			}
		}

		/// <summary>Gets or sets the page parser filter type that should be used at parse time.</summary>
		/// <returns>The type of the page parser filter.</returns>
		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00037AE2 File Offset: 0x00035CE2
		// (set) Token: 0x06001557 RID: 5463 RVA: 0x00037AE9 File Offset: 0x00035CE9
		public static Type DefaultPageParserFilterType
		{
			get
			{
				return PageParser.defaultPageParserFilterType;
			}
			set
			{
				BuildManager.AssertPreStartMethodsRunning();
				if (value != null && !typeof(PageParserFilter).IsAssignableFrom(value))
				{
					throw new ArgumentException(string.Format("The value assigned to property '{0}' is invalid.", "DefaultPageParserFilterType"));
				}
				PageParser.defaultPageParserFilterType = value;
			}
		}

		/// <summary>Gets or sets the type from which all user controls are derived.</summary>
		/// <returns>The type from which user controls are derived.</returns>
		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00037B26 File Offset: 0x00035D26
		// (set) Token: 0x06001559 RID: 5465 RVA: 0x00037B2D File Offset: 0x00035D2D
		public static Type DefaultUserControlBaseType
		{
			get
			{
				return PageParser.defaultUserControlBaseType;
			}
			set
			{
				if (value != null && !typeof(UserControl).IsAssignableFrom(value))
				{
					throw new ArgumentException(string.Format("The value assigned to property '{0}' is invalid.", "DefaultUserControlBaseType"));
				}
				BuildManager.AssertPreStartMethodsRunning();
				PageParser.defaultUserControlBaseType = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageParser" /> class. </summary>
		// Token: 0x0600155A RID: 5466 RVA: 0x00037B6A File Offset: 0x00035D6A
		public PageParser()
		{
			this.LoadConfigDefaults();
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00037B94 File Offset: 0x00035D94
		internal PageParser(string virtualPath, string inputFile, HttpContext context)
		{
			base.VirtualPath = new VirtualPath(virtualPath);
			base.Context = context;
			this.BaseVirtualDir = VirtualPathUtility.GetDirectory(virtualPath, false);
			base.InputFile = inputFile;
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00037BFD File Offset: 0x00035DFD
		internal PageParser(VirtualPath virtualPath, TextReader reader, HttpContext context)
			: this(virtualPath, null, reader, context)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00037C0C File Offset: 0x00035E0C
		internal PageParser(VirtualPath virtualPath, string inputFile, TextReader reader, HttpContext context)
		{
			base.VirtualPath = virtualPath;
			base.Context = context;
			this.BaseVirtualDir = virtualPath.DirectoryNoNormalize;
			this.Reader = reader;
			if (string.IsNullOrEmpty(inputFile))
			{
				base.InputFile = virtualPath.PhysicalPath;
			}
			else
			{
				base.InputFile = inputFile;
			}
			base.SetBaseType(null);
			base.AddApplicationAssembly();
			this.LoadConfigDefaults();
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00037C90 File Offset: 0x00035E90
		internal override void LoadConfigDefaults()
		{
			base.LoadConfigDefaults();
			PagesSection pagesConfig = base.PagesConfig;
			this.notBuffer = !pagesConfig.Buffer;
			this.enableSessionState = pagesConfig.EnableSessionState;
			this.enableViewStateMac = pagesConfig.EnableViewStateMac;
			this.smartNavigation = pagesConfig.SmartNavigation;
			this.validateRequest = pagesConfig.ValidateRequest;
			string masterPageFile = pagesConfig.MasterPageFile;
			if (masterPageFile.Length > 0)
			{
				this.masterPage = new MainDirectiveAttribute<string>(masterPageFile, true);
			}
			this.enable_event_validation = pagesConfig.EnableEventValidation;
			this.maxPageStateFieldLength = pagesConfig.MaxPageStateFieldLength;
			masterPageFile = pagesConfig.Theme;
			if (masterPageFile.Length > 0)
			{
				this.theme = new MainDirectiveAttribute<string>(masterPageFile, true);
			}
			this.styleSheetTheme = pagesConfig.StyleSheetTheme;
			if (this.styleSheetTheme.Length == 0)
			{
				this.styleSheetTheme = null;
			}
			this.maintainScrollPositionOnPostBack = pagesConfig.MaintainScrollPositionOnPostBack;
		}

		/// <summary>Returns an instance of a compiled page for the specific virtual path.</summary>
		/// <returns>Returns the compiled instance of the requested page. </returns>
		/// <param name="virtualPath">The virtual path of the requested file. </param>
		/// <param name="inputFile">The physical path of the page. </param>
		/// <param name="context">An object that contains information about the current Web request. </param>
		// Token: 0x0600155F RID: 5471 RVA: 0x00037D68 File Offset: 0x00035F68
		public static IHttpHandler GetCompiledPageInstance(string virtualPath, string inputFile, HttpContext context)
		{
			bool flag = false;
			if (!string.IsNullOrEmpty(inputFile))
			{
				flag = !inputFile.StartsWith(HttpRuntime.AppDomainAppPath);
			}
			return BuildManager.CreateInstanceFromVirtualPath(new VirtualPath(virtualPath, inputFile, flag), typeof(IHttpHandler)) as IHttpHandler;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00037DAC File Offset: 0x00035FAC
		internal override void ProcessMainAttributes(IDictionary atts)
		{
			string @string = BaseParser.GetString(atts, "EnableSessionState", null);
			if (@string != null)
			{
				if (string.Compare(@string, "readonly", true, Helpers.InvariantCulture) == 0)
				{
					this.enableSessionState = PagesEnableSessionState.ReadOnly;
				}
				else if (string.Compare(@string, "true", true, Helpers.InvariantCulture) == 0)
				{
					this.enableSessionState = PagesEnableSessionState.True;
				}
				else if (string.Compare(@string, "false", true, Helpers.InvariantCulture) == 0)
				{
					this.enableSessionState = PagesEnableSessionState.False;
				}
				else
				{
					base.ThrowParseException("Invalid value for enableSessionState: " + @string, Array.Empty<object>());
				}
			}
			string text = BaseParser.GetString(atts, "CodePage", null);
			if (text != null)
			{
				if (this.responseEncoding != null)
				{
					base.ThrowParseException("CodePage and ResponseEncoding are mutually exclusive.", Array.Empty<object>());
				}
				if (!BaseParser.IsExpression(text))
				{
					int num = -1;
					try
					{
						num = (int)uint.Parse(text);
					}
					catch
					{
						base.ThrowParseException("Invalid value for CodePage: " + text, Array.Empty<object>());
					}
					try
					{
						Encoding.GetEncoding(num);
					}
					catch
					{
						base.ThrowParseException("Unsupported codepage: " + text, Array.Empty<object>());
					}
					this.codepage = new MainDirectiveAttribute<int>(num, true);
				}
				else
				{
					this.codepage = new MainDirectiveAttribute<int>(text);
				}
			}
			text = BaseParser.GetString(atts, "ResponseEncoding", null);
			if (text != null)
			{
				if (this.codepage != null)
				{
					base.ThrowParseException("CodePage and ResponseEncoding are mutually exclusive.", Array.Empty<object>());
				}
				if (!BaseParser.IsExpression(text))
				{
					try
					{
						Encoding.GetEncoding(text);
					}
					catch
					{
						base.ThrowParseException("Unsupported encoding: " + text, Array.Empty<object>());
					}
					this.responseEncoding = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.responseEncoding = new MainDirectiveAttribute<string>(text);
				}
			}
			this.contentType = BaseParser.GetString(atts, "ContentType", null);
			text = BaseParser.GetString(atts, "LCID", null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					int num2 = -1;
					try
					{
						num2 = (int)uint.Parse(text);
					}
					catch
					{
						base.ThrowParseException("Invalid value for LCID: " + text, Array.Empty<object>());
					}
					CultureInfo cultureInfo = null;
					try
					{
						cultureInfo = new CultureInfo(num2);
					}
					catch
					{
						base.ThrowParseException("Unsupported LCID: " + text, Array.Empty<object>());
					}
					if (cultureInfo.IsNeutralCulture)
					{
						string text2 = PageParser.SuggestCulture(cultureInfo.Name);
						string text3 = "LCID attribute must be set to a non-neutral Culture.";
						if (text2 != null)
						{
							base.ThrowParseException(text3 + " Please try one of these: " + text2, Array.Empty<object>());
						}
						else
						{
							base.ThrowParseException(text3, Array.Empty<object>());
						}
					}
					this.lcid = new MainDirectiveAttribute<int>(num2, true);
				}
				else
				{
					this.lcid = new MainDirectiveAttribute<int>(text);
				}
			}
			this.culture = BaseParser.GetString(atts, "Culture", null);
			if (this.culture != null)
			{
				if (this.lcid != null)
				{
					base.ThrowParseException("Culture and LCID are mutually exclusive.", Array.Empty<object>());
				}
				CultureInfo cultureInfo2 = null;
				try
				{
					if (!this.culture.StartsWith("auto"))
					{
						cultureInfo2 = new CultureInfo(this.culture);
					}
				}
				catch
				{
					base.ThrowParseException("Unsupported Culture: " + this.culture, Array.Empty<object>());
				}
				if (cultureInfo2 != null && cultureInfo2.IsNeutralCulture)
				{
					string text4 = PageParser.SuggestCulture(this.culture);
					string text5 = "Culture attribute must be set to a non-neutral Culture.";
					if (text4 != null)
					{
						base.ThrowParseException(text5 + " Please try one of these: " + text4, Array.Empty<object>());
					}
					else
					{
						base.ThrowParseException(text5, Array.Empty<object>());
					}
				}
			}
			this.uiculture = BaseParser.GetString(atts, "UICulture", null);
			if (this.uiculture != null)
			{
				CultureInfo cultureInfo3 = null;
				try
				{
					if (!this.uiculture.StartsWith("auto"))
					{
						cultureInfo3 = new CultureInfo(this.uiculture);
					}
				}
				catch
				{
					base.ThrowParseException("Unsupported Culture: " + this.uiculture, Array.Empty<object>());
				}
				if (cultureInfo3 != null && cultureInfo3.IsNeutralCulture)
				{
					string text6 = PageParser.SuggestCulture(this.uiculture);
					string text7 = "UICulture attribute must be set to a non-neutral Culture.";
					if (text6 != null)
					{
						base.ThrowParseException(text7 + " Please try one of these: " + text6, Array.Empty<object>());
					}
					else
					{
						base.ThrowParseException(text7, Array.Empty<object>());
					}
				}
			}
			string string2 = BaseParser.GetString(atts, "Trace", null);
			if (string2 != null)
			{
				this.haveTrace = true;
				atts["Trace"] = string2;
				this.trace = base.GetBool(atts, "Trace", false);
			}
			string string3 = BaseParser.GetString(atts, "TraceMode", null);
			if (string3 != null)
			{
				bool flag = true;
				try
				{
					this.tracemode = (TraceMode)Enum.Parse(typeof(TraceMode), string3, false);
				}
				catch
				{
					flag = false;
				}
				if (!flag || this.tracemode == TraceMode.Default)
				{
					base.ThrowParseException("The 'tracemode' attribute is case sensitive and must be one of the following values: SortByTime, SortByCategory.", Array.Empty<object>());
				}
			}
			this.errorPage = BaseParser.GetString(atts, "ErrorPage", null);
			this.validateRequest = base.GetBool(atts, "ValidateRequest", this.validateRequest);
			text = BaseParser.GetString(atts, "ClientTarget", null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					text = text.Trim();
					ClientTargetSection configSection = base.GetConfigSection<ClientTargetSection>("system.web/clientTarget");
					ClientTarget clientTarget;
					if ((clientTarget = configSection.ClientTargets[text]) == null)
					{
						text = text.ToLowerInvariant();
					}
					if (clientTarget == null && (clientTarget = configSection.ClientTargets[text]) == null)
					{
						base.ThrowParseException(string.Format("ClientTarget '{0}' is an invalid alias. See the documentation for <clientTarget> config. section.", this.clientTarget), Array.Empty<object>());
					}
					text = clientTarget.UserAgent;
					this.clientTarget = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.clientTarget = new MainDirectiveAttribute<string>(text);
				}
			}
			this.notBuffer = !base.GetBool(atts, "Buffer", true);
			this.async = base.GetBool(atts, "Async", false);
			string string4 = BaseParser.GetString(atts, "AsyncTimeout", null);
			if (string4 != null)
			{
				try
				{
					this.asyncTimeout = int.Parse(string4);
				}
				catch (Exception)
				{
					base.ThrowParseException("AsyncTimeout must be an integer value", Array.Empty<object>());
				}
			}
			text = BaseParser.GetString(atts, "MasterPageFile", (this.masterPage != null) ? this.masterPage.Value : null);
			if (!string.IsNullOrEmpty(text))
			{
				if (!BaseParser.IsExpression(text))
				{
					text = VirtualPathUtility.Combine(this.BaseVirtualDir, text);
					VirtualPathProvider virtualPathProvider = HostingEnvironment.VirtualPathProvider;
					if (!virtualPathProvider.FileExists(text))
					{
						base.ThrowParseFileNotFound(text, Array.Empty<object>());
					}
					text = virtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, VirtualPathUtility.ToAbsolute(text));
					this.AddDependency(text, false);
					this.masterPage = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.masterPage = new MainDirectiveAttribute<string>(text);
				}
			}
			text = BaseParser.GetString(atts, "Title", null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					this.title = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.title = new MainDirectiveAttribute<string>(text);
				}
			}
			text = BaseParser.GetString(atts, "Theme", (this.theme != null) ? this.theme.Value : null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					this.theme = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.theme = new MainDirectiveAttribute<string>(text);
				}
			}
			this.styleSheetTheme = BaseParser.GetString(atts, "StyleSheetTheme", this.styleSheetTheme);
			this.enable_event_validation = base.GetBool(atts, "EnableEventValidation", this.enable_event_validation);
			this.maintainScrollPositionOnPostBack = base.GetBool(atts, "MaintainScrollPositionOnPostBack", this.maintainScrollPositionOnPostBack);
			if (atts.Contains("EnableViewStateMac"))
			{
				this.enableViewStateMac = base.GetBool(atts, "EnableViewStateMac", this.enableViewStateMac);
				this.enableViewStateMacSet = true;
			}
			text = BaseParser.GetString(atts, "MetaDescription", null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					this.metaDescription = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.metaDescription = new MainDirectiveAttribute<string>(text);
				}
			}
			text = BaseParser.GetString(atts, "MetaKeywords", null);
			if (text != null)
			{
				if (!BaseParser.IsExpression(text))
				{
					this.metaKeywords = new MainDirectiveAttribute<string>(text, true);
				}
				else
				{
					this.metaKeywords = new MainDirectiveAttribute<string>(text);
				}
			}
			BaseParser.GetString(atts, "SmartNavigation", null);
			base.ProcessMainAttributes(atts);
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000385A8 File Offset: 0x000367A8
		internal override void AddDirective(string directive, IDictionary atts)
		{
			bool flag = string.Compare("MasterType", directive, StringComparison.OrdinalIgnoreCase) == 0;
			bool flag2 = !flag && string.Compare("PreviousPageType", directive, StringComparison.OrdinalIgnoreCase) == 0;
			Type type = null;
			if (flag || flag2)
			{
				PageParserFilter pageParserFilter = base.PageParserFilter;
				if (pageParserFilter != null)
				{
					pageParserFilter.PreprocessDirective(directive.ToLowerInvariant(), atts);
				}
				string @string = BaseParser.GetString(atts, "TypeName", null);
				string string2 = BaseParser.GetString(atts, "VirtualPath", null);
				if (@string != null && string2 != null)
				{
					base.ThrowParseException(string.Format("The '{0}' directive must have exactly one attribute: TypeName or VirtualPath", directive), Array.Empty<object>());
				}
				if (@string != null)
				{
					type = base.LoadType(@string);
					if (type == null)
					{
						base.ThrowParseException(string.Format("Could not load type '{0}'.", @string), Array.Empty<object>());
					}
					if (flag)
					{
						this.masterType = type;
					}
					else
					{
						this.previousPageType = type;
					}
				}
				else if (!string.IsNullOrEmpty(string2))
				{
					if (!HostingEnvironment.VirtualPathProvider.FileExists(string2))
					{
						base.ThrowParseFileNotFound(string2, Array.Empty<object>());
					}
					this.AddDependency(string2, true);
					if (flag)
					{
						this.masterVirtualPath = string2;
					}
					else
					{
						this.previousPageVirtualPath = string2;
					}
				}
				else
				{
					base.ThrowParseException(string.Format("The {0} directive must have either a TypeName or a VirtualPath attribute.", directive), Array.Empty<object>());
				}
				if (type != null)
				{
					this.AddAssembly(type.Assembly, true);
					return;
				}
			}
			else
			{
				base.AddDirective(directive, atts);
			}
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000386F4 File Offset: 0x000368F4
		private static string SuggestCulture(string culture)
		{
			string text = null;
			foreach (CultureInfo cultureInfo in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
			{
				if (cultureInfo.Name.StartsWith(culture))
				{
					text = text + cultureInfo.Name + " ";
				}
			}
			return text;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0003873D File Offset: 0x0003693D
		internal Type GetCompiledPageType(string virtualPath, string inputFile, HttpContext context)
		{
			return BuildManager.GetCompiledType(virtualPath);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00038745 File Offset: 0x00036945
		internal override Type CompileIntoType()
		{
			return new AspGenerator(this).GetCompiledType();
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00038752 File Offset: 0x00036952
		internal bool EnableSessionState
		{
			get
			{
				return this.enableSessionState == PagesEnableSessionState.True || this.ReadOnlySessionState;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00038765 File Offset: 0x00036965
		internal bool EnableViewStateMac
		{
			get
			{
				return this.enableViewStateMac;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x0003876D File Offset: 0x0003696D
		internal bool EnableViewStateMacSet
		{
			get
			{
				return this.enableViewStateMacSet;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00038775 File Offset: 0x00036975
		internal bool SmartNavigation
		{
			get
			{
				return this.smartNavigation;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x0003877D File Offset: 0x0003697D
		internal bool ReadOnlySessionState
		{
			get
			{
				return this.enableSessionState == PagesEnableSessionState.ReadOnly;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00038788 File Offset: 0x00036988
		internal bool HaveTrace
		{
			get
			{
				return this.haveTrace;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00038790 File Offset: 0x00036990
		internal bool Trace
		{
			get
			{
				return this.trace;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x0600156C RID: 5484 RVA: 0x00038798 File Offset: 0x00036998
		internal TraceMode TraceMode
		{
			get
			{
				return this.tracemode;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000387A0 File Offset: 0x000369A0
		internal override Type DefaultBaseType
		{
			get
			{
				Type type = PageParser.DefaultPageBaseType;
				if (type == null)
				{
					return base.DefaultBaseType;
				}
				return type;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x0600156E RID: 5486 RVA: 0x000387C4 File Offset: 0x000369C4
		internal override string DefaultBaseTypeName
		{
			get
			{
				return base.PagesConfig.PageBaseType;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x000387D1 File Offset: 0x000369D1
		internal override string DefaultDirectiveName
		{
			get
			{
				return "page";
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x000387D8 File Offset: 0x000369D8
		internal string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x000387E0 File Offset: 0x000369E0
		internal MainDirectiveAttribute<string> ResponseEncoding
		{
			get
			{
				return this.responseEncoding;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x000387E8 File Offset: 0x000369E8
		internal MainDirectiveAttribute<int> CodePage
		{
			get
			{
				return this.codepage;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000387F0 File Offset: 0x000369F0
		internal MainDirectiveAttribute<int> LCID
		{
			get
			{
				return this.lcid;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000387F8 File Offset: 0x000369F8
		internal MainDirectiveAttribute<string> ClientTarget
		{
			get
			{
				return this.clientTarget;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001575 RID: 5493 RVA: 0x00038800 File Offset: 0x00036A00
		internal MainDirectiveAttribute<string> MasterPageFile
		{
			get
			{
				return this.masterPage;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00038808 File Offset: 0x00036A08
		internal MainDirectiveAttribute<string> Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x00038810 File Offset: 0x00036A10
		internal MainDirectiveAttribute<string> Theme
		{
			get
			{
				return this.theme;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00038818 File Offset: 0x00036A18
		internal MainDirectiveAttribute<string> MetaDescription
		{
			get
			{
				return this.metaDescription;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x00038820 File Offset: 0x00036A20
		internal MainDirectiveAttribute<string> MetaKeywords
		{
			get
			{
				return this.metaKeywords;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00038828 File Offset: 0x00036A28
		internal string Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00038830 File Offset: 0x00036A30
		internal string UICulture
		{
			get
			{
				return this.uiculture;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00038838 File Offset: 0x00036A38
		internal string ErrorPage
		{
			get
			{
				return this.errorPage;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00038840 File Offset: 0x00036A40
		internal bool ValidateRequest
		{
			get
			{
				return this.validateRequest;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00038848 File Offset: 0x00036A48
		internal bool NotBuffer
		{
			get
			{
				return this.notBuffer;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00038850 File Offset: 0x00036A50
		internal bool Async
		{
			get
			{
				return this.async;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00038858 File Offset: 0x00036A58
		internal int AsyncTimeout
		{
			get
			{
				return this.asyncTimeout;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00038860 File Offset: 0x00036A60
		internal string StyleSheetTheme
		{
			get
			{
				return this.styleSheetTheme;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00038868 File Offset: 0x00036A68
		internal Type MasterType
		{
			get
			{
				if (this.masterType == null && !string.IsNullOrEmpty(this.masterVirtualPath))
				{
					this.masterType = BuildManager.GetCompiledType(this.masterVirtualPath);
				}
				return this.masterType;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x0003889C File Offset: 0x00036A9C
		internal bool EnableEventValidation
		{
			get
			{
				return this.enable_event_validation;
			}
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x000388A4 File Offset: 0x00036AA4
		internal bool MaintainScrollPositionOnPostBack
		{
			get
			{
				return this.maintainScrollPositionOnPostBack;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x000388AC File Offset: 0x00036AAC
		internal int MaxPageStateFieldLength
		{
			get
			{
				return this.maxPageStateFieldLength;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x000388B4 File Offset: 0x00036AB4
		internal Type PreviousPageType
		{
			get
			{
				if (this.previousPageType == null && !string.IsNullOrEmpty(this.previousPageVirtualPath))
				{
					string text = base.MapPath(this.previousPageVirtualPath);
					this.previousPageType = this.GetCompiledPageType(this.previousPageVirtualPath, text, HttpContext.Current);
				}
				return this.previousPageType;
			}
		}

		// Token: 0x0400150B RID: 5387
		private static Type defaultPageBaseType;

		// Token: 0x0400150C RID: 5388
		private static Type defaultApplicationBaseType;

		// Token: 0x0400150D RID: 5389
		private static Type defaultPageParserFilterType;

		// Token: 0x0400150E RID: 5390
		private static Type defaultUserControlBaseType;

		// Token: 0x0400150F RID: 5391
		private static bool enableLongStringsAsResources = true;

		// Token: 0x04001510 RID: 5392
		private PagesEnableSessionState enableSessionState = PagesEnableSessionState.True;

		// Token: 0x04001511 RID: 5393
		private bool enableViewStateMac;

		// Token: 0x04001512 RID: 5394
		private bool enableViewStateMacSet;

		// Token: 0x04001513 RID: 5395
		private bool smartNavigation;

		// Token: 0x04001514 RID: 5396
		private bool haveTrace;

		// Token: 0x04001515 RID: 5397
		private bool trace;

		// Token: 0x04001516 RID: 5398
		private bool notBuffer;

		// Token: 0x04001517 RID: 5399
		private TraceMode tracemode = TraceMode.Default;

		// Token: 0x04001518 RID: 5400
		private string contentType;

		// Token: 0x04001519 RID: 5401
		private MainDirectiveAttribute<int> codepage;

		// Token: 0x0400151A RID: 5402
		private MainDirectiveAttribute<string> responseEncoding;

		// Token: 0x0400151B RID: 5403
		private MainDirectiveAttribute<int> lcid;

		// Token: 0x0400151C RID: 5404
		private MainDirectiveAttribute<string> clientTarget;

		// Token: 0x0400151D RID: 5405
		private MainDirectiveAttribute<string> masterPage;

		// Token: 0x0400151E RID: 5406
		private MainDirectiveAttribute<string> title;

		// Token: 0x0400151F RID: 5407
		private MainDirectiveAttribute<string> theme;

		// Token: 0x04001520 RID: 5408
		private MainDirectiveAttribute<string> metaDescription;

		// Token: 0x04001521 RID: 5409
		private MainDirectiveAttribute<string> metaKeywords;

		// Token: 0x04001522 RID: 5410
		private string culture;

		// Token: 0x04001523 RID: 5411
		private string uiculture;

		// Token: 0x04001524 RID: 5412
		private string errorPage;

		// Token: 0x04001525 RID: 5413
		private bool validateRequest;

		// Token: 0x04001526 RID: 5414
		private bool async;

		// Token: 0x04001527 RID: 5415
		private int asyncTimeout = -1;

		// Token: 0x04001528 RID: 5416
		private Type masterType;

		// Token: 0x04001529 RID: 5417
		private string masterVirtualPath;

		// Token: 0x0400152A RID: 5418
		private string styleSheetTheme;

		// Token: 0x0400152B RID: 5419
		private bool enable_event_validation;

		// Token: 0x0400152C RID: 5420
		private bool maintainScrollPositionOnPostBack;

		// Token: 0x0400152D RID: 5421
		private int maxPageStateFieldLength = -1;

		// Token: 0x0400152E RID: 5422
		private Type previousPageType;

		// Token: 0x0400152F RID: 5423
		private string previousPageVirtualPath;
	}
}
