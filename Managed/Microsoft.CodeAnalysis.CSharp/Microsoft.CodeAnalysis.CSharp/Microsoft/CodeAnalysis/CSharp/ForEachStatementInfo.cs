using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200010A RID: 266
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public struct ForEachStatementInfo : IEquatable<ForEachStatementInfo>
	{
		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00074745 File Offset: 0x00072945
		public readonly bool IsAsynchronous { get; }

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0007474D File Offset: 0x0007294D
		public readonly IMethodSymbol GetEnumeratorMethod { get; }

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00074755 File Offset: 0x00072955
		public readonly IMethodSymbol MoveNextMethod { get; }

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x0007475D File Offset: 0x0007295D
		public readonly IPropertySymbol CurrentProperty { get; }

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00074765 File Offset: 0x00072965
		public readonly IMethodSymbol DisposeMethod { get; }

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x0007476D File Offset: 0x0007296D
		public readonly ITypeSymbol ElementType { get; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00074775 File Offset: 0x00072975
		public readonly Conversion ElementConversion { get; }

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x0007477D File Offset: 0x0007297D
		public readonly Conversion CurrentConversion { get; }

		// Token: 0x0600158B RID: 5515 RVA: 0x00074785 File Offset: 0x00072985
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		internal ForEachStatementInfo(bool isAsync, IMethodSymbol getEnumeratorMethod, IMethodSymbol moveNextMethod, IPropertySymbol currentProperty, IMethodSymbol disposeMethod, ITypeSymbol elementType, Conversion elementConversion, Conversion currentConversion)
		{
			this.IsAsynchronous = isAsync;
			this.GetEnumeratorMethod = getEnumeratorMethod;
			this.MoveNextMethod = moveNextMethod;
			this.CurrentProperty = currentProperty;
			this.DisposeMethod = disposeMethod;
			this.ElementType = elementType;
			this.ElementConversion = elementConversion;
			this.CurrentConversion = currentConversion;
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x000747C4 File Offset: 0x000729C4
		public override bool Equals(object obj)
		{
			return obj is ForEachStatementInfo && this.Equals((ForEachStatementInfo)obj);
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x000747DC File Offset: 0x000729DC
		public bool Equals(ForEachStatementInfo other)
		{
			return this.IsAsynchronous == other.IsAsynchronous && object.Equals(this.GetEnumeratorMethod, other.GetEnumeratorMethod) && object.Equals(this.MoveNextMethod, other.MoveNextMethod) && object.Equals(this.CurrentProperty, other.CurrentProperty) && object.Equals(this.DisposeMethod, other.DisposeMethod) && object.Equals(this.ElementType, other.ElementType) && this.ElementConversion == other.ElementConversion && this.CurrentConversion == other.CurrentConversion;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x00074888 File Offset: 0x00072A88
		public override int GetHashCode()
		{
			return Hash.Combine(this.IsAsynchronous, Hash.Combine<IMethodSymbol>(this.GetEnumeratorMethod, Hash.Combine<IMethodSymbol>(this.MoveNextMethod, Hash.Combine<IPropertySymbol>(this.CurrentProperty, Hash.Combine<IMethodSymbol>(this.DisposeMethod, Hash.Combine<ITypeSymbol>(this.ElementType, Hash.Combine(this.ElementConversion.GetHashCode(), this.CurrentConversion.GetHashCode())))))));
		}
	}
}
