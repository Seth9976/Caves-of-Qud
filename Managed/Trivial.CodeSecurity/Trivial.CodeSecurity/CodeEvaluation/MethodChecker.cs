using System;
using Trivial.Mono.Cecil;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Collections.Generic;

namespace Trivial.CodeSecurity.CodeEvaluation
{
	// Token: 0x02000018 RID: 24
	internal class MethodChecker : CodeChecker<MethodDefinition>, ICodeUsageProvider
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004063 File Offset: 0x00002263
		public MethodChecker()
		{
			this.fieldReferenceChecker = new FieldReferenceChecker(this);
			this.methodReferenceChecker = new MethodReferenceChecker(this);
			this.typeReferenceChecker = new TypeReferenceChecker(this);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004090 File Offset: 0x00002290
		public override void SecurityCheckCode(CodeSecurityContext context, MethodDefinition methodDefinition)
		{
			this.currentMethod = methodDefinition;
			this.currentMethodBody = null;
			this.currentInstruction = null;
			this.methodReferenceChecker.SecurityCheckCodeMethodReturn(context, methodDefinition.ReturnType);
			if (methodDefinition.HasParameters)
			{
				this.methodReferenceChecker.SecurityCheckCodeMethodParameters(context, methodDefinition.Parameters);
			}
			if (methodDefinition.HasBody)
			{
				this.SecurityCheckCodeMethodBody(context, methodDefinition.Body);
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000040F4 File Offset: 0x000022F4
		private void SecurityCheckCodeMethodBody(CodeSecurityContext context, MethodBody body)
		{
			this.currentMethodBody = body;
			if (body.HasVariables)
			{
				this.SecurityCheckCodeMethodLocals(context, body.Variables);
			}
			foreach (Instruction instruction in body.Instructions)
			{
				this.currentInstruction = instruction;
				this.SecurityCheckCodeInstruction(context, body, instruction);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000416C File Offset: 0x0000236C
		private void SecurityCheckCodeMethodLocals(CodeSecurityContext context, Collection<VariableDefinition> variables)
		{
			foreach (VariableDefinition variableDefinition in variables)
			{
				this.typeReferenceChecker.SecurityCheckCode(context, variableDefinition.VariableType);
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000041C8 File Offset: 0x000023C8
		private void SecurityCheckCodeInstruction(CodeSecurityContext context, MethodBody body, Instruction instruction)
		{
			OperandType operandType = instruction.OpCode.OperandType;
			if (operandType == OperandType.InlineField)
			{
				this.typeReferenceChecker.SecurityCheckCode(context, (instruction.Operand as FieldReference).DeclaringType);
				this.fieldReferenceChecker.SecurityCheckCode(context, instruction.Operand as FieldReference);
				return;
			}
			if (operandType != OperandType.InlineMethod)
			{
				switch (operandType)
				{
				case OperandType.InlineType:
					this.typeReferenceChecker.SecurityCheckCode(context, instruction.Operand as TypeReference);
					return;
				case OperandType.InlineVar:
				case OperandType.ShortInlineVar:
					this.typeReferenceChecker.SecurityCheckCode(context, (instruction.Operand as VariableReference).VariableType);
					break;
				case OperandType.InlineArg:
					this.typeReferenceChecker.SecurityCheckCode(context, (instruction.Operand as ParameterReference).ParameterType);
					return;
				case OperandType.ShortInlineBrTarget:
				case OperandType.ShortInlineI:
				case OperandType.ShortInlineR:
					break;
				default:
					return;
				}
				return;
			}
			this.typeReferenceChecker.SecurityCheckCode(context, (instruction.Operand as MethodReference).DeclaringType);
			this.methodReferenceChecker.SecurityCheckCode(context, instruction.Operand as MethodReference);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000042CB File Offset: 0x000024CB
		public IllegalReferenceUsage GetIllegalUsage()
		{
			if (this.currentMethodBody != null && this.currentInstruction != null)
			{
				return new IllegalReferenceUsage(this.currentMethodBody, this.currentInstruction);
			}
			return new IllegalReferenceUsage(this.currentMethod);
		}

		// Token: 0x04000047 RID: 71
		private MethodDefinition currentMethod;

		// Token: 0x04000048 RID: 72
		private MethodBody currentMethodBody;

		// Token: 0x04000049 RID: 73
		private Instruction currentInstruction;

		// Token: 0x0400004A RID: 74
		private FieldReferenceChecker fieldReferenceChecker;

		// Token: 0x0400004B RID: 75
		private MethodReferenceChecker methodReferenceChecker;

		// Token: 0x0400004C RID: 76
		private TypeReferenceChecker typeReferenceChecker;
	}
}
