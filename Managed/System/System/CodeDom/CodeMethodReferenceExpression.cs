using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to a method.</summary>
	// Token: 0x02000343 RID: 835
	[Serializable]
	public class CodeMethodReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class.</summary>
		// Token: 0x06001A67 RID: 6759 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodeMethodReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object and method name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		// Token: 0x06001A68 RID: 6760 RVA: 0x0006186C File Offset: 0x0005FA6C
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeMethodReferenceExpression" /> class using the specified target object, method name, and generic type arguments.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object to target. </param>
		/// <param name="methodName">The name of the method to call. </param>
		/// <param name="typeParameters">An array of <see cref="T:System.CodeDom.CodeTypeReference" /> values that specify the <see cref="P:System.CodeDom.CodeMethodReferenceExpression.TypeArguments" /> for this <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</param>
		// Token: 0x06001A69 RID: 6761 RVA: 0x00061882 File Offset: 0x0005FA82
		public CodeMethodReferenceExpression(CodeExpression targetObject, string methodName, params CodeTypeReference[] typeParameters)
		{
			this.TargetObject = targetObject;
			this.MethodName = methodName;
			if (typeParameters != null && typeParameters.Length != 0)
			{
				this.TypeArguments.AddRange(typeParameters);
			}
		}

		/// <summary>Gets or sets the expression that indicates the method to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that represents the method to reference.</returns>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000618AB File Offset: 0x0005FAAB
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x000618B3 File Offset: 0x0005FAB3
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the method to reference.</summary>
		/// <returns>The name of the method to reference.</returns>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x000618BC File Offset: 0x0005FABC
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x000618CD File Offset: 0x0005FACD
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

		/// <summary>Gets the type arguments for the current generic method reference expression.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReferenceCollection" /> containing the type arguments for the current code <see cref="T:System.CodeDom.CodeMethodReferenceExpression" />.</returns>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000618D8 File Offset: 0x0005FAD8
		public CodeTypeReferenceCollection TypeArguments
		{
			get
			{
				CodeTypeReferenceCollection codeTypeReferenceCollection;
				if ((codeTypeReferenceCollection = this._typeArguments) == null)
				{
					codeTypeReferenceCollection = (this._typeArguments = new CodeTypeReferenceCollection());
				}
				return codeTypeReferenceCollection;
			}
		}

		// Token: 0x04000E38 RID: 3640
		private string _methodName;

		// Token: 0x04000E39 RID: 3641
		private CodeTypeReferenceCollection _typeArguments;
	}
}
