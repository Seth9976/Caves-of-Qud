using System;
using System.Collections;
using System.Globalization;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
	public class ValuesAttribute : DataAttribute, IParameterDataSource
	{
		// Token: 0x0600020B RID: 523 RVA: 0x00005E01 File Offset: 0x00004001
		public ValuesAttribute()
		{
			this.data = new object[0];
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00005E15 File Offset: 0x00004015
		public ValuesAttribute(object arg1)
		{
			this.data = new object[] { arg1 };
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00005E2D File Offset: 0x0000402D
		public ValuesAttribute(object arg1, object arg2)
		{
			this.data = new object[] { arg1, arg2 };
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00005E49 File Offset: 0x00004049
		public ValuesAttribute(object arg1, object arg2, object arg3)
		{
			this.data = new object[] { arg1, arg2, arg3 };
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00005E69 File Offset: 0x00004069
		public ValuesAttribute(params object[] args)
		{
			this.data = args;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00005E78 File Offset: 0x00004078
		public IEnumerable GetData(IParameterInfo parameter)
		{
			Type parameterType = parameter.ParameterType;
			if (parameterType.GetTypeInfo().IsEnum && this.data.Length == 0)
			{
				return TypeHelper.GetEnumValues(parameterType);
			}
			if (parameterType == typeof(bool) && this.data.Length == 0)
			{
				return new object[] { true, false };
			}
			return this.GetData(parameterType);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00005EE4 File Offset: 0x000040E4
		private IEnumerable GetData(Type targetType)
		{
			for (int i = 0; i < this.data.Length; i++)
			{
				object obj = this.data[i];
				if (obj != null)
				{
					if (obj.GetType().FullName == "NUnit.Framework.SpecialValue" && obj.ToString() == "Null")
					{
						this.data[i] = null;
					}
					else if (!targetType.GetTypeInfo().IsAssignableFrom(obj.GetType().GetTypeInfo()))
					{
						if (obj is DBNull)
						{
							this.data[i] = null;
						}
						else
						{
							bool flag = false;
							if (targetType == typeof(short) || targetType == typeof(byte) || targetType == typeof(sbyte))
							{
								flag = obj is int;
							}
							else if (targetType == typeof(decimal))
							{
								flag = obj is double || obj is string || obj is int;
							}
							else if (targetType == typeof(DateTime) || targetType == typeof(TimeSpan))
							{
								flag = obj is string;
							}
							if (flag)
							{
								this.data[i] = Convert.ChangeType(obj, targetType, CultureInfo.InvariantCulture);
							}
						}
					}
				}
			}
			return this.data;
		}

		// Token: 0x04000058 RID: 88
		protected object[] data;
	}
}
