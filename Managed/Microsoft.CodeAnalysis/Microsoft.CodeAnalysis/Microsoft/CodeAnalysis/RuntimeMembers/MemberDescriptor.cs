using System;
using System.Collections.Immutable;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.RuntimeMembers
{
	// Token: 0x020002C9 RID: 713
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct MemberDescriptor
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x000565C4 File Offset: 0x000547C4
		[Nullable(2)]
		public string DeclaringTypeMetadataName
		{
			[NullableContext(2)]
			get
			{
				if (this.DeclaringTypeId > 45)
				{
					return ((WellKnownType)this.DeclaringTypeId).GetMetadataName();
				}
				return ((SpecialType)this.DeclaringTypeId).GetMetadataName();
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x000565E8 File Offset: 0x000547E8
		public int ParametersCount
		{
			get
			{
				MemberFlags memberFlags = this.Flags & MemberFlags.KindMask;
				if (memberFlags <= MemberFlags.Constructor)
				{
					if (memberFlags != MemberFlags.Method && memberFlags != MemberFlags.Constructor)
					{
						goto IL_0031;
					}
				}
				else if (memberFlags != MemberFlags.PropertyGet && memberFlags != MemberFlags.Property)
				{
					goto IL_0031;
				}
				return (int)this.Signature[0];
				IL_0031:
				throw ExceptionUtilities.UnexpectedValue(memberFlags);
			}
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x00056631 File Offset: 0x00054831
		[NullableContext(0)]
		public MemberDescriptor(MemberFlags Flags, short DeclaringTypeId, [Nullable(1)] string Name, ImmutableArray<byte> Signature, ushort Arity = 0)
		{
			this.Flags = Flags;
			this.DeclaringTypeId = DeclaringTypeId;
			this.Name = Name;
			this.Arity = Arity;
			this.Signature = Signature;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x00056658 File Offset: 0x00054858
		[return: Nullable(0)]
		internal static ImmutableArray<MemberDescriptor> InitializeFromStream(Stream stream, string[] nameTable)
		{
			int num = nameTable.Length;
			ImmutableArray<MemberDescriptor>.Builder builder = ImmutableArray.CreateBuilder<MemberDescriptor>(num);
			ImmutableArray<byte>.Builder builder2 = ImmutableArray.CreateBuilder<byte>();
			for (int i = 0; i < num; i++)
			{
				MemberFlags memberFlags = (MemberFlags)stream.ReadByte();
				short num2 = MemberDescriptor.ReadTypeId(stream);
				ushort num3 = (ushort)stream.ReadByte();
				if ((memberFlags & MemberFlags.Field) != (MemberFlags)0)
				{
					MemberDescriptor.ParseType(builder2, stream, false);
				}
				else
				{
					MemberDescriptor.ParseMethodOrPropertySignature(builder2, stream);
				}
				builder.Add(new MemberDescriptor(memberFlags, num2, nameTable[i], builder2.ToImmutable(), num3));
				builder2.Clear();
			}
			return builder.ToImmutable();
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x000566DC File Offset: 0x000548DC
		private static short ReadTypeId(Stream stream)
		{
			byte b = (byte)stream.ReadByte();
			if (b == 255)
			{
				return (short)(stream.ReadByte() + 255);
			}
			return (short)b;
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x00056708 File Offset: 0x00054908
		private static void ParseMethodOrPropertySignature(ImmutableArray<byte>.Builder builder, Stream stream)
		{
			int num = stream.ReadByte();
			builder.Add((byte)num);
			MemberDescriptor.ParseType(builder, stream, true);
			for (int i = 0; i < num; i++)
			{
				MemberDescriptor.ParseType(builder, stream, true);
			}
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x00056740 File Offset: 0x00054940
		private static void ParseType(ImmutableArray<byte>.Builder builder, Stream stream, bool allowByRef = false)
		{
			SignatureTypeCode signatureTypeCode;
			for (;;)
			{
				signatureTypeCode = (SignatureTypeCode)stream.ReadByte();
				builder.Add((byte)signatureTypeCode);
				if (signatureTypeCode > SignatureTypeCode.SZArray)
				{
					goto IL_0040;
				}
				switch (signatureTypeCode)
				{
				case SignatureTypeCode.Pointer:
					break;
				case SignatureTypeCode.ByReference:
					if (!allowByRef)
					{
						goto Block_4;
					}
					break;
				case (SignatureTypeCode)17:
				case (SignatureTypeCode)18:
				case SignatureTypeCode.Array:
					goto IL_004A;
				case SignatureTypeCode.GenericTypeParameter:
					goto IL_005E;
				case SignatureTypeCode.GenericTypeInstance:
					goto IL_0071;
				default:
					if (signatureTypeCode != SignatureTypeCode.SZArray)
					{
						goto Block_2;
					}
					break;
				}
				allowByRef = false;
			}
			Block_2:
			goto IL_004A;
			IL_0040:
			if (signatureTypeCode == SignatureTypeCode.GenericMethodParameter)
			{
				goto IL_005E;
			}
			if (signatureTypeCode == SignatureTypeCode.TypeHandle)
			{
				MemberDescriptor.ParseTypeHandle(builder, stream);
				return;
			}
			IL_004A:
			throw ExceptionUtilities.UnexpectedValue(signatureTypeCode);
			IL_005E:
			builder.Add((byte)stream.ReadByte());
			return;
			Block_4:
			goto IL_004A;
			IL_0071:
			MemberDescriptor.ParseGenericTypeInstance(builder, stream);
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x000567CC File Offset: 0x000549CC
		private static void ParseTypeHandle(ImmutableArray<byte>.Builder builder, Stream stream)
		{
			byte b = (byte)stream.ReadByte();
			builder.Add(b);
			if (b == 255)
			{
				byte b2 = (byte)stream.ReadByte();
				builder.Add(b2);
			}
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x00056800 File Offset: 0x00054A00
		private static void ParseGenericTypeInstance(ImmutableArray<byte>.Builder builder, Stream stream)
		{
			MemberDescriptor.ParseType(builder, stream, false);
			int num = stream.ReadByte();
			builder.Add((byte)num);
			for (int i = 0; i < num; i++)
			{
				MemberDescriptor.ParseType(builder, stream, false);
			}
		}

		// Token: 0x04000D81 RID: 3457
		public readonly MemberFlags Flags;

		// Token: 0x04000D82 RID: 3458
		public readonly short DeclaringTypeId;

		// Token: 0x04000D83 RID: 3459
		public readonly ushort Arity;

		// Token: 0x04000D84 RID: 3460
		public readonly string Name;

		// Token: 0x04000D85 RID: 3461
		[Nullable(0)]
		public readonly ImmutableArray<byte> Signature;
	}
}
