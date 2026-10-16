using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Specifies the display proxy for a type.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020009BC RID: 2492
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerTypeProxyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type of the proxy. </summary>
		/// <param name="type">The proxy type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060059A7 RID: 22951 RVA: 0x00133082 File Offset: 0x00131282
		public DebuggerTypeProxyAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> class using the type name of the proxy. </summary>
		/// <param name="typeName">The type name of the proxy type.</param>
		// Token: 0x060059A8 RID: 22952 RVA: 0x001330AA File Offset: 0x001312AA
		public DebuggerTypeProxyAttribute(string typeName)
		{
			this.typeName = typeName;
		}

		/// <summary>Gets the type name of the proxy type. </summary>
		/// <returns>The type name of the proxy type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x060059A9 RID: 22953 RVA: 0x001330B9 File Offset: 0x001312B9
		public string ProxyTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		/// <summary>Gets or sets the target type for the attribute.</summary>
		/// <returns>The target type for the attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x060059AB RID: 22955 RVA: 0x001330EA File Offset: 0x001312EA
		// (set) Token: 0x060059AA RID: 22954 RVA: 0x001330C1 File Offset: 0x001312C1
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

		/// <summary>Gets or sets the name of the target type.</summary>
		/// <returns>The name of the target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x060059AC RID: 22956 RVA: 0x001330F2 File Offset: 0x001312F2
		// (set) Token: 0x060059AD RID: 22957 RVA: 0x001330FA File Offset: 0x001312FA
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

		// Token: 0x04003780 RID: 14208
		private string typeName;

		// Token: 0x04003781 RID: 14209
		private string targetName;

		// Token: 0x04003782 RID: 14210
		private Type target;
	}
}
