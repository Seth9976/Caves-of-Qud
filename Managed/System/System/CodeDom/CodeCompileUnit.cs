using System;
using System.Collections.Specialized;

namespace System.CodeDom
{
	/// <summary>Provides a container for a CodeDOM program graph.</summary>
	// Token: 0x02000305 RID: 773
	[Serializable]
	public class CodeCompileUnit : CodeObject
	{
		/// <summary>Gets the collection of namespaces.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeNamespaceCollection" /> that indicates the namespaces that the compile unit uses.</returns>
		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x0005F7BF File Offset: 0x0005D9BF
		public CodeNamespaceCollection Namespaces { get; } = new CodeNamespaceCollection();

		/// <summary>Gets the referenced assemblies.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the file names of the referenced assemblies.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0005F7C8 File Offset: 0x0005D9C8
		public StringCollection ReferencedAssemblies
		{
			get
			{
				StringCollection stringCollection;
				if ((stringCollection = this._assemblies) == null)
				{
					stringCollection = (this._assemblies = new StringCollection());
				}
				return stringCollection;
			}
		}

		/// <summary>Gets a collection of custom attributes for the generated assembly.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeAttributeDeclarationCollection" /> that indicates the custom attributes for the generated assembly.</returns>
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005F7F0 File Offset: 0x0005D9F0
		public CodeAttributeDeclarationCollection AssemblyCustomAttributes
		{
			get
			{
				CodeAttributeDeclarationCollection codeAttributeDeclarationCollection;
				if ((codeAttributeDeclarationCollection = this._attributes) == null)
				{
					codeAttributeDeclarationCollection = (this._attributes = new CodeAttributeDeclarationCollection());
				}
				return codeAttributeDeclarationCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing start directives.</returns>
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0005F818 File Offset: 0x0005DA18
		public CodeDirectiveCollection StartDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._startDirectives) == null)
				{
					codeDirectiveCollection = (this._startDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		/// <summary>Gets a <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeDirectiveCollection" /> object containing end directives.</returns>
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060018AE RID: 6318 RVA: 0x0005F840 File Offset: 0x0005DA40
		public CodeDirectiveCollection EndDirectives
		{
			get
			{
				CodeDirectiveCollection codeDirectiveCollection;
				if ((codeDirectiveCollection = this._endDirectives) == null)
				{
					codeDirectiveCollection = (this._endDirectives = new CodeDirectiveCollection());
				}
				return codeDirectiveCollection;
			}
		}

		// Token: 0x04000D98 RID: 3480
		private StringCollection _assemblies;

		// Token: 0x04000D99 RID: 3481
		private CodeAttributeDeclarationCollection _attributes;

		// Token: 0x04000D9A RID: 3482
		private CodeDirectiveCollection _startDirectives;

		// Token: 0x04000D9B RID: 3483
		private CodeDirectiveCollection _endDirectives;
	}
}
