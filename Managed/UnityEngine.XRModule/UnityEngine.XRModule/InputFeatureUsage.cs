using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x0200000D RID: 13
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputDevices.h")]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public struct InputFeatureUsage : IEquatable<InputFeatureUsage>
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002870 File Offset: 0x00000A70
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002888 File Offset: 0x00000A88
		public string name
		{
			get
			{
				return this.m_Name;
			}
			internal set
			{
				this.m_Name = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002894 File Offset: 0x00000A94
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000028AC File Offset: 0x00000AAC
		internal InputFeatureType internalType
		{
			get
			{
				return this.m_InternalType;
			}
			set
			{
				this.m_InternalType = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028B8 File Offset: 0x00000AB8
		public Type type
		{
			get
			{
				Type type;
				switch (this.m_InternalType)
				{
				case InputFeatureType.Custom:
					type = typeof(byte[]);
					break;
				case InputFeatureType.Binary:
					type = typeof(bool);
					break;
				case InputFeatureType.DiscreteStates:
					type = typeof(uint);
					break;
				case InputFeatureType.Axis1D:
					type = typeof(float);
					break;
				case InputFeatureType.Axis2D:
					type = typeof(Vector2);
					break;
				case InputFeatureType.Axis3D:
					type = typeof(Vector3);
					break;
				case InputFeatureType.Rotation:
					type = typeof(Quaternion);
					break;
				case InputFeatureType.Hand:
					type = typeof(Hand);
					break;
				case InputFeatureType.Bone:
					type = typeof(Bone);
					break;
				case InputFeatureType.Eyes:
					type = typeof(Eyes);
					break;
				default:
					throw new InvalidCastException("No valid managed type for unknown native type.");
				}
				return type;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002993 File Offset: 0x00000B93
		internal InputFeatureUsage(string name, InputFeatureType type)
		{
			this.m_Name = name;
			this.m_InternalType = type;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000029A4 File Offset: 0x00000BA4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage);
			return !flag && this.Equals((InputFeatureUsage)obj);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000029D8 File Offset: 0x00000BD8
		public bool Equals(InputFeatureUsage other)
		{
			return this.name == other.name && this.internalType == other.internalType;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002A10 File Offset: 0x00000C10
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ (this.internalType.GetHashCode() << 1);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A44 File Offset: 0x00000C44
		public static bool operator ==(InputFeatureUsage a, InputFeatureUsage b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002A60 File Offset: 0x00000C60
		public static bool operator !=(InputFeatureUsage a, InputFeatureUsage b)
		{
			return !(a == b);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A7C File Offset: 0x00000C7C
		public InputFeatureUsage<T> As<T>()
		{
			bool flag = this.type != typeof(T);
			if (flag)
			{
				throw new ArgumentException("InputFeatureUsage type does not match out variable type.");
			}
			return new InputFeatureUsage<T>(this.name);
		}

		// Token: 0x0400005B RID: 91
		internal string m_Name;

		// Token: 0x0400005C RID: 92
		[NativeName("m_FeatureType")]
		internal InputFeatureType m_InternalType;
	}
}
