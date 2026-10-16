using System;
using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000244 RID: 580
	[NullableContext(2)]
	public interface IMethodSymbol : ISymbol, IEquatable<ISymbol>
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001645 RID: 5701
		MethodKind MethodKind { get; }

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001646 RID: 5702
		int Arity { get; }

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001647 RID: 5703
		bool IsGenericMethod { get; }

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001648 RID: 5704
		bool IsExtensionMethod { get; }

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001649 RID: 5705
		bool IsAsync { get; }

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600164A RID: 5706
		bool IsVararg { get; }

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x0600164B RID: 5707
		bool IsCheckedBuiltin { get; }

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x0600164C RID: 5708
		bool HidesBaseMethodsByName { get; }

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600164D RID: 5709
		bool ReturnsVoid { get; }

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600164E RID: 5710
		bool ReturnsByRef { get; }

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600164F RID: 5711
		bool ReturnsByRefReadonly { get; }

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001650 RID: 5712
		RefKind RefKind { get; }

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001651 RID: 5713
		[Nullable(1)]
		ITypeSymbol ReturnType
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001652 RID: 5714
		NullableAnnotation ReturnNullableAnnotation { get; }

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001653 RID: 5715
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeSymbol> TypeArguments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001654 RID: 5716
		[Nullable(0)]
		ImmutableArray<NullableAnnotation> TypeArgumentNullableAnnotations
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001655 RID: 5717
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<ITypeParameterSymbol> TypeParameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001656 RID: 5718
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterSymbol> Parameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x06001657 RID: 5719
		[Nullable(1)]
		IMethodSymbol ConstructedFrom
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001658 RID: 5720
		bool IsReadOnly { get; }

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001659 RID: 5721
		bool IsInitOnly { get; }

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600165A RID: 5722
		[Nullable(1)]
		IMethodSymbol OriginalDefinition
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x0600165B RID: 5723
		IMethodSymbol OverriddenMethod { get; }

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600165C RID: 5724
		ITypeSymbol ReceiverType { get; }

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600165D RID: 5725
		NullableAnnotation ReceiverNullableAnnotation { get; }

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600165E RID: 5726
		IMethodSymbol ReducedFrom { get; }

		// Token: 0x0600165F RID: 5727
		[NullableContext(1)]
		[return: Nullable(2)]
		ITypeSymbol GetTypeInferredDuringReduction(ITypeParameterSymbol reducedFromTypeParameter);

		// Token: 0x06001660 RID: 5728
		[NullableContext(1)]
		[return: Nullable(2)]
		IMethodSymbol ReduceExtensionMethod(ITypeSymbol receiverType);

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001661 RID: 5729
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IMethodSymbol> ExplicitInterfaceImplementations
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001662 RID: 5730
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> ReturnTypeCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06001663 RID: 5731
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001664 RID: 5732
		[return: Nullable(new byte[] { 0, 1 })]
		ImmutableArray<AttributeData> GetReturnTypeAttributes();

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06001665 RID: 5733
		SignatureCallingConvention CallingConvention { get; }

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06001666 RID: 5734
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<INamedTypeSymbol> UnmanagedCallingConventionTypes
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06001667 RID: 5735
		ISymbol AssociatedSymbol { get; }

		// Token: 0x06001668 RID: 5736
		[NullableContext(1)]
		IMethodSymbol Construct(params ITypeSymbol[] typeArguments);

		// Token: 0x06001669 RID: 5737
		[NullableContext(0)]
		[return: Nullable(1)]
		IMethodSymbol Construct([Nullable(new byte[] { 0, 1 })] ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations);

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x0600166A RID: 5738
		IMethodSymbol PartialDefinitionPart { get; }

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x0600166B RID: 5739
		IMethodSymbol PartialImplementationPart { get; }

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x0600166C RID: 5740
		MethodImplAttributes MethodImplementationFlags { get; }

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x0600166D RID: 5741
		bool IsPartialDefinition { get; }

		// Token: 0x0600166E RID: 5742
		DllImportData GetDllImportData();

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x0600166F RID: 5743
		INamedTypeSymbol AssociatedAnonymousDelegate { get; }

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06001670 RID: 5744
		bool IsConditional { get; }
	}
}
