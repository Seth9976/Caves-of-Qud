using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.Cci
{
	// Token: 0x020000E0 RID: 224
	[NullableContext(1)]
	internal interface ITypeDefinition : IDefinition, IReference, ITypeReference
	{
		// Token: 0x170001BD RID: 445
		// (get) Token: 0x0600072C RID: 1836
		ushort Alignment { get; }

		// Token: 0x0600072D RID: 1837
		[NullableContext(2)]
		ITypeReference GetBaseClass(EmitContext context);

		// Token: 0x0600072E RID: 1838
		IEnumerable<IEventDefinition> GetEvents(EmitContext context);

		// Token: 0x0600072F RID: 1839
		IEnumerable<MethodImplementation> GetExplicitImplementationOverrides(EmitContext context);

		// Token: 0x06000730 RID: 1840
		IEnumerable<IFieldDefinition> GetFields(EmitContext context);

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000731 RID: 1841
		IEnumerable<IGenericTypeParameter> GenericParameters { get; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000732 RID: 1842
		ushort GenericParameterCount { get; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000733 RID: 1843
		bool HasDeclarativeSecurity { get; }

		// Token: 0x06000734 RID: 1844
		IEnumerable<TypeReferenceWithAttributes> Interfaces(EmitContext context);

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000735 RID: 1845
		bool IsAbstract { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000736 RID: 1846
		bool IsBeforeFieldInit { get; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000737 RID: 1847
		bool IsComObject { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000738 RID: 1848
		bool IsGeneric { get; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000739 RID: 1849
		bool IsInterface { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600073A RID: 1850
		bool IsDelegate { get; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600073B RID: 1851
		bool IsRuntimeSpecial { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600073C RID: 1852
		bool IsSerializable { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600073D RID: 1853
		bool IsSpecialName { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600073E RID: 1854
		bool IsWindowsRuntimeImport { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600073F RID: 1855
		bool IsSealed { get; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000740 RID: 1856
		LayoutKind Layout { get; }

		// Token: 0x06000741 RID: 1857
		IEnumerable<IMethodDefinition> GetMethods(EmitContext context);

		// Token: 0x06000742 RID: 1858
		IEnumerable<INestedTypeDefinition> GetNestedTypes(EmitContext context);

		// Token: 0x06000743 RID: 1859
		IEnumerable<IPropertyDefinition> GetProperties(EmitContext context);

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000744 RID: 1860
		IEnumerable<SecurityAttribute> SecurityAttributes { get; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000745 RID: 1861
		uint SizeOf { get; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000746 RID: 1862
		CharSet StringFormat { get; }
	}
}
