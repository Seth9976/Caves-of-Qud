using System;

namespace System.Web.UI
{
	/// <summary>Allows a <see cref="T:System.Web.UI.TemplateControl" />-derived class to specify the control builder used at the top level of the builder tree when parsing the file. This class cannot be inherited.</summary>
	// Token: 0x0200015C RID: 348
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FileLevelControlBuilderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> class.</summary>
		/// <param name="builderType">The <see cref="T:System.Type" /> of the control builder used when parsing the file.</param>
		// Token: 0x06000F03 RID: 3843 RVA: 0x00028820 File Offset: 0x00026A20
		public FileLevelControlBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the control builder used when parsing the file. This property is read-only. </summary>
		/// <returns>The <see cref="T:System.Type" /> of the control builder used when parsing the file.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0002882F File Offset: 0x00026A2F
		public Type BuilderType
		{
			get
			{
				return this.builderType;
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x00028837 File Offset: 0x00026A37
		public override int GetHashCode()
		{
			return this.builderType.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance. </param>
		// Token: 0x06000F06 RID: 3846 RVA: 0x00028844 File Offset: 0x00026A44
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is FileLevelControlBuilderAttribute && ((FileLevelControlBuilderAttribute)obj).BuilderType == this.builderType);
		}

		/// <summary>Determines whether the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object is the default.</summary>
		/// <returns>true if the current <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> is the default; otherwise, false.</returns>
		// Token: 0x06000F07 RID: 3847 RVA: 0x0002886F File Offset: 0x00026A6F
		public override bool IsDefaultAttribute()
		{
			return this.Equals(FileLevelControlBuilderAttribute.Default);
		}

		/// <summary>Specifies the new <see cref="T:System.Web.UI.FileLevelControlBuilderAttribute" /> object. By default, the new object is set to null. This field is read-only.</summary>
		// Token: 0x0400123C RID: 4668
		public static readonly FileLevelControlBuilderAttribute Default = new FileLevelControlBuilderAttribute(null);

		// Token: 0x0400123D RID: 4669
		private Type builderType;
	}
}
