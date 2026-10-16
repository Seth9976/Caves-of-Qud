using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Collections;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004D7 RID: 1239
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PrivateImplementationDetails : DefaultTypeDef, INamespaceTypeDefinition, INamedTypeDefinition, ITypeDefinition, IDefinition, IReference, ITypeReference, INamedTypeReference, INamedEntity, INamespaceTypeReference
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x0008E20C File Offset: 0x0008C40C
		internal PrivateImplementationDetails(CommonPEModuleBuilder moduleBuilder, string moduleName, int submissionSlotIndex, ITypeReference systemObject, ITypeReference systemValueType, ITypeReference systemInt8Type, ITypeReference systemInt16Type, ITypeReference systemInt32Type, ITypeReference systemInt64Type, ICustomAttribute compilerGeneratedAttribute)
		{
			this._moduleBuilder = moduleBuilder;
			this._systemObject = systemObject;
			this._systemValueType = systemValueType;
			this._systemInt8Type = systemInt8Type;
			this._systemInt16Type = systemInt16Type;
			this._systemInt32Type = systemInt32Type;
			this._systemInt64Type = systemInt64Type;
			this._compilerGeneratedAttribute = compilerGeneratedAttribute;
			bool flag = moduleBuilder.OutputKind == OutputKind.NetModule;
			this._name = PrivateImplementationDetails.GetClassName(moduleName, submissionSlotIndex, flag);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x0008E2A8 File Offset: 0x0008C4A8
		private static string GetClassName(string moduleName, int submissionSlotIndex, bool isNetModule)
		{
			string text = (isNetModule ? ("<PrivateImplementationDetails><" + MetadataHelpers.MangleForTypeNameIfNeeded(moduleName) + ">") : "<PrivateImplementationDetails>");
			if (submissionSlotIndex >= 0)
			{
				text += submissionSlotIndex.ToString();
			}
			return text;
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x0008E2E8 File Offset: 0x0008C4E8
		internal void Freeze()
		{
			if (Interlocked.Exchange(ref this._frozen, 1) != 0)
			{
				throw new InvalidOperationException();
			}
			ArrayBuilder<SynthesizedStaticField> instance = ArrayBuilder<SynthesizedStaticField>.GetInstance(this._mappedFields.Count + ((this._mvidField != null) ? 1 : 0));
			instance.AddRange(this._mappedFields.Values);
			if (this._mvidField != null)
			{
				instance.Add(this._mvidField);
			}
			instance.AddRange(this._instrumentationPayloadRootFields.Values);
			instance.Sort(PrivateImplementationDetails.FieldComparer.Instance);
			this._orderedSynthesizedFields = instance.ToImmutableAndFree();
			this._orderedSynthesizedMethods = Enumerable.Select<KeyValuePair<string, IMethodDefinition>, IMethodDefinition>(Enumerable.OrderBy<KeyValuePair<string, IMethodDefinition>, string>(this._synthesizedMethods, (KeyValuePair<string, IMethodDefinition> kvp) => kvp.Key), (KeyValuePair<string, IMethodDefinition> kvp) => kvp.Value).AsImmutable<IMethodDefinition>();
			this._orderedProxyTypes = Enumerable.Select<KeyValuePair<uint, ITypeReference>, ITypeReference>(Enumerable.OrderBy<KeyValuePair<uint, ITypeReference>, uint>(this._proxyTypes, (KeyValuePair<uint, ITypeReference> kvp) => kvp.Key), (KeyValuePair<uint, ITypeReference> kvp) => kvp.Value).AsImmutable<ITypeReference>();
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x0008E426 File Offset: 0x0008C626
		private bool IsFrozen
		{
			get
			{
				return this._frozen != 0;
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x0008E434 File Offset: 0x0008C634
		[NullableContext(0)]
		[return: Nullable(1)]
		internal IFieldReference CreateDataField(ImmutableArray<byte> data)
		{
			ITypeReference type = this._proxyTypes.GetOrAdd((uint)data.Length, new Func<uint, ITypeReference>(this.GetStorageStruct));
			return this._mappedFields.GetOrAdd(data, (ImmutableArray<byte> data0) => new MappedField(PrivateImplementationDetails.GenerateDataFieldName(data0), this, type, data0));
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x0008E48C File Offset: 0x0008C68C
		private ITypeReference GetStorageStruct(uint size)
		{
			switch (size)
			{
			case 1U:
				return this._systemInt8Type ?? new ExplicitSizeStruct(1U, this, this._systemValueType);
			case 2U:
				return this._systemInt16Type ?? new ExplicitSizeStruct(2U, this, this._systemValueType);
			case 3U:
				break;
			case 4U:
				return this._systemInt32Type ?? new ExplicitSizeStruct(4U, this, this._systemValueType);
			default:
				if (size == 8U)
				{
					return this._systemInt64Type ?? new ExplicitSizeStruct(8U, this, this._systemValueType);
				}
				break;
			}
			return new ExplicitSizeStruct(size, this, this._systemValueType);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x0008E524 File Offset: 0x0008C724
		internal IFieldReference GetModuleVersionId(ITypeReference mvidType)
		{
			if (this._mvidField == null)
			{
				Interlocked.CompareExchange<ModuleVersionIdField>(ref this._mvidField, new ModuleVersionIdField(this, mvidType), null);
			}
			return this._mvidField;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x0008E548 File Offset: 0x0008C748
		internal IFieldReference GetOrAddInstrumentationPayloadRoot(int analysisKind, ITypeReference payloadRootType)
		{
			InstrumentationPayloadRootField orAdd;
			if (!this._instrumentationPayloadRootFields.TryGetValue(analysisKind, ref orAdd))
			{
				orAdd = this._instrumentationPayloadRootFields.GetOrAdd(analysisKind, (int kind) => new InstrumentationPayloadRootField(this, kind, payloadRootType));
			}
			return orAdd;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x0008E593 File Offset: 0x0008C793
		[return: Nullable(new byte[] { 1, 0, 1 })]
		internal IOrderedEnumerable<KeyValuePair<int, InstrumentationPayloadRootField>> GetInstrumentationPayloadRoots()
		{
			return Enumerable.OrderBy<KeyValuePair<int, InstrumentationPayloadRootField>, int>(this._instrumentationPayloadRootFields, (KeyValuePair<int, InstrumentationPayloadRootField> analysis) => analysis.Key);
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x0008E5BF File Offset: 0x0008C7BF
		internal bool TryAddSynthesizedMethod(IMethodDefinition method)
		{
			return this._synthesizedMethods.TryAdd(method.Name, method);
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x0008E5D3 File Offset: 0x0008C7D3
		public override IEnumerable<IFieldDefinition> GetFields(EmitContext context)
		{
			return this._orderedSynthesizedFields;
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x0008E5E0 File Offset: 0x0008C7E0
		public override IEnumerable<IMethodDefinition> GetMethods(EmitContext context)
		{
			return this._orderedSynthesizedMethods;
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x0008E5F0 File Offset: 0x0008C7F0
		[return: Nullable(2)]
		internal IMethodDefinition GetMethod(string name)
		{
			IMethodDefinition methodDefinition;
			this._synthesizedMethods.TryGetValue(name, ref methodDefinition);
			return methodDefinition;
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x0008E60D File Offset: 0x0008C80D
		public override IEnumerable<INestedTypeDefinition> GetNestedTypes(EmitContext context)
		{
			return this._orderedProxyTypes.OfType<ExplicitSizeStruct>();
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x0008E61A File Offset: 0x0008C81A
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x0008E622 File Offset: 0x0008C822
		public override ITypeReference GetBaseClass(EmitContext context)
		{
			return this._systemObject;
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0008E62A File Offset: 0x0008C82A
		public override IEnumerable<ICustomAttribute> GetAttributes(EmitContext context)
		{
			if (this._compilerGeneratedAttribute != null)
			{
				return SpecializedCollections.SingletonEnumerable<ICustomAttribute>(this._compilerGeneratedAttribute);
			}
			return SpecializedCollections.EmptyEnumerable<ICustomAttribute>();
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0008E645 File Offset: 0x0008C845
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x0008E64E File Offset: 0x0008C84E
		public override INamespaceTypeDefinition AsNamespaceTypeDefinition(EmitContext context)
		{
			return this;
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x0008E651 File Offset: 0x0008C851
		public override INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x060031DC RID: 12764 RVA: 0x0008E654 File Offset: 0x0008C854
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x060031DD RID: 12765 RVA: 0x0008E65C File Offset: 0x0008C85C
		public bool IsPublic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x0008E65F File Offset: 0x0008C85F
		public IUnitReference GetUnit(EmitContext context)
		{
			return this._moduleBuilder;
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x060031DF RID: 12767 RVA: 0x0008E667 File Offset: 0x0008C867
		public string NamespaceName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x0008E670 File Offset: 0x0008C870
		[NullableContext(0)]
		[return: Nullable(1)]
		internal static string GenerateDataFieldName(ImmutableArray<byte> data)
		{
			ImmutableArray<byte> immutableArray = CryptographicHashProvider.ComputeSourceHash(data, SourceHashAlgorithm.Sha256);
			char[] array = new char[immutableArray.Length * 2];
			int num = 0;
			foreach (byte b in immutableArray)
			{
				array[num++] = PrivateImplementationDetails.Hexchar(b >> 4);
				array[num++] = PrivateImplementationDetails.Hexchar((int)(b & 15));
			}
			return new string(array);
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x0008E6D9 File Offset: 0x0008C8D9
		private static char Hexchar(int x)
		{
			return (char)((x <= 9) ? (x + 48) : (x + 55));
		}

		// Token: 0x04001341 RID: 4929
		internal const string SynthesizedStringHashFunctionName = "ComputeStringHash";

		// Token: 0x04001342 RID: 4930
		private readonly CommonPEModuleBuilder _moduleBuilder;

		// Token: 0x04001343 RID: 4931
		private readonly ITypeReference _systemObject;

		// Token: 0x04001344 RID: 4932
		private readonly ITypeReference _systemValueType;

		// Token: 0x04001345 RID: 4933
		private readonly ITypeReference _systemInt8Type;

		// Token: 0x04001346 RID: 4934
		private readonly ITypeReference _systemInt16Type;

		// Token: 0x04001347 RID: 4935
		private readonly ITypeReference _systemInt32Type;

		// Token: 0x04001348 RID: 4936
		private readonly ITypeReference _systemInt64Type;

		// Token: 0x04001349 RID: 4937
		private readonly ICustomAttribute _compilerGeneratedAttribute;

		// Token: 0x0400134A RID: 4938
		private readonly string _name;

		// Token: 0x0400134B RID: 4939
		private int _frozen;

		// Token: 0x0400134C RID: 4940
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<SynthesizedStaticField> _orderedSynthesizedFields;

		// Token: 0x0400134D RID: 4941
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly ConcurrentDictionary<ImmutableArray<byte>, MappedField> _mappedFields = new ConcurrentDictionary<ImmutableArray<byte>, MappedField>(ByteSequenceComparer.Instance);

		// Token: 0x0400134E RID: 4942
		[Nullable(2)]
		private ModuleVersionIdField _mvidField;

		// Token: 0x0400134F RID: 4943
		private readonly ConcurrentDictionary<int, InstrumentationPayloadRootField> _instrumentationPayloadRootFields = new ConcurrentDictionary<int, InstrumentationPayloadRootField>();

		// Token: 0x04001350 RID: 4944
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<IMethodDefinition> _orderedSynthesizedMethods;

		// Token: 0x04001351 RID: 4945
		private readonly ConcurrentDictionary<string, IMethodDefinition> _synthesizedMethods = new ConcurrentDictionary<string, IMethodDefinition>();

		// Token: 0x04001352 RID: 4946
		[Nullable(new byte[] { 0, 1 })]
		private ImmutableArray<ITypeReference> _orderedProxyTypes;

		// Token: 0x04001353 RID: 4947
		private readonly ConcurrentDictionary<uint, ITypeReference> _proxyTypes = new ConcurrentDictionary<uint, ITypeReference>();

		// Token: 0x0200077D RID: 1917
		[NullableContext(0)]
		private sealed class FieldComparer : IComparer<SynthesizedStaticField>
		{
			// Token: 0x06003ED1 RID: 16081 RVA: 0x000B0D26 File Offset: 0x000AEF26
			private FieldComparer()
			{
			}

			// Token: 0x06003ED2 RID: 16082 RVA: 0x000B0D2E File Offset: 0x000AEF2E
			[NullableContext(2)]
			public int Compare(SynthesizedStaticField x, SynthesizedStaticField y)
			{
				return x.Name.CompareTo(y.Name);
			}

			// Token: 0x04001D76 RID: 7542
			[Nullable(1)]
			public static readonly PrivateImplementationDetails.FieldComparer Instance = new PrivateImplementationDetails.FieldComparer();
		}
	}
}
