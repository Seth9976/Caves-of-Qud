using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E9 RID: 233
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ExceptionRegionEncoder
	{
		/// <summary>The underlying builder.</summary>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000193F0 File Offset: 0x000175F0
		public BlobBuilder Builder { get; }

		/// <summary>
		///   <see langword="true" /> if the encoder uses small format.</summary>
		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x000193F8 File Offset: 0x000175F8
		public bool HasSmallFormat { get; }

		// Token: 0x060008D8 RID: 2264 RVA: 0x00019400 File Offset: 0x00017600
		internal ExceptionRegionEncoder(BlobBuilder builder, bool hasSmallFormat)
		{
			this.Builder = builder;
			this.HasSmallFormat = hasSmallFormat;
		}

		/// <summary>Returns <see langword="true" /> if the number of exception regions first small format.</summary>
		/// <param name="exceptionRegionCount">Number of exception regions.</param>
		// Token: 0x060008D9 RID: 2265 RVA: 0x00019410 File Offset: 0x00017610
		public static bool IsSmallRegionCount(int exceptionRegionCount)
		{
			return exceptionRegionCount <= 20;
		}

		/// <summary>Returns <see langword="true" /> if the region fits small format.</summary>
		/// <param name="startOffset">Start offset of the region.</param>
		/// <param name="length">Length of the region.</param>
		// Token: 0x060008DA RID: 2266 RVA: 0x0001941A File Offset: 0x0001761A
		public static bool IsSmallExceptionRegion(int startOffset, int length)
		{
			return startOffset <= 65535 && length <= 255;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00019431 File Offset: 0x00017631
		internal static bool IsSmallExceptionRegionFromBounds(int startOffset, int endOffset)
		{
			return ExceptionRegionEncoder.IsSmallExceptionRegion(startOffset, endOffset - startOffset);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0001943C File Offset: 0x0001763C
		internal static int GetExceptionTableSize(int exceptionRegionCount, bool isSmallFormat)
		{
			return 4 + exceptionRegionCount * (isSmallFormat ? 12 : 24);
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0001944B File Offset: 0x0001764B
		internal static bool IsExceptionRegionCountInBounds(int exceptionRegionCount)
		{
			return exceptionRegionCount <= 699050;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00019458 File Offset: 0x00017658
		internal static bool IsValidCatchTypeHandle(EntityHandle catchType)
		{
			return !catchType.IsNil && (catchType.Kind == HandleKind.TypeDefinition || catchType.Kind == HandleKind.TypeSpecification || catchType.Kind == HandleKind.TypeReference);
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00019488 File Offset: 0x00017688
		internal static ExceptionRegionEncoder SerializeTableHeader(BlobBuilder builder, int exceptionRegionCount, bool hasSmallRegions)
		{
			bool flag = hasSmallRegions && ExceptionRegionEncoder.IsSmallRegionCount(exceptionRegionCount);
			int exceptionTableSize = ExceptionRegionEncoder.GetExceptionTableSize(exceptionRegionCount, flag);
			builder.Align(4);
			if (flag)
			{
				builder.WriteByte(1);
				builder.WriteByte((byte)exceptionTableSize);
				builder.WriteInt16(0);
			}
			else
			{
				builder.WriteByte(65);
				builder.WriteByte((byte)exceptionTableSize);
				builder.WriteUInt16((ushort)(exceptionTableSize >> 8));
			}
			return new ExceptionRegionEncoder(builder, flag);
		}

		/// <summary>Adds a finally clause.</summary>
		/// <param name="tryOffset">Try block start offset.</param>
		/// <param name="tryLength">Try block length.</param>
		/// <param name="handlerOffset">Handler start offset.</param>
		/// <param name="handlerLength">Handler length.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="tryLength" />, <paramref name="handlerOffset" /> or <paramref name="handlerLength" /> is out of range.</exception>
		/// <exception cref="T:System.InvalidOperationException">Method body was not declared to have exception regions.</exception>
		/// <returns>Encoder for the next clause.</returns>
		// Token: 0x060008E0 RID: 2272 RVA: 0x000194F0 File Offset: 0x000176F0
		public ExceptionRegionEncoder AddFinally(int tryOffset, int tryLength, int handlerOffset, int handlerLength)
		{
			return this.Add(ExceptionRegionKind.Finally, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), 0);
		}

		/// <summary>Adds a fault clause.</summary>
		/// <param name="tryOffset">Try block start offset.</param>
		/// <param name="tryLength">Try block length.</param>
		/// <param name="handlerOffset">Handler start offset.</param>
		/// <param name="handlerLength">Handler length.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="tryLength" />, <paramref name="handlerOffset" /> or <paramref name="handlerLength" /> is out of range.</exception>
		/// <exception cref="T:System.InvalidOperationException">Method body was not declared to have exception regions.</exception>
		/// <returns>Encoder for the next clause.</returns>
		// Token: 0x060008E1 RID: 2273 RVA: 0x00019514 File Offset: 0x00017714
		public ExceptionRegionEncoder AddFault(int tryOffset, int tryLength, int handlerOffset, int handlerLength)
		{
			return this.Add(ExceptionRegionKind.Fault, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), 0);
		}

		/// <summary>Adds a fault clause.</summary>
		/// <param name="tryOffset">Try block start offset.</param>
		/// <param name="tryLength">Try block length.</param>
		/// <param name="handlerOffset">Handler start offset.</param>
		/// <param name="handlerLength">Handler length.</param>
		/// <param name="catchType">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catchType" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="tryLength" />, <paramref name="handlerOffset" /> or <paramref name="handlerLength" /> is out of range.</exception>
		/// <exception cref="T:System.InvalidOperationException">Method body was not declared to have exception regions.</exception>
		/// <returns>Encoder for the next clause.</returns>
		// Token: 0x060008E2 RID: 2274 RVA: 0x00019537 File Offset: 0x00017737
		public ExceptionRegionEncoder AddCatch(int tryOffset, int tryLength, int handlerOffset, int handlerLength, EntityHandle catchType)
		{
			return this.Add(ExceptionRegionKind.Catch, tryOffset, tryLength, handlerOffset, handlerLength, catchType, 0);
		}

		/// <summary>Adds a fault clause.</summary>
		/// <param name="tryOffset">Try block start offset.</param>
		/// <param name="tryLength">Try block length.</param>
		/// <param name="handlerOffset">Handler start offset.</param>
		/// <param name="handlerLength">Handler length.</param>
		/// <param name="filterOffset">Offset of the filter block.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="tryOffset" />, <paramref name="tryLength" />, <paramref name="handlerOffset" /> or <paramref name="handlerLength" /> is out of range.</exception>
		/// <exception cref="T:System.InvalidOperationException">Method body was not declared to have exception regions.</exception>
		/// <returns>Encoder for the next clause.</returns>
		// Token: 0x060008E3 RID: 2275 RVA: 0x00019548 File Offset: 0x00017748
		public ExceptionRegionEncoder AddFilter(int tryOffset, int tryLength, int handlerOffset, int handlerLength, int filterOffset)
		{
			return this.Add(ExceptionRegionKind.Filter, tryOffset, tryLength, handlerOffset, handlerLength, default(EntityHandle), filterOffset);
		}

		/// <summary>Adds an exception clause.</summary>
		/// <param name="kind">Clause kind.</param>
		/// <param name="tryOffset">Try block start offset.</param>
		/// <param name="tryLength">Try block length.</param>
		/// <param name="handlerOffset">Handler start offset.</param>
		/// <param name="handlerLength">Handler length.</param>
		/// <param name="catchType">
		///   <see cref="T:System.Reflection.Metadata.TypeDefinitionHandle" />, <see cref="T:System.Reflection.Metadata.TypeReferenceHandle" /> or <see cref="T:System.Reflection.Metadata.TypeSpecificationHandle" />, or nil if <paramref name="kind" /> is not <see cref="F:System.Reflection.Metadata.ExceptionRegionKind.Catch" />.</param>
		/// <param name="filterOffset">Offset of the filter block, or 0 if the <paramref name="kind" /> is not <see cref="F:System.Reflection.Metadata.ExceptionRegionKind.Filter" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="catchType" /> is invalid.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="kind" /> has an invalid value.
		///
		/// -or-
		///
		/// <paramref name="tryOffset" />, <paramref name="tryLength" />, <paramref name="handlerOffset" />, or <paramref name="handlerLength" /> is out of range.</exception>
		/// <exception cref="T:System.InvalidOperationException">Method body was not declared to have exception regions.</exception>
		/// <returns>Encoder for the next clause.</returns>
		// Token: 0x060008E4 RID: 2276 RVA: 0x0001956C File Offset: 0x0001776C
		public ExceptionRegionEncoder Add(ExceptionRegionKind kind, int tryOffset, int tryLength, int handlerOffset, int handlerLength, EntityHandle catchType = default(EntityHandle), int filterOffset = 0)
		{
			if (this.Builder == null)
			{
				Throw.InvalidOperation(SR.MethodHasNoExceptionRegions);
			}
			if (this.HasSmallFormat)
			{
				if ((int)((ushort)tryOffset) != tryOffset)
				{
					Throw.ArgumentOutOfRange("tryOffset");
				}
				if ((int)((byte)tryLength) != tryLength)
				{
					Throw.ArgumentOutOfRange("tryLength");
				}
				if ((int)((ushort)handlerOffset) != handlerOffset)
				{
					Throw.ArgumentOutOfRange("handlerOffset");
				}
				if ((int)((byte)handlerLength) != handlerLength)
				{
					Throw.ArgumentOutOfRange("handlerLength");
				}
			}
			else
			{
				if (tryOffset < 0)
				{
					Throw.ArgumentOutOfRange("tryOffset");
				}
				if (tryLength < 0)
				{
					Throw.ArgumentOutOfRange("tryLength");
				}
				if (handlerOffset < 0)
				{
					Throw.ArgumentOutOfRange("handlerOffset");
				}
				if (handlerLength < 0)
				{
					Throw.ArgumentOutOfRange("handlerLength");
				}
			}
			int num;
			switch (kind)
			{
			case ExceptionRegionKind.Catch:
				if (!ExceptionRegionEncoder.IsValidCatchTypeHandle(catchType))
				{
					Throw.InvalidArgument_Handle("catchType");
				}
				num = MetadataTokens.GetToken(catchType);
				goto IL_00F3;
			case ExceptionRegionKind.Filter:
				if (filterOffset < 0)
				{
					Throw.ArgumentOutOfRange("filterOffset");
				}
				num = filterOffset;
				goto IL_00F3;
			case ExceptionRegionKind.Finally:
			case ExceptionRegionKind.Fault:
				num = 0;
				goto IL_00F3;
			}
			throw new ArgumentOutOfRangeException("kind");
			IL_00F3:
			this.AddUnchecked(kind, tryOffset, tryLength, handlerOffset, handlerLength, num);
			return this;
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x00019680 File Offset: 0x00017880
		internal void AddUnchecked(ExceptionRegionKind kind, int tryOffset, int tryLength, int handlerOffset, int handlerLength, int catchTokenOrOffset)
		{
			if (this.HasSmallFormat)
			{
				this.Builder.WriteUInt16((ushort)kind);
				this.Builder.WriteUInt16((ushort)tryOffset);
				this.Builder.WriteByte((byte)tryLength);
				this.Builder.WriteUInt16((ushort)handlerOffset);
				this.Builder.WriteByte((byte)handlerLength);
			}
			else
			{
				this.Builder.WriteInt32((int)kind);
				this.Builder.WriteInt32(tryOffset);
				this.Builder.WriteInt32(tryLength);
				this.Builder.WriteInt32(handlerOffset);
				this.Builder.WriteInt32(handlerLength);
			}
			this.Builder.WriteInt32(catchTokenOrOffset);
		}

		// Token: 0x0400054B RID: 1355
		private const int TableHeaderSize = 4;

		// Token: 0x0400054C RID: 1356
		private const int SmallRegionSize = 12;

		// Token: 0x0400054D RID: 1357
		private const int FatRegionSize = 24;

		// Token: 0x0400054E RID: 1358
		private const int ThreeBytesMaxValue = 16777215;

		// Token: 0x0400054F RID: 1359
		internal const int MaxSmallExceptionRegions = 20;

		// Token: 0x04000550 RID: 1360
		internal const int MaxExceptionRegions = 699050;
	}
}
