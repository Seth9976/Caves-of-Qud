using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004BE RID: 1214
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class ArrayMethod : IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x06003096 RID: 12438 RVA: 0x0008A71F File Offset: 0x0008891F
		protected ArrayMethod(IArrayTypeReference arrayType)
		{
			this.arrayType = arrayType;
			this._parameters = this.MakeParameters();
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003097 RID: 12439
		public abstract string Name { get; }

		// Token: 0x06003098 RID: 12440
		public abstract ITypeReference GetType(EmitContext context);

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0008A73A File Offset: 0x0008893A
		public virtual bool ReturnValueIsByRef
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0008A740 File Offset: 0x00088940
		[return: Nullable(new byte[] { 0, 1 })]
		protected virtual ImmutableArray<ArrayMethodParameterInfo> MakeParameters()
		{
			int rank = this.arrayType.Rank;
			ArrayBuilder<ArrayMethodParameterInfo> instance = ArrayBuilder<ArrayMethodParameterInfo>.GetInstance(rank);
			for (int i = 0; i < rank; i++)
			{
				instance.Add(ArrayMethodParameterInfo.GetIndexParameter((ushort)i));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0008A77F File Offset: 0x0008897F
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IParameterTypeInformation> GetParameters(EmitContext context)
		{
			return StaticCast<IParameterTypeInformation>.From<ArrayMethodParameterInfo>(this._parameters);
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x0008A78C File Offset: 0x0008898C
		public bool AcceptsExtraArguments
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0008A78F File Offset: 0x0008898F
		public ushort GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0008A792 File Offset: 0x00088992
		public bool IsGeneric
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0008A795 File Offset: 0x00088995
		[NullableContext(2)]
		public IMethodDefinition GetResolvedMethod(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x0008A798 File Offset: 0x00088998
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<IParameterTypeInformation> ExtraParameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<IParameterTypeInformation>.Empty;
			}
		}

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x0008A79F File Offset: 0x0008899F
		[Nullable(2)]
		public IGenericMethodInstanceReference AsGenericMethodInstanceReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x0008A7A2 File Offset: 0x000889A2
		[Nullable(2)]
		public ISpecializedMethodReference AsSpecializedMethodReference
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x0008A7A5 File Offset: 0x000889A5
		public CallingConvention CallingConvention
		{
			get
			{
				return CallingConvention.HasThis;
			}
		}

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x0008A7AC File Offset: 0x000889AC
		public ushort ParameterCount
		{
			get
			{
				return (ushort)this._parameters.Length;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x060030A5 RID: 12453 RVA: 0x0008A7C8 File Offset: 0x000889C8
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x0008A7CF File Offset: 0x000889CF
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> ReturnValueCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x0008A7D6 File Offset: 0x000889D6
		public ITypeReference GetContainingType(EmitContext context)
		{
			return this.arrayType;
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x0008A7DE File Offset: 0x000889DE
		public IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x0008A7E5 File Offset: 0x000889E5
		public void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x0008A7EE File Offset: 0x000889EE
		[NullableContext(2)]
		public IDefinition AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x0008A7F1 File Offset: 0x000889F1
		public override string ToString()
		{
			return (this.arrayType.GetInternalSymbol() ?? this.arrayType).ToString() + "." + this.Name;
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x0008A81D File Offset: 0x00088A1D
		[NullableContext(2)]
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x0008A820 File Offset: 0x00088A20
		[NullableContext(2)]
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x0008A827 File Offset: 0x00088A27
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x040012D7 RID: 4823
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ArrayMethodParameterInfo> _parameters;

		// Token: 0x040012D8 RID: 4824
		protected readonly IArrayTypeReference arrayType;
	}
}
