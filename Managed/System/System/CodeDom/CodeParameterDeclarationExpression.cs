using System;

namespace System.CodeDom
{
	/// <summary>Represents a parameter declaration for a method, property, or constructor.</summary>
	// Token: 0x02000324 RID: 804
	[Serializable]
	public class CodeParameterDeclarationExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class.</summary>
		// Token: 0x06001992 RID: 6546 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeParameterDeclarationExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">An object that indicates the type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06001993 RID: 6547 RVA: 0x000609FF File Offset: 0x0005EBFF
		public CodeParameterDeclarationExpression(CodeTypeReference type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06001994 RID: 6548 RVA: 0x00060A15 File Offset: 0x0005EC15
		public CodeParameterDeclarationExpression(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeParameterDeclarationExpression" /> class using the specified parameter type and name.</summary>
		/// <param name="type">The type of the parameter to declare. </param>
		/// <param name="name">The name of the parameter to declare. </param>
		// Token: 0x06001995 RID: 6549 RVA: 0x00060A30 File Offset: 0x0005EC30
		public CodeParameterDeclarationExpression(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			this.Name = name;
		}

		/// <summary>Gets or sets the custom attributes for the parameter declaration.</summary>
		/// <returns>An object that indicates the custom attributes.</returns>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001996 RID: 6550 RVA: 0x00060A4C File Offset: 0x0005EC4C
		// (set) Token: 0x06001997 RID: 6551 RVA: 0x00060A71 File Offset: 0x0005EC71
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
			set
			{
				this._customAttributes = value;
			}
		}

		/// <summary>Gets or sets the direction of the field.</summary>
		/// <returns>An object that indicates the direction of the field.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001998 RID: 6552 RVA: 0x00060A7A File Offset: 0x0005EC7A
		// (set) Token: 0x06001999 RID: 6553 RVA: 0x00060A82 File Offset: 0x0005EC82
		public FieldDirection Direction { get; set; }

		/// <summary>Gets or sets the type of the parameter.</summary>
		/// <returns>The type of the parameter.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600199A RID: 6554 RVA: 0x00060A8C File Offset: 0x0005EC8C
		// (set) Token: 0x0600199B RID: 6555 RVA: 0x00060AB6 File Offset: 0x0005ECB6
		public CodeTypeReference Type
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._type) == null)
				{
					codeTypeReference = (this._type = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._type = value;
			}
		}

		/// <summary>Gets or sets the name of the parameter.</summary>
		/// <returns>The name of the parameter.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x00060ABF File Offset: 0x0005ECBF
		// (set) Token: 0x0600199D RID: 6557 RVA: 0x00060AD0 File Offset: 0x0005ECD0
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

		// Token: 0x04000DE9 RID: 3561
		private CodeTypeReference _type;

		// Token: 0x04000DEA RID: 3562
		private string _name;

		// Token: 0x04000DEB RID: 3563
		private CodeAttributeDeclarationCollection _customAttributes;
	}
}
