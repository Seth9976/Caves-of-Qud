using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x0200001B RID: 27
	internal class PropertyChecker : CodeChecker<PropertyDefinition>, ICodeUsageProvider
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x0000449B File Offset: 0x0000269B
		public PropertyChecker()
		{
			this.typeReferenceChecker = new TypeReferenceChecker(this);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000044BC File Offset: 0x000026BC
		public override void SecurityCheckCode(CodeSecurityContext context, PropertyDefinition propertyDefinition)
		{
			this.currentProperty = propertyDefinition;
			this.typeReferenceChecker.SecurityCheckCode(context, propertyDefinition.PropertyType);
			base.SecurityCheckAndReportMemberReference(context, propertyDefinition, this);
			if (propertyDefinition.GetMethod != null)
			{
				this.accessorChecker.SecurityCheckCode(context, propertyDefinition.GetMethod);
			}
			if (propertyDefinition.SetMethod != null)
			{
				this.accessorChecker.SecurityCheckCode(context, propertyDefinition.SetMethod);
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000451F File Offset: 0x0000271F
		public IllegalReferenceUsage GetIllegalUsage()
		{
			return new IllegalReferenceUsage(this.currentProperty);
		}

		// Token: 0x04000050 RID: 80
		private PropertyDefinition currentProperty;

		// Token: 0x04000051 RID: 81
		private TypeReferenceChecker typeReferenceChecker;

		// Token: 0x04000052 RID: 82
		private MethodChecker accessorChecker = new MethodChecker();
	}
}
