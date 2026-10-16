using System;
using System.Linq;
using System.Reflection;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000090 RID: 144
	public class TypeWrapper : ITypeInfo, IReflectionInfo
	{
		// Token: 0x0600058C RID: 1420 RVA: 0x0000CFF3 File Offset: 0x0000B1F3
		public TypeWrapper(Type type)
		{
			Guard.ArgumentNotNull(type, "Type");
			this.Type = type;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000D00D File Offset: 0x0000B20D
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000D015 File Offset: 0x0000B215
		public Type Type { get; private set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000D020 File Offset: 0x0000B220
		public ITypeInfo BaseType
		{
			get
			{
				Type baseType = this.Type.GetTypeInfo().BaseType;
				if (!(baseType != null))
				{
					return null;
				}
				return new TypeWrapper(baseType);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000D04F File Offset: 0x0000B24F
		public string Name
		{
			get
			{
				return this.Type.Name;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000D05C File Offset: 0x0000B25C
		public string FullName
		{
			get
			{
				return this.Type.FullName;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000D069 File Offset: 0x0000B269
		public Assembly Assembly
		{
			get
			{
				return this.Type.GetTypeInfo().Assembly;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000D07B File Offset: 0x0000B27B
		public string Namespace
		{
			get
			{
				return this.Type.Namespace;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000D088 File Offset: 0x0000B288
		public bool IsAbstract
		{
			get
			{
				return this.Type.GetTypeInfo().IsAbstract;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000D09A File Offset: 0x0000B29A
		public bool IsGenericType
		{
			get
			{
				return this.Type.GetTypeInfo().IsGenericType;
			}
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0000D0AC File Offset: 0x0000B2AC
		public bool IsType(Type type)
		{
			return this.Type == type;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000D0BA File Offset: 0x0000B2BA
		public bool ContainsGenericParameters
		{
			get
			{
				return this.Type.GetTypeInfo().ContainsGenericParameters;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public bool IsGenericTypeDefinition
		{
			get
			{
				return this.Type.GetTypeInfo().IsGenericTypeDefinition;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000D0DE File Offset: 0x0000B2DE
		public bool IsSealed
		{
			get
			{
				return this.Type.GetTypeInfo().IsSealed;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public bool IsStaticClass
		{
			get
			{
				return this.Type.GetTypeInfo().IsSealed && this.Type.GetTypeInfo().IsAbstract;
			}
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0000D116 File Offset: 0x0000B316
		public string GetDisplayName()
		{
			return TypeHelper.GetDisplayName(this.Type);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x0000D123 File Offset: 0x0000B323
		public string GetDisplayName(object[] args)
		{
			return TypeHelper.GetDisplayName(this.Type, args);
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0000D131 File Offset: 0x0000B331
		public ITypeInfo MakeGenericType(Type[] typeArgs)
		{
			return new TypeWrapper(this.Type.MakeGenericType(typeArgs));
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0000D144 File Offset: 0x0000B344
		public Type GetGenericTypeDefinition()
		{
			return this.Type.GetGenericTypeDefinition();
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0000D151 File Offset: 0x0000B351
		public T[] GetCustomAttributes<T>(bool inherit) where T : class
		{
			return (T[])this.Type.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0000D16E File Offset: 0x0000B36E
		public bool IsDefined<T>(bool inherit)
		{
			return this.Type.GetTypeInfo().IsDefined(typeof(T), inherit);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x0000D18B File Offset: 0x0000B38B
		public bool HasMethodWithAttribute(Type attributeType)
		{
			return Reflect.HasMethodWithAttribute(this.Type, attributeType);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0000D19C File Offset: 0x0000B39C
		public IMethodInfo[] GetMethods(BindingFlags flags)
		{
			MethodInfo[] methods = this.Type.GetMethods(flags);
			MethodWrapper[] array = new MethodWrapper[methods.Length];
			for (int i = 0; i < methods.Length; i++)
			{
				array[i] = new MethodWrapper(this.Type, methods[i]);
			}
			return array;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		public ConstructorInfo GetConstructor(Type[] argTypes)
		{
			return (from c in this.Type.GetConstructors()
				where c.GetParameters().ParametersMatch(argTypes)
				select c).FirstOrDefault<ConstructorInfo>();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x0000D21F File Offset: 0x0000B41F
		public bool HasConstructor(Type[] argTypes)
		{
			return this.GetConstructor(argTypes) != null;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0000D22E File Offset: 0x0000B42E
		public object Construct(object[] args)
		{
			return Reflect.Construct(this.Type, args);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0000D23C File Offset: 0x0000B43C
		public override string ToString()
		{
			return this.Type.ToString();
		}
	}
}
