using System;
using System.CodeDom.Compiler;
using Unity;

namespace System.Web.Compilation
{
	/// <summary>Represents the compiler settings used within the ASP.NET build environment to generate and compile source code from a virtual path. This class cannot be inherited.</summary>
	// Token: 0x02000647 RID: 1607
	public sealed class CompilerType
	{
		// Token: 0x06004525 RID: 17701 RVA: 0x000BB497 File Offset: 0x000B9697
		internal CompilerType(Type type, CompilerParameters parameters)
		{
			this.type = type;
			this.parameters = parameters;
		}

		/// <summary>Determines whether the specified object represents the same code provider and compiler settings as the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Web.Compilation.CompilerType" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current instance of <see cref="T:System.Web.Compilation.CompilerType" />.</param>
		// Token: 0x06004526 RID: 17702 RVA: 0x000BB4B0 File Offset: 0x000B96B0
		public override bool Equals(object o)
		{
			if (!(o is CompilerType))
			{
				return false;
			}
			CompilerType compilerType = (CompilerType)o;
			return compilerType.type == this.type && compilerType.parameters == this.parameters;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current instance of <see cref="T:System.Web.Compilation.CompilerType" />, suitable for use in hashing algorithms and data structures, such as a hash table.</returns>
		// Token: 0x06004527 RID: 17703 RVA: 0x000BB4F1 File Offset: 0x000B96F1
		public override int GetHashCode()
		{
			return (this.type.GetHashCode() << 6) ^ this.parameters.GetHashCode();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> for the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> that represents the configured code provider type.</returns>
		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06004528 RID: 17704 RVA: 0x000BB50C File Offset: 0x000B970C
		public Type CodeDomProviderType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets the settings and options used to compile source code into an assembly.</summary>
		/// <returns>A read-only <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that represents the settings and options of the code compiler.</returns>
		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06004529 RID: 17705 RVA: 0x000BB514 File Offset: 0x000B9714
		public CompilerParameters CompilerParameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal CompilerType()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040024DA RID: 9434
		private Type type;

		// Token: 0x040024DB RID: 9435
		private CompilerParameters parameters;
	}
}
