using System;

namespace System.CodeDom
{
	/// <summary>Represents an attribute declaration.</summary>
	// Token: 0x020002F9 RID: 761
	[Serializable]
	public class CodeAttributeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class.</summary>
		// Token: 0x0600184A RID: 6218 RVA: 0x0005F250 File Offset: 0x0005D450
		public CodeAttributeDeclaration()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name.</summary>
		/// <param name="name">The name of the attribute. </param>
		// Token: 0x0600184B RID: 6219 RVA: 0x0005F263 File Offset: 0x0005D463
		public CodeAttributeDeclaration(string name)
		{
			this.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified name and arguments.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" />  that contains the arguments for the attribute. </param>
		// Token: 0x0600184C RID: 6220 RVA: 0x0005F27D File Offset: 0x0005D47D
		public CodeAttributeDeclaration(string name, params CodeAttributeArgument[] arguments)
		{
			this.Name = name;
			this.Arguments.AddRange(arguments);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		// Token: 0x0600184D RID: 6221 RVA: 0x0005F2A3 File Offset: 0x0005D4A3
		public CodeAttributeDeclaration(CodeTypeReference attributeType)
			: this(attributeType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeDeclaration" /> class using the specified code type reference and arguments.</summary>
		/// <param name="attributeType">The <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the attribute.</param>
		/// <param name="arguments">An array of type <see cref="T:System.CodeDom.CodeAttributeArgument" /> that contains the arguments for the attribute.</param>
		// Token: 0x0600184E RID: 6222 RVA: 0x0005F2AD File Offset: 0x0005D4AD
		public CodeAttributeDeclaration(CodeTypeReference attributeType, params CodeAttributeArgument[] arguments)
		{
			this._attributeType = attributeType;
			if (attributeType != null)
			{
				this._name = attributeType.BaseType;
			}
			if (arguments != null)
			{
				this.Arguments.AddRange(arguments);
			}
		}

		/// <summary>Gets or sets the name of the attribute being declared.</summary>
		/// <returns>The name of the attribute.</returns>
		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600184F RID: 6223 RVA: 0x0005F2E5 File Offset: 0x0005D4E5
		// (set) Token: 0x06001850 RID: 6224 RVA: 0x0005F2F6 File Offset: 0x0005D4F6
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
				this._attributeType = new CodeTypeReference(this._name);
			}
		}

		/// <summary>Gets the arguments for the attribute.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeArgumentCollection" /> that contains the arguments for the attribute.</returns>
		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001851 RID: 6225 RVA: 0x0005F310 File Offset: 0x0005D510
		public CodeAttributeArgumentCollection Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		/// <summary>Gets the code type reference for the code attribute declaration.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that identifies the <see cref="T:System.CodeDom.CodeAttributeDeclaration" />.</returns>
		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001852 RID: 6226 RVA: 0x0005F318 File Offset: 0x0005D518
		public CodeTypeReference AttributeType
		{
			get
			{
				return this._attributeType;
			}
		}

		// Token: 0x04000D75 RID: 3445
		private string _name;

		// Token: 0x04000D76 RID: 3446
		private readonly CodeAttributeArgumentCollection _arguments = new CodeAttributeArgumentCollection();

		// Token: 0x04000D77 RID: 3447
		private CodeTypeReference _attributeType;
	}
}
