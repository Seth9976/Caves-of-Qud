using System;

namespace System.CodeDom
{
	/// <summary>Represents an expression that creates a delegate.</summary>
	// Token: 0x02000309 RID: 777
	[Serializable]
	public class CodeDelegateCreateExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		// Token: 0x060018BD RID: 6333 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeDelegateCreateExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeDelegateCreateExpression" /> class.</summary>
		/// <param name="delegateType">A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate. </param>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method. </param>
		/// <param name="methodName">The name of the event-handler method. </param>
		// Token: 0x060018BE RID: 6334 RVA: 0x0005F98F File Offset: 0x0005DB8F
		public CodeDelegateCreateExpression(CodeTypeReference delegateType, CodeExpression targetObject, string methodName)
		{
			this._delegateType = delegateType;
			this.TargetObject = targetObject;
			this._methodName = methodName;
		}

		/// <summary>Gets or sets the data type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the data type of the delegate.</returns>
		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x060018BF RID: 6335 RVA: 0x0005F9AC File Offset: 0x0005DBAC
		// (set) Token: 0x060018C0 RID: 6336 RVA: 0x0005F9D6 File Offset: 0x0005DBD6
		public CodeTypeReference DelegateType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._delegateType) == null)
				{
					codeTypeReference = (this._delegateType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._delegateType = value;
			}
		}

		/// <summary>Gets or sets the object that contains the event-handler method.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object containing the event-handler method.</returns>
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x0005F9DF File Offset: 0x0005DBDF
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x0005F9E7 File Offset: 0x0005DBE7
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the event handler method.</summary>
		/// <returns>The name of the event handler method.</returns>
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x0005F9F0 File Offset: 0x0005DBF0
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0005FA01 File Offset: 0x0005DC01
		public string MethodName
		{
			get
			{
				return this._methodName ?? string.Empty;
			}
			set
			{
				this._methodName = value;
			}
		}

		// Token: 0x04000DA3 RID: 3491
		private CodeTypeReference _delegateType;

		// Token: 0x04000DA4 RID: 3492
		private string _methodName;
	}
}
