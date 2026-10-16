using System;

namespace System.Web.UI
{
	/// <summary>Defines a metadata attribute that is used to specify the number of allowed instances of a template. This class cannot be inherited.</summary>
	// Token: 0x02000190 RID: 400
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TemplateInstanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class with the specified <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</summary>
		/// <param name="instances">A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value.</param>
		// Token: 0x06000F84 RID: 3972 RVA: 0x00028C6F File Offset: 0x00026E6F
		public TemplateInstanceAttribute(TemplateInstance instances)
		{
			this._instances = instances;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</summary>
		/// <returns>A <see cref="T:System.Web.UI.TemplateInstance" /> enumeration value that the current template instance represents.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00028C7E File Offset: 0x00026E7E
		public TemplateInstance Instances
		{
			get
			{
				return this._instances;
			}
		}

		/// <summary>Indicates whether the specified object is a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>true if value is both a <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object and is identical to the this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object; otherwise false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to test.</param>
		// Token: 0x06000F86 RID: 3974 RVA: 0x00028C88 File Offset: 0x00026E88
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			TemplateInstanceAttribute templateInstanceAttribute = obj as TemplateInstanceAttribute;
			return templateInstanceAttribute != null && templateInstanceAttribute.Instances == this.Instances;
		}

		/// <summary>Gets a hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>The hash code for this <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</returns>
		// Token: 0x06000F87 RID: 3975 RVA: 0x00028CB5 File Offset: 0x00026EB5
		public override int GetHashCode()
		{
			return this._instances.GetHashCode();
		}

		/// <summary>Returns a value indicating if the current <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object is the same as the default <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> object.</summary>
		/// <returns>true if the value of the current instance of <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> is the default; otherwise, false.</returns>
		// Token: 0x06000F88 RID: 3976 RVA: 0x00028CC8 File Offset: 0x00026EC8
		public override bool IsDefaultAttribute()
		{
			return this.Equals(TemplateInstanceAttribute.Default);
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated multiple times. This field is read-only.</summary>
		// Token: 0x04001326 RID: 4902
		public static readonly TemplateInstanceAttribute Multiple = new TemplateInstanceAttribute(TemplateInstance.Multiple);

		/// <summary>Creates an instance of the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class as one representing a template that will be instantiated a single time. This field is read-only.</summary>
		// Token: 0x04001327 RID: 4903
		public static readonly TemplateInstanceAttribute Single = new TemplateInstanceAttribute(TemplateInstance.Single);

		/// <summary>Defines the default value for the <see cref="T:System.Web.UI.TemplateInstanceAttribute" /> class. This field is read-only. </summary>
		// Token: 0x04001328 RID: 4904
		public static readonly TemplateInstanceAttribute Default = TemplateInstanceAttribute.Multiple;

		// Token: 0x04001329 RID: 4905
		private TemplateInstance _instances;
	}
}
