using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides parsing at design time.</summary>
	// Token: 0x020001C5 RID: 453
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public static class DesignTimeTemplateParser
	{
		/// <summary>Parses and builds one control at design time.</summary>
		/// <returns>The built <see cref="T:System.Web.UI.Control" /> object.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x06001275 RID: 4725 RVA: 0x000305B8 File Offset: 0x0002E7B8
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static Control ParseControl(DesignTimeParseData data)
		{
			TemplateParser templateParser = DesignTimeTemplateParser.InitParser(data);
			templateParser.RootBuilder.Text = data.ParseText;
			if (templateParser.RootBuilder.Children == null)
			{
				return null;
			}
			using (IEnumerator enumerator = templateParser.RootBuilder.Children.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					return (Control)((ControlBuilder)enumerator.Current).CreateInstance();
				}
			}
			return null;
		}

		/// <summary>Parses a template at design time.</summary>
		/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the template.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x06001276 RID: 4726 RVA: 0x00030648 File Offset: 0x0002E848
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static ITemplate ParseTemplate(DesignTimeParseData data)
		{
			TemplateParser templateParser = DesignTimeTemplateParser.InitParser(data);
			templateParser.RootBuilder.Text = data.ParseText;
			return templateParser.RootBuilder;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x00030666 File Offset: 0x0002E866
		[global::System.MonoTODO]
		private static TemplateParser InitParser(DesignTimeParseData data)
		{
			return new PageParser();
		}

		/// <summary>Parses and builds one or more controls at design time.</summary>
		/// <returns>An array of built <see cref="T:System.Web.UI.Control" /> objects.</returns>
		/// <param name="data">Information used in creating the parser.</param>
		// Token: 0x06001278 RID: 4728 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
		public static Control[] ParseControls(DesignTimeParseData data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Parses a theme at design time.</summary>
		/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the theme.</returns>
		/// <param name="host">Manages designer components.</param>
		/// <param name="theme">The content to parse.</param>
		/// <param name="themePath">The path to the theme, which is used in creating the parser.</param>
		/// <exception cref="T:System.Exception">An error occurred while parsing the theme.</exception>
		// Token: 0x06001279 RID: 4729 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		public static ControlBuilder ParseTheme(IDesignerHost host, string theme, string themePath)
		{
			throw new NotImplementedException();
		}
	}
}
