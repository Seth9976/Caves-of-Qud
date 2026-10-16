using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace MonoMod.Utils.Cil
{
	// Token: 0x0200045D RID: 1117
	public sealed class CecilILGenerator : ILGeneratorShim
	{
		// Token: 0x060017C4 RID: 6084 RVA: 0x00053608 File Offset: 0x00051808
		unsafe static CecilILGenerator()
		{
			FieldInfo[] fields = typeof(Mono.Cecil.Cil.OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			{
				Mono.Cecil.Cil.OpCode opCode = (Mono.Cecil.Cil.OpCode)fields[i].GetValue(null);
				CecilILGenerator._MCCOpCodes[opCode.Value] = opCode;
			}
			Label label = default(Label);
			*(int*)(&label) = -1;
			CecilILGenerator.NullLabel = label;
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x000536EC File Offset: 0x000518EC
		public CecilILGenerator(ILProcessor il)
		{
			this.IL = il;
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0005373D File Offset: 0x0005193D
		private Mono.Cecil.Cil.OpCode _(global::System.Reflection.Emit.OpCode opcode)
		{
			return CecilILGenerator._MCCOpCodes[opcode.Value];
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00053750 File Offset: 0x00051950
		private CecilILGenerator.LabelInfo _(Label handle)
		{
			CecilILGenerator.LabelInfo labelInfo;
			if (!this._LabelInfos.TryGetValue(handle, out labelInfo))
			{
				return null;
			}
			return labelInfo;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00053770 File Offset: 0x00051970
		private VariableDefinition _(LocalBuilder handle)
		{
			return this._Variables[handle];
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0005377E File Offset: 0x0005197E
		private TypeReference _(Type info)
		{
			return this.IL.Body.Method.Module.ImportReference(info);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0005379B File Offset: 0x0005199B
		private FieldReference _(FieldInfo info)
		{
			return this.IL.Body.Method.Module.ImportReference(info);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x000537B8 File Offset: 0x000519B8
		private MethodReference _(MethodBase info)
		{
			return this.IL.Body.Method.Module.ImportReference(info);
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060017CC RID: 6092 RVA: 0x000537D5 File Offset: 0x000519D5
		public override int ILOffset
		{
			get
			{
				return this._ILOffset;
			}
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x000537E0 File Offset: 0x000519E0
		private Instruction ProcessLabels(Instruction ins)
		{
			if (this._LabelsToMark.Count != 0)
			{
				foreach (CecilILGenerator.LabelInfo labelInfo in this._LabelsToMark)
				{
					foreach (Instruction instruction in labelInfo.Branches)
					{
						object operand = instruction.Operand;
						if (!(operand is Instruction))
						{
							Instruction[] array = operand as Instruction[];
							if (array != null)
							{
								for (int i = 0; i < array.Length; i++)
								{
									if (array[i] == labelInfo.Instruction)
									{
										array[i] = ins;
										break;
									}
								}
							}
						}
						else
						{
							instruction.Operand = ins;
						}
					}
					labelInfo.Emitted = true;
					labelInfo.Instruction = ins;
				}
				this._LabelsToMark.Clear();
			}
			if (this._ExceptionHandlersToMark.Count != 0)
			{
				foreach (CecilILGenerator.LabelledExceptionHandler labelledExceptionHandler in this._ExceptionHandlersToMark)
				{
					Collection<Mono.Cecil.Cil.ExceptionHandler> exceptionHandlers = this.IL.Body.ExceptionHandlers;
					Mono.Cecil.Cil.ExceptionHandler exceptionHandler = new Mono.Cecil.Cil.ExceptionHandler(labelledExceptionHandler.HandlerType);
					CecilILGenerator.LabelInfo labelInfo2 = this._(labelledExceptionHandler.TryStart);
					exceptionHandler.TryStart = ((labelInfo2 != null) ? labelInfo2.Instruction : null);
					CecilILGenerator.LabelInfo labelInfo3 = this._(labelledExceptionHandler.TryEnd);
					exceptionHandler.TryEnd = ((labelInfo3 != null) ? labelInfo3.Instruction : null);
					CecilILGenerator.LabelInfo labelInfo4 = this._(labelledExceptionHandler.HandlerStart);
					exceptionHandler.HandlerStart = ((labelInfo4 != null) ? labelInfo4.Instruction : null);
					CecilILGenerator.LabelInfo labelInfo5 = this._(labelledExceptionHandler.HandlerEnd);
					exceptionHandler.HandlerEnd = ((labelInfo5 != null) ? labelInfo5.Instruction : null);
					CecilILGenerator.LabelInfo labelInfo6 = this._(labelledExceptionHandler.FilterStart);
					exceptionHandler.FilterStart = ((labelInfo6 != null) ? labelInfo6.Instruction : null);
					exceptionHandler.CatchType = labelledExceptionHandler.ExceptionType;
					exceptionHandlers.Add(exceptionHandler);
				}
				this._ExceptionHandlersToMark.Clear();
			}
			return ins;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00053A10 File Offset: 0x00051C10
		public unsafe override Label DefineLabel()
		{
			Label label = default(Label);
			ref int ptr = ref *(int*)(&label);
			int num = this.labelCounter;
			this.labelCounter = num + 1;
			ptr = num;
			this._LabelInfos[label] = new CecilILGenerator.LabelInfo();
			return label;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00053A4C File Offset: 0x00051C4C
		public override void MarkLabel(Label loc)
		{
			CecilILGenerator.LabelInfo labelInfo;
			if (!this._LabelInfos.TryGetValue(loc, out labelInfo) || labelInfo.Emitted)
			{
				return;
			}
			this._LabelsToMark.Add(labelInfo);
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00053A7E File Offset: 0x00051C7E
		public override LocalBuilder DeclareLocal(Type type)
		{
			return this.DeclareLocal(type, false);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00053A88 File Offset: 0x00051C88
		public override LocalBuilder DeclareLocal(Type type, bool pinned)
		{
			int count = this.IL.Body.Variables.Count;
			object obj;
			if (CecilILGenerator.c_LocalBuilder_params != 4)
			{
				if (CecilILGenerator.c_LocalBuilder_params != 3)
				{
					if (CecilILGenerator.c_LocalBuilder_params != 2)
					{
						if (CecilILGenerator.c_LocalBuilder_params != 0)
						{
							throw new NotSupportedException();
						}
						obj = CecilILGenerator.c_LocalBuilder.Invoke(new object[0]);
					}
					else
					{
						ConstructorInfo constructorInfo = CecilILGenerator.c_LocalBuilder;
						object[] array = new object[2];
						array[0] = type;
						obj = constructorInfo.Invoke(array);
					}
				}
				else
				{
					ConstructorInfo constructorInfo2 = CecilILGenerator.c_LocalBuilder;
					object[] array2 = new object[3];
					array2[0] = count;
					array2[1] = type;
					obj = constructorInfo2.Invoke(array2);
				}
			}
			else
			{
				obj = CecilILGenerator.c_LocalBuilder.Invoke(new object[] { count, type, null, pinned });
			}
			LocalBuilder localBuilder = (LocalBuilder)obj;
			FieldInfo fieldInfo = CecilILGenerator.f_LocalBuilder_position;
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(localBuilder, (ushort)count);
			}
			FieldInfo fieldInfo2 = CecilILGenerator.f_LocalBuilder_is_pinned;
			if (fieldInfo2 != null)
			{
				fieldInfo2.SetValue(localBuilder, pinned);
			}
			TypeReference typeReference = this._(type);
			if (pinned)
			{
				typeReference = new PinnedType(typeReference);
			}
			VariableDefinition variableDefinition = new VariableDefinition(typeReference);
			this.IL.Body.Variables.Add(variableDefinition);
			this._Variables[localBuilder] = variableDefinition;
			return localBuilder;
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00053BAF File Offset: 0x00051DAF
		private void Emit(Instruction ins)
		{
			ins.Offset = this._ILOffset;
			this._ILOffset += ins.GetSize();
			this.IL.Append(this.ProcessLabels(ins));
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00053BE2 File Offset: 0x00051DE2
		public override void Emit(global::System.Reflection.Emit.OpCode opcode)
		{
			this.Emit(this.IL.Create(this._(opcode)));
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00053BFC File Offset: 0x00051DFC
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, byte arg)
		{
			if (opcode.OperandType == global::System.Reflection.Emit.OperandType.ShortInlineVar || opcode.OperandType == global::System.Reflection.Emit.OperandType.InlineVar)
			{
				this._EmitInlineVar(this._(opcode), (int)arg);
				return;
			}
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00053C3C File Offset: 0x00051E3C
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, sbyte arg)
		{
			if (opcode.OperandType == global::System.Reflection.Emit.OperandType.ShortInlineVar || opcode.OperandType == global::System.Reflection.Emit.OperandType.InlineVar)
			{
				this._EmitInlineVar(this._(opcode), (int)arg);
				return;
			}
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00053C7C File Offset: 0x00051E7C
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, short arg)
		{
			if (opcode.OperandType == global::System.Reflection.Emit.OperandType.ShortInlineVar || opcode.OperandType == global::System.Reflection.Emit.OperandType.InlineVar)
			{
				this._EmitInlineVar(this._(opcode), (int)arg);
				return;
			}
			this.Emit(this.IL.Create(this._(opcode), (int)arg));
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00053CBC File Offset: 0x00051EBC
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, int arg)
		{
			if (opcode.OperandType == global::System.Reflection.Emit.OperandType.ShortInlineVar || opcode.OperandType == global::System.Reflection.Emit.OperandType.InlineVar)
			{
				this._EmitInlineVar(this._(opcode), arg);
				return;
			}
			if (opcode.Name.EndsWith(".s", StringComparison.Ordinal))
			{
				this.Emit(this.IL.Create(this._(opcode), (sbyte)arg));
				return;
			}
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00053D36 File Offset: 0x00051F36
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, long arg)
		{
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00053D51 File Offset: 0x00051F51
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, float arg)
		{
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00053D6C File Offset: 0x00051F6C
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, double arg)
		{
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x00053D87 File Offset: 0x00051F87
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, string arg)
		{
			this.Emit(this.IL.Create(this._(opcode), arg));
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x00053DA2 File Offset: 0x00051FA2
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, Type arg)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(arg)));
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x00053DC3 File Offset: 0x00051FC3
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, FieldInfo arg)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(arg)));
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x00053DE4 File Offset: 0x00051FE4
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, ConstructorInfo arg)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(arg)));
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x00053DE4 File Offset: 0x00051FE4
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, MethodInfo arg)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(arg)));
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x00053E08 File Offset: 0x00052008
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, Label label)
		{
			CecilILGenerator.LabelInfo labelInfo = this._(label);
			Instruction instruction = this.IL.Create(this._(opcode), this._(label).Instruction);
			labelInfo.Branches.Add(instruction);
			this.Emit(this.ProcessLabels(instruction));
		}

		// Token: 0x060017E1 RID: 6113 RVA: 0x00053E54 File Offset: 0x00052054
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, Label[] labels)
		{
			IEnumerable<CecilILGenerator.LabelInfo> enumerable = labels.Distinct<Label>().Select<Label, CecilILGenerator.LabelInfo>(new Func<Label, CecilILGenerator.LabelInfo>(this._));
			Instruction instruction = this.IL.Create(this._(opcode), enumerable.Select<CecilILGenerator.LabelInfo, Instruction>((CecilILGenerator.LabelInfo labelInfo) => labelInfo.Instruction).ToArray<Instruction>());
			foreach (CecilILGenerator.LabelInfo labelInfo2 in enumerable)
			{
				labelInfo2.Branches.Add(instruction);
			}
			this.Emit(this.ProcessLabels(instruction));
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x00053F04 File Offset: 0x00052104
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, LocalBuilder local)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(local)));
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x00053F25 File Offset: 0x00052125
		public override void Emit(global::System.Reflection.Emit.OpCode opcode, SignatureHelper signature)
		{
			this.Emit(this.IL.Create(this._(opcode), this.IL.Body.Method.Module.ImportCallSite(signature)));
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x00053F5A File Offset: 0x0005215A
		public void Emit(global::System.Reflection.Emit.OpCode opcode, ICallSiteGenerator signature)
		{
			this.Emit(this.IL.Create(this._(opcode), this.IL.Body.Method.Module.ImportCallSite(signature)));
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x00053F90 File Offset: 0x00052190
		private void _EmitInlineVar(Mono.Cecil.Cil.OpCode opcode, int index)
		{
			switch (opcode.OperandType)
			{
			case Mono.Cecil.Cil.OperandType.InlineVar:
			case Mono.Cecil.Cil.OperandType.ShortInlineVar:
				this.Emit(this.IL.Create(opcode, this.IL.Body.Variables[index]));
				return;
			case Mono.Cecil.Cil.OperandType.InlineArg:
			case Mono.Cecil.Cil.OperandType.ShortInlineArg:
				this.Emit(this.IL.Create(opcode, this.IL.Body.Method.Parameters[index]));
				return;
			}
			throw new NotSupportedException(string.Format("Unsupported SRE InlineVar -> Cecil {0} for {1} {2}", opcode.OperandType, opcode, index));
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00053DE4 File Offset: 0x00051FE4
		public override void EmitCall(global::System.Reflection.Emit.OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			this.Emit(this.IL.Create(this._(opcode), this._(methodInfo)));
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00003B32 File Offset: 0x00001D32
		public override void EmitCalli(global::System.Reflection.Emit.OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00003B32 File Offset: 0x00001D32
		public override void EmitCalli(global::System.Reflection.Emit.OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0005404C File Offset: 0x0005224C
		public override void EmitWriteLine(FieldInfo field)
		{
			if (field.IsStatic)
			{
				this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Ldsfld, this._(field)));
			}
			else
			{
				this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Ldarg_0));
				this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Ldfld, this._(field)));
			}
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Call, this._(typeof(Console).GetMethod("WriteLine", new Type[] { field.FieldType }))));
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x000540F4 File Offset: 0x000522F4
		public override void EmitWriteLine(LocalBuilder localBuilder)
		{
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Ldloc, this._(localBuilder)));
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Call, this._(typeof(Console).GetMethod("WriteLine", new Type[] { localBuilder.LocalType }))));
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00054160 File Offset: 0x00052360
		public override void EmitWriteLine(string value)
		{
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Ldstr, value));
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Call, this._(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }))));
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x000541C7 File Offset: 0x000523C7
		public override void ThrowException(Type type)
		{
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Newobj, this._(type.GetConstructor(Type.EmptyTypes))));
			this.Emit(this.IL.Create(Mono.Cecil.Cil.OpCodes.Throw));
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x00054208 File Offset: 0x00052408
		public override Label BeginExceptionBlock()
		{
			CecilILGenerator.ExceptionHandlerChain exceptionHandlerChain = new CecilILGenerator.ExceptionHandlerChain(this);
			this._ExceptionHandlers.Push(exceptionHandlerChain);
			return exceptionHandlerChain.SkipAll;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0005422E File Offset: 0x0005242E
		public override void BeginCatchBlock(Type exceptionType)
		{
			this._ExceptionHandlers.Peek().BeginHandler(ExceptionHandlerType.Catch).ExceptionType = ((exceptionType == null) ? null : this._(exceptionType));
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x00054259 File Offset: 0x00052459
		public override void BeginExceptFilterBlock()
		{
			this._ExceptionHandlers.Peek().BeginHandler(ExceptionHandlerType.Filter);
		}

		// Token: 0x060017F0 RID: 6128 RVA: 0x0005426D File Offset: 0x0005246D
		public override void BeginFaultBlock()
		{
			this._ExceptionHandlers.Peek().BeginHandler(ExceptionHandlerType.Fault);
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x00054281 File Offset: 0x00052481
		public override void BeginFinallyBlock()
		{
			this._ExceptionHandlers.Peek().BeginHandler(ExceptionHandlerType.Finally);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00054295 File Offset: 0x00052495
		public override void EndExceptionBlock()
		{
			this._ExceptionHandlers.Pop().End();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00018659 File Offset: 0x00016859
		public override void BeginScope()
		{
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x00018659 File Offset: 0x00016859
		public override void EndScope()
		{
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x00018659 File Offset: 0x00016859
		public override void UsingNamespace(string usingNamespace)
		{
		}

		// Token: 0x04001070 RID: 4208
		private static readonly ConstructorInfo c_LocalBuilder = (from c in typeof(LocalBuilder).GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			orderby c.GetParameters().Length descending
			select c).First<ConstructorInfo>();

		// Token: 0x04001071 RID: 4209
		private static readonly FieldInfo f_LocalBuilder_position = typeof(LocalBuilder).GetField("position", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04001072 RID: 4210
		private static readonly FieldInfo f_LocalBuilder_is_pinned = typeof(LocalBuilder).GetField("is_pinned", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04001073 RID: 4211
		private static int c_LocalBuilder_params = CecilILGenerator.c_LocalBuilder.GetParameters().Length;

		// Token: 0x04001074 RID: 4212
		private static readonly Dictionary<short, Mono.Cecil.Cil.OpCode> _MCCOpCodes = new Dictionary<short, Mono.Cecil.Cil.OpCode>();

		// Token: 0x04001075 RID: 4213
		private static Label NullLabel;

		// Token: 0x04001076 RID: 4214
		public readonly ILProcessor IL;

		// Token: 0x04001077 RID: 4215
		private readonly Dictionary<Label, CecilILGenerator.LabelInfo> _LabelInfos = new Dictionary<Label, CecilILGenerator.LabelInfo>();

		// Token: 0x04001078 RID: 4216
		private readonly List<CecilILGenerator.LabelInfo> _LabelsToMark = new List<CecilILGenerator.LabelInfo>();

		// Token: 0x04001079 RID: 4217
		private readonly List<CecilILGenerator.LabelledExceptionHandler> _ExceptionHandlersToMark = new List<CecilILGenerator.LabelledExceptionHandler>();

		// Token: 0x0400107A RID: 4218
		private readonly Dictionary<LocalBuilder, VariableDefinition> _Variables = new Dictionary<LocalBuilder, VariableDefinition>();

		// Token: 0x0400107B RID: 4219
		private readonly Stack<CecilILGenerator.ExceptionHandlerChain> _ExceptionHandlers = new Stack<CecilILGenerator.ExceptionHandlerChain>();

		// Token: 0x0400107C RID: 4220
		private int labelCounter;

		// Token: 0x0400107D RID: 4221
		private int _ILOffset;

		// Token: 0x0200045E RID: 1118
		private class LabelInfo
		{
			// Token: 0x0400107E RID: 4222
			public bool Emitted;

			// Token: 0x0400107F RID: 4223
			public Instruction Instruction = Instruction.Create(Mono.Cecil.Cil.OpCodes.Nop);

			// Token: 0x04001080 RID: 4224
			public readonly List<Instruction> Branches = new List<Instruction>();
		}

		// Token: 0x0200045F RID: 1119
		private class LabelledExceptionHandler
		{
			// Token: 0x04001081 RID: 4225
			public Label TryStart = CecilILGenerator.NullLabel;

			// Token: 0x04001082 RID: 4226
			public Label TryEnd = CecilILGenerator.NullLabel;

			// Token: 0x04001083 RID: 4227
			public Label HandlerStart = CecilILGenerator.NullLabel;

			// Token: 0x04001084 RID: 4228
			public Label HandlerEnd = CecilILGenerator.NullLabel;

			// Token: 0x04001085 RID: 4229
			public Label FilterStart = CecilILGenerator.NullLabel;

			// Token: 0x04001086 RID: 4230
			public ExceptionHandlerType HandlerType;

			// Token: 0x04001087 RID: 4231
			public TypeReference ExceptionType;
		}

		// Token: 0x02000460 RID: 1120
		private class ExceptionHandlerChain
		{
			// Token: 0x060017F8 RID: 6136 RVA: 0x00054309 File Offset: 0x00052509
			public ExceptionHandlerChain(CecilILGenerator il)
			{
				this.IL = il;
				this._Start = il.DefineLabel();
				il.MarkLabel(this._Start);
				this.SkipAll = il.DefineLabel();
			}

			// Token: 0x060017F9 RID: 6137 RVA: 0x0005433C File Offset: 0x0005253C
			public CecilILGenerator.LabelledExceptionHandler BeginHandler(ExceptionHandlerType type)
			{
				CecilILGenerator.LabelledExceptionHandler labelledExceptionHandler = (this._Prev = this._Handler);
				if (labelledExceptionHandler != null)
				{
					this.EndHandler(labelledExceptionHandler);
				}
				this.IL.Emit(global::System.Reflection.Emit.OpCodes.Leave, this._SkipHandler = this.IL.DefineLabel());
				Label label = this.IL.DefineLabel();
				this.IL.MarkLabel(label);
				CecilILGenerator.LabelledExceptionHandler labelledExceptionHandler2 = new CecilILGenerator.LabelledExceptionHandler();
				labelledExceptionHandler2.TryStart = this._Start;
				labelledExceptionHandler2.TryEnd = label;
				labelledExceptionHandler2.HandlerType = type;
				labelledExceptionHandler2.HandlerEnd = this._SkipHandler;
				CecilILGenerator.LabelledExceptionHandler labelledExceptionHandler3 = labelledExceptionHandler2;
				this._Handler = labelledExceptionHandler2;
				CecilILGenerator.LabelledExceptionHandler labelledExceptionHandler4 = labelledExceptionHandler3;
				if (type == ExceptionHandlerType.Filter)
				{
					labelledExceptionHandler4.FilterStart = label;
				}
				else
				{
					labelledExceptionHandler4.HandlerStart = label;
				}
				return labelledExceptionHandler4;
			}

			// Token: 0x060017FA RID: 6138 RVA: 0x000543EC File Offset: 0x000525EC
			public void EndHandler(CecilILGenerator.LabelledExceptionHandler handler)
			{
				Label skipHandler = this._SkipHandler;
				ExceptionHandlerType handlerType = handler.HandlerType;
				if (handlerType != ExceptionHandlerType.Filter)
				{
					if (handlerType != ExceptionHandlerType.Finally)
					{
						this.IL.Emit(global::System.Reflection.Emit.OpCodes.Leave, skipHandler);
					}
					else
					{
						this.IL.Emit(global::System.Reflection.Emit.OpCodes.Endfinally);
					}
				}
				else
				{
					this.IL.Emit(global::System.Reflection.Emit.OpCodes.Endfilter);
				}
				this.IL.MarkLabel(skipHandler);
				this.IL._ExceptionHandlersToMark.Add(handler);
			}

			// Token: 0x060017FB RID: 6139 RVA: 0x00054463 File Offset: 0x00052663
			public void End()
			{
				this.EndHandler(this._Handler);
				this.IL.MarkLabel(this.SkipAll);
			}

			// Token: 0x04001088 RID: 4232
			private readonly CecilILGenerator IL;

			// Token: 0x04001089 RID: 4233
			private readonly Label _Start;

			// Token: 0x0400108A RID: 4234
			public readonly Label SkipAll;

			// Token: 0x0400108B RID: 4235
			private Label _SkipHandler;

			// Token: 0x0400108C RID: 4236
			private CecilILGenerator.LabelledExceptionHandler _Prev;

			// Token: 0x0400108D RID: 4237
			private CecilILGenerator.LabelledExceptionHandler _Handler;
		}
	}
}
