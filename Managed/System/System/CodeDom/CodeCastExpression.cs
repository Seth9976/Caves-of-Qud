using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression cast to a data type or interface.</summary>
	// Token: 0x020002FE RID: 766
	[Serializable]
	public class CodeCastExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class.</summary>
		// Token: 0x06001869 RID: 6249 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeCastExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x0600186A RID: 6250 RVA: 0x0005F414 File Offset: 0x0005D614
		public CodeCastExpression(CodeTypeReference targetType, CodeExpression expression)
		{
			this.TargetType = targetType;
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The name of the destination type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x0600186B RID: 6251 RVA: 0x0005F42A File Offset: 0x0005D62A
		public CodeCastExpression(string targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeCastExpression" /> class using the specified destination type and expression.</summary>
		/// <param name="targetType">The destination data type of the cast. </param>
		/// <param name="expression">The <see cref="T:System.CodeDom.CodeExpression" /> to cast. </param>
		// Token: 0x0600186C RID: 6252 RVA: 0x0005F445 File Offset: 0x0005D645
		public CodeCastExpression(Type targetType, CodeExpression expression)
		{
			this.TargetType = new CodeTypeReference(targetType);
			this.Expression = expression;
		}

		/// <summary>Gets or sets the destination type of the cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the destination type to cast to.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600186D RID: 6253 RVA: 0x0005F460 File Offset: 0x0005D660
		// (set) Token: 0x0600186E RID: 6254 RVA: 0x0005F48A File Offset: 0x0005D68A
		public CodeTypeReference TargetType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._targetType) == null)
				{
					codeTypeReference = (this._targetType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._targetType = value;
			}
		}

		/// <summary>Gets or sets the expression to cast.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code to cast.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x0005F493 File Offset: 0x0005D693
		// (set) Token: 0x06001870 RID: 6256 RVA: 0x0005F49B File Offset: 0x0005D69B
		public CodeExpression Expression { get; set; }

		// Token: 0x04000D8D RID: 3469
		private CodeTypeReference _targetType;
	}
}
