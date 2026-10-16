using System;
using Mono.Cecil.PE;
using Mono.Collections.Generic;

namespace Mono.Cecil.Cil
{
	// Token: 0x020002B2 RID: 690
	internal sealed class CodeReader : BinaryStreamReader
	{
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x00037613 File Offset: 0x00035813
		private int Offset
		{
			get
			{
				return base.Position - this.start;
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x00037622 File Offset: 0x00035822
		public CodeReader(MetadataReader reader)
			: base(reader.image.Stream.value)
		{
			this.reader = reader;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00037641 File Offset: 0x00035841
		public int MoveTo(MethodDefinition method)
		{
			this.method = method;
			this.reader.context = method;
			int position = base.Position;
			base.Position = (int)this.reader.image.ResolveVirtualAddress((uint)method.RVA);
			return position;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00037678 File Offset: 0x00035878
		public void MoveBackTo(int position)
		{
			this.reader.context = null;
			base.Position = position;
		}

		// Token: 0x06001134 RID: 4404 RVA: 0x00037690 File Offset: 0x00035890
		public MethodBody ReadMethodBody(MethodDefinition method)
		{
			int num = this.MoveTo(method);
			this.body = new MethodBody(method);
			this.ReadMethodBody();
			this.MoveBackTo(num);
			return this.body;
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x000376C4 File Offset: 0x000358C4
		public int ReadCodeSize(MethodDefinition method)
		{
			int num = this.MoveTo(method);
			int num2 = this.ReadCodeSize();
			this.MoveBackTo(num);
			return num2;
		}

		// Token: 0x06001136 RID: 4406 RVA: 0x000376E8 File Offset: 0x000358E8
		private int ReadCodeSize()
		{
			byte b = this.ReadByte();
			int num = (int)(b & 3);
			if (num == 2)
			{
				return b >> 2;
			}
			if (num != 3)
			{
				throw new InvalidOperationException();
			}
			base.Advance(3);
			return (int)this.ReadUInt32();
		}

		// Token: 0x06001137 RID: 4407 RVA: 0x00037724 File Offset: 0x00035924
		private void ReadMethodBody()
		{
			byte b = this.ReadByte();
			int num = (int)(b & 3);
			if (num != 2)
			{
				if (num != 3)
				{
					throw new InvalidOperationException();
				}
				base.Advance(-1);
				this.ReadFatMethod();
			}
			else
			{
				this.body.code_size = b >> 2;
				this.body.MaxStackSize = 8;
				this.ReadCode();
			}
			ISymbolReader symbol_reader = this.reader.module.symbol_reader;
			if (symbol_reader != null && this.method.debug_info == null)
			{
				this.method.debug_info = symbol_reader.Read(this.method);
			}
			if (this.method.debug_info != null)
			{
				this.ReadDebugInfo();
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x000377C8 File Offset: 0x000359C8
		private void ReadFatMethod()
		{
			ushort num = this.ReadUInt16();
			this.body.max_stack_size = (int)this.ReadUInt16();
			this.body.code_size = (int)this.ReadUInt32();
			this.body.local_var_token = new MetadataToken(this.ReadUInt32());
			this.body.init_locals = (num & 16) > 0;
			if (this.body.local_var_token.RID != 0U)
			{
				this.body.variables = this.ReadVariables(this.body.local_var_token);
			}
			this.ReadCode();
			if ((num & 8) != 0)
			{
				this.ReadSection();
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00037868 File Offset: 0x00035A68
		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
		{
			int position = this.reader.position;
			VariableDefinitionCollection variableDefinitionCollection = this.reader.ReadVariables(local_var_token, this.method);
			this.reader.position = position;
			return variableDefinitionCollection;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x000378A0 File Offset: 0x00035AA0
		private void ReadCode()
		{
			this.start = base.Position;
			int num = this.body.code_size;
			if (num < 0 || (long)base.Length <= (long)((ulong)(num + base.Position)))
			{
				num = 0;
			}
			int num2 = this.start + num;
			Collection<Instruction> collection = (this.body.instructions = new InstructionCollection(this.method, (num + 1) / 2));
			while (base.Position < num2)
			{
				int num3 = base.Position - this.start;
				OpCode opCode = this.ReadOpCode();
				Instruction instruction = new Instruction(num3, opCode);
				if (opCode.OperandType != OperandType.InlineNone)
				{
					instruction.operand = this.ReadOperand(instruction);
				}
				collection.Add(instruction);
			}
			this.ResolveBranches(collection);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x00037958 File Offset: 0x00035B58
		private OpCode ReadOpCode()
		{
			byte b = this.ReadByte();
			if (b == 254)
			{
				return OpCodes.TwoBytesOpCode[(int)this.ReadByte()];
			}
			return OpCodes.OneByteOpCode[(int)b];
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x00037990 File Offset: 0x00035B90
		private object ReadOperand(Instruction instruction)
		{
			switch (instruction.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
				return this.ReadInt32() + this.Offset;
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				return this.reader.LookupToken(this.ReadToken());
			case OperandType.InlineI:
				return this.ReadInt32();
			case OperandType.InlineI8:
				return this.ReadInt64();
			case OperandType.InlineR:
				return this.ReadDouble();
			case OperandType.InlineSig:
				return this.GetCallSite(this.ReadToken());
			case OperandType.InlineString:
				return this.GetString(this.ReadToken());
			case OperandType.InlineSwitch:
			{
				int num = this.ReadInt32();
				int num2 = this.Offset + 4 * num;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = num2 + this.ReadInt32();
				}
				return array;
			}
			case OperandType.InlineVar:
				return this.GetVariable((int)this.ReadUInt16());
			case OperandType.InlineArg:
				return this.GetParameter((int)this.ReadUInt16());
			case OperandType.ShortInlineBrTarget:
				return (int)this.ReadSByte() + this.Offset;
			case OperandType.ShortInlineI:
				if (instruction.opcode == OpCodes.Ldc_I4_S)
				{
					return this.ReadSByte();
				}
				return this.ReadByte();
			case OperandType.ShortInlineR:
				return this.ReadSingle();
			case OperandType.ShortInlineVar:
				return this.GetVariable((int)this.ReadByte());
			case OperandType.ShortInlineArg:
				return this.GetParameter((int)this.ReadByte());
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x00037B20 File Offset: 0x00035D20
		public string GetString(MetadataToken token)
		{
			return this.reader.image.UserStringHeap.Read(token.RID);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00037B3E File Offset: 0x00035D3E
		public ParameterDefinition GetParameter(int index)
		{
			return this.body.GetParameter(index);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00037B4C File Offset: 0x00035D4C
		public VariableDefinition GetVariable(int index)
		{
			return this.body.GetVariable(index);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00037B5A File Offset: 0x00035D5A
		public CallSite GetCallSite(MetadataToken token)
		{
			return this.reader.ReadCallSite(token);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00037B68 File Offset: 0x00035D68
		private void ResolveBranches(Collection<Instruction> instructions)
		{
			Instruction[] items = instructions.items;
			int size = instructions.size;
			int i = 0;
			while (i < size)
			{
				Instruction instruction = items[i];
				OperandType operandType = instruction.opcode.OperandType;
				if (operandType == OperandType.InlineBrTarget)
				{
					goto IL_0036;
				}
				if (operandType != OperandType.InlineSwitch)
				{
					if (operandType == OperandType.ShortInlineBrTarget)
					{
						goto IL_0036;
					}
				}
				else
				{
					int[] array = (int[])instruction.operand;
					Instruction[] array2 = new Instruction[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array2[j] = this.GetInstruction(array[j]);
					}
					instruction.operand = array2;
				}
				IL_0092:
				i++;
				continue;
				IL_0036:
				instruction.operand = this.GetInstruction((int)instruction.operand);
				goto IL_0092;
			}
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00037C12 File Offset: 0x00035E12
		private Instruction GetInstruction(int offset)
		{
			return CodeReader.GetInstruction(this.body.Instructions, offset);
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00037C28 File Offset: 0x00035E28
		private static Instruction GetInstruction(Collection<Instruction> instructions, int offset)
		{
			int size = instructions.size;
			Instruction[] items = instructions.items;
			if (offset < 0 || offset > items[size - 1].offset)
			{
				return null;
			}
			int i = 0;
			int num = size - 1;
			while (i <= num)
			{
				int num2 = i + (num - i) / 2;
				Instruction instruction = items[num2];
				int offset2 = instruction.offset;
				if (offset == offset2)
				{
					return instruction;
				}
				if (offset < offset2)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return null;
		}

		// Token: 0x06001144 RID: 4420 RVA: 0x00037C94 File Offset: 0x00035E94
		private void ReadSection()
		{
			base.Align(4);
			byte b = this.ReadByte();
			if ((b & 64) == 0)
			{
				this.ReadSmallSection();
			}
			else
			{
				this.ReadFatSection();
			}
			if ((b & 128) != 0)
			{
				this.ReadSection();
			}
		}

		// Token: 0x06001145 RID: 4421 RVA: 0x00037CC8 File Offset: 0x00035EC8
		private void ReadSmallSection()
		{
			int num = (int)(this.ReadByte() / 12);
			base.Advance(2);
			this.ReadExceptionHandlers(num, () => (int)this.ReadUInt16(), () => (int)this.ReadByte());
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x00037D08 File Offset: 0x00035F08
		private void ReadFatSection()
		{
			base.Advance(-1);
			int num = (this.ReadInt32() >> 8) / 24;
			this.ReadExceptionHandlers(num, new Func<int>(this.ReadInt32), new Func<int>(this.ReadInt32));
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x00037D4C File Offset: 0x00035F4C
		private void ReadExceptionHandlers(int count, Func<int> read_entry, Func<int> read_length)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandler exceptionHandler = new ExceptionHandler((ExceptionHandlerType)(read_entry() & 7));
				exceptionHandler.TryStart = this.GetInstruction(read_entry());
				exceptionHandler.TryEnd = this.GetInstruction(exceptionHandler.TryStart.Offset + read_length());
				exceptionHandler.HandlerStart = this.GetInstruction(read_entry());
				exceptionHandler.HandlerEnd = this.GetInstruction(exceptionHandler.HandlerStart.Offset + read_length());
				this.ReadExceptionHandlerSpecific(exceptionHandler);
				this.body.ExceptionHandlers.Add(exceptionHandler);
			}
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x00037DF4 File Offset: 0x00035FF4
		private void ReadExceptionHandlerSpecific(ExceptionHandler handler)
		{
			ExceptionHandlerType handlerType = handler.HandlerType;
			if (handlerType == ExceptionHandlerType.Catch)
			{
				handler.CatchType = (TypeReference)this.reader.LookupToken(this.ReadToken());
				return;
			}
			if (handlerType != ExceptionHandlerType.Filter)
			{
				base.Advance(4);
				return;
			}
			handler.FilterStart = this.GetInstruction(this.ReadInt32());
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x00037E48 File Offset: 0x00036048
		public MetadataToken ReadToken()
		{
			return new MetadataToken(this.ReadUInt32());
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x00037E58 File Offset: 0x00036058
		private void ReadDebugInfo()
		{
			if (this.method.debug_info.sequence_points != null)
			{
				this.ReadSequencePoints();
			}
			if (this.method.debug_info.scope != null)
			{
				this.ReadScope(this.method.debug_info.scope);
			}
			if (this.method.custom_infos != null)
			{
				this.ReadCustomDebugInformations(this.method);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x00037EC0 File Offset: 0x000360C0
		private void ReadCustomDebugInformations(MethodDefinition method)
		{
			Collection<CustomDebugInformation> custom_infos = method.custom_infos;
			for (int i = 0; i < custom_infos.Count; i++)
			{
				StateMachineScopeDebugInformation stateMachineScopeDebugInformation = custom_infos[i] as StateMachineScopeDebugInformation;
				if (stateMachineScopeDebugInformation != null)
				{
					this.ReadStateMachineScope(stateMachineScopeDebugInformation);
				}
				AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = custom_infos[i] as AsyncMethodBodyDebugInformation;
				if (asyncMethodBodyDebugInformation != null)
				{
					this.ReadAsyncMethodBody(asyncMethodBodyDebugInformation);
				}
			}
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x00037F14 File Offset: 0x00036114
		private void ReadAsyncMethodBody(AsyncMethodBodyDebugInformation async_method)
		{
			if (async_method.catch_handler.Offset > -1)
			{
				async_method.catch_handler = new InstructionOffset(this.GetInstruction(async_method.catch_handler.Offset));
			}
			if (!async_method.yields.IsNullOrEmpty<InstructionOffset>())
			{
				for (int i = 0; i < async_method.yields.Count; i++)
				{
					async_method.yields[i] = new InstructionOffset(this.GetInstruction(async_method.yields[i].Offset));
				}
			}
			if (!async_method.resumes.IsNullOrEmpty<InstructionOffset>())
			{
				for (int j = 0; j < async_method.resumes.Count; j++)
				{
					async_method.resumes[j] = new InstructionOffset(this.GetInstruction(async_method.resumes[j].Offset));
				}
			}
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x00037FE8 File Offset: 0x000361E8
		private void ReadStateMachineScope(StateMachineScopeDebugInformation state_machine_scope)
		{
			if (state_machine_scope.scopes.IsNullOrEmpty<StateMachineScope>())
			{
				return;
			}
			foreach (StateMachineScope stateMachineScope in state_machine_scope.scopes)
			{
				stateMachineScope.start = new InstructionOffset(this.GetInstruction(stateMachineScope.start.Offset));
				Instruction instruction = this.GetInstruction(stateMachineScope.end.Offset);
				stateMachineScope.end = ((instruction == null) ? default(InstructionOffset) : new InstructionOffset(instruction));
			}
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0003808C File Offset: 0x0003628C
		private void ReadSequencePoints()
		{
			MethodDebugInformation debug_info = this.method.debug_info;
			for (int i = 0; i < debug_info.sequence_points.Count; i++)
			{
				SequencePoint sequencePoint = debug_info.sequence_points[i];
				Instruction instruction = this.GetInstruction(sequencePoint.Offset);
				if (instruction != null)
				{
					sequencePoint.offset = new InstructionOffset(instruction);
				}
			}
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x000380E4 File Offset: 0x000362E4
		private void ReadScopes(Collection<ScopeDebugInformation> scopes)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				this.ReadScope(scopes[i]);
			}
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x00038110 File Offset: 0x00036310
		private void ReadScope(ScopeDebugInformation scope)
		{
			Instruction instruction = this.GetInstruction(scope.Start.Offset);
			if (instruction != null)
			{
				scope.Start = new InstructionOffset(instruction);
			}
			Instruction instruction2 = this.GetInstruction(scope.End.Offset);
			scope.End = ((instruction2 != null) ? new InstructionOffset(instruction2) : default(InstructionOffset));
			if (!scope.variables.IsNullOrEmpty<VariableDebugInformation>())
			{
				for (int i = 0; i < scope.variables.Count; i++)
				{
					VariableDebugInformation variableDebugInformation = scope.variables[i];
					VariableDefinition variable = this.GetVariable(variableDebugInformation.Index);
					if (variable != null)
					{
						variableDebugInformation.index = new VariableIndex(variable);
					}
				}
			}
			if (!scope.scopes.IsNullOrEmpty<ScopeDebugInformation>())
			{
				this.ReadScopes(scope.scopes);
			}
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x000381DC File Offset: 0x000363DC
		public ByteBuffer PatchRawMethodBody(MethodDefinition method, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
		{
			int num = this.MoveTo(method);
			ByteBuffer byteBuffer = new ByteBuffer();
			byte b = this.ReadByte();
			int num2 = (int)(b & 3);
			if (num2 != 2)
			{
				if (num2 != 3)
				{
					throw new NotSupportedException();
				}
				base.Advance(-1);
				this.PatchRawFatMethod(byteBuffer, writer, out code_size, out local_var_token);
			}
			else
			{
				byteBuffer.WriteByte(b);
				local_var_token = MetadataToken.Zero;
				code_size = b >> 2;
				this.PatchRawCode(byteBuffer, code_size, writer);
			}
			this.MoveBackTo(num);
			return byteBuffer;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x00038254 File Offset: 0x00036454
		private void PatchRawFatMethod(ByteBuffer buffer, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
		{
			ushort num = this.ReadUInt16();
			buffer.WriteUInt16(num);
			buffer.WriteUInt16(this.ReadUInt16());
			code_size = this.ReadInt32();
			buffer.WriteInt32(code_size);
			local_var_token = this.ReadToken();
			if (local_var_token.RID > 0U)
			{
				VariableDefinitionCollection variableDefinitionCollection = this.ReadVariables(local_var_token);
				buffer.WriteUInt32((variableDefinitionCollection != null) ? writer.GetStandAloneSignature(variableDefinitionCollection).ToUInt32() : 0U);
			}
			else
			{
				buffer.WriteUInt32(0U);
			}
			this.PatchRawCode(buffer, code_size, writer);
			if ((num & 8) != 0)
			{
				this.PatchRawSection(buffer, writer.metadata);
			}
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x000382F0 File Offset: 0x000364F0
		private void PatchRawCode(ByteBuffer buffer, int code_size, CodeWriter writer)
		{
			MetadataBuilder metadata = writer.metadata;
			buffer.WriteBytes(this.ReadBytes(code_size));
			int position = buffer.position;
			buffer.position -= code_size;
			while (buffer.position < position)
			{
				byte b = buffer.ReadByte();
				OpCode opCode;
				if (b != 254)
				{
					opCode = OpCodes.OneByteOpCode[(int)b];
				}
				else
				{
					byte b2 = buffer.ReadByte();
					opCode = OpCodes.TwoBytesOpCode[(int)b2];
				}
				switch (opCode.OperandType)
				{
				case OperandType.InlineBrTarget:
				case OperandType.InlineI:
				case OperandType.ShortInlineR:
					buffer.position += 4;
					break;
				case OperandType.InlineField:
				case OperandType.InlineMethod:
				case OperandType.InlineTok:
				case OperandType.InlineType:
				{
					IMetadataTokenProvider metadataTokenProvider = this.reader.LookupToken(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(metadata.LookupToken(metadataTokenProvider).ToUInt32());
					break;
				}
				case OperandType.InlineI8:
				case OperandType.InlineR:
					buffer.position += 8;
					break;
				case OperandType.InlineSig:
				{
					CallSite callSite = this.GetCallSite(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(writer.GetStandAloneSignature(callSite).ToUInt32());
					break;
				}
				case OperandType.InlineString:
				{
					string @string = this.GetString(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(new MetadataToken(TokenType.String, metadata.user_string_heap.GetStringIndex(@string)).ToUInt32());
					break;
				}
				case OperandType.InlineSwitch:
				{
					int num = buffer.ReadInt32();
					buffer.position += num * 4;
					break;
				}
				case OperandType.InlineVar:
				case OperandType.InlineArg:
					buffer.position += 2;
					break;
				case OperandType.ShortInlineBrTarget:
				case OperandType.ShortInlineI:
				case OperandType.ShortInlineVar:
				case OperandType.ShortInlineArg:
					buffer.position++;
					break;
				}
			}
		}

		// Token: 0x06001154 RID: 4436 RVA: 0x000384F8 File Offset: 0x000366F8
		private void PatchRawSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			int position = base.Position;
			base.Align(4);
			buffer.WriteBytes(base.Position - position);
			byte b = this.ReadByte();
			if ((b & 64) == 0)
			{
				buffer.WriteByte(b);
				this.PatchRawSmallSection(buffer, metadata);
			}
			else
			{
				this.PatchRawFatSection(buffer, metadata);
			}
			if ((b & 128) != 0)
			{
				this.PatchRawSection(buffer, metadata);
			}
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x00038558 File Offset: 0x00036758
		private void PatchRawSmallSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			byte b = this.ReadByte();
			buffer.WriteByte(b);
			base.Advance(2);
			buffer.WriteUInt16(0);
			int num = (int)(b / 12);
			this.PatchRawExceptionHandlers(buffer, metadata, num, false);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x00038590 File Offset: 0x00036790
		private void PatchRawFatSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			base.Advance(-1);
			int num = this.ReadInt32();
			buffer.WriteInt32(num);
			int num2 = (num >> 8) / 24;
			this.PatchRawExceptionHandlers(buffer, metadata, num2, true);
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x000385C4 File Offset: 0x000367C4
		private void PatchRawExceptionHandlers(ByteBuffer buffer, MetadataBuilder metadata, int count, bool fat_entry)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandlerType exceptionHandlerType;
				if (fat_entry)
				{
					uint num = this.ReadUInt32();
					exceptionHandlerType = (ExceptionHandlerType)(num & 7U);
					buffer.WriteUInt32(num);
				}
				else
				{
					ushort num2 = this.ReadUInt16();
					exceptionHandlerType = (ExceptionHandlerType)(num2 & 7);
					buffer.WriteUInt16(num2);
				}
				buffer.WriteBytes(this.ReadBytes(fat_entry ? 16 : 6));
				if (exceptionHandlerType == ExceptionHandlerType.Catch)
				{
					IMetadataTokenProvider metadataTokenProvider = this.reader.LookupToken(this.ReadToken());
					buffer.WriteUInt32(metadata.LookupToken(metadataTokenProvider).ToUInt32());
				}
				else
				{
					buffer.WriteUInt32(this.ReadUInt32());
				}
			}
		}

		// Token: 0x040007C0 RID: 1984
		internal readonly MetadataReader reader;

		// Token: 0x040007C1 RID: 1985
		private int start;

		// Token: 0x040007C2 RID: 1986
		private MethodDefinition method;

		// Token: 0x040007C3 RID: 1987
		private MethodBody body;
	}
}
