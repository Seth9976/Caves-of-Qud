using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200007D RID: 125
	internal struct WordWrapState
	{
		// Token: 0x04000531 RID: 1329
		public int previous_WordBreak;

		// Token: 0x04000532 RID: 1330
		public int total_CharacterCount;

		// Token: 0x04000533 RID: 1331
		public int visible_CharacterCount;

		// Token: 0x04000534 RID: 1332
		public int visibleSpaceCount;

		// Token: 0x04000535 RID: 1333
		public int visible_SpriteCount;

		// Token: 0x04000536 RID: 1334
		public int visible_LinkCount;

		// Token: 0x04000537 RID: 1335
		public int firstCharacterIndex;

		// Token: 0x04000538 RID: 1336
		public int firstVisibleCharacterIndex;

		// Token: 0x04000539 RID: 1337
		public int lastCharacterIndex;

		// Token: 0x0400053A RID: 1338
		public int lastVisibleCharIndex;

		// Token: 0x0400053B RID: 1339
		public int lineNumber;

		// Token: 0x0400053C RID: 1340
		public float maxCapHeight;

		// Token: 0x0400053D RID: 1341
		public float maxAscender;

		// Token: 0x0400053E RID: 1342
		public float maxDescender;

		// Token: 0x0400053F RID: 1343
		public float startOfLineAscender;

		// Token: 0x04000540 RID: 1344
		public float maxLineAscender;

		// Token: 0x04000541 RID: 1345
		public float maxLineDescender;

		// Token: 0x04000542 RID: 1346
		public float pageAscender;

		// Token: 0x04000543 RID: 1347
		public HorizontalAlignmentOptions horizontalAlignment;

		// Token: 0x04000544 RID: 1348
		public float marginLeft;

		// Token: 0x04000545 RID: 1349
		public float marginRight;

		// Token: 0x04000546 RID: 1350
		public float xAdvance;

		// Token: 0x04000547 RID: 1351
		public float preferredWidth;

		// Token: 0x04000548 RID: 1352
		public float preferredHeight;

		// Token: 0x04000549 RID: 1353
		public float renderedWidth;

		// Token: 0x0400054A RID: 1354
		public float renderedHeight;

		// Token: 0x0400054B RID: 1355
		public float previousLineScale;

		// Token: 0x0400054C RID: 1356
		public int wordCount;

		// Token: 0x0400054D RID: 1357
		public FontStyles fontStyle;

		// Token: 0x0400054E RID: 1358
		public int italicAngle;

		// Token: 0x0400054F RID: 1359
		public float fontScaleMultiplier;

		// Token: 0x04000550 RID: 1360
		public float currentFontSize;

		// Token: 0x04000551 RID: 1361
		public float baselineOffset;

		// Token: 0x04000552 RID: 1362
		public float lineOffset;

		// Token: 0x04000553 RID: 1363
		public bool isDrivenLineSpacing;

		// Token: 0x04000554 RID: 1364
		public int lastBaseGlyphIndex;

		// Token: 0x04000555 RID: 1365
		public float cSpace;

		// Token: 0x04000556 RID: 1366
		public float mSpace;

		// Token: 0x04000557 RID: 1367
		public TMP_TextInfo textInfo;

		// Token: 0x04000558 RID: 1368
		public TMP_LineInfo lineInfo;

		// Token: 0x04000559 RID: 1369
		public Color32 vertexColor;

		// Token: 0x0400055A RID: 1370
		public Color32 underlineColor;

		// Token: 0x0400055B RID: 1371
		public Color32 strikethroughColor;

		// Token: 0x0400055C RID: 1372
		public HighlightState highlightState;

		// Token: 0x0400055D RID: 1373
		public TMP_FontStyleStack basicStyleStack;

		// Token: 0x0400055E RID: 1374
		public TMP_TextProcessingStack<int> italicAngleStack;

		// Token: 0x0400055F RID: 1375
		public TMP_TextProcessingStack<Color32> colorStack;

		// Token: 0x04000560 RID: 1376
		public TMP_TextProcessingStack<Color32> underlineColorStack;

		// Token: 0x04000561 RID: 1377
		public TMP_TextProcessingStack<Color32> strikethroughColorStack;

		// Token: 0x04000562 RID: 1378
		public TMP_TextProcessingStack<Color32> highlightColorStack;

		// Token: 0x04000563 RID: 1379
		public TMP_TextProcessingStack<HighlightState> highlightStateStack;

		// Token: 0x04000564 RID: 1380
		public TMP_TextProcessingStack<TMP_ColorGradient> colorGradientStack;

		// Token: 0x04000565 RID: 1381
		public TMP_TextProcessingStack<float> sizeStack;

		// Token: 0x04000566 RID: 1382
		public TMP_TextProcessingStack<float> indentStack;

		// Token: 0x04000567 RID: 1383
		public TMP_TextProcessingStack<FontWeight> fontWeightStack;

		// Token: 0x04000568 RID: 1384
		public TMP_TextProcessingStack<int> styleStack;

		// Token: 0x04000569 RID: 1385
		public TMP_TextProcessingStack<float> baselineStack;

		// Token: 0x0400056A RID: 1386
		public TMP_TextProcessingStack<int> actionStack;

		// Token: 0x0400056B RID: 1387
		public TMP_TextProcessingStack<MaterialReference> materialReferenceStack;

		// Token: 0x0400056C RID: 1388
		public TMP_TextProcessingStack<HorizontalAlignmentOptions> lineJustificationStack;

		// Token: 0x0400056D RID: 1389
		public int spriteAnimationID;

		// Token: 0x0400056E RID: 1390
		public TMP_FontAsset currentFontAsset;

		// Token: 0x0400056F RID: 1391
		public TMP_SpriteAsset currentSpriteAsset;

		// Token: 0x04000570 RID: 1392
		public Material currentMaterial;

		// Token: 0x04000571 RID: 1393
		public int currentMaterialIndex;

		// Token: 0x04000572 RID: 1394
		public Extents meshExtents;

		// Token: 0x04000573 RID: 1395
		public bool tagNoParsing;

		// Token: 0x04000574 RID: 1396
		public bool isNonBreakingSpace;

		// Token: 0x04000575 RID: 1397
		public Quaternion fxRotation;

		// Token: 0x04000576 RID: 1398
		public Vector3 fxScale;
	}
}
