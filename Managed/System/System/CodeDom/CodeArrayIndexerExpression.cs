using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an index of an array.</summary>
	// Token: 0x020002F4 RID: 756
	[Serializable]
	public class CodeArrayIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class.</summary>
		// Token: 0x06001824 RID: 6180 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeArrayIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArrayIndexerExpression" /> class using the specified target object and indexes.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the array the indexer targets. </param>
		/// <param name="indices">The index or indexes to reference. </param>
		// Token: 0x06001825 RID: 6181 RVA: 0x0005F065 File Offset: 0x0005D265
		public CodeArrayIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.TargetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object of the array indexer.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the array being indexed.</returns>
		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0005F080 File Offset: 0x0005D280
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0005F088 File Offset: 0x0005D288
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the index or indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0005F094 File Offset: 0x0005D294
		public CodeExpressionCollection Indices
		{
			get
			{
				CodeExpressionCollection codeExpressionCollection;
				if ((codeExpressionCollection = this._indices) == null)
				{
					codeExpressionCollection = (this._indices = new CodeExpressionCollection());
				}
				return codeExpressionCollection;
			}
		}

		// Token: 0x04000D6D RID: 3437
		private CodeExpressionCollection _indices;
	}
}
