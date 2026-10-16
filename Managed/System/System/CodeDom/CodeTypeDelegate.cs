using System;
using System.Reflection;

namespace System.CodeDom
{
	/// <summary>Represents a delegate declaration.</summary>
	// Token: 0x02000338 RID: 824
	[Serializable]
	public class CodeTypeDelegate : CodeTypeDeclaration
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		// Token: 0x06001A16 RID: 6678 RVA: 0x00061300 File Offset: 0x0005F500
		public CodeTypeDelegate()
		{
			base.TypeAttributes &= ~TypeAttributes.ClassSemanticsMask;
			base.TypeAttributes |= TypeAttributes.NotPublic;
			base.BaseTypes.Clear();
			base.BaseTypes.Add(new CodeTypeReference("System.Delegate"));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeTypeDelegate" /> class.</summary>
		/// <param name="name">The name of the delegate. </param>
		// Token: 0x06001A17 RID: 6679 RVA: 0x0006135C File Offset: 0x0005F55C
		public CodeTypeDelegate(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>Gets or sets the return type of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeTypeReference" /> that indicates the return type of the delegate.</returns>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001A18 RID: 6680 RVA: 0x0006136C File Offset: 0x0005F56C
		// (set) Token: 0x06001A19 RID: 6681 RVA: 0x00061396 File Offset: 0x0005F596
		public CodeTypeReference ReturnType
		{
			get
			{
				CodeTypeReference codeTypeReference;
				if ((codeTypeReference = this._returnType) == null)
				{
					codeTypeReference = (this._returnType = new CodeTypeReference(""));
				}
				return codeTypeReference;
			}
			set
			{
				this._returnType = value;
			}
		}

		/// <summary>Gets the parameters of the delegate.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeParameterDeclarationExpressionCollection" /> that indicates the parameters of the delegate.</returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001A1A RID: 6682 RVA: 0x0006139F File Offset: 0x0005F59F
		public CodeParameterDeclarationExpressionCollection Parameters { get; } = new CodeParameterDeclarationExpressionCollection();

		// Token: 0x04000E10 RID: 3600
		private CodeTypeReference _returnType;
	}
}
