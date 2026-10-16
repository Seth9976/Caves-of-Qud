using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	/// <summary>Provides an encoder for a method body stream.</summary>
	// Token: 0x020000DE RID: 222
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct MethodBodyStreamEncoder
	{
		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00013190 File Offset: 0x00011390
		public BlobBuilder Builder { get; }

		/// <param name="builder" />
		// Token: 0x06000803 RID: 2051 RVA: 0x00013198 File Offset: 0x00011398
		public MethodBodyStreamEncoder(BlobBuilder builder)
		{
			if (builder == null)
			{
				Throw.BuilderArgumentNull();
			}
			if (builder.Count % 4 != 0)
			{
				throw new ArgumentException(SR.BuilderMustAligned, "builder");
			}
			this.Builder = builder;
		}

		/// <summary>Encodes a method body and adds it to the method body stream, using the provided code size, maximum stack size, number of exception regions, local variables' signature handle, method body attributes and allowing to indicate whether the exception regions should be encoded in small format or not.</summary>
		/// <param name="codeSize">The number of bytes to be reserved for instructions.</param>
		/// <param name="maxStack">The maximum stack size.</param>
		/// <param name="exceptionRegionCount">The number of exception regions.</param>
		/// <param name="hasSmallExceptionRegions">
		///   <see langword="true" /> if the exception regions should be encoded in small format; <see langword="false" /> otherwise.</param>
		/// <param name="localVariablesSignature">The local variables' signature handle.</param>
		/// <param name="attributes">The method body attributes.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codeSize" />, <paramref name="exceptionRegionCount" />, or <paramref name="maxStack" /> is out of the allowed range.</exception>
		/// <returns>The offset of the encoded body within the method body stream.</returns>
		// Token: 0x06000804 RID: 2052 RVA: 0x000131C3 File Offset: 0x000113C3
		public MethodBodyStreamEncoder.MethodBody AddMethodBody(int codeSize, int maxStack, int exceptionRegionCount, bool hasSmallExceptionRegions, StandaloneSignatureHandle localVariablesSignature, MethodBodyAttributes attributes)
		{
			return this.AddMethodBody(codeSize, maxStack, exceptionRegionCount, hasSmallExceptionRegions, localVariablesSignature, attributes, false);
		}

		/// <summary>Encodes a method body and adds it to the method body stream, using the provided code size, maximum stack size, number of exception regions, local variables' signature handle, method body attributes, allowing to indicate whether the exception regions should be encoded in small format or not, and allowing to indicate whether the method should allocate from the dynamic local memory pool or not.</summary>
		/// <param name="codeSize">The number of bytes to be reserved for instructions.</param>
		/// <param name="maxStack">The maximum stack size.</param>
		/// <param name="exceptionRegionCount">The number of exception regions.</param>
		/// <param name="hasSmallExceptionRegions">
		///   <see langword="true" /> if the exception regions should be encoded in small format; <see langword="false" /> otherwise.</param>
		/// <param name="localVariablesSignature">The local variables' signature handle.</param>
		/// <param name="attributes">The method body attributes.</param>
		/// <param name="hasDynamicStackAllocation">
		///   <see langword="true" /> if the method allocates from the dynamic local memory pool (the <see langword="localloc" /> instruction); <see langword="false" /> otherwise.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="codeSize" />, <paramref name="exceptionRegionCount" />, or <paramref name="maxStack" /> is out of the allowed range.</exception>
		/// <returns>The offset of the encoded body within the method body stream.</returns>
		// Token: 0x06000805 RID: 2053 RVA: 0x000131D8 File Offset: 0x000113D8
		public MethodBodyStreamEncoder.MethodBody AddMethodBody(int codeSize, int maxStack = 8, int exceptionRegionCount = 0, bool hasSmallExceptionRegions = true, StandaloneSignatureHandle localVariablesSignature = default(StandaloneSignatureHandle), MethodBodyAttributes attributes = MethodBodyAttributes.InitLocals, bool hasDynamicStackAllocation = false)
		{
			if (codeSize < 0)
			{
				Throw.ArgumentOutOfRange("codeSize");
			}
			if (maxStack > 65535)
			{
				Throw.ArgumentOutOfRange("maxStack");
			}
			if (!ExceptionRegionEncoder.IsExceptionRegionCountInBounds(exceptionRegionCount))
			{
				Throw.ArgumentOutOfRange("exceptionRegionCount");
			}
			int num = this.SerializeHeader(codeSize, (ushort)maxStack, exceptionRegionCount, attributes, localVariablesSignature, hasDynamicStackAllocation);
			Blob blob = this.Builder.ReserveBytes(codeSize);
			ExceptionRegionEncoder exceptionRegionEncoder = ((exceptionRegionCount > 0) ? ExceptionRegionEncoder.SerializeTableHeader(this.Builder, exceptionRegionCount, hasSmallExceptionRegions) : default(ExceptionRegionEncoder));
			return new MethodBodyStreamEncoder.MethodBody(num, blob, exceptionRegionEncoder);
		}

		/// <summary>Encodes a method body and adds it to the method body stream.</summary>
		/// <param name="instructionEncoder">The instruction encoder.</param>
		/// <param name="maxStack">The maximum stack size.</param>
		/// <param name="localVariablesSignature">The local variables' signature handle.</param>
		/// <param name="attributes">The method body attributes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instructionEncoder" /> has default value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is out of range [0, <see cref="F:System.UInt16.MaxValue">UInt16.MaxValue</see>].</exception>
		/// <exception cref="T:System.InvalidOperationException">A label targeted by a branch in the instruction stream has not been marked, or the distance between a branch instruction and the target label doesn't fit the size of the instruction operand.</exception>
		/// <returns>The offset of the encoded body within the method body stream.</returns>
		// Token: 0x06000806 RID: 2054 RVA: 0x0001325B File Offset: 0x0001145B
		public int AddMethodBody(InstructionEncoder instructionEncoder, int maxStack, StandaloneSignatureHandle localVariablesSignature, MethodBodyAttributes attributes)
		{
			return this.AddMethodBody(instructionEncoder, maxStack, localVariablesSignature, attributes, false);
		}

		/// <summary>Encodes a method body and adds it to the method body stream, using the provided instruction encoder, maximum stack size, local variables' signature handle, method body attributes, and allowing to indicate whether the method should allocate from the dynamic local memory pool or not.</summary>
		/// <param name="instructionEncoder">The instruction encoder.</param>
		/// <param name="maxStack">The maximum stack size.</param>
		/// <param name="localVariablesSignature">The local variables' signature handle.</param>
		/// <param name="attributes">The method body attributes.</param>
		/// <param name="hasDynamicStackAllocation">
		///   <see langword="true" /> if the method allocates from the dynamic local memory pool (the IL contains the <see langword="localloc" /> instruction); <see langword="false" /> otherwise.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instructionEncoder" /> has default value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is out of range [0, <see cref="F:System.UInt16.MaxValue">UInt16.MaxValue</see>].</exception>
		/// <exception cref="T:System.InvalidOperationException">A label targeted by a branch in the instruction stream has not been marked, or the distance between a branch instruction and the target label doesn't fit the size of the instruction operand.</exception>
		/// <returns>The offset of the encoded body within the method body stream.</returns>
		// Token: 0x06000807 RID: 2055 RVA: 0x0001326C File Offset: 0x0001146C
		public int AddMethodBody(InstructionEncoder instructionEncoder, int maxStack = 8, StandaloneSignatureHandle localVariablesSignature = default(StandaloneSignatureHandle), MethodBodyAttributes attributes = MethodBodyAttributes.InitLocals, bool hasDynamicStackAllocation = false)
		{
			if (maxStack > 65535)
			{
				Throw.ArgumentOutOfRange("maxStack");
			}
			BlobBuilder codeBuilder = instructionEncoder.CodeBuilder;
			ControlFlowBuilder controlFlowBuilder = instructionEncoder.ControlFlowBuilder;
			if (codeBuilder == null)
			{
				Throw.ArgumentNull("instructionEncoder");
			}
			int num = ((controlFlowBuilder != null) ? controlFlowBuilder.ExceptionHandlerCount : 0);
			if (!ExceptionRegionEncoder.IsExceptionRegionCountInBounds(num))
			{
				Throw.ArgumentOutOfRange("instructionEncoder", SR.TooManyExceptionRegions);
			}
			int num2 = this.SerializeHeader(codeBuilder.Count, (ushort)maxStack, num, attributes, localVariablesSignature, hasDynamicStackAllocation);
			if (controlFlowBuilder != null && controlFlowBuilder.BranchCount > 0)
			{
				controlFlowBuilder.CopyCodeAndFixupBranches(codeBuilder, this.Builder);
			}
			else
			{
				codeBuilder.WriteContentTo(this.Builder);
			}
			if (controlFlowBuilder != null)
			{
				controlFlowBuilder.SerializeExceptionTable(this.Builder);
			}
			return num2;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00013318 File Offset: 0x00011518
		private int SerializeHeader(int codeSize, ushort maxStack, int exceptionRegionCount, MethodBodyAttributes attributes, StandaloneSignatureHandle localVariablesSignature, bool hasDynamicStackAllocation)
		{
			bool flag = (attributes & MethodBodyAttributes.InitLocals) > MethodBodyAttributes.None;
			bool flag2 = codeSize < 64 && maxStack <= 8 && localVariablesSignature.IsNil && (!hasDynamicStackAllocation || !flag) && exceptionRegionCount == 0;
			int num;
			if (flag2)
			{
				num = this.Builder.Count;
				this.Builder.WriteByte((byte)((codeSize << 2) | 2));
			}
			else
			{
				this.Builder.Align(4);
				num = this.Builder.Count;
				ushort num2 = 12291;
				if (exceptionRegionCount > 0)
				{
					num2 |= 8;
				}
				if (flag)
				{
					num2 |= 16;
				}
				this.Builder.WriteUInt16((ushort)(attributes | (MethodBodyAttributes)num2));
				this.Builder.WriteUInt16(maxStack);
				this.Builder.WriteInt32(codeSize);
				this.Builder.WriteInt32(localVariablesSignature.IsNil ? 0 : MetadataTokens.GetToken(localVariablesSignature));
			}
			return num;
		}

		/// <summary>Describes a method body. This class is meant to used along with the <see cref="T:System.Reflection.Metadata.Ecma335.MethodBodyStreamEncoder" /> class.</summary>
		// Token: 0x020001B8 RID: 440
		[NullableContext(0)]
		public readonly struct MethodBody
		{
			/// <summary>Gets the offset of the encoded method body in the method body stream.</summary>
			/// <returns>The offset of the encoded method body in the method body stream.</returns>
			// Token: 0x1700034F RID: 847
			// (get) Token: 0x06000D02 RID: 3330 RVA: 0x000263A3 File Offset: 0x000245A3
			public int Offset { get; }

			/// <summary>Gets a blob reserved for instructions.</summary>
			/// <returns>A blob reserved for instructions.</returns>
			// Token: 0x17000350 RID: 848
			// (get) Token: 0x06000D03 RID: 3331 RVA: 0x000263AB File Offset: 0x000245AB
			public Blob Instructions { get; }

			/// <summary>Gets an encoder object that can be used to encode exception regions to the method body.</summary>
			/// <returns>An exception region encoder instance.</returns>
			// Token: 0x17000351 RID: 849
			// (get) Token: 0x06000D04 RID: 3332 RVA: 0x000263B3 File Offset: 0x000245B3
			public ExceptionRegionEncoder ExceptionRegions { get; }

			// Token: 0x06000D05 RID: 3333 RVA: 0x000263BB File Offset: 0x000245BB
			internal MethodBody(int bodyOffset, Blob instructions, ExceptionRegionEncoder exceptionRegions)
			{
				this.Offset = bodyOffset;
				this.Instructions = instructions;
				this.ExceptionRegions = exceptionRegions;
			}
		}
	}
}
