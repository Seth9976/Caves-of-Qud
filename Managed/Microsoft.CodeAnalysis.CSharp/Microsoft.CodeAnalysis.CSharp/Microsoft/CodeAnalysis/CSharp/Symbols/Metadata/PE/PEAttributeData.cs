using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols.Metadata.PE
{
	// Token: 0x020005E0 RID: 1504
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PEAttributeData : CSharpAttributeData
	{
		// Token: 0x06007D94 RID: 32148 RVA: 0x001C3528 File Offset: 0x001C1728
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal PEAttributeData(PEModuleSymbol moduleSymbol, CustomAttributeHandle handle)
		{
			this._decoder = new MetadataDecoder(moduleSymbol);
			this._handle = handle;
		}

		// Token: 0x17002518 RID: 9496
		// (get) Token: 0x06007D95 RID: 32149 RVA: 0x001C354E File Offset: 0x001C174E
		public override NamedTypeSymbol AttributeClass
		{
			get
			{
				this.EnsureClassAndConstructorSymbolsAreLoaded();
				return this._lazyAttributeClass;
			}
		}

		// Token: 0x17002519 RID: 9497
		// (get) Token: 0x06007D96 RID: 32150 RVA: 0x001C355C File Offset: 0x001C175C
		public override MethodSymbol AttributeConstructor
		{
			get
			{
				this.EnsureClassAndConstructorSymbolsAreLoaded();
				return this._lazyAttributeConstructor;
			}
		}

		// Token: 0x1700251A RID: 9498
		// (get) Token: 0x06007D97 RID: 32151 RVA: 0x001C356A File Offset: 0x001C176A
		public override SyntaxReference ApplicationSyntaxReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700251B RID: 9499
		// (get) Token: 0x06007D98 RID: 32152 RVA: 0x001C356D File Offset: 0x001C176D
		[global::System.Runtime.CompilerServices.Nullable(0)]
		protected internal override ImmutableArray<TypedConstant> CommonConstructorArguments
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get
			{
				this.EnsureAttributeArgumentsAreLoaded();
				return this._lazyConstructorArguments;
			}
		}

		// Token: 0x1700251C RID: 9500
		// (get) Token: 0x06007D99 RID: 32153 RVA: 0x001C357B File Offset: 0x001C177B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		protected internal override ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
			get
			{
				this.EnsureAttributeArgumentsAreLoaded();
				return this._lazyNamedArguments;
			}
		}

		// Token: 0x06007D9A RID: 32154 RVA: 0x001C358C File Offset: 0x001C178C
		private void EnsureClassAndConstructorSymbolsAreLoaded()
		{
			if (this._lazyAttributeClass == ErrorTypeSymbol.UnknownResultType)
			{
				TypeSymbol typeSymbol;
				MethodSymbol methodSymbol;
				if (!this._decoder.GetCustomAttribute(this._handle, out typeSymbol, out methodSymbol))
				{
					this._lazyHasErrors = ThreeState.True;
				}
				else if (typeSymbol == null || typeSymbol.IsErrorType() || methodSymbol == null)
				{
					this._lazyHasErrors = ThreeState.True;
				}
				Interlocked.CompareExchange<MethodSymbol>(ref this._lazyAttributeConstructor, methodSymbol, null);
				Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazyAttributeClass, (NamedTypeSymbol)typeSymbol, ErrorTypeSymbol.UnknownResultType);
			}
		}

		// Token: 0x06007D9B RID: 32155 RVA: 0x001C3600 File Offset: 0x001C1800
		private void EnsureAttributeArgumentsAreLoaded()
		{
			if (this._lazyConstructorArguments.IsDefault || this._lazyNamedArguments.IsDefault)
			{
				TypedConstant[] array = null;
				KeyValuePair<string, TypedConstant>[] array2 = null;
				if (!this._decoder.GetCustomAttribute(this._handle, out array, out array2))
				{
					this._lazyHasErrors = ThreeState.True;
				}
				ImmutableInterlocked.InterlockedInitialize<TypedConstant>(ref this._lazyConstructorArguments, ImmutableArray.Create<TypedConstant>(array));
				ImmutableInterlocked.InterlockedInitialize<KeyValuePair<string, TypedConstant>>(ref this._lazyNamedArguments, ImmutableArray.Create<KeyValuePair<string, TypedConstant>>(array2));
			}
		}

		// Token: 0x06007D9C RID: 32156 RVA: 0x001C366D File Offset: 0x001C186D
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override bool IsTargetAttribute(string namespaceName, string typeName)
		{
			return this._decoder.IsTargetAttribute(this._handle, namespaceName, typeName, false);
		}

		// Token: 0x06007D9D RID: 32157 RVA: 0x001C3683 File Offset: 0x001C1883
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override int GetTargetAttributeSignatureIndex(Symbol targetSymbol, AttributeDescription description)
		{
			return this._decoder.GetTargetAttributeSignatureIndex(this._handle, description);
		}

		// Token: 0x1700251D RID: 9501
		// (get) Token: 0x06007D9E RID: 32158 RVA: 0x001C3697 File Offset: 0x001C1897
		[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
		internal override bool HasErrors
		{
			[MemberNotNullWhen(true, new string[] { "AttributeClass", "AttributeConstructor" })]
			get
			{
				if (this._lazyHasErrors == ThreeState.Unknown)
				{
					this.EnsureClassAndConstructorSymbolsAreLoaded();
					this.EnsureAttributeArgumentsAreLoaded();
					if (this._lazyHasErrors == ThreeState.Unknown)
					{
						this._lazyHasErrors = ThreeState.False;
					}
				}
				return this._lazyHasErrors.Value();
			}
		}

		// Token: 0x04001D95 RID: 7573
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly MetadataDecoder _decoder;

		// Token: 0x04001D96 RID: 7574
		private readonly CustomAttributeHandle _handle;

		// Token: 0x04001D97 RID: 7575
		private NamedTypeSymbol _lazyAttributeClass = ErrorTypeSymbol.UnknownResultType;

		// Token: 0x04001D98 RID: 7576
		private MethodSymbol _lazyAttributeConstructor;

		// Token: 0x04001D99 RID: 7577
		[global::System.Runtime.CompilerServices.Nullable(0)]
		private ImmutableArray<TypedConstant> _lazyConstructorArguments;

		// Token: 0x04001D9A RID: 7578
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 0, 1 })]
		private ImmutableArray<KeyValuePair<string, TypedConstant>> _lazyNamedArguments;

		// Token: 0x04001D9B RID: 7579
		private ThreeState _lazyHasErrors;
	}
}
