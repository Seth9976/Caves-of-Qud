using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x02000587 RID: 1415
	[Serializable]
	public abstract class UxmlSerializedData
	{
		// Token: 0x06002A6E RID: 10862 RVA: 0x00002152 File Offset: 0x00000352
		public static void Register()
		{
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x00098BC0 File Offset: 0x00096DC0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ShouldWriteAttributeValue(UxmlSerializedData.UxmlAttributeFlags attributeFlags)
		{
			return (attributeFlags & UxmlSerializedData.s_CurrentDeserializeFlags) > UxmlSerializedData.UxmlAttributeFlags.Ignore;
		}

		// Token: 0x06002A70 RID: 10864
		public abstract object CreateInstance();

		// Token: 0x06002A71 RID: 10865
		public abstract void Deserialize(object obj);

		// Token: 0x06002A72 RID: 10866 RVA: 0x00098BDC File Offset: 0x00096DDC
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void Deserialize(object obj, UxmlSerializedData.UxmlAttributeFlags flags)
		{
			try
			{
				UxmlSerializedData.s_CurrentDeserializeFlags = flags;
				this.Deserialize(obj);
			}
			finally
			{
				UxmlSerializedData.s_CurrentDeserializeFlags = UxmlSerializedData.UxmlAttributeFlags.OverriddenInUxml;
			}
		}

		// Token: 0x04001306 RID: 4870
		internal const string AttributeFlagSuffix = "_UxmlAttributeFlags";

		// Token: 0x04001307 RID: 4871
		private const UxmlSerializedData.UxmlAttributeFlags k_DefaultFlags = UxmlSerializedData.UxmlAttributeFlags.OverriddenInUxml;

		// Token: 0x04001308 RID: 4872
		[HideInInspector]
		[UxmlIgnore]
		[SerializeField]
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal int uxmlAssetId;

		// Token: 0x04001309 RID: 4873
		private static UxmlSerializedData.UxmlAttributeFlags s_CurrentDeserializeFlags = UxmlSerializedData.UxmlAttributeFlags.OverriddenInUxml;

		// Token: 0x02000588 RID: 1416
		[Flags]
		public enum UxmlAttributeFlags : byte
		{
			// Token: 0x0400130B RID: 4875
			Ignore = 0,
			// Token: 0x0400130C RID: 4876
			OverriddenInUxml = 1,
			// Token: 0x0400130D RID: 4877
			DefaultValue = 2
		}
	}
}
