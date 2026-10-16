using System;
using System.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x0200023C RID: 572
	internal sealed class MethodReferenceComparer : EqualityComparer<MethodReference>
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x0002B7D9 File Offset: 0x000299D9
		public override bool Equals(MethodReference x, MethodReference y)
		{
			return MethodReferenceComparer.AreEqual(x, y);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002B7E2 File Offset: 0x000299E2
		public override int GetHashCode(MethodReference obj)
		{
			return MethodReferenceComparer.GetHashCodeFor(obj);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002B7EC File Offset: 0x000299EC
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
			if (!TypeReferenceEqualityComparer.AreEqual(x.DeclaringType, y.DeclaringType, TypeComparisonMode.Exact))
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
					if (!TypeReferenceEqualityComparer.AreEqual(genericInstanceMethod.GenericArguments[i], genericInstanceMethod2.GenericArguments[i], TypeComparisonMode.Exact))
					{
						return false;
					}
				}
			}
			MethodDefinition methodDefinition = x.Resolve();
			MethodDefinition methodDefinition2 = y.Resolve();
			if (methodDefinition != methodDefinition2)
			{
				return false;
			}
			if (methodDefinition == null)
			{
				if (MethodReferenceComparer.xComparisonStack == null)
				{
					MethodReferenceComparer.xComparisonStack = new List<MethodReference>();
				}
				if (MethodReferenceComparer.yComparisonStack == null)
				{
					MethodReferenceComparer.yComparisonStack = new List<MethodReference>();
				}
				for (int j = 0; j < MethodReferenceComparer.xComparisonStack.Count; j++)
				{
					if (MethodReferenceComparer.xComparisonStack[j] == x && MethodReferenceComparer.yComparisonStack[j] == y)
					{
						return true;
					}
				}
				MethodReferenceComparer.xComparisonStack.Add(x);
				try
				{
					MethodReferenceComparer.yComparisonStack.Add(y);
					try
					{
						for (int k = 0; k < x.Parameters.Count; k++)
						{
							if (!TypeReferenceEqualityComparer.AreEqual(x.Parameters[k].ParameterType, y.Parameters[k].ParameterType, TypeComparisonMode.Exact))
							{
								return false;
							}
						}
					}
					finally
					{
						MethodReferenceComparer.yComparisonStack.RemoveAt(MethodReferenceComparer.yComparisonStack.Count - 1);
					}
				}
				finally
				{
					MethodReferenceComparer.xComparisonStack.RemoveAt(MethodReferenceComparer.xComparisonStack.Count - 1);
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002BA10 File Offset: 0x00029C10
		public static bool AreSignaturesEqual(MethodReference x, MethodReference y, TypeComparisonMode comparisonMode = TypeComparisonMode.Exact)
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
				if (!TypeReferenceEqualityComparer.AreEqual(x.Parameters[i].ParameterType, y.Parameters[i].ParameterType, comparisonMode))
				{
					return false;
				}
			}
			return TypeReferenceEqualityComparer.AreEqual(x.ReturnType, y.ReturnType, comparisonMode);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002BABC File Offset: 0x00029CBC
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

		// Token: 0x040003D7 RID: 983
		[ThreadStatic]
		private static List<MethodReference> xComparisonStack;

		// Token: 0x040003D8 RID: 984
		[ThreadStatic]
		private static List<MethodReference> yComparisonStack;
	}
}
