using System;
using Unity.Properties;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200045E RID: 1118
	public struct Scale : IEquatable<Scale>
	{
		// Token: 0x060022B1 RID: 8881 RVA: 0x00081A88 File Offset: 0x0007FC88
		public Scale(Vector2 scale)
		{
			this.m_Scale = new Vector3(scale.x, scale.y, 1f);
			this.m_IsNone = false;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x00081AAE File Offset: 0x0007FCAE
		public Scale(Vector3 scale)
		{
			this.m_Scale = scale;
			this.m_IsNone = false;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x00081AC0 File Offset: 0x0007FCC0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static Scale Initial()
		{
			return new Scale(Vector3.one);
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x00081ADC File Offset: 0x0007FCDC
		public static Scale None()
		{
			Scale scale = Scale.Initial();
			scale.m_IsNone = true;
			return scale;
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060022B5 RID: 8885 RVA: 0x00081AFD File Offset: 0x0007FCFD
		// (set) Token: 0x060022B6 RID: 8886 RVA: 0x00081B05 File Offset: 0x0007FD05
		public Vector3 value
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x00081B0E File Offset: 0x0007FD0E
		internal bool IsNone()
		{
			return this.m_IsNone;
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x00081B18 File Offset: 0x0007FD18
		public static implicit operator Scale(Vector2 scale)
		{
			return new Scale(scale);
		}

		// Token: 0x060022B9 RID: 8889 RVA: 0x00081B30 File Offset: 0x0007FD30
		public static bool operator ==(Scale lhs, Scale rhs)
		{
			return lhs.m_Scale == rhs.m_Scale;
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00081B54 File Offset: 0x0007FD54
		public static bool operator !=(Scale lhs, Scale rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00081B70 File Offset: 0x0007FD70
		public bool Equals(Scale other)
		{
			return other == this;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00081B90 File Offset: 0x0007FD90
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is Scale)
			{
				Scale scale = (Scale)obj;
				flag = this.Equals(scale);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00081BBC File Offset: 0x0007FDBC
		public override int GetHashCode()
		{
			return this.m_Scale.GetHashCode() * 793;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00081BE8 File Offset: 0x0007FDE8
		public override string ToString()
		{
			return this.m_Scale.ToString();
		}

		// Token: 0x04000FAA RID: 4010
		private Vector3 m_Scale;

		// Token: 0x04000FAB RID: 4011
		private bool m_IsNone;

		// Token: 0x0200045F RID: 1119
		internal class PropertyBag : ContainerPropertyBag<Scale>
		{
			// Token: 0x060022BF RID: 8895 RVA: 0x00081C0B File Offset: 0x0007FE0B
			public PropertyBag()
			{
				base.AddProperty<Vector3>(new Scale.PropertyBag.ValueProperty());
			}

			// Token: 0x02000460 RID: 1120
			private class ValueProperty : Property<Scale, Vector3>
			{
				// Token: 0x17000926 RID: 2342
				// (get) Token: 0x060022C0 RID: 8896 RVA: 0x00081C21 File Offset: 0x0007FE21
				public override string Name { get; } = "value";

				// Token: 0x17000927 RID: 2343
				// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00081C29 File Offset: 0x0007FE29
				public override bool IsReadOnly { get; } = false;

				// Token: 0x060022C2 RID: 8898 RVA: 0x00081C31 File Offset: 0x0007FE31
				public override Vector3 GetValue(ref Scale container)
				{
					return container.value;
				}

				// Token: 0x060022C3 RID: 8899 RVA: 0x00081C39 File Offset: 0x0007FE39
				public override void SetValue(ref Scale container, Vector3 value)
				{
					container.value = value;
				}
			}
		}
	}
}
