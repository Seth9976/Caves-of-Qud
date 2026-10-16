using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000082 RID: 130
	internal sealed class MethodReferenceComparer : EqualityComparer<MethodReference>
	{
		// Token: 0x0600051B RID: 1307 RVA: 0x00010948 File Offset: 0x0000EB48
		public override bool Equals(MethodReference x, MethodReference y)
		{
			return MethodReferenceComparer.AreEqual(x, y);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00010951 File Offset: 0x0000EB51
		public override int GetHashCode(MethodReference obj)
		{
			return MethodReferenceComparer.GetHashCodeFor(obj);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static bool AreEqual(MethodReference x, MethodReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.HasThis != y.HasThis)
			{
				return false;
			}
			if (x.HasParameters != y.HasParameters)
			{
				return false;
			}
			if (x.HasGenericParameters != y.HasGenericParameters)
			{
				return false;
			}
			if (x.Parameters.Count != y.Parameters.Count)
			{
				return false;
			}
			if (x.Name != y.Name)
			{
				return false;
			}
			if (!TypeReferenceEqualityComparer.AreEqual(x.DeclaringType, y.DeclaringType))
			{
				return false;
			}
			GenericInstanceMethod genericInstanceMethod = x as GenericInstanceMethod;
			GenericInstanceMethod genericInstanceMethod2 = y as GenericInstanceMethod;
			if (genericInstanceMethod != null || genericInstanceMethod2 != null)
			{
				if (genericInstanceMethod == null || genericInstanceMethod2 == null)
				{
					return false;
				}
				if (genericInstanceMethod.GenericArguments.Count != genericInstanceMethod2.GenericArguments.Count)
				{
					return false;
				}
				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++)
				{
					if (!TypeReferenceEqualityComparer.AreEqual(genericInstanceMethod.GenericArguments[i], genericInstanceMethod2.GenericArguments[i]))
					{
						return false;
					}
				}
			}
			return x.Resolve() == y.Resolve();
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00010A64 File Offset: 0x0000EC64
		public static bool AreSignaturesEqual(MethodReference x, MethodReference y)
		{
			if (x.HasThis != y.HasThis)
			{
				return false;
			}
			if (x.Parameters.Count != y.Parameters.Count)
			{
				return false;
			}
			if (x.GenericParameters.Count != y.GenericParameters.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Parameters.Count; i++)
			{
				if (!TypeReferenceEqualityComparer.AreEqual(x.Parameters[i].ParameterType, y.Parameters[i].ParameterType))
				{
					return false;
				}
			}
			return TypeReferenceEqualityComparer.AreEqual(x.ReturnType, y.ReturnType);
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public static int GetHashCodeFor(MethodReference obj)
		{
			GenericInstanceMethod genericInstanceMethod = obj as GenericInstanceMethod;
			if (genericInstanceMethod != null)
			{
				int num = MethodReferenceComparer.GetHashCodeFor(genericInstanceMethod.ElementMethod);
				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++)
				{
					num = num * 486187739 + TypeReferenceEqualityComparer.GetHashCodeFor(genericInstanceMethod.GenericArguments[i]);
				}
				return num;
			}
			return TypeReferenceEqualityComparer.GetHashCodeFor(obj.DeclaringType) * 486187739 + obj.Name.GetHashCode();
		}
	}
}
