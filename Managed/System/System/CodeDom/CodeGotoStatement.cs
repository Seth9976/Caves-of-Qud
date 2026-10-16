using System;

namespace System.CodeDom
{
	/// <summary>Represents a goto statement.</summary>
	// Token: 0x02000314 RID: 788
	[Serializable]
	public class CodeGotoStatement : CodeStatement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class. </summary>
		// Token: 0x060018FE RID: 6398 RVA: 0x0005F0B9 File Offset: 0x0005D2B9
		public CodeGotoStatement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeGotoStatement" /> class using the specified label name.</summary>
		/// <param name="label">The name of the label at which to continue program execution. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="Label" /> is null.</exception>
		// Token: 0x060018FF RID: 6399 RVA: 0x0005FC9D File Offset: 0x0005DE9D
		public CodeGotoStatement(string label)
		{
			this.Label = label;
		}

		/// <summary>Gets or sets the name of the label at which to continue program execution.</summary>
		/// <returns>A string that indicates the name of the label at which to continue program execution.</returns>
		/// <exception cref="T:System.ArgumentNullException">The label cannot be set because<paramref name=" value" /> is null or an empty string.</exception>
		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005FCAC File Offset: 0x0005DEAC
		// (set) Token: 0x06001901 RID: 6401 RVA: 0x0005FCB4 File Offset: 0x0005DEB4
		public string Label
		{
			get
			{
				return this._label;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("value");
				}
				this._label = value;
			}
		}

		// Token: 0x04000DAF RID: 3503
		private string _label;
	}
}
