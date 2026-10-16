using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000538 RID: 1336
	internal abstract class SourceFieldSymbolWithSyntaxReference : SourceFieldSymbol
	{
		// Token: 0x06007013 RID: 28691 RVA: 0x0019B9B5 File Offset: 0x00199BB5
		protected SourceFieldSymbolWithSyntaxReference(SourceMemberContainerTypeSymbol containingType, string name, SyntaxReference syntax, Location location)
			: base(containingType)
		{
			this._name = name;
			this._syntaxReference = syntax;
			this._location = location;
		}

		// Token: 0x17001E1B RID: 7707
		// (get) Token: 0x06007014 RID: 28692 RVA: 0x0019B9EA File Offset: 0x00199BEA
		public SyntaxTree SyntaxTree
		{
			get
			{
				return this._syntaxReference.SyntaxTree;
			}
		}

		// Token: 0x17001E1C RID: 7708
		// (get) Token: 0x06007015 RID: 28693 RVA: 0x0019B9F8 File Offset: 0x00199BF8
		public CSharpSyntaxNode SyntaxNode
		{
			get
			{
				return (CSharpSyntaxNode)this._syntaxReference.GetSyntax(default(CancellationToken));
			}
		}

		// Token: 0x17001E1D RID: 7709
		// (get) Token: 0x06007016 RID: 28694 RVA: 0x0019BA1E File Offset: 0x00199C1E
		public sealed override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06007017 RID: 28695 RVA: 0x0019BA26 File Offset: 0x00199C26
		internal override LexicalSortKey GetLexicalSortKey()
		{
			return new LexicalSortKey(this._location, this.DeclaringCompilation);
		}

		// Token: 0x17001E1E RID: 7710
		// (get) Token: 0x06007018 RID: 28696 RVA: 0x0019BA39 File Offset: 0x00199C39
		public sealed override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray.Create<Location>(this._location);
			}
		}

		// Token: 0x17001E1F RID: 7711
		// (get) Token: 0x06007019 RID: 28697 RVA: 0x0019BA46 File Offset: 0x00199C46
		internal sealed override Location ErrorLocation
		{
			get
			{
				return this._location;
			}
		}

		// Token: 0x17001E20 RID: 7712
		// (get) Token: 0x0600701A RID: 28698 RVA: 0x0019BA4E File Offset: 0x00199C4E
		public sealed override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this._syntaxReference);
			}
		}

		// Token: 0x0600701B RID: 28699 RVA: 0x0019BA5C File Offset: 0x00199C5C
		public sealed override string GetDocumentationCommentXml(CultureInfo preferredCulture = null, bool expandIncludes = false, CancellationToken cancellationToken = default(CancellationToken))
		{
			ref string ptr = (ref expandIncludes ? ref this._lazyExpandedDocComment : ref this._lazyDocComment);
			return SourceDocumentationCommentUtils.GetAndCacheDocumentationComment(this, expandIncludes, ref ptr);
		}

		// Token: 0x0600701C RID: 28700 RVA: 0x0019BA84 File Offset: 0x00199C84
		internal sealed override ConstantValue GetConstantValue(ConstantFieldsInProgress inProgress, bool earlyDecodingWellKnownAttributes)
		{
			ConstantValue lazyConstantValue = this.GetLazyConstantValue(earlyDecodingWellKnownAttributes);
			if (lazyConstantValue != Microsoft.CodeAnalysis.ConstantValue.Unset)
			{
				return lazyConstantValue;
			}
			if (!inProgress.IsEmpty)
			{
				inProgress.AddDependency(this);
				return Microsoft.CodeAnalysis.ConstantValue.Unset;
			}
			ArrayBuilder<ConstantEvaluationHelpers.FieldInfo> instance = ArrayBuilder<ConstantEvaluationHelpers.FieldInfo>.GetInstance();
			this.OrderAllDependencies(instance, earlyDecodingWellKnownAttributes);
			foreach (ConstantEvaluationHelpers.FieldInfo fieldInfo in instance)
			{
				fieldInfo.Field.BindConstantValueIfNecessary(earlyDecodingWellKnownAttributes, fieldInfo.StartsCycle);
			}
			instance.Free();
			return this.GetLazyConstantValue(earlyDecodingWellKnownAttributes);
		}

		// Token: 0x0600701D RID: 28701 RVA: 0x0019BB04 File Offset: 0x00199D04
		internal ImmutableHashSet<SourceFieldSymbolWithSyntaxReference> GetConstantValueDependencies(bool earlyDecodingWellKnownAttributes)
		{
			ConstantValue constantValue = this.GetLazyConstantValue(earlyDecodingWellKnownAttributes);
			if (constantValue != Microsoft.CodeAnalysis.ConstantValue.Unset)
			{
				return ImmutableHashSet<SourceFieldSymbolWithSyntaxReference>.Empty;
			}
			PooledHashSet<SourceFieldSymbolWithSyntaxReference> instance = PooledHashSet<SourceFieldSymbolWithSyntaxReference>.GetInstance();
			BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
			constantValue = this.MakeConstantValue(instance, earlyDecodingWellKnownAttributes, instance2);
			ImmutableHashSet<SourceFieldSymbolWithSyntaxReference> immutableHashSet;
			if (instance.Count == 0 && constantValue != null && !constantValue.IsBad && constantValue != Microsoft.CodeAnalysis.ConstantValue.Unset && !instance2.HasAnyResolvedErrors())
			{
				this.SetLazyConstantValue(constantValue, earlyDecodingWellKnownAttributes, instance2, false);
				immutableHashSet = ImmutableHashSet<SourceFieldSymbolWithSyntaxReference>.Empty;
			}
			else
			{
				immutableHashSet = ImmutableHashSet<SourceFieldSymbolWithSyntaxReference>.Empty.Union(instance);
			}
			instance2.Free();
			instance.Free();
			return immutableHashSet;
		}

		// Token: 0x0600701E RID: 28702 RVA: 0x0019BB9C File Offset: 0x00199D9C
		private void BindConstantValueIfNecessary(bool earlyDecodingWellKnownAttributes, bool startsCycle)
		{
			if (this.GetLazyConstantValue(earlyDecodingWellKnownAttributes) != Microsoft.CodeAnalysis.ConstantValue.Unset)
			{
				return;
			}
			PooledHashSet<SourceFieldSymbolWithSyntaxReference> instance = PooledHashSet<SourceFieldSymbolWithSyntaxReference>.GetInstance();
			BindingDiagnosticBag instance2 = BindingDiagnosticBag.GetInstance();
			if (startsCycle)
			{
				instance2.Add(ErrorCode.ERR_CircConstValue, this._location, new object[] { this });
			}
			ConstantValue constantValue = this.MakeConstantValue(instance, earlyDecodingWellKnownAttributes, instance2);
			this.SetLazyConstantValue(constantValue, earlyDecodingWellKnownAttributes, instance2, startsCycle);
			instance2.Free();
			instance.Free();
		}

		// Token: 0x0600701F RID: 28703 RVA: 0x0019BC05 File Offset: 0x00199E05
		private ConstantValue GetLazyConstantValue(bool earlyDecodingWellKnownAttributes)
		{
			if (!earlyDecodingWellKnownAttributes)
			{
				return this._lazyConstantValue;
			}
			return this._lazyConstantEarlyDecodingValue;
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x0019BC18 File Offset: 0x00199E18
		private void SetLazyConstantValue(ConstantValue value, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics, bool startsCycle)
		{
			if (earlyDecodingWellKnownAttributes)
			{
				Interlocked.CompareExchange<ConstantValue>(ref this._lazyConstantEarlyDecodingValue, value, Microsoft.CodeAnalysis.ConstantValue.Unset);
				return;
			}
			if (Interlocked.CompareExchange<ConstantValue>(ref this._lazyConstantValue, value, Microsoft.CodeAnalysis.ConstantValue.Unset) == Microsoft.CodeAnalysis.ConstantValue.Unset)
			{
				this.AddDeclarationDiagnostics(diagnostics);
				this.DeclaringCompilation.SymbolDeclaredEvent(this);
				this.state.NotePartComplete(CompletionPart.TypeMembers);
			}
		}

		// Token: 0x06007021 RID: 28705
		protected abstract ConstantValue MakeConstantValue(HashSet<SourceFieldSymbolWithSyntaxReference> dependencies, bool earlyDecodingWellKnownAttributes, BindingDiagnosticBag diagnostics);

		// Token: 0x04001B72 RID: 7026
		private readonly string _name;

		// Token: 0x04001B73 RID: 7027
		private readonly Location _location;

		// Token: 0x04001B74 RID: 7028
		private readonly SyntaxReference _syntaxReference;

		// Token: 0x04001B75 RID: 7029
		private string _lazyDocComment;

		// Token: 0x04001B76 RID: 7030
		private string _lazyExpandedDocComment;

		// Token: 0x04001B77 RID: 7031
		private ConstantValue _lazyConstantEarlyDecodingValue = Microsoft.CodeAnalysis.ConstantValue.Unset;

		// Token: 0x04001B78 RID: 7032
		private ConstantValue _lazyConstantValue = Microsoft.CodeAnalysis.ConstantValue.Unset;
	}
}
