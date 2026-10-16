using System;

namespace System.Web.ModelBinding
{
	/// <summary>Provides a base class for model-binding behavior attributes. </summary>
	// Token: 0x02000513 RID: 1299
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class BindingBehaviorAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.ModelBinding.BindingBehaviorAttribute" /> class.</summary>
		/// <param name="behavior">The model-binding behavior.</param>
		// Token: 0x06003991 RID: 14737 RVA: 0x0009A2EB File Offset: 0x000984EB
		public BindingBehaviorAttribute(BindingBehavior behavior)
		{
			this.Behavior = behavior;
		}

		/// <summary>Gets the model-binding behavior value.</summary>
		/// <returns>The model-binding behavior value.</returns>
		// Token: 0x170011E1 RID: 4577
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x0009A2FA File Offset: 0x000984FA
		// (set) Token: 0x06003993 RID: 14739 RVA: 0x0009A302 File Offset: 0x00098502
		public BindingBehavior Behavior { get; private set; }

		/// <summary>Gets the unique identifier for this attribute.</summary>
		/// <returns>The unique identifier for this attribute.</returns>
		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x06003994 RID: 14740 RVA: 0x0009A30B File Offset: 0x0009850B
		public override object TypeId
		{
			get
			{
				return BindingBehaviorAttribute._typeId;
			}
		}

		// Token: 0x04001F3C RID: 7996
		private static readonly object _typeId = new object();
	}
}
