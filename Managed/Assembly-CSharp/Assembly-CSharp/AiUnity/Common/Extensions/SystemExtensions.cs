using System;
using System.Linq;

namespace AiUnity.Common.Extensions
{
	// Token: 0x020002E1 RID: 737
	public static class SystemExtensions
	{
		// Token: 0x0600195E RID: 6494 RVA: 0x000811FC File Offset: 0x0007F3FC
		public static string GetCSharpName(this Type type, bool fullName = false)
		{
			if (type == null || type.Equals(typeof(void)))
			{
				return "void";
			}
			Type underlyingType = Nullable.GetUnderlyingType(type);
			Type type2 = ((underlyingType != null) ? underlyingType : type);
			string text = ((underlyingType != null) ? "?" : string.Empty);
			if (type2.IsGenericType)
			{
				return string.Format("{0}<{1}>{2}", type2.Name.Substring(0, type2.Name.IndexOf('`')), string.Join(", ", (from ga in type2.GetGenericArguments()
					select ga.GetCSharpName(false)).ToArray<string>()), text);
			}
			switch (Type.GetTypeCode(type2))
			{
			case TypeCode.Object:
				return (fullName ? type2.FullName : type2.Name) + text;
			case TypeCode.Boolean:
				return "bool";
			case TypeCode.Char:
				return "char";
			case TypeCode.SByte:
				return "sbyte";
			case TypeCode.Byte:
				return "byte";
			case TypeCode.Int16:
				return "short" + text;
			case TypeCode.UInt16:
				return "ushort" + text;
			case TypeCode.Int32:
				return "int" + text;
			case TypeCode.UInt32:
				return "uint" + text;
			case TypeCode.Int64:
				return "long" + text;
			case TypeCode.UInt64:
				return "ulong" + text;
			case TypeCode.Single:
				return "float" + text;
			case TypeCode.Double:
				return "double" + text;
			case TypeCode.Decimal:
				return "decimal" + text;
			case TypeCode.String:
				return "string";
			}
			return null;
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000813BC File Offset: 0x0007F5BC
		public static int GetInheritanceDepth(this Type type)
		{
			int num = 0;
			Type type2 = type;
			while (type2 != null)
			{
				num++;
				type2 = type2.BaseType;
			}
			return num;
		}
	}
}
