using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200052F RID: 1327
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SourceCustomEventSymbol : SourceEventSymbol
	{
		// Token: 0x06006F6F RID: 28527 RVA: 0x001998E0 File Offset: 0x00197AE0
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal SourceCustomEventSymbol(SourceMemberContainerTypeSymbol containingType, Binder binder, EventDeclarationSyntax syntax, BindingDiagnosticBag diagnostics)
			: base(containingType, syntax, syntax.Modifiers, false, syntax.ExplicitInterfaceSpecifier, syntax.Identifier, diagnostics)
		{
			ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = syntax.ExplicitInterfaceSpecifier;
			SyntaxToken identifier = syntax.Identifier;
			bool flag = explicitInterfaceSpecifier != null;
			string text;
			this._name = ExplicitInterfaceHelpers.GetMemberNameAndInterfaceSymbol(binder, explicitInterfaceSpecifier, identifier.ValueText, diagnostics, out this._explicitInterfaceType, out text);
			this._type = base.BindEventType(binder, syntax.Type, diagnostics);
			EventSymbol eventSymbol = this.FindExplicitlyImplementedEvent(this._explicitInterfaceType, identifier.ValueText, explicitInterfaceSpecifier, diagnostics);
			this.FindExplicitlyImplementedMemberVerification(eventSymbol, diagnostics);
			if (!flag)
			{
				if (this.IsOverride)
				{
					EventSymbol overriddenEvent = base.OverriddenEvent;
					if (overriddenEvent != null)
					{
						SourceEventSymbol.CopyEventCustomModifiers(overriddenEvent, ref this._type, this.ContainingAssembly);
					}
				}
			}
			else if (eventSymbol != null)
			{
				SourceEventSymbol.CopyEventCustomModifiers(eventSymbol, ref this._type, this.ContainingAssembly);
			}
			AccessorDeclarationSyntax accessorDeclarationSyntax = null;
			AccessorDeclarationSyntax accessorDeclarationSyntax2 = null;
			if (syntax.AccessorList != null)
			{
				foreach (AccessorDeclarationSyntax accessorDeclarationSyntax3 in syntax.AccessorList.Accessors)
				{
					bool flag2 = false;
					SyntaxKind syntaxKind = accessorDeclarationSyntax3.Kind();
					switch (syntaxKind)
					{
					case SyntaxKind.GetAccessorDeclaration:
					case SyntaxKind.SetAccessorDeclaration:
						goto IL_018D;
					case SyntaxKind.AddAccessorDeclaration:
						if (accessorDeclarationSyntax == null)
						{
							accessorDeclarationSyntax = accessorDeclarationSyntax3;
							flag2 = true;
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_DuplicateAccessor, accessorDeclarationSyntax3.Keyword.GetLocation());
						}
						break;
					case SyntaxKind.RemoveAccessorDeclaration:
						if (accessorDeclarationSyntax2 == null)
						{
							accessorDeclarationSyntax2 = accessorDeclarationSyntax3;
							flag2 = true;
						}
						else
						{
							diagnostics.Add(ErrorCode.ERR_DuplicateAccessor, accessorDeclarationSyntax3.Keyword.GetLocation());
						}
						break;
					case SyntaxKind.UnknownAccessorDeclaration:
						break;
					default:
						if (syntaxKind != SyntaxKind.InitAccessorDeclaration)
						{
							throw ExceptionUtilities.UnexpectedValue(accessorDeclarationSyntax3.Kind());
						}
						goto IL_018D;
					}
					IL_01BE:
					if (flag2 && !this.IsAbstract && accessorDeclarationSyntax3.Body == null && accessorDeclarationSyntax3.ExpressionBody == null && accessorDeclarationSyntax3.SemicolonToken.Kind() == SyntaxKind.SemicolonToken)
					{
						diagnostics.Add(ErrorCode.ERR_AddRemoveMustHaveBody, accessorDeclarationSyntax3.SemicolonToken.GetLocation());
						continue;
					}
					continue;
					IL_018D:
					diagnostics.Add(ErrorCode.ERR_AddOrRemoveExpected, accessorDeclarationSyntax3.Keyword.GetLocation());
					goto IL_01BE;
				}
				if (this.IsAbstract)
				{
					if (!syntax.AccessorList.OpenBraceToken.IsMissing)
					{
						diagnostics.Add(ErrorCode.ERR_AbstractEventHasAccessors, syntax.AccessorList.OpenBraceToken.GetLocation(), new object[] { this });
					}
				}
				else if ((accessorDeclarationSyntax == null || accessorDeclarationSyntax2 == null) && (!syntax.AccessorList.OpenBraceToken.IsMissing || !flag))
				{
					diagnostics.Add(ErrorCode.ERR_EventNeedsBothAccessors, this.Locations[0], new object[] { this });
				}
			}
			else if (flag && !this.IsAbstract)
			{
				diagnostics.Add(ErrorCode.ERR_ExplicitEventFieldImpl, this.Locations[0]);
			}
			if (flag && this.IsAbstract && syntax.AccessorList == null)
			{
				Binder.CheckFeatureAvailability(syntax, MessageID.IDS_DefaultInterfaceImplementation, diagnostics, this.Locations[0]);
				if (!this.ContainingAssembly.RuntimeSupportsDefaultInterfaceImplementation)
				{
					diagnostics.Add(ErrorCode.ERR_RuntimeDoesNotSupportDefaultInterfaceImplementation, this.Locations[0]);
				}
				this._addMethod = new SynthesizedEventAccessorSymbol(this, true, eventSymbol, text);
				this._removeMethod = new SynthesizedEventAccessorSymbol(this, false, eventSymbol, text);
			}
			else
			{
				this._addMethod = this.CreateAccessorSymbol(this.DeclaringCompilation, accessorDeclarationSyntax, eventSymbol, text, diagnostics);
				this._removeMethod = this.CreateAccessorSymbol(this.DeclaringCompilation, accessorDeclarationSyntax2, eventSymbol, text, diagnostics);
			}
			this._explicitInterfaceImplementations = ((eventSymbol == null) ? ImmutableArray<EventSymbol>.Empty : ImmutableArray.Create<EventSymbol>(eventSymbol));
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06006F70 RID: 28528 RVA: 0x00199C83 File Offset: 0x00197E83
		public override TypeWithAnnotations TypeWithAnnotations
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06006F71 RID: 28529 RVA: 0x00199C8B File Offset: 0x00197E8B
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public override string Name
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06006F72 RID: 28530 RVA: 0x00199C93 File Offset: 0x00197E93
		public override MethodSymbol AddMethod
		{
			get
			{
				return this._addMethod;
			}
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06006F73 RID: 28531 RVA: 0x00199C9B File Offset: 0x00197E9B
		public override MethodSymbol RemoveMethod
		{
			get
			{
				return this._removeMethod;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06006F74 RID: 28532 RVA: 0x00199CA3 File Offset: 0x00197EA3
		protected override AttributeLocation AllowedAttributeLocations
		{
			get
			{
				return AttributeLocation.Event;
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06006F75 RID: 28533 RVA: 0x00199CA7 File Offset: 0x00197EA7
		private ExplicitInterfaceSpecifierSyntax ExplicitInterfaceSpecifier
		{
			get
			{
				return ((EventDeclarationSyntax)base.CSharpSyntaxNode).ExplicitInterfaceSpecifier;
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x06006F76 RID: 28534 RVA: 0x00199CB9 File Offset: 0x00197EB9
		internal override bool IsExplicitInterfaceImplementation
		{
			get
			{
				return this.ExplicitInterfaceSpecifier != null;
			}
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x06006F77 RID: 28535 RVA: 0x00199CC4 File Offset: 0x00197EC4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<EventSymbol> ExplicitInterfaceImplementations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._explicitInterfaceImplementations;
			}
		}

		// Token: 0x06006F78 RID: 28536 RVA: 0x00199CCC File Offset: 0x00197ECC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal override void AfterAddingTypeMembersChecks(ConversionsBase conversions, BindingDiagnosticBag diagnostics)
		{
			base.AfterAddingTypeMembersChecks(conversions, diagnostics);
			if (this._explicitInterfaceType != null)
			{
				ExplicitInterfaceSpecifierSyntax explicitInterfaceSpecifier = this.ExplicitInterfaceSpecifier;
				this._explicitInterfaceType.CheckAllConstraints(this.DeclaringCompilation, conversions, new SourceLocation(explicitInterfaceSpecifier.Name), diagnostics);
			}
			if (!this._explicitInterfaceImplementations.IsEmpty)
			{
				EventSymbol eventSymbol = this._explicitInterfaceImplementations[0];
				TypeSymbol.CheckNullableReferenceTypeMismatchOnImplementingMember(this.ContainingType, this, eventSymbol, true, diagnostics);
			}
		}

		// Token: 0x06006F79 RID: 28537 RVA: 0x00199D3D File Offset: 0x00197F3D
		[return: NotNullIfNotNull("syntaxOpt")]
		private SourceCustomEventAccessorSymbol CreateAccessorSymbol([global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, AccessorDeclarationSyntax syntaxOpt, EventSymbol explicitlyImplementedEventOpt, string aliasQualifierOpt, [global::System.Runtime.CompilerServices.Nullable(1)] BindingDiagnosticBag diagnostics)
		{
			if (syntaxOpt == null)
			{
				return null;
			}
			return new SourceCustomEventAccessorSymbol(this, syntaxOpt, explicitlyImplementedEventOpt, aliasQualifierOpt, compilation.IsNullableAnalysisEnabledIn(syntaxOpt), diagnostics);
		}

		// Token: 0x04001B52 RID: 6994
		private readonly TypeWithAnnotations _type;

		// Token: 0x04001B53 RID: 6995
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly string _name;

		// Token: 0x04001B54 RID: 6996
		private readonly SourceEventAccessorSymbol _addMethod;

		// Token: 0x04001B55 RID: 6997
		private readonly SourceEventAccessorSymbol _removeMethod;

		// Token: 0x04001B56 RID: 6998
		[global::System.Runtime.CompilerServices.Nullable(1)]
		private readonly TypeSymbol _explicitInterfaceType;

		// Token: 0x04001B57 RID: 6999
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<EventSymbol> _explicitInterfaceImplementations;
	}
}
