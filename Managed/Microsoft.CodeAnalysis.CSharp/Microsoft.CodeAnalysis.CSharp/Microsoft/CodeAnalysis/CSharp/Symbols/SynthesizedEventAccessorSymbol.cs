using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200058E RID: 1422
	internal sealed class SynthesizedEventAccessorSymbol : SourceEventAccessorSymbol
	{
		// Token: 0x060076A8 RID: 30376 RVA: 0x001B7940 File Offset: 0x001B5B40
		internal SynthesizedEventAccessorSymbol(SourceEventSymbol @event, bool isAdder, EventSymbol explicitlyImplementedEventOpt = null, string aliasQualifierOpt = null)
			: base(@event, null, @event.Locations, explicitlyImplementedEventOpt, aliasQualifierOpt, isAdder, false, false)
		{
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x060076A9 RID: 30377 RVA: 0x001B796C File Offset: 0x001B5B6C
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x060076AA RID: 30378 RVA: 0x001B796F File Offset: 0x001B5B6F
		internal override bool GenerateDebugInfo
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x060076AB RID: 30379 RVA: 0x001B7972 File Offset: 0x001B5B72
		protected override SourceMemberMethodSymbol BoundAttributesSource
		{
			get
			{
				if (this.MethodKind != MethodKind.EventAdd)
				{
					return null;
				}
				return (SourceMemberMethodSymbol)base.AssociatedEvent.RemoveMethod;
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x060076AC RID: 30380 RVA: 0x001B798F File Offset: 0x001B5B8F
		protected override IAttributeTargetSymbol AttributeOwner
		{
			get
			{
				return base.AssociatedEvent;
			}
		}

		// Token: 0x060076AD RID: 30381 RVA: 0x001B7997 File Offset: 0x001B5B97
		internal override OneOrMany<SyntaxList<AttributeListSyntax>> GetAttributeDeclarations()
		{
			return OneOrMany.Create<SyntaxList<AttributeListSyntax>>(base.AssociatedEvent.AttributeDeclarationSyntaxList);
		}

		// Token: 0x060076AE RID: 30382 RVA: 0x001B79AC File Offset: 0x001B5BAC
		internal override void AddSynthesizedAttributes(PEModuleBuilder moduleBuilder, ref ArrayBuilder<SynthesizedAttributeData> attributes)
		{
			base.AddSynthesizedAttributes(moduleBuilder, ref attributes);
			CSharpCompilation declaringCompilation = this.DeclaringCompilation;
			Symbol.AddSynthesizedAttribute(ref attributes, declaringCompilation.TrySynthesizeAttribute(WellKnownMember.System_Runtime_CompilerServices_CompilerGeneratedAttribute__ctor, default(ImmutableArray<TypedConstant>), default(ImmutableArray<KeyValuePair<WellKnownMember, TypedConstant>>), false));
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x060076AF RID: 30383 RVA: 0x001B79E9 File Offset: 0x001B5BE9
		protected override object MethodChecksLockObject
		{
			get
			{
				return this._methodChecksLockObject;
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x060076B0 RID: 30384 RVA: 0x001B79F4 File Offset: 0x001B5BF4
		internal override MethodImplAttributes ImplementationAttributes
		{
			get
			{
				MethodImplAttributes methodImplAttributes = base.ImplementationAttributes;
				if (!this.IsAbstract && !base.AssociatedEvent.IsWindowsRuntimeEvent && !this.ContainingType.IsStructType() && this.DeclaringCompilation.GetWellKnownTypeMember(WellKnownMember.System_Threading_Interlocked__CompareExchange_T) == null)
				{
					methodImplAttributes |= 32;
				}
				return methodImplAttributes;
			}
		}

		// Token: 0x04001CC8 RID: 7368
		private readonly object _methodChecksLockObject = new object();
	}
}
