using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x02000233 RID: 563
	internal class MetadataResolver : IMetadataResolver
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00029B67 File Offset: 0x00027D67
		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return this.assembly_resolver;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00029B6F File Offset: 0x00027D6F
		public MetadataResolver(IAssemblyResolver assemblyResolver)
		{
			if (assemblyResolver == null)
			{
				throw new ArgumentNullException("assemblyResolver");
			}
			this.assembly_resolver = assemblyResolver;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00029B8C File Offset: 0x00027D8C
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
				if (type.Module.Assembly == null)
				{
					return null;
				}
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

		// Token: 0x06000C3C RID: 3132 RVA: 0x00029C6C File Offset: 0x00027E6C
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

		// Token: 0x06000C3D RID: 3133 RVA: 0x00029CE0 File Offset: 0x00027EE0
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

		// Token: 0x06000C3E RID: 3134 RVA: 0x00029D28 File Offset: 0x00027F28
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

		// Token: 0x06000C3F RID: 3135 RVA: 0x00029D60 File Offset: 0x00027F60
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

		// Token: 0x06000C40 RID: 3136 RVA: 0x00029DA0 File Offset: 0x00027FA0
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

		// Token: 0x06000C41 RID: 3137 RVA: 0x00029DF0 File Offset: 0x00027FF0
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

		// Token: 0x06000C42 RID: 3138 RVA: 0x00029E30 File Offset: 0x00028030
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

		// Token: 0x06000C43 RID: 3139 RVA: 0x00029E70 File Offset: 0x00028070
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

		// Token: 0x06000C44 RID: 3140 RVA: 0x00029F50 File Offset: 0x00028150
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

		// Token: 0x06000C45 RID: 3141 RVA: 0x00029FA4 File Offset: 0x000281A4
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

		// Token: 0x06000C46 RID: 3142 RVA: 0x0002A024 File Offset: 0x00028224
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

		// Token: 0x06000C47 RID: 3143 RVA: 0x0002A09D File Offset: 0x0002829D
		private static bool AreSame(ArrayType a, ArrayType b)
		{
			return a.Rank == b.Rank;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0002A0B0 File Offset: 0x000282B0
		private static bool AreSame(IModifierType a, IModifierType b)
		{
			return MetadataResolver.AreSame(a.ModifierType, b.ModifierType);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0002A0C4 File Offset: 0x000282C4
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

		// Token: 0x06000C4A RID: 3146 RVA: 0x0002A123 File Offset: 0x00028323
		private static bool AreSame(GenericParameter a, GenericParameter b)
		{
			return a.Position == b.Position;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0002A134 File Offset: 0x00028334
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

		// Token: 0x0400036E RID: 878
		private readonly IAssemblyResolver assembly_resolver;
	}
}
