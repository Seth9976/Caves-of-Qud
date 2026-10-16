using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the friendly name for a property of a Web Parts control.</summary>
	// Token: 0x0200046E RID: 1134
	[AttributeUsage(AttributeTargets.Property)]
	public class WebDisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class without a specified name. </summary>
		// Token: 0x060033DE RID: 13278 RVA: 0x00087EC2 File Offset: 0x000860C2
		public WebDisplayNameAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with a specified display name.</summary>
		/// <param name="displayName">The friendly name to use in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.  </param>
		// Token: 0x060033DF RID: 13279 RVA: 0x00087ECF File Offset: 0x000860CF
		public WebDisplayNameAttribute(string displayName)
		{
			this._displayName = displayName;
		}

		/// <summary>Gets the name of a property to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x060033E0 RID: 13280 RVA: 0x00087EDE File Offset: 0x000860DE
		public virtual string DisplayName
		{
			get
			{
				return this.DisplayNameValue;
			}
		}

		/// <summary>Gets or sets the name to display in the <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" /> control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value to display in a <see cref="T:System.Web.UI.WebControls.WebParts.PropertyGridEditorPart" />.</returns>
		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x060033E1 RID: 13281 RVA: 0x00087EE6 File Offset: 0x000860E6
		// (set) Token: 0x060033E2 RID: 13282 RVA: 0x00087EEE File Offset: 0x000860EE
		protected string DisplayNameValue
		{
			get
			{
				return this._displayName;
			}
			set
			{
				this._displayName = value;
			}
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance, or null. </param>
		// Token: 0x060033E3 RID: 13283 RVA: 0x00087EF8 File Offset: 0x000860F8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			WebDisplayNameAttribute webDisplayNameAttribute = obj as WebDisplayNameAttribute;
			return webDisplayNameAttribute != null && webDisplayNameAttribute.DisplayName == this.DisplayName;
		}

		/// <summary>Returns the hash code for the display name value.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060033E4 RID: 13284 RVA: 0x00087F28 File Offset: 0x00086128
		public override int GetHashCode()
		{
			return this.DisplayName.GetHashCode();
		}

		/// <summary>Determines whether the current instance is set to the default value.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> equals <see cref="F:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.Default" />; otherwise, false.</returns>
		// Token: 0x060033E5 RID: 13285 RVA: 0x00087F35 File Offset: 0x00086135
		public override bool IsDefaultAttribute()
		{
			return this.Equals(WebDisplayNameAttribute.Default);
		}

		/// <summary>Represents an instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute" /> class with the <see cref="P:System.Web.UI.WebControls.WebParts.WebDisplayNameAttribute.DisplayName" /> property set to an empty string ("").</summary>
		// Token: 0x04001CE9 RID: 7401
		public static readonly WebDisplayNameAttribute Default = new WebDisplayNameAttribute();

		// Token: 0x04001CEA RID: 7402
		private string _displayName;
	}
}
