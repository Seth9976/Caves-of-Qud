using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies that the type has a visualizer. This class cannot be inherited. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009BE RID: 2494
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		// Token: 0x060059B8 RID: 22968 RVA: 0x001331A4 File Offset: 0x001313A4
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		// Token: 0x060059B9 RID: 22969 RVA: 0x001331B3 File Offset: 0x001313B3
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type name of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizerTypeName">The fully qualified type name of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x060059BA RID: 22970 RVA: 0x001331C9 File Offset: 0x001313C9
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x060059BB RID: 22971 RVA: 0x001331F8 File Offset: 0x001313F8
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSource">The type of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="visualizerObjectSource" /> is null.</exception>
		// Token: 0x060059BC RID: 22972 RVA: 0x00133220 File Offset: 0x00131420
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> class, specifying the type of the visualizer and the type name of the visualizer object source.</summary>
		/// <param name="visualizer">The type of the visualizer.</param>
		/// <param name="visualizerObjectSourceTypeName">The fully qualified type name of the visualizer object source.</param>
		/// <exception cref="T:System.ArgumentNullException">v<paramref name="isualizer" /> is null.</exception>
		// Token: 0x060059BD RID: 22973 RVA: 0x00133273 File Offset: 0x00131473
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		/// <summary>Gets the fully qualified type name of the visualizer object source.</summary>
		/// <returns>The fully qualified type name of the visualizer object source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x060059BE RID: 22974 RVA: 0x001332A2 File Offset: 0x001314A2
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerObjectSourceName;
			}
		}

		/// <summary>Gets the fully qualified type name of the visualizer.</summary>
		/// <returns>The fully qualified visualizer type name.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x060059BF RID: 22975 RVA: 0x001332AA File Offset: 0x001314AA
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		/// <summary>Gets or sets the description of the visualizer.</summary>
		/// <returns>The description of the visualizer.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x060059C0 RID: 22976 RVA: 0x001332B2 File Offset: 0x001314B2
		// (set) Token: 0x060059C1 RID: 22977 RVA: 0x001332BA File Offset: 0x001314BA
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the target type when the attribute is applied at the assembly level.</summary>
		/// <returns>The type that is the target of the visualizer.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value cannot be set because it is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060059C3 RID: 22979 RVA: 0x001332EC File Offset: 0x001314EC
		// (set) Token: 0x060059C2 RID: 22978 RVA: 0x001332C3 File Offset: 0x001314C3
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the fully qualified type name when the attribute is applied at the assembly level.</summary>
		/// <returns>The fully qualified type name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x001332FD File Offset: 0x001314FD
		// (set) Token: 0x060059C4 RID: 22980 RVA: 0x001332F4 File Offset: 0x001314F4
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x04003788 RID: 14216
		private string visualizerObjectSourceName;

		// Token: 0x04003789 RID: 14217
		private string visualizerName;

		// Token: 0x0400378A RID: 14218
		private string description;

		// Token: 0x0400378B RID: 14219
		private string targetName;

		// Token: 0x0400378C RID: 14220
		private Type target;
	}
}
