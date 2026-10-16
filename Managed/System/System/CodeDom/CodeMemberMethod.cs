using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a method of a type.</summary>
	// Token: 0x0200031B RID: 795
	[Serializable]
	public class CodeMemberMethod : CodeTypeMember
	{
		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Parameters" /> collection is accessed.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x0600192B RID: 6443 RVA: 0x0005FF3C File Offset: 0x0005E13C
		// (remove) Token: 0x0600192C RID: 6444 RVA: 0x0005FF74 File Offset: 0x0005E174
		public event EventHandler PopulateParameters;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.Statements" /> collection is accessed.</summary>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x0600192D RID: 6445 RVA: 0x0005FFAC File Offset: 0x0005E1AC
		// (remove) Token: 0x0600192E RID: 6446 RVA: 0x0005FFE4 File Offset: 0x0005E1E4
		public event EventHandler PopulateStatements;

		/// <summary>An event that will be raised the first time the <see cref="P:System.CodeDom.CodeMemberMethod.ImplementationTypes" /> collection is accessed.</summary>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x0600192F RID: 6447 RVA: 0x0006001C File Offset: 0x0005E21C
		// (remove) Token: 0x06001930 RID: 6448 RVA: 0x00060054 File Offset: 0x0005E254
		public event EventHandler PopulateImplementationTypes;

		/// <summary>Gets or sets the data type of the return value of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the value returned by the method.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001931 RID: 6449 RVA: 0x0006008C File Offset: 0x0005E28C
		// (set) Token: 0x06001932 RID: 6450 RVA: 0x000600C0 File Offset: 0x0005E2C0
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(typeof(void).FullName));
				}
				return codeTypeReference;
			}
			set
			{
				this._returnType = value;
			}
		}

		/// <summary>Gets the statements within the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that indicates the statements within the method.</returns>
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001933 RID: 6451 RVA: 0x000600C9 File Offset: 0x0005E2C9
		public CodeStatementCollection Statements
		{
			get
			{
				if ((this._populated & 2) == 0)
				{
					this._populated |= 2;
					EventHandler populateStatements = this.PopulateStatements;
					if (populateStatements != null)
					{
						populateStatements(this, EventArgs.Empty);
					}
				}
				return this._statements;
			}
		}

		/// <summary>Gets the parameter declarations for the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the method parameters.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001934 RID: 6452 RVA: 0x00060100 File Offset: 0x0005E300
		public CodeParameterDeclarationExpressionCollection Parameters
		{
			get
			{
				if ((this._populated & 1) == 0)
				{
					this._populated |= 1;
					EventHandler populateParameters = this.PopulateParameters;
					if (populateParameters != null)
					{
						populateParameters(this, EventArgs.Empty);
					}
				}
				return this._parameters;
			}
		}

		/// <summary>Gets or sets the data type of the interface this method, if private, implements a method of, if any.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface with the method that the private method whose declaration is represented by this <see cref="T:System.CodeDom.CodeMemberMethod" /> implements.</returns>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00060137 File Offset: 0x0005E337
		// (set) Token: 0x06001936 RID: 6454 RVA: 0x0006013F File Offset: 0x0005E33F
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets the data types of the interfaces implemented by this method, unless it is a private method implementation, which is indicated by the <see cref="P:System.CodeDom.CodeMemberMethod.PrivateImplementationType" /> property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the interfaces implemented by this method.</returns>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06001937 RID: 6455 RVA: 0x00060148 File Offset: 0x0005E348
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				if (this._implementationTypes == null)
				{
					this._implementationTypes = new CodeTypeReferenceCollection();
				}
				if ((this._populated & 4) == 0)
				{
					this._populated |= 4;
					EventHandler populateImplementationTypes = this.PopulateImplementationTypes;
					if (populateImplementationTypes != null)
					{
						populateImplementationTypes(this, EventArgs.Empty);
					}
				}
				return this._implementationTypes;
			}
		}

		/// <summary>Gets the custom attributes of the return type of the method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes.</returns>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x000601A0 File Offset: 0x0005E3A0
		public CodeAttributeDeclarationCollection ReturnTypeCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._returnAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._returnAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets the type parameters for the current generic method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeParameterCollection" /> that contains the type parameters for the generic method.</returns>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x000601C8 File Offset: 0x0005E3C8
		public CodeTypeParameterCollection TypeParameters
		{
			get
			{
				CodeTypeParameterCollection codeTypeParameterCollection;
				if ((codeTypeParameterCollection = this._typeParameters) == null)
				{
					codeTypeParameterCollection = (this._typeParameters = new CodeTypeParameterCollection());
				}
				return codeTypeParameterCollection;
			}
		}

		// Token: 0x04000DBF RID: 3519
		private readonly CodeParameterDeclarationExpressionCollection _parameters = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04000DC0 RID: 3520
		private readonly CodeStatementCollection _statements = new CodeStatementCollection();

		// Token: 0x04000DC1 RID: 3521
		private CodeTypeReference _returnType;

		// Token: 0x04000DC2 RID: 3522
		private CodeTypeReferenceCollection _implementationTypes;

		// Token: 0x04000DC3 RID: 3523
		private CodeAttributeDeclarationCollection _returnAttributes;

		// Token: 0x04000DC4 RID: 3524
		private CodeTypeParameterCollection _typeParameters;

		// Token: 0x04000DC5 RID: 3525
		private int _populated;

		// Token: 0x04000DC6 RID: 3526
		private const int ParametersCollection = 1;

		// Token: 0x04000DC7 RID: 3527
		private const int StatementsCollection = 2;

		// Token: 0x04000DC8 RID: 3528
		private const int ImplTypesCollection = 4;
	}
}
