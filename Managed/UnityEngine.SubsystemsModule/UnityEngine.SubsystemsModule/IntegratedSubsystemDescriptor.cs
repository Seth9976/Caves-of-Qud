using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000007 RID: 7
	[UsedByNativeCode("SubsystemDescriptorBase")]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class IntegratedSubsystemDescriptor : ISubsystemDescriptorImpl, ISubsystemDescriptor
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002165 File Offset: 0x00000365
		public string id
		{
			get
			{
				return SubsystemDescriptorBindings.GetId(this.m_Ptr);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002172 File Offset: 0x00000372
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000217A File Offset: 0x0000037A
		IntPtr ISubsystemDescriptorImpl.ptr
		{
			get
			{
				return this.m_Ptr;
			}
			set
			{
				this.m_Ptr = value;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002183 File Offset: 0x00000383
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x06000018 RID: 24
		internal abstract ISubsystem CreateImpl();

		// Token: 0x04000003 RID: 3
		[VisibleToOtherModules(new string[] { "UnityEngine.XRModule" })]
		internal IntPtr m_Ptr;
	}
}
