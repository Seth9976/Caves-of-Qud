using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200003D RID: 61
	internal sealed class MethodArgumentInfo : IEquatable<MethodArgumentInfo>
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0003B713 File Offset: 0x00039913
		public MethodArgumentInfo([global::System.Runtime.CompilerServices.Nullable(1)] MethodSymbol Method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> Arguments, ImmutableArray<int> ArgsToParamsOpt, BitVector DefaultArguments, bool Expanded)
		{
			this.Method = Method;
			this.Arguments = Arguments;
			this.ArgsToParamsOpt = ArgsToParamsOpt;
			this.DefaultArguments = DefaultArguments;
			this.Expanded = Expanded;
			base..ctor();
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0003B740 File Offset: 0x00039940
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private Type EqualityContract
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			[CompilerGenerated]
			get
			{
				return typeof(MethodArgumentInfo);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0003B74C File Offset: 0x0003994C
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0003B754 File Offset: 0x00039954
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public MethodSymbol Method
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get;
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			set;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0003B75D File Offset: 0x0003995D
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0003B765 File Offset: 0x00039965
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
			[param: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			set;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0003B76E File Offset: 0x0003996E
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0003B776 File Offset: 0x00039976
		public ImmutableArray<int> ArgsToParamsOpt { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0003B77F File Offset: 0x0003997F
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0003B787 File Offset: 0x00039987
		public BitVector DefaultArguments { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0003B790 File Offset: 0x00039990
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0003B798 File Offset: 0x00039998
		public bool Expanded { get; set; }

		// Token: 0x060006C7 RID: 1735 RVA: 0x0003B7A4 File Offset: 0x000399A4
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public static MethodArgumentInfo CreateParameterlessMethod(MethodSymbol method)
		{
			return new MethodArgumentInfo(method, ImmutableArray<BoundExpression>.Empty, default(ImmutableArray<int>), default(BitVector), false);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0003B7D0 File Offset: 0x000399D0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("MethodArgumentInfo");
			stringBuilder.Append(" { ");
			if (this.PrintMembers(stringBuilder))
			{
				stringBuilder.Append(" ");
			}
			stringBuilder.Append("}");
			return stringBuilder.ToString();
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003B824 File Offset: 0x00039A24
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		private bool PrintMembers(StringBuilder builder)
		{
			builder.Append("Method");
			builder.Append(" = ");
			builder.Append(this.Method);
			builder.Append(", ");
			builder.Append("Arguments");
			builder.Append(" = ");
			builder.Append(this.Arguments.ToString());
			builder.Append(", ");
			builder.Append("ArgsToParamsOpt");
			builder.Append(" = ");
			builder.Append(this.ArgsToParamsOpt.ToString());
			builder.Append(", ");
			builder.Append("DefaultArguments");
			builder.Append(" = ");
			builder.Append(this.DefaultArguments.ToString());
			builder.Append(", ");
			builder.Append("Expanded");
			builder.Append(" = ");
			builder.Append(this.Expanded.ToString());
			return true;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0003B953 File Offset: 0x00039B53
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool operator !=(MethodArgumentInfo left, MethodArgumentInfo right)
		{
			return !(left == right);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0003B95F File Offset: 0x00039B5F
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static bool operator ==(MethodArgumentInfo left, MethodArgumentInfo right)
		{
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0003B974 File Offset: 0x00039B74
		public override int GetHashCode()
		{
			return ((((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<MethodSymbol>.Default.GetHashCode(this.<Method>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<BoundExpression>>.Default.GetHashCode(this.<Arguments>k__BackingField)) * -1521134295 + EqualityComparer<ImmutableArray<int>>.Default.GetHashCode(this.<ArgsToParamsOpt>k__BackingField)) * -1521134295 + EqualityComparer<BitVector>.Default.GetHashCode(this.<DefaultArguments>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Expanded>k__BackingField);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0003BA04 File Offset: 0x00039C04
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MethodArgumentInfo);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0003BA14 File Offset: 0x00039C14
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public bool Equals(MethodArgumentInfo other)
		{
			return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<MethodSymbol>.Default.Equals(this.<Method>k__BackingField, other.<Method>k__BackingField) && EqualityComparer<ImmutableArray<BoundExpression>>.Default.Equals(this.<Arguments>k__BackingField, other.<Arguments>k__BackingField) && EqualityComparer<ImmutableArray<int>>.Default.Equals(this.<ArgsToParamsOpt>k__BackingField, other.<ArgsToParamsOpt>k__BackingField) && EqualityComparer<BitVector>.Default.Equals(this.<DefaultArguments>k__BackingField, other.<DefaultArguments>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Expanded>k__BackingField, other.<Expanded>k__BackingField));
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0003BABB File Offset: 0x00039CBB
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public MethodArgumentInfo <Clone>$()
		{
			return new MethodArgumentInfo(this);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0003BAC4 File Offset: 0x00039CC4
		private MethodArgumentInfo([global::System.Runtime.CompilerServices.Nullable(1)] MethodArgumentInfo original)
		{
			this.Method = original.<Method>k__BackingField;
			this.Arguments = original.<Arguments>k__BackingField;
			this.ArgsToParamsOpt = original.<ArgsToParamsOpt>k__BackingField;
			this.DefaultArguments = original.<DefaultArguments>k__BackingField;
			this.Expanded = original.<Expanded>k__BackingField;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0003BB13 File Offset: 0x00039D13
		public void Deconstruct([global::System.Runtime.CompilerServices.Nullable(1)] out MethodSymbol Method, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] out ImmutableArray<BoundExpression> Arguments, out ImmutableArray<int> ArgsToParamsOpt, out BitVector DefaultArguments, out bool Expanded)
		{
			Method = this.Method;
			Arguments = this.Arguments;
			ArgsToParamsOpt = this.ArgsToParamsOpt;
			DefaultArguments = this.DefaultArguments;
			Expanded = this.Expanded;
		}
	}
}
