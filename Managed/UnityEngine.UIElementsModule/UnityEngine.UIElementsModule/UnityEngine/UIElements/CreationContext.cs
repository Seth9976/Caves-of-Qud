using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200059E RID: 1438
	public struct CreationContext : IEquatable<CreationContext>
	{
		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0009C363 File Offset: 0x0009A563
		// (set) Token: 0x06002B25 RID: 11045 RVA: 0x0009C36B File Offset: 0x0009A56B
		public VisualElement target { readonly get; private set; }

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0009C374 File Offset: 0x0009A574
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x0009C37C File Offset: 0x0009A57C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal List<int> veaIdsPath
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			readonly get;
			private set; }

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x0009C385 File Offset: 0x0009A585
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x0009C38D File Offset: 0x0009A58D
		public VisualTreeAsset visualTreeAsset { readonly get; private set; }

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x0009C396 File Offset: 0x0009A596
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x0009C39E File Offset: 0x0009A59E
		public Dictionary<string, VisualElement> slotInsertionPoints { readonly get; private set; }

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x0009C3A7 File Offset: 0x0009A5A7
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x0009C3AF File Offset: 0x0009A5AF
		internal List<CreationContext.AttributeOverrideRange> attributeOverrides { readonly get; private set; }

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x0009C3B8 File Offset: 0x0009A5B8
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x0009C3C0 File Offset: 0x0009A5C0
		internal List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides { readonly get; private set; }

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x0009C3C9 File Offset: 0x0009A5C9
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x0009C3D1 File Offset: 0x0009A5D1
		internal List<string> namesPath { readonly get; private set; }

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x0009C3DA File Offset: 0x0009A5DA
		internal bool hasOverrides
		{
			get
			{
				List<CreationContext.AttributeOverrideRange> attributeOverrides = this.attributeOverrides;
				bool flag;
				if (attributeOverrides == null || attributeOverrides.Count <= 0)
				{
					List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides = this.serializedDataOverrides;
					flag = serializedDataOverrides != null && serializedDataOverrides.Count > 0;
				}
				else
				{
					flag = true;
				}
				return flag;
			}
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x0009C40B File Offset: 0x0009A60B
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(VisualTreeAsset vta)
		{
			this = new CreationContext(null, vta, null);
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x0009C418 File Offset: 0x0009A618
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints)
		{
			this = new CreationContext(slotInsertionPoints, null, null, null);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x0009C426 File Offset: 0x0009A626
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides)
		{
			this = new CreationContext(slotInsertionPoints, attributeOverrides, null, null);
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x0009C434 File Offset: 0x0009A634
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, null, vta, target);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x0009C442 File Offset: 0x0009A642
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, attributeOverrides, null, vta, target, null, null);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x0009C454 File Offset: 0x0009A654
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<CreationContext.AttributeOverrideRange> attributeOverrides, List<CreationContext.SerializedDataOverrideRange> serializedDataOverrides, VisualTreeAsset vta, VisualElement target, List<int> veaIdsPath, List<string> namesPath)
		{
			this.target = target;
			this.slotInsertionPoints = slotInsertionPoints;
			this.attributeOverrides = attributeOverrides;
			this.serializedDataOverrides = serializedDataOverrides;
			this.visualTreeAsset = vta;
			this.namesPath = namesPath;
			this.veaIdsPath = veaIdsPath;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x0009C494 File Offset: 0x0009A694
		public override bool Equals(object obj)
		{
			return obj is CreationContext && this.Equals((CreationContext)obj);
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x0009C4C0 File Offset: 0x0009A6C0
		public bool Equals(CreationContext other)
		{
			return EqualityComparer<VisualElement>.Default.Equals(this.target, other.target) && EqualityComparer<VisualTreeAsset>.Default.Equals(this.visualTreeAsset, other.visualTreeAsset) && EqualityComparer<Dictionary<string, VisualElement>>.Default.Equals(this.slotInsertionPoints, other.slotInsertionPoints);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x0009C520 File Offset: 0x0009A720
		public override int GetHashCode()
		{
			int num = -2123482148;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.target);
			num = num * -1521134295 + EqualityComparer<VisualTreeAsset>.Default.GetHashCode(this.visualTreeAsset);
			return num * -1521134295 + EqualityComparer<Dictionary<string, VisualElement>>.Default.GetHashCode(this.slotInsertionPoints);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x0009C584 File Offset: 0x0009A784
		public static bool operator ==(CreationContext context1, CreationContext context2)
		{
			return context1.Equals(context2);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x0009C5A0 File Offset: 0x0009A7A0
		public static bool operator !=(CreationContext context1, CreationContext context2)
		{
			return !(context1 == context2);
		}

		// Token: 0x04001358 RID: 4952
		public static readonly CreationContext Default = default(CreationContext);

		// Token: 0x0200059F RID: 1439
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal struct AttributeOverrideRange
		{
			// Token: 0x06002B3F RID: 11071 RVA: 0x0009C5C9 File Offset: 0x0009A7C9
			public AttributeOverrideRange(VisualTreeAsset sourceAsset, List<TemplateAsset.AttributeOverride> attributeOverrides)
			{
				this.sourceAsset = sourceAsset;
				this.attributeOverrides = attributeOverrides;
			}

			// Token: 0x04001360 RID: 4960
			internal readonly VisualTreeAsset sourceAsset;

			// Token: 0x04001361 RID: 4961
			internal readonly List<TemplateAsset.AttributeOverride> attributeOverrides;
		}

		// Token: 0x020005A0 RID: 1440
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal struct SerializedDataOverrideRange
		{
			// Token: 0x06002B40 RID: 11072 RVA: 0x0009C5DA File Offset: 0x0009A7DA
			public SerializedDataOverrideRange(VisualTreeAsset sourceAsset, List<TemplateAsset.UxmlSerializedDataOverride> attributeOverrides, int templateId)
			{
				this.sourceAsset = sourceAsset;
				this.attributeOverrides = attributeOverrides;
				this.templateId = templateId;
			}

			// Token: 0x04001362 RID: 4962
			internal readonly VisualTreeAsset sourceAsset;

			// Token: 0x04001363 RID: 4963
			internal readonly int templateId;

			// Token: 0x04001364 RID: 4964
			internal readonly List<TemplateAsset.UxmlSerializedDataOverride> attributeOverrides;
		}
	}
}
