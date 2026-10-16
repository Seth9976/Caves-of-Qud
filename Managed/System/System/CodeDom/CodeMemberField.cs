using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for a field of a type.</summary>
	// Token: 0x0200031A RID: 794
	[Serializable]
	public class CodeMemberField : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class.</summary>
		// Token: 0x06001923 RID: 6435 RVA: 0x0005FE35 File Offset: 0x0005E035
		public CodeMemberField()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">An object that indicates the type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06001924 RID: 6436 RVA: 0x0005FEA9 File Offset: 0x0005E0A9
		public CodeMemberField(CodeTypeReference type, string name)
		{
			this.Type = type;
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06001925 RID: 6437 RVA: 0x0005FEBF File Offset: 0x0005E0BF
		public CodeMemberField(string type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMemberField" /> class using the specified field type and field name.</summary>
		/// <param name="type">The type of the field. </param>
		/// <param name="name">The name of the field. </param>
		// Token: 0x06001926 RID: 6438 RVA: 0x0005FEDA File Offset: 0x0005E0DA
		public CodeMemberField(Type type, string name)
		{
			this.Type = new CodeTypeReference(type);
			base.Name = name;
		}

		/// <summary>Gets or sets the type of the field.</summary>
		/// <returns>The type of the field.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06001927 RID: 6439 RVA: 0x0005FEF8 File Offset: 0x0005E0F8
		// (set) Token: 0x06001928 RID: 6440 RVA: 0x0005FF22 File Offset: 0x0005E122
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

		/// <summary>Gets or sets the initialization expression for the field.</summary>
		/// <returns>The initialization expression for the field.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001929 RID: 6441 RVA: 0x0005FF2B File Offset: 0x0005E12B
		// (set) Token: 0x0600192A RID: 6442 RVA: 0x0005FF33 File Offset: 0x0005E133
		public CodeExpression InitExpression { get; set; }

		// Token: 0x04000DBD RID: 3517
		private CodeTypeReference _type;
	}
}
