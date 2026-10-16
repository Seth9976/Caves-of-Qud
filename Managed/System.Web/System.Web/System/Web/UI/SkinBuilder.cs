using System;

namespace System.Web.UI
{
	/// <summary>Provides a <see cref="T:System.Web.UI.ControlBuilder" /> object used at design time to apply control skins to controls. </summary>
	// Token: 0x02000222 RID: 546
	public sealed class SkinBuilder : ControlBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.SkinBuilder" /> class, setting the control with which the builder is associated, the general <see cref="T:System.Web.UI.ControlBuilder" /> object provided by the <see cref="T:System.Web.UI.ThemeProvider" /> object for the control, and a path to the theme file.</summary>
		/// <param name="provider">A <see cref="T:System.Web.UI.ThemeProvider" /> that encapsulates theme information for controls in a designer environment.</param>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> with which the <see cref="T:System.Web.UI.SkinBuilder" /> is associated. The <see cref="M:System.Web.UI.SkinBuilder.ApplyTheme" /> method applies a control skin to this control and returns it.</param>
		/// <param name="skinBuilder">A <see cref="T:System.Web.UI.ControlBuilder" /> provided by the <see cref="T:System.Web.UI.ThemeProvider" /> for the control's type. </param>
		/// <param name="themePath">The absolute path to the theme file. </param>
		// Token: 0x06001652 RID: 5714 RVA: 0x0003A30D File Offset: 0x0003850D
		public SkinBuilder(ThemeProvider provider, Control control, ControlBuilder skinBuilder, string themePath)
		{
			this.control = control;
		}

		/// <summary>Applies a theme and a control skin to the current control at design time, if a <see cref="T:System.Web.UI.SkinBuilder" /> object is associated with the control. </summary>
		/// <returns>The control instance to which the theme or style sheet theme and any control skin was applied. This is the same instance passed to the builder's <see cref="M:System.Web.UI.SkinBuilder.#ctor(System.Web.UI.ThemeProvider,System.Web.UI.Control,System.Web.UI.ControlBuilder,System.String)" /> constructor. This method will return null if no control was passed to the constructor.</returns>
		// Token: 0x06001653 RID: 5715 RVA: 0x0003A31C File Offset: 0x0003851C
		public Control ApplyTheme()
		{
			return this.control;
		}

		// Token: 0x04001573 RID: 5491
		private Control control;
	}
}
