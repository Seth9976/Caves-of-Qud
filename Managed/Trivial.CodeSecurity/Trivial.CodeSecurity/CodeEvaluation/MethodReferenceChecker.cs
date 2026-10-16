using System;
using Trivial.Mono.Cecil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000019 RID: 25
	internal class MethodReferenceChecker : CodeChecker<MethodReference>
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000042FA File Offset: 0x000024FA
		public MethodReferenceChecker(ICodeUsageProvider usageProvider)
		{
			if (usageProvider == null)
			{
				throw new ArgumentNullException("usageProvider");
			}
			this.typeReferenceChecker = new TypeReferenceChecker(usageProvider);
			this.usageProvider = usageProvider;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004323 File Offset: 0x00002523
		public override void SecurityCheckCode(CodeSecurityContext context, MethodReference methodReference)
		{
			this.SecurityCheckCodeMethodReturn(context, methodReference.ReturnType);
			if (methodReference.HasParameters)
			{
				this.SecurityCheckCodeMethodParameters(context, methodReference.Parameters);
			}
			base.SecurityCheckAndReportMemberReference(context, methodReference, this.usageProvider);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004355 File Offset: 0x00002555
		public void SecurityCheckCodeMethodReturn(CodeSecurityContext context, TypeReference returnType)
		{
			this.typeReferenceChecker.SecurityCheckCode(context, returnType);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004364 File Offset: 0x00002564
		public void SecurityCheckCodeMethodParameters(CodeSecurityContext context, Collection<ParameterDefinition> parameters)
		{
			foreach (ParameterDefinition parameterDefinition in parameters)
			{
				this.typeReferenceChecker.SecurityCheckCode(context, parameterDefinition.ParameterType);
			}
		}

		// Token: 0x0400004D RID: 77
		private TypeReferenceChecker typeReferenceChecker;

		// Token: 0x0400004E RID: 78
		private ICodeUsageProvider usageProvider;
	}
}
