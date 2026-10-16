using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to an indexer property of an object.</summary>
	// Token: 0x02000315 RID: 789
	[Serializable]
	public class CodeIndexerExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class.</summary>
		// Token: 0x06001902 RID: 6402 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeIndexerExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeIndexerExpression" /> class using the specified target object and index.</summary>
		/// <param name="targetObject">The target object. </param>
		/// <param name="indices">The index or indexes of the indexer expression. </param>
		// Token: 0x06001903 RID: 6403 RVA: 0x0005FCD0 File Offset: 0x0005DED0
		public CodeIndexerExpression(CodeExpression targetObject, params CodeExpression[] indices)
		{
			this.TargetObject = targetObject;
			this.Indices.AddRange(indices);
		}

		/// <summary>Gets or sets the target object that can be indexed.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the indexer object.</returns>
		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0005FCEB File Offset: 0x0005DEEB
		// (set) Token: 0x06001905 RID: 6405 RVA: 0x0005FCF3 File Offset: 0x0005DEF3
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets the collection of indexes of the indexer expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the index or indexes of the indexer expression.</returns>
		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06001906 RID: 6406 RVA: 0x0005FCFC File Offset: 0x0005DEFC
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

		// Token: 0x04000DB0 RID: 3504
		private CodeExpressionCollection _indices;
	}
}
