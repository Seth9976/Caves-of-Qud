using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x0200013C RID: 316
	public struct NamedValue : IEquatable<NamedValue>
	{
		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001195 RID: 4501 RVA: 0x000546E0 File Offset: 0x000528E0
		// (set) Token: 0x06001196 RID: 4502 RVA: 0x000546E8 File Offset: 0x000528E8
		public string name { readonly get; set; }

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001197 RID: 4503 RVA: 0x000546F1 File Offset: 0x000528F1
		// (set) Token: 0x06001198 RID: 4504 RVA: 0x000546F9 File Offset: 0x000528F9
		public PrimitiveValue value { readonly get; set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001199 RID: 4505 RVA: 0x00054704 File Offset: 0x00052904
		public TypeCode type
		{
			get
			{
				return this.value.type;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00054720 File Offset: 0x00052920
		public NamedValue ConvertTo(TypeCode type)
		{
			return new NamedValue
			{
				name = this.name,
				value = this.value.ConvertTo(type)
			};
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0005475C File Offset: 0x0005295C
		public static NamedValue From<TValue>(string name, TValue value) where TValue : struct
		{
			return new NamedValue
			{
				name = name,
				value = PrimitiveValue.From<TValue>(value)
			};
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00054787 File Offset: 0x00052987
		public override string ToString()
		{
			return string.Format("{0}={1}", this.name, this.value);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000547A4 File Offset: 0x000529A4
		public bool Equals(NamedValue other)
		{
			return string.Equals(this.name, other.name, StringComparison.InvariantCultureIgnoreCase) && this.value == other.value;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000547D0 File Offset: 0x000529D0
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is NamedValue)
			{
				NamedValue namedValue = (NamedValue)obj;
				return this.Equals(namedValue);
			}
			return false;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000547FC File Offset: 0x000529FC
		public override int GetHashCode()
		{
			return (((this.name != null) ? this.name.GetHashCode() : 0) * 397) ^ this.value.GetHashCode();
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0005483A File Offset: 0x00052A3A
		public static bool operator ==(NamedValue left, NamedValue right)
		{
			return left.Equals(right);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x00054844 File Offset: 0x00052A44
		public static bool operator !=(NamedValue left, NamedValue right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x00054854 File Offset: 0x00052A54
		public static NamedValue[] ParseMultiple(string parameterString)
		{
			if (parameterString == null)
			{
				throw new ArgumentNullException("parameterString");
			}
			parameterString = parameterString.Trim();
			if (string.IsNullOrEmpty(parameterString))
			{
				return null;
			}
			int num = parameterString.CountOccurrences(","[0]) + 1;
			NamedValue[] array = new NamedValue[num];
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				NamedValue namedValue = NamedValue.ParseParameter(parameterString, ref num2);
				array[i] = namedValue;
			}
			return array;
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000548C0 File Offset: 0x00052AC0
		public static NamedValue Parse(string str)
		{
			int num = 0;
			return NamedValue.ParseParameter(str, ref num);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000548D8 File Offset: 0x00052AD8
		private static NamedValue ParseParameter(string parameterString, ref int index)
		{
			NamedValue namedValue = default(NamedValue);
			int length = parameterString.Length;
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			int num = index;
			while (index < length)
			{
				char c = parameterString[index];
				if (c == '=' || c == ","[0] || char.IsWhiteSpace(c))
				{
					break;
				}
				index++;
			}
			namedValue.name = parameterString.Substring(num, index - num);
			while (index < length && char.IsWhiteSpace(parameterString[index]))
			{
				index++;
			}
			if (index == length || parameterString[index] != '=')
			{
				namedValue.value = true;
			}
			else
			{
				index++;
				while (index < length && char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				int num2 = index;
				while (index < length && parameterString[index] != ","[0] && !char.IsWhiteSpace(parameterString[index]))
				{
					index++;
				}
				string text = parameterString.Substring(num2, index - num2);
				namedValue.value = PrimitiveValue.FromString(text);
			}
			if (index < length && parameterString[index] == ","[0])
			{
				index++;
			}
			return namedValue;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00054A28 File Offset: 0x00052C28
		public void ApplyToObject(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			Type type = instance.GetType();
			FieldInfo field = type.GetField(this.name, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new ArgumentException(string.Concat(new string[] { "Cannot find public field '", this.name, "' in '", type.Name, "' (while trying to apply parameter)" }), "instance");
			}
			TypeCode typeCode = Type.GetTypeCode(field.FieldType);
			field.SetValue(instance, this.value.ConvertTo(typeCode).ToObject());
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x00054ACC File Offset: 0x00052CCC
		public static void ApplyAllToObject<TParameterList>(object instance, TParameterList parameters) where TParameterList : IEnumerable<NamedValue>
		{
			foreach (NamedValue namedValue in parameters)
			{
				namedValue.ApplyToObject(instance);
			}
		}

		// Token: 0x04000723 RID: 1827
		public const string Separator = ",";
	}
}
