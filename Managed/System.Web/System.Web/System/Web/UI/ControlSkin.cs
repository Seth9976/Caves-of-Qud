using System;
using System.ComponentModel;

namespace System.Web.UI
{
	/// <summary>Represents a control skin, which is a means to define stylistic properties that are applied to an ASP.NET Web server control. </summary>
	// Token: 0x020001B5 RID: 437
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public class ControlSkin
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.ControlSkin" /> class using the passed <see cref="T:System.Web.UI.Control" /> type and delegate.</summary>
		/// <param name="controlType">The <see cref="T:System.Type" /> of <see cref="T:System.Web.UI.Control" /> to which the skin is applied, used to enforce type consistency among named skins. </param>
		/// <param name="themeDelegate">The <see cref="T:System.Web.UI.ControlSkinDelegate" /> that applies the style elements defined in a control skin file to the type identified by the <paramref name="controlType" /> parameter. </param>
		// Token: 0x060011BB RID: 4539 RVA: 0x0002F153 File Offset: 0x0002D353
		public ControlSkin(Type controlType, ControlSkinDelegate themeDelegate)
		{
			this.controlType = controlType;
			this.themeDelegate = themeDelegate;
		}

		/// <summary>Applies the skin to the <see cref="T:System.Web.UI.Control" /> control contained by the <see cref="T:System.Web.UI.ControlSkin" /> object.</summary>
		/// <param name="control">The control to which to apply the skin. </param>
		// Token: 0x060011BC RID: 4540 RVA: 0x0002F169 File Offset: 0x0002D369
		public void ApplySkin(Control control)
		{
			this.themeDelegate(control);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control that the <see cref="T:System.Web.UI.ControlSkin" /> object is associated with.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.Control" /> used in this instance.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0002F178 File Offset: 0x0002D378
		public Type ControlType
		{
			get
			{
				return this.controlType;
			}
		}

		// Token: 0x04001402 RID: 5122
		private Type controlType;

		// Token: 0x04001403 RID: 5123
		private ControlSkinDelegate themeDelegate;
	}
}
