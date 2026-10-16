using System;
using System.Text;
using System.Threading;
using Mono.Cecil.Metadata;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001B1 RID: 433
	internal sealed class ArrayType : TypeSpecification
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		public Collection<ArrayDimension> Dimensions
		{
			get
			{
				if (this.dimensions != null)
				{
					return this.dimensions;
				}
				Interlocked.CompareExchange<Collection<ArrayDimension>>(ref this.dimensions, new Collection<ArrayDimension> { default(ArrayDimension) }, null);
				return this.dimensions;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0001AA35 File Offset: 0x00018C35
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public bool IsVector
		{
			get
			{
				return this.dimensions == null || (this.dimensions.Count <= 1 && !this.dimensions[0].IsSized);
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00018318 File Offset: 0x00016518
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x000189AE File Offset: 0x00016BAE
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

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001AA8A File Offset: 0x00018C8A
		public override string Name
		{
			get
			{
				return base.Name + this.Suffix;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001AA9D File Offset: 0x00018C9D
		public override string FullName
		{
			get
			{
				return base.FullName + this.Suffix;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001AAB0 File Offset: 0x00018CB0
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool IsArray
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0001AB36 File Offset: 0x00018D36
		public ArrayType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			this.etype = Mono.Cecil.Metadata.ElementType.Array;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0001AB50 File Offset: 0x00018D50
		public ArrayType(TypeReference type, int rank)
			: this(type)
		{
			Mixin.CheckType(type);
			if (rank == 1)
			{
				return;
			}
			this.dimensions = new Collection<ArrayDimension>(rank);
			for (int i = 0; i < rank; i++)
			{
				this.dimensions.Add(default(ArrayDimension));
			}
			this.etype = Mono.Cecil.Metadata.ElementType.Array;
		}

		// Token: 0x0400026C RID: 620
		private Collection<ArrayDimension> dimensions;
	}
}
