using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.Web.UI
{
	/// <summary>Provides a class that encapsulates theme and style sheet information for controls in a designer environment. </summary>
	// Token: 0x02000236 RID: 566
	public sealed class ThemeProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ThemeProvider" /> class. </summary>
		/// <param name="host">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the current designer environment.</param>
		/// <param name="name">The name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> represents. This parameter can be null.</param>
		/// <param name="themeDefinition">Theme information passed to the <see cref="M:System.Web.UI.DesignTimeTemplateParser.ParseTheme(System.ComponentModel.Design.IDesignerHost,System.String,System.String)" /> method. These are the raw, concatenated contents of the theme.</param>
		/// <param name="cssFiles">The names of the cascading style sheet (CSS) files that are included with the theme that the <see cref="T:System.Web.UI.ThemeProvider" /> represents.</param>
		/// <param name="themePath">The absolute path of the file that contains the theme and control skin information for the named theme.</param>
		// Token: 0x06001766 RID: 5990 RVA: 0x0003DBC5 File Offset: 0x0003BDC5
		public ThemeProvider(IDesignerHost host, string name, string themeDefinition, string[] cssFiles, string themePath)
		{
			this.host = host;
			this.name = name;
			this.cssFiles = cssFiles;
		}

		/// <summary>Retrieves a <see cref="T:System.Web.UI.SkinBuilder" /> instance for the specified control, which is used to apply a theme and control skin in a designer environment.</summary>
		/// <returns>A <see cref="T:System.Web.UI.SkinBuilder" /> instance, if one is defined for the specified control type; otherwise, null.</returns>
		/// <param name="control">The control to apply a theme and control skin to.</param>
		// Token: 0x06001767 RID: 5991 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public SkinBuilder GetSkinBuilder(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Collections.IDictionary" /> object that contains a set of <see cref="T:System.Web.UI.SkinBuilder" /> objects for the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that contains a list of one or more <see cref="T:System.Web.UI.ControlBuilder" /> objects associated with the <see cref="T:System.Type" /> passed to the method. This list is populated with <see cref="T:System.Collections.DictionaryEntry" /> objects where the <see cref="P:System.Web.UI.Control.SkinID" /> is a <see cref="P:System.Collections.DictionaryEntry.Key" />, and its associated <see cref="T:System.Web.UI.SkinBuilder" /> is the <see cref="P:System.Collections.DictionaryEntry.Value" />. </returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to retrieve any associated <see cref="T:System.Web.UI.ControlBuilder" /> objects.</param>
		// Token: 0x06001768 RID: 5992 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public IDictionary GetSkinControlBuildersForControlType(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> of skin identifiers that are associated with the specified <see cref="T:System.Type" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of skin identifiers, if control skins are defined for the specified <see cref="T:System.Type" />. If no control skins are associated with the type, an empty <see cref="T:System.Collections.ICollection" /> is returned. </returns>
		/// <param name="type">A <see cref="T:System.Type" /> for which to retrieve any associated skin names.</param>
		// Token: 0x06001769 RID: 5993 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public ICollection GetSkinsForControl(Type type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the hash of the theme definition passed to the constructor.</summary>
		/// <returns>A hash code for the string passed as the theme definition to the class constructor.</returns>
		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public int ContentHashCode
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a set of strings that represent the names of any cascading style sheet (CSS) files associated with the current theme, if the theme is a style sheet theme.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of strings that identify the CSS files associated with the theme or style sheet theme. This property might return null. </returns>
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0003DBE3 File Offset: 0x0003BDE3
		public ICollection CssFiles
		{
			get
			{
				return this.cssFiles;
			}
		}

		/// <summary>Gets an <see cref="T:System.ComponentModel.Design.IDesignerHost" /> object that represents the current designer environment.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> that represents the current designer environment. </returns>
		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0003DBEB File Offset: 0x0003BDEB
		public IDesignerHost DesignerHost
		{
			get
			{
				return this.host;
			}
		}

		/// <summary>Gets the name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> instance represents.</summary>
		/// <returns>The name of the theme or style sheet theme that the <see cref="T:System.Web.UI.ThemeProvider" /> instance represents. </returns>
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0003DBF3 File Offset: 0x0003BDF3
		public string ThemeName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x040015E1 RID: 5601
		private IDesignerHost host;

		// Token: 0x040015E2 RID: 5602
		private string name;

		// Token: 0x040015E3 RID: 5603
		private string[] cssFiles;
	}
}
