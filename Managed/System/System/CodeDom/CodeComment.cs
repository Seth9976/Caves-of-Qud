using System;

namespace System.CodeDom
{
	/// <summary>Represents a comment.</summary>
	// Token: 0x02000302 RID: 770
	[Serializable]
	public class CodeComment : CodeObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class.</summary>
		// Token: 0x0600188F RID: 6287 RVA: 0x0005F669 File Offset: 0x0005D869
		public CodeComment()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class with the specified text as contents.</summary>
		/// <param name="text">The contents of the comment. </param>
		// Token: 0x06001890 RID: 6288 RVA: 0x0005F671 File Offset: 0x0005D871
		public CodeComment(string text)
		{
			this.Text = text;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeComment" /> class using the specified text and documentation comment flag.</summary>
		/// <param name="text">The contents of the comment. </param>
		/// <param name="docComment">true if the comment is a documentation comment; otherwise, false. </param>
		// Token: 0x06001891 RID: 6289 RVA: 0x0005F680 File Offset: 0x0005D880
		public CodeComment(string text, bool docComment)
		{
			this.Text = text;
			this.DocComment = docComment;
		}

		/// <summary>Gets or sets a value that indicates whether the comment is a documentation comment.</summary>
		/// <returns>true if the comment is a documentation comment; otherwise, false.</returns>
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0005F696 File Offset: 0x0005D896
		// (set) Token: 0x06001893 RID: 6291 RVA: 0x0005F69E File Offset: 0x0005D89E
		public bool DocComment { get; set; }

		/// <summary>Gets or sets the text of the comment.</summary>
		/// <returns>A string containing the comment text.</returns>
		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x0005F6A7 File Offset: 0x0005D8A7
		// (set) Token: 0x06001895 RID: 6293 RVA: 0x0005F6B8 File Offset: 0x0005D8B8
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

		// Token: 0x04000D95 RID: 3477
		private string _text;
	}
}
