using System;

namespace System.Web.UI
{
	/// <summary>Defines the attribute that indicates whether a control is treated as a visual or non-visual control during design time. This class cannot be inherited.</summary>
	// Token: 0x02000189 RID: 393
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class NonVisualControlAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
		// Token: 0x06000F70 RID: 3952 RVA: 0x00028ABD File Offset: 0x00026CBD
		public NonVisualControlAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a visual or non-visual control. </summary>
		/// <param name="nonVisual">true to initialize the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> to represent a Web control that is not rendered to the client at run time; otherwise, false.</param>
		// Token: 0x06000F71 RID: 3953 RVA: 0x00028AC6 File Offset: 0x00026CC6
		public NonVisualControlAttribute(bool nonVisual)
		{
			this._nonVisual = nonVisual;
		}

		/// <summary>Gets a value indicating whether the control is non-visual.</summary>
		/// <returns>true if the control has been marked as non-visual; otherwise, false. </returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x00028AD5 File Offset: 0x00026CD5
		public bool IsNonVisual
		{
			get
			{
				return this._nonVisual;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000F73 RID: 3955 RVA: 0x00028AE0 File Offset: 0x00026CE0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			NonVisualControlAttribute nonVisualControlAttribute = obj as NonVisualControlAttribute;
			return nonVisualControlAttribute != null && nonVisualControlAttribute.IsNonVisual == this.IsNonVisual;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000F74 RID: 3956 RVA: 0x00028B0D File Offset: 0x00026D0D
		public override int GetHashCode()
		{
			return this._nonVisual.GetHashCode();
		}

		/// <summary>Returns a value indicating whether the current instance is equivalent to a default instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
		/// <returns>true if the current instance is equivalent to a <see cref="F:System.Web.UI.NonVisualControlAttribute.Default" /> instance of the class; otherwise, false.</returns>
		// Token: 0x06000F75 RID: 3957 RVA: 0x00028B1A File Offset: 0x00026D1A
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NonVisualControlAttribute.Default);
		}

		/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a non-visual control during design time. This field is read-only.</summary>
		// Token: 0x0400130E RID: 4878
		public static readonly NonVisualControlAttribute NonVisual = new NonVisualControlAttribute(true);

		/// <summary>Gets a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a visual control during design time. </summary>
		// Token: 0x0400130F RID: 4879
		public static readonly NonVisualControlAttribute Visual = new NonVisualControlAttribute(false);

		/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that represents the application-defined default value of the attribute. This field is read-only.</summary>
		// Token: 0x04001310 RID: 4880
		public static readonly NonVisualControlAttribute Default = NonVisualControlAttribute.Visual;

		// Token: 0x04001311 RID: 4881
		private bool _nonVisual;
	}
}
