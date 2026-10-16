using System;
using Mono.Cecil.Cil;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000419 RID: 1049
	public interface IILVisitor
	{
		// Token: 0x06001619 RID: 5657
		void OnInlineNone(OpCode opcode);

		// Token: 0x0600161A RID: 5658
		void OnInlineSByte(OpCode opcode, sbyte value);

		// Token: 0x0600161B RID: 5659
		void OnInlineByte(OpCode opcode, byte value);

		// Token: 0x0600161C RID: 5660
		void OnInlineInt32(OpCode opcode, int value);

		// Token: 0x0600161D RID: 5661
		void OnInlineInt64(OpCode opcode, long value);

		// Token: 0x0600161E RID: 5662
		void OnInlineSingle(OpCode opcode, float value);

		// Token: 0x0600161F RID: 5663
		void OnInlineDouble(OpCode opcode, double value);

		// Token: 0x06001620 RID: 5664
		void OnInlineString(OpCode opcode, string value);

		// Token: 0x06001621 RID: 5665
		void OnInlineBranch(OpCode opcode, int offset);

		// Token: 0x06001622 RID: 5666
		void OnInlineSwitch(OpCode opcode, int[] offsets);

		// Token: 0x06001623 RID: 5667
		void OnInlineVariable(OpCode opcode, VariableDefinition variable);

		// Token: 0x06001624 RID: 5668
		void OnInlineArgument(OpCode opcode, ParameterDefinition parameter);

		// Token: 0x06001625 RID: 5669
		void OnInlineSignature(OpCode opcode, CallSite callSite);

		// Token: 0x06001626 RID: 5670
		void OnInlineType(OpCode opcode, TypeReference type);

		// Token: 0x06001627 RID: 5671
		void OnInlineField(OpCode opcode, FieldReference field);

		// Token: 0x06001628 RID: 5672
		void OnInlineMethod(OpCode opcode, MethodReference method);
	}
}
