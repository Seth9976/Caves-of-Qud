using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x020003D6 RID: 982
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x06001FA0 RID: 8096 RVA: 0x0006D6AB File Offset: 0x0006B8AB
		public LookupBindingPropertiesAttribute()
		{
			this.DataSource = null;
			this.DisplayMember = null;
			this.ValueMember = null;
			this.LookupMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x06001FA1 RID: 8097 RVA: 0x0006D6CF File Offset: 0x0006B8CF
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.DataSource = dataSource;
			this.DisplayMember = displayMember;
			this.ValueMember = valueMember;
			this.LookupMember = lookupMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x0006D6F4 File Offset: 0x0006B8F4
		public string DataSource { get; }

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0006D6FC File Offset: 0x0006B8FC
		public string DisplayMember { get; }

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001FA4 RID: 8100 RVA: 0x0006D704 File Offset: 0x0006B904
		public string ValueMember { get; }

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0006D70C File Offset: 0x0006B90C
		public string LookupMember { get; }

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance </param>
		// Token: 0x06001FA6 RID: 8102 RVA: 0x0006D714 File Offset: 0x0006B914
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.DataSource && lookupBindingPropertiesAttribute.DisplayMember == this.DisplayMember && lookupBindingPropertiesAttribute.ValueMember == this.ValueMember && lookupBindingPropertiesAttribute.LookupMember == this.LookupMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x06001FA7 RID: 8103 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000F97 RID: 3991
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
