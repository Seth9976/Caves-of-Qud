using System;

namespace System.Reflection
{
	// Token: 0x0200001C RID: 28
	[Flags]
	public enum AssemblyFlags
	{
		/// <summary>The assembly reference holds the full (unhashed) public key. Not applicable on assembly definition.</summary>
		// Token: 0x04000041 RID: 65
		PublicKey = 1,
		/// <summary>The implementation of the referenced assembly used at runtime is not expected to match the version seen at compile time.</summary>
		// Token: 0x04000042 RID: 66
		Retargetable = 256,
		/// <summary>The assembly contains Windows Runtime code.</summary>
		// Token: 0x04000043 RID: 67
		WindowsRuntime = 512,
		/// <summary>Content type masked bits that correspond to values of <see cref="T:System.Reflection.AssemblyContentType" />.</summary>
		// Token: 0x04000044 RID: 68
		ContentTypeMask = 3584,
		/// <summary>Just-In-Time (JIT) compiler optimization is disabled for the assembly.</summary>
		// Token: 0x04000045 RID: 69
		DisableJitCompileOptimizer = 16384,
		/// <summary>Just-In-Time (JIT) compiler tracking is enabled for the assembly.</summary>
		// Token: 0x04000046 RID: 70
		EnableJitCompileTracking = 32768
	}
}
