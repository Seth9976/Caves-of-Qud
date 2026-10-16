using System;

namespace System.CodeDom
{
	/// <summary>Represents a declaration for an instance constructor of a type.</summary>
	// Token: 0x02000307 RID: 775
	[Serializable]
	public class CodeConstructor : CodeMemberMethod
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodeConstructor" /> class.</summary>
		// Token: 0x060018B6 RID: 6326 RVA: 0x0005F912 File Offset: 0x0005DB12
		public CodeConstructor()
		{
			base.Name = ".ctor";
		}

		/// <summary>Gets the collection of base constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the base constructor arguments.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x0005F93B File Offset: 0x0005DB3B
		public CodeExpressionCollection BaseConstructorArgs { get; } = new CodeExpressionCollection();

		/// <summary>Gets the collection of chained constructor arguments.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpressionCollection" /> that contains the chained constructor arguments.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060018B8 RID: 6328 RVA: 0x0005F943 File Offset: 0x0005DB43
		public CodeExpressionCollection ChainedConstructorArgs { get; } = new CodeExpressionCollection();
	}
}
