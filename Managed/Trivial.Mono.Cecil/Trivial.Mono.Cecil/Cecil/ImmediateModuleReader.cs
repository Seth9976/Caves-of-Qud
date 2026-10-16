using System;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.PE;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000059 RID: 89
	internal sealed class ImmediateModuleReader : ModuleReader
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00006A94 File Offset: 0x00004C94
		public ImmediateModuleReader(Image image)
			: base(image, ReadingMode.Immediate)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00006A9E File Offset: 0x00004C9E
		protected override void ReadModule()
		{
			this.module.Read<ModuleDefinition, ModuleDefinition>(this.module, delegate(ModuleDefinition module, MetadataReader reader)
			{
				base.ReadModuleManifest(reader);
				this.ReadModule(module, true);
				return module;
			});
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public void ReadModule(ModuleDefinition module, bool resolve_attributes)
		{
			this.resolve_attributes = resolve_attributes;
			if (module.HasAssemblyReferences)
			{
				Mixin.Read(module.AssemblyReferences);
			}
			if (module.HasResources)
			{
				Mixin.Read(module.Resources);
			}
			if (module.HasModuleReferences)
			{
				Mixin.Read(module.ModuleReferences);
			}
			if (module.HasTypes)
			{
				this.ReadTypes(module.Types);
			}
			if (module.HasExportedTypes)
			{
				Mixin.Read(module.ExportedTypes);
			}
			this.ReadCustomAttributes(module);
			AssemblyDefinition assembly = module.Assembly;
			if (assembly != null)
			{
				this.ReadCustomAttributes(assembly);
				this.ReadSecurityDeclarations(assembly);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00006B54 File Offset: 0x00004D54
		private void ReadTypes(Collection<TypeDefinition> types)
		{
			for (int i = 0; i < types.Count; i++)
			{
				this.ReadType(types[i]);
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00006B80 File Offset: 0x00004D80
		private void ReadType(TypeDefinition type)
		{
			this.ReadGenericParameters(type);
			if (type.HasInterfaces)
			{
				this.ReadInterfaces(type);
			}
			if (type.HasNestedTypes)
			{
				this.ReadTypes(type.NestedTypes);
			}
			if (type.HasLayoutInfo)
			{
				Mixin.Read(type.ClassSize);
			}
			if (type.HasFields)
			{
				this.ReadFields(type);
			}
			if (type.HasMethods)
			{
				this.ReadMethods(type);
			}
			if (type.HasProperties)
			{
				this.ReadProperties(type);
			}
			if (type.HasEvents)
			{
				this.ReadEvents(type);
			}
			this.ReadSecurityDeclarations(type);
			this.ReadCustomAttributes(type);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00006C1C File Offset: 0x00004E1C
		private void ReadInterfaces(TypeDefinition type)
		{
			Collection<InterfaceImplementation> interfaces = type.Interfaces;
			for (int i = 0; i < interfaces.Count; i++)
			{
				this.ReadCustomAttributes(interfaces[i]);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00006C50 File Offset: 0x00004E50
		private void ReadGenericParameters(IGenericParameterProvider provider)
		{
			if (!provider.HasGenericParameters)
			{
				return;
			}
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				GenericParameter genericParameter = genericParameters[i];
				if (genericParameter.HasConstraints)
				{
					Mixin.Read(genericParameter.Constraints);
				}
				this.ReadCustomAttributes(genericParameter);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00006CA0 File Offset: 0x00004EA0
		private void ReadSecurityDeclarations(ISecurityDeclarationProvider provider)
		{
			if (!provider.HasSecurityDeclarations)
			{
				return;
			}
			Collection<SecurityDeclaration> securityDeclarations = provider.SecurityDeclarations;
			if (this.resolve_attributes)
			{
				for (int i = 0; i < securityDeclarations.Count; i++)
				{
					Mixin.Read(securityDeclarations[i].SecurityAttributes);
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00006CE8 File Offset: 0x00004EE8
		private void ReadCustomAttributes(ICustomAttributeProvider provider)
		{
			if (!provider.HasCustomAttributes)
			{
				return;
			}
			Collection<CustomAttribute> customAttributes = provider.CustomAttributes;
			if (this.resolve_attributes)
			{
				for (int i = 0; i < customAttributes.Count; i++)
				{
					Mixin.Read(customAttributes[i].ConstructorArguments);
				}
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00006D30 File Offset: 0x00004F30
		private void ReadFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (fieldDefinition.HasConstant)
				{
					Mixin.Read(fieldDefinition.Constant);
				}
				if (fieldDefinition.HasLayoutInfo)
				{
					Mixin.Read(fieldDefinition.Offset);
				}
				if (fieldDefinition.RVA > 0)
				{
					Mixin.Read(fieldDefinition.InitialValue);
				}
				if (fieldDefinition.HasMarshalInfo)
				{
					Mixin.Read(fieldDefinition.MarshalInfo);
				}
				this.ReadCustomAttributes(fieldDefinition);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00006DB8 File Offset: 0x00004FB8
		private void ReadMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				this.ReadGenericParameters(methodDefinition);
				if (methodDefinition.HasParameters)
				{
					this.ReadParameters(methodDefinition);
				}
				if (methodDefinition.HasOverrides)
				{
					Mixin.Read(methodDefinition.Overrides);
				}
				if (methodDefinition.IsPInvokeImpl)
				{
					Mixin.Read(methodDefinition.PInvokeInfo);
				}
				this.ReadSecurityDeclarations(methodDefinition);
				this.ReadCustomAttributes(methodDefinition);
				MethodReturnType methodReturnType = methodDefinition.MethodReturnType;
				if (methodReturnType.HasConstant)
				{
					Mixin.Read(methodReturnType.Constant);
				}
				if (methodReturnType.HasMarshalInfo)
				{
					Mixin.Read(methodReturnType.MarshalInfo);
				}
				this.ReadCustomAttributes(methodReturnType);
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00006E6C File Offset: 0x0000506C
		private void ReadParameters(MethodDefinition method)
		{
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameterDefinition = parameters[i];
				if (parameterDefinition.HasConstant)
				{
					Mixin.Read(parameterDefinition.Constant);
				}
				if (parameterDefinition.HasMarshalInfo)
				{
					Mixin.Read(parameterDefinition.MarshalInfo);
				}
				this.ReadCustomAttributes(parameterDefinition);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00006EC8 File Offset: 0x000050C8
		private void ReadProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDefinition propertyDefinition = properties[i];
				Mixin.Read(propertyDefinition.GetMethod);
				if (propertyDefinition.HasConstant)
				{
					Mixin.Read(propertyDefinition.Constant);
				}
				this.ReadCustomAttributes(propertyDefinition);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00006F1C File Offset: 0x0000511C
		private void ReadEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			for (int i = 0; i < events.Count; i++)
			{
				EventDefinition eventDefinition = events[i];
				Mixin.Read(eventDefinition.AddMethod);
				this.ReadCustomAttributes(eventDefinition);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00006F5B File Offset: 0x0000515B
		public override void ReadSymbols(ModuleDefinition module)
		{
			if (module.symbol_reader != null)
			{
				this.ReadTypesSymbols(module.Types, module.symbol_reader);
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00006F78 File Offset: 0x00005178
		private void ReadTypesSymbols(Collection<TypeDefinition> types, ISymbolReader symbol_reader)
		{
			for (int i = 0; i < types.Count; i++)
			{
				TypeDefinition typeDefinition = types[i];
				if (typeDefinition.HasNestedTypes)
				{
					this.ReadTypesSymbols(typeDefinition.NestedTypes, symbol_reader);
				}
				if (typeDefinition.HasMethods)
				{
					this.ReadMethodsSymbols(typeDefinition, symbol_reader);
				}
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00006FC4 File Offset: 0x000051C4
		private void ReadMethodsSymbols(TypeDefinition type, ISymbolReader symbol_reader)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (methodDefinition.HasBody && methodDefinition.token.RID != 0U && methodDefinition.debug_info == null)
				{
					methodDefinition.debug_info = symbol_reader.Read(methodDefinition);
				}
			}
		}

		// Token: 0x040000A0 RID: 160
		private bool resolve_attributes;
	}
}
