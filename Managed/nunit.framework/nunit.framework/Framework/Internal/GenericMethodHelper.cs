using System;
using System.Reflection;
using NUnit.Compatibility;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000063 RID: 99
	public class GenericMethodHelper
	{
		// Token: 0x06000358 RID: 856 RVA: 0x000076E8 File Offset: 0x000058E8
		public GenericMethodHelper(MethodInfo method)
		{
			Guard.ArgumentValid(method.IsGenericMethod, "Specified method must be generic", "method");
			this.Method = method;
			this.TypeParms = this.Method.GetGenericArguments();
			this.TypeArgs = new Type[this.TypeParms.Length];
			ParameterInfo[] parameters = this.Method.GetParameters();
			this.ParmTypes = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				this.ParmTypes[i] = parameters[i].ParameterType;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00007773 File Offset: 0x00005973
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000777B File Offset: 0x0000597B
		private MethodInfo Method { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00007784 File Offset: 0x00005984
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000778C File Offset: 0x0000598C
		private Type[] TypeParms { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00007795 File Offset: 0x00005995
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000779D File Offset: 0x0000599D
		private Type[] TypeArgs { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000077A6 File Offset: 0x000059A6
		// (set) Token: 0x06000360 RID: 864 RVA: 0x000077AE File Offset: 0x000059AE
		private Type[] ParmTypes { get; set; }

		// Token: 0x06000361 RID: 865 RVA: 0x000077B8 File Offset: 0x000059B8
		public Type[] GetTypeArguments(object[] argList)
		{
			Guard.ArgumentValid(argList.Length == this.ParmTypes.Length, "Supplied arguments do not match required method parameters", "argList");
			for (int i = 0; i < this.ParmTypes.Length; i++)
			{
				object obj = argList[i];
				if (obj != null)
				{
					Type type = obj.GetType();
					this.TryApplyArgType(this.ParmTypes[i], type);
				}
			}
			return this.TypeArgs;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00007818 File Offset: 0x00005A18
		private void TryApplyArgType(Type parmType, Type argType)
		{
			if (parmType.IsGenericParameter)
			{
				this.ApplyArgType(parmType, argType);
				return;
			}
			if (parmType.GetTypeInfo().ContainsGenericParameters)
			{
				Type[] genericArguments = parmType.GetGenericArguments();
				if (argType.HasElementType)
				{
					this.ApplyArgType(genericArguments[0], argType.GetElementType());
					return;
				}
				if (argType.GetTypeInfo().IsGenericType && this.IsAssignableToGenericType(argType, parmType))
				{
					Type[] genericArguments2 = argType.GetGenericArguments();
					if (genericArguments2.Length == genericArguments.Length)
					{
						for (int i = 0; i < genericArguments.Length; i++)
						{
							this.TryApplyArgType(genericArguments[i], genericArguments2[i]);
						}
					}
				}
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000078A4 File Offset: 0x00005AA4
		private void ApplyArgType(Type parmType, Type argType)
		{
			int genericParameterPosition = parmType.GenericParameterPosition;
			this.TypeArgs[genericParameterPosition] = TypeHelper.BestCommonType(this.TypeArgs[genericParameterPosition], argType);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000078D0 File Offset: 0x00005AD0
		private bool IsAssignableToGenericType(Type givenType, Type genericType)
		{
			foreach (Type type in givenType.GetInterfaces())
			{
				if (type.GetTypeInfo().IsGenericType)
				{
					Type genericTypeDefinition = type.GetGenericTypeDefinition();
					if (genericTypeDefinition.Name == genericType.Name && genericTypeDefinition.Namespace == genericType.Namespace)
					{
						return true;
					}
				}
			}
			if (givenType.GetTypeInfo().IsGenericType)
			{
				Type genericTypeDefinition2 = givenType.GetGenericTypeDefinition();
				if (genericTypeDefinition2.Name == genericType.Name && genericTypeDefinition2.Namespace == genericType.Namespace)
				{
					return true;
				}
			}
			Type baseType = givenType.GetTypeInfo().BaseType;
			return !(baseType == null) && this.IsAssignableToGenericType(baseType, genericType);
		}
	}
}
