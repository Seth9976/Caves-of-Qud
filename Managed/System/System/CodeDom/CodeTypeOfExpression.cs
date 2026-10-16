using System;

namespace System.CodeDom
{
	/// <summary>Represents a typeof expression, an expression that returns a <see cref="T:System.Type" /> for a specified type name.</summary>
	// Token: 0x0200033B RID: 827
	[Serializable]
	public class CodeTypeOfExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		// Token: 0x06001A34 RID: 6708 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeTypeOfExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type for the typeof expression. </param>
		// Token: 0x06001A35 RID: 6709 RVA: 0x00061528 File Offset: 0x0005F728
		public CodeTypeOfExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The name of the data type for the typeof expression. </param>
		// Token: 0x06001A36 RID: 6710 RVA: 0x00061537 File Offset: 0x0005F737
		public CodeTypeOfExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeOfExpression" /> class using the specified type.</summary>
		/// <param name="type">The data type of the data type of the typeof expression. </param>
		// Token: 0x06001A37 RID: 6711 RVA: 0x0006154B File Offset: 0x0005F74B
		public CodeTypeOfExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type referenced by the typeof expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type referenced by the typeof expression. This property will never return null, and defaults to the <see cref="T:System.Void" /> type.</returns>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001A38 RID: 6712 RVA: 0x00061560 File Offset: 0x0005F760
		// (set) Token: 0x06001A39 RID: 6713 RVA: 0x0006158A File Offset: 0x0005F78A
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

		// Token: 0x04000E19 RID: 3609
		private CodeTypeReference _type;
	}
}
