using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000650 RID: 1616
	internal abstract class GenericBuildProvider<TParser> : BuildProvider
	{
		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000BBF2F File Offset: 0x000BA12F
		protected bool Parsed
		{
			get
			{
				return this._parsed;
			}
		}

		// Token: 0x0600454C RID: 17740
		protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context);

		// Token: 0x0600454D RID: 17741
		protected abstract TParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context);

		// Token: 0x0600454E RID: 17742
		protected abstract BaseCompiler CreateCompiler(TParser parser);

		// Token: 0x0600454F RID: 17743
		protected abstract string GetParserLanguage(TParser parser);

		// Token: 0x06004550 RID: 17744
		protected abstract ICollection GetParserDependencies(TParser parser);

		// Token: 0x06004551 RID: 17745
		protected abstract string GetCodeBehindSource(TParser parser);

		// Token: 0x06004552 RID: 17746
		protected abstract string GetClassType(BaseCompiler compiler, TParser parser);

		// Token: 0x06004553 RID: 17747
		protected abstract AspGenerator CreateAspGenerator(TParser parser);

		// Token: 0x06004554 RID: 17748
		protected abstract List<string> GetReferencedAssemblies(TParser parser);

		// Token: 0x06004555 RID: 17749 RVA: 0x000BBF38 File Offset: 0x000BA138
		protected virtual string MapPath(VirtualPath virtualPath)
		{
			HttpContext httpContext = HttpContext.Current;
			HttpRequest httpRequest = ((httpContext != null) ? httpContext.Request : null);
			if (httpRequest != null)
			{
				return httpRequest.MapPath(base.VirtualPath);
			}
			return null;
		}

		// Token: 0x06004556 RID: 17750 RVA: 0x000BBF6C File Offset: 0x000BA16C
		protected virtual TParser Parse()
		{
			TParser parser = this.Parser;
			if (this._parsed)
			{
				return parser;
			}
			if (!this.IsDirectoryBuilder)
			{
				AspGenerator aspGenerator = this.CreateAspGenerator(parser);
				if (this._reader != null)
				{
					aspGenerator.Parse(this._reader, this.MapPath(base.VirtualPathInternal), true);
				}
				else
				{
					aspGenerator.Parse();
				}
			}
			this._parsed = true;
			return parser;
		}

		// Token: 0x06004557 RID: 17751 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OverrideAssemblyPrefix(TParser parser, AssemblyBuilder assemblyBuilder)
		{
		}

		// Token: 0x06004558 RID: 17752 RVA: 0x000BBFCC File Offset: 0x000BA1CC
		internal override void GenerateCode()
		{
			TParser tparser = this.Parse();
			this._compiler = this.CreateCompiler(tparser);
			if (this.NeedsConstructType)
			{
				this._compiler.ConstructType();
			}
			this._codeGenerated = true;
		}

		// Token: 0x06004559 RID: 17753 RVA: 0x000BC008 File Offset: 0x000BA208
		protected virtual void GenerateCode(AssemblyBuilder assemblyBuilder, TParser parser, BaseCompiler compiler)
		{
			CodeCompileUnit compileUnit = this._compiler.CompileUnit;
			if (compileUnit == null)
			{
				throw new HttpException("Unable to generate source code.");
			}
			assemblyBuilder.AddCodeCompileUnit(this, compileUnit);
		}

		// Token: 0x0600455A RID: 17754 RVA: 0x000BC038 File Offset: 0x000BA238
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			if (!this._codeGenerated)
			{
				this.GenerateCode();
			}
			TParser tparser = this.Parse();
			this.OverrideAssemblyPrefix(tparser, assemblyBuilder);
			string codeBehindSource = this.GetCodeBehindSource(tparser);
			if (codeBehindSource != null)
			{
				assemblyBuilder.AddCodeFile(codeBehindSource, this, true);
			}
			List<string> referencedAssemblies = this.GetReferencedAssemblies(tparser);
			if (referencedAssemblies != null && referencedAssemblies.Count > 0)
			{
				foreach (string text in referencedAssemblies)
				{
					assemblyBuilder.AddAssemblyReference(text);
				}
			}
			this.GenerateCode(assemblyBuilder, tparser, this._compiler);
		}

		// Token: 0x0600455B RID: 17755 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual Type LoadTypeFromBin(BaseCompiler compiler, TParser parser)
		{
			return null;
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000BC0DC File Offset: 0x000BA2DC
		public override Type GetGeneratedType(CompilerResults results)
		{
			if (this.NeedsLoadFromBin && this._compiler != null)
			{
				return this.LoadTypeFromBin(this._compiler, this.Parser);
			}
			Type type = null;
			Assembly assembly = ((results != null) ? results.CompiledAssembly : null);
			if (assembly != null)
			{
				type = assembly.GetType(this.GetClassType(this._compiler, this.Parser));
			}
			if (type == null)
			{
				throw new HttpException(500, string.Format("Type {0} could not be loaded", this.GetClassType(this._compiler, this.Parser)));
			}
			return type;
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x000BC16E File Offset: 0x000BA36E
		protected virtual TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
		{
			physicalPath = null;
			return base.OpenReader(virtualPath.Original);
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x000BC180 File Offset: 0x000BA380
		public override ICollection VirtualPathDependencies
		{
			get
			{
				TParser parser = this.Parser;
				return this.GetParserDependencies(parser);
			}
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x0600455F RID: 17759 RVA: 0x000BC19C File Offset: 0x000BA39C
		internal override string LanguageName
		{
			get
			{
				TParser tparser = this.Parse();
				if (tparser != null)
				{
					return this.GetParserLanguage(tparser);
				}
				return base.LanguageName;
			}
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x000BC1C6 File Offset: 0x000BA3C6
		public override CompilerType CodeCompilerType
		{
			get
			{
				if (this._compilerType == null)
				{
					this._compilerType = base.GetDefaultCompilerTypeForLanguage(this.LanguageName);
				}
				return this._compilerType;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x000BC1E8 File Offset: 0x000BA3E8
		public TParser Parser
		{
			get
			{
				if (this._parser == null)
				{
					VirtualPath virtualPathInternal = base.VirtualPathInternal;
					if (virtualPathInternal == null)
					{
						throw new HttpException("VirtualPath not set, cannot instantiate parser.");
					}
					if (!this.IsDirectoryBuilder)
					{
						string text;
						this._reader = this.SpecialOpenReader(virtualPathInternal, out text);
						this._parser = this.CreateParser(virtualPathInternal, text, this._reader, HttpContext.Current);
					}
					else
					{
						this._parser = this.CreateParser(virtualPathInternal, null, HttpContext.Current);
					}
					if (this._parser == null)
					{
						throw new HttpException("Unable to create type parser.");
					}
				}
				return this._parser;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool IsDirectoryBuilder
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06004563 RID: 17763 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected virtual bool NeedsConstructType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06004564 RID: 17764 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool NeedsLoadFromBin
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06004565 RID: 17765 RVA: 0x000BC27A File Offset: 0x000BA47A
		internal override CodeCompileUnit CodeUnit
		{
			get
			{
				if (!this._codeGenerated)
				{
					this.GenerateCode();
				}
				return this._compiler.CompileUnit;
			}
		}

		// Token: 0x040024F0 RID: 9456
		private TParser _parser;

		// Token: 0x040024F1 RID: 9457
		private CompilerType _compilerType;

		// Token: 0x040024F2 RID: 9458
		private BaseCompiler _compiler;

		// Token: 0x040024F3 RID: 9459
		private TextReader _reader;

		// Token: 0x040024F4 RID: 9460
		private bool _parsed;

		// Token: 0x040024F5 RID: 9461
		private bool _codeGenerated;
	}
}
