using System;

namespace System.CodeDom
{
	/// <summary>Represents the abstract base class from which all code statements derive.</summary>
	// Token: 0x02000330 RID: 816
	[Serializable]
	public class CodeStatement : CodeObject
	{
		/// <summary>Gets or sets the line on which the code statement occurs. </summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeLinePragma" /> object that indicates the context of the code statement.</returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x00060D4C File Offset: 0x0005EF4C
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x00060D54 File Offset: 0x0005EF54
		public CodeLinePragma LinePragma { get; set; }

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x00060D60 File Offset: 0x0005EF60
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

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object that contains end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00060D88 File Offset: 0x0005EF88
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

		// Token: 0x04000DFD RID: 3581
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000DFE RID: 3582
		private CodeDirectiveCollection _endDirectives;
	}
}
