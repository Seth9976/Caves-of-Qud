using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a property of a type.</summary>
	// Token: 0x0200031C RID: 796
	[Serializable]
	public class CodeMemberProperty : CodeTypeMember
	{
		/// <summary>Gets or sets the data type of the interface, if any, this property, if private, implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the interface, if any, the property, if private, implements.</returns>
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x0600193B RID: 6459 RVA: 0x0006020B File Offset: 0x0005E40B
		// (set) Token: 0x0600193C RID: 6460 RVA: 0x00060213 File Offset: 0x0005E413
		public CodeTypeReference PrivateImplementationType { get; set; }

		/// <summary>Gets the data types of any interfaces that the property implements.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> that indicates the data types the property implements.</returns>
		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x0600193D RID: 6461 RVA: 0x0006021C File Offset: 0x0005E41C
		public CodeTypeReferenceCollection ImplementationTypes
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._implementationTypes) == null)
				{
					codeTypeReferenceCollection = (this._implementationTypes = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		/// <summary>Gets or sets the data type of the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the property.</returns>
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x00060244 File Offset: 0x0005E444
		// (set) Token: 0x0600193F RID: 6463 RVA: 0x0006026E File Offset: 0x0005E46E
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

		/// <summary>Gets or sets a value indicating whether the property has a get method accessor.</summary>
		/// <returns>true if the Count property of the <see cref="P:System.CodeDom.CodeMemberProperty.GetStatements" /> collection is non-zero, or if the value of this property has been set to true; otherwise, false.</returns>
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00060277 File Offset: 0x0005E477
		// (set) Token: 0x06001941 RID: 6465 RVA: 0x00060291 File Offset: 0x0005E491
		public bool HasGet
		{
			get
			{
				return this._hasGet || this.GetStatements.Count > 0;
			}
			set
			{
				this._hasGet = value;
				if (!value)
				{
					this.GetStatements.Clear();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the property has a set method accessor.</summary>
		/// <returns>true if the <see cref="P:System.Collections.CollectionBase.Count" /> property of the <see cref="P:System.CodeDom.CodeMemberProperty.SetStatements" /> collection is non-zero; otherwise, false.</returns>
		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x000602A8 File Offset: 0x0005E4A8
		// (set) Token: 0x06001943 RID: 6467 RVA: 0x000602C2 File Offset: 0x0005E4C2
		public bool HasSet
		{
			get
			{
				return this._hasSet || this.SetStatements.Count > 0;
			}
			set
			{
				this._hasSet = value;
				if (!value)
				{
					this.SetStatements.Clear();
				}
			}
		}

		/// <summary>Gets the collection of get statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the get statements for the member property.</returns>
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x000602D9 File Offset: 0x0005E4D9
		public CodeStatementCollection GetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of set statements for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeStatementCollection" /> that contains the set statements for the member property.</returns>
		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x000602E1 File Offset: 0x0005E4E1
		public CodeStatementCollection SetStatements { get; } = new CodeStatementCollection();

		/// <summary>Gets the collection of declaration expressions for the property.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the declaration expressions for the property.</returns>
		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x000602E9 File Offset: 0x0005E4E9
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04000DCD RID: 3533
		private CodeTypeReference _type;

		// Token: 0x04000DCE RID: 3534
		private bool _hasGet;

		// Token: 0x04000DCF RID: 3535
		private bool _hasSet;

		// Token: 0x04000DD0 RID: 3536
		private CodeTypeReferenceCollection _implementationTypes;
	}
}
