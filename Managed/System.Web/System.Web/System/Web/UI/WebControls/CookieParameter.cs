using System;
using System.ComponentModel;
using System.Data;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Binds the value of a client-side HTTP cookie to a parameter object. The parameter can be used in a parameterized query or command to select, filter, or update data.</summary>
	// Token: 0x0200035B RID: 859
	[DefaultProperty("CookieName")]
	public class CookieParameter : Parameter
	{
		/// <summary>Initializes a new unnamed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class.</summary>
		// Token: 0x06001FB2 RID: 8114 RVA: 0x0004DC5C File Offset: 0x0004BE5C
		public CookieParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class with the values of the instance specified by the <paramref name="original" /> parameter.</summary>
		/// <param name="original">A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> from which the current instance is initialized. </param>
		// Token: 0x06001FB3 RID: 8115 RVA: 0x0004DE2B File Offset: 0x0004C02B
		protected CookieParameter(CookieParameter original)
			: base(original)
		{
			this.CookieName = original.CookieName;
		}

		/// <summary>Initializes a new named instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FB4 RID: 8116 RVA: 0x0004DE40 File Offset: 0x0004C040
		public CookieParameter(string name, string cookieName)
			: base(name)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Initializes a new named and strongly typed instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class, using the specified string to identify which HTTP cookie to bind to.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">The type that the parameter represents. The default is <see cref="F:System.TypeCode.Object" />. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FB5 RID: 8117 RVA: 0x0004DE50 File Offset: 0x0004C050
		public CookieParameter(string name, TypeCode type, string cookieName)
			: base(name, type)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> class that has the specified name and database type and that is bound to the specified HTTP cookie.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="dbType">The database type that the parameter represents. </param>
		/// <param name="cookieName">The name of the HTTP cookie that the parameter object is bound to. The default is <see cref="F:System.String.Empty" />. </param>
		// Token: 0x06001FB6 RID: 8118 RVA: 0x0004DE61 File Offset: 0x0004C061
		public CookieParameter(string name, DbType dbType, string cookieName)
			: base(name, dbType)
		{
			this.CookieName = cookieName;
		}

		/// <summary>Returns a duplicate of the current <see cref="T:System.Web.UI.WebControls.CookieParameter" /> instance.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.CookieParameter" /> that is an exact duplicate of the current one.</returns>
		// Token: 0x06001FB7 RID: 8119 RVA: 0x0004DE72 File Offset: 0x0004C072
		protected override Parameter Clone()
		{
			return new CookieParameter(this);
		}

		/// <summary>Updates and returns the value of the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> object.</summary>
		/// <returns>An object that represents the updated and current value of the parameter. If the context or the request is null, the <see cref="M:System.Web.UI.WebControls.CookieParameter.Evaluate(System.Web.HttpContext,System.Web.UI.Control)" /> method returns null.</returns>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> of the request.</param>
		/// <param name="control">A <see cref="T:System.Web.UI.Control" /> that is associated with the Web Forms page where the <see cref="T:System.Web.UI.WebControls.CookieParameter" /> is used. </param>
		// Token: 0x06001FB8 RID: 8120 RVA: 0x0004DE7C File Offset: 0x0004C07C
		protected internal override object Evaluate(HttpContext context, Control control)
		{
			if (context == null || context.Request == null)
			{
				return null;
			}
			HttpCookie httpCookie = context.Request.Cookies[this.CookieName];
			if (httpCookie == null)
			{
				return null;
			}
			return httpCookie.Value;
		}

		/// <summary>Gets or sets the name of the HTTP cookie that the parameter binds to.</summary>
		/// <returns>A string that identifies the client-side HTTP cookie that the parameter binds to.</returns>
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06001FB9 RID: 8121 RVA: 0x0004DEB8 File Offset: 0x0004C0B8
		// (set) Token: 0x06001FBA RID: 8122 RVA: 0x0004DECF File Offset: 0x0004C0CF
		[DefaultValue("")]
		public string CookieName
		{
			get
			{
				return base.ViewState.GetString("CookieName", string.Empty);
			}
			set
			{
				if (this.CookieName != value)
				{
					base.ViewState["CookieName"] = value;
					base.OnParameterChanged();
				}
			}
		}

		/// <summary>Gets or sets a value that specifies whether the parameter's value is validated.</summary>
		/// <returns>true if the parameter's value is validated; otherwise, false.</returns>
		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06001FBB RID: 8123 RVA: 0x0004DEF8 File Offset: 0x0004C0F8
		// (set) Token: 0x06001FBC RID: 8124 RVA: 0x0000B3D8 File Offset: 0x000095D8
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
