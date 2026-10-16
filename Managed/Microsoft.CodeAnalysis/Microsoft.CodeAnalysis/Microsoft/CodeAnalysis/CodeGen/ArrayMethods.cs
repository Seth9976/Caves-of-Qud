using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004BB RID: 1211
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMethods
	{
		// Token: 0x06003085 RID: 12421 RVA: 0x0008A599 File Offset: 0x00088799
		public ArrayMethod GetArrayConstructor(IArrayTypeReference arrayType)
		{
			return this.GetArrayMethod(arrayType, ArrayMethods.ArrayMethodKind.CTOR);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x0008A5A3 File Offset: 0x000887A3
		public ArrayMethod GetArrayGet(IArrayTypeReference arrayType)
		{
			return this.GetArrayMethod(arrayType, ArrayMethods.ArrayMethodKind.GET);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x0008A5AD File Offset: 0x000887AD
		public ArrayMethod GetArraySet(IArrayTypeReference arrayType)
		{
			return this.GetArrayMethod(arrayType, ArrayMethods.ArrayMethodKind.SET);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0008A5B7 File Offset: 0x000887B7
		public ArrayMethod GetArrayAddress(IArrayTypeReference arrayType)
		{
			return this.GetArrayMethod(arrayType, ArrayMethods.ArrayMethodKind.ADDRESS);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x0008A5C4 File Offset: 0x000887C4
		private ArrayMethod GetArrayMethod(IArrayTypeReference arrayType, ArrayMethods.ArrayMethodKind id)
		{
			ValueTuple<byte, IReferenceOrISignature> valueTuple = new ValueTuple<byte, IReferenceOrISignature>((byte)id, new IReferenceOrISignature(arrayType));
			ConcurrentDictionary<ValueTuple<byte, IReferenceOrISignature>, ArrayMethod> dict = this._dict;
			ArrayMethod arrayMethod;
			if (!dict.TryGetValue(valueTuple, ref arrayMethod))
			{
				arrayMethod = ArrayMethods.MakeArrayMethod(arrayType, id);
				arrayMethod = dict.GetOrAdd(valueTuple, arrayMethod);
			}
			return arrayMethod;
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x0008A604 File Offset: 0x00088804
		private static ArrayMethod MakeArrayMethod(IArrayTypeReference arrayType, ArrayMethods.ArrayMethodKind id)
		{
			switch (id)
			{
			case ArrayMethods.ArrayMethodKind.GET:
				return new ArrayMethods.ArrayGet(arrayType);
			case ArrayMethods.ArrayMethodKind.SET:
				return new ArrayMethods.ArraySet(arrayType);
			case ArrayMethods.ArrayMethodKind.ADDRESS:
				return new ArrayMethods.ArrayAddress(arrayType);
			case ArrayMethods.ArrayMethodKind.CTOR:
				return new ArrayMethods.ArrayConstructor(arrayType);
			default:
				throw ExceptionUtilities.UnexpectedValue(id);
			}
		}

		// Token: 0x040012D0 RID: 4816
		[TupleElementNames(new string[] { "methodKind", "arrayType" })]
		[Nullable(new byte[] { 1, 0, 1 })]
		private readonly ConcurrentDictionary<ValueTuple<byte, IReferenceOrISignature>, ArrayMethod> _dict = new ConcurrentDictionary<ValueTuple<byte, IReferenceOrISignature>, ArrayMethod>();

		// Token: 0x02000763 RID: 1891
		[NullableContext(0)]
		private enum ArrayMethodKind : byte
		{
			// Token: 0x04001D33 RID: 7475
			GET,
			// Token: 0x04001D34 RID: 7476
			SET,
			// Token: 0x04001D35 RID: 7477
			ADDRESS,
			// Token: 0x04001D36 RID: 7478
			CTOR
		}

		// Token: 0x02000764 RID: 1892
		[Nullable(0)]
		private sealed class ArrayConstructor : ArrayMethod
		{
			// Token: 0x06003E27 RID: 15911 RVA: 0x000AF55E File Offset: 0x000AD75E
			public ArrayConstructor(IArrayTypeReference arrayType)
				: base(arrayType)
			{
			}

			// Token: 0x1700112F RID: 4399
			// (get) Token: 0x06003E28 RID: 15912 RVA: 0x000AF567 File Offset: 0x000AD767
			public override string Name
			{
				get
				{
					return ".ctor";
				}
			}

			// Token: 0x06003E29 RID: 15913 RVA: 0x000AF56E File Offset: 0x000AD76E
			public override ITypeReference GetType(EmitContext context)
			{
				return context.Module.GetPlatformType(PlatformType.SystemVoid, context);
			}
		}

		// Token: 0x02000765 RID: 1893
		[Nullable(0)]
		private sealed class ArrayGet : ArrayMethod
		{
			// Token: 0x06003E2A RID: 15914 RVA: 0x000AF57D File Offset: 0x000AD77D
			public ArrayGet(IArrayTypeReference arrayType)
				: base(arrayType)
			{
			}

			// Token: 0x17001130 RID: 4400
			// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000AF586 File Offset: 0x000AD786
			public override string Name
			{
				get
				{
					return "Get";
				}
			}

			// Token: 0x06003E2C RID: 15916 RVA: 0x000AF58D File Offset: 0x000AD78D
			public override ITypeReference GetType(EmitContext context)
			{
				return this.arrayType.GetElementType(context);
			}
		}

		// Token: 0x02000766 RID: 1894
		[Nullable(0)]
		private sealed class ArrayAddress : ArrayMethod
		{
			// Token: 0x06003E2D RID: 15917 RVA: 0x000AF59B File Offset: 0x000AD79B
			public ArrayAddress(IArrayTypeReference arrayType)
				: base(arrayType)
			{
			}

			// Token: 0x17001131 RID: 4401
			// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000AF5A4 File Offset: 0x000AD7A4
			public override bool ReturnValueIsByRef
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06003E2F RID: 15919 RVA: 0x000AF5A7 File Offset: 0x000AD7A7
			public override ITypeReference GetType(EmitContext context)
			{
				return this.arrayType.GetElementType(context);
			}

			// Token: 0x17001132 RID: 4402
			// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000AF5B5 File Offset: 0x000AD7B5
			public override string Name
			{
				get
				{
					return "Address";
				}
			}
		}

		// Token: 0x02000767 RID: 1895
		[Nullable(0)]
		private sealed class ArraySet : ArrayMethod
		{
			// Token: 0x06003E31 RID: 15921 RVA: 0x000AF5BC File Offset: 0x000AD7BC
			public ArraySet(IArrayTypeReference arrayType)
				: base(arrayType)
			{
			}

			// Token: 0x17001133 RID: 4403
			// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000AF5C5 File Offset: 0x000AD7C5
			public override string Name
			{
				get
				{
					return "Set";
				}
			}

			// Token: 0x06003E33 RID: 15923 RVA: 0x000AF5CC File Offset: 0x000AD7CC
			public override ITypeReference GetType(EmitContext context)
			{
				return context.Module.GetPlatformType(PlatformType.SystemVoid, context);
			}

			// Token: 0x06003E34 RID: 15924 RVA: 0x000AF5DC File Offset: 0x000AD7DC
			[return: Nullable(new byte[] { 0, 1 })]
			protected override ImmutableArray<ArrayMethodParameterInfo> MakeParameters()
			{
				int rank = this.arrayType.Rank;
				ArrayBuilder<ArrayMethodParameterInfo> instance = ArrayBuilder<ArrayMethodParameterInfo>.GetInstance(rank + 1);
				for (int i = 0; i < rank; i++)
				{
					instance.Add(ArrayMethodParameterInfo.GetIndexParameter((ushort)i));
				}
				instance.Add(new ArraySetValueParameterInfo((ushort)rank, this.arrayType));
				return instance.ToImmutableAndFree();
			}
		}
	}
}
