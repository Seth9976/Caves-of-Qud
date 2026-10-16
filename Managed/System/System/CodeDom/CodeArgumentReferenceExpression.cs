using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of an argument passed to a method.</summary>
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public class CodeArgumentReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class.</summary>
		// Token: 0x0600180F RID: 6159 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeArgumentReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeArgumentReferenceExpression" /> class using the specified parameter name.</summary>
		/// <param name="parameterName">The name of the parameter to reference. </param>
		// Token: 0x06001810 RID: 6160 RVA: 0x0005EE74 File Offset: 0x0005D074
		public CodeArgumentReferenceExpression(string parameterName)
		{
			this._parameterName = parameterName;
		}

		/// <summary>Gets or sets the name of the parameter this expression references.</summary>
		/// <returns>The name of the parameter to reference.</returns>
		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0005EE83 File Offset: 0x0005D083
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x0005EE94 File Offset: 0x0005D094
		public string ParameterName
		{
			get
			{
				return this._parameterName ?? string.Empty;
			}
			set
			{
				this._parameterName = value;
			}
		}

		// Token: 0x04000D68 RID: 3432
		private string _parameterName;
	}
}
