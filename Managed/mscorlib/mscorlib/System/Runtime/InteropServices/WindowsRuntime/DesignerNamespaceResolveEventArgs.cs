using System;
using System.Collections.ObjectModel;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides data for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event. </summary>
	// Token: 0x02000797 RID: 1943
	[ComVisible(false)]
	public class DesignerNamespaceResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DesignerNamespaceResolveEventArgs" /> class. </summary>
		/// <param name="namespaceName">The name of the namespace to resolve. </param>
		// Token: 0x060044DB RID: 17627 RVA: 0x000E4F4C File Offset: 0x000E314C
		public DesignerNamespaceResolveEventArgs(string namespaceName)
		{
			this.NamespaceName = namespaceName;
			this.ResolvedAssemblyFiles = new Collection<string>();
		}

		/// <summary>Gets the name of the namespace to resolve. </summary>
		/// <returns>The name of the namespace to resolve. </returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060044DC RID: 17628 RVA: 0x000E4F66 File Offset: 0x000E3166
		// (set) Token: 0x060044DD RID: 17629 RVA: 0x000E4F6E File Offset: 0x000E316E
		public string NamespaceName { get; private set; }

		/// <summary>Gets a collection of assembly file paths; when the event handler for the <see cref="E:System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMetadata.DesignerNamespaceResolve" /> event is invoked, the collection is empty, and the event handler is responsible for adding the necessary assembly files. </summary>
		/// <returns>A collection of assembly files that define the requested namespace. </returns>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060044DE RID: 17630 RVA: 0x000E4F77 File Offset: 0x000E3177
		// (set) Token: 0x060044DF RID: 17631 RVA: 0x000E4F7F File Offset: 0x000E317F
		public Collection<string> ResolvedAssemblyFiles { get; private set; }
	}
}
