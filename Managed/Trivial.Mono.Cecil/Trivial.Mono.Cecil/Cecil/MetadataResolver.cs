using System;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000072 RID: 114
	public class MetadataResolver : IMetadataResolver
	{
		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000EB5C File Offset: 0x0000CD5C
		public MetadataResolver(IAssemblyResolver assemblyResolver)
		{
			if (assemblyResolver == null)
			{
				throw new ArgumentNullException("assemblyResolver");
			}
			this.assembly_resolver = assemblyResolver;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000EB7C File Offset: 0x0000CD7C
		public virtual TypeDefinition Resolve(TypeReference type)
		{
			Mixin.CheckType(type);
			type = type.GetElementType();
			IMetadataScope scope = type.Scope;
			if (scope == null)
			{
				return null;
			}
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
			{
				AssemblyDefinition assemblyDefinition = this.assembly_resolver.Resolve((AssemblyNameReference)scope);
				if (assemblyDefinition == null)
				{
					return null;
				}
				return MetadataResolver.GetType(assemblyDefinition.MainModule, type);
			}
			case MetadataScopeType.ModuleReference:
			{
				Collection<ModuleDefinition> modules = type.Module.Assembly.Modules;
				ModuleReference moduleReference = (ModuleReference)scope;
				for (int i = 0; i < modules.Count; i++)
				{
					ModuleDefinition moduleDefinition = modules[i];
					if (moduleDefinition.Name == moduleReference.Name)
					{
						return MetadataResolver.GetType(moduleDefinition, type);
					}
				}
				break;
			}
			case MetadataScopeType.ModuleDefinition:
				return MetadataResolver.GetType((ModuleDefinition)scope, type);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000EC50 File Offset: 0x0000CE50
		private static TypeDefinition GetType(ModuleDefinition module, TypeReference reference)
		{
			TypeDefinition typeDefinition = MetadataResolver.GetTypeDefinition(module, reference);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			if (!module.HasExportedTypes)
			{
				return null;
			}
			Collection<ExportedType> exportedTypes = module.ExportedTypes;
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				if (!(exportedType.Name != reference.Name) && !(exportedType.Namespace != reference.Namespace))
				{
					return exportedType.Resolve();
				}
			}
			return null;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000ECC2 File Offset: 0x0000CEC2
		private static TypeDefinition GetTypeDefinition(ModuleDefinition module, TypeReference type)
		{
			if (!type.IsNested)
			{
				return module.GetType(type.Namespace, type.Name);
			}
			TypeDefinition typeDefinition = type.DeclaringType.Resolve();
			if (typeDefinition == null)
			{
				return null;
			}
			return typeDefinition.GetNestedType(type.TypeFullName());
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000ECFC File Offset: 0x0000CEFC
		public virtual FieldDefinition Resolve(FieldReference field)
		{
			Mixin.CheckField(field);
			TypeDefinition typeDefinition = this.Resolve(field.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			if (!typeDefinition.HasFields)
			{
				return null;
			}
			return this.GetField(typeDefinition, field);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000ED34 File Offset: 0x0000CF34
		private FieldDefinition GetField(TypeDefinition type, FieldReference reference)
		{
			while (type != null)
			{
				FieldDefinition field = MetadataResolver.GetField(type.Fields, reference);
				if (field != null)
				{
					return field;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = this.Resolve(type.BaseType);
			}
			return null;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000ED74 File Offset: 0x0000CF74
		private static FieldDefinition GetField(Collection<FieldDefinition> fields, FieldReference reference)
		{
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!(fieldDefinition.Name != reference.Name) && MetadataResolver.AreSame(fieldDefinition.FieldType, reference.FieldType))
				{
					return fieldDefinition;
				}
			}
			return null;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000EDC4 File Offset: 0x0000CFC4
		public virtual MethodDefinition Resolve(MethodReference method)
		{
			Mixin.CheckMethod(method);
			TypeDefinition typeDefinition = this.Resolve(method.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			method = method.GetElementMethod();
			if (!typeDefinition.HasMethods)
			{
				return null;
			}
			return this.GetMethod(typeDefinition, method);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x0000EE04 File Offset: 0x0000D004
		private MethodDefinition GetMethod(TypeDefinition type, MethodReference reference)
		{
			while (type != null)
			{
				MethodDefinition method = MetadataResolver.GetMethod(type.Methods, reference);
				if (method != null)
				{
					return method;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = this.Resolve(type.BaseType);
			}
			return null;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000EE44 File Offset: 0x0000D044
		public static MethodDefinition GetMethod(Collection<MethodDefinition> methods, MethodReference reference)
		{
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (!(methodDefinition.Name != reference.Name) && methodDefinition.HasGenericParameters == reference.HasGenericParameters && (!methodDefinition.HasGenericParameters || methodDefinition.GenericParameters.Count == reference.GenericParameters.Count) && MetadataResolver.AreSame(methodDefinition.ReturnType, reference.ReturnType) && methodDefinition.IsVarArg() == reference.IsVarArg())
				{
					if (methodDefinition.IsVarArg() && MetadataResolver.IsVarArgCallTo(methodDefinition, reference))
					{
						return methodDefinition;
					}
					if (methodDefinition.HasParameters == reference.HasParameters)
					{
						if (!methodDefinition.HasParameters && !reference.HasParameters)
						{
							return methodDefinition;
						}
						if (MetadataResolver.AreSame(methodDefinition.Parameters, reference.Parameters))
						{
							return methodDefinition;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0000EF24 File Offset: 0x0000D124
		private static bool AreSame(Collection<ParameterDefinition> a, Collection<ParameterDefinition> b)
		{
			int count = a.Count;
			if (count != b.Count)
			{
				return false;
			}
			if (count == 0)
			{
				return true;
			}
			for (int i = 0; i < count; i++)
			{
				if (!MetadataResolver.AreSame(a[i].ParameterType, b[i].ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000EF78 File Offset: 0x0000D178
		private static bool IsVarArgCallTo(MethodDefinition method, MethodReference reference)
		{
			if (method.Parameters.Count >= reference.Parameters.Count)
			{
				return false;
			}
			if (reference.GetSentinelPosition() != method.Parameters.Count)
			{
				return false;
			}
			for (int i = 0; i < method.Parameters.Count; i++)
			{
				if (!MetadataResolver.AreSame(method.Parameters[i].ParameterType, reference.Parameters[i].ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		private static bool AreSame(TypeSpecification a, TypeSpecification b)
		{
			if (!MetadataResolver.AreSame(a.ElementType, b.ElementType))
			{
				return false;
			}
			if (a.IsGenericInstance)
			{
				return MetadataResolver.AreSame((GenericInstanceType)a, (GenericInstanceType)b);
			}
			if (a.IsRequiredModifier || a.IsOptionalModifier)
			{
				return MetadataResolver.AreSame((IModifierType)a, (IModifierType)b);
			}
			return !a.IsArray || MetadataResolver.AreSame((ArrayType)a, (ArrayType)b);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000F071 File Offset: 0x0000D271
		private static bool AreSame(ArrayType a, ArrayType b)
		{
			return a.Rank == b.Rank;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000F084 File Offset: 0x0000D284
		private static bool AreSame(IModifierType a, IModifierType b)
		{
			return MetadataResolver.AreSame(a.ModifierType, b.ModifierType);
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0000F098 File Offset: 0x0000D298
		private static bool AreSame(GenericInstanceType a, GenericInstanceType b)
		{
			if (a.GenericArguments.Count != b.GenericArguments.Count)
			{
				return false;
			}
			for (int i = 0; i < a.GenericArguments.Count; i++)
			{
				if (!MetadataResolver.AreSame(a.GenericArguments[i], b.GenericArguments[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0000F0F7 File Offset: 0x0000D2F7
		private static bool AreSame(GenericParameter a, GenericParameter b)
		{
			return a.Position == b.Position;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000F108 File Offset: 0x0000D308
		private static bool AreSame(TypeReference a, TypeReference b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.etype != b.etype)
			{
				return false;
			}
			if (a.IsGenericParameter)
			{
				return MetadataResolver.AreSame((GenericParameter)a, (GenericParameter)b);
			}
			if (a.IsTypeSpecification())
			{
				return MetadataResolver.AreSame((TypeSpecification)a, (TypeSpecification)b);
			}
			return !(a.Name != b.Name) && !(a.Namespace != b.Namespace) && MetadataResolver.AreSame(a.DeclaringType, b.DeclaringType);
		}

		// Token: 0x040000F8 RID: 248
		private readonly IAssemblyResolver assembly_resolver;
	}
}
