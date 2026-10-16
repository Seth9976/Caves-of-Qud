using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that invokes a method.</summary>
	// Token: 0x0200031D RID: 797
	[Serializable]
	public class CodeMethodInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class.</summary>
		// Token: 0x06001948 RID: 6472 RVA: 0x0006031A File Offset: 0x0005E51A
		public CodeMethodInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified method and parameters.</summary>
		/// <param name="method">A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters with which to invoke the method. </param>
		// Token: 0x06001949 RID: 6473 RVA: 0x0006032D File Offset: 0x0005E52D
		public CodeMethodInvokeExpression(CodeMethodReferenceExpression method, params CodeExpression[] parameters)
		{
			this._method = method;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodInvokeExpression" /> class using the specified target object, method name, and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object with the method to invoke. </param>
		/// <param name="methodName">The name of the method to invoke. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters to call the method with. </param>
		// Token: 0x0600194A RID: 6474 RVA: 0x00060353 File Offset: 0x0005E553
		public CodeMethodInvokeExpression(CodeExpression targetObject, string methodName, params CodeExpression[] parameters)
		{
			this._method = new CodeMethodReferenceExpression(targetObject, methodName);
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the method to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> that indicates the method to invoke.</returns>
		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600194B RID: 6475 RVA: 0x00060380 File Offset: 0x0005E580
		// (set) Token: 0x0600194C RID: 6476 RVA: 0x000603A5 File Offset: 0x0005E5A5
		public CodeMethodReferenceExpression Method
		{
			get
			{
				CodeMethodReferenceExpression codeMethodReferenceExpression;
				if ((codeMethodReferenceExpression = this._method) == null)
				{
					codeMethodReferenceExpression = (this._method = new CodeMethodReferenceExpression());
				}
				return codeMethodReferenceExpression;
			}
			set
			{
				this._method = value;
			}
		}

		/// <summary>Gets the parameters to invoke the method with.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that indicates the parameters to invoke the method with.</returns>
		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x0600194D RID: 6477 RVA: 0x000603AE File Offset: 0x0005E5AE
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();

		// Token: 0x04000DD5 RID: 3541
		private CodeMethodReferenceExpression _method;
	}
}
