using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Trivial.Mono.Cecil.Cil;
using Trivial.Mono.Cecil.Metadata;
using Trivial.Mono.Collections.Generic;
using Trivial.Mono.Security.Cryptography;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000089 RID: 137
	internal static class Mixin
	{
		// Token: 0x06000553 RID: 1363 RVA: 0x00010F57 File Offset: 0x0000F157
		public static bool IsNullOrEmpty<T>(this T[] self)
		{
			return self == null || self.Length == 0;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010F63 File Offset: 0x0000F163
		public static bool IsNullOrEmpty<T>(this Collection<T> self)
		{
			return self == null || self.size == 0;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00010F73 File Offset: 0x0000F173
		public static T[] Resize<T>(this T[] self, int length)
		{
			Array.Resize<T>(ref self, length);
			return self;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00010F7E File Offset: 0x0000F17E
		public static T[] Add<T>(this T[] self, T item)
		{
			if (self == null)
			{
				self = new T[] { item };
				return self;
			}
			self = self.Resize<T>(self.Length + 1);
			self[self.Length - 1] = item;
			return self;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00010FB0 File Offset: 0x0000F1B0
		public static Version CheckVersion(Version version)
		{
			if (version == null)
			{
				return Mixin.ZeroVersion;
			}
			if (version.Build == -1)
			{
				return new Version(version.Major, version.Minor, 0, 0);
			}
			if (version.Revision == -1)
			{
				return new Version(version.Major, version.Minor, version.Build, 0);
			}
			return version;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001100C File Offset: 0x0000F20C
		public static bool TryGetUniqueDocument(this MethodDebugInformation info, out Document document)
		{
			document = info.SequencePoints[0].Document;
			for (int i = 1; i < info.SequencePoints.Count; i++)
			{
				if (info.SequencePoints[i].Document != document)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x0001105C File Offset: 0x0000F25C
		public static void ResolveConstant(this IConstantProvider self, ref object constant, ModuleDefinition module)
		{
			if (module == null)
			{
				constant = Mixin.NoValue;
				return;
			}
			object syncRoot = module.SyncRoot;
			lock (syncRoot)
			{
				if (constant == Mixin.NotResolved)
				{
					if (module.HasImage())
					{
						constant = module.Read<IConstantProvider, object>(self, (IConstantProvider provider, MetadataReader reader) => reader.ReadConstant(provider));
					}
					else
					{
						constant = Mixin.NoValue;
					}
				}
			}
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x000110DC File Offset: 0x0000F2DC
		public static bool GetHasCustomAttributes(this ICustomAttributeProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<ICustomAttributeProvider, bool>(self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.HasCustomAttributes(provider));
			}
			return false;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00011110 File Offset: 0x0000F310
		public static Collection<CustomAttribute> GetCustomAttributes(this ICustomAttributeProvider self, ref Collection<CustomAttribute> variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<CustomAttribute> collection;
				variable = (collection = new Collection<CustomAttribute>());
				return collection;
			}
			return module.Read<ICustomAttributeProvider, Collection<CustomAttribute>>(ref variable, self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.ReadCustomAttributes(provider));
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00011158 File Offset: 0x0000F358
		public static bool ContainsGenericParameter(this IGenericInstance self)
		{
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (genericArguments[i].ContainsGenericParameter)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00011190 File Offset: 0x0000F390
		public static void GenericInstanceFullName(this IGenericInstance self, StringBuilder builder)
		{
			builder.Append("<");
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (i > 0)
				{
					builder.Append(",");
				}
				builder.Append(genericArguments[i].FullName);
			}
			builder.Append(">");
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x000111F0 File Offset: 0x0000F3F0
		public static bool GetHasGenericParameters(this IGenericParameterProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<IGenericParameterProvider, bool>(self, (IGenericParameterProvider provider, MetadataReader reader) => reader.HasGenericParameters(provider));
			}
			return false;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00011224 File Offset: 0x0000F424
		public static Collection<GenericParameter> GetGenericParameters(this IGenericParameterProvider self, ref Collection<GenericParameter> collection, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<GenericParameter> collection2;
				collection = (collection2 = new GenericParameterCollection(self));
				return collection2;
			}
			return module.Read<IGenericParameterProvider, Collection<GenericParameter>>(ref collection, self, (IGenericParameterProvider provider, MetadataReader reader) => reader.ReadGenericParameters(provider));
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001126C File Offset: 0x0000F46C
		public static bool GetHasMarshalInfo(this IMarshalInfoProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<IMarshalInfoProvider, bool>(self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.HasMarshalInfo(provider));
			}
			return false;
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001129E File Offset: 0x0000F49E
		public static MarshalInfo GetMarshalInfo(this IMarshalInfoProvider self, ref MarshalInfo variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				return null;
			}
			return module.Read<IMarshalInfoProvider, MarshalInfo>(ref variable, self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.ReadMarshalInfo(provider));
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x000112D1 File Offset: 0x0000F4D1
		public static bool GetAttributes(this uint self, uint attributes)
		{
			return (self & attributes) > 0U;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x000112D9 File Offset: 0x0000F4D9
		public static uint SetAttributes(this uint self, uint attributes, bool value)
		{
			if (value)
			{
				return self | attributes;
			}
			return self & ~attributes;
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x000112E6 File Offset: 0x0000F4E6
		public static bool GetMaskedAttributes(this uint self, uint mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x000112EE File Offset: 0x0000F4EE
		public static uint SetMaskedAttributes(this uint self, uint mask, uint attributes, bool value)
		{
			if (value)
			{
				self &= ~mask;
				return self | attributes;
			}
			return self & ~(mask & attributes);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x000112D1 File Offset: 0x0000F4D1
		public static bool GetAttributes(this ushort self, ushort attributes)
		{
			return (self & attributes) > 0;
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011303 File Offset: 0x0000F503
		public static ushort SetAttributes(this ushort self, ushort attributes, bool value)
		{
			if (value)
			{
				return self | attributes;
			}
			return self & ~attributes;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00011312 File Offset: 0x0000F512
		public static bool GetMaskedAttributes(this ushort self, ushort mask, uint attributes)
		{
			return (long)(self & mask) == (long)((ulong)attributes);
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001131C File Offset: 0x0000F51C
		public static ushort SetMaskedAttributes(this ushort self, ushort mask, uint attributes, bool value)
		{
			if (value)
			{
				self &= ~mask;
				return (ushort)((uint)self | attributes);
			}
			return (ushort)((uint)self & ~((uint)mask & attributes));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00011334 File Offset: 0x0000F534
		public static bool HasImplicitThis(this IMethodSignature self)
		{
			return self.HasThis && !self.ExplicitThis;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001134C File Offset: 0x0000F54C
		public static void MethodSignatureFullName(this IMethodSignature self, StringBuilder builder)
		{
			builder.Append("(");
			if (self.HasParameters)
			{
				Collection<ParameterDefinition> parameters = self.Parameters;
				for (int i = 0; i < parameters.Count; i++)
				{
					ParameterDefinition parameterDefinition = parameters[i];
					if (i > 0)
					{
						builder.Append(",");
					}
					if (parameterDefinition.ParameterType.IsSentinel)
					{
						builder.Append("...,");
					}
					builder.Append(parameterDefinition.ParameterType.FullName);
				}
			}
			builder.Append(")");
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x000113D4 File Offset: 0x0000F5D4
		public static void CheckModule(ModuleDefinition module)
		{
			if (module == null)
			{
				throw new ArgumentNullException(Mixin.Argument.module.ToString());
			}
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x000113FC File Offset: 0x0000F5FC
		public static bool TryGetAssemblyNameReference(this ModuleDefinition module, AssemblyNameReference name_reference, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (Mixin.Equals(name_reference, assemblyNameReference))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0001143C File Offset: 0x0000F63C
		private static bool Equals(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00011477 File Offset: 0x0000F677
		private static bool Equals<T>(T a, T b) where T : class, IEquatable<T>
		{
			if (a == b)
			{
				return true;
			}
			T t = a;
			return t != null && t.Equals(b);
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001149C File Offset: 0x0000F69C
		private static bool Equals(AssemblyNameReference a, AssemblyNameReference b)
		{
			return a == b || (!(a.Name != b.Name) && Mixin.Equals<Version>(a.Version, b.Version) && !(a.Culture != b.Culture) && Mixin.Equals(a.PublicKeyToken, b.PublicKeyToken));
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00011504 File Offset: 0x0000F704
		public static ParameterDefinition GetParameter(this MethodBody self, int index)
		{
			MethodDefinition method = self.method;
			if (method.HasThis)
			{
				if (index == 0)
				{
					return self.ThisParameter;
				}
				index--;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			if (index < 0 || index >= parameters.size)
			{
				return null;
			}
			return parameters[index];
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0001154C File Offset: 0x0000F74C
		public static VariableDefinition GetVariable(this MethodBody self, int index)
		{
			Collection<VariableDefinition> variables = self.Variables;
			if (index < 0 || index >= variables.size)
			{
				return null;
			}
			return variables[index];
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00011576 File Offset: 0x0000F776
		public static bool GetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics)
		{
			return (self.SemanticsAttributes & semantics) > MethodSemanticsAttributes.None;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00011583 File Offset: 0x0000F783
		public static void SetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics, bool value)
		{
			if (value)
			{
				self.SemanticsAttributes |= semantics;
				return;
			}
			self.SemanticsAttributes &= ~semantics;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000115A7 File Offset: 0x0000F7A7
		public static bool IsVarArg(this IMethodSignature self)
		{
			return (self.CallingConvention & MethodCallingConvention.VarArg) > MethodCallingConvention.Default;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x000115B4 File Offset: 0x0000F7B4
		public static int GetSentinelPosition(this IMethodSignature self)
		{
			if (!self.HasParameters)
			{
				return -1;
			}
			Collection<ParameterDefinition> parameters = self.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				if (parameters[i].ParameterType.IsSentinel)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000115FC File Offset: 0x0000F7FC
		public static void CheckName(object name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(Mixin.Argument.name.ToString());
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00011624 File Offset: 0x0000F824
		public static void CheckName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullOrEmptyException(Mixin.Argument.name.ToString());
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00011650 File Offset: 0x0000F850
		public static void CheckFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullOrEmptyException(Mixin.Argument.fileName.ToString());
			}
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0001167C File Offset: 0x0000F87C
		public static void CheckFullName(string fullName)
		{
			if (string.IsNullOrEmpty(fullName))
			{
				throw new ArgumentNullOrEmptyException(Mixin.Argument.fullName.ToString());
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x000116A8 File Offset: 0x0000F8A8
		public static void CheckStream(object stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(Mixin.Argument.stream.ToString());
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x000116CD File Offset: 0x0000F8CD
		public static void CheckWriteSeek(Stream stream)
		{
			if (!stream.CanWrite || !stream.CanSeek)
			{
				throw new ArgumentException("Stream must be writable and seekable.");
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x000116EA File Offset: 0x0000F8EA
		public static void CheckReadSeek(Stream stream)
		{
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException("Stream must be readable and seekable.");
			}
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00011708 File Offset: 0x0000F908
		public static void CheckType(object type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Mixin.Argument.type.ToString());
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0001172D File Offset: 0x0000F92D
		public static void CheckType(object type, Mixin.Argument argument)
		{
			if (type == null)
			{
				throw new ArgumentNullException(argument.ToString());
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00011748 File Offset: 0x0000F948
		public static void CheckField(object field)
		{
			if (field == null)
			{
				throw new ArgumentNullException(Mixin.Argument.field.ToString());
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00011770 File Offset: 0x0000F970
		public static void CheckMethod(object method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(Mixin.Argument.method.ToString());
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00011798 File Offset: 0x0000F998
		public static void CheckParameters(object parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException(Mixin.Argument.parameters.ToString());
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000117C0 File Offset: 0x0000F9C0
		public static uint GetTimestamp()
		{
			return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000117EF File Offset: 0x0000F9EF
		public static bool HasImage(this ModuleDefinition self)
		{
			return self != null && self.HasImage;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000117FC File Offset: 0x0000F9FC
		public static string GetFileName(this Stream self)
		{
			FileStream fileStream = self as FileStream;
			if (fileStream == null)
			{
				return string.Empty;
			}
			return Path.GetFullPath(fileStream.Name);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00011824 File Offset: 0x0000FA24
		public static void CopyTo(this Stream self, Stream target)
		{
			byte[] array = new byte[8192];
			int num;
			while ((num = self.Read(array, 0, array.Length)) > 0)
			{
				target.Write(array, 0, num);
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00011858 File Offset: 0x0000FA58
		public static TargetRuntime ParseRuntime(this string self)
		{
			char c = self.get_Chars(1);
			if (c != '1')
			{
				if (c != '2')
				{
					return TargetRuntime.Net_4_0;
				}
				return TargetRuntime.Net_2_0;
			}
			else
			{
				if (self.get_Chars(3) != '0')
				{
					return TargetRuntime.Net_1_1;
				}
				return TargetRuntime.Net_1_0;
			}
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x0001188B File Offset: 0x0000FA8B
		public static string RuntimeVersionString(this TargetRuntime runtime)
		{
			switch (runtime)
			{
			case TargetRuntime.Net_1_0:
				return "v1.0.3705";
			case TargetRuntime.Net_1_1:
				return "v1.1.4322";
			case TargetRuntime.Net_2_0:
				return "v2.0.50727";
			default:
				return "v4.0.30319";
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x000118B8 File Offset: 0x0000FAB8
		public static bool IsWindowsMetadata(this ModuleDefinition module)
		{
			return module.MetadataKind > MetadataKind.Ecma335;
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x000118C4 File Offset: 0x0000FAC4
		public static byte[] ReadAll(this Stream self)
		{
			MemoryStream memoryStream = new MemoryStream((int)self.Length);
			byte[] array = new byte[1024];
			int num;
			while ((num = self.Read(array, 0, array.Length)) != 0)
			{
				memoryStream.Write(array, 0, num);
			}
			return memoryStream.ToArray();
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00002921 File Offset: 0x00000B21
		public static void Read(object o)
		{
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00011909 File Offset: 0x0000FB09
		public static bool GetHasSecurityDeclarations(this ISecurityDeclarationProvider self, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read<ISecurityDeclarationProvider, bool>(self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.HasSecurityDeclarations(provider));
			}
			return false;
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x0001193C File Offset: 0x0000FB3C
		public static Collection<SecurityDeclaration> GetSecurityDeclarations(this ISecurityDeclarationProvider self, ref Collection<SecurityDeclaration> variable, ModuleDefinition module)
		{
			if (!module.HasImage())
			{
				Collection<SecurityDeclaration> collection;
				variable = (collection = new Collection<SecurityDeclaration>());
				return collection;
			}
			return module.Read<ISecurityDeclarationProvider, Collection<SecurityDeclaration>>(ref variable, self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.ReadSecurityDeclarations(provider));
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00011984 File Offset: 0x0000FB84
		public static TypeReference GetEnumUnderlyingType(this TypeDefinition self)
		{
			Collection<FieldDefinition> fields = self.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!fieldDefinition.IsStatic)
				{
					return fieldDefinition.FieldType;
				}
			}
			throw new ArgumentException();
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public static TypeDefinition GetNestedType(this TypeDefinition self, string fullname)
		{
			if (!self.HasNestedTypes)
			{
				return null;
			}
			Collection<TypeDefinition> nestedTypes = self.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				if (typeDefinition.TypeFullName() == fullname)
				{
					return typeDefinition;
				}
			}
			return null;
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00011A10 File Offset: 0x0000FC10
		public static bool IsPrimitive(this ElementType self)
		{
			return self - ElementType.Boolean <= 11 || self - ElementType.I <= 1;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00011A23 File Offset: 0x0000FC23
		public static string TypeFullName(this TypeReference self)
		{
			if (!string.IsNullOrEmpty(self.Namespace))
			{
				return self.Namespace + "." + self.Name;
			}
			return self.Name;
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x00011A4F File Offset: 0x0000FC4F
		public static bool IsTypeOf(this TypeReference self, string @namespace, string name)
		{
			return self.Name == name && self.Namespace == @namespace;
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x00011A70 File Offset: 0x0000FC70
		public static bool IsTypeSpecification(this TypeReference type)
		{
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.GenericInst:
			case ElementType.FnPtr:
			case ElementType.SzArray:
			case ElementType.MVar:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				break;
			case ElementType.ValueType:
			case ElementType.Class:
			case ElementType.TypedByRef:
			case (ElementType)23:
			case ElementType.I:
			case ElementType.U:
			case (ElementType)26:
			case ElementType.Object:
				return false;
			default:
				if (etype != ElementType.Sentinel && etype != ElementType.Pinned)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		public static TypeDefinition CheckedResolve(this TypeReference self)
		{
			TypeDefinition typeDefinition = self.Resolve();
			if (typeDefinition == null)
			{
				throw new ResolutionException(self);
			}
			return typeDefinition;
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00011B04 File Offset: 0x0000FD04
		public static bool TryGetCoreLibraryReference(this ModuleDefinition module, out AssemblyNameReference reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				reference = assemblyReferences[i];
				if (Mixin.IsCoreLibrary(reference))
				{
					return true;
				}
			}
			reference = null;
			return false;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00011B44 File Offset: 0x0000FD44
		public static bool IsCoreLibrary(this ModuleDefinition module)
		{
			if (module.Assembly == null)
			{
				return false;
			}
			if (!Mixin.IsCoreLibrary(module.Assembly.Name))
			{
				return false;
			}
			if (module.HasImage)
			{
				if (module.Read<ModuleDefinition, bool>(module, (ModuleDefinition m, MetadataReader reader) => reader.image.GetTableLength(Table.AssemblyRef) > 0))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x00011BA4 File Offset: 0x0000FDA4
		private static bool IsCoreLibrary(AssemblyNameReference reference)
		{
			string name = reference.Name;
			return name == "mscorlib" || name == "System.Runtime" || name == "System.Private.CoreLib" || name == "netstandard";
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00011BEC File Offset: 0x0000FDEC
		public static ImageDebugHeaderEntry GetCodeViewEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.CodeView);
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00011BF5 File Offset: 0x0000FDF5
		public static ImageDebugHeaderEntry GetDeterministicEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.Deterministic);
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x00011C00 File Offset: 0x0000FE00
		public static ImageDebugHeader AddDeterministicEntry(this ImageDebugHeader header)
		{
			ImageDebugHeaderEntry imageDebugHeaderEntry = new ImageDebugHeaderEntry(new ImageDebugDirectory
			{
				Type = ImageDebugType.Deterministic
			}, Empty<byte>.Array);
			if (header == null)
			{
				return new ImageDebugHeader(imageDebugHeaderEntry);
			}
			ImageDebugHeaderEntry[] array = new ImageDebugHeaderEntry[header.Entries.Length + 1];
			Array.Copy(header.Entries, array, header.Entries.Length);
			array[array.Length - 1] = imageDebugHeaderEntry;
			return new ImageDebugHeader(array);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x00011C66 File Offset: 0x0000FE66
		public static ImageDebugHeaderEntry GetEmbeddedPortablePdbEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.EmbeddedPortablePdb);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00011C70 File Offset: 0x0000FE70
		private static ImageDebugHeaderEntry GetEntry(this ImageDebugHeader header, ImageDebugType type)
		{
			if (!header.HasEntries)
			{
				return null;
			}
			for (int i = 0; i < header.Entries.Length; i++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry = header.Entries[i];
				if (imageDebugHeaderEntry.Directory.Type == type)
				{
					return imageDebugHeaderEntry;
				}
			}
			return null;
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x00011CB4 File Offset: 0x0000FEB4
		public static string GetPdbFileName(string assemblyFileName)
		{
			return Path.ChangeExtension(assemblyFileName, ".pdb");
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x00011CC1 File Offset: 0x0000FEC1
		public static string GetMdbFileName(string assemblyFileName)
		{
			return assemblyFileName + ".mdb";
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00011CD0 File Offset: 0x0000FED0
		public static bool IsPortablePdb(string fileName)
		{
			bool flag;
			using (FileStream fileStream = new FileStream(fileName, 3, 1, 1))
			{
				flag = Mixin.IsPortablePdb(fileStream);
			}
			return flag;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00011D0C File Offset: 0x0000FF0C
		public static bool IsPortablePdb(Stream stream)
		{
			if (stream.Length < 4L)
			{
				return false;
			}
			long position = stream.Position;
			bool flag;
			try
			{
				flag = new BinaryReader(stream).ReadUInt32() == 1112167234U;
			}
			finally
			{
				stream.Position = position;
			}
			return flag;
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00011D5C File Offset: 0x0000FF5C
		public static uint ReadCompressedUInt32(this byte[] data, ref int position)
		{
			uint num;
			if ((data[position] & 128) == 0)
			{
				num = (uint)data[position];
				position++;
			}
			else if ((data[position] & 64) == 0)
			{
				num = ((uint)data[position] & 4294967167U) << 8;
				num |= (uint)data[position + 1];
				position += 2;
			}
			else
			{
				num = ((uint)data[position] & 4294967103U) << 24;
				num |= (uint)((uint)data[position + 1] << 16);
				num |= (uint)((uint)data[position + 2] << 8);
				num |= (uint)data[position + 3];
				position += 4;
			}
			return num;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public static MetadataToken GetMetadataToken(this CodedIndex self, uint data)
		{
			uint num;
			TokenType tokenType;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				num = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					tokenType = TokenType.TypeDef;
					break;
				case 1U:
					tokenType = TokenType.TypeRef;
					break;
				case 2U:
					tokenType = TokenType.TypeSpec;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.HasConstant:
				num = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					tokenType = TokenType.Field;
					break;
				case 1U:
					tokenType = TokenType.Param;
					break;
				case 2U:
					tokenType = TokenType.Property;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.HasCustomAttribute:
				num = data >> 5;
				switch (data & 31U)
				{
				case 0U:
					tokenType = TokenType.Method;
					break;
				case 1U:
					tokenType = TokenType.Field;
					break;
				case 2U:
					tokenType = TokenType.TypeRef;
					break;
				case 3U:
					tokenType = TokenType.TypeDef;
					break;
				case 4U:
					tokenType = TokenType.Param;
					break;
				case 5U:
					tokenType = TokenType.InterfaceImpl;
					break;
				case 6U:
					tokenType = TokenType.MemberRef;
					break;
				case 7U:
					tokenType = TokenType.Module;
					break;
				case 8U:
					tokenType = TokenType.Permission;
					break;
				case 9U:
					tokenType = TokenType.Property;
					break;
				case 10U:
					tokenType = TokenType.Event;
					break;
				case 11U:
					tokenType = TokenType.Signature;
					break;
				case 12U:
					tokenType = TokenType.ModuleRef;
					break;
				case 13U:
					tokenType = TokenType.TypeSpec;
					break;
				case 14U:
					tokenType = TokenType.Assembly;
					break;
				case 15U:
					tokenType = TokenType.AssemblyRef;
					break;
				case 16U:
					tokenType = TokenType.File;
					break;
				case 17U:
					tokenType = TokenType.ExportedType;
					break;
				case 18U:
					tokenType = TokenType.ManifestResource;
					break;
				case 19U:
					tokenType = TokenType.GenericParam;
					break;
				case 20U:
					tokenType = TokenType.GenericParamConstraint;
					break;
				case 21U:
					tokenType = TokenType.MethodSpec;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.HasFieldMarshal:
			{
				num = data >> 1;
				uint num2 = data & 1U;
				if (num2 != 0U)
				{
					if (num2 != 1U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.Param;
				}
				else
				{
					tokenType = TokenType.Field;
				}
				break;
			}
			case CodedIndex.HasDeclSecurity:
				num = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					tokenType = TokenType.TypeDef;
					break;
				case 1U:
					tokenType = TokenType.Method;
					break;
				case 2U:
					tokenType = TokenType.Assembly;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.MemberRefParent:
				num = data >> 3;
				switch (data & 7U)
				{
				case 0U:
					tokenType = TokenType.TypeDef;
					break;
				case 1U:
					tokenType = TokenType.TypeRef;
					break;
				case 2U:
					tokenType = TokenType.ModuleRef;
					break;
				case 3U:
					tokenType = TokenType.Method;
					break;
				case 4U:
					tokenType = TokenType.TypeSpec;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.HasSemantics:
			{
				num = data >> 1;
				uint num3 = data & 1U;
				if (num3 != 0U)
				{
					if (num3 != 1U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.Property;
				}
				else
				{
					tokenType = TokenType.Event;
				}
				break;
			}
			case CodedIndex.MethodDefOrRef:
			{
				num = data >> 1;
				uint num4 = data & 1U;
				if (num4 != 0U)
				{
					if (num4 != 1U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.MemberRef;
				}
				else
				{
					tokenType = TokenType.Method;
				}
				break;
			}
			case CodedIndex.MemberForwarded:
			{
				num = data >> 1;
				uint num5 = data & 1U;
				if (num5 != 0U)
				{
					if (num5 != 1U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.Method;
				}
				else
				{
					tokenType = TokenType.Field;
				}
				break;
			}
			case CodedIndex.Implementation:
				num = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					tokenType = TokenType.File;
					break;
				case 1U:
					tokenType = TokenType.AssemblyRef;
					break;
				case 2U:
					tokenType = TokenType.ExportedType;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.CustomAttributeType:
			{
				num = data >> 3;
				uint num6 = data & 7U;
				if (num6 != 2U)
				{
					if (num6 != 3U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.MemberRef;
				}
				else
				{
					tokenType = TokenType.Method;
				}
				break;
			}
			case CodedIndex.ResolutionScope:
				num = data >> 2;
				switch (data & 3U)
				{
				case 0U:
					tokenType = TokenType.Module;
					break;
				case 1U:
					tokenType = TokenType.ModuleRef;
					break;
				case 2U:
					tokenType = TokenType.AssemblyRef;
					break;
				case 3U:
					tokenType = TokenType.TypeRef;
					break;
				default:
					goto IL_05AC;
				}
				break;
			case CodedIndex.TypeOrMethodDef:
			{
				num = data >> 1;
				uint num7 = data & 1U;
				if (num7 != 0U)
				{
					if (num7 != 1U)
					{
						goto IL_05AC;
					}
					tokenType = TokenType.Method;
				}
				else
				{
					tokenType = TokenType.TypeDef;
				}
				break;
			}
			case CodedIndex.HasCustomDebugInformation:
				num = data >> 5;
				switch (data & 31U)
				{
				case 0U:
					tokenType = TokenType.Method;
					break;
				case 1U:
					tokenType = TokenType.Field;
					break;
				case 2U:
					tokenType = TokenType.TypeRef;
					break;
				case 3U:
					tokenType = TokenType.TypeDef;
					break;
				case 4U:
					tokenType = TokenType.Param;
					break;
				case 5U:
					tokenType = TokenType.InterfaceImpl;
					break;
				case 6U:
					tokenType = TokenType.MemberRef;
					break;
				case 7U:
					tokenType = TokenType.Module;
					break;
				case 8U:
					tokenType = TokenType.Permission;
					break;
				case 9U:
					tokenType = TokenType.Property;
					break;
				case 10U:
					tokenType = TokenType.Event;
					break;
				case 11U:
					tokenType = TokenType.Signature;
					break;
				case 12U:
					tokenType = TokenType.ModuleRef;
					break;
				case 13U:
					tokenType = TokenType.TypeSpec;
					break;
				case 14U:
					tokenType = TokenType.Assembly;
					break;
				case 15U:
					tokenType = TokenType.AssemblyRef;
					break;
				case 16U:
					tokenType = TokenType.File;
					break;
				case 17U:
					tokenType = TokenType.ExportedType;
					break;
				case 18U:
					tokenType = TokenType.ManifestResource;
					break;
				case 19U:
					tokenType = TokenType.GenericParam;
					break;
				case 20U:
					tokenType = TokenType.GenericParamConstraint;
					break;
				case 21U:
					tokenType = TokenType.MethodSpec;
					break;
				case 22U:
					tokenType = TokenType.Document;
					break;
				case 23U:
					tokenType = TokenType.LocalScope;
					break;
				case 24U:
					tokenType = TokenType.LocalVariable;
					break;
				case 25U:
					tokenType = TokenType.LocalConstant;
					break;
				case 26U:
					tokenType = TokenType.ImportScope;
					break;
				default:
					goto IL_05AC;
				}
				break;
			default:
				goto IL_05AC;
			}
			return new MetadataToken(tokenType, num);
			IL_05AC:
			return MetadataToken.Zero;
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x000123A0 File Offset: 0x000105A0
		public static uint CompressMetadataToken(this CodedIndex self, MetadataToken token)
		{
			uint num = 0U;
			if (token.RID == 0U)
			{
				return num;
			}
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.TypeRef)
				{
					return num | 1U;
				}
				if (tokenType == TokenType.TypeDef)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.TypeSpec)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.HasConstant:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Field)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Param)
				{
					return num | 1U;
				}
				if (tokenType == TokenType.Property)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.HasCustomAttribute:
			{
				num = token.RID << 5;
				TokenType tokenType = token.TokenType;
				if (tokenType <= TokenType.Event)
				{
					if (tokenType <= TokenType.Method)
					{
						if (tokenType <= TokenType.TypeRef)
						{
							if (tokenType == TokenType.Module)
							{
								return num | 7U;
							}
							if (tokenType == TokenType.TypeRef)
							{
								return num | 2U;
							}
						}
						else
						{
							if (tokenType == TokenType.TypeDef)
							{
								return num | 3U;
							}
							if (tokenType == TokenType.Field)
							{
								return num | 1U;
							}
							if (tokenType == TokenType.Method)
							{
								return num | 0U;
							}
						}
					}
					else if (tokenType <= TokenType.MemberRef)
					{
						if (tokenType == TokenType.Param)
						{
							return num | 4U;
						}
						if (tokenType == TokenType.InterfaceImpl)
						{
							return num | 5U;
						}
						if (tokenType == TokenType.MemberRef)
						{
							return num | 6U;
						}
					}
					else
					{
						if (tokenType == TokenType.Permission)
						{
							return num | 8U;
						}
						if (tokenType == TokenType.Signature)
						{
							return num | 11U;
						}
						if (tokenType == TokenType.Event)
						{
							return num | 10U;
						}
					}
				}
				else if (tokenType <= TokenType.AssemblyRef)
				{
					if (tokenType <= TokenType.ModuleRef)
					{
						if (tokenType == TokenType.Property)
						{
							return num | 9U;
						}
						if (tokenType == TokenType.ModuleRef)
						{
							return num | 12U;
						}
					}
					else
					{
						if (tokenType == TokenType.TypeSpec)
						{
							return num | 13U;
						}
						if (tokenType == TokenType.Assembly)
						{
							return num | 14U;
						}
						if (tokenType == TokenType.AssemblyRef)
						{
							return num | 15U;
						}
					}
				}
				else if (tokenType <= TokenType.ManifestResource)
				{
					if (tokenType == TokenType.File)
					{
						return num | 16U;
					}
					if (tokenType == TokenType.ExportedType)
					{
						return num | 17U;
					}
					if (tokenType == TokenType.ManifestResource)
					{
						return num | 18U;
					}
				}
				else
				{
					if (tokenType == TokenType.GenericParam)
					{
						return num | 19U;
					}
					if (tokenType == TokenType.MethodSpec)
					{
						return num | 21U;
					}
					if (tokenType == TokenType.GenericParamConstraint)
					{
						return num | 20U;
					}
				}
				break;
			}
			case CodedIndex.HasFieldMarshal:
			{
				num = token.RID << 1;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Field)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Param)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.HasDeclSecurity:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.TypeDef)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Method)
				{
					return num | 1U;
				}
				if (tokenType == TokenType.Assembly)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.MemberRefParent:
			{
				num = token.RID << 3;
				TokenType tokenType = token.TokenType;
				if (tokenType <= TokenType.TypeDef)
				{
					if (tokenType == TokenType.TypeRef)
					{
						return num | 1U;
					}
					if (tokenType == TokenType.TypeDef)
					{
						return num | 0U;
					}
				}
				else
				{
					if (tokenType == TokenType.Method)
					{
						return num | 3U;
					}
					if (tokenType == TokenType.ModuleRef)
					{
						return num | 2U;
					}
					if (tokenType == TokenType.TypeSpec)
					{
						return num | 4U;
					}
				}
				break;
			}
			case CodedIndex.HasSemantics:
			{
				num = token.RID << 1;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Event)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Property)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.MethodDefOrRef:
			{
				num = token.RID << 1;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Method)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.MemberRef)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.MemberForwarded:
			{
				num = token.RID << 1;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Field)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Method)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.Implementation:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.AssemblyRef)
				{
					return num | 1U;
				}
				if (tokenType == TokenType.File)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.ExportedType)
				{
					return num | 2U;
				}
				break;
			}
			case CodedIndex.CustomAttributeType:
			{
				num = token.RID << 3;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.Method)
				{
					return num | 2U;
				}
				if (tokenType == TokenType.MemberRef)
				{
					return num | 3U;
				}
				break;
			}
			case CodedIndex.ResolutionScope:
			{
				num = token.RID << 2;
				TokenType tokenType = token.TokenType;
				if (tokenType <= TokenType.TypeRef)
				{
					if (tokenType == TokenType.Module)
					{
						return num | 0U;
					}
					if (tokenType == TokenType.TypeRef)
					{
						return num | 3U;
					}
				}
				else
				{
					if (tokenType == TokenType.ModuleRef)
					{
						return num | 1U;
					}
					if (tokenType == TokenType.AssemblyRef)
					{
						return num | 2U;
					}
				}
				break;
			}
			case CodedIndex.TypeOrMethodDef:
			{
				num = token.RID << 1;
				TokenType tokenType = token.TokenType;
				if (tokenType == TokenType.TypeDef)
				{
					return num | 0U;
				}
				if (tokenType == TokenType.Method)
				{
					return num | 1U;
				}
				break;
			}
			case CodedIndex.HasCustomDebugInformation:
			{
				num = token.RID << 5;
				TokenType tokenType = token.TokenType;
				if (tokenType <= TokenType.ModuleRef)
				{
					if (tokenType <= TokenType.Param)
					{
						if (tokenType <= TokenType.TypeDef)
						{
							if (tokenType == TokenType.Module)
							{
								return num | 7U;
							}
							if (tokenType == TokenType.TypeRef)
							{
								return num | 2U;
							}
							if (tokenType == TokenType.TypeDef)
							{
								return num | 3U;
							}
						}
						else
						{
							if (tokenType == TokenType.Field)
							{
								return num | 1U;
							}
							if (tokenType == TokenType.Method)
							{
								return num | 0U;
							}
							if (tokenType == TokenType.Param)
							{
								return num | 4U;
							}
						}
					}
					else if (tokenType <= TokenType.Permission)
					{
						if (tokenType == TokenType.InterfaceImpl)
						{
							return num | 5U;
						}
						if (tokenType == TokenType.MemberRef)
						{
							return num | 6U;
						}
						if (tokenType == TokenType.Permission)
						{
							return num | 8U;
						}
					}
					else if (tokenType <= TokenType.Event)
					{
						if (tokenType == TokenType.Signature)
						{
							return num | 11U;
						}
						if (tokenType == TokenType.Event)
						{
							return num | 10U;
						}
					}
					else
					{
						if (tokenType == TokenType.Property)
						{
							return num | 9U;
						}
						if (tokenType == TokenType.ModuleRef)
						{
							return num | 12U;
						}
					}
				}
				else if (tokenType <= TokenType.GenericParam)
				{
					if (tokenType <= TokenType.AssemblyRef)
					{
						if (tokenType == TokenType.TypeSpec)
						{
							return num | 13U;
						}
						if (tokenType == TokenType.Assembly)
						{
							return num | 14U;
						}
						if (tokenType == TokenType.AssemblyRef)
						{
							return num | 15U;
						}
					}
					else if (tokenType <= TokenType.ExportedType)
					{
						if (tokenType == TokenType.File)
						{
							return num | 16U;
						}
						if (tokenType == TokenType.ExportedType)
						{
							return num | 17U;
						}
					}
					else
					{
						if (tokenType == TokenType.ManifestResource)
						{
							return num | 18U;
						}
						if (tokenType == TokenType.GenericParam)
						{
							return num | 19U;
						}
					}
				}
				else if (tokenType <= TokenType.Document)
				{
					if (tokenType == TokenType.MethodSpec)
					{
						return num | 21U;
					}
					if (tokenType == TokenType.GenericParamConstraint)
					{
						return num | 20U;
					}
					if (tokenType == TokenType.Document)
					{
						return num | 22U;
					}
				}
				else if (tokenType <= TokenType.LocalVariable)
				{
					if (tokenType == TokenType.LocalScope)
					{
						return num | 23U;
					}
					if (tokenType == TokenType.LocalVariable)
					{
						return num | 24U;
					}
				}
				else
				{
					if (tokenType == TokenType.LocalConstant)
					{
						return num | 25U;
					}
					if (tokenType == TokenType.ImportScope)
					{
						return num | 26U;
					}
				}
				break;
			}
			}
			throw new ArgumentException();
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00012AA4 File Offset: 0x00010CA4
		public static int GetSize(this CodedIndex self, Func<Table, int> counter)
		{
			int num;
			Table[] array;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				num = 2;
				array = new Table[]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasConstant:
				num = 2;
				array = new Table[]
				{
					Table.Field,
					Table.Param,
					Table.Property
				};
				break;
			case CodedIndex.HasCustomAttribute:
				num = 5;
				array = new Table[]
				{
					Table.Method,
					Table.Field,
					Table.TypeRef,
					Table.TypeDef,
					Table.Param,
					Table.InterfaceImpl,
					Table.MemberRef,
					Table.Module,
					Table.DeclSecurity,
					Table.Property,
					Table.Event,
					Table.StandAloneSig,
					Table.ModuleRef,
					Table.TypeSpec,
					Table.Assembly,
					Table.AssemblyRef,
					Table.File,
					Table.ExportedType,
					Table.ManifestResource,
					Table.GenericParam,
					Table.GenericParamConstraint,
					Table.MethodSpec
				};
				break;
			case CodedIndex.HasFieldMarshal:
				num = 1;
				array = new Table[]
				{
					Table.Field,
					Table.Param
				};
				break;
			case CodedIndex.HasDeclSecurity:
				num = 2;
				array = new Table[]
				{
					Table.TypeDef,
					Table.Method,
					Table.Assembly
				};
				break;
			case CodedIndex.MemberRefParent:
				num = 3;
				array = new Table[]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.ModuleRef,
					Table.Method,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasSemantics:
				num = 1;
				array = new Table[]
				{
					Table.Event,
					Table.Property
				};
				break;
			case CodedIndex.MethodDefOrRef:
				num = 1;
				array = new Table[]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.MemberForwarded:
				num = 1;
				array = new Table[]
				{
					Table.Field,
					Table.Method
				};
				break;
			case CodedIndex.Implementation:
				num = 2;
				array = new Table[]
				{
					Table.File,
					Table.AssemblyRef,
					Table.ExportedType
				};
				break;
			case CodedIndex.CustomAttributeType:
				num = 3;
				array = new Table[]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.ResolutionScope:
				num = 2;
				array = new Table[]
				{
					Table.Module,
					Table.ModuleRef,
					Table.AssemblyRef,
					Table.TypeRef
				};
				break;
			case CodedIndex.TypeOrMethodDef:
				num = 1;
				array = new Table[]
				{
					Table.TypeDef,
					Table.Method
				};
				break;
			case CodedIndex.HasCustomDebugInformation:
				num = 5;
				array = new Table[]
				{
					Table.Method,
					Table.Field,
					Table.TypeRef,
					Table.TypeDef,
					Table.Param,
					Table.InterfaceImpl,
					Table.MemberRef,
					Table.Module,
					Table.DeclSecurity,
					Table.Property,
					Table.Event,
					Table.StandAloneSig,
					Table.ModuleRef,
					Table.TypeSpec,
					Table.Assembly,
					Table.AssemblyRef,
					Table.File,
					Table.ExportedType,
					Table.ManifestResource,
					Table.GenericParam,
					Table.GenericParamConstraint,
					Table.MethodSpec,
					Table.Document,
					Table.LocalScope,
					Table.LocalVariable,
					Table.LocalConstant,
					Table.ImportScope
				};
				break;
			default:
				throw new ArgumentException();
			}
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num2 = Math.Max(counter.Invoke(array[i]), num2);
			}
			if (num2 >= 1 << 16 - num)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00012D7C File Offset: 0x00010F7C
		public static RSA CreateRSA(this StrongNameKeyPair key_pair)
		{
			byte[] array;
			string text;
			if (!Mixin.TryGetKeyContainer(key_pair, out array, out text))
			{
				return CryptoConvert.FromCapiKeyBlob(array);
			}
			return new RSACryptoServiceProvider(new CspParameters
			{
				Flags = 1,
				KeyContainerName = text,
				KeyNumber = 2
			});
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00012DBC File Offset: 0x00010FBC
		private static bool TryGetKeyContainer(ISerializable key_pair, out byte[] key, out string key_container)
		{
			SerializationInfo serializationInfo = new SerializationInfo(typeof(StrongNameKeyPair), new FormatterConverter());
			key_pair.GetObjectData(serializationInfo, default(StreamingContext));
			key = (byte[])serializationInfo.GetValue("_keyPairArray", typeof(byte[]));
			key_container = serializationInfo.GetString("_keyPairContainer");
			return key_container != null;
		}

		// Token: 0x04000197 RID: 407
		public static Version ZeroVersion = new Version(0, 0, 0, 0);

		// Token: 0x04000198 RID: 408
		public const int NotResolvedMarker = -2;

		// Token: 0x04000199 RID: 409
		public const int NoDataMarker = -1;

		// Token: 0x0400019A RID: 410
		internal static object NoValue = new object();

		// Token: 0x0400019B RID: 411
		internal static object NotResolved = new object();

		// Token: 0x0400019C RID: 412
		public const string mscorlib = "mscorlib";

		// Token: 0x0400019D RID: 413
		public const string system_runtime = "System.Runtime";

		// Token: 0x0400019E RID: 414
		public const string system_private_corelib = "System.Private.CoreLib";

		// Token: 0x0400019F RID: 415
		public const string netstandard = "netstandard";

		// Token: 0x040001A0 RID: 416
		public const int TableCount = 58;

		// Token: 0x040001A1 RID: 417
		public const int CodedIndexCount = 14;

		// Token: 0x02000141 RID: 321
		public enum Argument
		{
			// Token: 0x04000701 RID: 1793
			name,
			// Token: 0x04000702 RID: 1794
			fileName,
			// Token: 0x04000703 RID: 1795
			fullName,
			// Token: 0x04000704 RID: 1796
			stream,
			// Token: 0x04000705 RID: 1797
			type,
			// Token: 0x04000706 RID: 1798
			method,
			// Token: 0x04000707 RID: 1799
			field,
			// Token: 0x04000708 RID: 1800
			parameters,
			// Token: 0x04000709 RID: 1801
			module,
			// Token: 0x0400070A RID: 1802
			modifierType,
			// Token: 0x0400070B RID: 1803
			eventType,
			// Token: 0x0400070C RID: 1804
			fieldType,
			// Token: 0x0400070D RID: 1805
			declaringType,
			// Token: 0x0400070E RID: 1806
			returnType,
			// Token: 0x0400070F RID: 1807
			propertyType,
			// Token: 0x04000710 RID: 1808
			interfaceType
		}
	}
}
