using System;

namespace System.Web.UI
{
	/// <summary>Specifies a <see cref="T:System.Web.UI.ControlBuilder" /> class for building a custom control within the ASP.NET parser. This class cannot be inherited.</summary>
	// Token: 0x02000152 RID: 338
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ControlBuilderAttribute : Attribute
	{
		/// <summary>Specifies the control builder for a custom control.</summary>
		/// <param name="builderType">The control builder type </param>
		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002863E File Offset: 0x0002683E
		public ControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control associated with the attribute. This property is read-only.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the control associated with the attribute.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0002864D File Offset: 0x0002684D
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		/// <summary>Returns the hash code of the <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. </summary>
		/// <returns>A 32-bit signed integer representing the hash code; otherwise, 0, if the <see cref="P:System.Web.UI.ControlBuilderAttribute.BuilderType" /> is null.</returns>
		// Token: 0x06000EE6 RID: 3814 RVA: 0x00028655 File Offset: 0x00026855
		public override int GetHashCode()
		{
			if (!(this.BuilderType != null))
			{
				return 0;
			}
			return this.BuilderType.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Web.UI.ControlBuilderAttribute" /> is identical to the specified object. </summary>
		/// <returns>true if <paramref name="obj" /> is a <see cref="T:System.Web.UI.ControlBuilderAttribute" /> and is identical to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />; otherwise, false.</returns>
		/// <param name="obj">An object to compare to the current <see cref="T:System.Web.UI.ControlBuilderAttribute" />.</param>
		// Token: 0x06000EE7 RID: 3815 RVA: 0x00028672 File Offset: 0x00026872
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is ControlBuilderAttribute && ((ControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		/// <summary>Determines whether the current control builder is the default.</summary>
		/// <returns>true if the current control builder is the default.</returns>
		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002869D File Offset: 0x0002689D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ControlBuilderAttribute.Default);
		}

		/// <summary>Specifies the new <see cref="T:System.Web.UI.ControlBuilderAttribute" /> object. By default, the new object is set to null. This field is read-only.</summary>
		// Token: 0x04001226 RID: 4646
		public static readonly ControlBuilderAttribute Default = new ControlBuilderAttribute(null);

		// Token: 0x04001227 RID: 4647
		private Type builderType;
	}
}
