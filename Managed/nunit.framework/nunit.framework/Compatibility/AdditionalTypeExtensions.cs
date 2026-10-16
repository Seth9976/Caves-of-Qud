using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NUnit.Compatibility
{
	// Token: 0x02000008 RID: 8
	public static class AdditionalTypeExtensions
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020F0 File Offset: 0x000002F0
		public static bool ParametersMatch(this ParameterInfo[] pinfos, Type[] ptypes)
		{
			if (pinfos.Length != ptypes.Length)
			{
				return false;
			}
			for (int i = 0; i < pinfos.Length; i++)
			{
				if (!pinfos[i].ParameterType.IsCastableFrom(ptypes[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public static bool IsCastableFrom(this Type to, Type from)
		{
			return to.IsAssignableFrom(from) || (from == typeof(NUnitNullType) && (to.GetTypeInfo().IsClass || to.FullName.StartsWith("System.Nullable"))) || (AdditionalTypeExtensions.convertibleValueTypes.ContainsKey(to) && AdditionalTypeExtensions.convertibleValueTypes[to].Contains(from)) || from.GetMethods(BindingFlags.Static | BindingFlags.Public).Any<MethodInfo>((MethodInfo m) => m.ReturnType == to && m.Name == "op_Implicit");
		}

		// Token: 0x04000011 RID: 17
		private static Dictionary<Type, List<Type>> convertibleValueTypes = new Dictionary<Type, List<Type>>
		{
			{
				typeof(decimal),
				new List<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char)
				}
			},
			{
				typeof(double),
				new List<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float)
				}
			},
			{
				typeof(float),
				new List<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(long),
					typeof(ulong),
					typeof(char),
					typeof(float)
				}
			},
			{
				typeof(ulong),
				new List<Type>
				{
					typeof(byte),
					typeof(ushort),
					typeof(uint),
					typeof(char)
				}
			},
			{
				typeof(long),
				new List<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(int),
					typeof(uint),
					typeof(char)
				}
			},
			{
				typeof(uint),
				new List<Type>
				{
					typeof(byte),
					typeof(ushort),
					typeof(char)
				}
			},
			{
				typeof(int),
				new List<Type>
				{
					typeof(sbyte),
					typeof(byte),
					typeof(short),
					typeof(ushort),
					typeof(char)
				}
			},
			{
				typeof(ushort),
				new List<Type>
				{
					typeof(byte),
					typeof(char)
				}
			},
			{
				typeof(short),
				new List<Type> { typeof(byte) }
			}
		};
	}
}
