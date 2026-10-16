using System;

namespace System.CodeDom
{
	/// <summary>Represents a type parameter of a generic type or method.</summary>
	// Token: 0x0200033C RID: 828
	[Serializable]
	public class CodeTypeParameter : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class. </summary>
		// Token: 0x06001A3A RID: 6714 RVA: 0x0005F669 File Offset: 0x0005D869
		public CodeTypeParameter()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeParameter" /> class with the specified type parameter name. </summary>
		/// <param name="name">The name of the type parameter.</param>
		// Token: 0x06001A3B RID: 6715 RVA: 0x00061593 File Offset: 0x0005F793
		public CodeTypeParameter(string name)
		{
			this._name = name;
		}

		/// <summary>Gets or sets the name of the type parameter.</summary>
		/// <returns>The name of the type parameter. The default is an empty string ("").</returns>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000615A2 File Offset: 0x0005F7A2
		// (set) Token: 0x06001A3D RID: 6717 RVA: 0x000615B3 File Offset: 0x0005F7B3
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets the constraints for the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> object that contains the constraints for the type parameter.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000615BC File Offset: 0x0005F7BC
		public CodeTypeReferenceCollection Constraints
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._constraints) == null)
				{
					codeTypeReferenceCollection = (this._constraints = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets the custom attributes of the type parameter.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the type parameter. The default is null.</returns>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000615E4 File Offset: 0x0005F7E4
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._customAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets or sets a value indicating whether the type parameter has a constructor constraint.</summary>
		/// <returns>true if the type parameter has a constructor constraint; otherwise, false. The default is false.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x00061609 File Offset: 0x0005F809
		// (set) Token: 0x06001A41 RID: 6721 RVA: 0x00061611 File Offset: 0x0005F811
		public bool HasConstructorConstraint { get; set; }

		// Token: 0x04000E1A RID: 3610
		private string _name;

		// Token: 0x04000E1B RID: 3611
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000E1C RID: 3612
		private CodeTypeReferenceCollection _constraints;
	}
}
