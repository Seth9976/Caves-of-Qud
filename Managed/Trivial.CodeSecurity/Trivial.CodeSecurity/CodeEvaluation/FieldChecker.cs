using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000015 RID: 21
	internal class FieldChecker : CodeChecker<FieldDefinition>, ICodeUsageProvider
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003FDC File Offset: 0x000021DC
		public FieldChecker()
		{
			this.typeReferenceChecker = new TypeReferenceChecker(this);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003FF0 File Offset: 0x000021F0
		public override void SecurityCheckCode(CodeSecurityContext context, FieldDefinition fieldDefinition)
		{
			this.currentField = fieldDefinition;
			this.typeReferenceChecker.SecurityCheckCode(context, fieldDefinition.FieldType);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000400B File Offset: 0x0000220B
		public IllegalReferenceUsage GetIllegalUsage()
		{
			return new IllegalReferenceUsage(this.currentField);
		}

		// Token: 0x04000043 RID: 67
		private FieldDefinition currentField;

		// Token: 0x04000044 RID: 68
		private TypeReferenceChecker typeReferenceChecker;
	}
}
