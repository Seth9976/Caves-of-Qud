using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Collections;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public struct InputControlScheme : IEquatable<InputControlScheme>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000E1B9 File Offset: 0x0000C3B9
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000E1C1 File Offset: 0x0000C3C1
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0000E1C9 File Offset: 0x0000C3C9
		public string bindingGroup
		{
			get
			{
				return this.m_BindingGroup;
			}
			set
			{
				this.m_BindingGroup = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000E1D2 File Offset: 0x0000C3D2
		public ReadOnlyArray<InputControlScheme.DeviceRequirement> deviceRequirements
		{
			get
			{
				return new ReadOnlyArray<InputControlScheme.DeviceRequirement>(this.m_DeviceRequirements);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000E1E0 File Offset: 0x0000C3E0
		public InputControlScheme(string name, IEnumerable<InputControlScheme.DeviceRequirement> devices = null, string bindingGroup = null)
		{
			this = default(InputControlScheme);
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException("name");
			}
			this.SetNameAndBindingGroup(name, bindingGroup);
			this.m_DeviceRequirements = null;
			if (devices != null)
			{
				this.m_DeviceRequirements = devices.ToArray<InputControlScheme.DeviceRequirement>();
				if (this.m_DeviceRequirements.Length == 0)
				{
					this.m_DeviceRequirements = null;
				}
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000E235 File Offset: 0x0000C435
		internal void SetNameAndBindingGroup(string name, string bindingGroup = null)
		{
			this.m_Name = name;
			if (!string.IsNullOrEmpty(bindingGroup))
			{
				this.m_BindingGroup = bindingGroup;
				return;
			}
			this.m_BindingGroup = (name.Contains(';') ? name.Replace(";", "") : name);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E274 File Offset: 0x0000C474
		public static InputControlScheme? FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, InputDevice mustIncludeDevice = null, bool allowUnsuccesfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme inputControlScheme;
			InputControlScheme.MatchResult matchResult;
			if (!InputControlScheme.FindControlSchemeForDevices<TDevices, TSchemes>(devices, schemes, out inputControlScheme, out matchResult, mustIncludeDevice, allowUnsuccesfulMatch))
			{
				return null;
			}
			matchResult.Dispose();
			return new InputControlScheme?(inputControlScheme);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E2D0 File Offset: 0x0000C4D0
		public static bool FindControlSchemeForDevices<TDevices, TSchemes>(TDevices devices, TSchemes schemes, out InputControlScheme controlScheme, out InputControlScheme.MatchResult matchResult, InputDevice mustIncludeDevice = null, bool allowUnsuccessfulMatch = false) where TDevices : IReadOnlyList<InputDevice> where TSchemes : IEnumerable<InputControlScheme>
		{
			if (devices == null)
			{
				throw new ArgumentNullException("devices");
			}
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			InputControlScheme.MatchResult? matchResult2 = null;
			InputControlScheme? inputControlScheme = null;
			foreach (InputControlScheme inputControlScheme2 in schemes)
			{
				InputControlScheme.MatchResult matchResult3 = inputControlScheme2.PickDevicesFrom<TDevices>(devices, mustIncludeDevice);
				if (!matchResult3.isSuccessfulMatch && (!allowUnsuccessfulMatch || matchResult3.score <= 0f))
				{
					matchResult3.Dispose();
				}
				else if (mustIncludeDevice != null && !matchResult3.devices.Contains(mustIncludeDevice))
				{
					matchResult3.Dispose();
				}
				else if (matchResult2 != null && matchResult2.Value.score >= matchResult3.score)
				{
					matchResult3.Dispose();
				}
				else
				{
					if (matchResult2 != null)
					{
						matchResult2.GetValueOrDefault().Dispose();
					}
					matchResult2 = new InputControlScheme.MatchResult?(matchResult3);
					inputControlScheme = new InputControlScheme?(inputControlScheme2);
				}
			}
			matchResult = matchResult2.GetValueOrDefault();
			controlScheme = inputControlScheme.GetValueOrDefault();
			return matchResult2 != null;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E41C File Offset: 0x0000C61C
		public static InputControlScheme? FindControlSchemeForDevice<TSchemes>(InputDevice device, TSchemes schemes) where TSchemes : IEnumerable<InputControlScheme>
		{
			if (schemes == null)
			{
				throw new ArgumentNullException("schemes");
			}
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			return InputControlScheme.FindControlSchemeForDevices<OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>, TSchemes>(new OneOrMore<InputDevice, ReadOnlyArray<InputDevice>>(device), schemes, null, false);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E450 File Offset: 0x0000C650
		public bool SupportsDevice(InputDevice device)
		{
			if (device == null)
			{
				throw new ArgumentNullException("device");
			}
			for (int i = 0; i < this.m_DeviceRequirements.Length; i++)
			{
				if (InputControlPath.TryFindControl(device, this.m_DeviceRequirements[i].controlPath, 0) != null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E49C File Offset: 0x0000C69C
		public InputControlScheme.MatchResult PickDevicesFrom<TDevices>(TDevices devices, InputDevice favorDevice = null) where TDevices : IReadOnlyList<InputDevice>
		{
			InputControlScheme.MatchResult matchResult;
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				matchResult = new InputControlScheme.MatchResult
				{
					m_Result = InputControlScheme.MatchResult.Result.AllSatisfied,
					m_Score = 0.5f
				};
				return matchResult;
			}
			bool flag = true;
			bool flag2 = true;
			int num = this.m_DeviceRequirements.Length;
			float num2 = 0f;
			InputControlList<InputControl> inputControlList = new InputControlList<InputControl>(Allocator.Persistent, num);
			try
			{
				bool flag3 = false;
				bool flag4 = false;
				for (int i = 0; i < num; i++)
				{
					bool isOR = this.m_DeviceRequirements[i].isOR;
					bool isOptional = this.m_DeviceRequirements[i].isOptional;
					if (isOR && flag3)
					{
						inputControlList.Add(null);
					}
					else
					{
						string controlPath = this.m_DeviceRequirements[i].controlPath;
						if (string.IsNullOrEmpty(controlPath))
						{
							num2 += 1f;
							inputControlList.Add(null);
						}
						else
						{
							InputControl inputControl = null;
							int j = 0;
							while (j < devices.Count)
							{
								InputDevice inputDevice = devices[j];
								if (favorDevice != null)
								{
									if (j == 0)
									{
										inputDevice = favorDevice;
									}
									else if (inputDevice == favorDevice)
									{
										inputDevice = devices[0];
									}
								}
								InputControl inputControl2 = InputControlPath.TryFindControl(inputDevice, controlPath, 0);
								if (inputControl2 != null && !inputControlList.Contains(inputControl2))
								{
									inputControl = inputControl2;
									InternedString internedString = new InternedString(InputControlPath.TryGetDeviceLayout(controlPath));
									if (internedString.IsEmpty())
									{
										num2 += 1f;
										break;
									}
									InternedString layout = inputControl2.device.m_Layout;
									int num3;
									if (InputControlLayout.s_Layouts.ComputeDistanceInInheritanceHierarchy(internedString, layout, out num3))
									{
										num2 += 1f + 1f / (float)(Math.Abs(num3) + 1);
										break;
									}
									num2 += 1f;
									break;
								}
								else
								{
									j++;
								}
							}
							if (i + 1 < num && this.m_DeviceRequirements[i + 1].isOR)
							{
								if (inputControl != null)
								{
									flag3 = true;
								}
								else if (!isOptional)
								{
									flag4 = true;
								}
							}
							else if (isOR && i == num - 1)
							{
								if (inputControl == null)
								{
									if (flag4)
									{
										flag = false;
									}
									else
									{
										flag2 = false;
									}
								}
							}
							else
							{
								if (inputControl == null)
								{
									if (isOptional)
									{
										flag2 = false;
									}
									else
									{
										flag = false;
									}
								}
								if (i > 0 && this.m_DeviceRequirements[i - 1].isOR)
								{
									if (!flag3)
									{
										if (flag4)
										{
											flag = false;
										}
										else
										{
											flag2 = false;
										}
									}
									flag3 = false;
								}
							}
							inputControlList.Add(inputControl);
						}
					}
				}
			}
			catch (Exception)
			{
				inputControlList.Dispose();
				throw;
			}
			matchResult = new InputControlScheme.MatchResult
			{
				m_Result = ((!flag) ? InputControlScheme.MatchResult.Result.MissingRequired : ((!flag2) ? InputControlScheme.MatchResult.Result.MissingOptional : InputControlScheme.MatchResult.Result.AllSatisfied)),
				m_Controls = inputControlList,
				m_Requirements = this.m_DeviceRequirements,
				m_Score = num2
			};
			return matchResult;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E750 File Offset: 0x0000C950
		public bool Equals(InputControlScheme other)
		{
			if (!string.Equals(this.m_Name, other.m_Name, StringComparison.InvariantCultureIgnoreCase) || !string.Equals(this.m_BindingGroup, other.m_BindingGroup, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}
			if (this.m_DeviceRequirements == null || this.m_DeviceRequirements.Length == 0)
			{
				return other.m_DeviceRequirements == null || other.m_DeviceRequirements.Length == 0;
			}
			if (other.m_DeviceRequirements == null || this.m_DeviceRequirements.Length != other.m_DeviceRequirements.Length)
			{
				return false;
			}
			int num = this.m_DeviceRequirements.Length;
			for (int i = 0; i < num; i++)
			{
				InputControlScheme.DeviceRequirement deviceRequirement = this.m_DeviceRequirements[i];
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					if (other.m_DeviceRequirements[j] == deviceRequirement)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E81C File Offset: 0x0000CA1C
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputControlScheme && this.Equals((InputControlScheme)obj);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E83C File Offset: 0x0000CA3C
		public override int GetHashCode()
		{
			return (((((this.m_Name != null) ? this.m_Name.GetHashCode() : 0) * 397) ^ ((this.m_BindingGroup != null) ? this.m_BindingGroup.GetHashCode() : 0)) * 397) ^ ((this.m_DeviceRequirements != null) ? this.m_DeviceRequirements.GetHashCode() : 0);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E89C File Offset: 0x0000CA9C
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.m_Name))
			{
				return base.ToString();
			}
			if (this.m_DeviceRequirements == null)
			{
				return this.m_Name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.m_Name);
			stringBuilder.Append('(');
			bool flag = true;
			foreach (InputControlScheme.DeviceRequirement deviceRequirement in this.m_DeviceRequirements)
			{
				if (!flag)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(deviceRequirement.controlPath);
				flag = false;
			}
			stringBuilder.Append(')');
			return stringBuilder.ToString();
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000E93D File Offset: 0x0000CB3D
		public static bool operator ==(InputControlScheme left, InputControlScheme right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000E947 File Offset: 0x0000CB47
		public static bool operator !=(InputControlScheme left, InputControlScheme right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040000FF RID: 255
		[SerializeField]
		internal string m_Name;

		// Token: 0x04000100 RID: 256
		[SerializeField]
		internal string m_BindingGroup;

		// Token: 0x04000101 RID: 257
		[SerializeField]
		internal InputControlScheme.DeviceRequirement[] m_DeviceRequirements;

		// Token: 0x02000186 RID: 390
		public struct MatchResult : IEnumerable<InputControlScheme.MatchResult.Match>, IEnumerable, IDisposable
		{
			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060013CA RID: 5066 RVA: 0x0005C5DB File Offset: 0x0005A7DB
			public float score
			{
				get
				{
					return this.m_Score;
				}
			}

			// Token: 0x17000562 RID: 1378
			// (get) Token: 0x060013CB RID: 5067 RVA: 0x0005C5E3 File Offset: 0x0005A7E3
			public bool isSuccessfulMatch
			{
				get
				{
					return this.m_Result != InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x17000563 RID: 1379
			// (get) Token: 0x060013CC RID: 5068 RVA: 0x0005C5F1 File Offset: 0x0005A7F1
			public bool hasMissingRequiredDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingRequired;
				}
			}

			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x060013CD RID: 5069 RVA: 0x0005C5FC File Offset: 0x0005A7FC
			public bool hasMissingOptionalDevices
			{
				get
				{
					return this.m_Result == InputControlScheme.MatchResult.Result.MissingOptional;
				}
			}

			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x060013CE RID: 5070 RVA: 0x0005C608 File Offset: 0x0005A808
			public InputControlList<InputDevice> devices
			{
				get
				{
					if (this.m_Devices.Count == 0 && !this.hasMissingRequiredDevices)
					{
						int count = this.m_Controls.Count;
						if (count != 0)
						{
							this.m_Devices.Capacity = count;
							for (int i = 0; i < count; i++)
							{
								InputControl inputControl = this.m_Controls[i];
								if (inputControl != null)
								{
									InputDevice device = inputControl.device;
									if (!this.m_Devices.Contains(device))
									{
										this.m_Devices.Add(device);
									}
								}
							}
						}
					}
					return this.m_Devices;
				}
			}

			// Token: 0x17000566 RID: 1382
			public InputControlScheme.MatchResult.Match this[int index]
			{
				get
				{
					if (index < 0 || this.m_Requirements == null || index >= this.m_Requirements.Length)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return new InputControlScheme.MatchResult.Match
					{
						m_RequirementIndex = index,
						m_Requirements = this.m_Requirements,
						m_Controls = this.m_Controls
					};
				}
			}

			// Token: 0x060013D0 RID: 5072 RVA: 0x0005C6E4 File Offset: 0x0005A8E4
			public IEnumerator<InputControlScheme.MatchResult.Match> GetEnumerator()
			{
				return new InputControlScheme.MatchResult.Enumerator
				{
					m_Index = -1,
					m_Requirements = this.m_Requirements,
					m_Controls = this.m_Controls
				};
			}

			// Token: 0x060013D1 RID: 5073 RVA: 0x0005C721 File Offset: 0x0005A921
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060013D2 RID: 5074 RVA: 0x0005C729 File Offset: 0x0005A929
			public void Dispose()
			{
				this.m_Controls.Dispose();
				this.m_Devices.Dispose();
			}

			// Token: 0x0400089A RID: 2202
			internal InputControlScheme.MatchResult.Result m_Result;

			// Token: 0x0400089B RID: 2203
			internal float m_Score;

			// Token: 0x0400089C RID: 2204
			internal InputControlList<InputDevice> m_Devices;

			// Token: 0x0400089D RID: 2205
			internal InputControlList<InputControl> m_Controls;

			// Token: 0x0400089E RID: 2206
			internal InputControlScheme.DeviceRequirement[] m_Requirements;

			// Token: 0x02000270 RID: 624
			internal enum Result
			{
				// Token: 0x04000CF8 RID: 3320
				AllSatisfied,
				// Token: 0x04000CF9 RID: 3321
				MissingRequired,
				// Token: 0x04000CFA RID: 3322
				MissingOptional
			}

			// Token: 0x02000271 RID: 625
			public struct Match
			{
				// Token: 0x17000614 RID: 1556
				// (get) Token: 0x06001696 RID: 5782 RVA: 0x0006795F File Offset: 0x00065B5F
				public InputControl control
				{
					get
					{
						return this.m_Controls[this.m_RequirementIndex];
					}
				}

				// Token: 0x17000615 RID: 1557
				// (get) Token: 0x06001697 RID: 5783 RVA: 0x00067972 File Offset: 0x00065B72
				public InputDevice device
				{
					get
					{
						InputControl control = this.control;
						if (control == null)
						{
							return null;
						}
						return control.device;
					}
				}

				// Token: 0x17000616 RID: 1558
				// (get) Token: 0x06001698 RID: 5784 RVA: 0x00067985 File Offset: 0x00065B85
				public int requirementIndex
				{
					get
					{
						return this.m_RequirementIndex;
					}
				}

				// Token: 0x17000617 RID: 1559
				// (get) Token: 0x06001699 RID: 5785 RVA: 0x0006798D File Offset: 0x00065B8D
				public InputControlScheme.DeviceRequirement requirement
				{
					get
					{
						return this.m_Requirements[this.m_RequirementIndex];
					}
				}

				// Token: 0x17000618 RID: 1560
				// (get) Token: 0x0600169A RID: 5786 RVA: 0x000679A0 File Offset: 0x00065BA0
				public bool isOptional
				{
					get
					{
						return this.requirement.isOptional;
					}
				}

				// Token: 0x04000CFB RID: 3323
				internal int m_RequirementIndex;

				// Token: 0x04000CFC RID: 3324
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000CFD RID: 3325
				internal InputControlList<InputControl> m_Controls;
			}

			// Token: 0x02000272 RID: 626
			private struct Enumerator : IEnumerator<InputControlScheme.MatchResult.Match>, IEnumerator, IDisposable
			{
				// Token: 0x0600169B RID: 5787 RVA: 0x000679BB File Offset: 0x00065BBB
				public bool MoveNext()
				{
					this.m_Index++;
					return this.m_Requirements != null && this.m_Index < this.m_Requirements.Length;
				}

				// Token: 0x0600169C RID: 5788 RVA: 0x000679E5 File Offset: 0x00065BE5
				public void Reset()
				{
					this.m_Index = -1;
				}

				// Token: 0x17000619 RID: 1561
				// (get) Token: 0x0600169D RID: 5789 RVA: 0x000679F0 File Offset: 0x00065BF0
				public InputControlScheme.MatchResult.Match Current
				{
					get
					{
						if (this.m_Requirements == null || this.m_Index < 0 || this.m_Index >= this.m_Requirements.Length)
						{
							throw new InvalidOperationException("Enumerator is not valid");
						}
						return new InputControlScheme.MatchResult.Match
						{
							m_RequirementIndex = this.m_Index,
							m_Requirements = this.m_Requirements,
							m_Controls = this.m_Controls
						};
					}
				}

				// Token: 0x1700061A RID: 1562
				// (get) Token: 0x0600169E RID: 5790 RVA: 0x00067A59 File Offset: 0x00065C59
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x0600169F RID: 5791 RVA: 0x00067A66 File Offset: 0x00065C66
				public void Dispose()
				{
				}

				// Token: 0x04000CFE RID: 3326
				internal int m_Index;

				// Token: 0x04000CFF RID: 3327
				internal InputControlScheme.DeviceRequirement[] m_Requirements;

				// Token: 0x04000D00 RID: 3328
				internal InputControlList<InputControl> m_Controls;
			}
		}

		// Token: 0x02000187 RID: 391
		[Serializable]
		public struct DeviceRequirement : IEquatable<InputControlScheme.DeviceRequirement>
		{
			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060013D3 RID: 5075 RVA: 0x0005C741 File Offset: 0x0005A941
			// (set) Token: 0x060013D4 RID: 5076 RVA: 0x0005C749 File Offset: 0x0005A949
			public string controlPath
			{
				get
				{
					return this.m_ControlPath;
				}
				set
				{
					this.m_ControlPath = value;
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060013D5 RID: 5077 RVA: 0x0005C752 File Offset: 0x0005A952
			// (set) Token: 0x060013D6 RID: 5078 RVA: 0x0005C75F File Offset: 0x0005A95F
			public bool isOptional
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Optional) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Optional;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Optional;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0005C782 File Offset: 0x0005A982
			// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0005C78D File Offset: 0x0005A98D
			public bool isAND
			{
				get
				{
					return !this.isOR;
				}
				set
				{
					this.isOR = !value;
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0005C799 File Offset: 0x0005A999
			// (set) Token: 0x060013DA RID: 5082 RVA: 0x0005C7A6 File Offset: 0x0005A9A6
			public bool isOR
			{
				get
				{
					return (this.m_Flags & InputControlScheme.DeviceRequirement.Flags.Or) > InputControlScheme.DeviceRequirement.Flags.None;
				}
				set
				{
					if (value)
					{
						this.m_Flags |= InputControlScheme.DeviceRequirement.Flags.Or;
						return;
					}
					this.m_Flags &= ~InputControlScheme.DeviceRequirement.Flags.Or;
				}
			}

			// Token: 0x060013DB RID: 5083 RVA: 0x0005C7CC File Offset: 0x0005A9CC
			public override string ToString()
			{
				if (string.IsNullOrEmpty(this.controlPath))
				{
					return base.ToString();
				}
				if (this.isOptional)
				{
					return this.controlPath + " (Optional)";
				}
				return this.controlPath + " (Required)";
			}

			// Token: 0x060013DC RID: 5084 RVA: 0x0005C820 File Offset: 0x0005AA20
			public bool Equals(InputControlScheme.DeviceRequirement other)
			{
				return string.Equals(this.m_ControlPath, other.m_ControlPath) && this.m_Flags == other.m_Flags && string.Equals(this.controlPath, other.controlPath) && this.isOptional == other.isOptional;
			}

			// Token: 0x060013DD RID: 5085 RVA: 0x0005C873 File Offset: 0x0005AA73
			public override bool Equals(object obj)
			{
				return obj != null && obj is InputControlScheme.DeviceRequirement && this.Equals((InputControlScheme.DeviceRequirement)obj);
			}

			// Token: 0x060013DE RID: 5086 RVA: 0x0005C890 File Offset: 0x0005AA90
			public override int GetHashCode()
			{
				return (((((((this.m_ControlPath != null) ? this.m_ControlPath.GetHashCode() : 0) * 397) ^ this.m_Flags.GetHashCode()) * 397) ^ ((this.controlPath != null) ? this.controlPath.GetHashCode() : 0)) * 397) ^ this.isOptional.GetHashCode();
			}

			// Token: 0x060013DF RID: 5087 RVA: 0x0005C8FD File Offset: 0x0005AAFD
			public static bool operator ==(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return left.Equals(right);
			}

			// Token: 0x060013E0 RID: 5088 RVA: 0x0005C907 File Offset: 0x0005AB07
			public static bool operator !=(InputControlScheme.DeviceRequirement left, InputControlScheme.DeviceRequirement right)
			{
				return !left.Equals(right);
			}

			// Token: 0x0400089F RID: 2207
			[SerializeField]
			internal string m_ControlPath;

			// Token: 0x040008A0 RID: 2208
			[SerializeField]
			internal InputControlScheme.DeviceRequirement.Flags m_Flags;

			// Token: 0x02000273 RID: 627
			[Flags]
			internal enum Flags
			{
				// Token: 0x04000D02 RID: 3330
				None = 0,
				// Token: 0x04000D03 RID: 3331
				Optional = 1,
				// Token: 0x04000D04 RID: 3332
				Or = 2
			}
		}

		// Token: 0x02000188 RID: 392
		[Serializable]
		internal struct SchemeJson
		{
			// Token: 0x060013E1 RID: 5089 RVA: 0x0005C914 File Offset: 0x0005AB14
			public InputControlScheme ToScheme()
			{
				InputControlScheme.DeviceRequirement[] array = null;
				if (this.devices != null && this.devices.Length != 0)
				{
					int num = this.devices.Length;
					array = new InputControlScheme.DeviceRequirement[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = this.devices[i].ToDeviceEntry();
					}
				}
				return new InputControlScheme
				{
					m_Name = (string.IsNullOrEmpty(this.name) ? null : this.name),
					m_BindingGroup = (string.IsNullOrEmpty(this.bindingGroup) ? null : this.bindingGroup),
					m_DeviceRequirements = array
				};
			}

			// Token: 0x060013E2 RID: 5090 RVA: 0x0005C9B4 File Offset: 0x0005ABB4
			public static InputControlScheme.SchemeJson ToJson(InputControlScheme scheme)
			{
				InputControlScheme.SchemeJson.DeviceJson[] array = null;
				if (scheme.m_DeviceRequirements != null && scheme.m_DeviceRequirements.Length != 0)
				{
					int num = scheme.m_DeviceRequirements.Length;
					array = new InputControlScheme.SchemeJson.DeviceJson[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = InputControlScheme.SchemeJson.DeviceJson.From(scheme.m_DeviceRequirements[i]);
					}
				}
				return new InputControlScheme.SchemeJson
				{
					name = scheme.m_Name,
					bindingGroup = scheme.m_BindingGroup,
					devices = array
				};
			}

			// Token: 0x060013E3 RID: 5091 RVA: 0x0005CA34 File Offset: 0x0005AC34
			public static InputControlScheme.SchemeJson[] ToJson(InputControlScheme[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				InputControlScheme.SchemeJson[] array = new InputControlScheme.SchemeJson[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = InputControlScheme.SchemeJson.ToJson(schemes[i]);
				}
				return array;
			}

			// Token: 0x060013E4 RID: 5092 RVA: 0x0005CA78 File Offset: 0x0005AC78
			public static InputControlScheme[] ToSchemes(InputControlScheme.SchemeJson[] schemes)
			{
				if (schemes == null || schemes.Length == 0)
				{
					return null;
				}
				int num = schemes.Length;
				InputControlScheme[] array = new InputControlScheme[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = schemes[i].ToScheme();
				}
				return array;
			}

			// Token: 0x040008A1 RID: 2209
			public string name;

			// Token: 0x040008A2 RID: 2210
			public string bindingGroup;

			// Token: 0x040008A3 RID: 2211
			public InputControlScheme.SchemeJson.DeviceJson[] devices;

			// Token: 0x02000274 RID: 628
			[Serializable]
			public struct DeviceJson
			{
				// Token: 0x060016A0 RID: 5792 RVA: 0x00067A68 File Offset: 0x00065C68
				public InputControlScheme.DeviceRequirement ToDeviceEntry()
				{
					return new InputControlScheme.DeviceRequirement
					{
						controlPath = this.devicePath,
						isOptional = this.isOptional,
						isOR = this.isOR
					};
				}

				// Token: 0x060016A1 RID: 5793 RVA: 0x00067AA8 File Offset: 0x00065CA8
				public static InputControlScheme.SchemeJson.DeviceJson From(InputControlScheme.DeviceRequirement requirement)
				{
					return new InputControlScheme.SchemeJson.DeviceJson
					{
						devicePath = requirement.controlPath,
						isOptional = requirement.isOptional,
						isOR = requirement.isOR
					};
				}

				// Token: 0x04000D05 RID: 3333
				public string devicePath;

				// Token: 0x04000D06 RID: 3334
				public bool isOptional;

				// Token: 0x04000D07 RID: 3335
				public bool isOR;
			}
		}
	}
}
