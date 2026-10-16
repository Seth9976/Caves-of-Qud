using System;

namespace System.CodeDom
{
	/// <summary>Represents a member of a type using a literal code fragment.</summary>
	// Token: 0x0200032F RID: 815
	[Serializable]
	public class CodeSnippetTypeMember : CodeTypeMember
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class.</summary>
		// Token: 0x060019D1 RID: 6609 RVA: 0x0005FE35 File Offset: 0x0005E035
		public CodeSnippetTypeMember()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeSnippetTypeMember" /> class using the specified text.</summary>
		/// <param name="text">The literal code fragment for the type member. </param>
		// Token: 0x060019D2 RID: 6610 RVA: 0x00060D23 File Offset: 0x0005EF23
		public CodeSnippetTypeMember(string text)
		{
			this.Text = text;
		}

		/// <summary>Gets or sets the literal code fragment for the type member.</summary>
		/// <returns>The literal code fragment for the type member.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x00060D32 File Offset: 0x0005EF32
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x00060D43 File Offset: 0x0005EF43
		public string Text
		{
			get
			{
				return this._text ?? string.Empty;
			}
			set
			{
				this._text = value;
			}
		}

		// Token: 0x04000DFC RID: 3580
		private string _text;
	}
}
