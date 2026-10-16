using System;
using System.Text;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x0200000D RID: 13
	public sealed class ArrayType : TypeSpecification
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002C58 File Offset: 0x00000E58
		public Collection<ArrayDimension> Dimensions
		{
			get
			{
				if (this.dimensions != null)
				{
					return this.dimensions;
				}
				this.dimensions = new Collection<ArrayDimension>();
				this.dimensions.Add(default(ArrayDimension));
				return this.dimensions;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002C99 File Offset: 0x00000E99
		public int Rank
		{
			get
			{
				if (this.dimensions != null)
				{
					return this.dimensions.Count;
				}
				return 1;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public bool IsVector
		{
			get
			{
				return this.dimensions == null || (this.dimensions.Count <= 1 && !this.dimensions[0].IsSized);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x000025EF File Offset: 0x000007EF
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002B65 File Offset: 0x00000D65
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002CEE File Offset: 0x00000EEE
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D01 File Offset: 0x00000F01
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002D14 File Offset: 0x00000F14
		private string Suffix
		{
			get
			{
				if (this.IsVector)
				{
					return "[]";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				for (int i = 0; i < this.dimensions.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(this.dimensions[i].ToString());
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002AF8 File Offset: 0x00000CF8
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002D9A File Offset: 0x00000F9A
		public ArrayType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Trivial.Mono.Cecil.Metadata.ElementType.Array;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public ArrayType(TypeReference type, int rank)
			: this(type)
		{
			Mixin.CheckType(type);
			if (rank != 1)
			{
				this.dimensions = new Collection<ArrayDimension>(rank);
				for (int i = 0; i < rank; i++)
				{
					this.dimensions.Add(default(ArrayDimension));
				}
				this.etype = Trivial.Mono.Cecil.Metadata.ElementType.Array;
			}
		}

		// Token: 0x04000010 RID: 16
		private Collection<ArrayDimension> dimensions;
	}
}
