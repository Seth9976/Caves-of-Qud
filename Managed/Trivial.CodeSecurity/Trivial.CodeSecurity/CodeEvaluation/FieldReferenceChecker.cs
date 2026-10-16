using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000016 RID: 22
	internal class FieldReferenceChecker : CodeChecker<FieldReference>
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00004018 File Offset: 0x00002218
		public FieldReferenceChecker(ICodeUsageProvider usageProvider)
		{
			if (usageProvider == null)
			{
				throw new ArgumentNullException("usageProvider");
			}
			this.typeReferenceChecker = new TypeReferenceChecker(usageProvider);
			this.usageProvider = usageProvider;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004041 File Offset: 0x00002241
		public override void SecurityCheckCode(CodeSecurityContext context, FieldReference fieldReference)
		{
			this.typeReferenceChecker.SecurityCheckCode(context, fieldReference.FieldType);
			base.SecurityCheckAndReportMemberReference(context, fieldReference, this.usageProvider);
		}

		// Token: 0x04000045 RID: 69
		private TypeReferenceChecker typeReferenceChecker;

		// Token: 0x04000046 RID: 70
		private ICodeUsageProvider usageProvider;
	}
}
