using System;
using System.Collections.Generic;
using System.Reflection;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000A9 RID: 169
	public class ReflectionImporter : IReflectionImporter
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x0001520D File Offset: 0x0001340D
		public ReflectionImporter(ModuleDefinition module)
		{
			Mixin.CheckModule(module);
			this.module = module;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00015222 File Offset: 0x00013422
		private TypeReference ImportType(Type type, ImportGenericContext context)
		{
			return this.ImportType(type, context, ReflectionImporter.ImportGenericKind.Open);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00015230 File Offset: 0x00013430
		private TypeReference ImportType(Type type, ImportGenericContext context, ReflectionImporter.ImportGenericKind import_kind)
		{
			if (ReflectionImporter.IsTypeSpecification(type) || ReflectionImporter.ImportOpenGenericType(type, import_kind))
			{
				return this.ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(string.Empty, type.Name, this.module, this.ImportScope(type.Assembly()), type.IsValueType());
			typeReference.etype = ReflectionImporter.ImportElementType(type);
			if (ReflectionImporter.IsNestedType(type))
			{
				typeReference.DeclaringType = this.ImportType(type.DeclaringType, context, import_kind);
			}
			else
			{
				typeReference.Namespace = type.Namespace ?? string.Empty;
			}
			if (type.IsGenericType())
			{
				ReflectionImporter.ImportGenericParameters(typeReference, type.GetGenericArguments());
			}
			return typeReference;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x000152D4 File Offset: 0x000134D4
		private static bool ImportOpenGenericType(Type type, ReflectionImporter.ImportGenericKind import_kind)
		{
			return type.IsGenericType() && type.IsGenericTypeDefinition() && import_kind == ReflectionImporter.ImportGenericKind.Open;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x000152EC File Offset: 0x000134EC
		private static bool ImportOpenGenericMethod(MethodBase method, ReflectionImporter.ImportGenericKind import_kind)
		{
			return method.IsGenericMethod && method.IsGenericMethodDefinition && import_kind == ReflectionImporter.ImportGenericKind.Open;
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x00015304 File Offset: 0x00013504
		private static bool IsNestedType(Type type)
		{
			return type.IsNested;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0001530C File Offset: 0x0001350C
		private TypeReference ImportTypeSpecification(Type type, ImportGenericContext context)
		{
			if (type.IsByRef)
			{
				return new ByReferenceType(this.ImportType(type.GetElementType(), context));
			}
			if (type.IsPointer)
			{
				return new PointerType(this.ImportType(type.GetElementType(), context));
			}
			if (type.IsArray)
			{
				return new ArrayType(this.ImportType(type.GetElementType(), context), type.GetArrayRank());
			}
			if (type.IsGenericType())
			{
				return this.ImportGenericInstance(type, context);
			}
			if (type.IsGenericParameter)
			{
				return ReflectionImporter.ImportGenericParameter(type, context);
			}
			throw new NotSupportedException(type.FullName);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0001539C File Offset: 0x0001359C
		private static TypeReference ImportGenericParameter(Type type, ImportGenericContext context)
		{
			if (context.IsEmpty)
			{
				throw new InvalidOperationException();
			}
			if (type.DeclaringMethod() != null)
			{
				return context.MethodParameter(ReflectionImporter.NormalizeMethodName(type.DeclaringMethod()), type.GenericParameterPosition);
			}
			if (type.DeclaringType != null)
			{
				return context.TypeParameter(ReflectionImporter.NormalizeTypeFullName(type.DeclaringType), type.GenericParameterPosition);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x000153FF File Offset: 0x000135FF
		private static string NormalizeMethodName(MethodBase method)
		{
			return ReflectionImporter.NormalizeTypeFullName(method.DeclaringType) + "." + method.Name;
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0001541C File Offset: 0x0001361C
		private static string NormalizeTypeFullName(Type type)
		{
			if (ReflectionImporter.IsNestedType(type))
			{
				return ReflectionImporter.NormalizeTypeFullName(type.DeclaringType) + "/" + type.Name;
			}
			return type.FullName;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00015448 File Offset: 0x00013648
		private TypeReference ImportGenericInstance(Type type, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(type.GetGenericTypeDefinition(), context, ReflectionImporter.ImportGenericKind.Definition);
			GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference);
			Type[] genericArguments = type.GetGenericArguments();
			Collection<TypeReference> genericArguments2 = genericInstanceType.GenericArguments;
			context.Push(typeReference);
			TypeReference typeReference2;
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(this.ImportType(genericArguments[i], context));
				}
				typeReference2 = genericInstanceType;
			}
			finally
			{
				context.Pop();
			}
			return typeReference2;
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000154C4 File Offset: 0x000136C4
		private static bool IsTypeSpecification(Type type)
		{
			return type.HasElementType || ReflectionImporter.IsGenericInstance(type) || type.IsGenericParameter;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000154DE File Offset: 0x000136DE
		private static bool IsGenericInstance(Type type)
		{
			return type.IsGenericType() && !type.IsGenericTypeDefinition();
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000154F4 File Offset: 0x000136F4
		private static ElementType ImportElementType(Type type)
		{
			ElementType elementType;
			if (!ReflectionImporter.type_etype_mapping.TryGetValue(type, ref elementType))
			{
				return ElementType.None;
			}
			return elementType;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00015514 File Offset: 0x00013714
		private AssemblyNameReference ImportScope(Assembly assembly)
		{
			AssemblyName name = assembly.GetName();
			AssemblyNameReference assemblyNameReference;
			if (this.TryGetAssemblyNameReference(name, out assemblyNameReference))
			{
				return assemblyNameReference;
			}
			assemblyNameReference = new AssemblyNameReference(name.Name, name.Version)
			{
				PublicKeyToken = name.GetPublicKeyToken(),
				Culture = name.CultureInfo.Name,
				HashAlgorithm = name.HashAlgorithm
			};
			this.module.AssemblyReferences.Add(assemblyNameReference);
			return assemblyNameReference;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00015584 File Offset: 0x00013784
		private bool TryGetAssemblyNameReference(AssemblyName name, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = this.module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (!(name.FullName != assemblyNameReference.FullName))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x000155D4 File Offset: 0x000137D4
		private FieldReference ImportField(FieldInfo field, ImportGenericContext context)
		{
			TypeReference typeReference = this.ImportType(field.DeclaringType, context);
			if (ReflectionImporter.IsGenericInstance(field.DeclaringType))
			{
				field = ReflectionImporter.ResolveFieldDefinition(field);
			}
			context.Push(typeReference);
			FieldReference fieldReference;
			try
			{
				fieldReference = new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = this.ImportType(field.FieldType, context)
				};
			}
			finally
			{
				context.Pop();
			}
			return fieldReference;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00015654 File Offset: 0x00013854
		private static FieldInfo ResolveFieldDefinition(FieldInfo field)
		{
			return field.Module.ResolveField(field.MetadataToken);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00015667 File Offset: 0x00013867
		private static MethodBase ResolveMethodDefinition(MethodBase method)
		{
			return method.Module.ResolveMethod(method.MetadataToken);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0001567C File Offset: 0x0001387C
		private MethodReference ImportMethod(MethodBase method, ImportGenericContext context, ReflectionImporter.ImportGenericKind import_kind)
		{
			if (ReflectionImporter.IsMethodSpecification(method) || ReflectionImporter.ImportOpenGenericMethod(method, import_kind))
			{
				return this.ImportMethodSpecification(method, context);
			}
			TypeReference typeReference = this.ImportType(method.DeclaringType, context);
			if (ReflectionImporter.IsGenericInstance(method.DeclaringType))
			{
				method = ReflectionImporter.ResolveMethodDefinition(method);
			}
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = ReflectionImporter.HasCallingConvention(method, 32),
				ExplicitThis = ReflectionImporter.HasCallingConvention(method, 64),
				DeclaringType = this.ImportType(method.DeclaringType, context, ReflectionImporter.ImportGenericKind.Definition)
			};
			if (ReflectionImporter.HasCallingConvention(method, 2))
			{
				methodReference.CallingConvention &= MethodCallingConvention.VarArg;
			}
			if (method.IsGenericMethod)
			{
				ReflectionImporter.ImportGenericParameters(methodReference, method.GetGenericArguments());
			}
			context.Push(methodReference);
			MethodReference methodReference2;
			try
			{
				MethodInfo methodInfo = method as MethodInfo;
				methodReference.ReturnType = ((methodInfo != null) ? this.ImportType(methodInfo.ReturnType, context) : this.ImportType(typeof(void), default(ImportGenericContext)));
				ParameterInfo[] parameters = method.GetParameters();
				Collection<ParameterDefinition> parameters2 = methodReference.Parameters;
				for (int i = 0; i < parameters.Length; i++)
				{
					parameters2.Add(new ParameterDefinition(this.ImportType(parameters[i].ParameterType, context)));
				}
				methodReference.DeclaringType = typeReference;
				methodReference2 = methodReference;
			}
			finally
			{
				context.Pop();
			}
			return methodReference2;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x000157D8 File Offset: 0x000139D8
		private static void ImportGenericParameters(IGenericParameterProvider provider, Type[] arguments)
		{
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < arguments.Length; i++)
			{
				genericParameters.Add(new GenericParameter(arguments[i].Name, provider));
			}
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0001580E File Offset: 0x00013A0E
		private static bool IsMethodSpecification(MethodBase method)
		{
			return method.IsGenericMethod && !method.IsGenericMethodDefinition;
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00015824 File Offset: 0x00013A24
		private MethodReference ImportMethodSpecification(MethodBase method, ImportGenericContext context)
		{
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo == null)
			{
				throw new InvalidOperationException();
			}
			MethodReference methodReference = this.ImportMethod(methodInfo.GetGenericMethodDefinition(), context, ReflectionImporter.ImportGenericKind.Definition);
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(methodReference);
			Type[] genericArguments = method.GetGenericArguments();
			Collection<TypeReference> genericArguments2 = genericInstanceMethod.GenericArguments;
			context.Push(methodReference);
			MethodReference methodReference2;
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(this.ImportType(genericArguments[i], context));
				}
				methodReference2 = genericInstanceMethod;
			}
			finally
			{
				context.Pop();
			}
			return methodReference2;
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x000158B4 File Offset: 0x00013AB4
		private static bool HasCallingConvention(MethodBase method, CallingConventions conventions)
		{
			return (method.CallingConvention & conventions) > 0;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x000158C1 File Offset: 0x00013AC1
		public virtual TypeReference ImportReference(Type type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			return this.ImportType(type, ImportGenericContext.For(context), (context != null) ? ReflectionImporter.ImportGenericKind.Open : ReflectionImporter.ImportGenericKind.Definition);
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x000158DD File Offset: 0x00013ADD
		public virtual FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			return this.ImportField(field, ImportGenericContext.For(context));
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x000158F2 File Offset: 0x00013AF2
		public virtual MethodReference ImportReference(MethodBase method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			return this.ImportMethod(method, ImportGenericContext.For(context), (context != null) ? ReflectionImporter.ImportGenericKind.Open : ReflectionImporter.ImportGenericKind.Definition);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00015910 File Offset: 0x00013B10
		// Note: this type is marked as 'beforefieldinit'.
		static ReflectionImporter()
		{
			Dictionary<Type, ElementType> dictionary = new Dictionary<Type, ElementType>(18);
			dictionary.Add(typeof(void), ElementType.Void);
			dictionary.Add(typeof(bool), ElementType.Boolean);
			dictionary.Add(typeof(char), ElementType.Char);
			dictionary.Add(typeof(sbyte), ElementType.I1);
			dictionary.Add(typeof(byte), ElementType.U1);
			dictionary.Add(typeof(short), ElementType.I2);
			dictionary.Add(typeof(ushort), ElementType.U2);
			dictionary.Add(typeof(int), ElementType.I4);
			dictionary.Add(typeof(uint), ElementType.U4);
			dictionary.Add(typeof(long), ElementType.I8);
			dictionary.Add(typeof(ulong), ElementType.U8);
			dictionary.Add(typeof(float), ElementType.R4);
			dictionary.Add(typeof(double), ElementType.R8);
			dictionary.Add(typeof(string), ElementType.String);
			dictionary.Add(typeof(TypedReference), ElementType.TypedByRef);
			dictionary.Add(typeof(IntPtr), ElementType.I);
			dictionary.Add(typeof(UIntPtr), ElementType.U);
			dictionary.Add(typeof(object), ElementType.Object);
			ReflectionImporter.type_etype_mapping = dictionary;
		}

		// Token: 0x04000250 RID: 592
		private readonly ModuleDefinition module;

		// Token: 0x04000251 RID: 593
		private static readonly Dictionary<Type, ElementType> type_etype_mapping;

		// Token: 0x02000147 RID: 327
		private enum ImportGenericKind
		{
			// Token: 0x04000736 RID: 1846
			Definition,
			// Token: 0x04000737 RID: 1847
			Open
		}
	}
}
