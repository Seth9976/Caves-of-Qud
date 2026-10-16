using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x0200001A RID: 26
	internal class ModuleChecker
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x000043C0 File Offset: 0x000025C0
		public void SecurityCheckModuleReferences(CodeSecurityContext context, ModuleDefinition module)
		{
			foreach (AssemblyNameReference assemblyNameReference in module.AssemblyReferences)
			{
				if (!context.Validator.IsAssemblyReferenceAllowed(assemblyNameReference.Name))
				{
					context.Report.AddIllegalAssemblyReferenceOccurence(assemblyNameReference, module);
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000442C File Offset: 0x0000262C
		public void SecurityCheckModuleCode(CodeSecurityContext context, ModuleDefinition module)
		{
			foreach (TypeDefinition typeDefinition in module.Types)
			{
				this.typeChecker.SecurityCheckCode(context, typeDefinition);
			}
		}

		// Token: 0x0400004F RID: 79
		private TypeChecker typeChecker = new TypeChecker();
	}
}
