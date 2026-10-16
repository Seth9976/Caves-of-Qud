using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000131 RID: 305
	internal struct MostCommonNullableValueBuilder
	{
		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060018DC RID: 6364 RVA: 0x0008523C File Offset: 0x0008343C
		internal byte? MostCommonValue
		{
			get
			{
				int num;
				byte b;
				if (this._value1 > this._value0)
				{
					num = this._value1;
					b = 1;
				}
				else
				{
					num = this._value0;
					b = 0;
				}
				if (this._value2 > num)
				{
					return new byte?((byte)2);
				}
				if (num != 0)
				{
					return new byte?(b);
				}
				return default(byte?);
			}
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x00085290 File Offset: 0x00083490
		internal void AddValue(byte value)
		{
			switch (value)
			{
			case 0:
				this._value0++;
				return;
			case 1:
				this._value1++;
				return;
			case 2:
				this._value2++;
				return;
			default:
				throw ExceptionUtilities.UnexpectedValue(value);
			}
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x000852E9 File Offset: 0x000834E9
		internal void AddValue(byte? value)
		{
			if (value != null)
			{
				this.AddValue(value.GetValueOrDefault());
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x00085304 File Offset: 0x00083504
		internal void AddValue(TypeWithAnnotations type)
		{
			ArrayBuilder<byte> instance = ArrayBuilder<byte>.GetInstance();
			type.AddNullableTransforms(instance);
			this.AddValue(MostCommonNullableValueBuilder.GetCommonValue(instance));
			instance.Free();
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x00085334 File Offset: 0x00083534
		internal static byte? GetCommonValue(ArrayBuilder<byte> builder)
		{
			int count = builder.Count;
			if (count == 0)
			{
				return default(byte?);
			}
			byte b = builder[0];
			for (int i = 1; i < count; i++)
			{
				if (builder[i] != b)
				{
					return default(byte?);
				}
			}
			return new byte?(b);
		}

		// Token: 0x040006DF RID: 1759
		private int _value0;

		// Token: 0x040006E0 RID: 1760
		private int _value1;

		// Token: 0x040006E1 RID: 1761
		private int _value2;
	}
}
