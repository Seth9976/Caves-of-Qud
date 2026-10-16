using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Defines a metadata attribute that you can use when developing ASP.NET server controls. Use the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class to indicate how the page parser should treat content nested inside a server control tag declared on a page. This class cannot be inherited.</summary>
	// Token: 0x02000213 RID: 531
	[AttributeUsage(AttributeTargets.Class)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class ParseChildrenAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class.</summary>
		// Token: 0x060015C6 RID: 5574 RVA: 0x00038B38 File Offset: 0x00036D38
		public ParseChildrenAttribute()
		{
			this.childrenAsProperties = false;
			this.defaultProperty = "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildrenAsProperties" /> property to determine if the elements that are contained within a server control are parsed as properties of the server control.</summary>
		/// <param name="childrenAsProperties">true to parse the elements as properties of the server control; otherwise, false. </param>
		// Token: 0x060015C7 RID: 5575 RVA: 0x00038B62 File Offset: 0x00036D62
		public ParseChildrenAttribute(bool childrenAsProperties)
		{
			this.childrenAsProperties = childrenAsProperties;
			this.defaultProperty = "";
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <paramref name="childrenAsProperties" /> and <paramref name="defaultProperty" /> parameters.</summary>
		/// <param name="childrenAsProperties">true to parse the elements as properties of the server control; otherwise, false. </param>
		/// <param name="defaultProperty">A string that defines a collection property of the server control into which nested content is parsed by default. </param>
		// Token: 0x060015C8 RID: 5576 RVA: 0x00038B8C File Offset: 0x00036D8C
		public ParseChildrenAttribute(bool childrenAsProperties, string defaultProperty)
		{
			this.childrenAsProperties = childrenAsProperties;
			if (childrenAsProperties)
			{
				this.defaultProperty = defaultProperty;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class using the <see cref="P:System.Web.UI.ParseChildrenAttribute.ChildControlType" /> property to determine which elements that are contained within a server control are parsed as controls.</summary>
		/// <param name="childControlType">The control type to parse as a property. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="childControlType" /> is null. </exception>
		// Token: 0x060015C9 RID: 5577 RVA: 0x00038BB5 File Offset: 0x00036DB5
		public ParseChildrenAttribute(Type childControlType)
		{
			this.childType = childControlType;
			this.defaultProperty = "";
		}

		/// <summary>Gets or sets a value indicating whether to parse the elements that are contained within a server control as properties.</summary>
		/// <returns>true to parse the elements as properties; otherwise, false. The default is true.</returns>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to false.</exception>
		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x00038BDF File Offset: 0x00036DDF
		// (set) Token: 0x060015CB RID: 5579 RVA: 0x00038BE7 File Offset: 0x00036DE7
		public bool ChildrenAsProperties
		{
			get
			{
				return this.childrenAsProperties;
			}
			set
			{
				this.childrenAsProperties = value;
			}
		}

		/// <summary>Gets or sets the default property for the server control into which the elements are parsed.</summary>
		/// <returns>The name of the default collection property of the server control into which the elements are parsed.</returns>
		/// <exception cref="T:System.NotSupportedException">The current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> was invoked with <paramref name="childrenAsProperties" /> set to false.</exception>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060015CC RID: 5580 RVA: 0x00038BF0 File Offset: 0x00036DF0
		// (set) Token: 0x060015CD RID: 5581 RVA: 0x00038BF8 File Offset: 0x00036DF8
		public string DefaultProperty
		{
			get
			{
				return this.defaultProperty;
			}
			set
			{
				this.defaultProperty = value;
			}
		}

		/// <summary>Gets a value indicating the allowed type of a control. </summary>
		/// <returns>The control type. The default is <see cref="T:System.Web.UI.Control" />. </returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060015CE RID: 5582 RVA: 0x00038C01 File Offset: 0x00036E01
		public Type ChildControlType
		{
			get
			{
				return this.childType;
			}
		}

		/// <summary>Determines whether the specified object is equal to the current object.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current object.</param>
		// Token: 0x060015CF RID: 5583 RVA: 0x00038C0C File Offset: 0x00036E0C
		public override bool Equals(object obj)
		{
			ParseChildrenAttribute parseChildrenAttribute = obj as ParseChildrenAttribute;
			return parseChildrenAttribute != null && this.childrenAsProperties == parseChildrenAttribute.childrenAsProperties && (!this.childrenAsProperties || this.defaultProperty == parseChildrenAttribute.DefaultProperty);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> object. </returns>
		// Token: 0x060015D0 RID: 5584 RVA: 0x0002F229 File Offset: 0x0002D429
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns a value indicating whether the value of the current instance of the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class is the default value of the derived class.</summary>
		/// <returns>true if the current <see cref="T:System.Web.UI.ParseChildrenAttribute" /> value is the default instance; otherwise, false.</returns>
		// Token: 0x060015D1 RID: 5585 RVA: 0x00038C50 File Offset: 0x00036E50
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ParseChildrenAttribute.Default);
		}

		// Token: 0x04001537 RID: 5431
		private bool childrenAsProperties;

		// Token: 0x04001538 RID: 5432
		private string defaultProperty;

		/// <summary>Defines the default value for the <see cref="T:System.Web.UI.ParseChildrenAttribute" /> class. This field is read-only.</summary>
		// Token: 0x04001539 RID: 5433
		public static readonly ParseChildrenAttribute Default = new ParseChildrenAttribute();

		/// <summary>Indicates that the nested content that is contained within the server control is parsed as controls.</summary>
		// Token: 0x0400153A RID: 5434
		public static readonly ParseChildrenAttribute ParseAsChildren = new ParseChildrenAttribute(false);

		/// <summary>Indicates that the nested content that is contained within a server control is parsed as properties of the control. </summary>
		// Token: 0x0400153B RID: 5435
		public static readonly ParseChildrenAttribute ParseAsProperties = new ParseChildrenAttribute(true);

		// Token: 0x0400153C RID: 5436
		private Type childType = typeof(Control);
	}
}
