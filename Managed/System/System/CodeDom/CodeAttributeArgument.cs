using System;

namespace System.CodeDom
{
	/// <summary>Represents an argument used in a metadata attribute declaration.</summary>
	// Token: 0x020002F7 RID: 759
	[Serializable]
	public class CodeAttributeArgument
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class.</summary>
		// Token: 0x06001836 RID: 6198 RVA: 0x0000219B File Offset: 0x0000039B
		public CodeAttributeArgument()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified value.</summary>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06001837 RID: 6199 RVA: 0x0005F15F File Offset: 0x0005D35F
		public CodeAttributeArgument(CodeExpression value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeAttributeArgument" /> class using the specified name and value.</summary>
		/// <param name="name">The name of the attribute property the argument applies to. </param>
		/// <param name="value">A <see cref="T:System.CodeDom.CodeExpression" /> that represents the value of the argument. </param>
		// Token: 0x06001838 RID: 6200 RVA: 0x0005F16E File Offset: 0x0005D36E
		public CodeAttributeArgument(string name, CodeExpression value)
		{
			this.Name = name;
			this.Value = value;
		}

		/// <summary>Gets or sets the name of the attribute.</summary>
		/// <returns>The name of the attribute property the argument is for.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x0005F184 File Offset: 0x0005D384
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x0005F195 File Offset: 0x0005D395
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

		/// <summary>Gets or sets the value for the attribute argument.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the value for the attribute argument.</returns>
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x0005F19E File Offset: 0x0005D39E
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x0005F1A6 File Offset: 0x0005D3A6
		public CodeExpression Value { get; set; }

		// Token: 0x04000D73 RID: 3443
		private string _name;
	}
}
