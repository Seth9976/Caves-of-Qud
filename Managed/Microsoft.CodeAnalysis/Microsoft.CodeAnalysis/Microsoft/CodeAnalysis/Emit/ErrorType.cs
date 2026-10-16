using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Emit
{
	// Token: 0x02000490 RID: 1168
	internal class ErrorType : INamespaceTypeReference, INamedTypeReference, ITypeReference, IReference, INamedEntity
	{
		// Token: 0x06002E12 RID: 11794 RVA: 0x00082D73 File Offset: 0x00080F73
		IUnitReference INamespaceTypeReference.GetUnit(EmitContext context)
		{
			return ErrorType.ErrorAssembly.Singleton;
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x00082D7A File Offset: 0x00080F7A
		string INamespaceTypeReference.NamespaceName
		{
			get
			{
				return "";
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x06002E14 RID: 11796 RVA: 0x00082D81 File Offset: 0x00080F81
		ushort INamedTypeReference.GenericParameterCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00082D84 File Offset: 0x00080F84
		bool INamedTypeReference.MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x00082D87 File Offset: 0x00080F87
		bool ITypeReference.IsEnum
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x00082D8A File Offset: 0x00080F8A
		bool ITypeReference.IsValueType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002E18 RID: 11800 RVA: 0x00082D8D File Offset: 0x00080F8D
		ITypeDefinition ITypeReference.GetResolvedType(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x00082D90 File Offset: 0x00080F90
		Microsoft.Cci.PrimitiveTypeCode ITypeReference.TypeCode
		{
			get
			{
				return Microsoft.Cci.PrimitiveTypeCode.NotPrimitive;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x00082D94 File Offset: 0x00080F94
		TypeDefinitionHandle ITypeReference.TypeDef
		{
			get
			{
				return default(TypeDefinitionHandle);
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x00082DAA File Offset: 0x00080FAA
		IGenericMethodParameterReference ITypeReference.AsGenericMethodParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x00082DAD File Offset: 0x00080FAD
		IGenericTypeInstanceReference ITypeReference.AsGenericTypeInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D47 RID: 3399
		// (get) Token: 0x06002E1D RID: 11805 RVA: 0x00082DB0 File Offset: 0x00080FB0
		IGenericTypeParameterReference ITypeReference.AsGenericTypeParameterReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E1E RID: 11806 RVA: 0x00082DB3 File Offset: 0x00080FB3
		INamespaceTypeDefinition ITypeReference.AsNamespaceTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x00082DB6 File Offset: 0x00080FB6
		INamespaceTypeReference ITypeReference.AsNamespaceTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002E20 RID: 11808 RVA: 0x00082DB9 File Offset: 0x00080FB9
		INestedTypeDefinition ITypeReference.AsNestedTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x17000D49 RID: 3401
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x00082DBC File Offset: 0x00080FBC
		INestedTypeReference ITypeReference.AsNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000D4A RID: 3402
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x00082DBF File Offset: 0x00080FBF
		ISpecializedNestedTypeReference ITypeReference.AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002E23 RID: 11811 RVA: 0x00082DC2 File Offset: 0x00080FC2
		ITypeDefinition ITypeReference.AsTypeDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x06002E24 RID: 11812 RVA: 0x00082DC5 File Offset: 0x00080FC5
		IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
		{
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x06002E25 RID: 11813 RVA: 0x00082DCC File Offset: 0x00080FCC
		void IReference.Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06002E26 RID: 11814 RVA: 0x00082DD5 File Offset: 0x00080FD5
		IDefinition IReference.AsDefinition(EmitContext context)
		{
			return null;
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x00082DD8 File Offset: 0x00080FD8
		ISymbolInternal IReference.GetInternalSymbol()
		{
			return null;
		}

		// Token: 0x17000D4B RID: 3403
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x00082DDB File Offset: 0x00080FDB
		string INamedEntity.Name
		{
			get
			{
				return ErrorType.s_name;
			}
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x00082DE2 File Offset: 0x00080FE2
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x00082DE9 File Offset: 0x00080FE9
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0400125A RID: 4698
		public static readonly ErrorType Singleton = new ErrorType();

		// Token: 0x0400125B RID: 4699
		private static readonly string s_name = "Error" + Guid.NewGuid().ToString("B");

		// Token: 0x0200073E RID: 1854
		private sealed class ErrorAssembly : IAssemblyReference, IModuleReference, IUnitReference, IReference, INamedEntity
		{
			// Token: 0x17001017 RID: 4119
			// (get) Token: 0x06003C10 RID: 15376 RVA: 0x000AC791 File Offset: 0x000AA991
			AssemblyIdentity IAssemblyReference.Identity
			{
				get
				{
					return ErrorType.ErrorAssembly.s_identity;
				}
			}

			// Token: 0x17001018 RID: 4120
			// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000AC798 File Offset: 0x000AA998
			Version IAssemblyReference.AssemblyVersionPattern
			{
				get
				{
					return null;
				}
			}

			// Token: 0x06003C12 RID: 15378 RVA: 0x000AC79B File Offset: 0x000AA99B
			IAssemblyReference IModuleReference.GetContainingAssembly(EmitContext context)
			{
				return this;
			}

			// Token: 0x06003C13 RID: 15379 RVA: 0x000AC79E File Offset: 0x000AA99E
			IEnumerable<ICustomAttribute> IReference.GetAttributes(EmitContext context)
			{
				return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
			}

			// Token: 0x06003C14 RID: 15380 RVA: 0x000AC7A5 File Offset: 0x000AA9A5
			void IReference.Dispatch(MetadataVisitor visitor)
			{
				visitor.Visit(this);
			}

			// Token: 0x06003C15 RID: 15381 RVA: 0x000AC7AE File Offset: 0x000AA9AE
			IDefinition IReference.AsDefinition(EmitContext context)
			{
				return null;
			}

			// Token: 0x06003C16 RID: 15382 RVA: 0x000AC7B1 File Offset: 0x000AA9B1
			ISymbolInternal IReference.GetInternalSymbol()
			{
				return null;
			}

			// Token: 0x17001019 RID: 4121
			// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000AC7B4 File Offset: 0x000AA9B4
			string INamedEntity.Name
			{
				get
				{
					return ErrorType.ErrorAssembly.s_identity.Name;
				}
			}

			// Token: 0x04001CC9 RID: 7369
			public static readonly ErrorType.ErrorAssembly Singleton = new ErrorType.ErrorAssembly();

			// Token: 0x04001CCA RID: 7370
			private static readonly AssemblyIdentity s_identity = new AssemblyIdentity("Error" + Guid.NewGuid().ToString("B"), AssemblyIdentity.NullVersion, "", ImmutableArray<byte>.Empty, false, false, 0);
		}
	}
}
