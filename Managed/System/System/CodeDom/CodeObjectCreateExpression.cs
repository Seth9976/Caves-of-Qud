using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a new instance of a type.</summary>
	// Token: 0x02000323 RID: 803
	[Serializable]
	public class CodeObjectCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class.</summary>
		// Token: 0x0600198B RID: 6539 RVA: 0x00060934 File Offset: 0x0005EB34
		public CodeObjectCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x0600198C RID: 6540 RVA: 0x00060947 File Offset: 0x0005EB47
		public CodeObjectCreateExpression(CodeTypeReference createType, params CodeExpression[] parameters)
		{
			this.CreateType = createType;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The name of the data type of object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x0600198D RID: 6541 RVA: 0x0006096D File Offset: 0x0005EB6D
		public CodeObjectCreateExpression(string createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeObjectCreateExpression" /> class using the specified type and parameters.</summary>
		/// <param name="createType">The data type of the object to create. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicates the parameters to use to create the object. </param>
		// Token: 0x0600198E RID: 6542 RVA: 0x00060998 File Offset: 0x0005EB98
		public CodeObjectCreateExpression(Type createType, params CodeExpression[] parameters)
		{
			this.CreateType = new CodeTypeReference(createType);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the data type of the object to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> to the data type of the object to create.</returns>
		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x0600198F RID: 6543 RVA: 0x000609C4 File Offset: 0x0005EBC4
		// (set) Token: 0x06001990 RID: 6544 RVA: 0x000609EE File Offset: 0x0005EBEE
		public CodeTypeReference CreateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._createType) == null)
				{
					codeTypeReference = (this._createType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._createType = value;
			}
		}

		/// <summary>Gets or sets the parameters to use in creating the object.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to use when creating the object.</returns>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001991 RID: 6545 RVA: 0x000609F7 File Offset: 0x0005EBF7
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x04000DE7 RID: 3559
		private CodeTypeReference _createType;
	}
}
