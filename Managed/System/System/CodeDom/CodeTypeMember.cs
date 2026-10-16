using System;

namespace System.CodeDom
{
	/// <summary>Provides a base class for a member of a type. Type members include fields, methods, properties, constructors and nested types.</summary>
	// Token: 0x02000339 RID: 825
	[Serializable]
	public class CodeTypeMember : CodeObject
	{
		/// <summary>Gets or sets the name of the member.</summary>
		/// <returns>The name of the member.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x000613A7 File Offset: 0x0005F5A7
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x000613B8 File Offset: 0x0005F5B8
		public string Name
		{
			get
			{
				return this._name ?? string.Empty;
			}
			set
			{
				this._name = value;
			}
		}

		/// <summary>Gets or sets the attributes of the member.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.CodeDom.MemberAttributes" /> values used to indicate the attributes of the member. The default value is <see cref="F:System.CodeDom.MemberAttributes.Private" /> | <see cref="F:System.CodeDom.MemberAttributes.Final" />. </returns>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x000613C1 File Offset: 0x0005F5C1
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x000613C9 File Offset: 0x0005F5C9
		public MemberAttributes Attributes { get; set; } = (MemberAttributes)20482;

		/// <summary>Gets or sets the custom attributes of the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes of the member.</returns>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x000613D4 File Offset: 0x0005F5D4
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x000613F9 File Offset: 0x0005F5F9
		public CodeAttributeDeclarationCollection CustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._customAttributes) == null)
				{
					codeAttributeDeclarationCollection = (this._customAttributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
			set
			{
				this._customAttributes = value;
			}
		}

		/// <summary>Gets or sets the line on which the type member statement occurs.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the location of the type member declaration.</returns>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00061402 File Offset: 0x0005F602
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0006140A File Offset: 0x0005F60A
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets the collection of comments for the type member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCommentStatementCollection" /> that indicates the comments for the member.</returns>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00061413 File Offset: 0x0005F613
		public CodeCommentStatementCollection Comments { get; } = new CodeCommentStatementCollection();

		/// <summary>Gets the start directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0006141C File Offset: 0x0005F61C
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets the end directives for the member.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00061444 File Offset: 0x0005F644
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04000E12 RID: 3602
		private string _name;

		// Token: 0x04000E13 RID: 3603
		private CodeAttributeDeclarationCollection _customAttributes;

		// Token: 0x04000E14 RID: 3604
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000E15 RID: 3605
		private CodeDirectiveCollection _endDirectives;
	}
}
