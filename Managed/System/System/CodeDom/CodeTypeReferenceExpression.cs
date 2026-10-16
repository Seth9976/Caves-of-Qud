using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a data type.</summary>
	// Token: 0x0200033E RID: 830
	[Serializable]
	public class CodeTypeReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class.</summary>
		// Token: 0x06001A50 RID: 6736 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeTypeReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified type.</summary>
		/// <param name="type">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference. </param>
		// Token: 0x06001A51 RID: 6737 RVA: 0x000616CC File Offset: 0x0005F8CC
		public CodeTypeReferenceExpression(CodeTypeReference type)
		{
			this.Type = type;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type name.</summary>
		/// <param name="type">The name of the data type to reference. </param>
		// Token: 0x06001A52 RID: 6738 RVA: 0x000616DB File Offset: 0x0005F8DB
		public CodeTypeReferenceExpression(string type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeReferenceExpression" /> class using the specified data type.</summary>
		/// <param name="type">An instance of the data type to reference. </param>
		// Token: 0x06001A53 RID: 6739 RVA: 0x000616EF File Offset: 0x0005F8EF
		public CodeTypeReferenceExpression(Type type)
		{
			this.Type = new CodeTypeReference(type);
		}

		/// <summary>Gets or sets the data type to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type to reference.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x00061704 File Offset: 0x0005F904
		// (set) Token: 0x06001A55 RID: 6741 RVA: 0x0006172E File Offset: 0x0005F92E
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

		// Token: 0x04000E1E RID: 3614
		private CodeTypeReference _type;
	}
}
