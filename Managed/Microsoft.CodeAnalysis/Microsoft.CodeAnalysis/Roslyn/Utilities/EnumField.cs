using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000040 RID: 64
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal readonly struct EnumField
	{
		// Token: 0x0600014E RID: 334 RVA: 0x00005592 File Offset: 0x00003792
		public EnumField(string name, ulong value, [Nullable(2)] object identityOpt = null)
		{
			this.Name = name;
			this.Value = value;
			this.IdentityOpt = identityOpt;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000055A9 File Offset: 0x000037A9
		public bool IsDefault
		{
			get
			{
				return this.Name == null;
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000055B4 File Offset: 0x000037B4
		private string GetDebuggerDisplay()
		{
			return string.Format("{{{0} = {1}}}", this.Name, this.Value);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000055D4 File Offset: 0x000037D4
		internal static EnumField FindValue(ArrayBuilder<EnumField> sortedFields, ulong value)
		{
			int i = 0;
			int num = sortedFields.Count;
			while (i < num)
			{
				int num2 = i + (num - i) / 2;
				long num3 = (long)(value - sortedFields[num2].Value);
				if (num3 == 0L)
				{
					while (num2 >= i && sortedFields[num2].Value == value)
					{
						num2--;
					}
					return sortedFields[num2 + 1];
				}
				if (num3 > 0L)
				{
					num = num2;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return default(EnumField);
		}

		// Token: 0x04000067 RID: 103
		public static readonly IComparer<EnumField> Comparer = new EnumField.EnumFieldComparer();

		// Token: 0x04000068 RID: 104
		public readonly string Name;

		// Token: 0x04000069 RID: 105
		public readonly ulong Value;

		// Token: 0x0400006A RID: 106
		[Nullable(2)]
		public readonly object IdentityOpt;

		// Token: 0x020004FE RID: 1278
		[NullableContext(0)]
		private class EnumFieldComparer : IComparer<EnumField>
		{
			// Token: 0x060032E8 RID: 13032 RVA: 0x00090758 File Offset: 0x0008E958
			int IComparer<EnumField>.Compare(EnumField field1, EnumField field2)
			{
				int num = ((long)field2.Value).CompareTo((long)field1.Value);
				if (num != 0)
				{
					return num;
				}
				return string.CompareOrdinal(field1.Name, field2.Name);
			}
		}
	}
}
