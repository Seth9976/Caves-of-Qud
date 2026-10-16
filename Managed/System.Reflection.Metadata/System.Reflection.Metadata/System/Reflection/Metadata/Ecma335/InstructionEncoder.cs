using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Encodes Common Intermediate Language (CIL) instructions.</summary>
	// Token: 0x020000EA RID: 234
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct InstructionEncoder
	{
		/// <summary>Underlying builder where encoded instructions are written to.</summary>
		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x00019724 File Offset: 0x00017924
		public BlobBuilder CodeBuilder { get; }

		/// <summary>Builder tracking labels, branches and exception handlers.</summary>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0001972C File Offset: 0x0001792C
		[Nullable(2)]
		public ControlFlowBuilder ControlFlowBuilder
		{
			[NullableContext(2)]
			get;
		}

		/// <summary>Creates an encoder backed by code and control-flow builders.</summary>
		/// <param name="codeBuilder">Builder to write encoded instructions to.</param>
		/// <param name="controlFlowBuilder">Builder tracking labels, branches and exception handlers.
		///  Must be specified to be able to use some of the control-flow factory methods of <see cref="T:System.Reflection.Metadata.Ecma335.InstructionEncoder" />, such as <see cref="M:System.Reflection.Metadata.Ecma335.InstructionEncoder.Branch(System.Reflection.Metadata.ILOpCode,System.Reflection.Metadata.Ecma335.LabelHandle)" />, <see cref="M:System.Reflection.Metadata.Ecma335.InstructionEncoder.DefineLabel" />, <see cref="M:System.Reflection.Metadata.Ecma335.InstructionEncoder.MarkLabel(System.Reflection.Metadata.Ecma335.LabelHandle)" /> etc.</param>
		// Token: 0x060008E8 RID: 2280 RVA: 0x00019734 File Offset: 0x00017934
		public InstructionEncoder(BlobBuilder codeBuilder, [Nullable(2)] ControlFlowBuilder controlFlowBuilder = null)
		{
			if (codeBuilder == null)
			{
				Throw.BuilderArgumentNull();
			}
			this.CodeBuilder = codeBuilder;
			this.ControlFlowBuilder = controlFlowBuilder;
		}

		/// <summary>Offset of the next encoded instruction.</summary>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0001974C File Offset: 0x0001794C
		public int Offset
		{
			get
			{
				return this.CodeBuilder.Count;
			}
		}

		/// <summary>Encodes specified op-code.</summary>
		/// <param name="code" />
		// Token: 0x060008EA RID: 2282 RVA: 0x00019759 File Offset: 0x00017959
		public void OpCode(ILOpCode code)
		{
			if ((ILOpCode)((byte)code) == code)
			{
				this.CodeBuilder.WriteByte((byte)code);
				return;
			}
			this.CodeBuilder.WriteUInt16BE((ushort)code);
		}

		/// <summary>Encodes a token.</summary>
		/// <param name="handle" />
		// Token: 0x060008EB RID: 2283 RVA: 0x0001977A File Offset: 0x0001797A
		public void Token(EntityHandle handle)
		{
			this.Token(MetadataTokens.GetToken(handle));
		}

		/// <summary>Encodes a token.</summary>
		/// <param name="token" />
		// Token: 0x060008EC RID: 2284 RVA: 0x00019788 File Offset: 0x00017988
		public void Token(int token)
		{
			this.CodeBuilder.WriteInt32(token);
		}

		/// <summary>Encodes <c>ldstr</c> instruction and its operand.</summary>
		/// <param name="handle" />
		// Token: 0x060008ED RID: 2285 RVA: 0x00019796 File Offset: 0x00017996
		public void LoadString(UserStringHandle handle)
		{
			this.OpCode(ILOpCode.Ldstr);
			this.Token(MetadataTokens.GetToken(handle));
		}

		/// <summary>Encodes <c>call</c> instruction and its operand.</summary>
		/// <param name="methodHandle" />
		// Token: 0x060008EE RID: 2286 RVA: 0x000197B1 File Offset: 0x000179B1
		public void Call(EntityHandle methodHandle)
		{
			if (methodHandle.Kind != HandleKind.MethodDefinition && methodHandle.Kind != HandleKind.MethodSpecification && methodHandle.Kind != HandleKind.MemberReference)
			{
				Throw.InvalidArgument_Handle("methodHandle");
			}
			this.OpCode(ILOpCode.Call);
			this.Token(methodHandle);
		}

		/// <summary>Encodes <c>call</c> instruction and its operand.</summary>
		/// <param name="methodHandle" />
		// Token: 0x060008EF RID: 2287 RVA: 0x000197ED File Offset: 0x000179ED
		public void Call(MethodDefinitionHandle methodHandle)
		{
			this.OpCode(ILOpCode.Call);
			this.Token(methodHandle);
		}

		/// <summary>Encodes <c>call</c> instruction and its operand.</summary>
		/// <param name="methodHandle" />
		// Token: 0x060008F0 RID: 2288 RVA: 0x00019803 File Offset: 0x00017A03
		public void Call(MethodSpecificationHandle methodHandle)
		{
			this.OpCode(ILOpCode.Call);
			this.Token(methodHandle);
		}

		/// <summary>Encodes <c>call</c> instruction and its operand.</summary>
		/// <param name="methodHandle" />
		// Token: 0x060008F1 RID: 2289 RVA: 0x00019819 File Offset: 0x00017A19
		public void Call(MemberReferenceHandle methodHandle)
		{
			this.OpCode(ILOpCode.Call);
			this.Token(methodHandle);
		}

		/// <summary>Encodes <c>calli</c> instruction and its operand.</summary>
		/// <param name="signature" />
		// Token: 0x060008F2 RID: 2290 RVA: 0x0001982F File Offset: 0x00017A2F
		public void CallIndirect(StandaloneSignatureHandle signature)
		{
			this.OpCode(ILOpCode.Calli);
			this.Token(signature);
		}

		/// <summary>Encodes <see cref="T:System.Int32" /> constant load instruction.</summary>
		/// <param name="value" />
		// Token: 0x060008F3 RID: 2291 RVA: 0x00019848 File Offset: 0x00017A48
		public void LoadConstantI4(int value)
		{
			ILOpCode ilopCode;
			switch (value)
			{
			case -1:
				ilopCode = ILOpCode.Ldc_i4_m1;
				break;
			case 0:
				ilopCode = ILOpCode.Ldc_i4_0;
				break;
			case 1:
				ilopCode = ILOpCode.Ldc_i4_1;
				break;
			case 2:
				ilopCode = ILOpCode.Ldc_i4_2;
				break;
			case 3:
				ilopCode = ILOpCode.Ldc_i4_3;
				break;
			case 4:
				ilopCode = ILOpCode.Ldc_i4_4;
				break;
			case 5:
				ilopCode = ILOpCode.Ldc_i4_5;
				break;
			case 6:
				ilopCode = ILOpCode.Ldc_i4_6;
				break;
			case 7:
				ilopCode = ILOpCode.Ldc_i4_7;
				break;
			case 8:
				ilopCode = ILOpCode.Ldc_i4_8;
				break;
			default:
				if ((int)((sbyte)value) == value)
				{
					this.OpCode(ILOpCode.Ldc_i4_s);
					this.CodeBuilder.WriteSByte((sbyte)value);
					return;
				}
				this.OpCode(ILOpCode.Ldc_i4);
				this.CodeBuilder.WriteInt32(value);
				return;
			}
			this.OpCode(ilopCode);
		}

		/// <summary>Encodes <see cref="T:System.Int64" /> constant load instruction.</summary>
		/// <param name="value" />
		// Token: 0x060008F4 RID: 2292 RVA: 0x000198F0 File Offset: 0x00017AF0
		public void LoadConstantI8(long value)
		{
			this.OpCode(ILOpCode.Ldc_i8);
			this.CodeBuilder.WriteInt64(value);
		}

		/// <summary>Encodes <see cref="T:System.Single" /> constant load instruction.</summary>
		/// <param name="value" />
		// Token: 0x060008F5 RID: 2293 RVA: 0x00019906 File Offset: 0x00017B06
		public void LoadConstantR4(float value)
		{
			this.OpCode(ILOpCode.Ldc_r4);
			this.CodeBuilder.WriteSingle(value);
		}

		/// <summary>Encodes <see cref="T:System.Double" /> constant load instruction.</summary>
		/// <param name="value" />
		// Token: 0x060008F6 RID: 2294 RVA: 0x0001991C File Offset: 0x00017B1C
		public void LoadConstantR8(double value)
		{
			this.OpCode(ILOpCode.Ldc_r8);
			this.CodeBuilder.WriteDouble(value);
		}

		/// <summary>Encodes local variable load instruction.</summary>
		/// <param name="slotIndex">Index of the local variable slot.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="slotIndex" /> is negative.</exception>
		// Token: 0x060008F7 RID: 2295 RVA: 0x00019934 File Offset: 0x00017B34
		public void LoadLocal(int slotIndex)
		{
			switch (slotIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Ldloc_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Ldloc_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Ldloc_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Ldloc_3);
				return;
			default:
				if (slotIndex <= 255)
				{
					this.OpCode(ILOpCode.Ldloc_s);
					this.CodeBuilder.WriteByte((byte)slotIndex);
					return;
				}
				if (slotIndex > 0)
				{
					this.OpCode(ILOpCode.Ldloc);
					this.CodeBuilder.WriteInt32(slotIndex);
					return;
				}
				Throw.ArgumentOutOfRange("slotIndex");
				return;
			}
		}

		/// <summary>Encodes local variable store instruction.</summary>
		/// <param name="slotIndex">Index of the local variable slot.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="slotIndex" /> is negative.</exception>
		// Token: 0x060008F8 RID: 2296 RVA: 0x000199C0 File Offset: 0x00017BC0
		public void StoreLocal(int slotIndex)
		{
			switch (slotIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Stloc_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Stloc_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Stloc_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Stloc_3);
				return;
			default:
				if (slotIndex <= 255)
				{
					this.OpCode(ILOpCode.Stloc_s);
					this.CodeBuilder.WriteByte((byte)slotIndex);
					return;
				}
				if (slotIndex > 0)
				{
					this.OpCode(ILOpCode.Stloc);
					this.CodeBuilder.WriteInt32(slotIndex);
					return;
				}
				Throw.ArgumentOutOfRange("slotIndex");
				return;
			}
		}

		/// <summary>Encodes local variable address load instruction.</summary>
		/// <param name="slotIndex">Index of the local variable slot.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="slotIndex" /> is negative.</exception>
		// Token: 0x060008F9 RID: 2297 RVA: 0x00019A50 File Offset: 0x00017C50
		public void LoadLocalAddress(int slotIndex)
		{
			if (slotIndex <= 255)
			{
				this.OpCode(ILOpCode.Ldloca_s);
				this.CodeBuilder.WriteByte((byte)slotIndex);
				return;
			}
			if (slotIndex > 0)
			{
				this.OpCode(ILOpCode.Ldloca);
				this.CodeBuilder.WriteInt32(slotIndex);
				return;
			}
			Throw.ArgumentOutOfRange("slotIndex");
		}

		/// <summary>Encodes argument load instruction.</summary>
		/// <param name="argumentIndex">Index of the argument.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="argumentIndex" /> is negative.</exception>
		// Token: 0x060008FA RID: 2298 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public void LoadArgument(int argumentIndex)
		{
			switch (argumentIndex)
			{
			case 0:
				this.OpCode(ILOpCode.Ldarg_0);
				return;
			case 1:
				this.OpCode(ILOpCode.Ldarg_1);
				return;
			case 2:
				this.OpCode(ILOpCode.Ldarg_2);
				return;
			case 3:
				this.OpCode(ILOpCode.Ldarg_3);
				return;
			default:
				if (argumentIndex <= 255)
				{
					this.OpCode(ILOpCode.Ldarg_s);
					this.CodeBuilder.WriteByte((byte)argumentIndex);
					return;
				}
				if (argumentIndex > 0)
				{
					this.OpCode(ILOpCode.Ldarg);
					this.CodeBuilder.WriteInt32(argumentIndex);
					return;
				}
				Throw.ArgumentOutOfRange("argumentIndex");
				return;
			}
		}

		/// <summary>Encodes argument address load instruction.</summary>
		/// <param name="argumentIndex">Index of the argument.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="argumentIndex" /> is negative.</exception>
		// Token: 0x060008FB RID: 2299 RVA: 0x00019B30 File Offset: 0x00017D30
		public void LoadArgumentAddress(int argumentIndex)
		{
			if (argumentIndex <= 255)
			{
				this.OpCode(ILOpCode.Ldarga_s);
				this.CodeBuilder.WriteByte((byte)argumentIndex);
				return;
			}
			if (argumentIndex > 0)
			{
				this.OpCode(ILOpCode.Ldarga);
				this.CodeBuilder.WriteInt32(argumentIndex);
				return;
			}
			Throw.ArgumentOutOfRange("argumentIndex");
		}

		/// <summary>Encodes argument store instruction.</summary>
		/// <param name="argumentIndex">Index of the argument.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="argumentIndex" /> is negative.</exception>
		// Token: 0x060008FC RID: 2300 RVA: 0x00019B84 File Offset: 0x00017D84
		public void StoreArgument(int argumentIndex)
		{
			if (argumentIndex <= 255)
			{
				this.OpCode(ILOpCode.Starg_s);
				this.CodeBuilder.WriteByte((byte)argumentIndex);
				return;
			}
			if (argumentIndex > 0)
			{
				this.OpCode(ILOpCode.Starg);
				this.CodeBuilder.WriteInt32(argumentIndex);
				return;
			}
			Throw.ArgumentOutOfRange("argumentIndex");
		}

		/// <summary>Defines a label that can later be used to mark and refer to a location in the instruction stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Reflection.Metadata.Ecma335.InstructionEncoder.ControlFlowBuilder" /> is <see langword="null" />.</exception>
		/// <returns>Label handle.</returns>
		// Token: 0x060008FD RID: 2301 RVA: 0x00019BD5 File Offset: 0x00017DD5
		public LabelHandle DefineLabel()
		{
			return this.GetBranchBuilder().AddLabel();
		}

		/// <summary>Encodes a branch instruction.</summary>
		/// <param name="code">Branch instruction to encode.</param>
		/// <param name="label">Label of the target location in instruction stream.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="code" /> is not a branch instruction.
		///
		/// -or-
		///
		/// <paramref name="label" /> was not defined by this encoder.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Reflection.Metadata.Ecma335.InstructionEncoder.ControlFlowBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="label" /> has default value.</exception>
		// Token: 0x060008FE RID: 2302 RVA: 0x00019BE4 File Offset: 0x00017DE4
		public void Branch(ILOpCode code, LabelHandle label)
		{
			int branchOperandSize = code.GetBranchOperandSize();
			this.GetBranchBuilder().AddBranch(this.Offset, label, code);
			this.OpCode(code);
			if (branchOperandSize == 1)
			{
				this.CodeBuilder.WriteSByte(-1);
				return;
			}
			this.CodeBuilder.WriteInt32(-1);
		}

		/// <summary>Associates specified label with the current IL offset.</summary>
		/// <param name="label">Label to mark.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Reflection.Metadata.Ecma335.InstructionEncoder.ControlFlowBuilder" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="label" /> was not defined by this encoder.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="label" /> has default value.</exception>
		// Token: 0x060008FF RID: 2303 RVA: 0x00019C2F File Offset: 0x00017E2F
		public void MarkLabel(LabelHandle label)
		{
			this.GetBranchBuilder().MarkLabel(this.Offset, label);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00019C43 File Offset: 0x00017E43
		private ControlFlowBuilder GetBranchBuilder()
		{
			if (this.ControlFlowBuilder == null)
			{
				Throw.ControlFlowBuilderNotAvailable();
			}
			return this.ControlFlowBuilder;
		}
	}
}
