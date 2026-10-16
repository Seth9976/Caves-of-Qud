using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the data source and data member properties for a component that supports complex data binding. This class cannot be inherited.</summary>
	// Token: 0x02000390 RID: 912
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x06001DE8 RID: 7656 RVA: 0x00003D9F File Offset: 0x00001F9F
		public ComplexBindingPropertiesAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		// Token: 0x06001DE9 RID: 7657 RVA: 0x00069A36 File Offset: 0x00067C36
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.DataSource = dataSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source and data member. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="dataMember">The name of the property to be used as the source for data.</param>
		// Token: 0x06001DEA RID: 7658 RVA: 0x00069A45 File Offset: 0x00067C45
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.DataSource = dataSource;
			this.DataMember = dataMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data source property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x00069A5B File Offset: 0x00067C5B
		public string DataSource { get; }

		/// <summary>Gets the name of the data member property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data member property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00069A63 File Offset: 0x00067C63
		public string DataMember { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance </param>
		// Token: 0x06001DED RID: 7661 RVA: 0x00069A6C File Offset: 0x00067C6C
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataSource == this.DataSource && complexBindingPropertiesAttribute.DataMember == this.DataMember;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001DEE RID: 7662 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000F20 RID: 3872
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}
