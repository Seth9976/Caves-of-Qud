using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression used as a method invoke parameter along with a reference direction indicator.</summary>
	// Token: 0x0200030B RID: 779
	[Serializable]
	public class CodeDirectionExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class.</summary>
		// Token: 0x060018CB RID: 6347 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeDirectionExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDirectionExpression" /> class using the specified field direction and expression.</summary>
		/// <param name="direction">A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction of the expression. </param>
		/// <param name="expression">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the code expression to represent. </param>
		// Token: 0x060018CC RID: 6348 RVA: 0x0005FA76 File Offset: 0x0005DC76
		public CodeDirectionExpression(FieldDirection direction, CodeExpression expression)
		{
			this.Expression = expression;
			this.Direction = direction;
		}

		/// <summary>Gets or sets the code expression to represent.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the expression to represent.</returns>
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x0005FA8C File Offset: 0x0005DC8C
		// (set) Token: 0x060018CE RID: 6350 RVA: 0x0005FA94 File Offset: 0x0005DC94
		public CodeExpression Expression { get; set; }

		/// <summary>Gets or sets the field direction for this direction expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.FieldDirection" /> that indicates the field direction for this direction expression.</returns>
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x0005FA9D File Offset: 0x0005DC9D
		// (set) Token: 0x060018D0 RID: 6352 RVA: 0x0005FAA5 File Offset: 0x0005DCA5
		public FieldDirection Direction { get; set; }
	}
}
