using System;

namespace System.CodeDom
{
	/// <summary>Represents a primitive data type value.</summary>
	// Token: 0x02000326 RID: 806
	[Serializable]
	public class CodePrimitiveExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class.</summary>
		// Token: 0x060019AB RID: 6571 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodePrimitiveExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePrimitiveExpression" /> class using the specified object.</summary>
		/// <param name="value">The object to represent. </param>
		// Token: 0x060019AC RID: 6572 RVA: 0x00060B7C File Offset: 0x0005ED7C
		public CodePrimitiveExpression(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets or sets the primitive data type to represent.</summary>
		/// <returns>The primitive data type instance to represent the value of.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x00060B8B File Offset: 0x0005ED8B
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x00060B93 File Offset: 0x0005ED93
		public object Value { get; set; }
	}
}
