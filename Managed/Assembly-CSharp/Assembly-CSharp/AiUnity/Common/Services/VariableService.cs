using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AiUnity.Common.Extensions;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;

namespace AiUnity.Common.Services
{
	// Token: 0x020002BF RID: 703
	public class VariableService : Singleton<VariableService>
	{
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x0600187D RID: 6269 RVA: 0x0007F3A8 File Offset: 0x0007D5A8
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<CommonInternalLogger>.Instance;
			}
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0007F3AF File Offset: 0x0007D5AF
		public string CreateCamelVariable(params string[] names)
		{
			return this.CreatePascalVariable(names).LowercaseLetter(0);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0007F3C0 File Offset: 0x0007D5C0
		public string CreatePascalVariable(params string[] names)
		{
			if (names == null)
			{
				return null;
			}
			IEnumerable<string> enumerable = from u in names.Where<string>((string s) => !string.IsNullOrEmpty(s)).SelectMany<string, string>((string s) => s.Split(new char[] { ' ', '-', '.' }))
				select u.UppercaseLetter(0);
			return string.Join(string.Empty, enumerable.ToArray<string>());
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0007F450 File Offset: 0x0007D650
		public string GetFormattedValue(string type, object value)
		{
			if (value is string && type != null && !type.After(".", true).Trim().Equals("string", StringComparison.OrdinalIgnoreCase))
			{
				return this.GetFormattedValue(typeof(object), value);
			}
			return this.GetFormattedValue(value.GetType(), value);
		}

		// Token: 0x06001881 RID: 6273 RVA: 0x0007F4A8 File Offset: 0x0007D6A8
		public string GetFormattedValue(Type type, object value)
		{
			if (type == null || value == null)
			{
				return null;
			}
			if (!type.Equals(typeof(string)))
			{
				if (value is IConvertible && TypeDescriptor.GetConverter(type).IsValid(value))
				{
					try
					{
						string text = Convert.ChangeType(value, type).ToString();
						if (type.Equals(typeof(bool)))
						{
							return text.ToLower();
						}
						if (type.Equals(typeof(float)))
						{
							return text + "f";
						}
						return text;
					}
					catch
					{
					}
				}
				return value.ToString();
			}
			if (value.ToString() == "null" && type.IsValueType)
			{
				return null;
			}
			return string.Format("\"{0}\"", value);
		}
	}
}
