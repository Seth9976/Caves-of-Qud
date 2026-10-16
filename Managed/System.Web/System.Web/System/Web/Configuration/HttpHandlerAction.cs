using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Web.Util;

namespace System.Web.Configuration
{
	/// <summary>Configures an <see cref="T:System.Web.Configuration.HttpHandlersSection" /> configuration section. This class cannot be inherited.</summary>
	// Token: 0x020005A7 RID: 1447
	public sealed class HttpHandlerAction : ConfigurationElement
	{
		// Token: 0x06003DDF RID: 15839 RVA: 0x000A2004 File Offset: 0x000A0204
		static HttpHandlerAction()
		{
			HttpHandlerAction._properties.Add(HttpHandlerAction.pathProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.typeProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.validateProp);
			HttpHandlerAction._properties.Add(HttpHandlerAction.verbProp);
		}

		// Token: 0x06003DE0 RID: 15840 RVA: 0x0009CC6D File Offset: 0x0009AE6D
		internal HttpHandlerAction()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> class using the passed parameters. </summary>
		/// <param name="path">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path.</param>
		/// <param name="type">A comma-separated class/assembly combination consisting of version, culture, and public-key tokens.</param>
		/// <param name="verb">A comma-separated list of HTTP verbs (for example, "GET, PUT, POST").</param>
		// Token: 0x06003DE1 RID: 15841 RVA: 0x000A2103 File Offset: 0x000A0303
		public HttpHandlerAction(string path, string type, string verb)
			: this(path, type, verb, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> class using the passed parameters.</summary>
		/// <param name="path">The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path.</param>
		/// <param name="type">A comma-separated class/assembly combination consisting of version, culture, and public-key tokens.</param>
		/// <param name="verb">A comma-separated list of HTTP verbs (for example, "GET, PUT, POST").</param>
		/// <param name="validate">true to allow validation; otherwise, false. If false, ASP.NET will not attempt to load the class until the actual matching request comes, potentially delaying the error but improving the startup time.</param>
		// Token: 0x06003DE2 RID: 15842 RVA: 0x000A210F File Offset: 0x000A030F
		public HttpHandlerAction(string path, string type, string verb, bool validate)
		{
			this.Path = path;
			this.Type = type;
			this.Verb = verb;
			this.Validate = validate;
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> path. </summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> URL path. </returns>
		// Token: 0x17001379 RID: 4985
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x000A2134 File Offset: 0x000A0334
		// (set) Token: 0x06003DE4 RID: 15844 RVA: 0x000A2146 File Offset: 0x000A0346
		[ConfigurationProperty("path", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Path
		{
			get
			{
				return (string)base[HttpHandlerAction.pathProp];
			}
			set
			{
				base[HttpHandlerAction.pathProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> type.</summary>
		/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlerAction" /> type.</returns>
		// Token: 0x1700137A RID: 4986
		// (get) Token: 0x06003DE5 RID: 15845 RVA: 0x000A2154 File Offset: 0x000A0354
		// (set) Token: 0x06003DE6 RID: 15846 RVA: 0x000A2166 File Offset: 0x000A0366
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[HttpHandlerAction.typeProp];
			}
			set
			{
				base[HttpHandlerAction.typeProp] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> validation.</summary>
		/// <returns>true if the validation is allowed; otherwise, false.</returns>
		// Token: 0x1700137B RID: 4987
		// (get) Token: 0x06003DE7 RID: 15847 RVA: 0x000A2174 File Offset: 0x000A0374
		// (set) Token: 0x06003DE8 RID: 15848 RVA: 0x000A2186 File Offset: 0x000A0386
		[ConfigurationProperty("validate", DefaultValue = true)]
		public bool Validate
		{
			get
			{
				return (bool)base[HttpHandlerAction.validateProp];
			}
			set
			{
				base[HttpHandlerAction.validateProp] = value;
			}
		}

		/// <summary>Gets or sets the verb allowed by the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> object.</summary>
		/// <returns>The verb allowed by the object.</returns>
		// Token: 0x1700137C RID: 4988
		// (get) Token: 0x06003DE9 RID: 15849 RVA: 0x000A2199 File Offset: 0x000A0399
		// (set) Token: 0x06003DEA RID: 15850 RVA: 0x000A21AB File Offset: 0x000A03AB
		[ConfigurationProperty("verb", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Verb
		{
			get
			{
				return (string)base[HttpHandlerAction.verbProp];
			}
			set
			{
				base[HttpHandlerAction.verbProp] = value;
			}
		}

		// Token: 0x1700137D RID: 4989
		// (get) Token: 0x06003DEB RID: 15851 RVA: 0x000A21B9 File Offset: 0x000A03B9
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlerAction._properties;
			}
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000A21C0 File Offset: 0x000A03C0
		private string[] SplitVerbs()
		{
			if (this.Verb == "*")
			{
				this.cached_verbs = null;
			}
			else
			{
				this.cached_verbs = this.Verb.Split(',', StringSplitOptions.None);
			}
			return this.cached_verbs;
		}

		// Token: 0x1700137E RID: 4990
		// (get) Token: 0x06003DED RID: 15853 RVA: 0x000A21F7 File Offset: 0x000A03F7
		internal string[] Verbs
		{
			get
			{
				if (this.cached_verb != this.Verb)
				{
					this.cached_verbs = this.SplitVerbs();
					this.cached_verb = this.Verb;
				}
				return this.cached_verbs;
			}
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000A222C File Offset: 0x000A042C
		internal static Type LoadType(string type_name)
		{
			Type type = HttpApplication.LoadType(type_name, false);
			if (type == null)
			{
				throw new HttpException(string.Format("Failed to load httpHandler type `{0}'", type_name));
			}
			if (typeof(IHttpHandler).IsAssignableFrom(type) || typeof(IHttpHandlerFactory).IsAssignableFrom(type))
			{
				return type;
			}
			throw new HttpException(string.Format("Type {0} does not implement IHttpHandler or IHttpHandlerFactory", type_name));
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000A2294 File Offset: 0x000A0494
		internal bool PathMatches(string pathToMatch)
		{
			if (string.IsNullOrEmpty(pathToMatch))
			{
				return false;
			}
			bool flag = false;
			string[] array = this.Path.Split(',', StringSplitOptions.None);
			int num = pathToMatch.LastIndexOf('/');
			string text = pathToMatch;
			string text2 = null;
			if (num != -1)
			{
				pathToMatch = pathToMatch.Substring(num);
			}
			SearchPattern searchPattern = null;
			foreach (string text3 in array)
			{
				if (text3.Length != 0)
				{
					if (text3 == "*")
					{
						flag = true;
						break;
					}
					string text4 = null;
					string text5 = null;
					if (text3.Length > 0)
					{
						if (text3[0] == '*' && text3.IndexOf('*', 1) == -1)
						{
							text5 = text3.Substring(1);
						}
						if (text3.IndexOf('*') == -1 && text3[0] != '/')
						{
							HttpContext httpContext = HttpContext.Current;
							HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
							string text6 = ((httpRequest != null) ? httpRequest.BaseVirtualDir : HttpRuntime.AppDomainAppVirtualPath);
							if (text6 == "/")
							{
								text6 = string.Empty;
							}
							text4 = text6 + "/" + text3;
						}
					}
					if (text4 != null)
					{
						flag = text4.Length == text.Length && StrUtils.EndsWith(text, text4, true);
						if (flag)
						{
							break;
						}
					}
					else if (text5 != null)
					{
						flag = StrUtils.EndsWith(pathToMatch, text5, true);
						if (flag)
						{
							break;
						}
					}
					else
					{
						string text7;
						if (text3[0] == '/')
						{
							text7 = text3.Substring(1);
						}
						else
						{
							text7 = text3;
						}
						if (searchPattern == null)
						{
							searchPattern = new SearchPattern(text7, true);
						}
						else
						{
							searchPattern.SetPattern(text7, true);
						}
						if (text2 == null)
						{
							if (text[0] == '/')
							{
								text2 = text.Substring(1);
							}
							else
							{
								text2 = text;
							}
						}
						if (text7.IndexOf('/') >= 0)
						{
							text2 = HttpHandlerAction.AdjustPath(text7, text2);
						}
						if (searchPattern.IsMatch(text2))
						{
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06003DF0 RID: 15856 RVA: 0x000A2470 File Offset: 0x000A0670
		private static string AdjustPath(string pattern, string path)
		{
			int num = 0;
			for (int i = 0; i < pattern.Length; i++)
			{
				if (pattern[i] == '/')
				{
					num++;
				}
			}
			int j;
			for (j = path.Length - 1; j >= 0; j--)
			{
				if (path[j] == '/')
				{
					num--;
					if (num == -1)
					{
						break;
					}
				}
			}
			if (num >= 0 || j == 0)
			{
				return path;
			}
			return path.Substring(j + 1);
		}

		// Token: 0x06003DF1 RID: 15857 RVA: 0x000A24DC File Offset: 0x000A06DC
		internal object GetHandlerInstance()
		{
			IHttpHandler httpHandler = this.instance as IHttpHandler;
			if (this.instance == null || (httpHandler != null && !httpHandler.IsReusable))
			{
				if (this.type == null)
				{
					this.type = HttpHandlerAction.LoadType(this.Type);
				}
				this.instance = Activator.CreateInstance(this.type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null);
			}
			return this.instance;
		}

		// Token: 0x04002200 RID: 8704
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x04002201 RID: 8705
		private static ConfigurationProperty pathProp = new ConfigurationProperty("path", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002202 RID: 8706
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04002203 RID: 8707
		private static ConfigurationProperty validateProp = new ConfigurationProperty("validate", typeof(bool), true);

		// Token: 0x04002204 RID: 8708
		private static ConfigurationProperty verbProp = new ConfigurationProperty("verb", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002205 RID: 8709
		private object instance;

		// Token: 0x04002206 RID: 8710
		private Type type;

		// Token: 0x04002207 RID: 8711
		private string cached_verb;

		// Token: 0x04002208 RID: 8712
		private string[] cached_verbs;
	}
}
