using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x02000327 RID: 807
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x060019AF RID: 6575 RVA: 0x0005EE6C File Offset: 0x0005D06C
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference. </param>
		/// <param name="propertyName">The name of the property to reference. </param>
		// Token: 0x060019B0 RID: 6576 RVA: 0x00060B9C File Offset: 0x0005ED9C
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x00060BB2 File Offset: 0x0005EDB2
		// (set) Token: 0x060019B2 RID: 6578 RVA: 0x00060BBA File Offset: 0x0005EDBA
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x00060BC3 File Offset: 0x0005EDC3
		// (set) Token: 0x060019B4 RID: 6580 RVA: 0x00060BD4 File Offset: 0x0005EDD4
		public string PropertyName
		{
			get
			{
				return this._propertyName ?? string.Empty;
			}
			set
			{
				this._propertyName = value;
			}
		}

		// Token: 0x04000DEE RID: 3566
		private string _propertyName;
	}
}
