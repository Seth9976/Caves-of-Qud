using System;

namespace System.Web.UI
{
	/// <summary>Provides an interface that designer tool developers can use to supply a set of <see cref="T:System.Web.UI.ThemeProvider" /> objects, which can be used to apply themes and control skins to controls in a design-time environment.</summary>
	// Token: 0x02000180 RID: 384
	public interface IThemeResolutionService
	{
		/// <summary>Gets an <see cref="T:System.Array" /> of <see cref="T:System.Web.UI.ThemeProvider" /> objects.</summary>
		/// <returns>An <see cref="T:System.Array" /> of <see cref="T:System.Web.UI.ThemeProvider" /> objects associated with the current <see cref="T:System.Web.UI.IThemeResolutionService" />.</returns>
		// Token: 0x06000F5A RID: 3930
		ThemeProvider[] GetAllThemeProviders();

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeProvider" /> object that represents the customization theme on an ASP.NET page. </summary>
		/// <returns>A <see cref="T:System.Web.UI.ThemeProvider" /> that represents the page theme that is applied to a control built by the <see cref="T:System.Web.UI.ControlBuilder" />.</returns>
		// Token: 0x06000F5B RID: 3931
		ThemeProvider GetThemeProvider();

		/// <summary>Gets a <see cref="T:System.Web.UI.ThemeProvider" /> object that represents the customization theme from a style sheet.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ThemeProvider" /> that represents the page theme that is applied to a control built by the <see cref="T:System.Web.UI.ControlBuilder" />.</returns>
		// Token: 0x06000F5C RID: 3932
		ThemeProvider GetStylesheetThemeProvider();
	}
}
