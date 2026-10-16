using System;

namespace System.Web.UI
{
	/// <summary>Specifies a design-time class that performs data binding of controls within a designer. This class cannot be inherited.</summary>
	// Token: 0x02000154 RID: 340
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataBindingHandlerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class using no parameters. This is the default constructor.</summary>
		// Token: 0x06000EEB RID: 3819 RVA: 0x000286B7 File Offset: 0x000268B7
		public DataBindingHandlerAttribute()
		{
			this._typeName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class of the specified <see cref="T:System.Type" />.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> for the data-binding handler. </param>
		// Token: 0x06000EEC RID: 3820 RVA: 0x000286CA File Offset: 0x000268CA
		public DataBindingHandlerAttribute(Type type)
		{
			this._typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class with the specified type name.</summary>
		/// <param name="typeName">The fully qualified name of the data-binding handler <see cref="T:System.Type" />. </param>
		// Token: 0x06000EED RID: 3821 RVA: 0x000286DE File Offset: 0x000268DE
		public DataBindingHandlerAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		/// <summary>Gets the type name of the data-binding handler. </summary>
		/// <returns>The type name of the handler. If the type name is null, this property returns an empty string ("").</returns>
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x000286ED File Offset: 0x000268ED
		public string HandlerTypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		/// <summary>Determines whether two object instances are equal.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter equals the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to the current <see cref="T:System.Web.UI.DataBindingHandlerAttribute" />.</param>
		// Token: 0x06000EEF RID: 3823 RVA: 0x00028704 File Offset: 0x00026904
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataBindingHandlerAttribute dataBindingHandlerAttribute = obj as DataBindingHandlerAttribute;
			return dataBindingHandlerAttribute != null && string.Compare(this.HandlerTypeName, dataBindingHandlerAttribute.HandlerTypeName, StringComparison.Ordinal) == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000EF0 RID: 3824 RVA: 0x00028738 File Offset: 0x00026938
		public override int GetHashCode()
		{
			return this.HandlerTypeName.GetHashCode();
		}

		// Token: 0x04001228 RID: 4648
		private string _typeName;

		/// <summary>Defines the default attribute for the <see cref="T:System.Web.UI.DataBindingHandlerAttribute" /> class.</summary>
		// Token: 0x04001229 RID: 4649
		public static readonly DataBindingHandlerAttribute Default = new DataBindingHandlerAttribute();
	}
}
