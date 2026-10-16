using System;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000071 RID: 113
	public class ParameterWrapper : IParameterInfo, IReflectionInfo
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00008A43 File Offset: 0x00006C43
		public ParameterWrapper(IMethodInfo method, ParameterInfo parameterInfo)
		{
			this.Method = method;
			this.ParameterInfo = parameterInfo;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00008A59 File Offset: 0x00006C59
		public bool IsOptional
		{
			get
			{
				return this.ParameterInfo.IsOptional;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x00008A66 File Offset: 0x00006C66
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x00008A6E File Offset: 0x00006C6E
		public IMethodInfo Method { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x00008A77 File Offset: 0x00006C77
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x00008A7F File Offset: 0x00006C7F
		public ParameterInfo ParameterInfo { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x00008A88 File Offset: 0x00006C88
		public Type ParameterType
		{
			get
			{
				return this.ParameterInfo.ParameterType;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00008A95 File Offset: 0x00006C95
		public T[] GetCustomAttributes<T>(bool inherit) where T : class
		{
			return (T[])this.ParameterInfo.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00008AB2 File Offset: 0x00006CB2
		public bool IsDefined<T>(bool inherit)
		{
			return this.ParameterInfo.IsDefined(typeof(T), inherit);
		}
	}
}
