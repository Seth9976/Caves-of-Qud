using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200011B RID: 283
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeCompilationState
	{
		// Token: 0x06001766 RID: 5990 RVA: 0x00080DD0 File Offset: 0x0007EFD0
		public TypeCompilationState(NamedTypeSymbol typeOpt, [global::System.Runtime.CompilerServices.Nullable(1)] CSharpCompilation compilation, PEModuleBuilder moduleBuilderOpt)
		{
			this.Compilation = compilation;
			this._typeOpt = typeOpt;
			this.ModuleBuilderOpt = moduleBuilderOpt;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x00080DED File Offset: 0x0007EFED
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public NamedTypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(1)]
			get
			{
				return this._typeOpt;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x00080DF5 File Offset: 0x0007EFF5
		public NamedTypeSymbol DynamicOperationContextType
		{
			get
			{
				PEModuleBuilder moduleBuilderOpt = this.ModuleBuilderOpt;
				if (moduleBuilderOpt == null)
				{
					return null;
				}
				return moduleBuilderOpt.GetDynamicOperationContextType(this.Type);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x00080E0E File Offset: 0x0007F00E
		[MemberNotNullWhen(true, "ModuleBuilderOpt")]
		public bool Emitting
		{
			[MemberNotNullWhen(true, "ModuleBuilderOpt")]
			get
			{
				return this.ModuleBuilderOpt != null;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x0600176A RID: 5994 RVA: 0x00080E19 File Offset: 0x0007F019
		// (set) Token: 0x0600176B RID: 5995 RVA: 0x00080E21 File Offset: 0x0007F021
		public ArrayBuilder<TypeCompilationState.MethodWithBody> SynthesizedMethods
		{
			get
			{
				return this._synthesizedMethods;
			}
			set
			{
				this._synthesizedMethods = value;
			}
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00080E2A File Offset: 0x0007F02A
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public void AddSynthesizedMethod(MethodSymbol method, BoundStatement body)
		{
			if (this._synthesizedMethods == null)
			{
				this._synthesizedMethods = ArrayBuilder<TypeCompilationState.MethodWithBody>.GetInstance();
			}
			this._synthesizedMethods.Add(new TypeCompilationState.MethodWithBody(method, body, this.CurrentImportChain));
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00080E57 File Offset: 0x0007F057
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		public void AddMethodWrapper(MethodSymbol method, MethodSymbol wrapper, BoundStatement body)
		{
			this.AddSynthesizedMethod(wrapper, body);
			if (this._wrappers == null)
			{
				this._wrappers = new Dictionary<MethodSymbol, MethodSymbol>();
			}
			this._wrappers.Add(method, wrapper);
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x00080E81 File Offset: 0x0007F081
		public int NextWrapperMethodIndex
		{
			get
			{
				if (this._wrappers != null)
				{
					return this._wrappers.Count;
				}
				return 0;
			}
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00080E98 File Offset: 0x0007F098
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetMethodWrapper(MethodSymbol method)
		{
			MethodSymbol methodSymbol = null;
			if (this._wrappers == null || !this._wrappers.TryGetValue(method, ref methodSymbol))
			{
				return null;
			}
			return methodSymbol;
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00080EC2 File Offset: 0x0007F0C2
		public void Free()
		{
			if (this._synthesizedMethods != null)
			{
				this._synthesizedMethods.Free();
				this._synthesizedMethods = null;
			}
			this._wrappers = null;
			this._constructorInitializers = null;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00080EEC File Offset: 0x0007F0EC
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal void ReportCtorInitializerCycles(MethodSymbol method1, MethodSymbol method2, SyntaxNode syntax, BindingDiagnosticBag diagnostics)
		{
			if (method1 == method2)
			{
				throw ExceptionUtilities.Unreachable;
			}
			if (this._constructorInitializers == null)
			{
				this._constructorInitializers = new SmallDictionary<MethodSymbol, MethodSymbol>();
				this._constructorInitializers.Add(method1, method2);
				return;
			}
			MethodSymbol methodSymbol = method2;
			while (this._constructorInitializers.TryGetValue(methodSymbol, out methodSymbol))
			{
				if (method1 == methodSymbol)
				{
					diagnostics.Add(ErrorCode.ERR_IndirectRecursiveConstructorCall, syntax.Location, new object[] { method1 });
					return;
				}
			}
			this._constructorInitializers.Add(method1, method2);
		}

		// Token: 0x0400067B RID: 1659
		private ArrayBuilder<TypeCompilationState.MethodWithBody> _synthesizedMethods;

		// Token: 0x0400067C RID: 1660
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private Dictionary<MethodSymbol, MethodSymbol> _wrappers;

		// Token: 0x0400067D RID: 1661
		private readonly NamedTypeSymbol _typeOpt;

		// Token: 0x0400067E RID: 1662
		public readonly PEModuleBuilder ModuleBuilderOpt;

		// Token: 0x0400067F RID: 1663
		public ImportChain CurrentImportChain;

		// Token: 0x04000680 RID: 1664
		[global::System.Runtime.CompilerServices.Nullable(1)]
		public readonly CSharpCompilation Compilation;

		// Token: 0x04000681 RID: 1665
		public SynthesizedClosureEnvironment StaticLambdaFrame;

		// Token: 0x04000682 RID: 1666
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 2, 1, 1 })]
		private SmallDictionary<MethodSymbol, MethodSymbol> _constructorInitializers;

		// Token: 0x020006CB RID: 1739
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[global::System.Runtime.CompilerServices.Nullable(0)]
		internal struct MethodWithBody
		{
			// Token: 0x0600846C RID: 33900 RVA: 0x001E0531 File Offset: 0x001DE731
			internal MethodWithBody(MethodSymbol method, BoundStatement body, [global::System.Runtime.CompilerServices.Nullable(2)] ImportChain importChain)
			{
				this.Method = method;
				this.Body = body;
				this.ImportChain = importChain;
			}

			// Token: 0x0400219D RID: 8605
			public readonly MethodSymbol Method;

			// Token: 0x0400219E RID: 8606
			public readonly BoundStatement Body;

			// Token: 0x0400219F RID: 8607
			[global::System.Runtime.CompilerServices.Nullable(2)]
			public readonly ImportChain ImportChain;
		}
	}
}
