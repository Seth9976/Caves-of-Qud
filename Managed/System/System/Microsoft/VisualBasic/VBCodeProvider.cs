using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000139 RID: 313
	public class VBCodeProvider : CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class. </summary>
		// Token: 0x060007B2 RID: 1970 RVA: 0x000182FE File Offset: 0x000164FE
		public VBCodeProvider()
		{
			this._generator = new VBCodeGenerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		// Token: 0x060007B3 RID: 1971 RVA: 0x00018311 File Offset: 0x00016511
		public VBCodeProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			this._generator = new VBCodeGenerator(providerOptions);
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00018333 File Offset: 0x00016533
		public override string FileExtension
		{
			get
			{
				return "vb";
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000390E File Offset: 0x00001B0E
		public override LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.CaseInsensitive;
			}
		}

		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007B6 RID: 1974 RVA: 0x0001833A File Offset: 0x0001653A
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this._generator;
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007B7 RID: 1975 RVA: 0x0001833A File Offset: 0x0001653A
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this._generator;
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007B8 RID: 1976 RVA: 0x00018342 File Offset: 0x00016542
		public override TypeConverter GetConverter(Type type)
		{
			if (type == typeof(MemberAttributes))
			{
				return VBMemberAttributeConverter.Default;
			}
			if (!(type == typeof(TypeAttributes)))
			{
				return base.GetConverter(type);
			}
			return VBTypeAttributeConverter.Default;
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007B9 RID: 1977 RVA: 0x0001837B File Offset: 0x0001657B
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			this._generator.GenerateCodeFromMember(member, writer, options);
		}

		// Token: 0x04000540 RID: 1344
		private VBCodeGenerator _generator;
	}
}
