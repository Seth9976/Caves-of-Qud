using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x0200009D RID: 157
	[NullableContext(1)]
	internal interface IMethodDefinition : ITypeDefinitionMember, ITypeMemberReference, IReference, INamedEntity, IDefinition, IMethodReference, ISignature
	{
		// Token: 0x06000433 RID: 1075
		IMethodBody GetBody(EmitContext context);

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000434 RID: 1076
		IEnumerable<IGenericMethodParameter> GenericParameters { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000435 RID: 1077
		bool HasDeclarativeSecurity { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000436 RID: 1078
		bool IsAbstract { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000437 RID: 1079
		bool IsAccessCheckedOnOverride { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000438 RID: 1080
		bool IsConstructor { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000439 RID: 1081
		bool IsExternal { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600043A RID: 1082
		bool IsHiddenBySignature { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600043B RID: 1083
		bool IsNewSlot { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600043C RID: 1084
		bool IsPlatformInvoke { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600043D RID: 1085
		bool IsRuntimeSpecial { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600043E RID: 1086
		bool IsSealed { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600043F RID: 1087
		bool IsSpecialName { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000440 RID: 1088
		bool IsStatic { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000441 RID: 1089
		bool IsVirtual { get; }

		// Token: 0x06000442 RID: 1090
		MethodImplAttributes GetImplementationAttributes(EmitContext context);

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000443 RID: 1091
		[Nullable(new byte[] { 0, 1 })]
		ImmutableArray<IParameterDefinition> Parameters
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000444 RID: 1092
		IPlatformInvokeInformation PlatformInvokeData { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000445 RID: 1093
		bool RequiresSecurityObject { get; }

		// Token: 0x06000446 RID: 1094
		IEnumerable<ICustomAttribute> GetReturnValueAttributes(EmitContext context);

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000447 RID: 1095
		bool ReturnValueIsMarshalledExplicitly { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000448 RID: 1096
		IMarshallingInformation ReturnValueMarshallingInformation { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000449 RID: 1097
		[Nullable(0)]
		ImmutableArray<byte> ReturnValueMarshallingDescriptor
		{
			[NullableContext(0)]
			get;
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600044A RID: 1098
		IEnumerable<SecurityAttribute> SecurityAttributes { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600044B RID: 1099
		INamespace ContainingNamespace { get; }
	}
}
