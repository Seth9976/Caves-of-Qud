using System;

namespace UnityEngine.XR
{
	// Token: 0x0200000E RID: 14
	public struct InputFeatureUsage<T> : IEquatable<InputFeatureUsage<T>>
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002ABD File Offset: 0x00000CBD
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002AC5 File Offset: 0x00000CC5
		public string name { readonly get; set; }

		// Token: 0x0600004C RID: 76 RVA: 0x00002ACE File Offset: 0x00000CCE
		public InputFeatureUsage(string usageName)
		{
			this.name = usageName;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ADC File Offset: 0x00000CDC
		public override bool Equals(object obj)
		{
			bool flag = !(obj is InputFeatureUsage<T>);
			return !flag && this.Equals((InputFeatureUsage<T>)obj);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B10 File Offset: 0x00000D10
		public bool Equals(InputFeatureUsage<T> other)
		{
			return this.name == other.name;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B34 File Offset: 0x00000D34
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B54 File Offset: 0x00000D54
		public static bool operator ==(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B70 File Offset: 0x00000D70
		public static bool operator !=(InputFeatureUsage<T> a, InputFeatureUsage<T> b)
		{
			return !(a == b);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002B8C File Offset: 0x00000D8C
		private Type usageType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002BA8 File Offset: 0x00000DA8
		public static explicit operator InputFeatureUsage(InputFeatureUsage<T> self)
		{
			InputFeatureType inputFeatureType = (InputFeatureType)4294967295U;
			Type usageType = self.usageType;
			bool flag = usageType == typeof(bool);
			if (flag)
			{
				inputFeatureType = InputFeatureType.Binary;
			}
			else
			{
				bool flag2 = usageType == typeof(uint);
				if (flag2)
				{
					inputFeatureType = InputFeatureType.DiscreteStates;
				}
				else
				{
					bool flag3 = usageType == typeof(float);
					if (flag3)
					{
						inputFeatureType = InputFeatureType.Axis1D;
					}
					else
					{
						bool flag4 = usageType == typeof(Vector2);
						if (flag4)
						{
							inputFeatureType = InputFeatureType.Axis2D;
						}
						else
						{
							bool flag5 = usageType == typeof(Vector3);
							if (flag5)
							{
								inputFeatureType = InputFeatureType.Axis3D;
							}
							else
							{
								bool flag6 = usageType == typeof(Quaternion);
								if (flag6)
								{
									inputFeatureType = InputFeatureType.Rotation;
								}
								else
								{
									bool flag7 = usageType == typeof(Hand);
									if (flag7)
									{
										inputFeatureType = InputFeatureType.Hand;
									}
									else
									{
										bool flag8 = usageType == typeof(Bone);
										if (flag8)
										{
											inputFeatureType = InputFeatureType.Bone;
										}
										else
										{
											bool flag9 = usageType == typeof(Eyes);
											if (flag9)
											{
												inputFeatureType = InputFeatureType.Eyes;
											}
											else
											{
												bool flag10 = usageType == typeof(byte[]);
												if (flag10)
												{
													inputFeatureType = InputFeatureType.Custom;
												}
												else
												{
													bool isEnum = usageType.IsEnum;
													if (isEnum)
													{
														inputFeatureType = InputFeatureType.DiscreteStates;
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			bool flag11 = inputFeatureType != (InputFeatureType)4294967295U;
			if (flag11)
			{
				return new InputFeatureUsage(self.name, inputFeatureType);
			}
			throw new InvalidCastException("No valid InputFeatureType for " + self.name + ".");
		}
	}
}
