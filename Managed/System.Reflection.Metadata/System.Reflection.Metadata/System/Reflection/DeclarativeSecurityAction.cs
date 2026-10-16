using System;

namespace System.Reflection
{
	/// <summary>Specifies the security actions that can be performed using declarative security.</summary>
	// Token: 0x02000018 RID: 24
	public enum DeclarativeSecurityAction : short
	{
		/// <summary>No declarative security action.</summary>
		// Token: 0x04000017 RID: 23
		None,
		/// <summary>Check that all callers in the call chain have been granted the specified permission.</summary>
		// Token: 0x04000018 RID: 24
		Demand = 2,
		/// <summary>The calling code can access the resource identified by the current permission object, even if callers higher in the stack have not been granted permission to access the resource.</summary>
		// Token: 0x04000019 RID: 25
		Assert,
		/// <summary>Without further checks refuse Demand for the specified permission.</summary>
		// Token: 0x0400001A RID: 26
		Deny,
		/// <summary>Without further checks, refuse the demand for all permissions other than those specified.</summary>
		// Token: 0x0400001B RID: 27
		PermitOnly,
		/// <summary>Check that the immediate caller has been granted the specified permission.</summary>
		// Token: 0x0400001C RID: 28
		LinkDemand,
		/// <summary>The derived class inheriting the class or overriding a method is required to have the specified permission.</summary>
		// Token: 0x0400001D RID: 29
		InheritanceDemand,
		/// <summary>Request the minimum permissions required for code to run. This action can only be used within the scope of the assembly.</summary>
		// Token: 0x0400001E RID: 30
		RequestMinimum,
		/// <summary>Request additional permissions that are optional (not required to run). This request implicitly refuses all other permissions not specifically requested. This action can only be used within the scope of the assembly.</summary>
		// Token: 0x0400001F RID: 31
		RequestOptional,
		/// <summary>Request that permissions that might be misused not be granted to the calling code. This action can only be used within the scope of the assembly.</summary>
		// Token: 0x04000020 RID: 32
		RequestRefuse
	}
}
