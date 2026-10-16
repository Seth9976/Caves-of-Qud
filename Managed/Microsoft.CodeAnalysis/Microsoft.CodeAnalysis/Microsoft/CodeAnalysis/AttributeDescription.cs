using System;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000214 RID: 532
	[NullableContext(1)]
	[Nullable(0)]
	internal struct AttributeDescription
	{
		// Token: 0x060014C0 RID: 5312 RVA: 0x00041549 File Offset: 0x0003F749
		public AttributeDescription(string @namespace, string name, byte[][] signatures, bool matchIgnoringCase = false)
		{
			this.Namespace = @namespace;
			this.Name = name;
			this.Signatures = signatures;
			this.MatchIgnoringCase = matchIgnoringCase;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x00041568 File Offset: 0x0003F768
		public string FullName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00041580 File Offset: 0x0003F780
		public override string ToString()
		{
			return this.FullName + "(" + this.Signatures.Length.ToString() + ")";
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000415B2 File Offset: 0x0003F7B2
		internal int GetParameterCount(int signatureIndex)
		{
			return (int)this.Signatures[signatureIndex][1];
		}

		// Token: 0x0400079C RID: 1948
		public readonly string Namespace;

		// Token: 0x0400079D RID: 1949
		public readonly string Name;

		// Token: 0x0400079E RID: 1950
		public readonly byte[][] Signatures;

		// Token: 0x0400079F RID: 1951
		public readonly bool MatchIgnoringCase;

		// Token: 0x040007A0 RID: 1952
		private const byte Void = 1;

		// Token: 0x040007A1 RID: 1953
		private const byte Boolean = 2;

		// Token: 0x040007A2 RID: 1954
		private const byte Char = 3;

		// Token: 0x040007A3 RID: 1955
		private const byte SByte = 4;

		// Token: 0x040007A4 RID: 1956
		private const byte Byte = 5;

		// Token: 0x040007A5 RID: 1957
		private const byte Int16 = 6;

		// Token: 0x040007A6 RID: 1958
		private const byte UInt16 = 7;

		// Token: 0x040007A7 RID: 1959
		private const byte Int32 = 8;

		// Token: 0x040007A8 RID: 1960
		private const byte UInt32 = 9;

		// Token: 0x040007A9 RID: 1961
		private const byte Int64 = 10;

		// Token: 0x040007AA RID: 1962
		private const byte UInt64 = 11;

		// Token: 0x040007AB RID: 1963
		private const byte Single = 12;

		// Token: 0x040007AC RID: 1964
		private const byte Double = 13;

		// Token: 0x040007AD RID: 1965
		private const byte String = 14;

		// Token: 0x040007AE RID: 1966
		private const byte Object = 28;

		// Token: 0x040007AF RID: 1967
		private const byte SzArray = 29;

		// Token: 0x040007B0 RID: 1968
		private const byte TypeHandle = 64;

		// Token: 0x040007B1 RID: 1969
		[Nullable(0)]
		internal static ImmutableArray<AttributeDescription.TypeHandleTargetInfo> TypeHandleTargets = new AttributeDescription.TypeHandleTargetInfo[]
		{
			new AttributeDescription.TypeHandleTargetInfo("System", "AttributeTargets", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Reflection", "AssemblyNameFlags", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.CompilerServices", "MethodImplOptions", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "CharSet", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "LayoutKind", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "UnmanagedType", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "TypeLibTypeFlags", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "ClassInterfaceType", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "ComInterfaceType", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.CompilerServices", "CompilationRelaxations", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Diagnostics.DebuggableAttribute", "DebuggingModes", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Security", "SecurityCriticalScope", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Runtime.InteropServices", "CallingConvention", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Configuration.Assemblies", "AssemblyHashAlgorithm", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.EnterpriseServices", "TransactionOption", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System.Security.Permissions", "SecurityAction", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("System", "Type", SerializationTypeCode.Type),
			new AttributeDescription.TypeHandleTargetInfo("Windows.Foundation.Metadata", "DeprecationType", SerializationTypeCode.Int32),
			new AttributeDescription.TypeHandleTargetInfo("Windows.Foundation.Metadata", "Platform", SerializationTypeCode.Int32)
		}.AsImmutable<AttributeDescription.TypeHandleTargetInfo>();

		// Token: 0x040007B2 RID: 1970
		private static readonly byte[] s_signature_HasThis_Void = new byte[]
		{
			32,
			default(byte),
			1
		};

		// Token: 0x040007B3 RID: 1971
		private static readonly byte[] s_signature_HasThis_Void_Byte = new byte[] { 32, 1, 1, 5 };

		// Token: 0x040007B4 RID: 1972
		private static readonly byte[] s_signature_HasThis_Void_Int16 = new byte[] { 32, 1, 1, 6 };

		// Token: 0x040007B5 RID: 1973
		private static readonly byte[] s_signature_HasThis_Void_Int32 = new byte[] { 32, 1, 1, 8 };

		// Token: 0x040007B6 RID: 1974
		private static readonly byte[] s_signature_HasThis_Void_UInt32 = new byte[] { 32, 1, 1, 9 };

		// Token: 0x040007B7 RID: 1975
		private static readonly byte[] s_signature_HasThis_Void_Int32_Int32 = new byte[] { 32, 2, 1, 8, 8 };

		// Token: 0x040007B8 RID: 1976
		private static readonly byte[] s_signature_HasThis_Void_Int32_Int32_Int32_Int32 = new byte[] { 32, 4, 1, 8, 8, 8, 8 };

		// Token: 0x040007B9 RID: 1977
		private static readonly byte[] s_signature_HasThis_Void_String = new byte[] { 32, 1, 1, 14 };

		// Token: 0x040007BA RID: 1978
		private static readonly byte[] s_signature_HasThis_Void_Object = new byte[] { 32, 1, 1, 28 };

		// Token: 0x040007BB RID: 1979
		private static readonly byte[] s_signature_HasThis_Void_String_String = new byte[] { 32, 2, 1, 14, 14 };

		// Token: 0x040007BC RID: 1980
		private static readonly byte[] s_signature_HasThis_Void_String_Boolean = new byte[] { 32, 2, 1, 14, 2 };

		// Token: 0x040007BD RID: 1981
		private static readonly byte[] s_signature_HasThis_Void_String_String_String = new byte[] { 32, 3, 1, 14, 14, 14 };

		// Token: 0x040007BE RID: 1982
		private static readonly byte[] s_signature_HasThis_Void_String_String_String_String = new byte[] { 32, 4, 1, 14, 14, 14, 14 };

		// Token: 0x040007BF RID: 1983
		private static readonly byte[] s_signature_HasThis_Void_AttributeTargets = new byte[] { 32, 1, 1, 64, 0 };

		// Token: 0x040007C0 RID: 1984
		private static readonly byte[] s_signature_HasThis_Void_AssemblyNameFlags = new byte[] { 32, 1, 1, 64, 1 };

		// Token: 0x040007C1 RID: 1985
		private static readonly byte[] s_signature_HasThis_Void_MethodImplOptions = new byte[] { 32, 1, 1, 64, 2 };

		// Token: 0x040007C2 RID: 1986
		private static readonly byte[] s_signature_HasThis_Void_CharSet = new byte[] { 32, 1, 1, 64, 3 };

		// Token: 0x040007C3 RID: 1987
		private static readonly byte[] s_signature_HasThis_Void_LayoutKind = new byte[] { 32, 1, 1, 64, 4 };

		// Token: 0x040007C4 RID: 1988
		private static readonly byte[] s_signature_HasThis_Void_UnmanagedType = new byte[] { 32, 1, 1, 64, 5 };

		// Token: 0x040007C5 RID: 1989
		private static readonly byte[] s_signature_HasThis_Void_TypeLibTypeFlags = new byte[] { 32, 1, 1, 64, 6 };

		// Token: 0x040007C6 RID: 1990
		private static readonly byte[] s_signature_HasThis_Void_ClassInterfaceType = new byte[] { 32, 1, 1, 64, 7 };

		// Token: 0x040007C7 RID: 1991
		private static readonly byte[] s_signature_HasThis_Void_ComInterfaceType = new byte[] { 32, 1, 1, 64, 8 };

		// Token: 0x040007C8 RID: 1992
		private static readonly byte[] s_signature_HasThis_Void_CompilationRelaxations = new byte[] { 32, 1, 1, 64, 9 };

		// Token: 0x040007C9 RID: 1993
		private static readonly byte[] s_signature_HasThis_Void_DebuggingModes = new byte[] { 32, 1, 1, 64, 10 };

		// Token: 0x040007CA RID: 1994
		private static readonly byte[] s_signature_HasThis_Void_SecurityCriticalScope = new byte[] { 32, 1, 1, 64, 11 };

		// Token: 0x040007CB RID: 1995
		private static readonly byte[] s_signature_HasThis_Void_CallingConvention = new byte[] { 32, 1, 1, 64, 12 };

		// Token: 0x040007CC RID: 1996
		private static readonly byte[] s_signature_HasThis_Void_AssemblyHashAlgorithm = new byte[] { 32, 1, 1, 64, 13 };

		// Token: 0x040007CD RID: 1997
		private static readonly byte[] s_signature_HasThis_Void_Int64 = new byte[] { 32, 1, 1, 10 };

		// Token: 0x040007CE RID: 1998
		private static readonly byte[] s_signature_HasThis_Void_UInt8_UInt8_UInt32_UInt32_UInt32 = new byte[] { 32, 5, 1, 5, 5, 9, 9, 9 };

		// Token: 0x040007CF RID: 1999
		private static readonly byte[] s_signature_HasThis_Void_UIn8_UInt8_Int32_Int32_Int32 = new byte[] { 32, 5, 1, 5, 5, 8, 8, 8 };

		// Token: 0x040007D0 RID: 2000
		private static readonly byte[] s_signature_HasThis_Void_Boolean = new byte[] { 32, 1, 1, 2 };

		// Token: 0x040007D1 RID: 2001
		private static readonly byte[] s_signature_HasThis_Void_Boolean_Boolean = new byte[] { 32, 2, 1, 2, 2 };

		// Token: 0x040007D2 RID: 2002
		private static readonly byte[] s_signature_HasThis_Void_Boolean_TransactionOption = new byte[] { 32, 2, 1, 2, 64, 14 };

		// Token: 0x040007D3 RID: 2003
		private static readonly byte[] s_signature_HasThis_Void_Boolean_TransactionOption_Int32 = new byte[] { 32, 3, 1, 2, 64, 14, 8 };

		// Token: 0x040007D4 RID: 2004
		private static readonly byte[] s_signature_HasThis_Void_Boolean_TransactionOption_Int32_Boolean = new byte[] { 32, 4, 1, 2, 64, 14, 8, 2 };

		// Token: 0x040007D5 RID: 2005
		private static readonly byte[] s_signature_HasThis_Void_SecurityAction = new byte[] { 32, 1, 1, 64, 15 };

		// Token: 0x040007D6 RID: 2006
		private static readonly byte[] s_signature_HasThis_Void_Type = new byte[] { 32, 1, 1, 64, 16 };

		// Token: 0x040007D7 RID: 2007
		private static readonly byte[] s_signature_HasThis_Void_Type_Type = new byte[] { 32, 2, 1, 64, 16, 64, 16 };

		// Token: 0x040007D8 RID: 2008
		private static readonly byte[] s_signature_HasThis_Void_Type_Type_Type = new byte[] { 32, 3, 1, 64, 16, 64, 16, 64, 16 };

		// Token: 0x040007D9 RID: 2009
		private static readonly byte[] s_signature_HasThis_Void_Type_Type_Type_Type = new byte[]
		{
			32, 4, 1, 64, 16, 64, 16, 64, 16, 64,
			16
		};

		// Token: 0x040007DA RID: 2010
		private static readonly byte[] s_signature_HasThis_Void_Type_Int32 = new byte[] { 32, 2, 1, 64, 16, 8 };

		// Token: 0x040007DB RID: 2011
		private static readonly byte[] s_signature_HasThis_Void_SzArray_Boolean = new byte[] { 32, 1, 1, 29, 2 };

		// Token: 0x040007DC RID: 2012
		private static readonly byte[] s_signature_HasThis_Void_SzArray_Byte = new byte[] { 32, 1, 1, 29, 5 };

		// Token: 0x040007DD RID: 2013
		private static readonly byte[] s_signature_HasThis_Void_SzArray_String = new byte[] { 32, 1, 1, 29, 14 };

		// Token: 0x040007DE RID: 2014
		private static readonly byte[] s_signature_HasThis_Void_Boolean_SzArray_String = new byte[] { 32, 2, 1, 2, 29, 14 };

		// Token: 0x040007DF RID: 2015
		private static readonly byte[] s_signature_HasThis_Void_Boolean_String = new byte[] { 32, 2, 1, 2, 14 };

		// Token: 0x040007E0 RID: 2016
		private static readonly byte[] s_signature_HasThis_Void_String_DeprecationType_UInt32 = new byte[] { 32, 3, 1, 14, 64, 17, 9 };

		// Token: 0x040007E1 RID: 2017
		private static readonly byte[] s_signature_HasThis_Void_String_DeprecationType_UInt32_Platform = new byte[] { 32, 4, 1, 14, 64, 17, 9, 64, 18 };

		// Token: 0x040007E2 RID: 2018
		private static readonly byte[] s_signature_HasThis_Void_String_DeprecationType_UInt32_Type = new byte[] { 32, 4, 1, 14, 64, 17, 9, 64, 16 };

		// Token: 0x040007E3 RID: 2019
		private static readonly byte[] s_signature_HasThis_Void_String_DeprecationType_UInt32_String = new byte[] { 32, 4, 1, 14, 64, 17, 9, 14 };

		// Token: 0x040007E4 RID: 2020
		private static readonly byte[][] s_signatures_HasThis_Void_Only = new byte[][] { AttributeDescription.s_signature_HasThis_Void };

		// Token: 0x040007E5 RID: 2021
		private static readonly byte[][] s_signatures_HasThis_Void_String_Only = new byte[][] { AttributeDescription.s_signature_HasThis_Void_String };

		// Token: 0x040007E6 RID: 2022
		private static readonly byte[][] s_signatures_HasThis_Void_Type_Only = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Type };

		// Token: 0x040007E7 RID: 2023
		private static readonly byte[][] s_signatures_HasThis_Void_Boolean_Only = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Boolean };

		// Token: 0x040007E8 RID: 2024
		private static readonly byte[][] s_signaturesOfTypeIdentifierAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_String_String
		};

		// Token: 0x040007E9 RID: 2025
		private static readonly byte[][] s_signaturesOfAttributeUsage = new byte[][] { AttributeDescription.s_signature_HasThis_Void_AttributeTargets };

		// Token: 0x040007EA RID: 2026
		private static readonly byte[][] s_signaturesOfAssemblySignatureKeyAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_String_String };

		// Token: 0x040007EB RID: 2027
		private static readonly byte[][] s_signaturesOfAssemblyFlagsAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_AssemblyNameFlags,
			AttributeDescription.s_signature_HasThis_Void_Int32,
			AttributeDescription.s_signature_HasThis_Void_UInt32
		};

		// Token: 0x040007EC RID: 2028
		private static readonly byte[][] s_signaturesOfDefaultParameterValueAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Object };

		// Token: 0x040007ED RID: 2029
		private static readonly byte[][] s_signaturesOfDateTimeConstantAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int64 };

		// Token: 0x040007EE RID: 2030
		private static readonly byte[][] s_signaturesOfDecimalConstantAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_UInt8_UInt8_UInt32_UInt32_UInt32,
			AttributeDescription.s_signature_HasThis_Void_UIn8_UInt8_Int32_Int32_Int32
		};

		// Token: 0x040007EF RID: 2031
		private static readonly byte[][] s_signaturesOfSecurityPermissionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_SecurityAction };

		// Token: 0x040007F0 RID: 2032
		private static readonly byte[][] s_signaturesOfMethodImplAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_MethodImplOptions
		};

		// Token: 0x040007F1 RID: 2033
		private static readonly byte[][] s_signaturesOfDefaultCharSetAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_CharSet };

		// Token: 0x040007F2 RID: 2034
		private static readonly byte[][] s_signaturesOfFieldOffsetAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32 };

		// Token: 0x040007F3 RID: 2035
		private static readonly byte[][] s_signaturesOfMemberNotNullAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_String,
			AttributeDescription.s_signature_HasThis_Void_SzArray_String
		};

		// Token: 0x040007F4 RID: 2036
		private static readonly byte[][] s_signaturesOfMemberNotNullWhenAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Boolean_String,
			AttributeDescription.s_signature_HasThis_Void_Boolean_SzArray_String
		};

		// Token: 0x040007F5 RID: 2037
		private static readonly byte[][] s_signaturesOfFixedBufferAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Type_Int32 };

		// Token: 0x040007F6 RID: 2038
		private static readonly byte[][] s_signaturesOfPrincipalPermissionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_SecurityAction };

		// Token: 0x040007F7 RID: 2039
		private static readonly byte[][] s_signaturesOfPermissionSetAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_SecurityAction };

		// Token: 0x040007F8 RID: 2040
		private static readonly byte[][] s_signaturesOfStructLayoutAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_LayoutKind
		};

		// Token: 0x040007F9 RID: 2041
		private static readonly byte[][] s_signaturesOfMarshalAsAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_UnmanagedType
		};

		// Token: 0x040007FA RID: 2042
		private static readonly byte[][] s_signaturesOfTypeLibTypeAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_TypeLibTypeFlags
		};

		// Token: 0x040007FB RID: 2043
		private static readonly byte[][] s_signaturesOfWebMethodAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_Boolean,
			AttributeDescription.s_signature_HasThis_Void_Boolean_TransactionOption,
			AttributeDescription.s_signature_HasThis_Void_Boolean_TransactionOption_Int32,
			AttributeDescription.s_signature_HasThis_Void_Boolean_TransactionOption_Int32_Boolean
		};

		// Token: 0x040007FC RID: 2044
		private static readonly byte[][] s_signaturesOfHostProtectionAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_SecurityAction
		};

		// Token: 0x040007FD RID: 2045
		private static readonly byte[][] s_signaturesOfVisualBasicComClassAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_String,
			AttributeDescription.s_signature_HasThis_Void_String_String,
			AttributeDescription.s_signature_HasThis_Void_String_String_String
		};

		// Token: 0x040007FE RID: 2046
		private static readonly byte[][] s_signaturesOfClassInterfaceAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_ClassInterfaceType
		};

		// Token: 0x040007FF RID: 2047
		private static readonly byte[][] s_signaturesOfInterfaceTypeAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int16,
			AttributeDescription.s_signature_HasThis_Void_ComInterfaceType
		};

		// Token: 0x04000800 RID: 2048
		private static readonly byte[][] s_signaturesOfCompilationRelaxationsAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Int32,
			AttributeDescription.s_signature_HasThis_Void_CompilationRelaxations
		};

		// Token: 0x04000801 RID: 2049
		private static readonly byte[][] s_signaturesOfDebuggableAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Boolean_Boolean,
			AttributeDescription.s_signature_HasThis_Void_DebuggingModes
		};

		// Token: 0x04000802 RID: 2050
		private static readonly byte[][] s_signaturesOfComSourceInterfacesAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_String,
			AttributeDescription.s_signature_HasThis_Void_Type,
			AttributeDescription.s_signature_HasThis_Void_Type_Type,
			AttributeDescription.s_signature_HasThis_Void_Type_Type_Type,
			AttributeDescription.s_signature_HasThis_Void_Type_Type_Type_Type
		};

		// Token: 0x04000803 RID: 2051
		private static readonly byte[][] s_signaturesOfTypeLibVersionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32_Int32 };

		// Token: 0x04000804 RID: 2052
		private static readonly byte[][] s_signaturesOfComCompatibleVersionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32_Int32_Int32_Int32 };

		// Token: 0x04000805 RID: 2053
		private static readonly byte[][] s_signaturesOfObsoleteAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_String,
			AttributeDescription.s_signature_HasThis_Void_String_Boolean
		};

		// Token: 0x04000806 RID: 2054
		private static readonly byte[][] s_signaturesOfDynamicAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_SzArray_Boolean
		};

		// Token: 0x04000807 RID: 2055
		private static readonly byte[][] s_signaturesOfTupleElementNamesAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_SzArray_String
		};

		// Token: 0x04000808 RID: 2056
		private static readonly byte[][] s_signaturesOfSecurityCriticalAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_SecurityCriticalScope
		};

		// Token: 0x04000809 RID: 2057
		private static readonly byte[][] s_signaturesOfMyGroupCollectionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_String_String_String_String };

		// Token: 0x0400080A RID: 2058
		private static readonly byte[][] s_signaturesOfComEventInterfaceAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Type_Type };

		// Token: 0x0400080B RID: 2059
		private static readonly byte[][] s_signaturesOfLCIDConversionAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32 };

		// Token: 0x0400080C RID: 2060
		private static readonly byte[][] s_signaturesOfUnmanagedFunctionPointerAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_CallingConvention };

		// Token: 0x0400080D RID: 2061
		private static readonly byte[][] s_signaturesOfPrimaryInteropAssemblyAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32_Int32 };

		// Token: 0x0400080E RID: 2062
		private static readonly byte[][] s_signaturesOfAssemblyAlgorithmIdAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_AssemblyHashAlgorithm,
			AttributeDescription.s_signature_HasThis_Void_UInt32
		};

		// Token: 0x0400080F RID: 2063
		private static readonly byte[][] s_signaturesOfDeprecatedAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_String_DeprecationType_UInt32,
			AttributeDescription.s_signature_HasThis_Void_String_DeprecationType_UInt32_Platform,
			AttributeDescription.s_signature_HasThis_Void_String_DeprecationType_UInt32_Type,
			AttributeDescription.s_signature_HasThis_Void_String_DeprecationType_UInt32_String
		};

		// Token: 0x04000810 RID: 2064
		private static readonly byte[][] s_signaturesOfNullableAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void_Byte,
			AttributeDescription.s_signature_HasThis_Void_SzArray_Byte
		};

		// Token: 0x04000811 RID: 2065
		private static readonly byte[][] s_signaturesOfNullableContextAttribute = new byte[][] { AttributeDescription.s_signature_HasThis_Void_Byte };

		// Token: 0x04000812 RID: 2066
		private static readonly byte[][] s_signaturesOfNativeIntegerAttribute = new byte[][]
		{
			AttributeDescription.s_signature_HasThis_Void,
			AttributeDescription.s_signature_HasThis_Void_SzArray_Boolean
		};

		// Token: 0x04000813 RID: 2067
		internal static readonly AttributeDescription OptionalAttribute = new AttributeDescription("System.Runtime.InteropServices", "OptionalAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000814 RID: 2068
		internal static readonly AttributeDescription ComImportAttribute = new AttributeDescription("System.Runtime.InteropServices", "ComImportAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000815 RID: 2069
		internal static readonly AttributeDescription AttributeUsageAttribute = new AttributeDescription("System", "AttributeUsageAttribute", AttributeDescription.s_signaturesOfAttributeUsage, false);

		// Token: 0x04000816 RID: 2070
		internal static readonly AttributeDescription ConditionalAttribute = new AttributeDescription("System.Diagnostics", "ConditionalAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000817 RID: 2071
		internal static readonly AttributeDescription CaseInsensitiveExtensionAttribute = new AttributeDescription("System.Runtime.CompilerServices", "ExtensionAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, true);

		// Token: 0x04000818 RID: 2072
		internal static readonly AttributeDescription CaseSensitiveExtensionAttribute = new AttributeDescription("System.Runtime.CompilerServices", "ExtensionAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000819 RID: 2073
		internal static readonly AttributeDescription InternalsVisibleToAttribute = new AttributeDescription("System.Runtime.CompilerServices", "InternalsVisibleToAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400081A RID: 2074
		internal static readonly AttributeDescription AssemblySignatureKeyAttribute = new AttributeDescription("System.Reflection", "AssemblySignatureKeyAttribute", AttributeDescription.s_signaturesOfAssemblySignatureKeyAttribute, false);

		// Token: 0x0400081B RID: 2075
		internal static readonly AttributeDescription AssemblyKeyFileAttribute = new AttributeDescription("System.Reflection", "AssemblyKeyFileAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400081C RID: 2076
		internal static readonly AttributeDescription AssemblyKeyNameAttribute = new AttributeDescription("System.Reflection", "AssemblyKeyNameAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400081D RID: 2077
		internal static readonly AttributeDescription ParamArrayAttribute = new AttributeDescription("System", "ParamArrayAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400081E RID: 2078
		internal static readonly AttributeDescription DefaultMemberAttribute = new AttributeDescription("System.Reflection", "DefaultMemberAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400081F RID: 2079
		internal static readonly AttributeDescription IndexerNameAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IndexerNameAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000820 RID: 2080
		internal static readonly AttributeDescription AssemblyDelaySignAttribute = new AttributeDescription("System.Reflection", "AssemblyDelaySignAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000821 RID: 2081
		internal static readonly AttributeDescription AssemblyVersionAttribute = new AttributeDescription("System.Reflection", "AssemblyVersionAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000822 RID: 2082
		internal static readonly AttributeDescription AssemblyFileVersionAttribute = new AttributeDescription("System.Reflection", "AssemblyFileVersionAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000823 RID: 2083
		internal static readonly AttributeDescription AssemblyTitleAttribute = new AttributeDescription("System.Reflection", "AssemblyTitleAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000824 RID: 2084
		internal static readonly AttributeDescription AssemblyDescriptionAttribute = new AttributeDescription("System.Reflection", "AssemblyDescriptionAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000825 RID: 2085
		internal static readonly AttributeDescription AssemblyCultureAttribute = new AttributeDescription("System.Reflection", "AssemblyCultureAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000826 RID: 2086
		internal static readonly AttributeDescription AssemblyCompanyAttribute = new AttributeDescription("System.Reflection", "AssemblyCompanyAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000827 RID: 2087
		internal static readonly AttributeDescription AssemblyProductAttribute = new AttributeDescription("System.Reflection", "AssemblyProductAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000828 RID: 2088
		internal static readonly AttributeDescription AssemblyInformationalVersionAttribute = new AttributeDescription("System.Reflection", "AssemblyInformationalVersionAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000829 RID: 2089
		internal static readonly AttributeDescription AssemblyCopyrightAttribute = new AttributeDescription("System.Reflection", "AssemblyCopyrightAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400082A RID: 2090
		internal static readonly AttributeDescription SatelliteContractVersionAttribute = new AttributeDescription("System.Resources", "SatelliteContractVersionAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400082B RID: 2091
		internal static readonly AttributeDescription AssemblyTrademarkAttribute = new AttributeDescription("System.Reflection", "AssemblyTrademarkAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400082C RID: 2092
		internal static readonly AttributeDescription AssemblyFlagsAttribute = new AttributeDescription("System.Reflection", "AssemblyFlagsAttribute", AttributeDescription.s_signaturesOfAssemblyFlagsAttribute, false);

		// Token: 0x0400082D RID: 2093
		internal static readonly AttributeDescription DecimalConstantAttribute = new AttributeDescription("System.Runtime.CompilerServices", "DecimalConstantAttribute", AttributeDescription.s_signaturesOfDecimalConstantAttribute, false);

		// Token: 0x0400082E RID: 2094
		internal static readonly AttributeDescription IUnknownConstantAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IUnknownConstantAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400082F RID: 2095
		internal static readonly AttributeDescription CallerFilePathAttribute = new AttributeDescription("System.Runtime.CompilerServices", "CallerFilePathAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000830 RID: 2096
		internal static readonly AttributeDescription CallerLineNumberAttribute = new AttributeDescription("System.Runtime.CompilerServices", "CallerLineNumberAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000831 RID: 2097
		internal static readonly AttributeDescription CallerMemberNameAttribute = new AttributeDescription("System.Runtime.CompilerServices", "CallerMemberNameAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000832 RID: 2098
		internal static readonly AttributeDescription IDispatchConstantAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IDispatchConstantAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000833 RID: 2099
		internal static readonly AttributeDescription DefaultParameterValueAttribute = new AttributeDescription("System.Runtime.InteropServices", "DefaultParameterValueAttribute", AttributeDescription.s_signaturesOfDefaultParameterValueAttribute, false);

		// Token: 0x04000834 RID: 2100
		internal static readonly AttributeDescription UnverifiableCodeAttribute = new AttributeDescription("System.Runtime.InteropServices", "UnverifiableCodeAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000835 RID: 2101
		internal static readonly AttributeDescription SecurityPermissionAttribute = new AttributeDescription("System.Runtime.InteropServices", "SecurityPermissionAttribute", AttributeDescription.s_signaturesOfSecurityPermissionAttribute, false);

		// Token: 0x04000836 RID: 2102
		internal static readonly AttributeDescription DllImportAttribute = new AttributeDescription("System.Runtime.InteropServices", "DllImportAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000837 RID: 2103
		internal static readonly AttributeDescription MethodImplAttribute = new AttributeDescription("System.Runtime.CompilerServices", "MethodImplAttribute", AttributeDescription.s_signaturesOfMethodImplAttribute, false);

		// Token: 0x04000838 RID: 2104
		internal static readonly AttributeDescription PreserveSigAttribute = new AttributeDescription("System.Runtime.InteropServices", "PreserveSigAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000839 RID: 2105
		internal static readonly AttributeDescription DefaultCharSetAttribute = new AttributeDescription("System.Runtime.InteropServices", "DefaultCharSetAttribute", AttributeDescription.s_signaturesOfDefaultCharSetAttribute, false);

		// Token: 0x0400083A RID: 2106
		internal static readonly AttributeDescription SpecialNameAttribute = new AttributeDescription("System.Runtime.CompilerServices", "SpecialNameAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400083B RID: 2107
		internal static readonly AttributeDescription SerializableAttribute = new AttributeDescription("System", "SerializableAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400083C RID: 2108
		internal static readonly AttributeDescription NonSerializedAttribute = new AttributeDescription("System", "NonSerializedAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400083D RID: 2109
		internal static readonly AttributeDescription StructLayoutAttribute = new AttributeDescription("System.Runtime.InteropServices", "StructLayoutAttribute", AttributeDescription.s_signaturesOfStructLayoutAttribute, false);

		// Token: 0x0400083E RID: 2110
		internal static readonly AttributeDescription FieldOffsetAttribute = new AttributeDescription("System.Runtime.InteropServices", "FieldOffsetAttribute", AttributeDescription.s_signaturesOfFieldOffsetAttribute, false);

		// Token: 0x0400083F RID: 2111
		internal static readonly AttributeDescription FixedBufferAttribute = new AttributeDescription("System.Runtime.CompilerServices", "FixedBufferAttribute", AttributeDescription.s_signaturesOfFixedBufferAttribute, false);

		// Token: 0x04000840 RID: 2112
		internal static readonly AttributeDescription AllowNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "AllowNullAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000841 RID: 2113
		internal static readonly AttributeDescription DisallowNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "DisallowNullAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000842 RID: 2114
		internal static readonly AttributeDescription MaybeNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "MaybeNullAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000843 RID: 2115
		internal static readonly AttributeDescription MaybeNullWhenAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "MaybeNullWhenAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000844 RID: 2116
		internal static readonly AttributeDescription NotNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "NotNullAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000845 RID: 2117
		internal static readonly AttributeDescription MemberNotNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "MemberNotNullAttribute", AttributeDescription.s_signaturesOfMemberNotNullAttribute, false);

		// Token: 0x04000846 RID: 2118
		internal static readonly AttributeDescription MemberNotNullWhenAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "MemberNotNullWhenAttribute", AttributeDescription.s_signaturesOfMemberNotNullWhenAttribute, false);

		// Token: 0x04000847 RID: 2119
		internal static readonly AttributeDescription NotNullIfNotNullAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "NotNullIfNotNullAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000848 RID: 2120
		internal static readonly AttributeDescription NotNullWhenAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "NotNullWhenAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000849 RID: 2121
		internal static readonly AttributeDescription DoesNotReturnIfAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "DoesNotReturnIfAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x0400084A RID: 2122
		internal static readonly AttributeDescription DoesNotReturnAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "DoesNotReturnAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400084B RID: 2123
		internal static readonly AttributeDescription MarshalAsAttribute = new AttributeDescription("System.Runtime.InteropServices", "MarshalAsAttribute", AttributeDescription.s_signaturesOfMarshalAsAttribute, false);

		// Token: 0x0400084C RID: 2124
		internal static readonly AttributeDescription InAttribute = new AttributeDescription("System.Runtime.InteropServices", "InAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400084D RID: 2125
		internal static readonly AttributeDescription OutAttribute = new AttributeDescription("System.Runtime.InteropServices", "OutAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400084E RID: 2126
		internal static readonly AttributeDescription IsReadOnlyAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IsReadOnlyAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400084F RID: 2127
		internal static readonly AttributeDescription IsUnmanagedAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IsUnmanagedAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000850 RID: 2128
		internal static readonly AttributeDescription CoClassAttribute = new AttributeDescription("System.Runtime.InteropServices", "CoClassAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x04000851 RID: 2129
		internal static readonly AttributeDescription GuidAttribute = new AttributeDescription("System.Runtime.InteropServices", "GuidAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000852 RID: 2130
		internal static readonly AttributeDescription CLSCompliantAttribute = new AttributeDescription("System", "CLSCompliantAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000853 RID: 2131
		internal static readonly AttributeDescription HostProtectionAttribute = new AttributeDescription("System.Security.Permissions", "HostProtectionAttribute", AttributeDescription.s_signaturesOfHostProtectionAttribute, false);

		// Token: 0x04000854 RID: 2132
		internal static readonly AttributeDescription SuppressUnmanagedCodeSecurityAttribute = new AttributeDescription("System.Security", "SuppressUnmanagedCodeSecurityAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000855 RID: 2133
		internal static readonly AttributeDescription PrincipalPermissionAttribute = new AttributeDescription("System.Security.Permissions", "PrincipalPermissionAttribute", AttributeDescription.s_signaturesOfPrincipalPermissionAttribute, false);

		// Token: 0x04000856 RID: 2134
		internal static readonly AttributeDescription PermissionSetAttribute = new AttributeDescription("System.Security.Permissions", "PermissionSetAttribute", AttributeDescription.s_signaturesOfPermissionSetAttribute, false);

		// Token: 0x04000857 RID: 2135
		internal static readonly AttributeDescription TypeIdentifierAttribute = new AttributeDescription("System.Runtime.InteropServices", "TypeIdentifierAttribute", AttributeDescription.s_signaturesOfTypeIdentifierAttribute, false);

		// Token: 0x04000858 RID: 2136
		internal static readonly AttributeDescription VisualBasicEmbeddedAttribute = new AttributeDescription("Microsoft.VisualBasic", "Embedded", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000859 RID: 2137
		internal static readonly AttributeDescription CodeAnalysisEmbeddedAttribute = new AttributeDescription("Microsoft.CodeAnalysis", "EmbeddedAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400085A RID: 2138
		internal static readonly AttributeDescription VisualBasicComClassAttribute = new AttributeDescription("Microsoft.VisualBasic", "ComClassAttribute", AttributeDescription.s_signaturesOfVisualBasicComClassAttribute, false);

		// Token: 0x0400085B RID: 2139
		internal static readonly AttributeDescription StandardModuleAttribute = new AttributeDescription("Microsoft.VisualBasic.CompilerServices", "StandardModuleAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400085C RID: 2140
		internal static readonly AttributeDescription OptionCompareAttribute = new AttributeDescription("Microsoft.VisualBasic.CompilerServices", "OptionCompareAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400085D RID: 2141
		internal static readonly AttributeDescription AccessedThroughPropertyAttribute = new AttributeDescription("System.Runtime.CompilerServices", "AccessedThroughPropertyAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400085E RID: 2142
		internal static readonly AttributeDescription WebMethodAttribute = new AttributeDescription("System.Web.Services", "WebMethodAttribute", AttributeDescription.s_signaturesOfWebMethodAttribute, false);

		// Token: 0x0400085F RID: 2143
		internal static readonly AttributeDescription DateTimeConstantAttribute = new AttributeDescription("System.Runtime.CompilerServices", "DateTimeConstantAttribute", AttributeDescription.s_signaturesOfDateTimeConstantAttribute, false);

		// Token: 0x04000860 RID: 2144
		internal static readonly AttributeDescription ClassInterfaceAttribute = new AttributeDescription("System.Runtime.InteropServices", "ClassInterfaceAttribute", AttributeDescription.s_signaturesOfClassInterfaceAttribute, false);

		// Token: 0x04000861 RID: 2145
		internal static readonly AttributeDescription ComSourceInterfacesAttribute = new AttributeDescription("System.Runtime.InteropServices", "ComSourceInterfacesAttribute", AttributeDescription.s_signaturesOfComSourceInterfacesAttribute, false);

		// Token: 0x04000862 RID: 2146
		internal static readonly AttributeDescription ComVisibleAttribute = new AttributeDescription("System.Runtime.InteropServices", "ComVisibleAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000863 RID: 2147
		internal static readonly AttributeDescription DispIdAttribute = new AttributeDescription("System.Runtime.InteropServices", "DispIdAttribute", new byte[][] { AttributeDescription.s_signature_HasThis_Void_Int32 }, false);

		// Token: 0x04000864 RID: 2148
		internal static readonly AttributeDescription TypeLibVersionAttribute = new AttributeDescription("System.Runtime.InteropServices", "TypeLibVersionAttribute", AttributeDescription.s_signaturesOfTypeLibVersionAttribute, false);

		// Token: 0x04000865 RID: 2149
		internal static readonly AttributeDescription ComCompatibleVersionAttribute = new AttributeDescription("System.Runtime.InteropServices", "ComCompatibleVersionAttribute", AttributeDescription.s_signaturesOfComCompatibleVersionAttribute, false);

		// Token: 0x04000866 RID: 2150
		internal static readonly AttributeDescription InterfaceTypeAttribute = new AttributeDescription("System.Runtime.InteropServices", "InterfaceTypeAttribute", AttributeDescription.s_signaturesOfInterfaceTypeAttribute, false);

		// Token: 0x04000867 RID: 2151
		internal static readonly AttributeDescription WindowsRuntimeImportAttribute = new AttributeDescription("System.Runtime.InteropServices.WindowsRuntime", "WindowsRuntimeImportAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000868 RID: 2152
		internal static readonly AttributeDescription DynamicSecurityMethodAttribute = new AttributeDescription("System.Security", "DynamicSecurityMethodAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000869 RID: 2153
		internal static readonly AttributeDescription RequiredAttributeAttribute = new AttributeDescription("System.Runtime.CompilerServices", "RequiredAttributeAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x0400086A RID: 2154
		internal static readonly AttributeDescription AsyncMethodBuilderAttribute = new AttributeDescription("System.Runtime.CompilerServices", "AsyncMethodBuilderAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x0400086B RID: 2155
		internal static readonly AttributeDescription AsyncStateMachineAttribute = new AttributeDescription("System.Runtime.CompilerServices", "AsyncStateMachineAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x0400086C RID: 2156
		internal static readonly AttributeDescription IteratorStateMachineAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IteratorStateMachineAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x0400086D RID: 2157
		internal static readonly AttributeDescription CompilationRelaxationsAttribute = new AttributeDescription("System.Runtime.CompilerServices", "CompilationRelaxationsAttribute", AttributeDescription.s_signaturesOfCompilationRelaxationsAttribute, false);

		// Token: 0x0400086E RID: 2158
		internal static readonly AttributeDescription ReferenceAssemblyAttribute = new AttributeDescription("System.Runtime.CompilerServices", "ReferenceAssemblyAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400086F RID: 2159
		internal static readonly AttributeDescription RuntimeCompatibilityAttribute = new AttributeDescription("System.Runtime.CompilerServices", "RuntimeCompatibilityAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000870 RID: 2160
		internal static readonly AttributeDescription DebuggableAttribute = new AttributeDescription("System.Diagnostics", "DebuggableAttribute", AttributeDescription.s_signaturesOfDebuggableAttribute, false);

		// Token: 0x04000871 RID: 2161
		internal static readonly AttributeDescription TypeForwardedToAttribute = new AttributeDescription("System.Runtime.CompilerServices", "TypeForwardedToAttribute", AttributeDescription.s_signatures_HasThis_Void_Type_Only, false);

		// Token: 0x04000872 RID: 2162
		internal static readonly AttributeDescription STAThreadAttribute = new AttributeDescription("System", "STAThreadAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000873 RID: 2163
		internal static readonly AttributeDescription MTAThreadAttribute = new AttributeDescription("System", "MTAThreadAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000874 RID: 2164
		internal static readonly AttributeDescription ObsoleteAttribute = new AttributeDescription("System", "ObsoleteAttribute", AttributeDescription.s_signaturesOfObsoleteAttribute, false);

		// Token: 0x04000875 RID: 2165
		internal static readonly AttributeDescription TypeLibTypeAttribute = new AttributeDescription("System.Runtime.InteropServices", "TypeLibTypeAttribute", AttributeDescription.s_signaturesOfTypeLibTypeAttribute, false);

		// Token: 0x04000876 RID: 2166
		internal static readonly AttributeDescription DynamicAttribute = new AttributeDescription("System.Runtime.CompilerServices", "DynamicAttribute", AttributeDescription.s_signaturesOfDynamicAttribute, false);

		// Token: 0x04000877 RID: 2167
		internal static readonly AttributeDescription TupleElementNamesAttribute = new AttributeDescription("System.Runtime.CompilerServices", "TupleElementNamesAttribute", AttributeDescription.s_signaturesOfTupleElementNamesAttribute, false);

		// Token: 0x04000878 RID: 2168
		internal static readonly AttributeDescription IsByRefLikeAttribute = new AttributeDescription("System.Runtime.CompilerServices", "IsByRefLikeAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000879 RID: 2169
		internal static readonly AttributeDescription DebuggerHiddenAttribute = new AttributeDescription("System.Diagnostics", "DebuggerHiddenAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400087A RID: 2170
		internal static readonly AttributeDescription DebuggerNonUserCodeAttribute = new AttributeDescription("System.Diagnostics", "DebuggerNonUserCodeAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400087B RID: 2171
		internal static readonly AttributeDescription DebuggerStepperBoundaryAttribute = new AttributeDescription("System.Diagnostics", "DebuggerStepperBoundaryAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400087C RID: 2172
		internal static readonly AttributeDescription DebuggerStepThroughAttribute = new AttributeDescription("System.Diagnostics", "DebuggerStepThroughAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400087D RID: 2173
		internal static readonly AttributeDescription SecurityCriticalAttribute = new AttributeDescription("System.Security", "SecurityCriticalAttribute", AttributeDescription.s_signaturesOfSecurityCriticalAttribute, false);

		// Token: 0x0400087E RID: 2174
		internal static readonly AttributeDescription SecuritySafeCriticalAttribute = new AttributeDescription("System.Security", "SecuritySafeCriticalAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x0400087F RID: 2175
		internal static readonly AttributeDescription DesignerGeneratedAttribute = new AttributeDescription("Microsoft.VisualBasic.CompilerServices", "DesignerGeneratedAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000880 RID: 2176
		internal static readonly AttributeDescription MyGroupCollectionAttribute = new AttributeDescription("Microsoft.VisualBasic", "MyGroupCollectionAttribute", AttributeDescription.s_signaturesOfMyGroupCollectionAttribute, false);

		// Token: 0x04000881 RID: 2177
		internal static readonly AttributeDescription ComEventInterfaceAttribute = new AttributeDescription("System.Runtime.InteropServices", "ComEventInterfaceAttribute", AttributeDescription.s_signaturesOfComEventInterfaceAttribute, false);

		// Token: 0x04000882 RID: 2178
		internal static readonly AttributeDescription BestFitMappingAttribute = new AttributeDescription("System.Runtime.InteropServices", "BestFitMappingAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x04000883 RID: 2179
		internal static readonly AttributeDescription FlagsAttribute = new AttributeDescription("System", "FlagsAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000884 RID: 2180
		internal static readonly AttributeDescription LCIDConversionAttribute = new AttributeDescription("System.Runtime.InteropServices", "LCIDConversionAttribute", AttributeDescription.s_signaturesOfLCIDConversionAttribute, false);

		// Token: 0x04000885 RID: 2181
		internal static readonly AttributeDescription UnmanagedFunctionPointerAttribute = new AttributeDescription("System.Runtime.InteropServices", "UnmanagedFunctionPointerAttribute", AttributeDescription.s_signaturesOfUnmanagedFunctionPointerAttribute, false);

		// Token: 0x04000886 RID: 2182
		internal static readonly AttributeDescription PrimaryInteropAssemblyAttribute = new AttributeDescription("System.Runtime.InteropServices", "PrimaryInteropAssemblyAttribute", AttributeDescription.s_signaturesOfPrimaryInteropAssemblyAttribute, false);

		// Token: 0x04000887 RID: 2183
		internal static readonly AttributeDescription ImportedFromTypeLibAttribute = new AttributeDescription("System.Runtime.InteropServices", "ImportedFromTypeLibAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000888 RID: 2184
		internal static readonly AttributeDescription DefaultEventAttribute = new AttributeDescription("System.ComponentModel", "DefaultEventAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x04000889 RID: 2185
		internal static readonly AttributeDescription AssemblyConfigurationAttribute = new AttributeDescription("System.Reflection", "AssemblyConfigurationAttribute", AttributeDescription.s_signatures_HasThis_Void_String_Only, false);

		// Token: 0x0400088A RID: 2186
		internal static readonly AttributeDescription AssemblyAlgorithmIdAttribute = new AttributeDescription("System.Reflection", "AssemblyAlgorithmIdAttribute", AttributeDescription.s_signaturesOfAssemblyAlgorithmIdAttribute, false);

		// Token: 0x0400088B RID: 2187
		internal static readonly AttributeDescription DeprecatedAttribute = new AttributeDescription("Windows.Foundation.Metadata", "DeprecatedAttribute", AttributeDescription.s_signaturesOfDeprecatedAttribute, false);

		// Token: 0x0400088C RID: 2188
		internal static readonly AttributeDescription NullableAttribute = new AttributeDescription("System.Runtime.CompilerServices", "NullableAttribute", AttributeDescription.s_signaturesOfNullableAttribute, false);

		// Token: 0x0400088D RID: 2189
		internal static readonly AttributeDescription NullableContextAttribute = new AttributeDescription("System.Runtime.CompilerServices", "NullableContextAttribute", AttributeDescription.s_signaturesOfNullableContextAttribute, false);

		// Token: 0x0400088E RID: 2190
		internal static readonly AttributeDescription NullablePublicOnlyAttribute = new AttributeDescription("System.Runtime.CompilerServices", "NullablePublicOnlyAttribute", AttributeDescription.s_signatures_HasThis_Void_Boolean_Only, false);

		// Token: 0x0400088F RID: 2191
		internal static readonly AttributeDescription ExperimentalAttribute = new AttributeDescription("Windows.Foundation.Metadata", "ExperimentalAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000890 RID: 2192
		internal static readonly AttributeDescription ExcludeFromCodeCoverageAttribute = new AttributeDescription("System.Diagnostics.CodeAnalysis", "ExcludeFromCodeCoverageAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000891 RID: 2193
		internal static readonly AttributeDescription EnumeratorCancellationAttribute = new AttributeDescription("System.Runtime.CompilerServices", "EnumeratorCancellationAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000892 RID: 2194
		internal static readonly AttributeDescription SkipLocalsInitAttribute = new AttributeDescription("System.Runtime.CompilerServices", "SkipLocalsInitAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000893 RID: 2195
		internal static readonly AttributeDescription NativeIntegerAttribute = new AttributeDescription("System.Runtime.CompilerServices", "NativeIntegerAttribute", AttributeDescription.s_signaturesOfNativeIntegerAttribute, false);

		// Token: 0x04000894 RID: 2196
		internal static readonly AttributeDescription ModuleInitializerAttribute = new AttributeDescription("System.Runtime.CompilerServices", "ModuleInitializerAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x04000895 RID: 2197
		internal static readonly AttributeDescription UnmanagedCallersOnlyAttribute = new AttributeDescription("System.Runtime.InteropServices", "UnmanagedCallersOnlyAttribute", AttributeDescription.s_signatures_HasThis_Void_Only, false);

		// Token: 0x020005EA RID: 1514
		[NullableContext(0)]
		internal enum TypeHandleTarget : byte
		{
			// Token: 0x040016F9 RID: 5881
			AttributeTargets,
			// Token: 0x040016FA RID: 5882
			AssemblyNameFlags,
			// Token: 0x040016FB RID: 5883
			MethodImplOptions,
			// Token: 0x040016FC RID: 5884
			CharSet,
			// Token: 0x040016FD RID: 5885
			LayoutKind,
			// Token: 0x040016FE RID: 5886
			UnmanagedType,
			// Token: 0x040016FF RID: 5887
			TypeLibTypeFlags,
			// Token: 0x04001700 RID: 5888
			ClassInterfaceType,
			// Token: 0x04001701 RID: 5889
			ComInterfaceType,
			// Token: 0x04001702 RID: 5890
			CompilationRelaxations,
			// Token: 0x04001703 RID: 5891
			DebuggingModes,
			// Token: 0x04001704 RID: 5892
			SecurityCriticalScope,
			// Token: 0x04001705 RID: 5893
			CallingConvention,
			// Token: 0x04001706 RID: 5894
			AssemblyHashAlgorithm,
			// Token: 0x04001707 RID: 5895
			TransactionOption,
			// Token: 0x04001708 RID: 5896
			SecurityAction,
			// Token: 0x04001709 RID: 5897
			SystemType,
			// Token: 0x0400170A RID: 5898
			DeprecationType,
			// Token: 0x0400170B RID: 5899
			Platform
		}

		// Token: 0x020005EB RID: 1515
		[Nullable(0)]
		internal struct TypeHandleTargetInfo
		{
			// Token: 0x060036EA RID: 14058 RVA: 0x0009A18E File Offset: 0x0009838E
			public TypeHandleTargetInfo(string @namespace, string name, SerializationTypeCode underlying)
			{
				this.Namespace = @namespace;
				this.Name = name;
				this.Underlying = underlying;
			}

			// Token: 0x0400170C RID: 5900
			public readonly string Namespace;

			// Token: 0x0400170D RID: 5901
			public readonly string Name;

			// Token: 0x0400170E RID: 5902
			public readonly SerializationTypeCode Underlying;
		}
	}
}
