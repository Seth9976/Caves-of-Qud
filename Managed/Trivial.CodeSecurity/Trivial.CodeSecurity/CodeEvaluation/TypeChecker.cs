using System;
using Trivial.Mono.Cecil;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x0200001C RID: 28
	internal class TypeChecker : CodeChecker<TypeDefinition>, ICodeUsageProvider
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x0000452C File Offset: 0x0000272C
		public TypeChecker()
		{
			this.typeReferenceChecker = new TypeReferenceChecker(this);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000456C File Offset: 0x0000276C
		public override void SecurityCheckCode(CodeSecurityContext context, TypeDefinition typeDefinition)
		{
			this.currentType = typeDefinition;
			if (typeDefinition.BaseType != null)
			{
				this.typeReferenceChecker.SecurityCheckCode(context, typeDefinition.BaseType);
			}
			foreach (InterfaceImplementation interfaceImplementation in typeDefinition.Interfaces)
			{
				this.typeReferenceChecker.SecurityCheckCode(context, interfaceImplementation.InterfaceType);
			}
			foreach (FieldDefinition fieldDefinition in typeDefinition.Fields)
			{
				this.fieldChecker.SecurityCheckCode(context, fieldDefinition);
			}
			foreach (PropertyDefinition propertyDefinition in typeDefinition.Properties)
			{
				this.properyChecker.SecurityCheckCode(context, propertyDefinition);
			}
			foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
			{
				this.methodChecker.SecurityCheckCode(context, methodDefinition);
			}
			foreach (EventDefinition eventDefinition in typeDefinition.Events)
			{
				this.eventChecker.SecurityCheckCode(context, eventDefinition);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004714 File Offset: 0x00002914
		public IllegalReferenceUsage GetIllegalUsage()
		{
			return new IllegalReferenceUsage(this.currentType);
		}

		// Token: 0x04000053 RID: 83
		private TypeDefinition currentType;

		// Token: 0x04000054 RID: 84
		private TypeReferenceChecker typeReferenceChecker;

		// Token: 0x04000055 RID: 85
		private FieldChecker fieldChecker = new FieldChecker();

		// Token: 0x04000056 RID: 86
		private PropertyChecker properyChecker = new PropertyChecker();

		// Token: 0x04000057 RID: 87
		private MethodChecker methodChecker = new MethodChecker();

		// Token: 0x04000058 RID: 88
		private EventChecker eventChecker = new EventChecker();
	}
}
