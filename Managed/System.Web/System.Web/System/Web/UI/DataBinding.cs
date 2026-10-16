using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Contains information about a single data-binding expression in an ASP.NET server control, which allows rapid-application development (RAD) designers, such as Microsoft Visual Studio, to create data-binding expressions at design time. This class cannot be inherited.</summary>
	// Token: 0x020001BA RID: 442
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBinding" /> class.</summary>
		/// <param name="propertyName">The property to bind data to. </param>
		/// <param name="propertyType">The .NET Framework type of the property to bind data to. </param>
		/// <param name="expression">The data-binding expression to be evaluated. </param>
		// Token: 0x060011EA RID: 4586 RVA: 0x0002FAD3 File Offset: 0x0002DCD3
		public DataBinding(string propertyName, Type propertyType, string expression)
		{
			this.propertyName = propertyName;
			this.propertyType = propertyType;
			this.expression = expression;
		}

		/// <summary>Gets or sets the data-binding expression to be evaluated.</summary>
		/// <returns>The data-binding expression to be evaluated.</returns>
		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0002FAF0 File Offset: 0x0002DCF0
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x0002FAF8 File Offset: 0x0002DCF8
		public string Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		/// <summary>Gets the name of the ASP.NET server control property to bind data to.</summary>
		/// <returns>The property to bind data to.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x0002FB01 File Offset: 0x0002DD01
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the .NET Framework type of the data-bound ASP.NET server control property.</summary>
		/// <returns>The .NET Framework type of the data-bound property.</returns>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060011EE RID: 4590 RVA: 0x0002FB09 File Offset: 0x0002DD09
		public Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		/// <summary>Determines whether the specified object is the same instance of the <see cref="T:System.Web.UI.DataBinding" /> class as the current object.</summary>
		/// <returns>true if the data-binding property names match; otherwise, false.</returns>
		/// <param name="obj">The object to compare against the current <see cref="T:System.Web.UI.DataBinding" />. </param>
		// Token: 0x060011EF RID: 4591 RVA: 0x0002FB14 File Offset: 0x0002DD14
		public override bool Equals(object obj)
		{
			DataBinding dataBinding = obj as DataBinding;
			return dataBinding != null && (dataBinding.Expression == this.expression && dataBinding.PropertyName == this.propertyName) && dataBinding.PropertyType == this.propertyType;
		}

		/// <summary>Retrieves the hash code for an instance of the <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060011F0 RID: 4592 RVA: 0x0002FB66 File Offset: 0x0002DD66
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode() + (this.propertyType.GetHashCode() << 1) + (this.expression.GetHashCode() << 2);
		}

		// Token: 0x0400140B RID: 5131
		private string propertyName;

		// Token: 0x0400140C RID: 5132
		private Type propertyType;

		// Token: 0x0400140D RID: 5133
		private string expression;
	}
}
