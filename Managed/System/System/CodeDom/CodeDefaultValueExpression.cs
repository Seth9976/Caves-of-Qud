using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a default value.</summary>
	// Token: 0x02000308 RID: 776
	[Serializable]
	public class CodeDefaultValueExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class. </summary>
		// Token: 0x060018B9 RID: 6329 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeDefaultValueExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDefaultValueExpression" /> class using the specified code type reference.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that specifies the reference to a value type.</param>
		// Token: 0x060018BA RID: 6330 RVA: 0x0005F94B File Offset: 0x0005DB4B
		public CodeDefaultValueExpression(CodeTypeReference type)
		{
			this._type = type;
		}

		/// <summary>Gets or sets the data type reference for a default value.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> object representing a data type that has a default value.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0005F95C File Offset: 0x0005DB5C
		// (set) Token: 0x060018BC RID: 6332 RVA: 0x0005F986 File Offset: 0x0005DB86
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

		// Token: 0x04000DA2 RID: 3490
		private CodeTypeReference _type;
	}
}
