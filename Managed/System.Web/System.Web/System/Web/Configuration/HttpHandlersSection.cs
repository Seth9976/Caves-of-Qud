using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures an HTTP handler for a Web application. This class cannot be inherited.</summary>
	// Token: 0x020005A9 RID: 1449
	public sealed class HttpHandlersSection : ConfigurationSection
	{
		// Token: 0x06003E01 RID: 15873 RVA: 0x000A25F5 File Offset: 0x000A07F5
		static HttpHandlersSection()
		{
			HttpHandlersSection.properties.Add(HttpHandlersSection.handlersProp);
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects contained by the <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</summary>
		/// <returns>An <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> that contains the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects, or handlers.</returns>
		// Token: 0x17001383 RID: 4995
		// (get) Token: 0x06003E03 RID: 15875 RVA: 0x000A2631 File Offset: 0x000A0831
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public HttpHandlerActionCollection Handlers
		{
			get
			{
				return (HttpHandlerActionCollection)base[HttpHandlersSection.handlersProp];
			}
		}

		// Token: 0x17001384 RID: 4996
		// (get) Token: 0x06003E04 RID: 15876 RVA: 0x000A2643 File Offset: 0x000A0843
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpHandlersSection.properties;
			}
		}

		// Token: 0x06003E05 RID: 15877 RVA: 0x000A264C File Offset: 0x000A084C
		internal object LocateHandler(string verb, string filepath, out bool allowCache)
		{
			int count = this.Handlers.Count;
			for (int i = 0; i < count; i++)
			{
				HttpHandlerAction httpHandlerAction = this.Handlers[i];
				string[] verbs = httpHandlerAction.Verbs;
				if (verbs == null)
				{
					if (httpHandlerAction.PathMatches(filepath))
					{
						allowCache = httpHandlerAction.Path != "*";
						return httpHandlerAction.GetHandlerInstance();
					}
				}
				else
				{
					int j = verbs.Length;
					while (j > 0)
					{
						j--;
						if (!(verbs[j] != verb) && httpHandlerAction.PathMatches(filepath))
						{
							allowCache = httpHandlerAction.Path != "*";
							return httpHandlerAction.GetHandlerInstance();
						}
					}
				}
			}
			allowCache = false;
			return null;
		}

		// Token: 0x0400220A RID: 8714
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x0400220B RID: 8715
		private static ConfigurationProperty handlersProp = new ConfigurationProperty("", typeof(HttpHandlerActionCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
