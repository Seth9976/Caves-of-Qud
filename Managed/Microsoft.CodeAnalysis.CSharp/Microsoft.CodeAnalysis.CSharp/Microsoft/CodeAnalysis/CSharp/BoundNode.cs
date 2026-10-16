using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000B6 RID: 182
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal abstract class BoundNode : IBoundNodeWithIOperationChildren
	{
		// Token: 0x06000D58 RID: 3416 RVA: 0x00056795 File Offset: 0x00054995
		protected BoundNode MemberwiseClone()
		{
			BoundNode boundNode = (BoundNode)base.MemberwiseClone();
			boundNode._attributes &= BoundNode.BoundNodeAttributes.AttributesPreservedInClone;
			return boundNode;
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000567B4 File Offset: 0x000549B4
		protected BoundNode(BoundKind kind, SyntaxNode syntax)
		{
			this._kind = kind;
			this.Syntax = syntax;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000567CA File Offset: 0x000549CA
		protected BoundNode(BoundKind kind, SyntaxNode syntax, bool hasErrors)
			: this(kind, syntax)
		{
			if (hasErrors)
			{
				this._attributes = BoundNode.BoundNodeAttributes.HasErrors;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x000567E0 File Offset: 0x000549E0
		public bool HasAnyErrors
		{
			get
			{
				if (this.HasErrors || (this.Syntax != null && this.Syntax.HasErrors))
				{
					return true;
				}
				BoundExpression boundExpression = this as BoundExpression;
				if (boundExpression == null)
				{
					return false;
				}
				TypeSymbol type = boundExpression.Type;
				bool? flag = ((type != null) ? new bool?(type.IsErrorType()) : default(bool?));
				bool flag2 = true;
				return (flag.GetValueOrDefault() == flag2) & (flag != null);
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x0005684B File Offset: 0x00054A4B
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00056858 File Offset: 0x00054A58
		public bool HasErrors
		{
			get
			{
				return (this._attributes & BoundNode.BoundNodeAttributes.HasErrors) > (BoundNode.BoundNodeAttributes)0;
			}
			private set
			{
				if (value)
				{
					this._attributes |= BoundNode.BoundNodeAttributes.HasErrors;
				}
			}
		}

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0005686B File Offset: 0x00054A6B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public SyntaxTree SyntaxTree
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				SyntaxNode syntax = this.Syntax;
				if (syntax == null)
				{
					return null;
				}
				return syntax.SyntaxTree;
			}
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0005687E File Offset: 0x00054A7E
		protected void CopyAttributes(BoundNode original)
		{
			this.WasCompilerGenerated = original.WasCompilerGenerated;
			this.IsSuppressed = original.IsSuppressed;
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00056898 File Offset: 0x00054A98
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000568A5 File Offset: 0x00054AA5
		public bool WasCompilerGenerated
		{
			get
			{
				return (this._attributes & BoundNode.BoundNodeAttributes.CompilerGenerated) > (BoundNode.BoundNodeAttributes)0;
			}
			internal set
			{
				if (value)
				{
					this._attributes |= BoundNode.BoundNodeAttributes.CompilerGenerated;
				}
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x000568B8 File Offset: 0x00054AB8
		public void ResetCompilerGenerated(bool newCompilerGenerated)
		{
			if (newCompilerGenerated)
			{
				this._attributes |= BoundNode.BoundNodeAttributes.CompilerGenerated;
				return;
			}
			this._attributes &= ~BoundNode.BoundNodeAttributes.CompilerGenerated;
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000568DB File Offset: 0x00054ADB
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x000568E4 File Offset: 0x00054AE4
		[DebuggerBrowsable(0)]
		protected NullabilityInfo TopLevelNullability
		{
			get
			{
				return this.TopLevelNullabilityCore;
			}
			set
			{
				this._attributes &= ~(BoundNode.BoundNodeAttributes.TopLevelFlowStateMaybeNull | BoundNode.BoundNodeAttributes.TopLevelNotAnnotated | BoundNode.BoundNodeAttributes.TopLevelAnnotated);
				BoundNode.BoundNodeAttributes attributes = this._attributes;
				NullableAnnotation annotation = value.Annotation;
				BoundNode.BoundNodeAttributes boundNodeAttributes;
				switch (annotation)
				{
				case NullableAnnotation.None:
					boundNodeAttributes = BoundNode.BoundNodeAttributes.TopLevelNone;
					break;
				case NullableAnnotation.NotAnnotated:
					boundNodeAttributes = BoundNode.BoundNodeAttributes.TopLevelNotAnnotated;
					break;
				case NullableAnnotation.Annotated:
					boundNodeAttributes = BoundNode.BoundNodeAttributes.TopLevelAnnotated;
					break;
				default:
					throw ExceptionUtilities.UnexpectedValue(annotation);
				}
				this._attributes = attributes | boundNodeAttributes;
				NullableFlowState flowState = value.FlowState;
				if (flowState == NullableFlowState.NotNull)
				{
					return;
				}
				if (flowState == NullableFlowState.MaybeNull)
				{
					this._attributes |= BoundNode.BoundNodeAttributes.TopLevelFlowStateMaybeNull;
					return;
				}
				throw ExceptionUtilities.UnexpectedValue(value.FlowState);
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000D65 RID: 3429 RVA: 0x00056978 File Offset: 0x00054B78
		private NullabilityInfo TopLevelNullabilityCore
		{
			get
			{
				if ((this._attributes & BoundNode.BoundNodeAttributes.TopLevelNone) == (BoundNode.BoundNodeAttributes)0)
				{
					return default(NullabilityInfo);
				}
				BoundNode.BoundNodeAttributes boundNodeAttributes = this._attributes & BoundNode.BoundNodeAttributes.TopLevelNone;
				NullableAnnotation nullableAnnotation;
				if (boundNodeAttributes != BoundNode.BoundNodeAttributes.TopLevelNotAnnotated)
				{
					if (boundNodeAttributes != BoundNode.BoundNodeAttributes.TopLevelAnnotated)
					{
						if (boundNodeAttributes != BoundNode.BoundNodeAttributes.TopLevelNone)
						{
							throw ExceptionUtilities.UnexpectedValue(boundNodeAttributes);
						}
						nullableAnnotation = NullableAnnotation.None;
					}
					else
					{
						nullableAnnotation = NullableAnnotation.Annotated;
					}
				}
				else
				{
					nullableAnnotation = NullableAnnotation.NotAnnotated;
				}
				NullableAnnotation nullableAnnotation2 = nullableAnnotation;
				NullableFlowState nullableFlowState = (((this._attributes & BoundNode.BoundNodeAttributes.TopLevelFlowStateMaybeNull) == (BoundNode.BoundNodeAttributes)0) ? NullableFlowState.NotNull : NullableFlowState.MaybeNull);
				return new NullabilityInfo(nullableAnnotation2, nullableFlowState);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000569E3 File Offset: 0x00054BE3
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000569F0 File Offset: 0x00054BF0
		public bool IsSuppressed
		{
			get
			{
				return (this._attributes & BoundNode.BoundNodeAttributes.IsSuppressed) > (BoundNode.BoundNodeAttributes)0;
			}
			protected set
			{
				if (value)
				{
					this._attributes |= BoundNode.BoundNodeAttributes.IsSuppressed;
				}
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x00056A03 File Offset: 0x00054C03
		public BoundKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x00056A0B File Offset: 0x00054C0B
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public virtual BoundNode Accept(BoundTreeVisitor visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x00056A12 File Offset: 0x00054C12
		internal BoundNode WithHasErrors()
		{
			if (this.HasErrors)
			{
				return this;
			}
			BoundNode boundNode = this.MemberwiseClone();
			boundNode.HasErrors = true;
			return boundNode;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x00056A2C File Offset: 0x00054C2C
		internal string GetDebuggerDisplay()
		{
			string text = base.GetType().Name;
			if (this.Syntax != null)
			{
				text = text + " " + this.Syntax.ToString();
			}
			return text;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00056A65 File Offset: 0x00054C65
		[Conditional("DEBUG")]
		public void CheckLocalsDefined()
		{
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000D6D RID: 3437 RVA: 0x00056A67 File Offset: 0x00054C67
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		ImmutableArray<BoundNode> IBoundNodeWithIOperationChildren.Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return this.Children;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00056A6F File Offset: 0x00054C6F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected virtual ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray<BoundNode>.Empty;
			}
		}

		// Token: 0x0400054F RID: 1359
		private readonly BoundKind _kind;

		// Token: 0x04000550 RID: 1360
		private BoundNode.BoundNodeAttributes _attributes;

		// Token: 0x04000551 RID: 1361
		public readonly SyntaxNode Syntax;

		// Token: 0x02000686 RID: 1670
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		[Flags]
		private enum BoundNodeAttributes : short
		{
			// Token: 0x04002092 RID: 8338
			HasErrors = 1,
			// Token: 0x04002093 RID: 8339
			CompilerGenerated = 2,
			// Token: 0x04002094 RID: 8340
			IsSuppressed = 4,
			// Token: 0x04002095 RID: 8341
			TopLevelFlowStateMaybeNull = 8,
			// Token: 0x04002096 RID: 8342
			TopLevelNotAnnotated = 16,
			// Token: 0x04002097 RID: 8343
			TopLevelAnnotated = 32,
			// Token: 0x04002098 RID: 8344
			TopLevelNone = 48,
			// Token: 0x04002099 RID: 8345
			TopLevelAnnotationMask = 48,
			// Token: 0x0400209A RID: 8346
			WasCompilerGeneratedIsChecked = 64,
			// Token: 0x0400209B RID: 8347
			WasTopLevelNullabilityChecked = 128,
			// Token: 0x0400209C RID: 8348
			WasConverted = 256,
			// Token: 0x0400209D RID: 8349
			AttributesPreservedInClone = 263
		}
	}
}
