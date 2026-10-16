using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the string value to use as a ToolTip for a property of a Web Parts control.</summary>
	// Token: 0x0200046D RID: 1133
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class. </summary>
		// Token: 0x060033D5 RID: 13269 RVA: 0x00087E35 File Offset: 0x00086035
		public WebDescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the specified description.</summary>
		/// <param name="description">The ToolTip to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />. </param>
		// Token: 0x060033D6 RID: 13270 RVA: 0x00087E42 File Offset: 0x00086042
		public WebDescriptionAttribute(string description)
		{
			this._description = description;
		}

		/// <summary>Gets the ToolTip for a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x060033D7 RID: 13271 RVA: 0x00087E51 File Offset: 0x00086051
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the ToolTip to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x060033D8 RID: 13272 RVA: 0x00087E59 File Offset: 0x00086059
		// (set) Token: 0x060033D9 RID: 13273 RVA: 0x00087E61 File Offset: 0x00086061
		protected string DescriptionValue
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or null. </param>
		// Token: 0x060033DA RID: 13274 RVA: 0x00087E6C File Offset: 0x0008606C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDescriptionAttribute webDescriptionAttribute = obj as WebDescriptionAttribute;
			return webDescriptionAttribute != null && webDescriptionAttribute.Description == this.Description;
		}

		/// <summary>Returns the hash code for the display name value.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060033DB RID: 13275 RVA: 0x00087E9C File Offset: 0x0008609C
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Determines whether the current instance is set to the default value.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x060033DC RID: 13276 RVA: 0x00087EA9 File Offset: 0x000860A9
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDescriptionAttribute.Default);
		}

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDescriptionAttribute.Description" /> property set to an empty string ("").</summary>
		// Token: 0x04001CE7 RID: 7399
		public static readonly WebDescriptionAttribute Default = new WebDescriptionAttribute();

		// Token: 0x04001CE8 RID: 7400
		private string _description;
	}
}
