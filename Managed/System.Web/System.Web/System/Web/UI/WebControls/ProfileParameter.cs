using System;
using System.ComponentModel;
using System.Data;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of an ASP.NET Profile property to a parameter object. </summary>
	// Token: 0x020003F1 RID: 1009
	[DefaultProperty("PropertyName")]
	public class ProfileParameter : Parameter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class.</summary>
		// Token: 0x06002C80 RID: 11392 RVA: 0x0004DC5C File Offset: 0x0004BE5C
		public ProfileParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance from which the current instance is initialized.</param>
		// Token: 0x06002C81 RID: 11393 RVA: 0x00074400 File Offset: 0x00072600
		protected ProfileParameter(ProfileParameter original)
			: base(original)
		{
			this.PropertyName = original.PropertyName;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06002C82 RID: 11394 RVA: 0x00074415 File Offset: 0x00072615
		public ProfileParameter(string name, string propertyName)
			: base(name)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to.</summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />.</param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />.</param>
		// Token: 0x06002C83 RID: 11395 RVA: 0x00074425 File Offset: 0x00072625
		public ProfileParameter(string name, TypeCode type, string propertyName)
			: base(name, type)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> class, using the specified property name to identify which ASP.NET Profile property to bind to. </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <param name="dbType">The database type that the parameter represents. </param>
		/// <param name="propertyName">The name of the ASP.NET Profile property that the parameter object is bound to.</param>
		// Token: 0x06002C84 RID: 11396 RVA: 0x00074436 File Offset: 0x00072636
		public ProfileParameter(string name, DbType dbType, string propertyName)
			: base(name, dbType)
		{
			this.PropertyName = propertyName;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object that is an exact duplicate of the current one.</returns>
		// Token: 0x06002C85 RID: 11397 RVA: 0x00074447 File Offset: 0x00072647
		protected override Parameter Clone()
		{
			return new ProfileParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the ASP.NET Profile is null (Nothing in Visual Basic), the <see cref="M:System.Web.UI.WebControls.ProfileParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Form where the <see cref="T:System.Web.UI.WebControls.ProfileParameter" /> is used.</param>
		// Token: 0x06002C86 RID: 11398 RVA: 0x0007444F File Offset: 0x0007264F
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Profile == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(this.PropertyName))
			{
				return null;
			}
			return context.Profile[this.PropertyName];
		}

		/// <summary>Gets or sets the name of the ASP.NET Profile property that the parameter binds to.</summary>
		/// <returns>A string that identifies the ASP.NET Profile property that the parameter binds to.</returns>
		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002C87 RID: 11399 RVA: 0x00074480 File Offset: 0x00072680
		// (set) Token: 0x06002C88 RID: 11400 RVA: 0x000744AD File Offset: 0x000726AD
		[DefaultValue("")]
		public string PropertyName
		{
			get
			{
				object obj = base.ViewState["PropertyName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PropertyName"] = value;
			}
		}
	}
}
