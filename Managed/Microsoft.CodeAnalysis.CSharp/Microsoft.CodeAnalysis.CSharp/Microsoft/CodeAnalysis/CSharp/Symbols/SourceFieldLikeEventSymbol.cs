using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000536 RID: 1334
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SourceFieldLikeEventSymbol : SourceEventSymbol
	{
		// Token: 0x06006FFA RID: 28666 RVA: 0x0019B4F8 File Offset: 0x001996F8
		internal SourceFieldLikeEventSymbol(SourceMemberContainerTypeSymbol containingType, Binder binder, SyntaxTokenList modifiers, VariableDeclaratorSyntax declaratorSyntax, BindingDiagnosticBag diagnostics)
			: base(containingType, declaratorSyntax, modifiers, true, null, declaratorSyntax.Identifier, diagnostics)
		{
			this._name = declaratorSyntax.Identifier.ValueText;
			BindingDiagnosticBag instance = BindingDiagnosticBag.GetInstance();
			VariableDeclarationSyntax variableDeclarationSyntax = (VariableDeclarationSyntax)declaratorSyntax.Parent;
			this._type = base.BindEventType(binder, variableDeclarationSyntax.Type, instance);
			if (this.IsOverride)
			{
				EventSymbol overriddenEvent = base.OverriddenEvent;
				if (overriddenEvent != null)
				{
					SourceEventSymbol.CopyEventCustomModifiers(overriddenEvent, ref this._type, this.ContainingAssembly);
				}
			}
			bool flag = declaratorSyntax.Initializer != null;
			bool flag2 = containingType.IsInterfaceType();
			if (flag)
			{
				if (flag2 && !this.IsStatic)
				{
					diagnostics.Add(ErrorCode.ERR_InterfaceEventInitializer, this.Locations[0], new object[] { this });
				}
				else if (this.IsAbstract)
				{
					diagnostics.Add(ErrorCode.ERR_AbstractEventInitializer, this.Locations[0], new object[] { this });
				}
				else if (this.IsExtern)
				{
					diagnostics.Add(ErrorCode.ERR_ExternEventInitializer, this.Locations[0], new object[] { this });
				}
			}
			if (flag || (!this.IsExtern && !this.IsAbstract))
			{
				this.AssociatedEventField = this.MakeAssociatedField(declaratorSyntax);
			}
			if (!this.IsStatic && this.ContainingType.IsReadOnly)
			{
				diagnostics.Add(ErrorCode.ERR_FieldlikeEventsInRoStruct, this.Locations[0]);
			}
			if (flag2)
			{
				if (this.IsExtern || this.IsStatic)
				{
					if (!this.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
					{
						diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation, this.Locations[0]);
					}
				}
				else if (!this.IsAbstract)
				{
					diagnostics.Add(ErrorCode.ERR_EventNeedsBothAccessors, this.Locations[0], new object[] { this });
				}
			}
			this._addMethod = new SynthesizedEventAccessorSymbol(this, true, null, null);
			this._removeMethod = new SynthesizedEventAccessorSymbol(this, false, null, null);
			if (variableDeclarationSyntax.Variables[0] == declaratorSyntax)
			{
				diagnostics.AddRange(instance, false);
			}
			instance.Free();
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06006FFB RID: 28667 RVA: 0x0019B723 File Offset: 0x00199923
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override FieldSymbol AssociatedField
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.AssociatedEventField;
			}
		}

		// Token: 0x17001E0B RID: 7691
		// (get) Token: 0x06006FFC RID: 28668 RVA: 0x0019B72B File Offset: 0x0019992B
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal SourceEventFieldSymbol AssociatedEventField
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x17001E0C RID: 7692
		// (get) Token: 0x06006FFD RID: 28669 RVA: 0x0019B733 File Offset: 0x00199933
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06006FFE RID: 28670 RVA: 0x0019B73B File Offset: 0x0019993B
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001E0E RID: 7694
		// (get) Token: 0x06006FFF RID: 28671 RVA: 0x0019B743 File Offset: 0x00199943
		public override MethodSymbol AddMethod
		{
			get
			{
				return this._addMethod;
			}
		}

		// Token: 0x17001E0F RID: 7695
		// (get) Token: 0x06007000 RID: 28672 RVA: 0x0019B74B File Offset: 0x0019994B
		public override MethodSymbol RemoveMethod
		{
			get
			{
				return this._removeMethod;
			}
		}

		// Token: 0x17001E10 RID: 7696
		// (get) Token: 0x06007001 RID: 28673 RVA: 0x0019B753 File Offset: 0x00199953
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001E11 RID: 7697
		// (get) Token: 0x06007002 RID: 28674 RVA: 0x0019B756 File Offset: 0x00199956
		protected override AttributeLocation AllowedAttributeLocations
		{
			get
			{
				if (this.AssociatedEventField == null)
				{
					return AttributeLocation.Method | AttributeLocation.Event;
				}
				return AttributeLocation.Method | AttributeLocation.Field | AttributeLocation.Event;
			}
		}

		// Token: 0x17001E12 RID: 7698
		// (get) Token: 0x06007003 RID: 28675 RVA: 0x0019B765 File Offset: 0x00199965
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<EventSymbol>.Empty;
			}
		}

		// Token: 0x06007004 RID: 28676 RVA: 0x0019B76C File Offset: 0x0019996C
		private SourceEventFieldSymbol MakeAssociatedField(VariableDeclaratorSyntax declaratorSyntax)
		{
			return new SourceEventFieldSymbol(this, declaratorSyntax, BindingDiagnosticBag.Discarded);
		}

		// Token: 0x06007005 RID: 28677 RVA: 0x0019B77A File Offset: 0x0019997A
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			if (this.AssociatedField != null)
			{
				this.AssociatedField.ForceComplete(locationOpt, cancellationToken);
			}
			base.ForceComplete(locationOpt, cancellationToken);
		}

		// Token: 0x04001B6C RID: 7020
		private readonly string _name;

		// Token: 0x04001B6D RID: 7021
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001B6E RID: 7022
		private readonly SynthesizedEventAccessorSymbol _addMethod;

		// Token: 0x04001B6F RID: 7023
		private readonly SynthesizedEventAccessorSymbol _removeMethod;
	}
}
