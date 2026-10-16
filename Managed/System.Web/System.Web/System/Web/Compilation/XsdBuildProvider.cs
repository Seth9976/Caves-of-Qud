using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Data.Design;
using System.IO;

namespace System.Web.Compilation
{
	// Token: 0x02000671 RID: 1649
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Code)]
	internal sealed class XsdBuildProvider : BuildProvider
	{
		// Token: 0x0600469A RID: 18074 RVA: 0x000C41A4 File Offset: 0x000C23A4
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			CodeCompileUnit codeCompileUnit = new CodeCompileUnit();
			CodeNamespace codeNamespace = new CodeNamespace(null);
			codeCompileUnit.Namespaces.Add(codeNamespace);
			TextReader textReader = new StreamReader(HttpContext.Current.Request.MapPath(base.VirtualPath));
			CodeDomProvider codeDomProvider = assemblyBuilder.CodeDomProvider;
			if (codeDomProvider == null)
			{
				throw new HttpException("Assembly builder has no code provider");
			}
			TypedDataSetGenerator.Generate(textReader.ReadToEnd(), codeCompileUnit, codeNamespace, codeDomProvider);
			assemblyBuilder.AddCodeCompileUnit(codeCompileUnit);
		}
	}
}
