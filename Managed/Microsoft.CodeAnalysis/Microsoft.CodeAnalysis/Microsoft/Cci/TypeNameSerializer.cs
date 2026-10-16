using System;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.Cci
{
	// Token: 0x020000C6 RID: 198
	internal static class TypeNameSerializer
	{
		// Token: 0x060006F1 RID: 1777 RVA: 0x00017B98 File Offset: 0x00015D98
		internal static string GetSerializedTypeName(this ITypeReference typeReference, EmitContext context)
		{
			bool flag = true;
			return typeReference.GetSerializedTypeName(context, ref flag);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00017BB0 File Offset: 0x00015DB0
		internal static string GetSerializedTypeName(this ITypeReference typeReference, EmitContext context, ref bool isAssemblyQualified)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			IArrayTypeReference arrayTypeReference = typeReference as IArrayTypeReference;
			if (arrayTypeReference != null)
			{
				typeReference = arrayTypeReference.GetElementType(context);
				bool flag = false;
				TypeNameSerializer.AppendSerializedTypeName(builder, typeReference, ref flag, context);
				if (arrayTypeReference.IsSZArray)
				{
					builder.Append("[]");
				}
				else
				{
					builder.Append('[');
					if (arrayTypeReference.Rank == 1)
					{
						builder.Append('*');
					}
					builder.Append(',', arrayTypeReference.Rank - 1);
					builder.Append(']');
				}
			}
			else
			{
				IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
				if (pointerTypeReference != null)
				{
					typeReference = pointerTypeReference.GetTargetType(context);
					bool flag2 = false;
					TypeNameSerializer.AppendSerializedTypeName(builder, typeReference, ref flag2, context);
					builder.Append('*');
				}
				else
				{
					INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
					if (asNamespaceTypeReference != null)
					{
						string namespaceName = asNamespaceTypeReference.NamespaceName;
						if (namespaceName.Length != 0)
						{
							builder.Append(namespaceName);
							builder.Append('.');
						}
						builder.Append(TypeNameSerializer.GetMangledAndEscapedName(asNamespaceTypeReference));
					}
					else if (typeReference.IsTypeSpecification())
					{
						ITypeReference uninstantiatedGenericType = typeReference.GetUninstantiatedGenericType(context);
						ArrayBuilder<ITypeReference> instance2 = ArrayBuilder<ITypeReference>.GetInstance();
						typeReference.GetConsolidatedTypeArguments(instance2, context);
						bool flag3 = false;
						builder.Append(uninstantiatedGenericType.GetSerializedTypeName(context, ref flag3));
						builder.Append('[');
						bool flag4 = true;
						foreach (ITypeReference typeReference2 in instance2)
						{
							if (flag4)
							{
								flag4 = false;
							}
							else
							{
								builder.Append(',');
							}
							bool flag5 = true;
							TypeNameSerializer.AppendSerializedTypeName(builder, typeReference2, ref flag5, context);
						}
						instance2.Free();
						builder.Append(']');
					}
					else
					{
						INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
						if (asNestedTypeReference != null)
						{
							bool flag6 = false;
							builder.Append(asNestedTypeReference.GetContainingType(context).GetSerializedTypeName(context, ref flag6));
							builder.Append('+');
							builder.Append(TypeNameSerializer.GetMangledAndEscapedName(asNestedTypeReference));
						}
					}
				}
			}
			if (isAssemblyQualified)
			{
				TypeNameSerializer.AppendAssemblyQualifierIfNecessary(builder, TypeNameSerializer.UnwrapTypeReference(typeReference, context), out isAssemblyQualified, context);
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00017D98 File Offset: 0x00015F98
		private static void AppendSerializedTypeName(StringBuilder sb, ITypeReference type, ref bool isAssemQualified, EmitContext context)
		{
			string serializedTypeName = type.GetSerializedTypeName(context, ref isAssemQualified);
			if (isAssemQualified)
			{
				sb.Append('[');
			}
			sb.Append(serializedTypeName);
			if (isAssemQualified)
			{
				sb.Append(']');
			}
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00017DD0 File Offset: 0x00015FD0
		private static void AppendAssemblyQualifierIfNecessary(StringBuilder sb, ITypeReference typeReference, out bool isAssemQualified, EmitContext context)
		{
			INestedTypeReference asNestedTypeReference = typeReference.AsNestedTypeReference;
			if (asNestedTypeReference != null)
			{
				TypeNameSerializer.AppendAssemblyQualifierIfNecessary(sb, asNestedTypeReference.GetContainingType(context), out isAssemQualified, context);
				return;
			}
			IGenericTypeInstanceReference asGenericTypeInstanceReference = typeReference.AsGenericTypeInstanceReference;
			if (asGenericTypeInstanceReference != null)
			{
				TypeNameSerializer.AppendAssemblyQualifierIfNecessary(sb, asGenericTypeInstanceReference.GetGenericType(context), out isAssemQualified, context);
				return;
			}
			IArrayTypeReference arrayTypeReference = typeReference as IArrayTypeReference;
			if (arrayTypeReference != null)
			{
				TypeNameSerializer.AppendAssemblyQualifierIfNecessary(sb, arrayTypeReference.GetElementType(context), out isAssemQualified, context);
				return;
			}
			IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
			if (pointerTypeReference != null)
			{
				TypeNameSerializer.AppendAssemblyQualifierIfNecessary(sb, pointerTypeReference.GetTargetType(context), out isAssemQualified, context);
				return;
			}
			isAssemQualified = false;
			IAssemblyReference assemblyReference = null;
			INamespaceTypeReference asNamespaceTypeReference = typeReference.AsNamespaceTypeReference;
			if (asNamespaceTypeReference != null)
			{
				assemblyReference = asNamespaceTypeReference.GetUnit(context) as IAssemblyReference;
			}
			if (assemblyReference != null)
			{
				IAssemblyReference containingAssembly = context.Module.GetContainingAssembly(context);
				if (containingAssembly == null || assemblyReference != containingAssembly)
				{
					sb.Append(", ");
					sb.Append(MetadataWriter.StrongName(assemblyReference));
					isAssemQualified = true;
				}
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00017EA0 File Offset: 0x000160A0
		private static string GetMangledAndEscapedName(INamedTypeReference namedType)
		{
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			string name = namedType.Name;
			for (int i = 0; i < name.Length; i++)
			{
				char c = name.get_Chars(i);
				if ("\\[]*.+,& ".IndexOf(c) >= 0)
				{
					builder.Append('\\');
				}
				builder.Append(c);
			}
			if (namedType.MangleName && namedType.GenericParameterCount > 0)
			{
				builder.Append(MetadataHelpers.GetAritySuffix((int)namedType.GenericParameterCount));
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00017F28 File Offset: 0x00016128
		private static ITypeReference UnwrapTypeReference(ITypeReference typeReference, EmitContext context)
		{
			for (;;)
			{
				IArrayTypeReference arrayTypeReference = typeReference as IArrayTypeReference;
				if (arrayTypeReference != null)
				{
					typeReference = arrayTypeReference.GetElementType(context);
				}
				else
				{
					IPointerTypeReference pointerTypeReference = typeReference as IPointerTypeReference;
					if (pointerTypeReference == null)
					{
						break;
					}
					typeReference = pointerTypeReference.GetTargetType(context);
				}
			}
			return typeReference;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00017F60 File Offset: 0x00016160
		internal static string BuildQualifiedNamespaceName(INamespace @namespace)
		{
			if (@namespace.ContainingNamespace == null)
			{
				return @namespace.Name;
			}
			ArrayBuilder<string> instance = ArrayBuilder<string>.GetInstance();
			do
			{
				string name = @namespace.Name;
				if (name.Length != 0)
				{
					instance.Add(name);
				}
				@namespace = @namespace.ContainingNamespace;
			}
			while (@namespace != null);
			PooledStringBuilder instance2 = PooledStringBuilder.GetInstance();
			for (int i = instance.Count - 1; i >= 0; i--)
			{
				instance2.Builder.Append(instance[i]);
				if (i > 0)
				{
					instance2.Builder.Append('.');
				}
			}
			instance.Free();
			return instance2.ToStringAndFree();
		}
	}
}
