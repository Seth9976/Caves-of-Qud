using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents a generalized Clipboard format. </summary>
	// Token: 0x0200018D RID: 397
	public struct FORMATETC
	{
		/// <summary>Specifies the particular clipboard format of interest.</summary>
		// Token: 0x04000722 RID: 1826
		[MarshalAs(UnmanagedType.U2)]
		public short cfFormat;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration constants that indicates how much detail should be contained in the rendering.</summary>
		// Token: 0x04000723 RID: 1827
		[MarshalAs(UnmanagedType.U4)]
		public DVASPECT dwAspect;

		/// <summary>Specifies part of the aspect when the data must be split across page boundaries. </summary>
		// Token: 0x04000724 RID: 1828
		public int lindex;

		/// <summary>Specifies a pointer to a DVTARGETDEVICE structure containing information about the target device that the data is being composed for. </summary>
		// Token: 0x04000725 RID: 1829
		public IntPtr ptd;

		/// <summary>Specifies one of the <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> enumeration constants, which indicates the type of storage medium used to transfer the object's data. </summary>
		// Token: 0x04000726 RID: 1830
		[MarshalAs(UnmanagedType.U4)]
		public TYMED tymed;
	}
}
