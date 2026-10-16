using System;

namespace System.ComponentModel
{
	/// <summary>Specifies what type to use as a converter for the object this attribute is bound to.</summary>
	// Token: 0x020003F5 RID: 1013
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class with the default type converter, which is an empty string ("").</summary>
		// Token: 0x06002103 RID: 8451 RVA: 0x00071B93 File Offset: 0x0006FD93
		public TypeConverterAttribute()
		{
			this.ConverterTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type as the data converter for the object this attribute is bound to.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the converter class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06002104 RID: 8452 RVA: 0x00071BA6 File Offset: 0x0006FDA6
		public TypeConverterAttribute(Type type)
		{
			this.ConverterTypeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeConverterAttribute" /> class, using the specified type name as the data converter for the object this attribute is bound to.</summary>
		/// <param name="typeName">The fully qualified name of the class to use for data conversion for the object this attribute is bound to. </param>
		// Token: 0x06002105 RID: 8453 RVA: 0x00071BBA File Offset: 0x0006FDBA
		public TypeConverterAttribute(string typeName)
		{
			this.ConverterTypeName = typeName;
		}

		/// <summary>Gets the fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to.</summary>
		/// <returns>The fully qualified type name of the <see cref="T:System.Type" /> to use as a converter for the object this attribute is bound to, or an empty string ("") if none exists. The default value is an empty string ("").</returns>
		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00071BC9 File Offset: 0x0006FDC9
		public string ConverterTypeName { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06002107 RID: 8455 RVA: 0x00071BD4 File Offset: 0x0006FDD4
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.ConverterTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.TypeConverterAttribute" />.</returns>
		// Token: 0x06002108 RID: 8456 RVA: 0x00071BFE File Offset: 0x0006FDFE
		public override int GetHashCode()
		{
			return this.ConverterTypeName.GetHashCode();
		}

		/// <summary>Specifies the type to use as a converter for the object this attribute is bound to. </summary>
		// Token: 0x04001013 RID: 4115
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}
