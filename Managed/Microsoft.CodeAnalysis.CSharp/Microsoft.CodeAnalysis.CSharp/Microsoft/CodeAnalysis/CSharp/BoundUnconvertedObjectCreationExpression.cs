using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000EC RID: 236
	internal sealed class BoundUnconvertedObjectCreationExpression : BoundExpression
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0005F3F8 File Offset: 0x0005D5F8
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override object Display
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				ImmutableArray<BoundExpression> arguments = this.Arguments;
				if (arguments.Length == 0)
				{
					return "new()";
				}
				PooledStringBuilder instance = PooledStringBuilder.GetInstance();
				StringBuilder builder = instance.Builder;
				object[] array = new object[arguments.Length];
				builder.Append("new");
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

		// Token: 0x06001207 RID: 4615 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
		public BoundUnconvertedObjectCreationExpression([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<IdentifierNameSyntax> argumentNamesOpt, ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializerOpt, bool hasErrors = false)
			: base(BoundKind.UnconvertedObjectCreationExpression, syntax, null, hasErrors || arguments.HasErrors<BoundExpression>())
		{
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.InitializerOpt = initializerOpt;
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x0005F4FA File Offset: 0x0005D6FA
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0005F502 File Offset: 0x0005D702
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x0600120A RID: 4618 RVA: 0x0005F50A File Offset: 0x0005D70A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IdentifierNameSyntax> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x0005F512 File Offset: 0x0005D712
		public ImmutableArray<RefKind> ArgumentRefKindsOpt { get; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600120C RID: 4620 RVA: 0x0005F51A File Offset: 0x0005D71A
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public InitializerExpressionSyntax InitializerOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x0005F522 File Offset: 0x0005D722
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUnconvertedObjectCreationExpression(this);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x0005F52C File Offset: 0x0005D72C
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundUnconvertedObjectCreationExpression Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<IdentifierNameSyntax> argumentNamesOpt, ImmutableArray<RefKind> argumentRefKindsOpt, [global::System.Runtime.CompilerServices.Nullable(2)] InitializerExpressionSyntax initializerOpt)
		{
			if (arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || initializerOpt != this.InitializerOpt)
			{
				BoundUnconvertedObjectCreationExpression boundUnconvertedObjectCreationExpression = new BoundUnconvertedObjectCreationExpression(this.Syntax, arguments, argumentNamesOpt, argumentRefKindsOpt, initializerOpt, base.HasErrors);
				boundUnconvertedObjectCreationExpression.CopyAttributes(this);
				return boundUnconvertedObjectCreationExpression;
			}
			return this;
		}
	}
}
