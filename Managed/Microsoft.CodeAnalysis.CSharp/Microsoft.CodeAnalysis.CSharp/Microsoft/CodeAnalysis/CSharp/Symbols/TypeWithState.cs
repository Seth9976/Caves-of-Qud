using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005AB RID: 1451
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
	internal readonly struct TypeWithState
	{
		// Token: 0x170022B5 RID: 8885
		// (get) Token: 0x060079F3 RID: 31219 RVA: 0x001BE778 File Offset: 0x001BC978
		[MemberNotNullWhen(false, "Type")]
		public bool HasNullType
		{
			[MemberNotNullWhen(false, "Type")]
			get
			{
				return this.Type == null;
			}
		}

		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x060079F4 RID: 31220 RVA: 0x001BE783 File Offset: 0x001BC983
		public bool MayBeNull
		{
			get
			{
				return this.State == NullableFlowState.MaybeNull;
			}
		}

		// Token: 0x170022B7 RID: 8887
		// (get) Token: 0x060079F5 RID: 31221 RVA: 0x001BE78E File Offset: 0x001BC98E
		public bool IsNotNull
		{
			get
			{
				return this.State == NullableFlowState.NotNull;
			}
		}

		// Token: 0x060079F6 RID: 31222 RVA: 0x001BE799 File Offset: 0x001BC999
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static TypeWithState ForType(TypeSymbol type)
		{
			return TypeWithState.Create(type, NullableFlowState.MaybeDefault);
		}

		// Token: 0x060079F7 RID: 31223 RVA: 0x001BE7A4 File Offset: 0x001BC9A4
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public static TypeWithState Create(TypeSymbol type, NullableFlowState defaultState)
		{
			if (defaultState == NullableFlowState.MaybeDefault && (type == null || type.IsTypeParameterDisallowingAnnotationInCSharp8()))
			{
				return new TypeWithState(type, defaultState);
			}
			NullableFlowState nullableFlowState = ((defaultState != NullableFlowState.NotNull && (type == null || type.CanContainNull())) ? NullableFlowState.MaybeNull : NullableFlowState.NotNull);
			return new TypeWithState(type, nullableFlowState);
		}

		// Token: 0x060079F8 RID: 31224 RVA: 0x001BE7E8 File Offset: 0x001BC9E8
		public static TypeWithState Create(TypeWithAnnotations typeWithAnnotations, FlowAnalysisAnnotations annotations = FlowAnalysisAnnotations.None)
		{
			TypeSymbol type = typeWithAnnotations.Type;
			NullableFlowState nullableFlowState;
			if (type.CanContainNull())
			{
				if ((annotations & FlowAnalysisAnnotations.MaybeNull) == FlowAnalysisAnnotations.MaybeNull)
				{
					nullableFlowState = NullableFlowState.MaybeDefault;
				}
				else
				{
					if ((annotations & FlowAnalysisAnnotations.NotNull) != FlowAnalysisAnnotations.NotNull)
					{
						return typeWithAnnotations.ToTypeWithState();
					}
					nullableFlowState = NullableFlowState.NotNull;
				}
			}
			else
			{
				nullableFlowState = NullableFlowState.NotNull;
			}
			return TypeWithState.Create(type, nullableFlowState);
		}

		// Token: 0x060079F9 RID: 31225 RVA: 0x001BE82E File Offset: 0x001BCA2E
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		private TypeWithState(TypeSymbol type, NullableFlowState state)
		{
			this.Type = type;
			this.State = state;
		}

		// Token: 0x060079FA RID: 31226 RVA: 0x001BE83E File Offset: 0x001BCA3E
		public string GetDebuggerDisplay()
		{
			string text = "{{Type:{0}, State:{1}{2}";
			TypeSymbol type = this.Type;
			return string.Format(text, (type != null) ? type.GetDebuggerDisplay() : null, this.State, "}");
		}

		// Token: 0x060079FB RID: 31227 RVA: 0x001BE86C File Offset: 0x001BCA6C
		public override string ToString()
		{
			return this.GetDebuggerDisplay();
		}

		// Token: 0x060079FC RID: 31228 RVA: 0x001BE874 File Offset: 0x001BCA74
		public TypeWithState WithNotNullState()
		{
			return new TypeWithState(this.Type, NullableFlowState.NotNull);
		}

		// Token: 0x060079FD RID: 31229 RVA: 0x001BE882 File Offset: 0x001BCA82
		public TypeWithState WithSuppression(bool suppress)
		{
			if (!suppress)
			{
				return this;
			}
			return new TypeWithState(this.Type, NullableFlowState.NotNull);
		}

		// Token: 0x060079FE RID: 31230 RVA: 0x001BE89C File Offset: 0x001BCA9C
		public TypeWithAnnotations ToTypeWithAnnotations(CSharpCompilation compilation, bool asAnnotatedType = false)
		{
			TypeSymbol type = this.Type;
			if (type == null || !type.IsTypeParameterDisallowingAnnotationInCSharp8())
			{
				NullableAnnotation nullableAnnotation;
				if (!asAnnotatedType)
				{
					if (!this.State.IsNotNull())
					{
						TypeSymbol type2 = this.Type;
						if (type2 == null || type2.CanContainNull())
						{
							nullableAnnotation = NullableAnnotation.Annotated;
							goto IL_0087;
						}
					}
					nullableAnnotation = NullableAnnotation.NotAnnotated;
				}
				else
				{
					TypeSymbol type3 = this.Type;
					nullableAnnotation = ((type3 != null && type3.IsValueType) ? NullableAnnotation.NotAnnotated : NullableAnnotation.Annotated);
				}
				IL_0087:
				NullableAnnotation nullableAnnotation2 = nullableAnnotation;
				return TypeWithAnnotations.Create(this.Type, nullableAnnotation2, default(ImmutableArray<CustomModifier>));
			}
			TypeWithAnnotations typeWithAnnotations = TypeWithAnnotations.Create(this.Type, NullableAnnotation.NotAnnotated, default(ImmutableArray<CustomModifier>));
			if (this.State != NullableFlowState.MaybeDefault && !asAnnotatedType)
			{
				return typeWithAnnotations;
			}
			return typeWithAnnotations.SetIsAnnotated(compilation);
		}

		// Token: 0x060079FF RID: 31231 RVA: 0x001BE946 File Offset: 0x001BCB46
		public TypeWithAnnotations ToAnnotatedTypeWithAnnotations(CSharpCompilation compilation)
		{
			return this.ToTypeWithAnnotations(compilation, true);
		}

		// Token: 0x04001D24 RID: 7460
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public readonly TypeSymbol Type;

		// Token: 0x04001D25 RID: 7461
		public readonly NullableFlowState State;
	}
}
