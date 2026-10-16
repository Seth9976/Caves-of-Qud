using System;
using System.IO;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an empty implementation of the <see cref="T:System.CodeDom.Compiler.ICodeParser" /> interface.</summary>
	// Token: 0x0200034A RID: 842
	public abstract class CodeParser : ICodeParser
	{
		/// <summary>Compiles the specified text stream into a <see cref="T:System.CodeDom.CodeCompileUnit" />.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeCompileUnit" /> containing the code model produced from parsing the code.</returns>
		/// <param name="codeStream">A <see cref="T:System.IO.TextReader" /> that is used to read the code to be parsed. </param>
		// Token: 0x06001B59 RID: 7001
		public abstract CodeCompileUnit Parse(TextReader codeStream);
	}
}
