using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AE RID: 174
	internal abstract class BoundTupleExpression : BoundExpression
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00055CB0 File Offset: 0x00053EB0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal void VisitAllElements<[global::System.Runtime.CompilerServices.Nullable(2)] T>(Action<BoundExpression, T> action, T args)
		{
			foreach (BoundExpression boundExpression in this.Arguments)
			{
				if (boundExpression.Kind == BoundKind.TupleLiteral)
				{
					((BoundTupleExpression)boundExpression).VisitAllElements<T>(action, args);
				}
				else
				{
					action.Invoke(boundExpression, args);
				}
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x00055D04 File Offset: 0x00053F04
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				ImmutableArray<BoundExpression> arguments = this.Arguments;
				object[] array = new object[arguments.Length];
				builder.Append('(');
				builder.Append("{0}");
				array[0] = arguments[0].Display;
				for (int i = 1; i < arguments.Length; i++)
				{
					builder.Append(", {" + i.ToString() + "}");
					array[i] = arguments[i].Display;
				}
				builder.Append(')');
				return FormattableStringFactory.Create(instance.ToStringAndFree(), array);
			}
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00055DB1 File Offset: 0x00053FB1
		protected BoundTupleExpression(BoundKind kind, [global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })] ImmutableArray<string> argumentNamesOpt, ImmutableArray<bool> inferredNamesOpt, [global::System.Runtime.CompilerServices.Nullable(2)] TypeSymbol type, bool hasErrors = false)
			: base(kind, syntax, type, hasErrors)
		{
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.InferredNamesOpt = inferredNamesOpt;
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x00055DD6 File Offset: 0x00053FD6
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x00055DDE File Offset: 0x00053FDE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000D1C RID: 3356 RVA: 0x00055DE6 File Offset: 0x00053FE6
		public ImmutableArray<bool> InferredNamesOpt { get; }
	}
}
