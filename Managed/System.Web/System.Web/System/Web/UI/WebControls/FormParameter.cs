using System;
using System.ComponentModel;
using System.Data;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of an HTTP request <see cref="P:System.Web.HttpRequest.Form" /> field to a parameter object.</summary>
	// Token: 0x02000395 RID: 917
	[DefaultProperty("FormField")]
	public class FormParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class.</summary>
		// Token: 0x0600240F RID: 9231 RVA: 0x0004DC5C File Offset: 0x0004BE5C
		public FormParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.FormParameter" /> instance that the current instance is initialized from. </param>
		// Token: 0x06002410 RID: 9232 RVA: 0x0005BB86 File Offset: 0x00059D86
		protected FormParameter(FormParameter original)
			: base(original)
		{
			this.FormField = original.FormField;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable field to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="formField">The name of the form variable that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06002411 RID: 9233 RVA: 0x0005BB9B File Offset: 0x00059D9B
		public FormParameter(string name, string formField)
			: base(name)
		{
			this.FormField = formField;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="formField">The name of the form variable that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06002412 RID: 9234 RVA: 0x0005BBAB File Offset: 0x00059DAB
		public FormParameter(string name, TypeCode type, string formField)
			: base(name, type)
		{
			this.FormField = formField;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> class, using the specified string to identify which form variable field to bind to. </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dbType">The database type of the parameter.</param>
		/// <param name="formField">The name of the form variable that the parameter object is bound to. </param>
		// Token: 0x06002413 RID: 9235 RVA: 0x0005BBBC File Offset: 0x00059DBC
		public FormParameter(string name, DbType dbType, string formField)
			: base(name, dbType)
		{
			this.FormField = formField;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.FormParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormParameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06002414 RID: 9236 RVA: 0x0005BBCD File Offset: 0x00059DCD
		protected override Parameter Clone()
		{
			return new FormParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.FormParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is null (Nothing in Visual Basic), the <see cref="M:System.Web.UI.WebControls.FormParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the page where the <see cref="T:System.Web.UI.WebControls.FormParameter" /> is used. </param>
		// Token: 0x06002415 RID: 9237 RVA: 0x0005BBD8 File Offset: 0x00059DD8
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			HttpRequest httpRequest = ((context != null) ? context.Request : null);
			if (httpRequest == null)
			{
				return null;
			}
			return httpRequest.Form[this.FormField];
		}

		/// <summary>Gets or sets the name of the form variable that the parameter binds to.</summary>
		/// <returns>A string that identifies the form variable to which the parameter binds.</returns>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x0005BC08 File Offset: 0x00059E08
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x0005BC35 File Offset: 0x00059E35
		[DefaultValue("")]
		public string FormField
		{
			get
			{
				string text = base.ViewState["FormField"] as string;
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (this.FormField != value)
				{
					base.ViewState["FormField"] = value;
					base.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether the client input in the parameter is validated.</summary>
		/// <returns>true if client input is validated; otherwise, false.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x0005BC5C File Offset: 0x00059E5C
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public bool ValidateInput
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
