using System;

namespace System.ComponentModel
{
	/// <summary>Provides metadata for a property representing a data field. This class cannot be inherited.</summary>
	// Token: 0x02000398 RID: 920
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DataObjectFieldAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		// Token: 0x06001E1C RID: 7708 RVA: 0x0006B55E File Offset: 0x0006975E
		public DataObjectFieldAttribute(bool primaryKey)
			: this(primaryKey, false, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, and whether the field is a database identity field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		// Token: 0x06001E1D RID: 7709 RVA: 0x0006B56A File Offset: 0x0006976A
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity)
			: this(primaryKey, isIdentity, false, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether the field is a database identity field, and whether the field can be null.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		// Token: 0x06001E1E RID: 7710 RVA: 0x0006B576 File Offset: 0x00069776
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable)
			: this(primaryKey, isIdentity, isNullable, -1)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataObjectFieldAttribute" /> class and indicates whether the field is the primary key for the data row, whether it is a database identity field, and whether it can be null and sets the length of the field.</summary>
		/// <param name="primaryKey">true to indicate that the field is in the primary key of the data row; otherwise, false.</param>
		/// <param name="isIdentity">true to indicate that the field is an identity field that uniquely identifies the data row; otherwise, false.</param>
		/// <param name="isNullable">true to indicate that the field can be null in the data store; otherwise, false.</param>
		/// <param name="length">The length of the field in bytes.</param>
		// Token: 0x06001E1F RID: 7711 RVA: 0x0006B582 File Offset: 0x00069782
		public DataObjectFieldAttribute(bool primaryKey, bool isIdentity, bool isNullable, int length)
		{
			this.PrimaryKey = primaryKey;
			this.IsIdentity = isIdentity;
			this.IsNullable = isNullable;
			this.Length = length;
		}

		/// <summary>Gets a value indicating whether a property represents an identity field in the underlying data.</summary>
		/// <returns>true if the property represents an identity field in the underlying data; otherwise, false. The default value is false.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0006B5A7 File Offset: 0x000697A7
		public bool IsIdentity { get; }

		/// <summary>Gets a value indicating whether a property represents a field that can be null in the underlying data store.</summary>
		/// <returns>true if the property represents a field that can be null in the underlying data store; otherwise, false.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0006B5AF File Offset: 0x000697AF
		public bool IsNullable { get; }

		/// <summary>Gets the length of the property in bytes.</summary>
		/// <returns>The length of the property in bytes, or -1 if not set.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x0006B5B7 File Offset: 0x000697B7
		public int Length { get; }

		/// <summary>Gets a value indicating whether a property is in the primary key in the underlying data.</summary>
		/// <returns>true if the property is in the primary key of the data store; otherwise, false.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x0006B5BF File Offset: 0x000697BF
		public bool PrimaryKey { get; }

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if this instance is the same as the instance specified by the <paramref name="obj" /> parameter; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance of <see cref="T:System.ComponentModel.DataObjectFieldAttribute" />.</param>
		// Token: 0x06001E24 RID: 7716 RVA: 0x0006B5C8 File Offset: 0x000697C8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataObjectFieldAttribute dataObjectFieldAttribute = obj as DataObjectFieldAttribute;
			return dataObjectFieldAttribute != null && dataObjectFieldAttribute.IsIdentity == this.IsIdentity && dataObjectFieldAttribute.IsNullable == this.IsNullable && dataObjectFieldAttribute.Length == this.Length && dataObjectFieldAttribute.PrimaryKey == this.PrimaryKey;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001E25 RID: 7717 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
