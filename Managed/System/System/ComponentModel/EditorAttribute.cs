using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the editor to use to change a property. This class cannot be inherited.</summary>
	// Token: 0x020003A8 RID: 936
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public sealed class EditorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the default editor, which is no editor.</summary>
		// Token: 0x06001E82 RID: 7810 RVA: 0x0006C185 File Offset: 0x0006A385
		public EditorAttribute()
		{
			this.EditorTypeName = string.Empty;
			this.EditorBaseTypeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and base type name of the editor.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseTypeName">The fully qualified type name of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06001E83 RID: 7811 RVA: 0x0006C1A3 File Offset: 0x0006A3A3
		public EditorAttribute(string typeName, string baseTypeName)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type name and the base type.</summary>
		/// <param name="typeName">The fully qualified type name of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06001E84 RID: 7812 RVA: 0x0006C1C5 File Offset: 0x0006A3C5
		public EditorAttribute(string typeName, Type baseType)
		{
			typeName.ToUpper(CultureInfo.InvariantCulture);
			this.EditorTypeName = typeName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorAttribute" /> class with the type and the base type.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the editor. </param>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the base class or interface to use as a lookup key for the editor. This class must be or derive from <see cref="T:System.Drawing.Design.UITypeEditor" />. </param>
		// Token: 0x06001E85 RID: 7813 RVA: 0x0006C1EC File Offset: 0x0006A3EC
		public EditorAttribute(Type type, Type baseType)
		{
			this.EditorTypeName = type.AssemblyQualifiedName;
			this.EditorBaseTypeName = baseType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of the base class or interface serving as a lookup key for this editor.</summary>
		/// <returns>The name of the base class or interface serving as a lookup key for this editor.</returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001E86 RID: 7814 RVA: 0x0006C20C File Offset: 0x0006A40C
		public string EditorBaseTypeName { get; }

		/// <summary>Gets the name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</summary>
		/// <returns>The name of the editor class in the <see cref="P:System.Type.AssemblyQualifiedName" /> format.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x0006C214 File Offset: 0x0006A414
		public string EditorTypeName { get; }

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001E88 RID: 7816 RVA: 0x0006C21C File Offset: 0x0006A41C
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.EditorBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001E89 RID: 7817 RVA: 0x0006C26C File Offset: 0x0006A46C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorAttribute editorAttribute = obj as EditorAttribute;
			return editorAttribute != null && editorAttribute.EditorTypeName == this.EditorTypeName && editorAttribute.EditorBaseTypeName == this.EditorBaseTypeName;
		}

		// Token: 0x06001E8A RID: 7818 RVA: 0x000678DE File Offset: 0x00065ADE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000F58 RID: 3928
		private string _typeId;
	}
}
