using System;

namespace Mono.Cecil.Rocks
{
	// Token: 0x02000427 RID: 1063
	internal static class TypeReferenceRocks
	{
		// Token: 0x06001665 RID: 5733 RVA: 0x000487FE File Offset: 0x000469FE
		public static ArrayType MakeArrayType(this TypeReference self)
		{
			return new ArrayType(self);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00048808 File Offset: 0x00046A08
		public static ArrayType MakeArrayType(this TypeReference self, int rank)
		{
			if (rank == 0)
			{
				throw new ArgumentOutOfRangeException("rank");
			}
			ArrayType arrayType = new ArrayType(self);
			for (int i = 1; i < rank; i++)
			{
				arrayType.Dimensions.Add(default(ArrayDimension));
			}
			return arrayType;
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004884B File Offset: 0x00046A4B
		public static PointerType MakePointerType(this TypeReference self)
		{
			return new PointerType(self);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00048853 File Offset: 0x00046A53
		public static ByReferenceType MakeByReferenceType(this TypeReference self)
		{
			return new ByReferenceType(self);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004885B File Offset: 0x00046A5B
		public static OptionalModifierType MakeOptionalModifierType(this TypeReference self, TypeReference modifierType)
		{
			return new OptionalModifierType(modifierType, self);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00048864 File Offset: 0x00046A64
		public static RequiredModifierType MakeRequiredModifierType(this TypeReference self, TypeReference modifierType)
		{
			return new RequiredModifierType(modifierType, self);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00048870 File Offset: 0x00046A70
		public static GenericInstanceType MakeGenericInstanceType(this TypeReference self, params TypeReference[] arguments)
		{
			if (self == null)
			{
				throw new ArgumentNullException("self");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			if (arguments.Length == 0)
			{
				throw new ArgumentException();
			}
			if (self.GenericParameters.Count != arguments.Length)
			{
				throw new ArgumentException();
			}
			GenericInstanceType genericInstanceType = new GenericInstanceType(self, arguments.Length);
			foreach (TypeReference typeReference in arguments)
			{
				genericInstanceType.GenericArguments.Add(typeReference);
			}
			return genericInstanceType;
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000488E4 File Offset: 0x00046AE4
		public static PinnedType MakePinnedType(this TypeReference self)
		{
			return new PinnedType(self);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x000488EC File Offset: 0x00046AEC
		public static SentinelType MakeSentinelType(this TypeReference self)
		{
			return new SentinelType(self);
		}
	}
}
