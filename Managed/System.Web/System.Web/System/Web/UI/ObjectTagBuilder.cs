using System;
using System.Collections;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI
{
	/// <summary>Used by the ASP.NET <see cref="T:System.Web.UI.TemplateParser" /> class to parse server-side &lt;object&gt; tags. This class can not be inherited.</summary>
	// Token: 0x02000204 RID: 516
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ObjectTagBuilder : ControlBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectTagBuilder" /> class. </summary>
		// Token: 0x06001426 RID: 5158 RVA: 0x000342E5 File Offset: 0x000324E5
		public ObjectTagBuilder()
		{
			base.SetTagName("object");
		}

		/// <summary>Adds content, such as text or HTML, to a control.</summary>
		/// <param name="s">The content to add to the control.</param>
		// Token: 0x06001427 RID: 5159 RVA: 0x00003946 File Offset: 0x00001B46
		public override void AppendLiteralString(string s)
		{
		}

		/// <summary>Adds builders to the <see cref="T:System.Web.UI.ObjectTagBuilder" /> object for any child controls that belong to the container control.</summary>
		/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> assigned to the child control.</param>
		// Token: 0x06001428 RID: 5160 RVA: 0x00003946 File Offset: 0x00001B46
		public override void AppendSubBuilder(ControlBuilder subBuilder)
		{
		}

		/// <summary>Initializes the object tag builder when the page is parsed.</summary>
		/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control.</param>
		/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> responsible for building the control.</param>
		/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create.</param>
		/// <param name="tagName">The name of the tag to be built. This allows the builder to support multiple tag types.</param>
		/// <param name="id">The <see cref="P:System.Web.UI.Control.ID" /> assigned to the control.</param>
		/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> that holds all the specified tag attributes.</param>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="id" /> parameter is null.- or -The object tag scope is invalid.- or -The classid or progid attributes are not included or are invalid.</exception>
		// Token: 0x06001429 RID: 5161 RVA: 0x000342F8 File Offset: 0x000324F8
		public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
		{
			if (id == null && attribs == null)
			{
				throw new HttpException("Missing 'id'.");
			}
			if (attribs == null)
			{
				throw new ParseException(parser.Location, "Error in ObjectTag.");
			}
			attribs.Remove("runat");
			this.id = attribs["id"] as string;
			attribs.Remove("id");
			if (this.id == null || this.id.Trim() == "")
			{
				throw new ParseException(parser.Location, "Object tag must have a valid ID.");
			}
			this.scope = attribs["scope"] as string;
			string text = attribs["class"] as string;
			attribs.Remove("scope");
			attribs.Remove("class");
			if (text == null || text.Trim() == "")
			{
				throw new ParseException(parser.Location, "Object tag must have 'class' attribute.");
			}
			this.type = parser.LoadType(text);
			if (this.type == null)
			{
				throw new ParseException(parser.Location, "Type " + text + " not found.");
			}
			if (attribs["progid"] != null || attribs["classid"] != null)
			{
				throw new ParseException(parser.Location, "ClassID and ProgID are not supported.");
			}
			if (attribs.Count > 0)
			{
				throw new ParseException(parser.Location, "Unknown attribute");
			}
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x0600142A RID: 5162 RVA: 0x00034472 File Offset: 0x00032672
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x0600142B RID: 5163 RVA: 0x0003447A File Offset: 0x0003267A
		internal string ObjectID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x0600142C RID: 5164 RVA: 0x00034482 File Offset: 0x00032682
		internal string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x0400148D RID: 5261
		private string id;

		// Token: 0x0400148E RID: 5262
		private string scope;

		// Token: 0x0400148F RID: 5263
		private Type type;
	}
}
