using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000665 RID: 1637
	internal abstract class SimpleBuildProvider : GenericBuildProvider<SimpleWebHandlerParser>
	{
		// Token: 0x060045FE RID: 17918 RVA: 0x000BEB0C File Offset: 0x000BCD0C
		protected override SimpleWebHandlerParser Parse()
		{
			SimpleWebHandlerParser parser = base.Parser;
			if (this._parsed)
			{
				return parser;
			}
			this._parsed = true;
			return parser;
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x000BEB34 File Offset: 0x000BCD34
		protected override void GenerateCode(AssemblyBuilder assemblyBuilder, SimpleWebHandlerParser parser, BaseCompiler compiler)
		{
			if (assemblyBuilder == null || parser == null)
			{
				return;
			}
			string text = parser.Program.Trim();
			if (string.IsNullOrEmpty(text))
			{
				this._needLoadFromBin = true;
				return;
			}
			this._needLoadFromBin = false;
			using (TextWriter textWriter = assemblyBuilder.CreateCodeFile(this))
			{
				textWriter.WriteLine(text);
			}
		}

		// Token: 0x06004600 RID: 17920 RVA: 0x000BEB98 File Offset: 0x000BCD98
		protected override Type LoadTypeFromBin(BaseCompiler compiler, SimpleWebHandlerParser parser)
		{
			return parser.GetTypeFromBin(parser.ClassName);
		}

		// Token: 0x06004601 RID: 17921 RVA: 0x000BEBA6 File Offset: 0x000BCDA6
		protected override string GetClassType(BaseCompiler compiler, SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.ClassName;
			}
			return null;
		}

		// Token: 0x06004602 RID: 17922 RVA: 0x000BEBB3 File Offset: 0x000BCDB3
		protected override ICollection GetParserDependencies(SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.Dependencies;
			}
			return null;
		}

		// Token: 0x06004603 RID: 17923 RVA: 0x000BEBC0 File Offset: 0x000BCDC0
		protected override string GetParserLanguage(SimpleWebHandlerParser parser)
		{
			if (parser != null)
			{
				return parser.Language;
			}
			return null;
		}

		// Token: 0x06004604 RID: 17924 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected override string GetCodeBehindSource(SimpleWebHandlerParser parser)
		{
			return null;
		}

		// Token: 0x06004605 RID: 17925 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected override AspGenerator CreateAspGenerator(SimpleWebHandlerParser parser)
		{
			return null;
		}

		// Token: 0x06004606 RID: 17926 RVA: 0x000BEBCD File Offset: 0x000BCDCD
		protected override BaseCompiler CreateCompiler(SimpleWebHandlerParser parser)
		{
			return new WebServiceCompiler(parser);
		}

		// Token: 0x06004607 RID: 17927 RVA: 0x000BEBD8 File Offset: 0x000BCDD8
		protected override List<string> GetReferencedAssemblies(SimpleWebHandlerParser parser)
		{
			if (parser == null)
			{
				return null;
			}
			ArrayList assemblies = parser.Assemblies;
			if (assemblies == null || assemblies.Count == 0)
			{
				return null;
			}
			List<string> list = new List<string>();
			foreach (object obj in assemblies)
			{
				string text = obj as string;
				if (text != null && !list.Contains(text))
				{
					list.Add(text);
				}
			}
			return list;
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06004608 RID: 17928 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool NeedsConstructType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06004609 RID: 17929 RVA: 0x000BEC5C File Offset: 0x000BCE5C
		protected override bool NeedsLoadFromBin
		{
			get
			{
				return this._needLoadFromBin;
			}
		}

		// Token: 0x04002525 RID: 9509
		private bool _parsed;

		// Token: 0x04002526 RID: 9510
		private bool _needLoadFromBin;
	}
}
