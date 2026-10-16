using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x0200047F RID: 1151
	public readonly struct EditAndContinueMethodDebugInformation
	{
		// Token: 0x06002D19 RID: 11545 RVA: 0x0007E8A9 File Offset: 0x0007CAA9
		internal EditAndContinueMethodDebugInformation(int methodOrdinal, ImmutableArray<LocalSlotDebugInfo> localSlots, ImmutableArray<ClosureDebugInfo> closures, ImmutableArray<LambdaDebugInfo> lambdas)
		{
			this.MethodOrdinal = methodOrdinal;
			this.LocalSlots = localSlots;
			this.Lambdas = lambdas;
			this.Closures = closures;
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0007E8C8 File Offset: 0x0007CAC8
		public static EditAndContinueMethodDebugInformation Create(ImmutableArray<byte> compressedSlotMap, ImmutableArray<byte> compressedLambdaMap)
		{
			int num;
			ImmutableArray<ClosureDebugInfo> immutableArray;
			ImmutableArray<LambdaDebugInfo> immutableArray2;
			EditAndContinueMethodDebugInformation.UncompressLambdaMap(compressedLambdaMap, out num, out immutableArray, out immutableArray2);
			return new EditAndContinueMethodDebugInformation(num, EditAndContinueMethodDebugInformation.UncompressSlotMap(compressedSlotMap), immutableArray, immutableArray2);
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0007E8F0 File Offset: 0x0007CAF0
		[return: Nullable(1)]
		private static InvalidDataException CreateInvalidDataException(ImmutableArray<byte> data, int offset)
		{
			int num = Math.Max(0, offset - 512);
			int num2 = Math.Min(data.Length, offset + 512);
			byte[] array = new byte[offset - num];
			data.CopyTo(num, array, 0, array.Length);
			byte[] array2 = new byte[num2 - offset];
			data.CopyTo(offset, array2, 0, array2.Length);
			throw new InvalidDataException(string.Format(CodeAnalysisResources.InvalidDataAtOffset, new object[]
			{
				offset,
				(num != 0) ? "..." : "",
				BitConverter.ToString(array),
				BitConverter.ToString(array2),
				(num2 != data.Length) ? "..." : ""
			}));
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0007E9A8 File Offset: 0x0007CBA8
		private unsafe static ImmutableArray<LocalSlotDebugInfo> UncompressSlotMap(ImmutableArray<byte> compressedSlotMap)
		{
			if (compressedSlotMap.IsDefaultOrEmpty)
			{
				return default(ImmutableArray<LocalSlotDebugInfo>);
			}
			ArrayBuilder<LocalSlotDebugInfo> instance = ArrayBuilder<LocalSlotDebugInfo>.GetInstance();
			int num = -1;
			fixed (byte* ptr = &compressedSlotMap.ToArray<byte>()[0])
			{
				byte* ptr2 = ptr;
				BlobReader blobReader = new BlobReader(ptr2, compressedSlotMap.Length);
				while (blobReader.RemainingBytes > 0)
				{
					try
					{
						byte b = blobReader.ReadByte();
						if (b == 255)
						{
							num = -blobReader.ReadCompressedInteger();
						}
						else if (b == 0)
						{
							instance.Add(new LocalSlotDebugInfo(SynthesizedLocalKind.LoweringTemp, default(LocalDebugId)));
						}
						else
						{
							SynthesizedLocalKind synthesizedLocalKind = (SynthesizedLocalKind)((b & 63) - 1);
							bool flag = (b & 128) > 0;
							int num2 = blobReader.ReadCompressedInteger() + num;
							int num3 = (flag ? blobReader.ReadCompressedInteger() : 0);
							instance.Add(new LocalSlotDebugInfo(synthesizedLocalKind, new LocalDebugId(num2, num3)));
						}
					}
					catch (BadImageFormatException)
					{
						throw EditAndContinueMethodDebugInformation.CreateInvalidDataException(compressedSlotMap, blobReader.Offset);
					}
				}
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0007EAAC File Offset: 0x0007CCAC
		[NullableContext(1)]
		internal void SerializeLocalSlots(BlobBuilder writer)
		{
			int num = -1;
			foreach (LocalSlotDebugInfo localSlotDebugInfo in this.LocalSlots)
			{
				if (localSlotDebugInfo.Id.SyntaxOffset < num)
				{
					num = localSlotDebugInfo.Id.SyntaxOffset;
				}
			}
			if (num != -1)
			{
				writer.WriteByte(byte.MaxValue);
				writer.WriteCompressedInteger(-num);
			}
			foreach (LocalSlotDebugInfo localSlotDebugInfo2 in this.LocalSlots)
			{
				SynthesizedLocalKind synthesizedKind = localSlotDebugInfo2.SynthesizedKind;
				if (!synthesizedKind.IsLongLived())
				{
					writer.WriteByte(0);
				}
				else
				{
					byte b = (byte)(synthesizedKind + 1);
					bool flag = localSlotDebugInfo2.Id.Ordinal > 0;
					if (flag)
					{
						b |= 128;
					}
					writer.WriteByte(b);
					writer.WriteCompressedInteger(localSlotDebugInfo2.Id.SyntaxOffset - num);
					if (flag)
					{
						writer.WriteCompressedInteger(localSlotDebugInfo2.Id.Ordinal);
					}
				}
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x0007EBA4 File Offset: 0x0007CDA4
		private unsafe static void UncompressLambdaMap(ImmutableArray<byte> compressedLambdaMap, out int methodOrdinal, out ImmutableArray<ClosureDebugInfo> closures, out ImmutableArray<LambdaDebugInfo> lambdas)
		{
			methodOrdinal = -1;
			closures = default(ImmutableArray<ClosureDebugInfo>);
			lambdas = default(ImmutableArray<LambdaDebugInfo>);
			if (compressedLambdaMap.IsDefaultOrEmpty)
			{
				return;
			}
			ArrayBuilder<ClosureDebugInfo> instance = ArrayBuilder<ClosureDebugInfo>.GetInstance();
			ArrayBuilder<LambdaDebugInfo> instance2 = ArrayBuilder<LambdaDebugInfo>.GetInstance();
			fixed (byte* ptr = &compressedLambdaMap.ToArray<byte>()[0])
			{
				byte* ptr2 = ptr;
				BlobReader blobReader = new BlobReader(ptr2, compressedLambdaMap.Length);
				try
				{
					methodOrdinal = blobReader.ReadCompressedInteger() - 1;
					int num = -blobReader.ReadCompressedInteger();
					int num2 = blobReader.ReadCompressedInteger();
					for (int i = 0; i < num2; i++)
					{
						int num3 = blobReader.ReadCompressedInteger();
						DebugId debugId = new DebugId(instance.Count, 0);
						instance.Add(new ClosureDebugInfo(num3 + num, debugId));
					}
					while (blobReader.RemainingBytes > 0)
					{
						int num4 = blobReader.ReadCompressedInteger();
						int num5 = blobReader.ReadCompressedInteger() + -2;
						if (num5 >= num2)
						{
							throw EditAndContinueMethodDebugInformation.CreateInvalidDataException(compressedLambdaMap, blobReader.Offset);
						}
						DebugId debugId2 = new DebugId(instance2.Count, 0);
						instance2.Add(new LambdaDebugInfo(num4 + num, debugId2, num5));
					}
				}
				catch (BadImageFormatException)
				{
					throw EditAndContinueMethodDebugInformation.CreateInvalidDataException(compressedLambdaMap, blobReader.Offset);
				}
			}
			closures = instance.ToImmutableAndFree();
			lambdas = instance2.ToImmutableAndFree();
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0007ECE4 File Offset: 0x0007CEE4
		[NullableContext(1)]
		internal void SerializeLambdaMap(BlobBuilder writer)
		{
			writer.WriteCompressedInteger(this.MethodOrdinal + 1);
			int num = -1;
			foreach (ClosureDebugInfo closureDebugInfo in this.Closures)
			{
				if (closureDebugInfo.SyntaxOffset < num)
				{
					num = closureDebugInfo.SyntaxOffset;
				}
			}
			foreach (LambdaDebugInfo lambdaDebugInfo in this.Lambdas)
			{
				if (lambdaDebugInfo.SyntaxOffset < num)
				{
					num = lambdaDebugInfo.SyntaxOffset;
				}
			}
			writer.WriteCompressedInteger(-num);
			writer.WriteCompressedInteger(this.Closures.Length);
			foreach (ClosureDebugInfo closureDebugInfo2 in this.Closures)
			{
				writer.WriteCompressedInteger(closureDebugInfo2.SyntaxOffset - num);
			}
			foreach (LambdaDebugInfo lambdaDebugInfo2 in this.Lambdas)
			{
				writer.WriteCompressedInteger(lambdaDebugInfo2.SyntaxOffset - num);
				writer.WriteCompressedInteger(lambdaDebugInfo2.ClosureOrdinal - -2);
			}
		}

		// Token: 0x040011E3 RID: 4579
		internal readonly int MethodOrdinal;

		// Token: 0x040011E4 RID: 4580
		internal readonly ImmutableArray<LocalSlotDebugInfo> LocalSlots;

		// Token: 0x040011E5 RID: 4581
		internal readonly ImmutableArray<LambdaDebugInfo> Lambdas;

		// Token: 0x040011E6 RID: 4582
		internal readonly ImmutableArray<ClosureDebugInfo> Closures;

		// Token: 0x040011E7 RID: 4583
		private const byte SyntaxOffsetBaseline = 255;
	}
}
