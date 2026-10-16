using System;
using System.Reflection;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x0200006E RID: 110
	public class MethodWrapper : IMethodInfo, IReflectionInfo
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x000083B8 File Offset: 0x000065B8
		public MethodWrapper(Type type, MethodInfo method)
		{
			this.TypeInfo = new TypeWrapper(type);
			this.MethodInfo = method;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000083D3 File Offset: 0x000065D3
		public MethodWrapper(Type type, string methodName)
		{
			this.TypeInfo = new TypeWrapper(type);
			this.MethodInfo = type.GetMethod(methodName);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000083F4 File Offset: 0x000065F4
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000083FC File Offset: 0x000065FC
		public ITypeInfo TypeInfo { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00008405 File Offset: 0x00006605
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000840D File Offset: 0x0000660D
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00008416 File Offset: 0x00006616
		public string Name
		{
			get
			{
				return this.MethodInfo.Name;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00008423 File Offset: 0x00006623
		public bool IsAbstract
		{
			get
			{
				return this.MethodInfo.IsAbstract;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00008430 File Offset: 0x00006630
		public bool IsPublic
		{
			get
			{
				return this.MethodInfo.IsPublic;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000843D File Offset: 0x0000663D
		public bool ContainsGenericParameters
		{
			get
			{
				return this.MethodInfo.ContainsGenericParameters;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000844A File Offset: 0x0000664A
		public bool IsGenericMethod
		{
			get
			{
				return this.MethodInfo.IsGenericMethod;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00008457 File Offset: 0x00006657
		public bool IsGenericMethodDefinition
		{
			get
			{
				return this.MethodInfo.IsGenericMethodDefinition;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00008464 File Offset: 0x00006664
		public ITypeInfo ReturnType
		{
			get
			{
				return new TypeWrapper(this.MethodInfo.ReturnType);
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00008478 File Offset: 0x00006678
		public IParameterInfo[] GetParameters()
		{
			ParameterInfo[] parameters = this.MethodInfo.GetParameters();
			IParameterInfo[] array = new IParameterInfo[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = new ParameterWrapper(this, parameters[i]);
			}
			return array;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000084B5 File Offset: 0x000066B5
		public Type[] GetGenericArguments()
		{
			return this.MethodInfo.GetGenericArguments();
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000084C2 File Offset: 0x000066C2
		public IMethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return new MethodWrapper(this.TypeInfo.Type, this.MethodInfo.MakeGenericMethod(typeArguments));
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000084E0 File Offset: 0x000066E0
		public T[] GetCustomAttributes<T>(bool inherit) where T : class
		{
			return (T[])this.MethodInfo.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000084FD File Offset: 0x000066FD
		public bool IsDefined<T>(bool inherit)
		{
			return this.MethodInfo.IsDefined(typeof(T), inherit);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00008515 File Offset: 0x00006715
		public object Invoke(object fixture, params object[] args)
		{
			return Reflect.InvokeMethod(this.MethodInfo, fixture, args);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00008416 File Offset: 0x00006616
		public override string ToString()
		{
			return this.MethodInfo.Name;
		}
	}
}
