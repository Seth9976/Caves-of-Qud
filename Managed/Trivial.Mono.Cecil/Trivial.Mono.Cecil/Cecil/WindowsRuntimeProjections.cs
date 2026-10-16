using System;
using System.Collections.Generic;
using System.Threading;
using Trivial.Mono.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x020000CC RID: 204
	internal sealed class WindowsRuntimeProjections
	{
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0001A6CC File Offset: 0x000188CC
		private static Dictionary<string, WindowsRuntimeProjections.ProjectionInfo> Projections
		{
			get
			{
				if (WindowsRuntimeProjections.projections != null)
				{
					return WindowsRuntimeProjections.projections;
				}
				Dictionary<string, WindowsRuntimeProjections.ProjectionInfo> dictionary = new Dictionary<string, WindowsRuntimeProjections.ProjectionInfo>();
				dictionary.Add("AttributeTargets", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Metadata", "System", "AttributeTargets", "System.Runtime", false));
				dictionary.Add("AttributeUsageAttribute", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Metadata", "System", "AttributeUsageAttribute", "System.Runtime", true));
				dictionary.Add("Color", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI", "Windows.UI", "Color", "System.Runtime.WindowsRuntime", false));
				dictionary.Add("CornerRadius", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "CornerRadius", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("DateTime", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "DateTimeOffset", "System.Runtime", false));
				dictionary.Add("Duration", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "Duration", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("DurationType", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "DurationType", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("EventHandler`1", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "EventHandler`1", "System.Runtime", false));
				dictionary.Add("EventRegistrationToken", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System.Runtime.InteropServices.WindowsRuntime", "EventRegistrationToken", "System.Runtime.InteropServices.WindowsRuntime", false));
				dictionary.Add("GeneratorPosition", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Controls.Primitives", "Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("GridLength", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "GridLength", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("GridUnitType", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "GridUnitType", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("HResult", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "Exception", "System.Runtime", false));
				dictionary.Add("IBindableIterable", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections", "IEnumerable", "System.Runtime", false));
				dictionary.Add("IBindableVector", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections", "IList", "System.Runtime", false));
				dictionary.Add("IClosable", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "IDisposable", "System.Runtime", false));
				dictionary.Add("ICommand", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Input", "System.Windows.Input", "ICommand", "System.ObjectModel", false));
				dictionary.Add("IIterable`1", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IEnumerable`1", "System.Runtime", false));
				dictionary.Add("IKeyValuePair`2", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "KeyValuePair`2", "System.Runtime", false));
				dictionary.Add("IMapView`2", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IReadOnlyDictionary`2", "System.Runtime", false));
				dictionary.Add("IMap`2", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IDictionary`2", "System.Runtime", false));
				dictionary.Add("INotifyCollectionChanged", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "INotifyCollectionChanged", "System.ObjectModel", false));
				dictionary.Add("INotifyPropertyChanged", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "INotifyPropertyChanged", "System.ObjectModel", false));
				dictionary.Add("IReference`1", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "Nullable`1", "System.Runtime", false));
				dictionary.Add("IVectorView`1", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IReadOnlyList`1", "System.Runtime", false));
				dictionary.Add("IVector`1", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IList`1", "System.Runtime", false));
				dictionary.Add("KeyTime", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "KeyTime", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("Matrix", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Media", "Windows.UI.Xaml.Media", "Matrix", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("Matrix3D", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Media.Media3D", "Windows.UI.Xaml.Media.Media3D", "Matrix3D", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("Matrix3x2", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Matrix3x2", "System.Numerics.Vectors", false));
				dictionary.Add("Matrix4x4", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Matrix4x4", "System.Numerics.Vectors", false));
				dictionary.Add("NotifyCollectionChangedAction", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedAction", "System.ObjectModel", false));
				dictionary.Add("NotifyCollectionChangedEventArgs", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedEventArgs", "System.ObjectModel", false));
				dictionary.Add("NotifyCollectionChangedEventHandler", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedEventHandler", "System.ObjectModel", false));
				dictionary.Add("Plane", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Plane", "System.Numerics.Vectors", false));
				dictionary.Add("Point", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Point", "System.Runtime.WindowsRuntime", false));
				dictionary.Add("PropertyChangedEventArgs", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "PropertyChangedEventArgs", "System.ObjectModel", false));
				dictionary.Add("PropertyChangedEventHandler", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "PropertyChangedEventHandler", "System.ObjectModel", false));
				dictionary.Add("Quaternion", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Quaternion", "System.Numerics.Vectors", false));
				dictionary.Add("Rect", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Rect", "System.Runtime.WindowsRuntime", false));
				dictionary.Add("RepeatBehavior", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "RepeatBehavior", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("RepeatBehaviorType", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("Size", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Size", "System.Runtime.WindowsRuntime", false));
				dictionary.Add("Thickness", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "Thickness", "System.Runtime.WindowsRuntime.UI.Xaml", false));
				dictionary.Add("TimeSpan", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "TimeSpan", "System.Runtime", false));
				dictionary.Add("TypeName", new WindowsRuntimeProjections.ProjectionInfo("Windows.UI.Xaml.Interop", "System", "Type", "System.Runtime", false));
				dictionary.Add("Uri", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation", "System", "Uri", "System.Runtime", false));
				dictionary.Add("Vector2", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector2", "System.Numerics.Vectors", false));
				dictionary.Add("Vector3", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector3", "System.Numerics.Vectors", false));
				dictionary.Add("Vector4", new WindowsRuntimeProjections.ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector4", "System.Numerics.Vectors", false));
				Dictionary<string, WindowsRuntimeProjections.ProjectionInfo> dictionary2 = dictionary;
				WindowsRuntimeProjections.projections = dictionary2;
				return dictionary2;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001AE2D File Offset: 0x0001902D
		private AssemblyNameReference[] VirtualReferences
		{
			get
			{
				if (this.virtual_references == null)
				{
					Mixin.Read(this.module.AssemblyReferences);
				}
				return this.virtual_references;
			}
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x0001AE4D File Offset: 0x0001904D
		public WindowsRuntimeProjections(ModuleDefinition module)
		{
			this.module = module;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x0001AE7C File Offset: 0x0001907C
		public static void Project(TypeDefinition type)
		{
			TypeDefinitionTreatment typeDefinitionTreatment = TypeDefinitionTreatment.None;
			MetadataKind metadataKind = type.Module.MetadataKind;
			Collection<MethodDefinition> collection = null;
			Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> collection2 = null;
			if (type.IsWindowsRuntime)
			{
				if (metadataKind != MetadataKind.WindowsMetadata)
				{
					if (metadataKind == MetadataKind.ManagedWindowsMetadata)
					{
						if (WindowsRuntimeProjections.NeedsWindowsRuntimePrefix(type))
						{
							typeDefinitionTreatment = TypeDefinitionTreatment.PrefixWindowsRuntimeName;
						}
					}
				}
				else
				{
					typeDefinitionTreatment = WindowsRuntimeProjections.GetWellKnownTypeDefinitionTreatment(type);
					if (typeDefinitionTreatment != TypeDefinitionTreatment.None)
					{
						WindowsRuntimeProjections.ApplyProjection(type, new TypeDefinitionProjection(type, typeDefinitionTreatment, collection, collection2));
						return;
					}
					TypeReference baseType = type.BaseType;
					typeDefinitionTreatment = ((baseType == null || !WindowsRuntimeProjections.IsAttribute(baseType)) ? WindowsRuntimeProjections.GenerateRedirectionInformation(type, out collection, out collection2) : TypeDefinitionTreatment.NormalAttribute);
				}
				if ((typeDefinitionTreatment == TypeDefinitionTreatment.PrefixWindowsRuntimeName || typeDefinitionTreatment == TypeDefinitionTreatment.NormalType) && !type.IsInterface && WindowsRuntimeProjections.HasAttribute(type, "Windows.UI.Xaml", "TreatAsAbstractComposableClassAttribute"))
				{
					typeDefinitionTreatment |= TypeDefinitionTreatment.Abstract;
				}
			}
			else if (metadataKind == MetadataKind.ManagedWindowsMetadata && WindowsRuntimeProjections.IsClrImplementationType(type))
			{
				typeDefinitionTreatment = TypeDefinitionTreatment.UnmangleWindowsRuntimeName;
			}
			if (typeDefinitionTreatment != TypeDefinitionTreatment.None)
			{
				WindowsRuntimeProjections.ApplyProjection(type, new TypeDefinitionProjection(type, typeDefinitionTreatment, collection, collection2));
			}
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001AF40 File Offset: 0x00019140
		private static TypeDefinitionTreatment GetWellKnownTypeDefinitionTreatment(TypeDefinition type)
		{
			WindowsRuntimeProjections.ProjectionInfo projectionInfo;
			if (!WindowsRuntimeProjections.Projections.TryGetValue(type.Name, ref projectionInfo))
			{
				return TypeDefinitionTreatment.None;
			}
			TypeDefinitionTreatment typeDefinitionTreatment = (projectionInfo.Attribute ? TypeDefinitionTreatment.RedirectToClrAttribute : TypeDefinitionTreatment.RedirectToClrType);
			if (type.Namespace == projectionInfo.ClrNamespace)
			{
				return typeDefinitionTreatment;
			}
			if (type.Namespace == projectionInfo.WinRTNamespace)
			{
				return typeDefinitionTreatment | TypeDefinitionTreatment.Internal;
			}
			return TypeDefinitionTreatment.None;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001AFA0 File Offset: 0x000191A0
		private static TypeDefinitionTreatment GenerateRedirectionInformation(TypeDefinition type, out Collection<MethodDefinition> redirectedMethods, out Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces)
		{
			bool flag = false;
			redirectedMethods = null;
			redirectedInterfaces = null;
			foreach (InterfaceImplementation interfaceImplementation in type.Interfaces)
			{
				if (WindowsRuntimeProjections.IsRedirectedType(interfaceImplementation.InterfaceType))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return TypeDefinitionTreatment.NormalType;
			}
			HashSet<TypeReference> hashSet = new HashSet<TypeReference>(new TypeReferenceEqualityComparer());
			redirectedMethods = new Collection<MethodDefinition>();
			redirectedInterfaces = new Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>>();
			foreach (InterfaceImplementation interfaceImplementation2 in type.Interfaces)
			{
				TypeReference interfaceType = interfaceImplementation2.InterfaceType;
				if (WindowsRuntimeProjections.IsRedirectedType(interfaceType))
				{
					hashSet.Add(interfaceType);
					WindowsRuntimeProjections.CollectImplementedInterfaces(interfaceType, hashSet);
				}
			}
			foreach (InterfaceImplementation interfaceImplementation3 in type.Interfaces)
			{
				TypeReference interfaceType2 = interfaceImplementation3.InterfaceType;
				if (WindowsRuntimeProjections.IsRedirectedType(interfaceImplementation3.InterfaceType))
				{
					TypeReference elementType = interfaceType2.GetElementType();
					TypeReference typeReference = new TypeReference(elementType.Namespace, elementType.Name, elementType.Module, elementType.Scope)
					{
						DeclaringType = elementType.DeclaringType,
						projection = elementType.projection
					};
					WindowsRuntimeProjections.RemoveProjection(typeReference);
					GenericInstanceType genericInstanceType = interfaceType2 as GenericInstanceType;
					if (genericInstanceType != null)
					{
						GenericInstanceType genericInstanceType2 = new GenericInstanceType(typeReference);
						foreach (TypeReference typeReference2 in genericInstanceType.GenericArguments)
						{
							genericInstanceType2.GenericArguments.Add(typeReference2);
						}
						typeReference = genericInstanceType2;
					}
					InterfaceImplementation interfaceImplementation4 = new InterfaceImplementation(typeReference);
					redirectedInterfaces.Add(new KeyValuePair<InterfaceImplementation, InterfaceImplementation>(interfaceImplementation3, interfaceImplementation4));
				}
			}
			if (!type.IsInterface)
			{
				foreach (TypeReference typeReference3 in hashSet)
				{
					WindowsRuntimeProjections.RedirectInterfaceMethods(typeReference3, redirectedMethods);
				}
			}
			return TypeDefinitionTreatment.RedirectImplementedMethods;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001B1F8 File Offset: 0x000193F8
		private static void CollectImplementedInterfaces(TypeReference type, HashSet<TypeReference> results)
		{
			TypeResolver typeResolver = TypeResolver.For(type);
			foreach (InterfaceImplementation interfaceImplementation in type.Resolve().Interfaces)
			{
				TypeReference typeReference = typeResolver.Resolve(interfaceImplementation.InterfaceType);
				results.Add(typeReference);
				WindowsRuntimeProjections.CollectImplementedInterfaces(typeReference, results);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001B26C File Offset: 0x0001946C
		private static void RedirectInterfaceMethods(TypeReference interfaceType, Collection<MethodDefinition> redirectedMethods)
		{
			TypeResolver typeResolver = TypeResolver.For(interfaceType);
			foreach (MethodDefinition methodDefinition in interfaceType.Resolve().Methods)
			{
				MethodDefinition methodDefinition2 = new MethodDefinition(methodDefinition.Name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.VtableLayoutMask, typeResolver.Resolve(methodDefinition.ReturnType));
				methodDefinition2.ImplAttributes = MethodImplAttributes.CodeTypeMask;
				foreach (ParameterDefinition parameterDefinition in methodDefinition.Parameters)
				{
					methodDefinition2.Parameters.Add(new ParameterDefinition(parameterDefinition.Name, parameterDefinition.Attributes, typeResolver.Resolve(parameterDefinition.ParameterType)));
				}
				methodDefinition2.Overrides.Add(typeResolver.Resolve(methodDefinition));
				redirectedMethods.Add(methodDefinition2);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001B370 File Offset: 0x00019570
		private static bool IsRedirectedType(TypeReference type)
		{
			TypeReferenceProjection typeReferenceProjection = type.GetElementType().projection as TypeReferenceProjection;
			return typeReferenceProjection != null && typeReferenceProjection.Treatment == TypeReferenceTreatment.UseProjectionInfo;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001B39C File Offset: 0x0001959C
		private static bool NeedsWindowsRuntimePrefix(TypeDefinition type)
		{
			if ((type.Attributes & (TypeAttributes.VisibilityMask | TypeAttributes.ClassSemanticMask)) != TypeAttributes.Public)
			{
				return false;
			}
			TypeReference baseType = type.BaseType;
			if (baseType == null || baseType.MetadataToken.TokenType != TokenType.TypeRef)
			{
				return false;
			}
			if (baseType.Namespace == "System")
			{
				string name = baseType.Name;
				if (name == "Attribute" || name == "MulticastDelegate" || name == "ValueType")
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0001B41B File Offset: 0x0001961B
		public static bool IsClrImplementationType(TypeDefinition type)
		{
			return (type.Attributes & (TypeAttributes.VisibilityMask | TypeAttributes.SpecialName)) == TypeAttributes.SpecialName && type.Name.StartsWith("<CLR>");
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x0001B444 File Offset: 0x00019644
		public static void ApplyProjection(TypeDefinition type, TypeDefinitionProjection projection)
		{
			if (projection != null)
			{
				TypeDefinitionTreatment treatment = projection.Treatment;
				switch (treatment & TypeDefinitionTreatment.KindMask)
				{
				case TypeDefinitionTreatment.NormalType:
					type.Attributes |= TypeAttributes.Import | TypeAttributes.WindowsRuntime;
					break;
				case TypeDefinitionTreatment.NormalAttribute:
					type.Attributes |= TypeAttributes.Sealed | TypeAttributes.WindowsRuntime;
					break;
				case TypeDefinitionTreatment.UnmangleWindowsRuntimeName:
					type.Attributes = (type.Attributes & ~TypeAttributes.SpecialName) | TypeAttributes.Public;
					type.Name = type.Name.Substring("<CLR>".Length);
					break;
				case TypeDefinitionTreatment.PrefixWindowsRuntimeName:
					type.Attributes = (type.Attributes & ~TypeAttributes.Public) | TypeAttributes.Import;
					type.Name = "<WinRT>" + type.Name;
					break;
				case TypeDefinitionTreatment.RedirectToClrType:
					type.Attributes = (type.Attributes & ~TypeAttributes.Public) | TypeAttributes.Import;
					break;
				case TypeDefinitionTreatment.RedirectToClrAttribute:
					type.Attributes &= ~TypeAttributes.Public;
					break;
				case TypeDefinitionTreatment.RedirectImplementedMethods:
					type.Attributes |= TypeAttributes.Import | TypeAttributes.WindowsRuntime;
					foreach (KeyValuePair<InterfaceImplementation, InterfaceImplementation> keyValuePair in projection.RedirectedInterfaces)
					{
						type.Interfaces.Add(keyValuePair.Value);
						foreach (CustomAttribute customAttribute in keyValuePair.Key.CustomAttributes)
						{
							keyValuePair.Value.CustomAttributes.Add(customAttribute);
						}
						keyValuePair.Key.CustomAttributes.Clear();
						foreach (MethodDefinition methodDefinition in type.Methods)
						{
							foreach (MethodReference methodReference in methodDefinition.Overrides)
							{
								if (TypeReferenceEqualityComparer.AreEqual(methodReference.DeclaringType, keyValuePair.Key.InterfaceType))
								{
									methodReference.DeclaringType = keyValuePair.Value.InterfaceType;
								}
							}
						}
					}
					foreach (MethodDefinition methodDefinition2 in projection.RedirectedMethods)
					{
						type.Methods.Add(methodDefinition2);
					}
					break;
				}
				if ((treatment & TypeDefinitionTreatment.Abstract) != TypeDefinitionTreatment.None)
				{
					type.Attributes |= TypeAttributes.Abstract;
				}
				if ((treatment & TypeDefinitionTreatment.Internal) != TypeDefinitionTreatment.None)
				{
					type.Attributes &= ~TypeAttributes.Public;
				}
				type.WindowsRuntimeProjection = projection;
			}
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x0001B780 File Offset: 0x00019980
		public static TypeDefinitionProjection RemoveProjection(TypeDefinition type)
		{
			if (!type.IsWindowsRuntimeProjection)
			{
				return null;
			}
			TypeDefinitionProjection windowsRuntimeProjection = type.WindowsRuntimeProjection;
			type.WindowsRuntimeProjection = null;
			type.Attributes = windowsRuntimeProjection.Attributes;
			type.Name = windowsRuntimeProjection.Name;
			if (windowsRuntimeProjection.Treatment == TypeDefinitionTreatment.RedirectImplementedMethods)
			{
				foreach (MethodDefinition methodDefinition in windowsRuntimeProjection.RedirectedMethods)
				{
					type.Methods.Remove(methodDefinition);
				}
				foreach (KeyValuePair<InterfaceImplementation, InterfaceImplementation> keyValuePair in windowsRuntimeProjection.RedirectedInterfaces)
				{
					foreach (MethodDefinition methodDefinition2 in type.Methods)
					{
						foreach (MethodReference methodReference in methodDefinition2.Overrides)
						{
							if (TypeReferenceEqualityComparer.AreEqual(methodReference.DeclaringType, keyValuePair.Value.InterfaceType))
							{
								methodReference.DeclaringType = keyValuePair.Key.InterfaceType;
							}
						}
					}
					foreach (CustomAttribute customAttribute in keyValuePair.Value.CustomAttributes)
					{
						keyValuePair.Key.CustomAttributes.Add(customAttribute);
					}
					keyValuePair.Value.CustomAttributes.Clear();
					type.Interfaces.Remove(keyValuePair.Value);
				}
				return windowsRuntimeProjection;
			}
			return windowsRuntimeProjection;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public static void Project(TypeReference type)
		{
			WindowsRuntimeProjections.ProjectionInfo projectionInfo;
			TypeReferenceTreatment typeReferenceTreatment = ((!WindowsRuntimeProjections.Projections.TryGetValue(type.Name, ref projectionInfo) || !(projectionInfo.WinRTNamespace == type.Namespace)) ? WindowsRuntimeProjections.GetSpecialTypeReferenceTreatment(type) : TypeReferenceTreatment.UseProjectionInfo);
			if (typeReferenceTreatment != TypeReferenceTreatment.None)
			{
				WindowsRuntimeProjections.ApplyProjection(type, new TypeReferenceProjection(type, typeReferenceTreatment));
			}
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x0001BA06 File Offset: 0x00019C06
		private static TypeReferenceTreatment GetSpecialTypeReferenceTreatment(TypeReference type)
		{
			if (type.Namespace == "System")
			{
				if (type.Name == "MulticastDelegate")
				{
					return TypeReferenceTreatment.SystemDelegate;
				}
				if (type.Name == "Attribute")
				{
					return TypeReferenceTreatment.SystemAttribute;
				}
			}
			return TypeReferenceTreatment.None;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x0001BA44 File Offset: 0x00019C44
		private static bool IsAttribute(TypeReference type)
		{
			return type.MetadataToken.TokenType == TokenType.TypeRef && type.Name == "Attribute" && type.Namespace == "System";
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x0001BA8C File Offset: 0x00019C8C
		private static bool IsEnum(TypeReference type)
		{
			return type.MetadataToken.TokenType == TokenType.TypeRef && type.Name == "Enum" && type.Namespace == "System";
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x0001BAD4 File Offset: 0x00019CD4
		public static void ApplyProjection(TypeReference type, TypeReferenceProjection projection)
		{
			if (projection != null)
			{
				TypeReferenceTreatment treatment = projection.Treatment;
				if (treatment - TypeReferenceTreatment.SystemDelegate > 1)
				{
					if (treatment == TypeReferenceTreatment.UseProjectionInfo)
					{
						WindowsRuntimeProjections.ProjectionInfo projectionInfo = WindowsRuntimeProjections.Projections[type.Name];
						type.Name = projectionInfo.ClrName;
						type.Namespace = projectionInfo.ClrNamespace;
						type.Scope = type.Module.Projections.GetAssemblyReference(projectionInfo.ClrAssembly);
					}
				}
				else
				{
					type.Scope = type.Module.Projections.GetAssemblyReference("System.Runtime");
				}
				type.WindowsRuntimeProjection = projection;
			}
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001BB60 File Offset: 0x00019D60
		public static TypeReferenceProjection RemoveProjection(TypeReference type)
		{
			if (!type.IsWindowsRuntimeProjection)
			{
				return null;
			}
			TypeReferenceProjection windowsRuntimeProjection = type.WindowsRuntimeProjection;
			type.WindowsRuntimeProjection = null;
			type.Name = windowsRuntimeProjection.Name;
			type.Namespace = windowsRuntimeProjection.Namespace;
			type.Scope = windowsRuntimeProjection.Scope;
			return windowsRuntimeProjection;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x0001BBAC File Offset: 0x00019DAC
		public static void Project(MethodDefinition method)
		{
			MethodDefinitionTreatment methodDefinitionTreatment = MethodDefinitionTreatment.None;
			bool flag = false;
			TypeDefinition declaringType = method.DeclaringType;
			if (declaringType.IsWindowsRuntime)
			{
				if (WindowsRuntimeProjections.IsClrImplementationType(declaringType))
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else if (declaringType.IsNested)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else if (declaringType.IsInterface)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
				}
				else if (declaringType.Module.MetadataKind == MetadataKind.ManagedWindowsMetadata && !method.IsPublic)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else
				{
					flag = true;
					TypeReference baseType = declaringType.BaseType;
					if (baseType != null && baseType.MetadataToken.TokenType == TokenType.TypeRef)
					{
						TypeReferenceTreatment specialTypeReferenceTreatment = WindowsRuntimeProjections.GetSpecialTypeReferenceTreatment(baseType);
						if (specialTypeReferenceTreatment != TypeReferenceTreatment.SystemDelegate)
						{
							if (specialTypeReferenceTreatment == TypeReferenceTreatment.SystemAttribute)
							{
								methodDefinitionTreatment = MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
								flag = false;
							}
						}
						else
						{
							methodDefinitionTreatment = MethodDefinitionTreatment.Public | MethodDefinitionTreatment.Runtime;
							flag = false;
						}
					}
				}
			}
			if (flag)
			{
				bool flag2 = false;
				bool flag3 = false;
				foreach (MethodReference methodReference in method.Overrides)
				{
					if (methodReference.MetadataToken.TokenType == TokenType.MemberRef && WindowsRuntimeProjections.ImplementsRedirectedInterface(methodReference))
					{
						flag2 = true;
					}
					else
					{
						flag3 = true;
					}
				}
				if (flag2 && !flag3)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.Private | MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
					flag = false;
				}
			}
			if (flag)
			{
				methodDefinitionTreatment |= WindowsRuntimeProjections.GetMethodDefinitionTreatmentFromCustomAttributes(method);
			}
			if (methodDefinitionTreatment != MethodDefinitionTreatment.None)
			{
				WindowsRuntimeProjections.ApplyProjection(method, new MethodDefinitionProjection(method, methodDefinitionTreatment));
			}
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x0001BCEC File Offset: 0x00019EEC
		private static MethodDefinitionTreatment GetMethodDefinitionTreatmentFromCustomAttributes(MethodDefinition method)
		{
			MethodDefinitionTreatment methodDefinitionTreatment = MethodDefinitionTreatment.None;
			foreach (CustomAttribute customAttribute in method.CustomAttributes)
			{
				TypeReference attributeType = customAttribute.AttributeType;
				if (!(attributeType.Namespace != "Windows.UI.Xaml"))
				{
					if (attributeType.Name == "TreatAsPublicMethodAttribute")
					{
						methodDefinitionTreatment |= MethodDefinitionTreatment.Public;
					}
					else if (attributeType.Name == "TreatAsAbstractMethodAttribute")
					{
						methodDefinitionTreatment |= MethodDefinitionTreatment.Abstract;
					}
				}
			}
			return methodDefinitionTreatment;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x0001BD84 File Offset: 0x00019F84
		public static void ApplyProjection(MethodDefinition method, MethodDefinitionProjection projection)
		{
			if (projection != null)
			{
				MethodDefinitionTreatment treatment = projection.Treatment;
				if ((treatment & MethodDefinitionTreatment.Abstract) != MethodDefinitionTreatment.None)
				{
					method.Attributes |= MethodAttributes.Abstract;
				}
				if ((treatment & MethodDefinitionTreatment.Private) != MethodDefinitionTreatment.None)
				{
					method.Attributes = (method.Attributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Private;
				}
				if ((treatment & MethodDefinitionTreatment.Public) != MethodDefinitionTreatment.None)
				{
					method.Attributes = (method.Attributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Public;
				}
				if ((treatment & MethodDefinitionTreatment.Runtime) != MethodDefinitionTreatment.None)
				{
					method.ImplAttributes |= MethodImplAttributes.CodeTypeMask;
				}
				if ((treatment & MethodDefinitionTreatment.InternalCall) != MethodDefinitionTreatment.None)
				{
					method.ImplAttributes |= MethodImplAttributes.InternalCall;
				}
				method.WindowsRuntimeProjection = projection;
			}
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x0001BE1C File Offset: 0x0001A01C
		public static MethodDefinitionProjection RemoveProjection(MethodDefinition method)
		{
			if (!method.IsWindowsRuntimeProjection)
			{
				return null;
			}
			MethodDefinitionProjection windowsRuntimeProjection = method.WindowsRuntimeProjection;
			method.WindowsRuntimeProjection = null;
			method.Attributes = windowsRuntimeProjection.Attributes;
			method.ImplAttributes = windowsRuntimeProjection.ImplAttributes;
			method.Name = windowsRuntimeProjection.Name;
			return windowsRuntimeProjection;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x0001BE68 File Offset: 0x0001A068
		public static void Project(FieldDefinition field)
		{
			FieldDefinitionTreatment fieldDefinitionTreatment = FieldDefinitionTreatment.None;
			TypeDefinition declaringType = field.DeclaringType;
			if (declaringType.Module.MetadataKind == MetadataKind.WindowsMetadata && field.IsRuntimeSpecialName && field.Name == "value__")
			{
				TypeReference baseType = declaringType.BaseType;
				if (baseType != null && WindowsRuntimeProjections.IsEnum(baseType))
				{
					fieldDefinitionTreatment = FieldDefinitionTreatment.Public;
				}
			}
			if (fieldDefinitionTreatment != FieldDefinitionTreatment.None)
			{
				WindowsRuntimeProjections.ApplyProjection(field, new FieldDefinitionProjection(field, fieldDefinitionTreatment));
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0001BECA File Offset: 0x0001A0CA
		public static void ApplyProjection(FieldDefinition field, FieldDefinitionProjection projection)
		{
			if (projection != null)
			{
				if (projection.Treatment == FieldDefinitionTreatment.Public)
				{
					field.Attributes = (field.Attributes & ~FieldAttributes.FieldAccessMask) | FieldAttributes.Public;
				}
				field.WindowsRuntimeProjection = projection;
			}
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001BEF4 File Offset: 0x0001A0F4
		public static FieldDefinitionProjection RemoveProjection(FieldDefinition field)
		{
			if (!field.IsWindowsRuntimeProjection)
			{
				return null;
			}
			FieldDefinitionProjection windowsRuntimeProjection = field.WindowsRuntimeProjection;
			field.WindowsRuntimeProjection = null;
			field.Attributes = windowsRuntimeProjection.Attributes;
			return windowsRuntimeProjection;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x0001BF28 File Offset: 0x0001A128
		private static bool ImplementsRedirectedInterface(MemberReference member)
		{
			TypeReference declaringType = member.DeclaringType;
			TokenType tokenType = declaringType.MetadataToken.TokenType;
			TypeReference typeReference;
			if (tokenType != TokenType.TypeRef)
			{
				if (tokenType != TokenType.TypeSpec)
				{
					return false;
				}
				if (!declaringType.IsGenericInstance)
				{
					return false;
				}
				typeReference = ((TypeSpecification)declaringType).ElementType;
				if (typeReference.MetadataType != MetadataType.Class || typeReference.MetadataToken.TokenType != TokenType.TypeRef)
				{
					return false;
				}
			}
			else
			{
				typeReference = declaringType;
			}
			TypeReferenceProjection typeReferenceProjection = WindowsRuntimeProjections.RemoveProjection(typeReference);
			bool flag = false;
			WindowsRuntimeProjections.ProjectionInfo projectionInfo;
			if (WindowsRuntimeProjections.Projections.TryGetValue(typeReference.Name, ref projectionInfo) && typeReference.Namespace == projectionInfo.WinRTNamespace)
			{
				flag = true;
			}
			WindowsRuntimeProjections.ApplyProjection(typeReference, typeReferenceProjection);
			return flag;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x0001BFDC File Offset: 0x0001A1DC
		public void AddVirtualReferences(Collection<AssemblyNameReference> references)
		{
			AssemblyNameReference coreLibrary = WindowsRuntimeProjections.GetCoreLibrary(references);
			this.corlib_version = coreLibrary.Version;
			coreLibrary.Version = WindowsRuntimeProjections.version;
			if (this.virtual_references == null)
			{
				AssemblyNameReference[] assemblyReferences = WindowsRuntimeProjections.GetAssemblyReferences(coreLibrary);
				Interlocked.CompareExchange<AssemblyNameReference[]>(ref this.virtual_references, assemblyReferences, null);
			}
			AssemblyNameReference[] array = this.virtual_references;
			foreach (AssemblyNameReference assemblyNameReference in array)
			{
				references.Add(assemblyNameReference);
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x0001C050 File Offset: 0x0001A250
		public void RemoveVirtualReferences(Collection<AssemblyNameReference> references)
		{
			WindowsRuntimeProjections.GetCoreLibrary(references).Version = this.corlib_version;
			AssemblyNameReference[] virtualReferences = this.VirtualReferences;
			foreach (AssemblyNameReference assemblyNameReference in virtualReferences)
			{
				references.Remove(assemblyNameReference);
			}
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x0001C094 File Offset: 0x0001A294
		private static AssemblyNameReference[] GetAssemblyReferences(AssemblyNameReference corlib)
		{
			AssemblyNameReference assemblyNameReference = new AssemblyNameReference("System.Runtime", WindowsRuntimeProjections.version);
			AssemblyNameReference assemblyNameReference2 = new AssemblyNameReference("System.Runtime.InteropServices.WindowsRuntime", WindowsRuntimeProjections.version);
			AssemblyNameReference assemblyNameReference3 = new AssemblyNameReference("System.ObjectModel", WindowsRuntimeProjections.version);
			AssemblyNameReference assemblyNameReference4 = new AssemblyNameReference("System.Runtime.WindowsRuntime", WindowsRuntimeProjections.version);
			AssemblyNameReference assemblyNameReference5 = new AssemblyNameReference("System.Runtime.WindowsRuntime.UI.Xaml", WindowsRuntimeProjections.version);
			AssemblyNameReference assemblyNameReference6 = new AssemblyNameReference("System.Numerics.Vectors", WindowsRuntimeProjections.version);
			if (corlib.HasPublicKey)
			{
				byte[] array = (assemblyNameReference4.PublicKey = (assemblyNameReference5.PublicKey = corlib.PublicKey));
				AssemblyNameReference assemblyNameReference7 = assemblyNameReference;
				AssemblyNameReference assemblyNameReference8 = assemblyNameReference2;
				AssemblyNameReference assemblyNameReference9 = assemblyNameReference3;
				byte[] array2 = (assemblyNameReference6.PublicKey = WindowsRuntimeProjections.contract_pk);
				byte[] array3 = (assemblyNameReference9.PublicKey = array2);
				array = (assemblyNameReference7.PublicKey = (assemblyNameReference8.PublicKey = array3));
			}
			else
			{
				byte[] array = (assemblyNameReference4.PublicKeyToken = (assemblyNameReference5.PublicKeyToken = corlib.PublicKeyToken));
				AssemblyNameReference assemblyNameReference10 = assemblyNameReference;
				AssemblyNameReference assemblyNameReference11 = assemblyNameReference2;
				AssemblyNameReference assemblyNameReference12 = assemblyNameReference3;
				byte[] array4 = (assemblyNameReference6.PublicKeyToken = WindowsRuntimeProjections.contract_pk_token);
				byte[] array5 = (assemblyNameReference12.PublicKeyToken = array4);
				array = (assemblyNameReference10.PublicKeyToken = (assemblyNameReference11.PublicKeyToken = array5));
			}
			return new AssemblyNameReference[] { assemblyNameReference, assemblyNameReference2, assemblyNameReference3, assemblyNameReference4, assemblyNameReference5, assemblyNameReference6 };
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		private static AssemblyNameReference GetCoreLibrary(Collection<AssemblyNameReference> references)
		{
			foreach (AssemblyNameReference assemblyNameReference in references)
			{
				if (assemblyNameReference.Name == "mscorlib")
				{
					return assemblyNameReference;
				}
			}
			throw new BadImageFormatException("Missing mscorlib reference in AssemblyRef table.");
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0001C268 File Offset: 0x0001A468
		private AssemblyNameReference GetAssemblyReference(string name)
		{
			AssemblyNameReference[] virtualReferences = this.VirtualReferences;
			foreach (AssemblyNameReference assemblyNameReference in virtualReferences)
			{
				if (assemblyNameReference.Name == name)
				{
					return assemblyNameReference;
				}
			}
			throw new Exception();
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0001C2A8 File Offset: 0x0001A4A8
		public static void Project(ICustomAttributeProvider owner, CustomAttribute attribute)
		{
			if (!WindowsRuntimeProjections.IsWindowsAttributeUsageAttribute(owner, attribute))
			{
				return;
			}
			CustomAttributeValueTreatment customAttributeValueTreatment = CustomAttributeValueTreatment.None;
			TypeDefinition typeDefinition = (TypeDefinition)owner;
			if (typeDefinition.Namespace == "Windows.Foundation.Metadata")
			{
				if (typeDefinition.Name == "VersionAttribute")
				{
					customAttributeValueTreatment = CustomAttributeValueTreatment.VersionAttribute;
				}
				else if (typeDefinition.Name == "DeprecatedAttribute")
				{
					customAttributeValueTreatment = CustomAttributeValueTreatment.DeprecatedAttribute;
				}
			}
			if (customAttributeValueTreatment == CustomAttributeValueTreatment.None)
			{
				customAttributeValueTreatment = ((!WindowsRuntimeProjections.HasAttribute(typeDefinition, "Windows.Foundation.Metadata", "AllowMultipleAttribute")) ? CustomAttributeValueTreatment.AllowSingle : CustomAttributeValueTreatment.AllowMultiple);
			}
			if (customAttributeValueTreatment != CustomAttributeValueTreatment.None)
			{
				AttributeTargets attributeTargets = (AttributeTargets)attribute.ConstructorArguments[0].Value;
				WindowsRuntimeProjections.ApplyProjection(attribute, new CustomAttributeValueProjection(attributeTargets, customAttributeValueTreatment));
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001C348 File Offset: 0x0001A548
		private static bool IsWindowsAttributeUsageAttribute(ICustomAttributeProvider owner, CustomAttribute attribute)
		{
			if (owner.MetadataToken.TokenType != TokenType.TypeDef)
			{
				return false;
			}
			MethodReference constructor = attribute.Constructor;
			if (constructor.MetadataToken.TokenType != TokenType.MemberRef)
			{
				return false;
			}
			TypeReference declaringType = constructor.DeclaringType;
			return declaringType.MetadataToken.TokenType == TokenType.TypeRef && declaringType.Name == "AttributeUsageAttribute" && declaringType.Namespace == "System";
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001C3CC File Offset: 0x0001A5CC
		private static bool HasAttribute(TypeDefinition type, string @namespace, string name)
		{
			foreach (CustomAttribute customAttribute in type.CustomAttributes)
			{
				TypeReference attributeType = customAttribute.AttributeType;
				if (attributeType.Name == name && attributeType.Namespace == @namespace)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001C444 File Offset: 0x0001A644
		public static void ApplyProjection(CustomAttribute attribute, CustomAttributeValueProjection projection)
		{
			if (projection != null)
			{
				bool flag;
				bool flag2;
				switch (projection.Treatment)
				{
				case CustomAttributeValueTreatment.AllowSingle:
					flag = false;
					flag2 = false;
					break;
				case CustomAttributeValueTreatment.AllowMultiple:
					flag = false;
					flag2 = true;
					break;
				case CustomAttributeValueTreatment.VersionAttribute:
				case CustomAttributeValueTreatment.DeprecatedAttribute:
					flag = true;
					flag2 = true;
					break;
				default:
					throw new ArgumentException();
				}
				AttributeTargets attributeTargets = (AttributeTargets)attribute.ConstructorArguments[0].Value;
				if (flag)
				{
					attributeTargets |= 160;
				}
				attribute.ConstructorArguments[0] = new CustomAttributeArgument(attribute.ConstructorArguments[0].Type, attributeTargets);
				attribute.Properties.Add(new CustomAttributeNamedArgument("AllowMultiple", new CustomAttributeArgument(attribute.Module.TypeSystem.Boolean, flag2)));
				attribute.projection = projection;
			}
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0001C51C File Offset: 0x0001A71C
		public static CustomAttributeValueProjection RemoveProjection(CustomAttribute attribute)
		{
			if (attribute.projection == null)
			{
				return null;
			}
			CustomAttributeValueProjection projection = attribute.projection;
			attribute.projection = null;
			attribute.ConstructorArguments[0] = new CustomAttributeArgument(attribute.ConstructorArguments[0].Type, projection.Targets);
			attribute.Properties.Clear();
			return projection;
		}

		// Token: 0x04000323 RID: 803
		private static readonly Version version = new Version(4, 0, 0, 0);

		// Token: 0x04000324 RID: 804
		private static readonly byte[] contract_pk_token = new byte[] { 176, 63, 95, 127, 17, 213, 10, 58 };

		// Token: 0x04000325 RID: 805
		private static readonly byte[] contract_pk = new byte[]
		{
			0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
			0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
			82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
			1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
			163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
			253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
			76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
			220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
			11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
			16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
			146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
			232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
			40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
			61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
			15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
			24, 32, 109, 192, 147, 52, 77, 90, 210, 147
		};

		// Token: 0x04000326 RID: 806
		private static Dictionary<string, WindowsRuntimeProjections.ProjectionInfo> projections;

		// Token: 0x04000327 RID: 807
		private readonly ModuleDefinition module;

		// Token: 0x04000328 RID: 808
		private Version corlib_version = new Version(255, 255, 255, 255);

		// Token: 0x04000329 RID: 809
		private AssemblyNameReference[] virtual_references;

		// Token: 0x0200014D RID: 333
		private struct ProjectionInfo
		{
			// Token: 0x06000BE5 RID: 3045 RVA: 0x000265B9 File Offset: 0x000247B9
			public ProjectionInfo(string winrt_namespace, string clr_namespace, string clr_name, string clr_assembly, bool attribute = false)
			{
				this.WinRTNamespace = winrt_namespace;
				this.ClrNamespace = clr_namespace;
				this.ClrName = clr_name;
				this.ClrAssembly = clr_assembly;
				this.Attribute = attribute;
			}

			// Token: 0x04000750 RID: 1872
			public readonly string WinRTNamespace;

			// Token: 0x04000751 RID: 1873
			public readonly string ClrNamespace;

			// Token: 0x04000752 RID: 1874
			public readonly string ClrName;

			// Token: 0x04000753 RID: 1875
			public readonly string ClrAssembly;

			// Token: 0x04000754 RID: 1876
			public readonly bool Attribute;
		}
	}
}
