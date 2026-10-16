using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a local variable.</summary>
	// Token: 0x02000340 RID: 832
	[Serializable]
	public class CodeVariableReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class.</summary>
		// Token: 0x06001A63 RID: 6755 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeVariableReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeVariableReferenceExpression" /> class using the specified local variable name.</summary>
		/// <param name="variableName">The name of the local variable to reference. </param>
		// Token: 0x06001A64 RID: 6756 RVA: 0x00061843 File Offset: 0x0005FA43
		public CodeVariableReferenceExpression(string variableName)
		{
			this._variableName = variableName;
		}

		/// <summary>Gets or sets the name of the local variable to reference.</summary>
		/// <returns>The name of the local variable to reference.</returns>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00061852 File Offset: 0x0005FA52
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x00061863 File Offset: 0x0005FA63
		public string VariableName
		{
			get
			{
				return this._variableName ?? string.Empty;
			}
			set
			{
				this._variableName = value;
			}
		}

		// Token: 0x04000E22 RID: 3618
		private string _variableName;
	}
}
