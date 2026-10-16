using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200002F RID: 47
	[DebuggerDisplay("{DebuggerDisplay(),nq}")]
	public abstract class InputControl
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0001020A File Offset: 0x0000E40A
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00010217 File Offset: 0x0000E417
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x00010248 File Offset: 0x0000E448
		public string displayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_DisplayName != null)
				{
					return this.m_DisplayName;
				}
				if (this.m_DisplayNameFromLayout != null)
				{
					return this.m_DisplayNameFromLayout;
				}
				return this.m_Name;
			}
			protected set
			{
				this.m_DisplayName = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00010251 File Offset: 0x0000E451
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x00010278 File Offset: 0x0000E478
		public string shortDisplayName
		{
			get
			{
				this.RefreshConfigurationIfNeeded();
				if (this.m_ShortDisplayName != null)
				{
					return this.m_ShortDisplayName;
				}
				if (this.m_ShortDisplayNameFromLayout != null)
				{
					return this.m_ShortDisplayNameFromLayout;
				}
				return null;
			}
			protected set
			{
				this.m_ShortDisplayName = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00010281 File Offset: 0x0000E481
		public string path
		{
			get
			{
				if (this.m_Path == null)
				{
					this.m_Path = InputControlPath.Combine(this.m_Parent, this.m_Name);
				}
				return this.m_Path;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x000102AD File Offset: 0x0000E4AD
		public string layout
		{
			get
			{
				return this.m_Layout;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x000102BA File Offset: 0x0000E4BA
		public string variants
		{
			get
			{
				return this.m_Variants;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x000102C7 File Offset: 0x0000E4C7
		public InputDevice device
		{
			get
			{
				return this.m_Device;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000102CF File Offset: 0x0000E4CF
		public InputControl parent
		{
			get
			{
				return this.m_Parent;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000102D7 File Offset: 0x0000E4D7
		public ReadOnlyArray<InputControl> children
		{
			get
			{
				return new ReadOnlyArray<InputControl>(this.m_Device.m_ChildrenForEachControl, this.m_ChildStartIndex, this.m_ChildCount);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000102F5 File Offset: 0x0000E4F5
		public ReadOnlyArray<InternedString> usages
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_UsagesForEachControl, this.m_UsageStartIndex, this.m_UsageCount);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003AB RID: 939 RVA: 0x00010313 File Offset: 0x0000E513
		public ReadOnlyArray<InternedString> aliases
		{
			get
			{
				return new ReadOnlyArray<InternedString>(this.m_Device.m_AliasesForEachControl, this.m_AliasStartIndex, this.m_AliasCount);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00010331 File Offset: 0x0000E531
		public InputStateBlock stateBlock
		{
			get
			{
				return this.m_StateBlock;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00010339 File Offset: 0x0000E539
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00010348 File Offset: 0x0000E548
		public bool noisy
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsNoisy) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsNoisy;
					ReadOnlyArray<InputControl> children = this.children;
					for (int i = 0; i < children.Count; i++)
					{
						if (children[i] != null)
						{
							children[i].noisy = true;
						}
					}
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsNoisy;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000103A7 File Offset: 0x0000E5A7
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x000103B4 File Offset: 0x0000E5B4
		public bool synthetic
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsSynthetic) > (InputControl.ControlFlags)0;
			}
			internal set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsSynthetic;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsSynthetic;
			}
		}

		// Token: 0x17000108 RID: 264
		public InputControl this[string path]
		{
			get
			{
				InputControl inputControl = InputControlPath.TryFindChild(this, path, 0);
				if (inputControl == null)
				{
					throw new KeyNotFoundException(string.Format("Cannot find control '{0}' as child of '{1}'", path, this));
				}
				return inputControl;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003B2 RID: 946
		public abstract Type valueType { get; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003B3 RID: 947
		public abstract int valueSizeInBytes { get; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000103F6 File Offset: 0x0000E5F6
		public float magnitude
		{
			get
			{
				return this.EvaluateMagnitude();
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000103FE File Offset: 0x0000E5FE
		public override string ToString()
		{
			return this.layout + ":" + this.path;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010418 File Offset: 0x0000E618
		private string DebuggerDisplay()
		{
			if (!this.device.added)
			{
				return this.ToString();
			}
			string text;
			try
			{
				text = string.Format("{0}:{1}={2}", this.layout, this.path, this.ReadValueAsObject());
			}
			catch (Exception)
			{
				text = this.ToString();
			}
			return text;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00010474 File Offset: 0x0000E674
		public float EvaluateMagnitude()
		{
			return this.EvaluateMagnitude(this.currentStatePtr);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00010482 File Offset: 0x0000E682
		public unsafe virtual float EvaluateMagnitude(void* statePtr)
		{
			return -1f;
		}

		// Token: 0x060003B9 RID: 953
		public unsafe abstract object ReadValueFromBufferAsObject(void* buffer, int bufferSize);

		// Token: 0x060003BA RID: 954
		public unsafe abstract object ReadValueFromStateAsObject(void* statePtr);

		// Token: 0x060003BB RID: 955
		public unsafe abstract void ReadValueFromStateIntoBuffer(void* statePtr, void* bufferPtr, int bufferSize);

		// Token: 0x060003BC RID: 956 RVA: 0x00010489 File Offset: 0x0000E689
		public unsafe virtual void WriteValueFromBufferIntoState(void* bufferPtr, int bufferSize, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001049B File Offset: 0x0000E69B
		public unsafe virtual void WriteValueFromObjectIntoState(object value, void* statePtr)
		{
			throw new NotSupportedException(string.Format("Control '{0}' does not support writing", this));
		}

		// Token: 0x060003BE RID: 958
		public unsafe abstract bool CompareValue(void* firstStatePtr, void* secondStatePtr);

		// Token: 0x060003BF RID: 959 RVA: 0x000104AD File Offset: 0x0000E6AD
		public InputControl TryGetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			return InputControlPath.TryFindChild(this, path, 0);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000104CC File Offset: 0x0000E6CC
		public TControl TryGetChildControl<TControl>(string path) where TControl : InputControl
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl inputControl = this.TryGetChildControl(path);
			if (inputControl == null)
			{
				return default(TControl);
			}
			TControl tcontrol = inputControl as TControl;
			if (tcontrol == null)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					inputControl.GetType().Name,
					"' instead!"
				}));
			}
			return tcontrol;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001056B File Offset: 0x0000E76B
		public InputControl GetChildControl(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}
			InputControl inputControl = this.TryGetChildControl(path);
			if (inputControl == null)
			{
				throw new ArgumentException("Cannot find input control '" + this.MakeChildPath(path) + "'", "path");
			}
			return inputControl;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000105AC File Offset: 0x0000E7AC
		public TControl GetChildControl<TControl>(string path) where TControl : InputControl
		{
			InputControl childControl = this.GetChildControl(path);
			TControl tcontrol = childControl as TControl;
			if (tcontrol == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Expected control '",
					path,
					"' to be of type '",
					typeof(TControl).Name,
					"' but is of type '",
					childControl.GetType().Name,
					"' instead!"
				}), "path");
			}
			return tcontrol;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010630 File Offset: 0x0000E830
		protected InputControl()
		{
			this.m_StateBlock.byteOffset = 4294967294U;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010653 File Offset: 0x0000E853
		protected virtual void FinishSetup()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010655 File Offset: 0x0000E855
		protected void RefreshConfigurationIfNeeded()
		{
			if (!this.isConfigUpToDate)
			{
				this.RefreshConfiguration();
				this.isConfigUpToDate = true;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001066C File Offset: 0x0000E86C
		protected virtual void RefreshConfiguration()
		{
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0001066E File Offset: 0x0000E86E
		protected internal unsafe void* currentStatePtr
		{
			get
			{
				return InputStateBuffers.GetFrontBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001067B File Offset: 0x0000E87B
		protected internal unsafe void* previousFrameStatePtr
		{
			get
			{
				return InputStateBuffers.GetBackBufferForDevice(this.GetDeviceIndex());
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00010688 File Offset: 0x0000E888
		protected internal unsafe void* defaultStatePtr
		{
			get
			{
				return InputStateBuffers.s_DefaultStateBuffer;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001068F File Offset: 0x0000E88F
		protected internal unsafe void* noiseMaskPtr
		{
			get
			{
				return InputStateBuffers.s_NoiseMaskBuffer;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00010698 File Offset: 0x0000E898
		protected internal uint stateOffsetRelativeToDeviceRoot
		{
			get
			{
				uint byteOffset = this.device.m_StateBlock.byteOffset;
				return this.m_StateBlock.byteOffset - byteOffset;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000106C3 File Offset: 0x0000E8C3
		public FourCC optimizedControlDataType
		{
			get
			{
				return this.m_OptimizedControlDataType;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000106CB File Offset: 0x0000E8CB
		protected virtual FourCC CalculateOptimizedControlDataType()
		{
			return 0;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000106D4 File Offset: 0x0000E8D4
		public void ApplyParameterChanges()
		{
			this.SetOptimizedControlDataTypeRecursively();
			for (InputControl inputControl = this.parent; inputControl != null; inputControl = inputControl.parent)
			{
				inputControl.SetOptimizedControlDataType();
			}
			this.MarkAsStaleRecursively();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00010706 File Offset: 0x0000E906
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void SetOptimizedControlDataType()
		{
			this.m_OptimizedControlDataType = (InputSystem.s_Manager.optimizedControlsFeatureEnabled ? this.CalculateOptimizedControlDataType() : 0);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00010728 File Offset: 0x0000E928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void SetOptimizedControlDataTypeRecursively()
		{
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
					inputControl.SetOptimizedControlDataTypeRecursively();
				}
			}
			this.SetOptimizedControlDataType();
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001078C File Offset: 0x0000E98C
		[Conditional("UNITY_EDITOR")]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void EnsureOptimizationTypeHasNotChanged()
		{
			if (!InputSystem.s_Manager.optimizedControlsFeatureEnabled)
			{
				return;
			}
			FourCC fourCC = this.CalculateOptimizedControlDataType();
			if (fourCC != this.optimizedControlDataType)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Control '",
					this.name,
					"' / '",
					this.path,
					"' suddenly changed optimization state due to either format ",
					string.Format("change or control parameters change (was '{0}' but became '{1}'), ", this.optimizedControlDataType, fourCC),
					"this hinders control hot path optimization, please call control.ApplyParameterChanges() after the changes to the control to fix this error."
				}));
				this.m_OptimizedControlDataType = fourCC;
			}
			if (this.m_ChildCount > 0)
			{
				foreach (InputControl inputControl in this.children)
				{
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00010870 File Offset: 0x0000EA70
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0001087F File Offset: 0x0000EA7F
		internal bool isSetupFinished
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.SetupFinished) == InputControl.ControlFlags.SetupFinished;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.SetupFinished;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.SetupFinished;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000108A3 File Offset: 0x0000EAA3
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x000108B0 File Offset: 0x0000EAB0
		internal bool isButton
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.IsButton) == InputControl.ControlFlags.IsButton;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.IsButton;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.IsButton;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000108D3 File Offset: 0x0000EAD3
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x000108E0 File Offset: 0x0000EAE0
		internal bool isConfigUpToDate
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.ConfigUpToDate) == InputControl.ControlFlags.ConfigUpToDate;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.ConfigUpToDate;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.ConfigUpToDate;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00010903 File Offset: 0x0000EB03
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00010912 File Offset: 0x0000EB12
		internal bool dontReset
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.DontReset) == InputControl.ControlFlags.DontReset;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.DontReset;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.DontReset;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003DA RID: 986 RVA: 0x00010936 File Offset: 0x0000EB36
		// (set) Token: 0x060003DB RID: 987 RVA: 0x00010945 File Offset: 0x0000EB45
		internal bool usesStateFromOtherControl
		{
			get
			{
				return (this.m_ControlFlags & InputControl.ControlFlags.UsesStateFromOtherControl) == InputControl.ControlFlags.UsesStateFromOtherControl;
			}
			set
			{
				if (value)
				{
					this.m_ControlFlags |= InputControl.ControlFlags.UsesStateFromOtherControl;
					return;
				}
				this.m_ControlFlags &= ~InputControl.ControlFlags.UsesStateFromOtherControl;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003DC RID: 988 RVA: 0x00010969 File Offset: 0x0000EB69
		internal bool hasDefaultState
		{
			get
			{
				return !this.m_DefaultState.isEmpty;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001097C File Offset: 0x0000EB7C
		internal void CallFinishSetupRecursive()
		{
			ReadOnlyArray<InputControl> children = this.children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].CallFinishSetupRecursive();
			}
			this.FinishSetup();
			this.SetOptimizedControlDataTypeRecursively();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000109BB File Offset: 0x0000EBBB
		internal string MakeChildPath(string path)
		{
			if (this is InputDevice)
			{
				return path;
			}
			return this.path + "/" + path;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x000109D8 File Offset: 0x0000EBD8
		internal void BakeOffsetIntoStateBlockRecursive(uint offset)
		{
			this.m_StateBlock.byteOffset = this.m_StateBlock.byteOffset + offset;
			ReadOnlyArray<InputControl> children = this.children;
			for (int i = 0; i < children.Count; i++)
			{
				children[i].BakeOffsetIntoStateBlockRecursive(offset);
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00010A20 File Offset: 0x0000EC20
		internal int GetDeviceIndex()
		{
			int deviceIndex = this.m_Device.m_DeviceIndex;
			if (deviceIndex == -1)
			{
				throw new InvalidOperationException(string.Concat(new string[]
				{
					"Cannot query value of control '",
					this.path,
					"' before '",
					this.device.name,
					"' has been added to system!"
				}));
			}
			return deviceIndex;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00010A7E File Offset: 0x0000EC7E
		internal bool IsValueConsideredPressed(float value)
		{
			if (this.isButton)
			{
				return ((ButtonControl)this).IsValueConsideredPressed(value);
			}
			return value >= ButtonControl.s_GlobalDefaultButtonPressPoint;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		internal virtual void AddProcessor(object first)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00010AA2 File Offset: 0x0000ECA2
		internal void MarkAsStale()
		{
			this.m_CachedValueIsStale = true;
			this.m_UnprocessedCachedValueIsStale = true;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00010AB4 File Offset: 0x0000ECB4
		internal void MarkAsStaleRecursively()
		{
			this.MarkAsStale();
			foreach (InputControl inputControl in this.children)
			{
				inputControl.MarkAsStale();
				ButtonControl buttonControl = inputControl as ButtonControl;
				if (buttonControl != null)
				{
					buttonControl.UpdateWasPressed();
				}
			}
		}

		// Token: 0x04000125 RID: 293
		protected internal InputStateBlock m_StateBlock;

		// Token: 0x04000126 RID: 294
		internal InternedString m_Name;

		// Token: 0x04000127 RID: 295
		internal string m_Path;

		// Token: 0x04000128 RID: 296
		internal string m_DisplayName;

		// Token: 0x04000129 RID: 297
		internal string m_DisplayNameFromLayout;

		// Token: 0x0400012A RID: 298
		internal string m_ShortDisplayName;

		// Token: 0x0400012B RID: 299
		internal string m_ShortDisplayNameFromLayout;

		// Token: 0x0400012C RID: 300
		internal InternedString m_Layout;

		// Token: 0x0400012D RID: 301
		internal InternedString m_Variants;

		// Token: 0x0400012E RID: 302
		internal InputDevice m_Device;

		// Token: 0x0400012F RID: 303
		internal InputControl m_Parent;

		// Token: 0x04000130 RID: 304
		internal int m_UsageCount;

		// Token: 0x04000131 RID: 305
		internal int m_UsageStartIndex;

		// Token: 0x04000132 RID: 306
		internal int m_AliasCount;

		// Token: 0x04000133 RID: 307
		internal int m_AliasStartIndex;

		// Token: 0x04000134 RID: 308
		internal int m_ChildCount;

		// Token: 0x04000135 RID: 309
		internal int m_ChildStartIndex;

		// Token: 0x04000136 RID: 310
		internal InputControl.ControlFlags m_ControlFlags;

		// Token: 0x04000137 RID: 311
		internal bool m_CachedValueIsStale = true;

		// Token: 0x04000138 RID: 312
		internal bool m_UnprocessedCachedValueIsStale = true;

		// Token: 0x04000139 RID: 313
		internal PrimitiveValue m_DefaultState;

		// Token: 0x0400013A RID: 314
		internal PrimitiveValue m_MinValue;

		// Token: 0x0400013B RID: 315
		internal PrimitiveValue m_MaxValue;

		// Token: 0x0400013C RID: 316
		internal FourCC m_OptimizedControlDataType;

		// Token: 0x0200018D RID: 397
		[Flags]
		internal enum ControlFlags
		{
			// Token: 0x040008B0 RID: 2224
			ConfigUpToDate = 1,
			// Token: 0x040008B1 RID: 2225
			IsNoisy = 2,
			// Token: 0x040008B2 RID: 2226
			IsSynthetic = 4,
			// Token: 0x040008B3 RID: 2227
			IsButton = 8,
			// Token: 0x040008B4 RID: 2228
			DontReset = 16,
			// Token: 0x040008B5 RID: 2229
			SetupFinished = 32,
			// Token: 0x040008B6 RID: 2230
			UsesStateFromOtherControl = 64
		}
	}
}
