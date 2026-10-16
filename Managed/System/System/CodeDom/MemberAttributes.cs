using System;

namespace System.CodeDom
{
	/// <summary>Defines member attribute identifiers for class members.</summary>
	// Token: 0x02000342 RID: 834
	public enum MemberAttributes
	{
		/// <summary>An abstract member.</summary>
		// Token: 0x04000E28 RID: 3624
		Abstract = 1,
		/// <summary>A member that cannot be overridden in a derived class.</summary>
		// Token: 0x04000E29 RID: 3625
		Final,
		/// <summary>A static member. In Visual Basic, this is equivalent to the Shared keyword.</summary>
		// Token: 0x04000E2A RID: 3626
		Static,
		/// <summary>A member that overrides a base class member.</summary>
		// Token: 0x04000E2B RID: 3627
		Override,
		/// <summary>A constant member.</summary>
		// Token: 0x04000E2C RID: 3628
		Const,
		/// <summary>A new member.</summary>
		// Token: 0x04000E2D RID: 3629
		New = 16,
		/// <summary>An overloaded member. Some languages, such as Visual Basic, require overloaded members to be explicitly indicated.</summary>
		// Token: 0x04000E2E RID: 3630
		Overloaded = 256,
		/// <summary>A member that is accessible to any class within the same assembly.</summary>
		// Token: 0x04000E2F RID: 3631
		Assembly = 4096,
		/// <summary>A member that is accessible within its class, and derived classes in the same assembly.</summary>
		// Token: 0x04000E30 RID: 3632
		FamilyAndAssembly = 8192,
		/// <summary>A member that is accessible within the family of its class and derived classes.</summary>
		// Token: 0x04000E31 RID: 3633
		Family = 12288,
		/// <summary>A member that is accessible within its class, its derived classes in any assembly, and any class in the same assembly.</summary>
		// Token: 0x04000E32 RID: 3634
		FamilyOrAssembly = 16384,
		/// <summary>A private member.</summary>
		// Token: 0x04000E33 RID: 3635
		Private = 20480,
		/// <summary>A public member.</summary>
		// Token: 0x04000E34 RID: 3636
		Public = 24576,
		/// <summary>An access mask.</summary>
		// Token: 0x04000E35 RID: 3637
		AccessMask = 61440,
		/// <summary>A scope mask.</summary>
		// Token: 0x04000E36 RID: 3638
		ScopeMask = 15,
		/// <summary>A VTable mask.</summary>
		// Token: 0x04000E37 RID: 3639
		VTableMask = 240
	}
}
