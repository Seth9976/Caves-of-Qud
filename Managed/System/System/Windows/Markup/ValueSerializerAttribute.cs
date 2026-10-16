using System;
using System.Runtime.CompilerServices;

namespace System.Windows.Markup
{
	/// <summary>Identifies the <see cref="T:System.Windows.Markup.ValueSerializer" /> class that a type or property should use when it is serialized.</summary>
	// Token: 0x0200017E RID: 382
	[TypeForwardedFrom("WindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
	public sealed class ValueSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using the specified type.</summary>
		/// <param name="valueSerializerType">A type that represents the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class.</param>
		// Token: 0x06000A24 RID: 2596 RVA: 0x0002C36C File Offset: 0x0002A56C
		public ValueSerializerAttribute(Type valueSerializerType)
		{
			this._valueSerializerType = valueSerializerType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Markup.ValueSerializerAttribute" /> class, using an assembly qualified type name string.</summary>
		/// <param name="valueSerializerTypeName">The assembly qualified type name string for the <see cref="T:System.Windows.Markup.ValueSerializer" /> class to use.</param>
		// Token: 0x06000A25 RID: 2597 RVA: 0x0002C37B File Offset: 0x0002A57B
		public ValueSerializerAttribute(string valueSerializerTypeName)
		{
			this._valueSerializerTypeName = valueSerializerTypeName;
		}

		/// <summary>Gets the type of the <see cref="T:System.Windows.Markup.ValueSerializer" /> class reported by this attribute.</summary>
		/// <returns>The type of the <see cref="T:System.Windows.Markup.ValueSerializer" />.</returns>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002C38A File Offset: 0x0002A58A
		public Type ValueSerializerType
		{
			get
			{
				if (this._valueSerializerType == null && this._valueSerializerTypeName != null)
				{
					this._valueSerializerType = Type.GetType(this._valueSerializerTypeName);
				}
				return this._valueSerializerType;
			}
		}

		/// <summary>Gets the assembly qualified name of the <see cref="T:System.Windows.Markup.ValueSerializer" /> type for this type or property.</summary>
		/// <returns>The assembly qualified name of the type.</returns>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0002C3B9 File Offset: 0x0002A5B9
		public string ValueSerializerTypeName
		{
			get
			{
				if (this._valueSerializerType != null)
				{
					return this._valueSerializerType.AssemblyQualifiedName;
				}
				return this._valueSerializerTypeName;
			}
		}

		// Token: 0x040006E8 RID: 1768
		private Type _valueSerializerType;

		// Token: 0x040006E9 RID: 1769
		private string _valueSerializerTypeName;
	}
}
