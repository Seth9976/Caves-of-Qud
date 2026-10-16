using System;

namespace System.CodeDom
{
	/// <summary>Represents a simple assignment statement.</summary>
	// Token: 0x020002F5 RID: 757
	[Serializable]
	public class CodeAssignStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class.</summary>
		// Token: 0x06001829 RID: 6185 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeAssignStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAssignStatement" /> class using the specified expressions.</summary>
		/// <param name="left">The variable to assign to. </param>
		/// <param name="right">The value to assign. </param>
		// Token: 0x0600182A RID: 6186 RVA: 0x0005F0C1 File Offset: 0x0005D2C1
		public CodeAssignStatement(CodeExpression left, CodeExpression right)
		{
			this.Left = left;
			this.Right = right;
		}

		/// <summary>Gets or sets the expression representing the object or reference to assign to.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign to.</returns>
		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600182B RID: 6187 RVA: 0x0005F0D7 File Offset: 0x0005D2D7
		// (set) Token: 0x0600182C RID: 6188 RVA: 0x0005F0DF File Offset: 0x0005D2DF
		public CodeExpression Left { get; set; }

		/// <summary>Gets or sets the expression representing the object or reference to assign.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object or reference to assign.</returns>
		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x0600182D RID: 6189 RVA: 0x0005F0E8 File Offset: 0x0005D2E8
		// (set) Token: 0x0600182E RID: 6190 RVA: 0x0005F0F0 File Offset: 0x0005D2F0
		public CodeExpression Right { get; set; }
	}
}
