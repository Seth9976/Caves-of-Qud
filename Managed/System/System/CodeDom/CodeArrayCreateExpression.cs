using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates an array.</summary>
	// Token: 0x020002F3 RID: 755
	[Serializable]
	public class CodeArrayCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class.</summary>
		// Token: 0x06001813 RID: 6163 RVA: 0x0005EE9D File Offset: 0x0005D09D
		public CodeArrayCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initialization expressions.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06001814 RID: 6164 RVA: 0x0005EEB0 File Offset: 0x0005D0B0
		public CodeArrayCreateExpression(CodeTypeReference createType, params CodeExpression[] initializers)
		{
			this._createType = createType;
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and initializers.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06001815 RID: 6165 RVA: 0x0005EED6 File Offset: 0x0005D0D6
		public CodeArrayCreateExpression(string createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and initializers.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="initializers">An array of expressions to use to initialize the array. </param>
		// Token: 0x06001816 RID: 6166 RVA: 0x0005EF01 File Offset: 0x0005D101
		public CodeArrayCreateExpression(Type createType, params CodeExpression[] initializers)
		{
			this._createType = new CodeTypeReference(createType);
			this._initializers.AddRange(initializers);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06001817 RID: 6167 RVA: 0x0005EF2C File Offset: 0x0005D12C
		public CodeArrayCreateExpression(CodeTypeReference createType, int size)
		{
			this._createType = createType;
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06001818 RID: 6168 RVA: 0x0005EF4D File Offset: 0x0005D14D
		public CodeArrayCreateExpression(string createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">The number of indexes of the array to create. </param>
		// Token: 0x06001819 RID: 6169 RVA: 0x0005EF73 File Offset: 0x0005D173
		public CodeArrayCreateExpression(Type createType, int size)
		{
			this._createType = new CodeTypeReference(createType);
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">A <see cref="T:System.CodeDom.CodeTypeReference" /> indicating the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x0600181A RID: 6170 RVA: 0x0005EF99 File Offset: 0x0005D199
		public CodeArrayCreateExpression(CodeTypeReference createType, CodeExpression size)
		{
			this._createType = createType;
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type name and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The name of the data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x0600181B RID: 6171 RVA: 0x0005EFBA File Offset: 0x0005D1BA
		public CodeArrayCreateExpression(string createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayCreateExpression" /> class using the specified array data type and code expression indicating the number of indexes for the array.</summary>
		/// <param name="createType">The data type of the array to create. </param>
		/// <param name="size">An expression that indicates the number of indexes of the array to create. </param>
		// Token: 0x0600181C RID: 6172 RVA: 0x0005EFE0 File Offset: 0x0005D1E0
		public CodeArrayCreateExpression(Type createType, CodeExpression size)
		{
			this._createType = new CodeTypeReference(createType);
			this.SizeExpression = size;
		}

		/// <summary>Gets or sets the type of array to create.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the type of the array.</returns>
		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0005F008 File Offset: 0x0005D208
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0005F032 File Offset: 0x0005D232
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

		/// <summary>Gets the initializers with which to initialize the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the initialization values.</returns>
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x0005F03B File Offset: 0x0005D23B
		public CodeExpressionCollection Initializers
		{
			get
			{
				return this._initializers;
			}
		}

		/// <summary>Gets or sets the number of indexes in the array.</summary>
		/// <returns>The number of indexes in the array.</returns>
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0005F043 File Offset: 0x0005D243
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x0005F04B File Offset: 0x0005D24B
		public int Size { get; set; }

		/// <summary>Gets or sets the expression that indicates the size of the array.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the size of the array.</returns>
		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0005F054 File Offset: 0x0005D254
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0005F05C File Offset: 0x0005D25C
		public CodeExpression SizeExpression { get; set; }

		// Token: 0x04000D69 RID: 3433
		private readonly CodeExpressionCollection _initializers = new CodeExpressionCollection();

		// Token: 0x04000D6A RID: 3434
		private CodeTypeReference _createType;
	}
}
