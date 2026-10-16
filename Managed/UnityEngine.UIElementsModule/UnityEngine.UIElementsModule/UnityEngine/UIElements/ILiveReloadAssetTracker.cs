using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002D7 RID: 727
	internal interface ILiveReloadAssetTracker<T> where T : ScriptableObject
	{
		// Token: 0x060014D1 RID: 5329
		int StartTrackingAsset(T asset);

		// Token: 0x060014D2 RID: 5330
		void StopTrackingAsset(T asset);

		// Token: 0x060014D3 RID: 5331
		bool IsTrackingAsset(T asset);

		// Token: 0x060014D4 RID: 5332
		bool IsTrackingAssets();

		// Token: 0x060014D5 RID: 5333
		bool CheckTrackedAssetsDirty();

		// Token: 0x060014D6 RID: 5334
		void UpdateAssetTrackerCounts(T asset, int newDirtyCount, int newElementCount, int newInlinePropertiesCount, int newAttributePropertiesDirtyCount);

		// Token: 0x060014D7 RID: 5335
		bool OnAssetsImported(HashSet<T> changedAssets, HashSet<string> deletedAssets);

		// Token: 0x060014D8 RID: 5336
		void OnTrackedAssetChanged();
	}
}
