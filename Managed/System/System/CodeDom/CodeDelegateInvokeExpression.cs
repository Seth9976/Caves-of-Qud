using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that raises an event.</summary>
	// Token: 0x0200030A RID: 778
	[Serializable]
	public class CodeDelegateInvokeExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class.</summary>
		// Token: 0x060018C5 RID: 6341 RVA: 0x0005FA0A File Offset: 0x0005DC0A
		public CodeDelegateInvokeExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		// Token: 0x060018C6 RID: 6342 RVA: 0x0005FA1D File Offset: 0x0005DC1D
		public CodeDelegateInvokeExpression(CodeExpression targetObject)
		{
			this.TargetObject = targetObject;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateInvokeExpression" /> class using the specified target object and parameters.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the target object. </param>
		/// <param name="parameters">An array of <see cref="T:System.CodeDom.CodeExpression" /> objects that indicate the parameters. </param>
		// Token: 0x060018C7 RID: 6343 RVA: 0x0005FA37 File Offset: 0x0005DC37
		public CodeDelegateInvokeExpression(CodeExpression targetObject, params CodeExpression[] parameters)
		{
			this.TargetObject = targetObject;
			this.Parameters.AddRange(parameters);
		}

		/// <summary>Gets or sets the event to invoke.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the event to invoke.</returns>
		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005FA5D File Offset: 0x0005DC5D
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0005FA65 File Offset: 0x0005DC65
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the parameters to pass to the event handling methods attached to the event.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the parameters to pass to the event handling methods attached to the event.</returns>
		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005FA6E File Offset: 0x0005DC6E
		public CodeExpressionCollection Parameters { get; } = new CodeExpressionCollection();
	}
}
