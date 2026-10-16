using System;
using System.Collections.Immutable;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Microsoft.Cci
{
	// Token: 0x02000091 RID: 145
	internal static class InstructionOperandTypes
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000DBF8 File Offset: 0x0000BDF8
		internal static OperandType ReadOperandType(ImmutableArray<byte> il, ref int position)
		{
			int num = position;
			position = num + 1;
			byte b = il[num];
			if (b == 254)
			{
				byte[] twoByte = InstructionOperandTypes.TwoByte;
				num = position;
				position = num + 1;
				return twoByte[(int)il[num]];
			}
			return InstructionOperandTypes.OneByte[(int)b];
		}

		// Token: 0x04000193 RID: 403
		[Nullable(1)]
		internal static readonly byte[] OneByte = new byte[]
		{
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 18, 18, 18, 18, 18, 18,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 16, 2, 3, 17, 7, 0, 5, 5, 4,
			4, 9, 5, 15, 15, 15, 15, 15, 15, 15,
			15, 15, 15, 15, 15, 15, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 11,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 4, 13, 13, 10, 4, 13, 13, 5, 0,
			0, 13, 5, 1, 1, 1, 1, 1, 1, 13,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			13, 13, 5, 13, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 5, 5, 13, 13, 13, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 5,
			5, 5, 5, 5, 5, 5, 5, 0, 0, 0,
			0, 0, 0, 0, 13, 5, 0, 0, 13, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 12, 5,
			5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
			5, 0, 15, 5, 5, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0
		};

		// Token: 0x04000194 RID: 404
		[Nullable(1)]
		internal static readonly byte[] TwoByte = new byte[]
		{
			5, 5, 5, 5, 5, 5, 4, 4, 0, 14,
			14, 14, 14, 14, 14, 5, 0, 5, 16, 5,
			5, 13, 13, 5, 5, 0, 5, 0, 13, 5,
			5
		};
	}
}
