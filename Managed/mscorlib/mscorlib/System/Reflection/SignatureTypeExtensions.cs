using System;

namespace System.Reflection
{
	// Token: 0x020008C9 RID: 2249
	internal static class SignatureTypeExtensions
	{
		// Token: 0x06004ADF RID: 19167 RVA: 0x000EFA40 File Offset: 0x000EDC40
		public static bool MatchesParameterTypeExactly(this Type pattern, ParameterInfo parameter)
		{
			SignatureType signatureType = pattern as SignatureType;
			if (signatureType != null)
			{
				return signatureType.MatchesExactly(parameter.ParameterType);
			}
			return pattern == parameter.ParameterType;
		}

		// Token: 0x06004AE0 RID: 19168 RVA: 0x000EFA70 File Offset: 0x000EDC70
		internal static bool MatchesExactly(this SignatureType pattern, Type actual)
		{
			if (pattern.IsSZArray)
			{
				return actual.IsSZArray && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsVariableBoundArray)
			{
				return actual.IsVariableBoundArray && pattern.GetArrayRank() == actual.GetArrayRank() && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsByRef)
			{
				return actual.IsByRef && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (pattern.IsPointer)
			{
				return actual.IsPointer && pattern.ElementType.MatchesExactly(actual.GetElementType());
			}
			if (!pattern.IsConstructedGenericType)
			{
				return pattern.IsGenericMethodParameter && actual.IsGenericMethodParameter && pattern.GenericParameterPosition == actual.GenericParameterPosition;
			}
			if (!actual.IsConstructedGenericType)
			{
				return false;
			}
			if (!(pattern.GetGenericTypeDefinition() == actual.GetGenericTypeDefinition()))
			{
				return false;
			}
			Type[] genericTypeArguments = pattern.GenericTypeArguments;
			Type[] genericTypeArguments2 = actual.GenericTypeArguments;
			int num = genericTypeArguments.Length;
			if (num != genericTypeArguments2.Length)
			{
				return false;
			}
			for (int i = 0; i < num; i++)
			{
				Type type = genericTypeArguments[i];
				SignatureType signatureType = type as SignatureType;
				if (signatureType != null)
				{
					if (!signatureType.MatchesExactly(genericTypeArguments2[i]))
					{
						return false;
					}
				}
				else if (type != genericTypeArguments2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x000EFBBD File Offset: 0x000EDDBD
		internal static Type TryResolveAgainstGenericMethod(this SignatureType signatureType, MethodInfo genericMethod)
		{
			return signatureType.TryResolve(genericMethod.GetGenericArguments());
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x000EFBCC File Offset: 0x000EDDCC
		private static Type TryResolve(this SignatureType signatureType, Type[] genericMethodParameters)
		{
			if (signatureType.IsSZArray)
			{
				Type type = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type == null)
				{
					return null;
				}
				return type.TryMakeArrayType();
			}
			else if (signatureType.IsVariableBoundArray)
			{
				Type type2 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type2 == null)
				{
					return null;
				}
				return type2.TryMakeArrayType(signatureType.GetArrayRank());
			}
			else if (signatureType.IsByRef)
			{
				Type type3 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type3 == null)
				{
					return null;
				}
				return type3.TryMakeByRefType();
			}
			else if (signatureType.IsPointer)
			{
				Type type4 = signatureType.ElementType.TryResolve(genericMethodParameters);
				if (type4 == null)
				{
					return null;
				}
				return type4.TryMakePointerType();
			}
			else
			{
				if (signatureType.IsConstructedGenericType)
				{
					Type[] genericTypeArguments = signatureType.GenericTypeArguments;
					int num = genericTypeArguments.Length;
					Type[] array = new Type[num];
					for (int i = 0; i < num; i++)
					{
						Type type5 = genericTypeArguments[i];
						SignatureType signatureType2 = type5 as SignatureType;
						if (signatureType2 != null)
						{
							array[i] = signatureType2.TryResolve(genericMethodParameters);
							if (array[i] == null)
							{
								return null;
							}
						}
						else
						{
							array[i] = type5;
						}
					}
					return signatureType.GetGenericTypeDefinition().TryMakeGenericType(array);
				}
				if (!signatureType.IsGenericMethodParameter)
				{
					return null;
				}
				int genericParameterPosition = signatureType.GenericParameterPosition;
				if (genericParameterPosition >= genericMethodParameters.Length)
				{
					return null;
				}
				return genericMethodParameters[genericParameterPosition];
			}
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x000EFCE0 File Offset: 0x000EDEE0
		private static Type TryMakeArrayType(this Type type)
		{
			Type type2;
			try
			{
				type2 = type.MakeArrayType();
			}
			catch
			{
				type2 = null;
			}
			return type2;
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x000EFD0C File Offset: 0x000EDF0C
		private static Type TryMakeArrayType(this Type type, int rank)
		{
			Type type2;
			try
			{
				type2 = type.MakeArrayType(rank);
			}
			catch
			{
				type2 = null;
			}
			return type2;
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x000EFD3C File Offset: 0x000EDF3C
		private static Type TryMakeByRefType(this Type type)
		{
			Type type2;
			try
			{
				type2 = type.MakeByRefType();
			}
			catch
			{
				type2 = null;
			}
			return type2;
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x000EFD68 File Offset: 0x000EDF68
		private static Type TryMakePointerType(this Type type)
		{
			Type type2;
			try
			{
				type2 = type.MakePointerType();
			}
			catch
			{
				type2 = null;
			}
			return type2;
		}

		// Token: 0x06004AE7 RID: 19175 RVA: 0x000EFD94 File Offset: 0x000EDF94
		private static Type TryMakeGenericType(this Type type, Type[] instantiation)
		{
			Type type2;
			try
			{
				type2 = type.MakeGenericType(instantiation);
			}
			catch
			{
				type2 = null;
			}
			return type2;
		}
	}
}
