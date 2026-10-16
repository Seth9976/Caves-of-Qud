using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.Layout;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x020003CF RID: 975
	public class VisualElement : Focusable, IResolvedStyle, IStylePropertyAnimations, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x00070225 File Offset: 0x0006E425
		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.resolvedStyle.alignContent;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x00070232 File Offset: 0x0006E432
		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.resolvedStyle.alignItems;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x0007023F File Offset: 0x0006E43F
		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.resolvedStyle.alignSelf;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x0007024C File Offset: 0x0006E44C
		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.resolvedStyle.backgroundColor;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00070259 File Offset: 0x0006E459
		Background IResolvedStyle.backgroundImage
		{
			get
			{
				return this.resolvedStyle.backgroundImage;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x00070266 File Offset: 0x0006E466
		BackgroundPosition IResolvedStyle.backgroundPositionX
		{
			get
			{
				return this.resolvedStyle.backgroundPositionX;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x00070273 File Offset: 0x0006E473
		BackgroundPosition IResolvedStyle.backgroundPositionY
		{
			get
			{
				return this.resolvedStyle.backgroundPositionY;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x00070280 File Offset: 0x0006E480
		BackgroundRepeat IResolvedStyle.backgroundRepeat
		{
			get
			{
				return this.resolvedStyle.backgroundRepeat;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x0007028D File Offset: 0x0006E48D
		BackgroundSize IResolvedStyle.backgroundSize
		{
			get
			{
				return this.resolvedStyle.backgroundSize;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x0007029A File Offset: 0x0006E49A
		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.resolvedStyle.borderBottomColor;
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x000702A7 File Offset: 0x0006E4A7
		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.resolvedStyle.borderBottomLeftRadius;
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000702B4 File Offset: 0x0006E4B4
		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.resolvedStyle.borderBottomRightRadius;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001DD5 RID: 7637 RVA: 0x000702C1 File Offset: 0x0006E4C1
		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.resolvedStyle.borderBottomWidth;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001DD6 RID: 7638 RVA: 0x000702CE File Offset: 0x0006E4CE
		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.resolvedStyle.borderLeftColor;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x000702DB File Offset: 0x0006E4DB
		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.resolvedStyle.borderLeftWidth;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001DD8 RID: 7640 RVA: 0x000702E8 File Offset: 0x0006E4E8
		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.resolvedStyle.borderRightColor;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001DD9 RID: 7641 RVA: 0x000702F5 File Offset: 0x0006E4F5
		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.resolvedStyle.borderRightWidth;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001DDA RID: 7642 RVA: 0x00070302 File Offset: 0x0006E502
		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.resolvedStyle.borderTopColor;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001DDB RID: 7643 RVA: 0x0007030F File Offset: 0x0006E50F
		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.resolvedStyle.borderTopLeftRadius;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001DDC RID: 7644 RVA: 0x0007031C File Offset: 0x0006E51C
		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.resolvedStyle.borderTopRightRadius;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001DDD RID: 7645 RVA: 0x00070329 File Offset: 0x0006E529
		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.resolvedStyle.borderTopWidth;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001DDE RID: 7646 RVA: 0x00070336 File Offset: 0x0006E536
		float IResolvedStyle.bottom
		{
			get
			{
				return this.resolvedStyle.bottom;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001DDF RID: 7647 RVA: 0x00070343 File Offset: 0x0006E543
		Color IResolvedStyle.color
		{
			get
			{
				return this.resolvedStyle.color;
			}
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x00070350 File Offset: 0x0006E550
		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.resolvedStyle.display;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001DE1 RID: 7649 RVA: 0x0007035D File Offset: 0x0006E55D
		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return this.resolvedStyle.flexBasis;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001DE2 RID: 7650 RVA: 0x0007036A File Offset: 0x0006E56A
		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.resolvedStyle.flexDirection;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001DE3 RID: 7651 RVA: 0x00070377 File Offset: 0x0006E577
		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.resolvedStyle.flexGrow;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x00070384 File Offset: 0x0006E584
		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.resolvedStyle.flexShrink;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x00070391 File Offset: 0x0006E591
		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.resolvedStyle.flexWrap;
			}
		}

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0007039E File Offset: 0x0006E59E
		float IResolvedStyle.fontSize
		{
			get
			{
				return this.resolvedStyle.fontSize;
			}
		}

		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x06001DE7 RID: 7655 RVA: 0x000703AB File Offset: 0x0006E5AB
		float IResolvedStyle.height
		{
			get
			{
				return this.resolvedStyle.height;
			}
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x000703B8 File Offset: 0x0006E5B8
		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.resolvedStyle.justifyContent;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x06001DE9 RID: 7657 RVA: 0x000703C5 File Offset: 0x0006E5C5
		float IResolvedStyle.left
		{
			get
			{
				return this.resolvedStyle.left;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x000703D2 File Offset: 0x0006E5D2
		float IResolvedStyle.letterSpacing
		{
			get
			{
				return this.resolvedStyle.letterSpacing;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001DEB RID: 7659 RVA: 0x000703DF File Offset: 0x0006E5DF
		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.resolvedStyle.marginBottom;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x000703EC File Offset: 0x0006E5EC
		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.resolvedStyle.marginLeft;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001DED RID: 7661 RVA: 0x000703F9 File Offset: 0x0006E5F9
		float IResolvedStyle.marginRight
		{
			get
			{
				return this.resolvedStyle.marginRight;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00070406 File Offset: 0x0006E606
		float IResolvedStyle.marginTop
		{
			get
			{
				return this.resolvedStyle.marginTop;
			}
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001DEF RID: 7663 RVA: 0x00070413 File Offset: 0x0006E613
		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.resolvedStyle.maxHeight;
			}
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00070420 File Offset: 0x0006E620
		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.resolvedStyle.maxWidth;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x0007042D File Offset: 0x0006E62D
		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.resolvedStyle.minHeight;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0007043A File Offset: 0x0006E63A
		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.resolvedStyle.minWidth;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x00070447 File Offset: 0x0006E647
		float IResolvedStyle.opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00070454 File Offset: 0x0006E654
		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.resolvedStyle.paddingBottom;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x00070461 File Offset: 0x0006E661
		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.resolvedStyle.paddingLeft;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0007046E File Offset: 0x0006E66E
		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.resolvedStyle.paddingRight;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001DF7 RID: 7671 RVA: 0x0007047B File Offset: 0x0006E67B
		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.resolvedStyle.paddingTop;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x00070488 File Offset: 0x0006E688
		Position IResolvedStyle.position
		{
			get
			{
				return this.resolvedStyle.position;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00070495 File Offset: 0x0006E695
		float IResolvedStyle.right
		{
			get
			{
				return this.resolvedStyle.right;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x000704A2 File Offset: 0x0006E6A2
		Rotate IResolvedStyle.rotate
		{
			get
			{
				return this.resolvedStyle.rotate;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x000704AF File Offset: 0x0006E6AF
		Scale IResolvedStyle.scale
		{
			get
			{
				return this.resolvedStyle.scale;
			}
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x000704BC File Offset: 0x0006E6BC
		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.resolvedStyle.textOverflow;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000704C9 File Offset: 0x0006E6C9
		float IResolvedStyle.top
		{
			get
			{
				return this.resolvedStyle.top;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x000704D6 File Offset: 0x0006E6D6
		Vector3 IResolvedStyle.transformOrigin
		{
			get
			{
				return this.resolvedStyle.transformOrigin;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000704E3 File Offset: 0x0006E6E3
		IEnumerable<TimeValue> IResolvedStyle.transitionDelay
		{
			get
			{
				return this.resolvedStyle.transitionDelay;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x000704F0 File Offset: 0x0006E6F0
		IEnumerable<TimeValue> IResolvedStyle.transitionDuration
		{
			get
			{
				return this.resolvedStyle.transitionDuration;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000704FD File Offset: 0x0006E6FD
		IEnumerable<StylePropertyName> IResolvedStyle.transitionProperty
		{
			get
			{
				return this.resolvedStyle.transitionProperty;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0007050A File Offset: 0x0006E70A
		IEnumerable<EasingFunction> IResolvedStyle.transitionTimingFunction
		{
			get
			{
				return this.resolvedStyle.transitionTimingFunction;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x00070517 File Offset: 0x0006E717
		Vector3 IResolvedStyle.translate
		{
			get
			{
				return this.resolvedStyle.translate;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x00070524 File Offset: 0x0006E724
		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.resolvedStyle.unityBackgroundImageTintColor;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001E05 RID: 7685 RVA: 0x00070531 File Offset: 0x0006E731
		EditorTextRenderingMode IResolvedStyle.unityEditorTextRenderingMode
		{
			get
			{
				return this.resolvedStyle.unityEditorTextRenderingMode;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0007053E File Offset: 0x0006E73E
		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.resolvedStyle.unityFont;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001E07 RID: 7687 RVA: 0x0007054B File Offset: 0x0006E74B
		FontDefinition IResolvedStyle.unityFontDefinition
		{
			get
			{
				return this.resolvedStyle.unityFontDefinition;
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x00070558 File Offset: 0x0006E758
		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.resolvedStyle.unityFontStyleAndWeight;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x00070565 File Offset: 0x0006E765
		float IResolvedStyle.unityParagraphSpacing
		{
			get
			{
				return this.resolvedStyle.unityParagraphSpacing;
			}
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00070572 File Offset: 0x0006E772
		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.resolvedStyle.unitySliceBottom;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0007057F File Offset: 0x0006E77F
		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.resolvedStyle.unitySliceLeft;
			}
		}

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x0007058C File Offset: 0x0006E78C
		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.resolvedStyle.unitySliceRight;
			}
		}

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x00070599 File Offset: 0x0006E799
		float IResolvedStyle.unitySliceScale
		{
			get
			{
				return this.resolvedStyle.unitySliceScale;
			}
		}

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x000705A6 File Offset: 0x0006E7A6
		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.resolvedStyle.unitySliceTop;
			}
		}

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x000705B3 File Offset: 0x0006E7B3
		SliceType IResolvedStyle.unitySliceType
		{
			get
			{
				return this.resolvedStyle.unitySliceType;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x000705C0 File Offset: 0x0006E7C0
		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.resolvedStyle.unityTextAlign;
			}
		}

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x000705CD File Offset: 0x0006E7CD
		TextGeneratorType IResolvedStyle.unityTextGenerator
		{
			get
			{
				return this.resolvedStyle.unityTextGenerator;
			}
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x000705DA File Offset: 0x0006E7DA
		Color IResolvedStyle.unityTextOutlineColor
		{
			get
			{
				return this.resolvedStyle.unityTextOutlineColor;
			}
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000705E7 File Offset: 0x0006E7E7
		float IResolvedStyle.unityTextOutlineWidth
		{
			get
			{
				return this.resolvedStyle.unityTextOutlineWidth;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000705F4 File Offset: 0x0006E7F4
		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.resolvedStyle.unityTextOverflowPosition;
			}
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x00070601 File Offset: 0x0006E801
		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.resolvedStyle.visibility;
			}
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0007060E File Offset: 0x0006E80E
		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.resolvedStyle.whiteSpace;
			}
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0007061B File Offset: 0x0006E81B
		float IResolvedStyle.width
		{
			get
			{
				return this.resolvedStyle.width;
			}
		}

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00070628 File Offset: 0x0006E828
		float IResolvedStyle.wordSpacing
		{
			get
			{
				return this.resolvedStyle.wordSpacing;
			}
		}

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00070635 File Offset: 0x0006E835
		internal bool hasRunningAnimations
		{
			get
			{
				return this.styleAnimation.runningAnimationCount > 0;
			}
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x00070645 File Offset: 0x0006E845
		internal bool hasCompletedAnimations
		{
			get
			{
				return this.styleAnimation.completedAnimationCount > 0;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00070655 File Offset: 0x0006E855
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0007065D File Offset: 0x0006E85D
		int IStylePropertyAnimations.runningAnimationCount { get; set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x00070666 File Offset: 0x0006E866
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x0007066E File Offset: 0x0006E86E
		int IStylePropertyAnimations.completedAnimationCount { get; set; }

		// Token: 0x06001E1F RID: 7711 RVA: 0x00070678 File Offset: 0x0006E878
		private IStylePropertyAnimationSystem GetStylePropertyAnimationSystem()
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			return (elementPanel != null) ? elementPanel.styleAnimationSystem : null;
		}

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x000435E1 File Offset: 0x000417E1
		internal IStylePropertyAnimations styleAnimation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x0007069C File Offset: 0x0006E89C
		bool IStylePropertyAnimations.Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x000706C4 File Offset: 0x0006E8C4
		bool IStylePropertyAnimations.Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x000706EC File Offset: 0x0006E8EC
		bool IStylePropertyAnimations.Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00070714 File Offset: 0x0006E914
		bool IStylePropertyAnimations.Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x0007073C File Offset: 0x0006E93C
		bool IStylePropertyAnimations.StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransitionEnum(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00070764 File Offset: 0x0006E964
		bool IStylePropertyAnimations.Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x0007078C File Offset: 0x0006E98C
		bool IStylePropertyAnimations.Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E28 RID: 7720 RVA: 0x000707B4 File Offset: 0x0006E9B4
		bool IStylePropertyAnimations.Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E29 RID: 7721 RVA: 0x000707DC File Offset: 0x0006E9DC
		bool IStylePropertyAnimations.Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2A RID: 7722 RVA: 0x00070804 File Offset: 0x0006EA04
		bool IStylePropertyAnimations.Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2B RID: 7723 RVA: 0x0007082C File Offset: 0x0006EA2C
		bool IStylePropertyAnimations.Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2C RID: 7724 RVA: 0x00070854 File Offset: 0x0006EA54
		bool IStylePropertyAnimations.Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2D RID: 7725 RVA: 0x0007087C File Offset: 0x0006EA7C
		bool IStylePropertyAnimations.Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x000708A4 File Offset: 0x0006EAA4
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundPosition from, BackgroundPosition to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x000708CC File Offset: 0x0006EACC
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundRepeat from, BackgroundRepeat to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x000708F4 File Offset: 0x0006EAF4
		bool IStylePropertyAnimations.Start(StylePropertyId id, BackgroundSize from, BackgroundSize to, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return this.GetStylePropertyAnimationSystem().StartTransition(this, id, from, to, durationMs, delayMs, easingCurve);
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0007091B File Offset: 0x0006EB1B
		void IStylePropertyAnimations.CancelAnimation(StylePropertyId id)
		{
			IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
			if (stylePropertyAnimationSystem != null)
			{
				stylePropertyAnimationSystem.CancelAnimation(this, id);
			}
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x00070934 File Offset: 0x0006EB34
		void IStylePropertyAnimations.CancelAllAnimations()
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				IStylePropertyAnimationSystem stylePropertyAnimationSystem = this.GetStylePropertyAnimationSystem();
				if (stylePropertyAnimationSystem != null)
				{
					stylePropertyAnimationSystem.CancelAllAnimations(this);
				}
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0007096C File Offset: 0x0006EB6C
		bool IStylePropertyAnimations.HasRunningAnimation(StylePropertyId id)
		{
			return this.hasRunningAnimations && this.GetStylePropertyAnimationSystem().HasRunningAnimation(this, id);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x00070996 File Offset: 0x0006EB96
		void IStylePropertyAnimations.UpdateAnimation(StylePropertyId id)
		{
			this.GetStylePropertyAnimationSystem().UpdateAnimation(this, id);
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x000709A8 File Offset: 0x0006EBA8
		void IStylePropertyAnimations.GetAllAnimations(List<StylePropertyId> outPropertyIds)
		{
			bool flag = this.hasRunningAnimations || this.hasCompletedAnimations;
			if (flag)
			{
				this.GetStylePropertyAnimationSystem().GetAllAnimations(this, outPropertyIds);
			}
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000709DC File Offset: 0x0006EBDC
		internal bool TryConvertLengthUnits(StylePropertyId id, ref Length from, ref Length to, int subPropertyIndex = 0)
		{
			bool flag = from.IsAuto() || from.IsNone() || to.IsAuto() || to.IsNone();
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = float.IsNaN(from.value) || float.IsNaN(to.value);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					bool flag4 = from.unit == to.unit;
					if (flag4)
					{
						flag2 = true;
					}
					else
					{
						bool flag5 = to.unit == LengthUnit.Pixel;
						if (flag5)
						{
							bool flag6 = Mathf.Approximately(from.value, 0f);
							if (flag6)
							{
								from = new Length(0f, LengthUnit.Pixel);
								return true;
							}
							float? parentSizeForLengthConversion = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag7 = parentSizeForLengthConversion == null || parentSizeForLengthConversion.Value < 0f;
							if (flag7)
							{
								return false;
							}
							from = new Length(from.value * parentSizeForLengthConversion.Value / 100f, LengthUnit.Pixel);
						}
						else
						{
							Assert.AreEqual<LengthUnit>(LengthUnit.Percent, to.unit);
							float? parentSizeForLengthConversion2 = this.GetParentSizeForLengthConversion(id, subPropertyIndex);
							bool flag8 = parentSizeForLengthConversion2 == null || parentSizeForLengthConversion2.Value <= 0f;
							if (flag8)
							{
								return false;
							}
							from = new Length(from.value * 100f / parentSizeForLengthConversion2.Value, LengthUnit.Percent);
						}
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00070B54 File Offset: 0x0006ED54
		internal bool TryConvertTransformOriginUnits(ref TransformOrigin from, ref TransformOrigin to)
		{
			Length x = from.x;
			Length y = from.y;
			Length x2 = to.x;
			Length y2 = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref x, ref x2, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.TransformOrigin, ref y, ref y2, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = x;
					from.y = y;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		internal bool TryConvertTranslateUnits(ref Translate from, ref Translate to)
		{
			Length x = from.x;
			Length y = from.y;
			Length x2 = to.x;
			Length y2 = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref x, ref x2, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.Translate, ref y, ref y2, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = x;
					from.y = y;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00070C54 File Offset: 0x0006EE54
		internal bool TryConvertBackgroundPositionUnits(ref BackgroundPosition from, ref BackgroundPosition to)
		{
			Length offset = from.offset;
			Length offset2 = to.offset;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.BackgroundPosition, ref offset, ref offset2, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				from.offset = offset;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x00070C98 File Offset: 0x0006EE98
		internal bool TryConvertBackgroundSizeUnits(ref BackgroundSize from, ref BackgroundSize to)
		{
			Length x = from.x;
			Length y = from.y;
			Length x2 = to.x;
			Length y2 = to.y;
			bool flag = !this.TryConvertLengthUnits(StylePropertyId.BackgroundSize, ref x, ref x2, 0);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = !this.TryConvertLengthUnits(StylePropertyId.BackgroundSize, ref y, ref y2, 1);
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					from.x = x;
					from.y = y;
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x00070D18 File Offset: 0x0006EF18
		private float? GetParentSizeForLengthConversion(StylePropertyId id, int subPropertyIndex = 0)
		{
			if (id <= StylePropertyId.WordSpacing)
			{
				if (id - StylePropertyId.FontSize <= 1 || id == StylePropertyId.UnityParagraphSpacing || id == StylePropertyId.WordSpacing)
				{
					return null;
				}
			}
			else if (id <= StylePropertyId.Translate)
			{
				switch (id)
				{
				case StylePropertyId.Bottom:
				case StylePropertyId.Height:
				case StylePropertyId.MaxHeight:
				case StylePropertyId.MinHeight:
				case StylePropertyId.Top:
				{
					VisualElement parent = this.hierarchy.parent;
					return (parent != null) ? new float?(parent.resolvedStyle.height) : null;
				}
				case StylePropertyId.Display:
				case StylePropertyId.FlexDirection:
				case StylePropertyId.FlexGrow:
				case StylePropertyId.FlexShrink:
				case StylePropertyId.FlexWrap:
				case StylePropertyId.JustifyContent:
				case StylePropertyId.Position:
					break;
				case StylePropertyId.FlexBasis:
				{
					bool flag = this.hierarchy.parent == null;
					if (flag)
					{
						return null;
					}
					FlexDirection flexDirection = this.hierarchy.parent.resolvedStyle.flexDirection;
					FlexDirection flexDirection2 = flexDirection;
					if (flexDirection2 > FlexDirection.ColumnReverse)
					{
						return new float?(this.hierarchy.parent.resolvedStyle.width);
					}
					return new float?(this.hierarchy.parent.resolvedStyle.height);
				}
				case StylePropertyId.Left:
				case StylePropertyId.MarginBottom:
				case StylePropertyId.MarginLeft:
				case StylePropertyId.MarginRight:
				case StylePropertyId.MarginTop:
				case StylePropertyId.MaxWidth:
				case StylePropertyId.MinWidth:
				case StylePropertyId.PaddingBottom:
				case StylePropertyId.PaddingLeft:
				case StylePropertyId.PaddingRight:
				case StylePropertyId.PaddingTop:
				case StylePropertyId.Right:
				case StylePropertyId.Width:
				{
					VisualElement parent2 = this.hierarchy.parent;
					return (parent2 != null) ? new float?(parent2.resolvedStyle.width) : null;
				}
				default:
					if (id - StylePropertyId.TransformOrigin <= 1)
					{
						return new float?((subPropertyIndex == 0) ? this.resolvedStyle.width : this.resolvedStyle.height);
					}
					break;
				}
			}
			else if (id - StylePropertyId.BorderBottomLeftRadius <= 1 || id - StylePropertyId.BorderTopLeftRadius <= 1)
			{
				return new float?(this.resolvedStyle.width);
			}
			return null;
		}

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001E3C RID: 7740 RVA: 0x00070F5A File Offset: 0x0006F15A
		// (set) Token: 0x06001E3D RID: 7741 RVA: 0x00070F6F File Offset: 0x0006F16F
		internal bool isCompositeRoot
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.CompositeRoot) == VisualElementFlags.CompositeRoot;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.CompositeRoot) : (this.m_Flags & ~VisualElementFlags.CompositeRoot));
			}
		}

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001E3E RID: 7742 RVA: 0x00070F94 File Offset: 0x0006F194
		// (set) Token: 0x06001E3F RID: 7743 RVA: 0x00070FAC File Offset: 0x0006F1AC
		internal bool areAncestorsAndSelfDisplayed
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.HierarchyDisplayed) == VisualElementFlags.HierarchyDisplayed;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.HierarchyDisplayed) : (this.m_Flags & ~VisualElementFlags.HierarchyDisplayed));
				bool flag = value && (this.renderChainData.pendingRepaint || this.renderChainData.pendingHierarchicalRepaint);
				if (flag)
				{
					this.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06001E40 RID: 7744 RVA: 0x0007100E File Offset: 0x0006F20E
		// (set) Token: 0x06001E41 RID: 7745 RVA: 0x00071018 File Offset: 0x0006F218
		[CreateProperty]
		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
					base.NotifyPropertyChanged(in VisualElement.viewDataKeyProperty);
				}
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x00071060 File Offset: 0x0006F260
		// (set) Token: 0x06001E43 RID: 7747 RVA: 0x00071075 File Offset: 0x0006F275
		internal bool enableViewDataPersistence
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EnableViewDataPersistence) == VisualElementFlags.EnableViewDataPersistence;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EnableViewDataPersistence) : (this.m_Flags & ~VisualElementFlags.EnableViewDataPersistence));
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0007109C File Offset: 0x0006F29C
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x000710D4 File Offset: 0x0006F2D4
		[CreateProperty]
		public object userData
		{
			get
			{
				bool flag = this.m_PropertyBag != null;
				object obj2;
				if (flag)
				{
					object obj;
					this.m_PropertyBag.TryGetValue(VisualElement.userDataPropertyKey, out obj);
					obj2 = obj;
				}
				else
				{
					obj2 = null;
				}
				return obj2;
			}
			set
			{
				object userData = this.userData;
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
				bool flag = userData != this.userData;
				if (flag)
				{
					base.NotifyPropertyChanged(in VisualElement.userDataProperty);
				}
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x00071114 File Offset: 0x0006F314
		public override bool canGrabFocus
		{
			get
			{
				bool flag = false;
				for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool isCompositeRoot = visualElement.isCompositeRoot;
					if (isCompositeRoot)
					{
						flag |= !visualElement.canGrabFocus;
						break;
					}
				}
				return !flag && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x00071194 File Offset: 0x0006F394
		public override FocusController focusController
		{
			get
			{
				IPanel panel = this.panel;
				return (panel != null) ? panel.focusController : null;
			}
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x000711B8 File Offset: 0x0006F3B8
		// (set) Token: 0x06001E49 RID: 7753 RVA: 0x00002152 File Offset: 0x00000352
		[CreateProperty]
		public bool disablePlayModeTint
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001E4A RID: 7754 RVA: 0x000711CB File Offset: 0x0006F3CB
		internal Color playModeTintColor
		{
			get
			{
				return this.disablePlayModeTint ? Color.white : UIElementsUtility.editorPlayModeTintColor;
			}
		}

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000711E4 File Offset: 0x0006F3E4
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x00071234 File Offset: 0x0006F434
		[CreateProperty]
		public UsageHints usageHints
		{
			get
			{
				return (((this.renderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.renderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None) | (((this.renderHints & RenderHints.MaskContainer) != RenderHints.None) ? UsageHints.MaskContainer : UsageHints.None) | (((this.renderHints & RenderHints.DynamicColor) != RenderHints.None) ? UsageHints.DynamicColor : UsageHints.None);
			}
			set
			{
				bool flag = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag)
				{
					this.renderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.GroupTransform;
				}
				bool flag2 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag2)
				{
					this.renderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.renderHints &= ~RenderHints.BoneTransform;
				}
				bool flag3 = (value & UsageHints.MaskContainer) > UsageHints.None;
				if (flag3)
				{
					this.renderHints |= RenderHints.MaskContainer;
				}
				else
				{
					this.renderHints &= ~RenderHints.MaskContainer;
				}
				bool flag4 = (value & UsageHints.DynamicColor) > UsageHints.None;
				if (flag4)
				{
					this.renderHints |= RenderHints.DynamicColor;
				}
				else
				{
					this.renderHints &= ~RenderHints.DynamicColor;
				}
				base.NotifyPropertyChanged(in VisualElement.usageHintsProperty);
			}
		}

		// Token: 0x170007B6 RID: 1974
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000712FC File Offset: 0x0006F4FC
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x00071314 File Offset: 0x0006F514
		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				RenderHints renderHints = this.m_RenderHints & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints renderHints2 = value & ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
				RenderHints renderHints3 = renderHints ^ renderHints2;
				bool flag = renderHints3 > RenderHints.None;
				if (flag)
				{
					RenderHints renderHints4 = this.m_RenderHints & RenderHints.DirtyAll;
					RenderHints renderHints5 = renderHints3 << 5;
					this.m_RenderHints = renderHints2 | renderHints4 | renderHints5;
					this.IncrementVersion(VersionChangeType.RenderHints);
				}
			}
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00071371 File Offset: 0x0006F571
		internal void MarkRenderHintsClean()
		{
			this.m_RenderHints &= ~(RenderHints.DirtyGroupTransform | RenderHints.DirtyBoneTransform | RenderHints.DirtyClipWithScissors | RenderHints.DirtyMaskContainer | RenderHints.DirtyDynamicColor);
		}

		// Token: 0x170007B7 RID: 1975
		// (get) Token: 0x06001E50 RID: 7760 RVA: 0x00071388 File Offset: 0x0006F588
		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007B8 RID: 1976
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x0007139C File Offset: 0x0006F59C
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x000713B9 File Offset: 0x0006F5B9
		Vector3 ITransform.position
		{
			get
			{
				return this.resolvedStyle.translate;
			}
			set
			{
				this.style.translate = new Translate(value.x, value.y, value.z);
			}
		}

		// Token: 0x170007B9 RID: 1977
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000713F0 File Offset: 0x0006F5F0
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x00071415 File Offset: 0x0006F615
		Quaternion ITransform.rotation
		{
			get
			{
				return this.resolvedStyle.rotate.ToQuaternion();
			}
			set
			{
				this.style.rotate = new Rotate(value);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x00071430 File Offset: 0x0006F630
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x0007147C File Offset: 0x0006F67C
		Vector3 ITransform.scale
		{
			get
			{
				Vector3 value = this.resolvedStyle.scale.value;
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag = elementPanel != null && elementPanel.isFlat;
				if (flag)
				{
					value.z = 1f;
				}
				return value;
			}
			set
			{
				this.style.scale = new Scale(value);
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0007149C File Offset: 0x0006F69C
		Matrix4x4 ITransform.matrix
		{
			get
			{
				Vector3 value = this.resolvedStyle.scale.value;
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag = elementPanel != null && elementPanel.isFlat;
				if (flag)
				{
					value.z = 1f;
				}
				return Matrix4x4.TRS(this.resolvedStyle.translate, this.resolvedStyle.rotate.ToQuaternion(), value);
			}
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0007150C File Offset: 0x0006F70C
		// (set) Token: 0x06001E59 RID: 7769 RVA: 0x0007151B File Offset: 0x0006F71B
		internal bool isLayoutManual
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LayoutManual) == VisualElementFlags.LayoutManual;
			}
			private set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LayoutManual) : (this.m_Flags & ~VisualElementFlags.LayoutManual));
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0007153C File Offset: 0x0006F73C
		public float scaledPixelsPerPoint
		{
			get
			{
				bool flag = this.elementPanel == null;
				float num;
				if (flag)
				{
					Debug.LogWarning("Trying to access the DPI setting of a visual element that is not on a panel.");
					num = GUIUtility.pixelsPerPoint;
				}
				else
				{
					num = this.elementPanel.scaledPixelsPerPoint;
				}
				return num;
			}
		}

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0007157A File Offset: 0x0006F77A
		[Obsolete("scaledPixelsPerPoint_noChecks is deprecated. Use scaledPixelsPerPoint instead.")]
		internal float scaledPixelsPerPoint_noChecks
		{
			get
			{
				BaseVisualElementPanel elementPanel = this.elementPanel;
				return (elementPanel != null) ? elementPanel.scaledPixelsPerPoint : GUIUtility.pixelsPerPoint;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x00071592 File Offset: 0x0006F792
		[Obsolete("unityBackgroundScaleMode is deprecated. Use background-* properties instead.")]
		StyleEnum<ScaleMode> IResolvedStyle.unityBackgroundScaleMode
		{
			get
			{
				return this.resolvedStyle.unityBackgroundScaleMode;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x000715A0 File Offset: 0x0006F7A0
		// (set) Token: 0x06001E5E RID: 7774 RVA: 0x00071628 File Offset: 0x0006F828
		[CreateProperty(ReadOnly = true)]
		public Rect layout
		{
			get
			{
				Rect layout = this.m_Layout;
				bool flag = !this.layoutNode.IsUndefined && !this.isLayoutManual;
				if (flag)
				{
					layout.x = this.layoutNode.LayoutX;
					layout.y = this.layoutNode.LayoutY;
					layout.width = this.layoutNode.LayoutWidth;
					layout.height = this.layoutNode.LayoutHeight;
				}
				return layout;
			}
			internal set
			{
				bool flag = this.isLayoutManual && this.m_Layout == value;
				if (!flag)
				{
					Rect layout = this.layout;
					VersionChangeType versionChangeType = (VersionChangeType)0;
					bool flag2 = !Mathf.Approximately(layout.x, value.x) || !Mathf.Approximately(layout.y, value.y);
					if (flag2)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
					bool flag3 = !Mathf.Approximately(layout.width, value.width) || !Mathf.Approximately(layout.height, value.height);
					if (flag3)
					{
						versionChangeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle style = this.style;
					style.position = Position.Absolute;
					style.marginLeft = 0f;
					style.marginRight = 0f;
					style.marginBottom = 0f;
					style.marginTop = 0f;
					style.left = value.x;
					style.top = value.y;
					style.right = float.NaN;
					style.bottom = float.NaN;
					style.width = value.width;
					style.height = value.height;
					bool flag4 = versionChangeType > (VersionChangeType)0;
					if (flag4)
					{
						this.IncrementVersion(versionChangeType);
					}
				}
			}
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000717C0 File Offset: 0x0006F9C0
		internal void ClearManualLayout()
		{
			this.isLayoutManual = false;
			IStyle style = this.style;
			style.position = StyleKeyword.Null;
			style.marginLeft = StyleKeyword.Null;
			style.marginRight = StyleKeyword.Null;
			style.marginBottom = StyleKeyword.Null;
			style.marginTop = StyleKeyword.Null;
			style.left = StyleKeyword.Null;
			style.top = StyleKeyword.Null;
			style.right = StyleKeyword.Null;
			style.bottom = StyleKeyword.Null;
			style.width = StyleKeyword.Null;
			style.height = StyleKeyword.Null;
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0007186C File Offset: 0x0006FA6C
		[CreateProperty(ReadOnly = true)]
		public Rect contentRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - spacing;
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000718C0 File Offset: 0x0006FAC0
		protected Rect paddingRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - spacing;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x00071911 File Offset: 0x0006FB11
		// (set) Token: 0x06001E63 RID: 7779 RVA: 0x00071922 File Offset: 0x0006FB22
		internal bool needs3DBounds
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.Needs3DBounds) > (VisualElementFlags)0;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.Needs3DBounds) : (this.m_Flags & ~VisualElementFlags.Needs3DBounds));
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x00071947 File Offset: 0x0006FB47
		// (set) Token: 0x06001E65 RID: 7781 RVA: 0x00071958 File Offset: 0x0006FB58
		internal bool isLocalBounds3DDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.LocalBounds3DDirty) > (VisualElementFlags)0;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.LocalBounds3DDirty) : (this.m_Flags & ~VisualElementFlags.LocalBounds3DDirty));
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0007197D File Offset: 0x0006FB7D
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x0007198A File Offset: 0x0006FB8A
		internal bool isBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirty) == VisualElementFlags.BoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirty));
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001E68 RID: 7784 RVA: 0x000719A8 File Offset: 0x0006FBA8
		// (set) Token: 0x06001E69 RID: 7785 RVA: 0x000719B7 File Offset: 0x0006FBB7
		internal bool isWorldBoundingBoxDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldBoundingBoxDirty) == VisualElementFlags.WorldBoundingBoxDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldBoundingBoxDirty) : (this.m_Flags & ~VisualElementFlags.WorldBoundingBoxDirty));
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001E6A RID: 7786 RVA: 0x000719D6 File Offset: 0x0006FBD6
		internal bool isWorldBoundingBoxOrDependenciesDirty
		{
			get
			{
				return (this.m_Flags & (VisualElementFlags.WorldTransformDirty | VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty)) > (VisualElementFlags)0;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001E6B RID: 7787 RVA: 0x000719E4 File Offset: 0x0006FBE4
		internal Rect boundingBox
		{
			get
			{
				bool isBoundingBoxDirty = this.isBoundingBoxDirty;
				if (isBoundingBoxDirty)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001E6C RID: 7788 RVA: 0x00071A18 File Offset: 0x0006FC18
		internal Rect worldBoundingBox
		{
			get
			{
				bool isWorldBoundingBoxOrDependenciesDirty = this.isWorldBoundingBoxOrDependenciesDirty;
				if (isWorldBoundingBoxOrDependenciesDirty)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001E6D RID: 7789 RVA: 0x00071A4C File Offset: 0x0006FC4C
		private Rect boundingBoxInParentSpace
		{
			get
			{
				Rect boundingBox = this.boundingBox;
				this.TransformAlignedRectToParentSpace(ref boundingBox);
				return boundingBox;
			}
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x00071A70 File Offset: 0x0006FC70
		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				bool flag2 = !this.ShouldClip() && this.resolvedStyle.display == DisplayStyle.Flex;
				if (flag2)
				{
					int count = this.m_Children.Count;
					for (int i = 0; i < count; i++)
					{
						bool flag3 = !this.m_Children[i].areAncestorsAndSelfDisplayed;
						if (!flag3)
						{
							Rect boundingBoxInParentSpace = this.m_Children[i].boundingBoxInParentSpace;
							this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, boundingBoxInParentSpace.xMin);
							this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, boundingBoxInParentSpace.xMax);
							this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, boundingBoxInParentSpace.yMin);
							this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, boundingBoxInParentSpace.yMax);
						}
					}
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x00071C0B File Offset: 0x0006FE0B
		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = this.boundingBox;
			VisualElement.TransformAlignedRect(this.worldTransformRef, ref this.m_WorldBoundingBox);
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001E70 RID: 7792 RVA: 0x00071C2C File Offset: 0x0006FE2C
		internal Bounds localBounds3D
		{
			get
			{
				bool isLocalBounds3DDirty = this.isLocalBounds3DDirty;
				if (isLocalBounds3DDirty)
				{
					this.UpdateLocalBoundsAndPickingBounds3D();
					this.isLocalBounds3DDirty = false;
				}
				return WorldSpaceDataStore.GetWorldSpaceData(this).localBounds3D;
			}
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x00071C64 File Offset: 0x0006FE64
		private void UpdateLocalBoundsAndPickingBounds3D()
		{
			bool flag = !this.areAncestorsAndSelfDisplayed;
			if (flag)
			{
				WorldSpaceDataStore.SetWorldSpaceData(this, new WorldSpaceData
				{
					localBounds3D = WorldSpaceData.k_Empty3DBounds
				});
			}
			else
			{
				bool flag2 = !this.needs3DBounds;
				if (flag2)
				{
					Rect boundingBox = this.boundingBox;
					Bounds bounds = new Bounds(boundingBox.center, boundingBox.size);
					WorldSpaceDataStore.SetWorldSpaceData(this, new WorldSpaceData
					{
						localBounds3D = bounds
					});
				}
				else
				{
					Bounds bounds2 = new Bounds(this.rect.center, this.rect.size);
					bool flag3 = !this.ShouldClip();
					if (flag3)
					{
						int childCount = this.hierarchy.childCount;
						for (int i = 0; i < childCount; i++)
						{
							Bounds localBounds3D = this.hierarchy[i].localBounds3D;
							bool flag4 = localBounds3D.extents.x >= 0f;
							if (flag4)
							{
								this.hierarchy[i].TransformAlignedBoundsToParentSpace(ref localBounds3D);
								bounds2.Encapsulate(localBounds3D);
							}
						}
					}
					WorldSpaceDataStore.SetWorldSpaceData(this, new WorldSpaceData
					{
						localBounds3D = bounds2
					});
				}
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x00071DD4 File Offset: 0x0006FFD4
		[CreateProperty(ReadOnly = true)]
		public Rect worldBound
		{
			get
			{
				Rect rect = this.rect;
				VisualElement.TransformAlignedRect(this.worldTransformRef, ref rect);
				return rect;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x00071DFC File Offset: 0x0006FFFC
		[CreateProperty(ReadOnly = true)]
		public Rect localBound
		{
			get
			{
				Rect rect = this.rect;
				this.TransformAlignedRectToParentSpace(ref rect);
				return rect;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001E74 RID: 7796 RVA: 0x00071E20 File Offset: 0x00070020
		internal Rect rect
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				Rect layout = this.layout;
				return new Rect(0f, 0f, layout.width, layout.height);
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x00071E56 File Offset: 0x00070056
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x00071E63 File Offset: 0x00070063
		internal bool isWorldTransformDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformDirty) == VisualElementFlags.WorldTransformDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformDirty));
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001E77 RID: 7799 RVA: 0x00071E81 File Offset: 0x00070081
		// (set) Token: 0x06001E78 RID: 7800 RVA: 0x00071E8E File Offset: 0x0007008E
		internal bool isWorldTransformInverseDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldTransformInverseDirty) == VisualElementFlags.WorldTransformInverseDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldTransformInverseDirty) : (this.m_Flags & ~VisualElementFlags.WorldTransformInverseDirty));
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001E79 RID: 7801 RVA: 0x00071EAC File Offset: 0x000700AC
		internal bool isWorldTransformInverseOrDependenciesDirty
		{
			get
			{
				return (this.m_Flags & (VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldTransformInverseDirty)) > (VisualElementFlags)0;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001E7A RID: 7802 RVA: 0x00071EBC File Offset: 0x000700BC
		[CreateProperty(ReadOnly = true)]
		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06001E7B RID: 7803 RVA: 0x00071EE8 File Offset: 0x000700E8
		internal ref Matrix4x4 worldTransformRef
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return ref this.m_WorldTransformCache;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06001E7C RID: 7804 RVA: 0x00071F14 File Offset: 0x00070114
		internal ref Matrix4x4 worldTransformInverse
		{
			get
			{
				bool isWorldTransformInverseOrDependenciesDirty = this.isWorldTransformInverseOrDependenciesDirty;
				if (isWorldTransformInverseOrDependenciesDirty)
				{
					this.UpdateWorldTransformInverse();
				}
				return ref this.m_WorldTransformInverseCache;
			}
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00071F40 File Offset: 0x00070140
		internal void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
				if (hasDefaultRotationAndScale)
				{
					VisualElement.TranslateMatrix34(this.hierarchy.parent.worldTransformRef, this.positionWithLayout, out this.m_WorldTransformCache);
				}
				else
				{
					Matrix4x4 matrix4x;
					this.GetPivotedMatrixWithLayout(out matrix4x);
					VisualElement.MultiplyMatrix34(this.hierarchy.parent.worldTransformRef, ref matrix4x, out this.m_WorldTransformCache);
				}
			}
			else
			{
				this.GetPivotedMatrixWithLayout(out this.m_WorldTransformCache);
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x0007200C File Offset: 0x0007020C
		internal void UpdateWorldTransformInverse()
		{
			Matrix4x4.Inverse3DAffine(this.worldTransform, ref this.m_WorldTransformInverseCache);
			this.isWorldTransformInverseDirty = false;
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00072029 File Offset: 0x00070229
		// (set) Token: 0x06001E80 RID: 7808 RVA: 0x00072036 File Offset: 0x00070236
		internal bool isWorldClipDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.WorldClipDirty) == VisualElementFlags.WorldClipDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.WorldClipDirty) : (this.m_Flags & ~VisualElementFlags.WorldClipDirty));
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00072054 File Offset: 0x00070254
		internal Rect worldClip
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00072088 File Offset: 0x00070288
		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000720BC File Offset: 0x000702BC
		internal bool worldClipIsInfinite
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipIsInfinite;
			}
		}

		// Token: 0x06001E84 RID: 7812 RVA: 0x000720F0 File Offset: 0x000702F0
		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		// Token: 0x06001E85 RID: 7813 RVA: 0x0007212C File Offset: 0x0007032C
		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool flag2 = this.hierarchy.parent.worldClipIsInfinite;
				bool flag3 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag3)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					flag2 = true;
					this.m_WorldClipMinusGroup = VisualElement.s_InfiniteRect;
				}
				bool flag4 = this.ShouldClip();
				if (flag4)
				{
					Rect rect = this.SubstractBorderPadding(this.worldBound);
					this.m_WorldClip = this.CombineClipRects(rect, this.m_WorldClip);
					this.m_WorldClipMinusGroup = (flag2 ? rect : this.CombineClipRects(rect, this.m_WorldClipMinusGroup));
					this.m_WorldClipIsInfinite = false;
				}
				else
				{
					this.m_WorldClipIsInfinite = flag2;
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
				this.m_WorldClipIsInfinite = true;
			}
		}

		// Token: 0x06001E86 RID: 7814 RVA: 0x00072268 File Offset: 0x00070468
		private Rect CombineClipRects(Rect rect, Rect parentRect)
		{
			float num = Mathf.Max(rect.xMin, parentRect.xMin);
			float num2 = Mathf.Min(rect.xMax, parentRect.xMax);
			float num3 = Mathf.Max(rect.yMin, parentRect.yMin);
			float num4 = Mathf.Min(rect.yMax, parentRect.yMax);
			float num5 = Mathf.Max(num2 - num, 0f);
			float num6 = Mathf.Max(num4 - num3, 0f);
			return new Rect(num, num3, num5, num6);
		}

		// Token: 0x06001E87 RID: 7815 RVA: 0x000722F8 File Offset: 0x000704F8
		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float m = this.worldTransform.m00;
			float m2 = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * m;
			worldRect.y += this.resolvedStyle.borderTopWidth * m2;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * m;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * m2;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * m;
				worldRect.y += this.resolvedStyle.paddingTop * m2;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * m;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * m2;
			}
			return worldRect;
		}

		// Token: 0x06001E88 RID: 7816 RVA: 0x0007244C File Offset: 0x0007064C
		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect rect = position;
			Vector3 vector = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y, 0f));
			Vector3 vector2 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y, 0f));
			Vector3 vector3 = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y + rect.height, 0f));
			Vector3 vector4 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y + rect.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(vector.x, Mathf.Min(vector2.x, Mathf.Min(vector3.x, vector4.x))), Mathf.Min(vector.y, Mathf.Min(vector2.y, Mathf.Min(vector3.y, vector4.y))), Mathf.Max(vector.x, Mathf.Max(vector2.x, Mathf.Max(vector3.x, vector4.x))), Mathf.Max(vector.y, Mathf.Max(vector2.y, Mathf.Max(vector3.y, vector4.y))));
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000725A8 File Offset: 0x000707A8
		// (set) Token: 0x06001E8A RID: 7818 RVA: 0x000725BD File Offset: 0x000707BD
		internal bool receivesHierarchyGeometryChangedEvents
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.ReceivesHierarchyGeometryChangedEvents) == VisualElementFlags.ReceivesHierarchyGeometryChangedEvents;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.ReceivesHierarchyGeometryChangedEvents) : (this.m_Flags & ~VisualElementFlags.ReceivesHierarchyGeometryChangedEvents));
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x000725E2 File Offset: 0x000707E2
		// (set) Token: 0x06001E8C RID: 7820 RVA: 0x000725F7 File Offset: 0x000707F7
		internal bool boundingBoxDirtiedSinceLastLayoutPass
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass) == VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass) : (this.m_Flags & ~VisualElementFlags.BoundingBoxDirtiedSinceLastLayoutPass));
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x0007261C File Offset: 0x0007081C
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x00072634 File Offset: 0x00070834
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				PseudoStates pseudoStates = this.m_PseudoStates ^ value;
				this.m_PseudoStates = value;
				bool flag = pseudoStates > (PseudoStates)0;
				if (flag)
				{
					bool flag2 = (value & PseudoStates.Root) == PseudoStates.Root;
					if (flag2)
					{
						this.isRootVisualContainer = true;
					}
					bool flag3 = pseudoStates != PseudoStates.Root;
					if (flag3)
					{
						PseudoStates pseudoStates2 = pseudoStates & value;
						PseudoStates pseudoStates3 = pseudoStates ^ pseudoStates2;
						bool flag4 = (this.triggerPseudoMask & pseudoStates2) != (PseudoStates)0 || (this.dependencyPseudoMask & pseudoStates3) > (PseudoStates)0;
						if (flag4)
						{
							this.IncrementVersion(VersionChangeType.StyleSheet);
						}
					}
				}
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x000726BD File Offset: 0x000708BD
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x000726C5 File Offset: 0x000708C5
		internal int containedPointerIds { get; set; }

		// Token: 0x06001E91 RID: 7825 RVA: 0x000726D0 File Offset: 0x000708D0
		internal void UpdateHoverPseudoState()
		{
			bool flag = this.containedPointerIds == 0 || this.panel == null;
			if (flag)
			{
				this.pseudoStates &= ~PseudoStates.Hover;
			}
			else
			{
				bool flag2 = false;
				for (int i = 0; i < PointerId.maxPointers; i++)
				{
					bool flag3 = (this.containedPointerIds & (1 << i)) != 0;
					if (flag3)
					{
						IEventHandler capturingElement = this.panel.GetCapturingElement(i);
						bool flag4 = VisualElement.IsPartOfCapturedChain(this, in capturingElement);
						if (flag4)
						{
							flag2 = true;
							break;
						}
					}
				}
				bool flag5 = flag2;
				if (flag5)
				{
					this.pseudoStates |= PseudoStates.Hover;
				}
				else
				{
					this.pseudoStates &= ~PseudoStates.Hover;
				}
			}
		}

		// Token: 0x06001E92 RID: 7826 RVA: 0x00072784 File Offset: 0x00070984
		private static bool IsPartOfCapturedChain(VisualElement self, in IEventHandler capturingElement)
		{
			bool flag = self == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = capturingElement == null;
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					bool flag4 = capturingElement == self;
					flag2 = flag4 || self.Contains(capturingElement as VisualElement);
				}
			}
			return flag2;
		}

		// Token: 0x06001E93 RID: 7827 RVA: 0x000727CC File Offset: 0x000709CC
		internal void UpdateHoverPseudoStateAfterCaptureChange(int pointerId)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.parent)
			{
				visualElement.UpdateHoverPseudoState();
			}
			BaseVisualElementPanel elementPanel = this.elementPanel;
			VisualElement visualElement2 = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(pointerId) : null);
			VisualElement visualElement3 = visualElement2;
			while (visualElement3 != null && visualElement3 != this)
			{
				visualElement3.UpdateHoverPseudoState();
				visualElement3 = visualElement3.parent;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0007282F File Offset: 0x00070A2F
		// (set) Token: 0x06001E95 RID: 7829 RVA: 0x00072838 File Offset: 0x00070A38
		[CreateProperty]
		public PickingMode pickingMode
		{
			get
			{
				return this.m_PickingMode;
			}
			set
			{
				bool flag = this.m_PickingMode == value;
				if (!flag)
				{
					this.m_PickingMode = value;
					this.IncrementVersion(VersionChangeType.Picking);
					base.NotifyPropertyChanged(in VisualElement.pickingModeProperty);
				}
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x00072874 File Offset: 0x00070A74
		// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0007288C File Offset: 0x00070A8C
		[CreateProperty]
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
					base.NotifyPropertyChanged(in VisualElement.nameProperty);
				}
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000728C8 File Offset: 0x00070AC8
		internal List<string> classList
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				return this.m_ClassList;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x000728FE File Offset: 0x00070AFE
		internal string fullTypeName
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.typeData.fullTypeName;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0007290B File Offset: 0x00070B0B
		internal string typeName
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return this.typeData.typeName;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x00072918 File Offset: 0x00070B18
		internal ref LayoutNode layoutNode
		{
			get
			{
				return ref this.m_LayoutNode;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x00072930 File Offset: 0x00070B30
		internal ref ComputedStyle computedStyle
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get
			{
				return ref this.m_Style;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00072938 File Offset: 0x00070B38
		internal bool hasInlineStyle
		{
			get
			{
				return this.inlineStyleAccess != null;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001E9E RID: 7838 RVA: 0x00072943 File Offset: 0x00070B43
		// (set) Token: 0x06001E9F RID: 7839 RVA: 0x00072958 File Offset: 0x00070B58
		internal bool styleInitialized
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.StyleInitialized) == VisualElementFlags.StyleInitialized;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.StyleInitialized) : (this.m_Flags & ~VisualElementFlags.StyleInitialized));
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x00072980 File Offset: 0x00070B80
		// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x0007299D File Offset: 0x00070B9D
		internal float opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
			set
			{
				this.style.opacity = value;
			}
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x000729B4 File Offset: 0x00070BB4
		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.imguiContainerDescendantCount += delta;
			}
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x000729F0 File Offset: 0x00070BF0
		public unsafe VisualElement()
		{
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1U);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_Flags = VisualElementFlags.Init;
			this.enabledSelf = true;
			this.focusable = false;
			this.name = string.Empty;
			*this.layoutNode = LayoutManager.SharedManager.CreateNode();
			this.renderHints = RenderHints.None;
			int num;
			int num2;
			int num3;
			int num4;
			EventInterestReflectionUtils.GetDefaultEventInterests(base.GetType(), out num, out num2, out num3, out num4);
			this.m_TrickleDownHandleEventCategories = num3;
			this.m_BubbleUpHandleEventCategories = num4 | num2 | num;
			this.UpdateEventInterestSelfCategories();
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00072B2C File Offset: 0x00070D2C
		~VisualElement()
		{
			LayoutManager.SharedManager.DestroyNode(ref this.m_LayoutNode);
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00072B68 File Offset: 0x00070D68
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual Rect GetTooltipRect()
		{
			return this.worldBound;
		}

		// Token: 0x06001EA6 RID: 7846 RVA: 0x00072B80 File Offset: 0x00070D80
		internal void SetTooltip(TooltipEvent e)
		{
			VisualElement visualElement = e.currentTarget as VisualElement;
			bool flag = visualElement != null && !string.IsNullOrEmpty(visualElement.tooltip);
			if (flag)
			{
				e.rect = visualElement.GetTooltipRect();
				e.tooltip = visualElement.tooltip;
				e.StopImmediatePropagation();
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00072BD8 File Offset: 0x00070DD8
		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00072C28 File Offset: 0x00070E28
		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> list = VisualElementListPool.Get(0);
				try
				{
					list.Add(this);
					this.GatherAllChildren(list);
					EventDispatcherGate? eventDispatcherGate = null;
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = null;
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						eventDispatcherGate2 = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					BaseVisualElementPanel elementPanel = this.elementPanel;
					uint num = ((elementPanel != null) ? elementPanel.hierarchyVersion : 0U);
					EventDispatcherGate? eventDispatcherGate3 = eventDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate4 = eventDispatcherGate2;
						try
						{
							IPanel panel2 = this.panel;
							if (panel2 != null)
							{
								EventDispatcher dispatcher = panel2.dispatcher;
								if (dispatcher != null)
								{
									dispatcher.m_ClickDetector.Cleanup(list);
								}
							}
							foreach (VisualElement visualElement in list)
							{
								visualElement.WillChangePanel(p);
							}
							uint num2 = ((elementPanel != null) ? elementPanel.hierarchyVersion : 0U);
							bool flag4 = num != num2;
							if (flag4)
							{
								list.Clear();
								list.Add(this);
								this.GatherAllChildren(list);
							}
							VisualElementFlags visualElementFlags = ((p != null) ? VisualElementFlags.NeedsAttachToPanelEvent : ((VisualElementFlags)0));
							this.InvokeHierarchyChanged(HierarchyChangeType.DetachedFromPanel, list);
							foreach (VisualElement visualElement2 in list)
							{
								visualElement2.elementPanel = p;
								visualElement2.m_Flags |= visualElementFlags;
								visualElement2.m_CachedNextParentWithEventInterests = null;
							}
							this.InvokeHierarchyChanged(HierarchyChangeType.AttachedToPanel, list);
							foreach (VisualElement visualElement3 in list)
							{
								visualElement3.HasChangedPanel(elementPanel);
							}
						}
						finally
						{
							if (eventDispatcherGate4 != null)
							{
								((IDisposable)eventDispatcherGate4.GetValueOrDefault()).Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate3 != null)
						{
							((IDisposable)eventDispatcherGate3.GetValueOrDefault()).Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(list);
				}
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00072F30 File Offset: 0x00071130
		private void WillChangePanel(BaseVisualElementPanel destinationPanel)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				this.UnregisterRunningAnimations();
				this.CreateBindingRequests();
				this.DetachDataSource();
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == (VisualElementFlags)0;
				if (flag2)
				{
					bool flag3 = this.HasSelfEventInterests(EventBase<DetachFromPanelEvent>.EventCategory);
					if (flag3)
					{
						using (DetachFromPanelEvent pooled = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.elementPanel, destinationPanel))
						{
							pooled.elementTarget = this;
							EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(pooled, this.elementPanel, this);
						}
					}
				}
				this.UnregisterRunningAnimations();
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x00072FD4 File Offset: 0x000711D4
		private void HasChangedPanel(BaseVisualElementPanel prevPanel)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				this.layoutNode.Config = this.elementPanel.layoutConfig;
				this.layoutNode.SoftReset();
				this.RegisterRunningAnimations();
				this.ProcessBindingRequests();
				this.AttachDataSource();
				this.pseudoStates &= ~(PseudoStates.Active | PseudoStates.Hover | PseudoStates.Focus);
				this.m_Flags &= ~VisualElementFlags.HierarchyDisplayed;
				bool flag2 = (this.m_Flags & VisualElementFlags.NeedsAttachToPanelEvent) == VisualElementFlags.NeedsAttachToPanelEvent;
				if (flag2)
				{
					bool flag3 = this.HasSelfEventInterests(EventBase<AttachToPanelEvent>.EventCategory);
					if (flag3)
					{
						using (AttachToPanelEvent pooled = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(prevPanel, this.elementPanel))
						{
							pooled.elementTarget = this;
							EventDispatchUtilities.HandleEventAtTargetAndDefaultPhase(pooled, this.elementPanel, this);
						}
					}
					this.m_Flags &= ~VisualElementFlags.NeedsAttachToPanelEvent;
				}
			}
			else
			{
				this.layoutNode.Config = LayoutManager.SharedManager.GetDefaultConfig();
			}
			this.styleInitialized = false;
			this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
			bool flag4 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag4)
			{
				this.IncrementVersion(VersionChangeType.ViewData);
			}
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00073118 File Offset: 0x00071318
		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, DispatchMode.Default);
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x0007312F File Offset: 0x0007132F
		internal sealed override void SendEvent(EventBase e, DispatchMode dispatchMode)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, dispatchMode);
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x00073146 File Offset: 0x00071346
		internal sealed override void HandleEvent(EventBase e)
		{
			EventDispatchUtilities.HandleEvent(e, this);
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x00073151 File Offset: 0x00071351
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.OnVersionChanged(this, changeType);
			}
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x00073168 File Offset: 0x00071368
		internal void InvokeHierarchyChanged(HierarchyChangeType changeType, IReadOnlyList<VisualElement> additionalContext = null)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.InvokeHierarchyChanged(this, changeType, additionalContext);
			}
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00073180 File Offset: 0x00071380
		[Obsolete("SetEnabledFromHierarchy is deprecated and will be removed in a future release. Please use SetEnabled instead.")]
		protected internal bool SetEnabledFromHierarchy(bool state)
		{
			return this.SetEnabledFromHierarchyPrivate(state);
		}

		// Token: 0x06001EB1 RID: 7857 RVA: 0x0007319C File Offset: 0x0007139C
		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool enabledInHierarchy = this.enabledInHierarchy;
			bool flag = false;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						flag = true;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					flag = true;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				flag = true;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = this.focusController != null && this.focusController.IsFocused(this);
				if (flag3)
				{
					EventDispatcherGate? eventDispatcherGate = null;
					IPanel panel = this.panel;
					bool flag4 = ((panel != null) ? panel.dispatcher : null) != null;
					if (flag4)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = eventDispatcherGate;
					try
					{
						base.BlurImmediately();
					}
					finally
					{
						if (eventDispatcherGate2 != null)
						{
							((IDisposable)eventDispatcherGate2.GetValueOrDefault()).Dispose();
						}
					}
				}
				this.pseudoStates |= PseudoStates.Disabled;
			}
			else
			{
				this.pseudoStates &= ~PseudoStates.Disabled;
			}
			return enabledInHierarchy != this.enabledInHierarchy;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001EB2 RID: 7858 RVA: 0x000732FC File Offset: 0x000714FC
		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x00073334 File Offset: 0x00071534
		[CreateProperty(ReadOnly = true)]
		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001EB4 RID: 7860 RVA: 0x00073356 File Offset: 0x00071556
		// (set) Token: 0x06001EB5 RID: 7861 RVA: 0x00073360 File Offset: 0x00071560
		[CreateProperty]
		public bool enabledSelf
		{
			get
			{
				return this.m_EnabledSelf;
			}
			set
			{
				bool flag = this.m_EnabledSelf == value;
				if (!flag)
				{
					this.m_EnabledSelf = value;
					base.NotifyPropertyChanged(in VisualElement.enabledSelfProperty);
					this.PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x06001EB6 RID: 7862 RVA: 0x00073398 File Offset: 0x00071598
		public void SetEnabled(bool value)
		{
			this.enabledSelf = value;
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000733A4 File Offset: 0x000715A4
		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001EB8 RID: 7864 RVA: 0x000733F0 File Offset: 0x000715F0
		// (set) Token: 0x06001EB9 RID: 7865 RVA: 0x000733F8 File Offset: 0x000715F8
		[CreateProperty]
		public LanguageDirection languageDirection
		{
			get
			{
				return this.m_LanguageDirection;
			}
			set
			{
				bool flag = this.m_LanguageDirection == value;
				if (!flag)
				{
					this.m_LanguageDirection = value;
					this.localLanguageDirection = this.m_LanguageDirection;
					base.NotifyPropertyChanged(in VisualElement.languageDirectionProperty);
				}
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00073435 File Offset: 0x00071635
		// (set) Token: 0x06001EBB RID: 7867 RVA: 0x00073440 File Offset: 0x00071640
		internal LanguageDirection localLanguageDirection
		{
			get
			{
				return this.m_LocalLanguageDirection;
			}
			set
			{
				bool flag = this.m_LocalLanguageDirection == value;
				if (!flag)
				{
					this.m_LocalLanguageDirection = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
					int count = this.m_Children.Count;
					for (int i = 0; i < count; i++)
					{
						bool flag2 = this.m_Children[i].languageDirection == LanguageDirection.Inherit;
						if (flag2)
						{
							this.m_Children[i].localLanguageDirection = this.m_LocalLanguageDirection;
						}
					}
				}
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x000734C0 File Offset: 0x000716C0
		// (set) Token: 0x06001EBD RID: 7869 RVA: 0x000734E0 File Offset: 0x000716E0
		[CreateProperty]
		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				bool visible = this.visible;
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
				bool flag = visible != this.visible;
				if (flag)
				{
					base.NotifyPropertyChanged(in VisualElement.visibleProperty);
				}
			}
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x00073529 File Offset: 0x00071729
		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x00073538 File Offset: 0x00071738
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x00073540 File Offset: 0x00071740
		public Action<MeshGenerationContext> generateVisualContent { get; set; }

		// Token: 0x06001EC1 RID: 7873 RVA: 0x0007354C File Offset: 0x0007174C
		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					using (VisualElement.k_GenerateVisualContentMarker.Auto())
					{
						this.generateVisualContent(mgc);
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x000735C4 File Offset: 0x000717C4
		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0007361C File Offset: 0x0007181C
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00073644 File Offset: 0x00071844
		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = new T();
				}
			}
			else
			{
				string text = "__";
				Type typeFromHandle = typeof(T);
				string text2 = key + text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text2, new T());
				}
				t = serializableJsonDictionary.Get<T>(text2);
			}
			return t;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x0007371C File Offset: 0x0007191C
		internal T GetOrCreateViewData<T>(ScriptableObject existing, string key) where T : ScriptableObject
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = ScriptableObject.CreateInstance<T>();
				}
			}
			else
			{
				string text = "__";
				Type typeFromHandle = typeof(T);
				string text2 = key + text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text2, ScriptableObject.CreateInstance<T>());
				}
				t = serializableJsonDictionary.GetScriptable<T>(text2);
			}
			return t;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x000737F8 File Offset: 0x000719F8
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag2 = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string text = "__";
				Type type = obj.GetType();
				string text2 = key + text + ((type != null) ? type.ToString() : null);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text2);
				if (flag3)
				{
					serializableJsonDictionary.Set<object>(text2, obj);
				}
				else
				{
					serializableJsonDictionary.Overwrite(obj, text2);
				}
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x000738C0 File Offset: 0x00071AC0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x00073910 File Offset: 0x00071B10
		internal bool IsViewDataPersitenceSupportedOnChildren(bool existingState)
		{
			bool flag = existingState;
			bool flag2 = string.IsNullOrEmpty(this.viewDataKey) && this != this.contentContainer;
			if (flag2)
			{
				flag = false;
			}
			bool flag3 = this.parent != null && this == this.parent.contentContainer;
			if (flag3)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x00073966 File Offset: 0x00071B66
		internal void OnViewDataReady(bool enablePersistence)
		{
			this.enableViewDataPersistence = enablePersistence;
			this.OnViewDataReady();
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00002152 File Offset: 0x00000352
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal virtual void OnViewDataReady()
		{
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00073978 File Offset: 0x00071B78
		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0007399C File Offset: 0x00071B9C
		public virtual bool Overlaps(Rect rectangle)
		{
			return this.rect.Overlaps(rectangle, true);
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001ECD RID: 7885 RVA: 0x000739BE File Offset: 0x00071BBE
		// (set) Token: 0x06001ECE RID: 7886 RVA: 0x000739D4 File Offset: 0x00071BD4
		internal bool requireMeasureFunction
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.RequireMeasureFunction) == VisualElementFlags.RequireMeasureFunction;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.RequireMeasureFunction) : (this.m_Flags & ~VisualElementFlags.RequireMeasureFunction));
				bool flag = value && !this.layoutNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !value && this.layoutNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00073A46 File Offset: 0x00071C46
		private void AssignMeasureFunction()
		{
			this.layoutNode.SetOwner(this);
			this.layoutNode.Measure = new LayoutMeasureFunction(VisualElement.Measure);
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00073A6E File Offset: 0x00071C6E
		private void RemoveMeasureFunction()
		{
			this.layoutNode.Measure = null;
			this.layoutNode.SetOwner(null);
		}

		// Token: 0x06001ED1 RID: 7889 RVA: 0x00073A8C File Offset: 0x00071C8C
		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00073AB0 File Offset: 0x00071CB0
		internal unsafe static void Measure(VisualElement ve, ref LayoutNode node, float width, LayoutMeasureMode widthMode, float height, LayoutMeasureMode heightMode, out LayoutSize result)
		{
			result = default(LayoutSize);
			Debug.Assert(node.Equals(*ve.layoutNode), "LayoutNode instance mismatch");
			Vector2 vector = ve.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			float scaledPixelsPerPoint = ve.scaledPixelsPerPoint;
			result = new LayoutSize(AlignmentUtils.RoundToPixelGrid(vector.x, scaledPixelsPerPoint, 0.02f), AlignmentUtils.RoundToPixelGrid(vector.y, scaledPixelsPerPoint, 0.02f));
		}

		// Token: 0x06001ED3 RID: 7891 RVA: 0x00073B24 File Offset: 0x00071D24
		internal void SetSize(Vector2 size)
		{
			Rect layout = this.layout;
			layout.width = size.x;
			layout.height = size.y;
			this.layout = layout;
		}

		// Token: 0x06001ED4 RID: 7892 RVA: 0x00073B5D File Offset: 0x00071D5D
		private unsafe void FinalizeLayout()
		{
			this.layoutNode.CopyFromComputedStyle(*this.computedStyle);
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00073B78 File Offset: 0x00071D78
		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x00073BB0 File Offset: 0x00071DB0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal unsafe void UpdateInlineRule(StyleSheet sheet, StyleRule rule)
		{
			ComputedStyle computedStyle = this.computedStyle.Acquire();
			long matchingRulesHash = this.computedStyle.matchingRulesHash;
			ComputedStyle computedStyle2;
			bool flag = !StyleCache.TryGetValue(matchingRulesHash, out computedStyle2);
			if (flag)
			{
				computedStyle2 = *InitialStyle.Get();
			}
			this.m_Style.CopyFrom(ref computedStyle2);
			this.SetInlineRule(sheet, rule);
			this.FinalizeLayout();
			VersionChangeType versionChangeType = ComputedStyle.CompareChanges(ref computedStyle, this.computedStyle);
			computedStyle.Release();
			this.IncrementVersion(versionChangeType);
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00073C30 File Offset: 0x00071E30
		internal void SetComputedStyle(ref ComputedStyle newStyle)
		{
			bool flag = this.m_Style.matchingRulesHash == newStyle.matchingRulesHash;
			if (!flag)
			{
				VersionChangeType versionChangeType = ComputedStyle.CompareChanges(ref this.m_Style, ref newStyle);
				this.m_Style.CopyFrom(ref newStyle);
				this.FinalizeLayout();
				BaseVisualElementPanel elementPanel = this.elementPanel;
				bool flag2 = ((elementPanel != null) ? elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) : null) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.m_Style.cursor);
				}
				this.IncrementVersion(versionChangeType);
			}
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00073CBC File Offset: 0x00071EBC
		internal void ResetPositionProperties()
		{
			bool flag = !this.hasInlineStyle;
			if (!flag)
			{
				this.style.position = StyleKeyword.Null;
				this.style.marginLeft = StyleKeyword.Null;
				this.style.marginRight = StyleKeyword.Null;
				this.style.marginBottom = StyleKeyword.Null;
				this.style.marginTop = StyleKeyword.Null;
				this.style.left = StyleKeyword.Null;
				this.style.top = StyleKeyword.Null;
				this.style.right = StyleKeyword.Null;
				this.style.bottom = StyleKeyword.Null;
				this.style.width = StyleKeyword.Null;
				this.style.height = StyleKeyword.Null;
			}
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00073DA4 File Offset: 0x00071FA4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout.ToString(),
				" world rect: ",
				this.worldBound.ToString()
			});
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00073E20 File Offset: 0x00072020
		public IEnumerable<string> GetClasses()
		{
			return this.m_ClassList;
		}

		// Token: 0x06001EDB RID: 7899 RVA: 0x00073E38 File Offset: 0x00072038
		internal List<string> GetClassesForIteration()
		{
			return this.m_ClassList;
		}

		// Token: 0x06001EDC RID: 7900 RVA: 0x00073E50 File Offset: 0x00072050
		public void ClearClassList()
		{
			bool flag = this.m_ClassList.Count > 0;
			if (flag)
			{
				ObjectListPool<string>.Release(this.m_ClassList);
				this.m_ClassList = VisualElement.s_EmptyClassList;
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x06001EDD RID: 7901 RVA: 0x00073E94 File Offset: 0x00072094
		public void AddToClassList(string className)
		{
			bool flag = string.IsNullOrEmpty(className);
			if (!flag)
			{
				bool flag2 = this.m_ClassList == VisualElement.s_EmptyClassList;
				if (flag2)
				{
					this.m_ClassList = ObjectListPool<string>.Get();
				}
				else
				{
					bool flag3 = this.m_ClassList.Contains(className);
					if (flag3)
					{
						return;
					}
					bool flag4 = this.m_ClassList.Capacity == this.m_ClassList.Count;
					if (flag4)
					{
						this.m_ClassList.Capacity++;
					}
				}
				this.m_ClassList.Add(className);
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00073F2C File Offset: 0x0007212C
		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				bool flag2 = this.m_ClassList.Count == 0;
				if (flag2)
				{
					ObjectListPool<string>.Release(this.m_ClassList);
					this.m_ClassList = VisualElement.s_EmptyClassList;
				}
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x06001EDF RID: 7903 RVA: 0x00073F80 File Offset: 0x00072180
		public void ToggleInClassList(string className)
		{
			bool flag = this.ClassListContains(className);
			if (flag)
			{
				this.RemoveFromClassList(className);
			}
			else
			{
				this.AddToClassList(className);
			}
		}

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00073FAC File Offset: 0x000721AC
		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00073FD4 File Offset: 0x000721D4
		public bool ClassListContains(string cls)
		{
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i].Equals(cls, StringComparison.Ordinal);
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00074020 File Offset: 0x00072220
		public object FindAncestorUserData()
		{
			for (VisualElement visualElement = this.parent; visualElement != null; visualElement = visualElement.parent)
			{
				bool flag = visualElement.userData != null;
				if (flag)
				{
					return visualElement.userData;
				}
			}
			return null;
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00074064 File Offset: 0x00072264
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			bool flag = this.m_PropertyBag != null;
			object obj2;
			if (flag)
			{
				object obj;
				this.m_PropertyBag.TryGetValue(key, out obj);
				obj2 = obj;
			}
			else
			{
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x0007409F File Offset: 0x0007229F
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		// Token: 0x06001EE5 RID: 7909 RVA: 0x000740B4 File Offset: 0x000722B4
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal bool HasProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			Dictionary<PropertyName, object> propertyBag = this.m_PropertyBag;
			return propertyBag != null && propertyBag.ContainsKey(key);
		}

		// Token: 0x06001EE6 RID: 7910 RVA: 0x000740E0 File Offset: 0x000722E0
		internal bool ClearProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			Dictionary<PropertyName, object> propertyBag = this.m_PropertyBag;
			return propertyBag != null && propertyBag.Remove(key);
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0007410C File Offset: 0x0007230C
		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x00074158 File Offset: 0x00072358
		private void SetPropertyInternal(PropertyName key, object value)
		{
			if (this.m_PropertyBag == null)
			{
				this.m_PropertyBag = new Dictionary<PropertyName, object>();
			}
			this.m_PropertyBag[key] = value;
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0007417C File Offset: 0x0007237C
		internal void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel == null;
			if (!flag)
			{
				bool flag2 = eventType == EventBase<MouseCaptureOutEvent>.TypeId();
				if (flag2)
				{
					VisualElement topElementUnderPointer = this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId);
					bool flag3 = topElementUnderPointer != null;
					if (flag3)
					{
						this.elementPanel.cursorManager.SetCursor(topElementUnderPointer.computedStyle.cursor);
					}
					else
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
				else
				{
					IEventHandler capturingElement = this.elementPanel.GetCapturingElement(PointerId.mousePointerId);
					bool flag4 = capturingElement != null && capturingElement != this;
					if (!flag4)
					{
						bool flag5 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
						if (flag5)
						{
							this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor);
						}
						else
						{
							bool flag6 = eventType == EventBase<MouseOutEvent>.TypeId() && capturingElement == null;
							if (flag6)
							{
								this.elementPanel.cursorManager.ResetCursor();
							}
						}
					}
				}
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00074290 File Offset: 0x00072490
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x000742A8 File Offset: 0x000724A8
		internal VisualElement.RenderTargetMode subRenderTargetMode
		{
			get
			{
				return this.m_SubRenderTargetMode;
			}
			set
			{
				bool flag = this.m_SubRenderTargetMode == value;
				if (!flag)
				{
					this.m_SubRenderTargetMode = value;
					this.IncrementVersion(VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x000742D8 File Offset: 0x000724D8
		private Material getRuntimeMaterial()
		{
			bool flag = VisualElement.s_runtimeMaterial != null;
			Material material;
			if (flag)
			{
				material = VisualElement.s_runtimeMaterial;
			}
			else
			{
				Shader shader = Shader.Find(UIRUtility.k_DefaultShaderName);
				Debug.Assert(shader != null, "Failed to load UIElements default shader");
				bool flag2 = shader != null;
				if (flag2)
				{
					shader.hideFlags |= HideFlags.DontSaveInEditor;
					Material material2 = new Material(shader);
					material2.hideFlags |= HideFlags.DontSaveInEditor;
					material = (VisualElement.s_runtimeMaterial = material2);
				}
				else
				{
					material = null;
				}
			}
			return material;
		}

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00074360 File Offset: 0x00072560
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x00074378 File Offset: 0x00072578
		internal Material defaultMaterial
		{
			get
			{
				return this.m_defaultMaterial;
			}
			private set
			{
				bool flag = this.m_defaultMaterial == value;
				if (!flag)
				{
					this.m_defaultMaterial = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				}
			}
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x000743AC File Offset: 0x000725AC
		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem visualElementAnimationSystem;
			if (flag)
			{
				visualElementAnimationSystem = this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem;
			}
			else
			{
				visualElementAnimationSystem = null;
			}
			return visualElementAnimationSystem;
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x000743E4 File Offset: 0x000725E4
		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.RegisterAnimation(anim);
			}
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00074434 File Offset: 0x00072634
		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.UnregisterAnimation(anim);
			}
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00074478 File Offset: 0x00072678
		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
			this.styleAnimation.CancelAllAnimations();
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x000744D0 File Offset: 0x000726D0
		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0007451C File Offset: 0x0007271C
		ValueAnimation<float> ITransitionAnimations.Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0007455C File Offset: 0x0007275C
		ValueAnimation<Rect> ITransitionAnimations.Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x0007459C File Offset: 0x0007279C
		ValueAnimation<Color> ITransitionAnimations.Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x000745DC File Offset: 0x000727DC
		ValueAnimation<Vector3> ITransitionAnimations.Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0007461C File Offset: 0x0007281C
		ValueAnimation<Vector2> ITransitionAnimations.Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0007465C File Offset: 0x0007285C
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0007469C File Offset: 0x0007289C
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues from, StyleValues to, int durationMs)
		{
			bool flag = from.m_StyleValues == null;
			if (flag)
			{
				from.Values();
			}
			bool flag2 = to.m_StyleValues == null;
			if (flag2)
			{
				to.Values();
			}
			return this.Start((VisualElement e) => from, to, durationMs);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x00074704 File Offset: 0x00072904
		ValueAnimation<float> ITransitionAnimations.Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return VisualElement.StartAnimation<float>(ValueAnimation<float>.Create(this, new Func<float, float, float, float>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x00074734 File Offset: 0x00072934
		ValueAnimation<Rect> ITransitionAnimations.Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return VisualElement.StartAnimation<Rect>(ValueAnimation<Rect>.Create(this, new Func<Rect, Rect, float, Rect>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x00074764 File Offset: 0x00072964
		ValueAnimation<Color> ITransitionAnimations.Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return VisualElement.StartAnimation<Color>(ValueAnimation<Color>.Create(this, new Func<Color, Color, float, Color>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x00074794 File Offset: 0x00072994
		ValueAnimation<Vector3> ITransitionAnimations.Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector3>(ValueAnimation<Vector3>.Create(this, new Func<Vector3, Vector3, float, Vector3>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x000747C4 File Offset: 0x000729C4
		ValueAnimation<Vector2> ITransitionAnimations.Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector2>(ValueAnimation<Vector2>.Create(this, new Func<Vector2, Vector2, float, Vector2>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x000747F4 File Offset: 0x000729F4
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return VisualElement.StartAnimation<Quaternion>(ValueAnimation<Quaternion>.Create(this, new Func<Quaternion, Quaternion, float, Quaternion>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x00074824 File Offset: 0x00072A24
		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x00074860 File Offset: 0x00072A60
		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle style = ve.style;
			bool flag = src.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue styleValue in src.m_StyleValues.m_Values)
				{
					StylePropertyId id = styleValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId <= StylePropertyId.Color)
						{
							if (stylePropertyId != StylePropertyId.Unknown)
							{
								if (stylePropertyId == StylePropertyId.Color)
								{
									style.color = styleValue.color;
								}
							}
						}
						else if (stylePropertyId != StylePropertyId.FontSize)
						{
							switch (stylePropertyId)
							{
							case StylePropertyId.BorderBottomWidth:
								style.borderBottomWidth = styleValue.number;
								break;
							case StylePropertyId.BorderLeftWidth:
								style.borderLeftWidth = styleValue.number;
								break;
							case StylePropertyId.BorderRightWidth:
								style.borderRightWidth = styleValue.number;
								break;
							case StylePropertyId.BorderTopWidth:
								style.borderTopWidth = styleValue.number;
								break;
							case StylePropertyId.Bottom:
								style.bottom = styleValue.number;
								break;
							case StylePropertyId.FlexGrow:
								style.flexGrow = styleValue.number;
								break;
							case StylePropertyId.FlexShrink:
								style.flexShrink = styleValue.number;
								break;
							case StylePropertyId.Height:
								style.height = styleValue.number;
								break;
							case StylePropertyId.Left:
								style.left = styleValue.number;
								break;
							case StylePropertyId.MarginBottom:
								style.marginBottom = styleValue.number;
								break;
							case StylePropertyId.MarginLeft:
								style.marginLeft = styleValue.number;
								break;
							case StylePropertyId.MarginRight:
								style.marginRight = styleValue.number;
								break;
							case StylePropertyId.MarginTop:
								style.marginTop = styleValue.number;
								break;
							case StylePropertyId.PaddingBottom:
								style.paddingBottom = styleValue.number;
								break;
							case StylePropertyId.PaddingLeft:
								style.paddingLeft = styleValue.number;
								break;
							case StylePropertyId.PaddingRight:
								style.paddingRight = styleValue.number;
								break;
							case StylePropertyId.PaddingTop:
								style.paddingTop = styleValue.number;
								break;
							case StylePropertyId.Right:
								style.right = styleValue.number;
								break;
							case StylePropertyId.Top:
								style.top = styleValue.number;
								break;
							case StylePropertyId.Width:
								style.width = styleValue.number;
								break;
							}
						}
						else
						{
							style.fontSize = styleValue.number;
						}
					}
					else if (stylePropertyId <= StylePropertyId.BorderColor)
					{
						if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
						{
							if (stylePropertyId == StylePropertyId.BorderColor)
							{
								style.borderLeftColor = styleValue.color;
								style.borderTopColor = styleValue.color;
								style.borderRightColor = styleValue.color;
								style.borderBottomColor = styleValue.color;
							}
						}
						else
						{
							style.unityBackgroundImageTintColor = styleValue.color;
						}
					}
					else if (stylePropertyId != StylePropertyId.BackgroundColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BorderBottomLeftRadius:
							style.borderBottomLeftRadius = styleValue.number;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							style.borderBottomRightRadius = styleValue.number;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							style.borderTopLeftRadius = styleValue.number;
							break;
						case StylePropertyId.BorderTopRightRadius:
							style.borderTopRightRadius = styleValue.number;
							break;
						case StylePropertyId.Opacity:
							style.opacity = styleValue.number;
							break;
						}
					}
					else
					{
						style.backgroundColor = styleValue.color;
					}
				}
			}
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x00074D00 File Offset: 0x00072F00
		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues styleValues = default(StyleValues);
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			bool flag = targetValuesToRead.m_StyleValues != null;
			if (flag)
			{
				foreach (StyleValue styleValue in targetValuesToRead.m_StyleValues.m_Values)
				{
					StylePropertyId id = styleValue.id;
					StylePropertyId stylePropertyId = id;
					if (stylePropertyId <= StylePropertyId.Width)
					{
						if (stylePropertyId != StylePropertyId.Unknown)
						{
							if (stylePropertyId != StylePropertyId.Color)
							{
								switch (stylePropertyId)
								{
								case StylePropertyId.BorderBottomWidth:
									styleValues.borderBottomWidth = resolvedStyle.borderBottomWidth;
									break;
								case StylePropertyId.BorderLeftWidth:
									styleValues.borderLeftWidth = resolvedStyle.borderLeftWidth;
									break;
								case StylePropertyId.BorderRightWidth:
									styleValues.borderRightWidth = resolvedStyle.borderRightWidth;
									break;
								case StylePropertyId.BorderTopWidth:
									styleValues.borderTopWidth = resolvedStyle.borderTopWidth;
									break;
								case StylePropertyId.Bottom:
									styleValues.bottom = resolvedStyle.bottom;
									break;
								case StylePropertyId.FlexGrow:
									styleValues.flexGrow = resolvedStyle.flexGrow;
									break;
								case StylePropertyId.FlexShrink:
									styleValues.flexShrink = resolvedStyle.flexShrink;
									break;
								case StylePropertyId.Height:
									styleValues.height = resolvedStyle.height;
									break;
								case StylePropertyId.Left:
									styleValues.left = resolvedStyle.left;
									break;
								case StylePropertyId.MarginBottom:
									styleValues.marginBottom = resolvedStyle.marginBottom;
									break;
								case StylePropertyId.MarginLeft:
									styleValues.marginLeft = resolvedStyle.marginLeft;
									break;
								case StylePropertyId.MarginRight:
									styleValues.marginRight = resolvedStyle.marginRight;
									break;
								case StylePropertyId.MarginTop:
									styleValues.marginTop = resolvedStyle.marginTop;
									break;
								case StylePropertyId.PaddingBottom:
									styleValues.paddingBottom = resolvedStyle.paddingBottom;
									break;
								case StylePropertyId.PaddingLeft:
									styleValues.paddingLeft = resolvedStyle.paddingLeft;
									break;
								case StylePropertyId.PaddingRight:
									styleValues.paddingRight = resolvedStyle.paddingRight;
									break;
								case StylePropertyId.PaddingTop:
									styleValues.paddingTop = resolvedStyle.paddingTop;
									break;
								case StylePropertyId.Right:
									styleValues.right = resolvedStyle.right;
									break;
								case StylePropertyId.Top:
									styleValues.top = resolvedStyle.top;
									break;
								case StylePropertyId.Width:
									styleValues.width = resolvedStyle.width;
									break;
								}
							}
							else
							{
								styleValues.color = resolvedStyle.color;
							}
						}
					}
					else if (stylePropertyId <= StylePropertyId.BorderColor)
					{
						if (stylePropertyId != StylePropertyId.UnityBackgroundImageTintColor)
						{
							if (stylePropertyId == StylePropertyId.BorderColor)
							{
								styleValues.borderColor = resolvedStyle.borderLeftColor;
							}
						}
						else
						{
							styleValues.unityBackgroundImageTintColor = resolvedStyle.unityBackgroundImageTintColor;
						}
					}
					else if (stylePropertyId != StylePropertyId.BackgroundColor)
					{
						switch (stylePropertyId)
						{
						case StylePropertyId.BorderBottomLeftRadius:
							styleValues.borderBottomLeftRadius = resolvedStyle.borderBottomLeftRadius;
							break;
						case StylePropertyId.BorderBottomRightRadius:
							styleValues.borderBottomRightRadius = resolvedStyle.borderBottomRightRadius;
							break;
						case StylePropertyId.BorderTopLeftRadius:
							styleValues.borderTopLeftRadius = resolvedStyle.borderTopLeftRadius;
							break;
						case StylePropertyId.BorderTopRightRadius:
							styleValues.borderTopRightRadius = resolvedStyle.borderTopRightRadius;
							break;
						case StylePropertyId.Opacity:
							styleValues.opacity = resolvedStyle.opacity;
							break;
						}
					}
					else
					{
						styleValues.backgroundColor = resolvedStyle.backgroundColor;
					}
				}
			}
			return styleValues;
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x000750CC File Offset: 0x000732CC
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			bool flag = to.m_StyleValues == null;
			if (flag)
			{
				to.Values();
			}
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0007512C File Offset: 0x0007332C
		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x00075164 File Offset: 0x00073364
		ValueAnimation<Rect> ITransitionAnimations.Layout(Rect to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height), to, durationMs, delegate(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			});
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000751C8 File Offset: 0x000733C8
		ValueAnimation<Vector2> ITransitionAnimations.TopLeft(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Vector2(e.resolvedStyle.left, e.resolvedStyle.top), to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			});
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0007522C File Offset: 0x0007342C
		ValueAnimation<Vector2> ITransitionAnimations.Size(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.layout.size, to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			});
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x00075290 File Offset: 0x00073490
		ValueAnimation<float> ITransitionAnimations.Scale(float to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.scale.x, to, durationMs, delegate(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			});
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000752F4 File Offset: 0x000734F4
		ValueAnimation<Vector3> ITransitionAnimations.Position(Vector3 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.position, to, durationMs, delegate(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			});
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x00075358 File Offset: 0x00073558
		ValueAnimation<Quaternion> ITransitionAnimations.Rotation(Quaternion to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.rotation, to, durationMs, delegate(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			});
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x000753BA File Offset: 0x000735BA
		// (set) Token: 0x06001F0D RID: 7949 RVA: 0x000753C4 File Offset: 0x000735C4
		[CreateProperty]
		public object dataSource
		{
			get
			{
				return this.m_DataSource;
			}
			set
			{
				bool flag = this.m_DataSource == value;
				if (!flag)
				{
					object dataSource = this.m_DataSource;
					this.m_DataSource = value;
					this.TrackSource(dataSource, this.m_DataSource);
					this.IncrementVersion(VersionChangeType.DataSource);
					base.NotifyPropertyChanged(in VisualElement.dataSourceProperty);
				}
			}
		}

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06001F0E RID: 7950 RVA: 0x00075415 File Offset: 0x00073615
		// (set) Token: 0x06001F0F RID: 7951 RVA: 0x00075422 File Offset: 0x00073622
		[UxmlAttributeBindingPath("dataSource")]
		internal Object dataSourceUnityObject
		{
			get
			{
				return this.dataSource as Object;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x0007542C File Offset: 0x0007362C
		// (set) Token: 0x06001F11 RID: 7953 RVA: 0x00075434 File Offset: 0x00073634
		[CreateProperty]
		public PropertyPath dataSourcePath
		{
			get
			{
				return this.m_DataSourcePath;
			}
			set
			{
				bool flag = this.m_DataSourcePath == value;
				if (!flag)
				{
					this.m_DataSourcePath = value;
					this.IncrementVersion(VersionChangeType.DataSource);
					base.NotifyPropertyChanged(in VisualElement.dataSourcePathProperty);
				}
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x00075474 File Offset: 0x00073674
		// (set) Token: 0x06001F13 RID: 7955 RVA: 0x00075495 File Offset: 0x00073695
		internal string dataSourcePathString
		{
			get
			{
				return this.dataSourcePath.ToString();
			}
			set
			{
				this.dataSourcePath = new PropertyPath(value);
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000754A4 File Offset: 0x000736A4
		// (set) Token: 0x06001F15 RID: 7957 RVA: 0x000754C9 File Offset: 0x000736C9
		private List<Binding> bindings
		{
			get
			{
				List<Binding> list;
				if ((list = this.m_Bindings) == null)
				{
					list = (this.m_Bindings = new List<Binding>());
				}
				return list;
			}
			set
			{
				this.m_Bindings = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000754D2 File Offset: 0x000736D2
		// (set) Token: 0x06001F17 RID: 7959 RVA: 0x000754DA File Offset: 0x000736DA
		public Type dataSourceType { get; set; }

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x000754E3 File Offset: 0x000736E3
		// (set) Token: 0x06001F19 RID: 7961 RVA: 0x000754F0 File Offset: 0x000736F0
		internal string dataSourceTypeString
		{
			get
			{
				return UxmlUtility.TypeToString(this.dataSourceType);
			}
			set
			{
				this.dataSourceType = UxmlUtility.ParseType(value, null);
			}
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x00075500 File Offset: 0x00073700
		public void SetBinding(BindingId bindingId, Binding binding)
		{
			this.RegisterBinding(bindingId, binding);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0007550C File Offset: 0x0007370C
		public Binding GetBinding(BindingId bindingId)
		{
			Binding binding;
			return this.TryGetBinding(bindingId, out binding) ? binding : null;
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x00075530 File Offset: 0x00073730
		public bool TryGetBinding(BindingId bindingId, out Binding binding)
		{
			BindingInfo bindingInfo;
			bool flag = DataBindingUtility.TryGetBinding(this, in bindingId, out bindingInfo);
			bool flag2;
			if (flag)
			{
				binding = bindingInfo.binding;
				flag2 = true;
			}
			else
			{
				binding = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001F1D RID: 7965 RVA: 0x00075562 File Offset: 0x00073762
		public IEnumerable<BindingInfo> GetBindingInfos()
		{
			VisualElement.<GetBindingInfos>d__582 <GetBindingInfos>d__ = new VisualElement.<GetBindingInfos>d__582(-2);
			<GetBindingInfos>d__.<>4__this = this;
			return <GetBindingInfos>d__;
		}

		// Token: 0x06001F1E RID: 7966 RVA: 0x00075574 File Offset: 0x00073774
		public bool HasBinding(BindingId bindingId)
		{
			Binding binding;
			return this.TryGetBinding(bindingId, out binding);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00075590 File Offset: 0x00073790
		public void ClearBinding(BindingId bindingId)
		{
			this.SetBinding(bindingId, null);
			List<Binding> bindings = this.bindings;
			if (bindings != null)
			{
				bindings.RemoveAll(delegate(Binding b)
				{
					BindingId bindingId2 = b.property;
					return (in bindingId2) == (in bindingId);
				});
			}
		}

		// Token: 0x06001F20 RID: 7968 RVA: 0x000755D8 File Offset: 0x000737D8
		public void ClearBindings()
		{
			DataBindingManager.CreateClearAllBindingsRequest(this);
			List<Binding> bindings = this.bindings;
			if (bindings != null)
			{
				bindings.Clear();
			}
			bool flag = this.panel != null;
			if (flag)
			{
				this.ProcessBindingRequests();
			}
		}

		// Token: 0x06001F21 RID: 7969 RVA: 0x00075614 File Offset: 0x00073814
		public DataSourceContext GetHierarchicalDataSourceContext()
		{
			VisualElement visualElement = this;
			PropertyPath propertyPath = default(PropertyPath);
			while (visualElement != null)
			{
				PropertyPath propertyPath2 = visualElement.dataSourcePath;
				bool flag = !propertyPath2.IsEmpty;
				if (flag)
				{
					propertyPath2 = visualElement.dataSourcePath;
					propertyPath = PropertyPath.Combine(in propertyPath2, in propertyPath);
				}
				bool flag2 = visualElement.dataSource != null;
				if (flag2)
				{
					object dataSource = visualElement.dataSource;
					return new DataSourceContext(dataSource, in propertyPath);
				}
				visualElement = visualElement.hierarchy.parent;
			}
			return new DataSourceContext(null, in propertyPath);
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x000756A4 File Offset: 0x000738A4
		public DataSourceContext GetDataSourceContext(BindingId bindingId)
		{
			DataSourceContext dataSourceContext;
			bool flag = this.TryGetDataSourceContext(bindingId, out dataSourceContext);
			if (flag)
			{
				return dataSourceContext;
			}
			throw new ArgumentOutOfRangeException("bindingId", string.Format("[UI Toolkit] could not get binding with id '{0}' on the element.", bindingId));
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x000756E0 File Offset: 0x000738E0
		public bool TryGetDataSourceContext(BindingId bindingId, out DataSourceContext context)
		{
			Binding binding = this.GetBinding(bindingId);
			Binding binding2 = binding;
			Binding binding3 = binding2;
			bool flag;
			if (binding3 != null)
			{
				IDataSourceProvider dataSourceProvider = binding3 as IDataSourceProvider;
				if (dataSourceProvider != null)
				{
					if (dataSourceProvider.dataSource != null)
					{
						object dataSource = dataSourceProvider.dataSource;
						PropertyPath propertyPath = dataSourceProvider.dataSourcePath;
						context = new DataSourceContext(dataSource, in propertyPath);
						goto IL_00BD;
					}
					bool isEmpty = dataSourceProvider.dataSourcePath.IsEmpty;
					if (!isEmpty)
					{
						IDataSourceProvider dataSourceProvider2 = dataSourceProvider;
						DataSourceContext hierarchicalDataSourceContext = this.GetHierarchicalDataSourceContext();
						object dataSource2 = hierarchicalDataSourceContext.dataSource;
						PropertyPath propertyPath = hierarchicalDataSourceContext.dataSourcePath;
						PropertyPath dataSourcePath = dataSourceProvider2.dataSourcePath;
						PropertyPath propertyPath2 = PropertyPath.Combine(in propertyPath, in dataSourcePath);
						context = new DataSourceContext(dataSource2, in propertyPath2);
						goto IL_00BD;
					}
				}
				context = this.GetHierarchicalDataSourceContext();
				IL_00BD:
				flag = true;
			}
			else
			{
				context = default(DataSourceContext);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x000757B4 File Offset: 0x000739B4
		public bool TryGetLastBindingToUIResult(in BindingId bindingId, out BindingResult result)
		{
			bool flag = this.elementPanel == null;
			bool flag2;
			if (flag)
			{
				result = default(BindingResult);
				flag2 = false;
			}
			else
			{
				DataBindingManager dataBindingManager = this.elementPanel.dataBindingManager;
				DataBindingManager.BindingData bindingData;
				bool flag3 = dataBindingManager.TryGetBindingData(this, in bindingId, out bindingData) && dataBindingManager.TryGetLastUIBindingResult(bindingData, out result);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					result = default(BindingResult);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001F25 RID: 7973 RVA: 0x00075818 File Offset: 0x00073A18
		public bool TryGetLastBindingToSourceResult(in BindingId bindingId, out BindingResult result)
		{
			bool flag = this.elementPanel == null;
			bool flag2;
			if (flag)
			{
				result = default(BindingResult);
				flag2 = false;
			}
			else
			{
				DataBindingManager dataBindingManager = this.elementPanel.dataBindingManager;
				DataBindingManager.BindingData bindingData;
				bool flag3 = dataBindingManager.TryGetBindingData(this, in bindingId, out bindingData) && dataBindingManager.TryGetLastSourceBindingResult(bindingData, out result);
				if (flag3)
				{
					flag2 = true;
				}
				else
				{
					result = default(BindingResult);
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0007587C File Offset: 0x00073A7C
		private void RegisterBinding(BindingId bindingId, Binding binding)
		{
			this.AddBindingRequest(bindingId, binding);
			bool flag = this.panel != null;
			if (flag)
			{
				this.ProcessBindingRequests();
			}
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x000758A7 File Offset: 0x00073AA7
		internal void AddBindingRequest(BindingId bindingId, Binding binding)
		{
			DataBindingManager.CreateBindingRequest(this, in bindingId, binding);
		}

		// Token: 0x06001F28 RID: 7976 RVA: 0x000758B4 File Offset: 0x00073AB4
		private void ProcessBindingRequests()
		{
			Assert.IsFalse(this.elementPanel == null, null);
			bool flag = DataBindingManager.AnyPendingBindingRequests(this);
			if (flag)
			{
				this.IncrementVersion(VersionChangeType.BindingRegistration);
			}
		}

		// Token: 0x06001F29 RID: 7977 RVA: 0x000758E8 File Offset: 0x00073AE8
		private void CreateBindingRequests()
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			Assert.IsFalse(elementPanel == null, null);
			elementPanel.dataBindingManager.TransferBindingRequests(this);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x00075918 File Offset: 0x00073B18
		private void TrackSource(object previous, object current)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			DataBindingManager dataBindingManager = ((elementPanel != null) ? elementPanel.dataBindingManager : null);
			bool flag = dataBindingManager == null;
			if (!flag)
			{
				bool flag2 = (this.m_Flags & VisualElementFlags.DetachedDataSource) == VisualElementFlags.DetachedDataSource;
				if (!flag2)
				{
					BaseVisualElementPanel elementPanel2 = this.elementPanel;
					if (elementPanel2 != null)
					{
						elementPanel2.dataBindingManager.TrackDataSource(previous, current);
					}
				}
			}
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x00075975 File Offset: 0x00073B75
		private void DetachDataSource()
		{
			this.TrackSource(this.dataSource, null);
			this.m_Flags |= VisualElementFlags.DetachedDataSource;
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x00075998 File Offset: 0x00073B98
		private void AttachDataSource()
		{
			this.m_Flags &= ~VisualElementFlags.DetachedDataSource;
			this.TrackSource(null, this.dataSource);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x000759BC File Offset: 0x00073BBC
		private void DirtyNextParentWithEventInterests()
		{
			bool flag = this.m_CachedNextParentWithEventInterests != null && this.m_NextParentCachedVersion == this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion;
			if (flag)
			{
				this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
			}
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x00075A08 File Offset: 0x00073C08
		internal void SetAsNextParentWithEventInterests()
		{
			bool flag = this.m_NextParentRequiredVersion > 0U;
			if (!flag)
			{
				this.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
				bool flag2 = this.m_CachedNextParentWithEventInterests != null && this.m_NextParentCachedVersion == this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion;
				if (flag2)
				{
					this.m_CachedNextParentWithEventInterests.m_NextParentRequiredVersion = (VisualElement.s_NextParentVersion += 1U);
				}
			}
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x00075A74 File Offset: 0x00073C74
		internal bool GetCachedNextParentWithEventInterests(out VisualElement nextParent)
		{
			nextParent = this.m_CachedNextParentWithEventInterests;
			return nextParent != null && nextParent.m_NextParentRequiredVersion == this.m_NextParentCachedVersion;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001F30 RID: 7984 RVA: 0x00075AA4 File Offset: 0x00073CA4
		internal VisualElement nextParentWithEventInterests
		{
			get
			{
				VisualElement visualElement;
				bool cachedNextParentWithEventInterests = this.GetCachedNextParentWithEventInterests(out visualElement);
				VisualElement visualElement2;
				if (cachedNextParentWithEventInterests)
				{
					visualElement2 = visualElement;
				}
				else
				{
					for (VisualElement visualElement3 = this.hierarchy.parent; visualElement3 != null; visualElement3 = visualElement3.hierarchy.parent)
					{
						bool flag = visualElement3.m_NextParentRequiredVersion > 0U;
						if (flag)
						{
							this.PropagateCachedNextParentWithEventInterests(visualElement3, visualElement3);
							return visualElement3;
						}
						VisualElement visualElement4;
						bool cachedNextParentWithEventInterests2 = visualElement3.GetCachedNextParentWithEventInterests(out visualElement4);
						if (cachedNextParentWithEventInterests2)
						{
							this.PropagateCachedNextParentWithEventInterests(visualElement4, visualElement3);
							return visualElement4;
						}
					}
					this.m_CachedNextParentWithEventInterests = null;
					visualElement2 = null;
				}
				return visualElement2;
			}
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00075B38 File Offset: 0x00073D38
		private void PropagateCachedNextParentWithEventInterests(VisualElement nextParent, VisualElement stopParent)
		{
			for (VisualElement visualElement = this; visualElement != stopParent; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.m_CachedNextParentWithEventInterests = nextParent;
				visualElement.m_NextParentCachedVersion = nextParent.m_NextParentRequiredVersion;
			}
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x00075B7C File Offset: 0x00073D7C
		internal void AddEventCallbackCategories(int eventCategories, TrickleDown trickleDown)
		{
			bool flag = trickleDown == TrickleDown.TrickleDown;
			if (flag)
			{
				this.m_TrickleDownEventCallbackCategories |= eventCategories;
			}
			else
			{
				this.m_BubbleUpEventCallbackCategories |= eventCategories;
			}
			this.UpdateEventInterestSelfCategories();
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x00075BB8 File Offset: 0x00073DB8
		internal void RemoveEventCallbackCategories(int eventCategories, TrickleDown trickleDown)
		{
			bool flag = trickleDown == TrickleDown.TrickleDown;
			if (flag)
			{
				this.m_TrickleDownEventCallbackCategories &= ~eventCategories;
			}
			else
			{
				this.m_BubbleUpEventCallbackCategories &= ~eventCategories;
			}
			this.UpdateEventInterestSelfCategories();
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x00075BF5 File Offset: 0x00073DF5
		internal int eventInterestSelfCategories
		{
			get
			{
				return this.m_EventInterestSelfCategories;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x00075C00 File Offset: 0x00073E00
		internal int eventInterestParentCategories
		{
			get
			{
				bool flag = this.elementPanel == null;
				int num;
				if (flag)
				{
					num = -1;
				}
				else
				{
					bool isEventInterestParentCategoriesDirty = this.isEventInterestParentCategoriesDirty;
					if (isEventInterestParentCategoriesDirty)
					{
						this.UpdateEventInterestParentCategories();
						this.isEventInterestParentCategoriesDirty = false;
					}
					num = this.m_CachedEventInterestParentCategories;
				}
				return num;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x00075C44 File Offset: 0x00073E44
		// (set) Token: 0x06001F37 RID: 7991 RVA: 0x00075C53 File Offset: 0x00073E53
		internal bool isEventInterestParentCategoriesDirty
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.EventInterestParentCategoriesDirty) == VisualElementFlags.EventInterestParentCategoriesDirty;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.EventInterestParentCategoriesDirty) : (this.m_Flags & ~VisualElementFlags.EventInterestParentCategoriesDirty));
			}
		}

		// Token: 0x06001F38 RID: 7992 RVA: 0x00075C74 File Offset: 0x00073E74
		private void UpdateEventInterestSelfCategories()
		{
			int num = this.m_TrickleDownHandleEventCategories | this.m_BubbleUpHandleEventCategories | this.m_TrickleDownEventCallbackCategories | this.m_BubbleUpEventCallbackCategories;
			bool flag = this.m_EventInterestSelfCategories != num;
			if (flag)
			{
				int num2 = this.m_EventInterestSelfCategories ^ num;
				bool flag2 = (num2 & -5537) != 0;
				if (flag2)
				{
					this.SetAsNextParentWithEventInterests();
					this.IncrementVersion(VersionChangeType.EventCallbackCategories);
				}
				else
				{
					this.m_CachedEventInterestParentCategories |= num;
				}
				this.m_EventInterestSelfCategories = num;
			}
		}

		// Token: 0x06001F39 RID: 7993 RVA: 0x00075CF8 File Offset: 0x00073EF8
		private void UpdateEventInterestParentCategories()
		{
			this.m_CachedEventInterestParentCategories = this.m_EventInterestSelfCategories;
			VisualElement nextParentWithEventInterests = this.nextParentWithEventInterests;
			bool flag = nextParentWithEventInterests == null;
			if (!flag)
			{
				this.m_CachedEventInterestParentCategories |= nextParentWithEventInterests.eventInterestParentCategories;
				bool flag2 = this.hierarchy.parent != null;
				if (flag2)
				{
					for (VisualElement visualElement = this.hierarchy.parent; visualElement != nextParentWithEventInterests; visualElement = visualElement.hierarchy.parent)
					{
						visualElement.m_CachedEventInterestParentCategories = this.m_CachedEventInterestParentCategories;
						visualElement.isEventInterestParentCategoriesDirty = false;
					}
				}
			}
		}

		// Token: 0x06001F3A RID: 7994 RVA: 0x00075D96 File Offset: 0x00073F96
		internal bool HasParentEventInterests(EventCategory eventCategory)
		{
			return (this.eventInterestParentCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001F3B RID: 7995 RVA: 0x00075DA8 File Offset: 0x00073FA8
		internal bool HasParentEventInterests(int eventCategories)
		{
			return (this.eventInterestParentCategories & eventCategories) != 0;
		}

		// Token: 0x06001F3C RID: 7996 RVA: 0x00075DB5 File Offset: 0x00073FB5
		internal bool HasSelfEventInterests(EventCategory eventCategory)
		{
			return (this.m_EventInterestSelfCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001F3D RID: 7997 RVA: 0x00075DC7 File Offset: 0x00073FC7
		internal bool HasSelfEventInterests(int eventCategories)
		{
			return (this.m_EventInterestSelfCategories & eventCategories) != 0;
		}

		// Token: 0x06001F3E RID: 7998 RVA: 0x00075DD4 File Offset: 0x00073FD4
		internal bool HasTrickleDownEventInterests(int eventCategories)
		{
			return ((this.m_TrickleDownHandleEventCategories | this.m_TrickleDownEventCallbackCategories) & eventCategories) != 0;
		}

		// Token: 0x06001F3F RID: 7999 RVA: 0x00075DE8 File Offset: 0x00073FE8
		internal bool HasBubbleUpEventInterests(int eventCategories)
		{
			return ((this.m_BubbleUpHandleEventCategories | this.m_BubbleUpEventCallbackCategories) & eventCategories) != 0;
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x00075DFC File Offset: 0x00073FFC
		internal bool HasTrickleDownEventCallbacks(int eventCategories)
		{
			return (this.m_TrickleDownEventCallbackCategories & eventCategories) != 0;
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x00075E09 File Offset: 0x00074009
		internal bool HasBubbleUpEventCallbacks(int eventCategories)
		{
			return (this.m_BubbleUpEventCallbackCategories & eventCategories) != 0;
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x00075E16 File Offset: 0x00074016
		internal bool HasTrickleDownHandleEvent(EventCategory eventCategory)
		{
			return (this.m_TrickleDownHandleEventCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00075E28 File Offset: 0x00074028
		internal bool HasTrickleDownHandleEvent(int eventCategories)
		{
			return (this.m_TrickleDownHandleEventCategories & eventCategories) != 0;
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00075E35 File Offset: 0x00074035
		internal bool HasBubbleUpHandleEvent(EventCategory eventCategory)
		{
			return (this.m_BubbleUpHandleEventCategories & (1 << (int)eventCategory)) != 0;
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00075E47 File Offset: 0x00074047
		internal bool HasBubbleUpHandleEvent(int eventCategories)
		{
			return (this.m_BubbleUpHandleEventCategories & eventCategories) != 0;
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x06001F46 RID: 8006 RVA: 0x00075E54 File Offset: 0x00074054
		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x06001F47 RID: 8007 RVA: 0x00075E68 File Offset: 0x00074068
		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06001F48 RID: 8008 RVA: 0x00075E7B File Offset: 0x0007407B
		public VisualElement.Hierarchy hierarchy { get; }

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001F49 RID: 8009 RVA: 0x00075E83 File Offset: 0x00074083
		// (set) Token: 0x06001F4A RID: 8010 RVA: 0x00075E8B File Offset: 0x0007408B
		internal bool isRootVisualContainer { get; set; }

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001F4B RID: 8011 RVA: 0x00075E94 File Offset: 0x00074094
		// (set) Token: 0x06001F4C RID: 8012 RVA: 0x00075E9C File Offset: 0x0007409C
		[Obsolete("VisualElement.cacheAsBitmap is deprecated and has no effect")]
		public bool cacheAsBitmap { get; set; }

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001F4D RID: 8013 RVA: 0x00075EA5 File Offset: 0x000740A5
		// (set) Token: 0x06001F4E RID: 8014 RVA: 0x00075EBA File Offset: 0x000740BA
		internal bool disableClipping
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableClipping) == VisualElementFlags.DisableClipping;
			}
			set
			{
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableClipping) : (this.m_Flags & ~VisualElementFlags.DisableClipping));
			}
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x00075EE0 File Offset: 0x000740E0
		internal bool ShouldClip()
		{
			return this.computedStyle.overflow != OverflowInternal.Visible && !this.disableClipping;
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001F50 RID: 8016 RVA: 0x00075F0B File Offset: 0x0007410B
		// (set) Token: 0x06001F51 RID: 8017 RVA: 0x00075F20 File Offset: 0x00074120
		internal bool disableRendering
		{
			get
			{
				return (this.m_Flags & VisualElementFlags.DisableRendering) == VisualElementFlags.DisableRendering;
			}
			set
			{
				VisualElementFlags flags = this.m_Flags;
				this.m_Flags = (value ? (this.m_Flags | VisualElementFlags.DisableRendering) : (this.m_Flags & ~VisualElementFlags.DisableRendering));
				bool flag = flags != this.m_Flags;
				if (flag)
				{
					this.IncrementVersion(VersionChangeType.DisableRendering);
				}
			}
		}

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001F52 RID: 8018 RVA: 0x00075F78 File Offset: 0x00074178
		// (remove) Token: 0x06001F53 RID: 8019 RVA: 0x00075FB0 File Offset: 0x000741B0
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<VisualElement, int> elementAdded;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001F54 RID: 8020 RVA: 0x00075FE8 File Offset: 0x000741E8
		// (remove) Token: 0x06001F55 RID: 8021 RVA: 0x00076020 File Offset: 0x00074220
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		internal event Action<VisualElement> elementRemoved;

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001F56 RID: 8022 RVA: 0x00076058 File Offset: 0x00074258
		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001F57 RID: 8023 RVA: 0x00076070 File Offset: 0x00074270
		// (set) Token: 0x06001F58 RID: 8024 RVA: 0x00076078 File Offset: 0x00074278
		internal BaseVisualElementPanel elementPanel
		{
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			get;
			private set; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001F59 RID: 8025 RVA: 0x00076084 File Offset: 0x00074284
		[CreateProperty(ReadOnly = true)]
		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001F5A RID: 8026 RVA: 0x0007609C File Offset: 0x0007429C
		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001F5B RID: 8027 RVA: 0x000760AF File Offset: 0x000742AF
		// (set) Token: 0x06001F5C RID: 8028 RVA: 0x000760B7 File Offset: 0x000742B7
		[CreateProperty(ReadOnly = true)]
		public VisualTreeAsset visualTreeAssetSource
		{
			get
			{
				return this.m_VisualTreeAssetSource;
			}
			internal set
			{
				this.m_VisualTreeAssetSource = value;
			}
		}

		// Token: 0x06001F5D RID: 8029 RVA: 0x000760C0 File Offset: 0x000742C0
		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				VisualElement contentContainer = this.contentContainer;
				bool flag2 = contentContainer == null;
				if (flag2)
				{
					throw new InvalidOperationException("You can't add directly to this VisualElement. Use hierarchy.Add() if you know what you're doing.");
				}
				bool flag3 = contentContainer == this;
				if (flag3)
				{
					this.hierarchy.Add(child);
				}
				else if (contentContainer != null)
				{
					contentContainer.Add(child);
				}
				child.m_LogicalParent = this;
			}
		}

		// Token: 0x06001F5E RID: 8030 RVA: 0x00076128 File Offset: 0x00074328
		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		// Token: 0x06001F5F RID: 8031 RVA: 0x00076180 File Offset: 0x00074380
		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Remove(element);
				}
			}
		}

		// Token: 0x06001F60 RID: 8032 RVA: 0x000761C4 File Offset: 0x000743C4
		public void RemoveAt(int index)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.RemoveAt(index);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.RemoveAt(index);
				}
			}
		}

		// Token: 0x06001F61 RID: 8033 RVA: 0x00076208 File Offset: 0x00074408
		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Clear();
				}
			}
		}

		// Token: 0x06001F62 RID: 8034 RVA: 0x0007624C File Offset: 0x0007444C
		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x17000808 RID: 2056
		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement visualElement;
				if (flag)
				{
					visualElement = this.hierarchy[key];
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					visualElement = ((contentContainer != null) ? contentContainer[key] : null);
				}
				return visualElement;
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x000762B0 File Offset: 0x000744B0
		[CreateProperty(ReadOnly = true)]
		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int num;
				if (flag)
				{
					num = this.hierarchy.childCount;
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					num = ((contentContainer != null) ? contentContainer.childCount : 0);
				}
				return num;
			}
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000762F4 File Offset: 0x000744F4
		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int num;
			if (flag)
			{
				num = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				num = ((contentContainer != null) ? contentContainer.IndexOf(element) : (-1));
			}
			return num;
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x0007633C File Offset: 0x0007453C
		internal VisualElement ElementAtTreePath(List<int> childIndexes)
		{
			VisualElement visualElement = this;
			foreach (int num in childIndexes)
			{
				bool flag = num >= 0 && num < visualElement.hierarchy.childCount;
				if (!flag)
				{
					return null;
				}
				visualElement = visualElement.hierarchy[num];
			}
			return visualElement;
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x000763CC File Offset: 0x000745CC
		internal bool FindElementInTree(VisualElement element, List<int> outChildIndexes)
		{
			VisualElement visualElement = element;
			for (VisualElement visualElement2 = visualElement.hierarchy.parent; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				outChildIndexes.Insert(0, visualElement2.hierarchy.IndexOf(visualElement));
				bool flag = visualElement2 == this;
				if (flag)
				{
					return true;
				}
				visualElement = visualElement2;
			}
			outChildIndexes.Clear();
			return false;
		}

		// Token: 0x06001F68 RID: 8040 RVA: 0x00076440 File Offset: 0x00074640
		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> enumerable;
			if (flag)
			{
				enumerable = this.hierarchy.Children();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				enumerable = ((contentContainer != null) ? contentContainer.Children() : null) ?? VisualElement.s_EmptyList;
			}
			return enumerable;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x0007648C File Offset: 0x0007468C
		public void Sort(Comparison<VisualElement> comp)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Sort(comp);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Sort(comp);
				}
			}
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000764D0 File Offset: 0x000746D0
		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x00076514 File Offset: 0x00074714
		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x00076558 File Offset: 0x00074758
		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x000765DC File Offset: 0x000747DC
		public void PlaceInFront(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceInFront(this, sibling);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x00076660 File Offset: 0x00074860
		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000766A4 File Offset: 0x000748A4
		public T GetFirstOfType<T>() where T : class
		{
			T t = this as T;
			bool flag = t != null;
			T t2;
			if (flag)
			{
				t2 = t;
			}
			else
			{
				t2 = this.GetFirstAncestorOfType<T>();
			}
			return t2;
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000766DC File Offset: 0x000748DC
		public T GetFirstAncestorOfType<T>() where T : class
		{
			for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				T t = visualElement as T;
				bool flag = t != null;
				if (flag)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x00076748 File Offset: 0x00074948
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetFirstAncestorWhere(Predicate<VisualElement> predicate)
		{
			for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = predicate(visualElement);
				if (flag)
				{
					return visualElement;
				}
			}
			return null;
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x00076798 File Offset: 0x00074998
		public bool Contains(VisualElement child)
		{
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x000767E4 File Offset: 0x000749E4
		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int i = elements.Count;
				elements.AddRange(this.m_Children);
				while (i < elements.Count)
				{
					VisualElement visualElement = elements[i];
					elements.AddRange(visualElement.m_Children);
					i++;
				}
			}
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x00076844 File Offset: 0x00074A44
		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = null;
			}
			else
			{
				VisualElement visualElement2 = this;
				int i = 0;
				while (visualElement2 != null)
				{
					i++;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				VisualElement visualElement3 = other;
				int j = 0;
				while (visualElement3 != null)
				{
					j++;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement2 = this;
				visualElement3 = other;
				while (i > j)
				{
					i--;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				while (j > i)
				{
					j--;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				while (visualElement2 != visualElement3)
				{
					visualElement2 = visualElement2.hierarchy.parent;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x00076954 File Offset: 0x00074B54
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.panel.visualTree;
			}
			else
			{
				VisualElement visualElement2 = this;
				while (visualElement2.m_PhysicalParent != null)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x000769A0 File Offset: 0x00074BA0
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement GetRootVisualContainer(bool stopAtNearestRoot = false)
		{
			VisualElement visualElement = null;
			for (VisualElement visualElement2 = this; visualElement2 != null; visualElement2 = visualElement2.hierarchy.parent)
			{
				bool isRootVisualContainer = visualElement2.isRootVisualContainer;
				if (isRootVisualContainer)
				{
					visualElement = visualElement2;
					if (stopAtNearestRoot)
					{
						return visualElement;
					}
				}
			}
			return visualElement;
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x000769F0 File Offset: 0x00074BF0
		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_Children[0];
			}
			else
			{
				VisualElement visualElement2 = this.m_PhysicalParent;
				VisualElement visualElement3 = this;
				while (visualElement2 != null)
				{
					int i;
					for (i = 0; i < visualElement2.m_Children.Count; i++)
					{
						bool flag2 = visualElement2.m_Children[i] == visualElement3;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < visualElement2.m_Children.Count - 1;
					if (flag3)
					{
						return visualElement2.m_Children[i + 1];
					}
					visualElement3 = visualElement2;
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x00076AB0 File Offset: 0x00074CB0
		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement visualElement2;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement visualElement = this.m_PhysicalParent.m_Children[i - 1];
					while (visualElement.m_Children.Count > 0)
					{
						visualElement = visualElement.m_Children[visualElement.m_Children.Count - 1];
					}
					visualElement2 = visualElement;
				}
				else
				{
					visualElement2 = this.m_PhysicalParent;
				}
			}
			else
			{
				visualElement2 = null;
			}
			return visualElement2;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x00076B78 File Offset: 0x00074D78
		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this;
			}
			else
			{
				VisualElement visualElement2 = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (visualElement2.m_PhysicalParent != null && !visualElement2.isCompositeRoot)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				VisualElement visualElement3 = this;
				VisualElement visualElement4 = this.m_PhysicalParent;
				while (visualElement4 != null)
				{
					visualElement4 = visualElement4.m_PhysicalParent;
					bool flag2 = visualElement4 == visualElement2;
					if (flag2)
					{
						return visualElement3;
					}
					bool flag3 = visualElement4 != null && visualElement4.isCompositeRoot;
					if (flag3)
					{
						visualElement3 = visualElement4;
					}
				}
				visualElement = this;
			}
			return visualElement;
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001F7A RID: 8058 RVA: 0x00076C10 File Offset: 0x00074E10
		private Vector3 positionWithLayout
		{
			get
			{
				return this.ResolveTranslate() + this.layout.min;
			}
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x00076C40 File Offset: 0x00074E40
		internal void GetPivotedMatrixWithLayout(out Matrix4x4 result)
		{
			Vector3 vector = this.ResolveTransformOrigin();
			result = Matrix4x4.TRS(this.positionWithLayout + vector, this.ResolveRotation(), this.ResolveScale());
			VisualElement.TranslateMatrix34InPlace(ref result, -vector);
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x00076C88 File Offset: 0x00074E88
		internal bool hasDefaultRotationAndScale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.computedStyle.rotate.angle.value == 0f && this.computedStyle.scale.value == Vector3.one;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x00076CDC File Offset: 0x00074EDC
		internal bool has3DTransform
		{
			get
			{
				return this.has3DTranslation || this.has3DRotation;
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x00076CF0 File Offset: 0x00074EF0
		private bool has3DTranslation
		{
			get
			{
				return this.computedStyle.translate.z != 0f;
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001F7F RID: 8063 RVA: 0x00076D1C File Offset: 0x00074F1C
		private bool has3DRotation
		{
			get
			{
				Rotate rotate = this.computedStyle.rotate;
				return rotate.angle != 0f && rotate.axis != Vector3.forward;
			}
		}

		// Token: 0x06001F80 RID: 8064 RVA: 0x00076D68 File Offset: 0x00074F68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Min(float a, float b, float c, float d)
		{
			return Mathf.Min(Mathf.Min(a, b), Mathf.Min(c, d));
		}

		// Token: 0x06001F81 RID: 8065 RVA: 0x00076D90 File Offset: 0x00074F90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static float Max(float a, float b, float c, float d)
		{
			return Mathf.Max(Mathf.Max(a, b), Mathf.Max(c, d));
		}

		// Token: 0x06001F82 RID: 8066 RVA: 0x00076DB8 File Offset: 0x00074FB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void TransformAlignedBoundsToParentSpace(ref Bounds bounds)
		{
			bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
			if (hasDefaultRotationAndScale)
			{
				bounds.center += this.positionWithLayout;
			}
			else
			{
				Matrix4x4 matrix4x;
				this.GetPivotedMatrixWithLayout(out matrix4x);
				bounds = VisualElement.CalculateConservativeBounds(ref matrix4x, bounds);
			}
		}

		// Token: 0x06001F83 RID: 8067 RVA: 0x00076E0C File Offset: 0x0007500C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void TransformAlignedRectToParentSpace(ref Rect rect)
		{
			bool hasDefaultRotationAndScale = this.hasDefaultRotationAndScale;
			if (hasDefaultRotationAndScale)
			{
				rect.position += this.positionWithLayout;
			}
			else
			{
				Matrix4x4 matrix4x;
				this.GetPivotedMatrixWithLayout(out matrix4x);
				rect = VisualElement.CalculateConservativeRect(ref matrix4x, rect);
			}
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00076E64 File Offset: 0x00075064
		internal static Rect CalculateConservativeRect(ref Matrix4x4 matrix, Rect rect)
		{
			bool flag = float.IsNaN(rect.height) | float.IsNaN(rect.width) | float.IsNaN(rect.x) | float.IsNaN(rect.y);
			Rect rect2;
			if (flag)
			{
				rect = new Rect(VisualElement.MultiplyMatrix44Point2(ref matrix, rect.position), VisualElement.MultiplyVector2(ref matrix, rect.size));
				VisualElement.OrderMinMaxRect(ref rect);
				rect2 = rect;
			}
			else
			{
				Vector2 vector = new Vector2(rect.xMin, rect.yMin);
				Vector2 vector2 = new Vector2(rect.xMax, rect.yMax);
				Vector2 vector3 = new Vector2(rect.xMax, rect.yMin);
				Vector2 vector4 = new Vector2(rect.xMin, rect.yMax);
				Vector3 vector5 = matrix.MultiplyPoint3x4(vector);
				Vector3 vector6 = matrix.MultiplyPoint3x4(vector2);
				Vector3 vector7 = matrix.MultiplyPoint3x4(vector3);
				Vector3 vector8 = matrix.MultiplyPoint3x4(vector4);
				Vector2 vector9 = new Vector2(VisualElement.Min(vector5.x, vector6.x, vector7.x, vector8.x), VisualElement.Min(vector5.y, vector6.y, vector7.y, vector8.y));
				Vector2 vector10 = new Vector2(VisualElement.Max(vector5.x, vector6.x, vector7.x, vector8.x), VisualElement.Max(vector5.y, vector6.y, vector7.y, vector8.y));
				rect2 = new Rect(vector9.x, vector9.y, vector10.x - vector9.x, vector10.y - vector9.y);
			}
			return rect2;
		}

		// Token: 0x06001F85 RID: 8069 RVA: 0x00077034 File Offset: 0x00075234
		internal static Bounds CalculateConservativeBounds(ref Matrix4x4 matrix, Bounds bounds)
		{
			bool flag = VisualElement.<CalculateConservativeBounds>g__IsNaN|732_0(bounds.center) | VisualElement.<CalculateConservativeBounds>g__IsNaN|732_0(bounds.extents);
			Bounds bounds2;
			if (flag)
			{
				bounds = new Bounds(matrix.MultiplyPoint3x4(bounds.center), matrix.MultiplyVector(bounds.size));
				VisualElement.OrderMinMaxBounds(ref bounds);
				bounds2 = bounds;
			}
			else
			{
				Vector3 min = bounds.min;
				Vector3 max = bounds.max;
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				for (int i = 0; i < 8; i++)
				{
					Vector3 vector3 = new Vector3(((i & 1) != 0) ? max.x : min.x, ((i & 2) != 0) ? max.y : min.y, ((i & 4) != 0) ? max.z : min.z);
					vector3 = matrix.MultiplyPoint3x4(vector3);
					vector = ((i == 0) ? vector3 : Vector3.Min(vector, vector3));
					vector2 = ((i == 0) ? vector3 : Vector3.Max(vector2, vector3));
				}
				bounds.SetMinMax(vector, vector2);
				bounds2 = bounds;
			}
			return bounds2;
		}

		// Token: 0x06001F86 RID: 8070 RVA: 0x0007714B File Offset: 0x0007534B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void TransformAlignedRect(ref Matrix4x4 matrix, ref Rect rect)
		{
			rect = VisualElement.CalculateConservativeRect(ref matrix, rect);
		}

		// Token: 0x06001F87 RID: 8071 RVA: 0x00077160 File Offset: 0x00075360
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void TransformAlignedBounds(ref Matrix4x4 matrix, ref Bounds bounds)
		{
			bounds = VisualElement.CalculateConservativeBounds(ref matrix, bounds);
		}

		// Token: 0x06001F88 RID: 8072 RVA: 0x00077178 File Offset: 0x00075378
		internal static void OrderMinMaxRect(ref Rect rect)
		{
			bool flag = rect.width < 0f;
			if (flag)
			{
				rect.x += rect.width;
				rect.width = -rect.width;
			}
			bool flag2 = rect.height < 0f;
			if (flag2)
			{
				rect.y += rect.height;
				rect.height = -rect.height;
			}
		}

		// Token: 0x06001F89 RID: 8073 RVA: 0x000771F0 File Offset: 0x000753F0
		internal static void OrderMinMaxBounds(ref Bounds bounds)
		{
			Vector3 extents = bounds.extents;
			bounds.extents = new Vector3(Mathf.Abs(extents.x), Mathf.Abs(extents.y), Mathf.Abs(extents.z));
		}

		// Token: 0x06001F8A RID: 8074 RVA: 0x00077234 File Offset: 0x00075434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyMatrix44Point2(ref Matrix4x4 lhs, Vector2 point)
		{
			Vector2 vector;
			vector.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			vector.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return vector;
		}

		// Token: 0x06001F8B RID: 8075 RVA: 0x0007729C File Offset: 0x0007549C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Vector2 MultiplyVector2(ref Matrix4x4 lhs, Vector2 vector)
		{
			Vector2 vector2;
			vector2.x = lhs.m00 * vector.x + lhs.m01 * vector.y;
			vector2.y = lhs.m10 * vector.x + lhs.m11 * vector.y;
			return vector2;
		}

		// Token: 0x06001F8C RID: 8076 RVA: 0x000772F4 File Offset: 0x000754F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Rect MultiplyMatrix44Rect2(ref Matrix4x4 lhs, Rect r)
		{
			r.position = VisualElement.MultiplyMatrix44Point2(ref lhs, r.position);
			r.size = VisualElement.MultiplyVector2(ref lhs, r.size);
			return r;
		}

		// Token: 0x06001F8D RID: 8077 RVA: 0x00077334 File Offset: 0x00075534
		internal static void MultiplyMatrix34(ref Matrix4x4 lhs, ref Matrix4x4 rhs, out Matrix4x4 res)
		{
			res.m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20;
			res.m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21;
			res.m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22;
			res.m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03;
			res.m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20;
			res.m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21;
			res.m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22;
			res.m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13;
			res.m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20;
			res.m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21;
			res.m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22;
			res.m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23;
			res.m30 = 0f;
			res.m31 = 0f;
			res.m32 = 0f;
			res.m33 = 1f;
		}

		// Token: 0x06001F8E RID: 8078 RVA: 0x000775B7 File Offset: 0x000757B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34(ref Matrix4x4 lhs, Vector3 rhs, out Matrix4x4 res)
		{
			res = lhs;
			VisualElement.TranslateMatrix34InPlace(ref res, rhs);
		}

		// Token: 0x06001F8F RID: 8079 RVA: 0x000775D0 File Offset: 0x000757D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void TranslateMatrix34InPlace(ref Matrix4x4 lhs, Vector3 rhs)
		{
			lhs.m03 += lhs.m00 * rhs.x + lhs.m01 * rhs.y + lhs.m02 * rhs.z;
			lhs.m13 += lhs.m10 * rhs.x + lhs.m11 * rhs.y + lhs.m12 * rhs.z;
			lhs.m23 += lhs.m20 * rhs.x + lhs.m21 * rhs.y + lhs.m22 * rhs.z;
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x06001F90 RID: 8080 RVA: 0x00077678 File Offset: 0x00075878
		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001F91 RID: 8081 RVA: 0x0007768C File Offset: 0x0007588C
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem timerStateScheduledItem = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			timerStateScheduledItem.Resume();
			return timerStateScheduledItem;
		}

		// Token: 0x06001F92 RID: 8082 RVA: 0x000776BC File Offset: 0x000758BC
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem simpleScheduledItem = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			simpleScheduledItem.Resume();
			return simpleScheduledItem;
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x06001F93 RID: 8083 RVA: 0x000776EC File Offset: 0x000758EC
		[CreateProperty]
		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x00077720 File Offset: 0x00075920
		[CreateProperty]
		public IResolvedStyle resolvedStyle
		{
			get
			{
				bool flag = this.resolvedStyleAccess == null;
				if (flag)
				{
					this.resolvedStyleAccess = new ResolvedStyleAccess(this);
				}
				return this.resolvedStyleAccess;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001F95 RID: 8085 RVA: 0x00077754 File Offset: 0x00075954
		public ICustomStyle customStyle
		{
			get
			{
				VisualElement.s_CustomStyleAccess.SetContext(this.computedStyle.customProperties, this.computedStyle.dpiScaling);
				return VisualElement.s_CustomStyleAccess;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x0007778C File Offset: 0x0007598C
		[CreateProperty(ReadOnly = true)]
		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		// Token: 0x06001F97 RID: 8087 RVA: 0x00077794 File Offset: 0x00075994
		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint_noChecks) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(styleSheet);
			}
		}

		// Token: 0x06001F98 RID: 8088 RVA: 0x00077804 File Offset: 0x00075A04
		internal bool HasStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint_noChecks) as StyleSheet;
			bool flag = styleSheet == null;
			bool flag2;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				flag2 = false;
			}
			else
			{
				flag2 = this.styleSheets.Contains(styleSheet);
			}
			return flag2;
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x00077864 File Offset: 0x00075A64
		internal void RemoveStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint_noChecks) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
			}
			else
			{
				this.styleSheets.Remove(styleSheet);
			}
		}

		// Token: 0x06001F9A RID: 8090 RVA: 0x000778C0 File Offset: 0x00075AC0
		internal StyleFloat ResolveLengthValue(Length length, bool isRow)
		{
			bool flag = length.IsAuto();
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = new StyleFloat(StyleKeyword.Auto);
			}
			else
			{
				bool flag2 = length.IsNone();
				if (flag2)
				{
					styleFloat = new StyleFloat(StyleKeyword.None);
				}
				else
				{
					bool flag3 = length.unit != LengthUnit.Percent;
					if (flag3)
					{
						styleFloat = new StyleFloat(length.value);
					}
					else
					{
						VisualElement parent = this.hierarchy.parent;
						bool flag4 = parent == null;
						if (flag4)
						{
							styleFloat = 0f;
						}
						else
						{
							float num = (isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height);
							styleFloat = length.value * num / 100f;
						}
					}
				}
			}
			return styleFloat;
		}

		// Token: 0x06001F9B RID: 8091 RVA: 0x00077978 File Offset: 0x00075B78
		internal Vector3 ResolveTranslate()
		{
			Translate translate = this.computedStyle.translate;
			Length x = translate.x;
			bool flag = x.unit == LengthUnit.Percent;
			float num;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				num = (float.IsNaN(width) ? 0f : (width * x.value / 100f));
			}
			else
			{
				num = x.value;
				num = (float.IsNaN(num) ? 0f : num);
			}
			Length y = translate.y;
			bool flag2 = y.unit == LengthUnit.Percent;
			float num2;
			if (flag2)
			{
				float height = this.resolvedStyle.height;
				num2 = (float.IsNaN(height) ? 0f : (height * y.value / 100f));
			}
			else
			{
				num2 = y.value;
				num2 = (float.IsNaN(num2) ? 0f : num2);
			}
			float num3 = translate.z;
			num3 = (float.IsNaN(num3) ? 0f : num3);
			return new Vector3(num, num2, num3);
		}

		// Token: 0x06001F9C RID: 8092 RVA: 0x00077A8C File Offset: 0x00075C8C
		internal Vector3 ResolveTransformOrigin()
		{
			TransformOrigin transformOrigin = this.computedStyle.transformOrigin;
			Length x = transformOrigin.x;
			bool flag = x.IsNone();
			float num;
			if (flag)
			{
				float width = this.resolvedStyle.width;
				num = (float.IsNaN(width) ? 0f : (width / 2f));
			}
			else
			{
				bool flag2 = x.unit == LengthUnit.Percent;
				if (flag2)
				{
					float width2 = this.resolvedStyle.width;
					num = (float.IsNaN(width2) ? 0f : (width2 * x.value / 100f));
				}
				else
				{
					num = x.value;
				}
			}
			Length y = transformOrigin.y;
			bool flag3 = y.IsNone();
			float num2;
			if (flag3)
			{
				float height = this.resolvedStyle.height;
				num2 = (float.IsNaN(height) ? 0f : (height / 2f));
			}
			else
			{
				bool flag4 = y.unit == LengthUnit.Percent;
				if (flag4)
				{
					float height2 = this.resolvedStyle.height;
					num2 = (float.IsNaN(height2) ? 0f : (height2 * y.value / 100f));
				}
				else
				{
					num2 = y.value;
				}
			}
			float z = transformOrigin.z;
			return new Vector3(num, num2, z);
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x00077BE4 File Offset: 0x00075DE4
		private Quaternion ResolveRotation()
		{
			Rotate rotate = this.computedStyle.rotate;
			Vector3 axis = rotate.axis;
			bool flag = float.IsNaN(rotate.angle.value) || float.IsNaN(axis.x) || float.IsNaN(axis.y) || float.IsNaN(axis.z);
			if (flag)
			{
				rotate = Rotate.Initial();
			}
			return rotate.ToQuaternion();
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x00077C5C File Offset: 0x00075E5C
		private Vector3 ResolveScale()
		{
			Vector3 value = this.computedStyle.scale.value;
			BaseVisualElementPanel elementPanel = this.elementPanel;
			bool flag = elementPanel != null && elementPanel.isFlat;
			if (flag)
			{
				value.z = 1f;
			}
			return (float.IsNaN(value.x) || float.IsNaN(value.y) || float.IsNaN(value.z)) ? Vector3.one : value;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x00077CD8 File Offset: 0x00075ED8
		// (set) Token: 0x06001FA0 RID: 8096 RVA: 0x00077D08 File Offset: 0x00075F08
		[CreateProperty]
		public string tooltip
		{
			get
			{
				string text = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				return text ?? string.Empty;
			}
			set
			{
				bool flag = !this.HasProperty(VisualElement.tooltipPropertyKey);
				if (flag)
				{
					bool flag2 = string.IsNullOrEmpty(value);
					if (flag2)
					{
						return;
					}
					base.RegisterCallback<TooltipEvent>(new EventCallback<TooltipEvent>(this.SetTooltip), TrickleDown.NoTrickleDown);
				}
				string text = this.GetProperty(VisualElement.tooltipPropertyKey) as string;
				bool flag3 = string.CompareOrdinal(text, value) == 0;
				if (!flag3)
				{
					this.SetProperty(VisualElement.tooltipPropertyKey, value);
					base.NotifyPropertyChanged(in VisualElement.tooltipProperty);
				}
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x00077D88 File Offset: 0x00075F88
		internal static VisualElement.TypeData GetOrCreateTypeData(Type t)
		{
			VisualElement.TypeData typeData;
			bool flag = !VisualElement.s_TypeData.TryGetValue(t, out typeData);
			if (flag)
			{
				typeData = new VisualElement.TypeData(t);
				VisualElement.s_TypeData.Add(t, typeData);
			}
			return typeData;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06001FA2 RID: 8098 RVA: 0x00077DC8 File Offset: 0x00075FC8
		private VisualElement.TypeData typeData
		{
			get
			{
				bool flag = this.m_TypeData == null;
				if (flag)
				{
					Type type = base.GetType();
					bool flag2 = !VisualElement.s_TypeData.TryGetValue(type, out this.m_TypeData);
					if (flag2)
					{
						this.m_TypeData = new VisualElement.TypeData(type);
						VisualElement.s_TypeData.Add(type, this.m_TypeData);
					}
				}
				return this.m_TypeData;
			}
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x00078015 File Offset: 0x00076215
		[CompilerGenerated]
		internal static bool <CalculateConservativeBounds>g__IsNaN|732_0(Vector3 v)
		{
			return float.IsNaN(v.x) || float.IsNaN(v.y) || float.IsNaN(v.z);
		}

		// Token: 0x04000DCD RID: 3533
		private static uint s_NextId;

		// Token: 0x04000DCE RID: 3534
		private static List<string> s_EmptyClassList = new List<string>(0);

		// Token: 0x04000DCF RID: 3535
		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		// Token: 0x04000DD0 RID: 3536
		public static readonly string disabledUssClassName = "unity-disabled";

		// Token: 0x04000DD1 RID: 3537
		private string m_Name;

		// Token: 0x04000DD2 RID: 3538
		private List<string> m_ClassList;

		// Token: 0x04000DD3 RID: 3539
		private Dictionary<PropertyName, object> m_PropertyBag;

		// Token: 0x04000DD4 RID: 3540
		internal VisualElementFlags m_Flags;

		// Token: 0x04000DD5 RID: 3541
		private string m_ViewDataKey;

		// Token: 0x04000DD6 RID: 3542
		private RenderHints m_RenderHints;

		// Token: 0x04000DD7 RID: 3543
		internal Rect lastLayout;

		// Token: 0x04000DD8 RID: 3544
		internal Rect lastPseudoPadding;

		// Token: 0x04000DD9 RID: 3545
		internal RenderChainVEData renderChainData;

		// Token: 0x04000DDA RID: 3546
		internal bool shouldCutRenderChain;

		// Token: 0x04000DDB RID: 3547
		internal UIRenderer uiRenderer;

		// Token: 0x04000DDC RID: 3548
		private Rect m_Layout;

		// Token: 0x04000DDD RID: 3549
		private Rect m_BoundingBox;

		// Token: 0x04000DDE RID: 3550
		private const VisualElementFlags worldBoundingBoxDirtyDependencies = VisualElementFlags.WorldTransformDirty | VisualElementFlags.BoundingBoxDirty | VisualElementFlags.WorldBoundingBoxDirty;

		// Token: 0x04000DDF RID: 3551
		private Rect m_WorldBoundingBox;

		// Token: 0x04000DE0 RID: 3552
		private const VisualElementFlags worldTransformInverseDirtyDependencies = VisualElementFlags.WorldTransformDirty | VisualElementFlags.WorldTransformInverseDirty;

		// Token: 0x04000DE1 RID: 3553
		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		// Token: 0x04000DE2 RID: 3554
		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		// Token: 0x04000DE3 RID: 3555
		private Rect m_WorldClip = Rect.zero;

		// Token: 0x04000DE4 RID: 3556
		private Rect m_WorldClipMinusGroup = Rect.zero;

		// Token: 0x04000DE5 RID: 3557
		private bool m_WorldClipIsInfinite = false;

		// Token: 0x04000DE6 RID: 3558
		internal static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		// Token: 0x04000DE7 RID: 3559
		internal PseudoStates triggerPseudoMask;

		// Token: 0x04000DE8 RID: 3560
		internal PseudoStates dependencyPseudoMask;

		// Token: 0x04000DE9 RID: 3561
		private PseudoStates m_PseudoStates;

		// Token: 0x04000DEB RID: 3563
		private PickingMode m_PickingMode;

		// Token: 0x04000DEC RID: 3564
		private LayoutNode m_LayoutNode;

		// Token: 0x04000DED RID: 3565
		internal ComputedStyle m_Style = InitialStyle.Acquire();

		// Token: 0x04000DEE RID: 3566
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal StyleVariableContext variableContext = StyleVariableContext.none;

		// Token: 0x04000DEF RID: 3567
		internal int inheritedStylesHash = 0;

		// Token: 0x04000DF0 RID: 3568
		internal readonly uint controlid;

		// Token: 0x04000DF1 RID: 3569
		internal int imguiContainerDescendantCount = 0;

		// Token: 0x04000DF2 RID: 3570
		private bool m_EnabledSelf;

		// Token: 0x04000DF3 RID: 3571
		private LanguageDirection m_LanguageDirection;

		// Token: 0x04000DF4 RID: 3572
		private LanguageDirection m_LocalLanguageDirection;

		// Token: 0x04000DF6 RID: 3574
		private static readonly ProfilerMarker k_GenerateVisualContentMarker = new ProfilerMarker("GenerateVisualContent");

		// Token: 0x04000DF7 RID: 3575
		private VisualElement.RenderTargetMode m_SubRenderTargetMode = VisualElement.RenderTargetMode.None;

		// Token: 0x04000DF8 RID: 3576
		private static Material s_runtimeMaterial;

		// Token: 0x04000DF9 RID: 3577
		private Material m_defaultMaterial;

		// Token: 0x04000DFA RID: 3578
		private List<IValueAnimationUpdate> m_RunningAnimations;

		// Token: 0x04000DFB RID: 3579
		internal static readonly BindingId childCountProperty = "childCount";

		// Token: 0x04000DFC RID: 3580
		internal static readonly BindingId contentRectProperty = "contentRect";

		// Token: 0x04000DFD RID: 3581
		internal static readonly BindingId dataSourcePathProperty = "dataSourcePath";

		// Token: 0x04000DFE RID: 3582
		internal static readonly BindingId dataSourceProperty = "dataSource";

		// Token: 0x04000DFF RID: 3583
		internal static readonly BindingId disablePlayModeTintProperty = "disablePlayModeTint";

		// Token: 0x04000E00 RID: 3584
		internal static readonly BindingId enabledInHierarchyProperty = "enabledInHierarchy";

		// Token: 0x04000E01 RID: 3585
		internal static readonly BindingId enabledSelfProperty = "enabledSelf";

		// Token: 0x04000E02 RID: 3586
		internal static readonly BindingId layoutProperty = "layout";

		// Token: 0x04000E03 RID: 3587
		internal static readonly BindingId languageDirectionProperty = "languageDirection";

		// Token: 0x04000E04 RID: 3588
		internal static readonly BindingId localBoundProperty = "localBound";

		// Token: 0x04000E05 RID: 3589
		internal static readonly BindingId nameProperty = "name";

		// Token: 0x04000E06 RID: 3590
		internal static readonly BindingId panelProperty = "panel";

		// Token: 0x04000E07 RID: 3591
		internal static readonly BindingId pickingModeProperty = "pickingMode";

		// Token: 0x04000E08 RID: 3592
		internal static readonly BindingId styleSheetsProperty = "styleSheets";

		// Token: 0x04000E09 RID: 3593
		internal static readonly BindingId tooltipProperty = "tooltip";

		// Token: 0x04000E0A RID: 3594
		internal static readonly BindingId usageHintsProperty = "usageHints";

		// Token: 0x04000E0B RID: 3595
		internal static readonly BindingId userDataProperty = "userData";

		// Token: 0x04000E0C RID: 3596
		internal static readonly BindingId viewDataKeyProperty = "viewDataKey";

		// Token: 0x04000E0D RID: 3597
		internal static readonly BindingId visibleProperty = "visible";

		// Token: 0x04000E0E RID: 3598
		internal static readonly BindingId visualTreeAssetSourceProperty = "visualTreeAssetSource";

		// Token: 0x04000E0F RID: 3599
		internal static readonly BindingId worldBoundProperty = "worldBound";

		// Token: 0x04000E10 RID: 3600
		internal static readonly BindingId worldTransformProperty = "worldTransform";

		// Token: 0x04000E11 RID: 3601
		private object m_DataSource;

		// Token: 0x04000E12 RID: 3602
		private PropertyPath m_DataSourcePath;

		// Token: 0x04000E13 RID: 3603
		private List<Binding> m_Bindings;

		// Token: 0x04000E15 RID: 3605
		private readonly int m_TrickleDownHandleEventCategories;

		// Token: 0x04000E16 RID: 3606
		private readonly int m_BubbleUpHandleEventCategories;

		// Token: 0x04000E17 RID: 3607
		private int m_BubbleUpEventCallbackCategories = 0;

		// Token: 0x04000E18 RID: 3608
		private int m_TrickleDownEventCallbackCategories = 0;

		// Token: 0x04000E19 RID: 3609
		private int m_EventInterestSelfCategories = 0;

		// Token: 0x04000E1A RID: 3610
		private int m_CachedEventInterestParentCategories = 0;

		// Token: 0x04000E1B RID: 3611
		private static uint s_NextParentVersion;

		// Token: 0x04000E1C RID: 3612
		private uint m_NextParentCachedVersion;

		// Token: 0x04000E1D RID: 3613
		private uint m_NextParentRequiredVersion;

		// Token: 0x04000E1E RID: 3614
		private VisualElement m_CachedNextParentWithEventInterests;

		// Token: 0x04000E1F RID: 3615
		internal const string k_RootVisualContainerName = "rootVisualContainer";

		// Token: 0x04000E23 RID: 3619
		private VisualElement m_PhysicalParent;

		// Token: 0x04000E24 RID: 3620
		private VisualElement m_LogicalParent;

		// Token: 0x04000E27 RID: 3623
		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		// Token: 0x04000E28 RID: 3624
		private List<VisualElement> m_Children;

		// Token: 0x04000E2A RID: 3626
		private VisualTreeAsset m_VisualTreeAssetSource = null;

		// Token: 0x04000E2B RID: 3627
		internal static VisualElement.CustomStyleAccess s_CustomStyleAccess = new VisualElement.CustomStyleAccess();

		// Token: 0x04000E2C RID: 3628
		internal InlineStyleAccess inlineStyleAccess;

		// Token: 0x04000E2D RID: 3629
		internal ResolvedStyleAccess resolvedStyleAccess;

		// Token: 0x04000E2E RID: 3630
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal List<StyleSheet> styleSheetList;

		// Token: 0x04000E2F RID: 3631
		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", RegexOptions.Compiled);

		// Token: 0x04000E30 RID: 3632
		internal static readonly PropertyName tooltipPropertyKey = new PropertyName("--unity-tooltip");

		// Token: 0x04000E31 RID: 3633
		private static readonly Dictionary<Type, VisualElement.TypeData> s_TypeData = new Dictionary<Type, VisualElement.TypeData>();

		// Token: 0x04000E32 RID: 3634
		private VisualElement.TypeData m_TypeData;

		// Token: 0x020003D0 RID: 976
		internal static class ResolvedStyleProperties
		{
			// Token: 0x04000E33 RID: 3635
			internal static readonly BindingId alignContentProperty = "resolvedStyle.alignContent";

			// Token: 0x04000E34 RID: 3636
			internal static readonly BindingId alignItemsProperty = "resolvedStyle.alignItems";

			// Token: 0x04000E35 RID: 3637
			internal static readonly BindingId alignSelfProperty = "resolvedStyle.alignSelf";

			// Token: 0x04000E36 RID: 3638
			internal static readonly BindingId backgroundColorProperty = "resolvedStyle.backgroundColor";

			// Token: 0x04000E37 RID: 3639
			internal static readonly BindingId backgroundImageProperty = "resolvedStyle.backgroundImage";

			// Token: 0x04000E38 RID: 3640
			internal static readonly BindingId backgroundPositionXProperty = "resolvedStyle.backgroundPositionX";

			// Token: 0x04000E39 RID: 3641
			internal static readonly BindingId backgroundPositionYProperty = "resolvedStyle.backgroundPositionY";

			// Token: 0x04000E3A RID: 3642
			internal static readonly BindingId backgroundRepeatProperty = "resolvedStyle.backgroundRepeat";

			// Token: 0x04000E3B RID: 3643
			internal static readonly BindingId backgroundSizeProperty = "resolvedStyle.backgroundSize";

			// Token: 0x04000E3C RID: 3644
			internal static readonly BindingId borderBottomColorProperty = "resolvedStyle.borderBottomColor";

			// Token: 0x04000E3D RID: 3645
			internal static readonly BindingId borderBottomLeftRadiusProperty = "resolvedStyle.borderBottomLeftRadius";

			// Token: 0x04000E3E RID: 3646
			internal static readonly BindingId borderBottomRightRadiusProperty = "resolvedStyle.borderBottomRightRadius";

			// Token: 0x04000E3F RID: 3647
			internal static readonly BindingId borderBottomWidthProperty = "resolvedStyle.borderBottomWidth";

			// Token: 0x04000E40 RID: 3648
			internal static readonly BindingId borderLeftColorProperty = "resolvedStyle.borderLeftColor";

			// Token: 0x04000E41 RID: 3649
			internal static readonly BindingId borderLeftWidthProperty = "resolvedStyle.borderLeftWidth";

			// Token: 0x04000E42 RID: 3650
			internal static readonly BindingId borderRightColorProperty = "resolvedStyle.borderRightColor";

			// Token: 0x04000E43 RID: 3651
			internal static readonly BindingId borderRightWidthProperty = "resolvedStyle.borderRightWidth";

			// Token: 0x04000E44 RID: 3652
			internal static readonly BindingId borderTopColorProperty = "resolvedStyle.borderTopColor";

			// Token: 0x04000E45 RID: 3653
			internal static readonly BindingId borderTopLeftRadiusProperty = "resolvedStyle.borderTopLeftRadius";

			// Token: 0x04000E46 RID: 3654
			internal static readonly BindingId borderTopRightRadiusProperty = "resolvedStyle.borderTopRightRadius";

			// Token: 0x04000E47 RID: 3655
			internal static readonly BindingId borderTopWidthProperty = "resolvedStyle.borderTopWidth";

			// Token: 0x04000E48 RID: 3656
			internal static readonly BindingId bottomProperty = "resolvedStyle.bottom";

			// Token: 0x04000E49 RID: 3657
			internal static readonly BindingId colorProperty = "resolvedStyle.color";

			// Token: 0x04000E4A RID: 3658
			internal static readonly BindingId displayProperty = "resolvedStyle.display";

			// Token: 0x04000E4B RID: 3659
			internal static readonly BindingId flexBasisProperty = "resolvedStyle.flexBasis";

			// Token: 0x04000E4C RID: 3660
			internal static readonly BindingId flexDirectionProperty = "resolvedStyle.flexDirection";

			// Token: 0x04000E4D RID: 3661
			internal static readonly BindingId flexGrowProperty = "resolvedStyle.flexGrow";

			// Token: 0x04000E4E RID: 3662
			internal static readonly BindingId flexShrinkProperty = "resolvedStyle.flexShrink";

			// Token: 0x04000E4F RID: 3663
			internal static readonly BindingId flexWrapProperty = "resolvedStyle.flexWrap";

			// Token: 0x04000E50 RID: 3664
			internal static readonly BindingId fontSizeProperty = "resolvedStyle.fontSize";

			// Token: 0x04000E51 RID: 3665
			internal static readonly BindingId heightProperty = "resolvedStyle.height";

			// Token: 0x04000E52 RID: 3666
			internal static readonly BindingId justifyContentProperty = "resolvedStyle.justifyContent";

			// Token: 0x04000E53 RID: 3667
			internal static readonly BindingId leftProperty = "resolvedStyle.left";

			// Token: 0x04000E54 RID: 3668
			internal static readonly BindingId letterSpacingProperty = "resolvedStyle.letterSpacing";

			// Token: 0x04000E55 RID: 3669
			internal static readonly BindingId marginBottomProperty = "resolvedStyle.marginBottom";

			// Token: 0x04000E56 RID: 3670
			internal static readonly BindingId marginLeftProperty = "resolvedStyle.marginLeft";

			// Token: 0x04000E57 RID: 3671
			internal static readonly BindingId marginRightProperty = "resolvedStyle.marginRight";

			// Token: 0x04000E58 RID: 3672
			internal static readonly BindingId marginTopProperty = "resolvedStyle.marginTop";

			// Token: 0x04000E59 RID: 3673
			internal static readonly BindingId maxHeightProperty = "resolvedStyle.maxHeight";

			// Token: 0x04000E5A RID: 3674
			internal static readonly BindingId maxWidthProperty = "resolvedStyle.maxWidth";

			// Token: 0x04000E5B RID: 3675
			internal static readonly BindingId minHeightProperty = "resolvedStyle.minHeight";

			// Token: 0x04000E5C RID: 3676
			internal static readonly BindingId minWidthProperty = "resolvedStyle.minWidth";

			// Token: 0x04000E5D RID: 3677
			internal static readonly BindingId opacityProperty = "resolvedStyle.opacity";

			// Token: 0x04000E5E RID: 3678
			internal static readonly BindingId paddingBottomProperty = "resolvedStyle.paddingBottom";

			// Token: 0x04000E5F RID: 3679
			internal static readonly BindingId paddingLeftProperty = "resolvedStyle.paddingLeft";

			// Token: 0x04000E60 RID: 3680
			internal static readonly BindingId paddingRightProperty = "resolvedStyle.paddingRight";

			// Token: 0x04000E61 RID: 3681
			internal static readonly BindingId paddingTopProperty = "resolvedStyle.paddingTop";

			// Token: 0x04000E62 RID: 3682
			internal static readonly BindingId positionProperty = "resolvedStyle.position";

			// Token: 0x04000E63 RID: 3683
			internal static readonly BindingId rightProperty = "resolvedStyle.right";

			// Token: 0x04000E64 RID: 3684
			internal static readonly BindingId rotateProperty = "resolvedStyle.rotate";

			// Token: 0x04000E65 RID: 3685
			internal static readonly BindingId scaleProperty = "resolvedStyle.scale";

			// Token: 0x04000E66 RID: 3686
			internal static readonly BindingId textOverflowProperty = "resolvedStyle.textOverflow";

			// Token: 0x04000E67 RID: 3687
			internal static readonly BindingId topProperty = "resolvedStyle.top";

			// Token: 0x04000E68 RID: 3688
			internal static readonly BindingId transformOriginProperty = "resolvedStyle.transformOrigin";

			// Token: 0x04000E69 RID: 3689
			internal static readonly BindingId transitionDelayProperty = "resolvedStyle.transitionDelay";

			// Token: 0x04000E6A RID: 3690
			internal static readonly BindingId transitionDurationProperty = "resolvedStyle.transitionDuration";

			// Token: 0x04000E6B RID: 3691
			internal static readonly BindingId transitionPropertyProperty = "resolvedStyle.transitionProperty";

			// Token: 0x04000E6C RID: 3692
			internal static readonly BindingId transitionTimingFunctionProperty = "resolvedStyle.transitionTimingFunction";

			// Token: 0x04000E6D RID: 3693
			internal static readonly BindingId translateProperty = "resolvedStyle.translate";

			// Token: 0x04000E6E RID: 3694
			internal static readonly BindingId unityBackgroundImageTintColorProperty = "resolvedStyle.unityBackgroundImageTintColor";

			// Token: 0x04000E6F RID: 3695
			internal static readonly BindingId unityEditorTextRenderingModeProperty = "resolvedStyle.unityEditorTextRenderingMode";

			// Token: 0x04000E70 RID: 3696
			internal static readonly BindingId unityFontProperty = "resolvedStyle.unityFont";

			// Token: 0x04000E71 RID: 3697
			internal static readonly BindingId unityFontDefinitionProperty = "resolvedStyle.unityFontDefinition";

			// Token: 0x04000E72 RID: 3698
			internal static readonly BindingId unityFontStyleAndWeightProperty = "resolvedStyle.unityFontStyleAndWeight";

			// Token: 0x04000E73 RID: 3699
			internal static readonly BindingId unityParagraphSpacingProperty = "resolvedStyle.unityParagraphSpacing";

			// Token: 0x04000E74 RID: 3700
			internal static readonly BindingId unitySliceBottomProperty = "resolvedStyle.unitySliceBottom";

			// Token: 0x04000E75 RID: 3701
			internal static readonly BindingId unitySliceLeftProperty = "resolvedStyle.unitySliceLeft";

			// Token: 0x04000E76 RID: 3702
			internal static readonly BindingId unitySliceRightProperty = "resolvedStyle.unitySliceRight";

			// Token: 0x04000E77 RID: 3703
			internal static readonly BindingId unitySliceScaleProperty = "resolvedStyle.unitySliceScale";

			// Token: 0x04000E78 RID: 3704
			internal static readonly BindingId unitySliceTopProperty = "resolvedStyle.unitySliceTop";

			// Token: 0x04000E79 RID: 3705
			internal static readonly BindingId unitySliceTypeProperty = "resolvedStyle.unitySliceType";

			// Token: 0x04000E7A RID: 3706
			internal static readonly BindingId unityTextAlignProperty = "resolvedStyle.unityTextAlign";

			// Token: 0x04000E7B RID: 3707
			internal static readonly BindingId unityTextGeneratorProperty = "resolvedStyle.unityTextGenerator";

			// Token: 0x04000E7C RID: 3708
			internal static readonly BindingId unityTextOutlineColorProperty = "resolvedStyle.unityTextOutlineColor";

			// Token: 0x04000E7D RID: 3709
			internal static readonly BindingId unityTextOutlineWidthProperty = "resolvedStyle.unityTextOutlineWidth";

			// Token: 0x04000E7E RID: 3710
			internal static readonly BindingId unityTextOverflowPositionProperty = "resolvedStyle.unityTextOverflowPosition";

			// Token: 0x04000E7F RID: 3711
			internal static readonly BindingId visibilityProperty = "resolvedStyle.visibility";

			// Token: 0x04000E80 RID: 3712
			internal static readonly BindingId whiteSpaceProperty = "resolvedStyle.whiteSpace";

			// Token: 0x04000E81 RID: 3713
			internal static readonly BindingId widthProperty = "resolvedStyle.width";

			// Token: 0x04000E82 RID: 3714
			internal static readonly BindingId wordSpacingProperty = "resolvedStyle.wordSpacing";
		}

		// Token: 0x020003D1 RID: 977
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal static class StyleProperties
		{
			// Token: 0x04000E83 RID: 3715
			internal static readonly BindingId alignContentProperty = "style.alignContent";

			// Token: 0x04000E84 RID: 3716
			internal static readonly BindingId alignItemsProperty = "style.alignItems";

			// Token: 0x04000E85 RID: 3717
			internal static readonly BindingId alignSelfProperty = "style.alignSelf";

			// Token: 0x04000E86 RID: 3718
			internal static readonly BindingId backgroundColorProperty = "style.backgroundColor";

			// Token: 0x04000E87 RID: 3719
			internal static readonly BindingId backgroundImageProperty = "style.backgroundImage";

			// Token: 0x04000E88 RID: 3720
			internal static readonly BindingId backgroundPositionXProperty = "style.backgroundPositionX";

			// Token: 0x04000E89 RID: 3721
			internal static readonly BindingId backgroundPositionYProperty = "style.backgroundPositionY";

			// Token: 0x04000E8A RID: 3722
			internal static readonly BindingId backgroundRepeatProperty = "style.backgroundRepeat";

			// Token: 0x04000E8B RID: 3723
			internal static readonly BindingId backgroundSizeProperty = "style.backgroundSize";

			// Token: 0x04000E8C RID: 3724
			internal static readonly BindingId borderBottomColorProperty = "style.borderBottomColor";

			// Token: 0x04000E8D RID: 3725
			internal static readonly BindingId borderBottomLeftRadiusProperty = "style.borderBottomLeftRadius";

			// Token: 0x04000E8E RID: 3726
			internal static readonly BindingId borderBottomRightRadiusProperty = "style.borderBottomRightRadius";

			// Token: 0x04000E8F RID: 3727
			internal static readonly BindingId borderBottomWidthProperty = "style.borderBottomWidth";

			// Token: 0x04000E90 RID: 3728
			internal static readonly BindingId borderLeftColorProperty = "style.borderLeftColor";

			// Token: 0x04000E91 RID: 3729
			internal static readonly BindingId borderLeftWidthProperty = "style.borderLeftWidth";

			// Token: 0x04000E92 RID: 3730
			internal static readonly BindingId borderRightColorProperty = "style.borderRightColor";

			// Token: 0x04000E93 RID: 3731
			internal static readonly BindingId borderRightWidthProperty = "style.borderRightWidth";

			// Token: 0x04000E94 RID: 3732
			internal static readonly BindingId borderTopColorProperty = "style.borderTopColor";

			// Token: 0x04000E95 RID: 3733
			internal static readonly BindingId borderTopLeftRadiusProperty = "style.borderTopLeftRadius";

			// Token: 0x04000E96 RID: 3734
			internal static readonly BindingId borderTopRightRadiusProperty = "style.borderTopRightRadius";

			// Token: 0x04000E97 RID: 3735
			internal static readonly BindingId borderTopWidthProperty = "style.borderTopWidth";

			// Token: 0x04000E98 RID: 3736
			internal static readonly BindingId bottomProperty = "style.bottom";

			// Token: 0x04000E99 RID: 3737
			internal static readonly BindingId colorProperty = "style.color";

			// Token: 0x04000E9A RID: 3738
			internal static readonly BindingId cursorProperty = "style.cursor";

			// Token: 0x04000E9B RID: 3739
			internal static readonly BindingId displayProperty = "style.display";

			// Token: 0x04000E9C RID: 3740
			internal static readonly BindingId flexBasisProperty = "style.flexBasis";

			// Token: 0x04000E9D RID: 3741
			internal static readonly BindingId flexDirectionProperty = "style.flexDirection";

			// Token: 0x04000E9E RID: 3742
			internal static readonly BindingId flexGrowProperty = "style.flexGrow";

			// Token: 0x04000E9F RID: 3743
			internal static readonly BindingId flexShrinkProperty = "style.flexShrink";

			// Token: 0x04000EA0 RID: 3744
			internal static readonly BindingId flexWrapProperty = "style.flexWrap";

			// Token: 0x04000EA1 RID: 3745
			internal static readonly BindingId fontSizeProperty = "style.fontSize";

			// Token: 0x04000EA2 RID: 3746
			internal static readonly BindingId heightProperty = "style.height";

			// Token: 0x04000EA3 RID: 3747
			internal static readonly BindingId justifyContentProperty = "style.justifyContent";

			// Token: 0x04000EA4 RID: 3748
			internal static readonly BindingId leftProperty = "style.left";

			// Token: 0x04000EA5 RID: 3749
			internal static readonly BindingId letterSpacingProperty = "style.letterSpacing";

			// Token: 0x04000EA6 RID: 3750
			internal static readonly BindingId marginBottomProperty = "style.marginBottom";

			// Token: 0x04000EA7 RID: 3751
			internal static readonly BindingId marginLeftProperty = "style.marginLeft";

			// Token: 0x04000EA8 RID: 3752
			internal static readonly BindingId marginRightProperty = "style.marginRight";

			// Token: 0x04000EA9 RID: 3753
			internal static readonly BindingId marginTopProperty = "style.marginTop";

			// Token: 0x04000EAA RID: 3754
			internal static readonly BindingId maxHeightProperty = "style.maxHeight";

			// Token: 0x04000EAB RID: 3755
			internal static readonly BindingId maxWidthProperty = "style.maxWidth";

			// Token: 0x04000EAC RID: 3756
			internal static readonly BindingId minHeightProperty = "style.minHeight";

			// Token: 0x04000EAD RID: 3757
			internal static readonly BindingId minWidthProperty = "style.minWidth";

			// Token: 0x04000EAE RID: 3758
			internal static readonly BindingId opacityProperty = "style.opacity";

			// Token: 0x04000EAF RID: 3759
			internal static readonly BindingId overflowProperty = "style.overflow";

			// Token: 0x04000EB0 RID: 3760
			internal static readonly BindingId paddingBottomProperty = "style.paddingBottom";

			// Token: 0x04000EB1 RID: 3761
			internal static readonly BindingId paddingLeftProperty = "style.paddingLeft";

			// Token: 0x04000EB2 RID: 3762
			internal static readonly BindingId paddingRightProperty = "style.paddingRight";

			// Token: 0x04000EB3 RID: 3763
			internal static readonly BindingId paddingTopProperty = "style.paddingTop";

			// Token: 0x04000EB4 RID: 3764
			internal static readonly BindingId positionProperty = "style.position";

			// Token: 0x04000EB5 RID: 3765
			internal static readonly BindingId rightProperty = "style.right";

			// Token: 0x04000EB6 RID: 3766
			internal static readonly BindingId rotateProperty = "style.rotate";

			// Token: 0x04000EB7 RID: 3767
			internal static readonly BindingId scaleProperty = "style.scale";

			// Token: 0x04000EB8 RID: 3768
			internal static readonly BindingId textOverflowProperty = "style.textOverflow";

			// Token: 0x04000EB9 RID: 3769
			internal static readonly BindingId textShadowProperty = "style.textShadow";

			// Token: 0x04000EBA RID: 3770
			internal static readonly BindingId topProperty = "style.top";

			// Token: 0x04000EBB RID: 3771
			internal static readonly BindingId transformOriginProperty = "style.transformOrigin";

			// Token: 0x04000EBC RID: 3772
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly BindingId transitionDelayProperty = "style.transitionDelay";

			// Token: 0x04000EBD RID: 3773
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly BindingId transitionDurationProperty = "style.transitionDuration";

			// Token: 0x04000EBE RID: 3774
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly BindingId transitionPropertyProperty = "style.transitionProperty";

			// Token: 0x04000EBF RID: 3775
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal static readonly BindingId transitionTimingFunctionProperty = "style.transitionTimingFunction";

			// Token: 0x04000EC0 RID: 3776
			internal static readonly BindingId translateProperty = "style.translate";

			// Token: 0x04000EC1 RID: 3777
			internal static readonly BindingId unityBackgroundImageTintColorProperty = "style.unityBackgroundImageTintColor";

			// Token: 0x04000EC2 RID: 3778
			internal static readonly BindingId unityEditorTextRenderingModeProperty = "style.unityEditorTextRenderingMode";

			// Token: 0x04000EC3 RID: 3779
			internal static readonly BindingId unityFontProperty = "style.unityFont";

			// Token: 0x04000EC4 RID: 3780
			internal static readonly BindingId unityFontDefinitionProperty = "style.unityFontDefinition";

			// Token: 0x04000EC5 RID: 3781
			internal static readonly BindingId unityFontStyleAndWeightProperty = "style.unityFontStyleAndWeight";

			// Token: 0x04000EC6 RID: 3782
			internal static readonly BindingId unityOverflowClipBoxProperty = "style.unityOverflowClipBox";

			// Token: 0x04000EC7 RID: 3783
			internal static readonly BindingId unityParagraphSpacingProperty = "style.unityParagraphSpacing";

			// Token: 0x04000EC8 RID: 3784
			internal static readonly BindingId unitySliceBottomProperty = "style.unitySliceBottom";

			// Token: 0x04000EC9 RID: 3785
			internal static readonly BindingId unitySliceLeftProperty = "style.unitySliceLeft";

			// Token: 0x04000ECA RID: 3786
			internal static readonly BindingId unitySliceRightProperty = "style.unitySliceRight";

			// Token: 0x04000ECB RID: 3787
			internal static readonly BindingId unitySliceScaleProperty = "style.unitySliceScale";

			// Token: 0x04000ECC RID: 3788
			internal static readonly BindingId unitySliceTopProperty = "style.unitySliceTop";

			// Token: 0x04000ECD RID: 3789
			internal static readonly BindingId unitySliceTypeProperty = "style.unitySliceType";

			// Token: 0x04000ECE RID: 3790
			internal static readonly BindingId unityTextAlignProperty = "style.unityTextAlign";

			// Token: 0x04000ECF RID: 3791
			internal static readonly BindingId unityTextGeneratorProperty = "style.unityTextGenerator";

			// Token: 0x04000ED0 RID: 3792
			internal static readonly BindingId unityTextOutlineColorProperty = "style.unityTextOutlineColor";

			// Token: 0x04000ED1 RID: 3793
			internal static readonly BindingId unityTextOutlineWidthProperty = "style.unityTextOutlineWidth";

			// Token: 0x04000ED2 RID: 3794
			internal static readonly BindingId unityTextOverflowPositionProperty = "style.unityTextOverflowPosition";

			// Token: 0x04000ED3 RID: 3795
			internal static readonly BindingId visibilityProperty = "style.visibility";

			// Token: 0x04000ED4 RID: 3796
			internal static readonly BindingId whiteSpaceProperty = "style.whiteSpace";

			// Token: 0x04000ED5 RID: 3797
			internal static readonly BindingId widthProperty = "style.width";

			// Token: 0x04000ED6 RID: 3798
			internal static readonly BindingId wordSpacingProperty = "style.wordSpacing";
		}

		// Token: 0x020003D2 RID: 978
		[Serializable]
		public class UxmlSerializedData : UnityEngine.UIElements.UxmlSerializedData
		{
			// Token: 0x06001FA7 RID: 8103 RVA: 0x000789FC File Offset: 0x00076BFC
			[Conditional("UNITY_EDITOR")]
			public new static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(VisualElement.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("name", "name", null, Array.Empty<string>()),
					new UxmlAttributeNames("enabledSelf", "enabled", null, Array.Empty<string>()),
					new UxmlAttributeNames("viewDataKey", "view-data-key", null, Array.Empty<string>()),
					new UxmlAttributeNames("pickingMode", "picking-mode", null, new string[] { "pickingMode" }),
					new UxmlAttributeNames("tooltip", "tooltip", null, Array.Empty<string>()),
					new UxmlAttributeNames("usageHints", "usage-hints", null, Array.Empty<string>()),
					new UxmlAttributeNames("tabIndex", "tabindex", null, Array.Empty<string>()),
					new UxmlAttributeNames("focusable", "focusable", null, Array.Empty<string>()),
					new UxmlAttributeNames("languageDirection", "language-direction", null, Array.Empty<string>()),
					new UxmlAttributeNames("dataSourceUnityObject", "data-source", null, Array.Empty<string>()),
					new UxmlAttributeNames("dataSourcePathString", "data-source-path", null, Array.Empty<string>()),
					new UxmlAttributeNames("dataSourceTypeString", "data-source-type", typeof(object), Array.Empty<string>()),
					new UxmlAttributeNames("bindings", "Bindings", null, Array.Empty<string>())
				});
			}

			// Token: 0x06001FA8 RID: 8104 RVA: 0x00078BA4 File Offset: 0x00076DA4
			[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
			internal bool HasBindingInternal(string property)
			{
				bool flag = this.bindings == null;
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					foreach (Binding.UxmlSerializedData uxmlSerializedData in this.bindings)
					{
						bool flag3 = uxmlSerializedData.property == property;
						if (flag3)
						{
							return true;
						}
					}
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06001FA9 RID: 8105 RVA: 0x00078C24 File Offset: 0x00076E24
			[ExcludeFromDocs]
			public override object CreateInstance()
			{
				return new VisualElement();
			}

			// Token: 0x06001FAA RID: 8106 RVA: 0x00078C2C File Offset: 0x00076E2C
			[ExcludeFromDocs]
			public override void Deserialize(object obj)
			{
				VisualElement visualElement = (VisualElement)obj;
				bool flag = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.name_UxmlAttributeFlags);
				if (flag)
				{
					visualElement.name = this.name;
				}
				bool flag2 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.enabledSelf_UxmlAttributeFlags);
				if (flag2)
				{
					visualElement.enabledSelf = this.enabledSelf;
				}
				bool flag3 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.viewDataKey_UxmlAttributeFlags);
				if (flag3)
				{
					visualElement.viewDataKey = this.viewDataKey;
				}
				bool flag4 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.pickingMode_UxmlAttributeFlags);
				if (flag4)
				{
					visualElement.pickingMode = this.pickingMode;
				}
				bool flag5 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.tooltip_UxmlAttributeFlags);
				if (flag5)
				{
					visualElement.tooltip = this.tooltip;
				}
				bool flag6 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.usageHints_UxmlAttributeFlags);
				if (flag6)
				{
					visualElement.usageHints = this.usageHints;
				}
				bool flag7 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.tabIndex_UxmlAttributeFlags);
				if (flag7)
				{
					visualElement.tabIndex = this.tabIndex;
				}
				bool flag8 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.focusable_UxmlAttributeFlags);
				if (flag8)
				{
					visualElement.focusable = this.focusable;
				}
				bool flag9 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSourceUnityObject_UxmlAttributeFlags);
				if (flag9)
				{
					visualElement.dataSourceUnityObject = (this.dataSourceUnityObject ? this.dataSourceUnityObject : null);
				}
				bool flag10 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSourcePathString_UxmlAttributeFlags);
				if (flag10)
				{
					visualElement.dataSourcePathString = this.dataSourcePathString;
				}
				bool flag11 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.dataSourceTypeString_UxmlAttributeFlags);
				if (flag11)
				{
					visualElement.dataSourceTypeString = this.dataSourceTypeString;
				}
				bool flag12 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.languageDirection_UxmlAttributeFlags);
				if (flag12)
				{
					visualElement.languageDirection = this.languageDirection;
				}
				bool flag13 = UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this.bindings_UxmlAttributeFlags);
				if (flag13)
				{
					visualElement.bindings.Clear();
					bool flag14 = this.bindings != null;
					if (flag14)
					{
						foreach (Binding.UxmlSerializedData uxmlSerializedData in this.bindings)
						{
							Binding binding = (Binding)uxmlSerializedData.CreateInstance();
							uxmlSerializedData.Deserialize(binding);
							visualElement.SetBinding(binding.property, binding);
							visualElement.bindings.Add(binding);
						}
					}
				}
			}

			// Token: 0x04000ED7 RID: 3799
			[HideInInspector]
			[SerializeField]
			private string name;

			// Token: 0x04000ED8 RID: 3800
			[SerializeReference]
			[HideInInspector]
			[UxmlObjectReference("Bindings")]
			private List<Binding.UxmlSerializedData> bindings;

			// Token: 0x04000ED9 RID: 3801
			[SerializeField]
			private string tooltip;

			// Token: 0x04000EDA RID: 3802
			[HideInInspector]
			[Tooltip("The path to the value in the data source used by this binding. To see resolved bindings in the UI Builder, define a path that is compatible with the target source property.")]
			[UxmlAttribute("data-source-path")]
			[SerializeField]
			private string dataSourcePathString;

			// Token: 0x04000EDB RID: 3803
			[UxmlAttribute("data-source-type")]
			[HideInInspector]
			[UxmlTypeReference(typeof(object))]
			[SerializeField]
			[Tooltip("A data source is a collection of information. By default, a binding will inherit the existing data source from the hierarchy. You can instead define another object here as the data source, or define the type of property it may be if the source is not yet available.")]
			private string dataSourceTypeString;

			// Token: 0x04000EDC RID: 3804
			[SerializeField]
			[Tooltip("A data source is a collection of information. By default, a binding will inherit the existing data source from the hierarchy. You can instead define another object here as the data source, or define the type of property it may be if the source is not yet available.")]
			[DataSourceDrawer]
			[HideInInspector]
			[UxmlAttribute("data-source")]
			private Object dataSourceUnityObject;

			// Token: 0x04000EDD RID: 3805
			[SerializeField]
			private string viewDataKey;

			// Token: 0x04000EDE RID: 3806
			[SerializeField]
			[UxmlAttribute(obsoleteNames = new string[] { "pickingMode" })]
			private PickingMode pickingMode;

			// Token: 0x04000EDF RID: 3807
			[SerializeField]
			private UsageHints usageHints;

			// Token: 0x04000EE0 RID: 3808
			[SerializeField]
			private LanguageDirection languageDirection;

			// Token: 0x04000EE1 RID: 3809
			[UxmlAttribute("tabindex")]
			[SerializeField]
			private int tabIndex;

			// Token: 0x04000EE2 RID: 3810
			[SerializeField]
			private bool focusable;

			// Token: 0x04000EE3 RID: 3811
			[UxmlAttribute("enabled")]
			[Tooltip("Sets the element to disabled which will not accept input. Utilizes the :disabled pseudo state.")]
			[SerializeField]
			private bool enabledSelf;

			// Token: 0x04000EE4 RID: 3812
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags name_UxmlAttributeFlags;

			// Token: 0x04000EE5 RID: 3813
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags enabledSelf_UxmlAttributeFlags;

			// Token: 0x04000EE6 RID: 3814
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags viewDataKey_UxmlAttributeFlags;

			// Token: 0x04000EE7 RID: 3815
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags pickingMode_UxmlAttributeFlags;

			// Token: 0x04000EE8 RID: 3816
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags tooltip_UxmlAttributeFlags;

			// Token: 0x04000EE9 RID: 3817
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags usageHints_UxmlAttributeFlags;

			// Token: 0x04000EEA RID: 3818
			[UxmlIgnore]
			[SerializeField]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags tabIndex_UxmlAttributeFlags;

			// Token: 0x04000EEB RID: 3819
			[HideInInspector]
			[SerializeField]
			[UxmlIgnore]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags focusable_UxmlAttributeFlags;

			// Token: 0x04000EEC RID: 3820
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags languageDirection_UxmlAttributeFlags;

			// Token: 0x04000EED RID: 3821
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSourceUnityObject_UxmlAttributeFlags;

			// Token: 0x04000EEE RID: 3822
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSourcePathString_UxmlAttributeFlags;

			// Token: 0x04000EEF RID: 3823
			[UxmlIgnore]
			[HideInInspector]
			[SerializeField]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags dataSourceTypeString_UxmlAttributeFlags;

			// Token: 0x04000EF0 RID: 3824
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags bindings_UxmlAttributeFlags;
		}

		// Token: 0x020003D3 RID: 979
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
		}

		// Token: 0x020003D4 RID: 980
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			// Token: 0x17000816 RID: 2070
			// (get) Token: 0x06001FAD RID: 8109 RVA: 0x00078E71 File Offset: 0x00077071
			// (set) Token: 0x06001FAE RID: 8110 RVA: 0x00078E79 File Offset: 0x00077079
			protected UxmlIntAttributeDescription focusIndex { get; set; } = new UxmlIntAttributeDescription
			{
				name = null,
				obsoleteNames = new string[] { "focus-index", "focusIndex" },
				defaultValue = -1
			};

			// Token: 0x17000817 RID: 2071
			// (get) Token: 0x06001FAF RID: 8111 RVA: 0x00078E82 File Offset: 0x00077082
			// (set) Token: 0x06001FB0 RID: 8112 RVA: 0x00078E8A File Offset: 0x0007708A
			protected UxmlBoolAttributeDescription focusable { get; set; } = new UxmlBoolAttributeDescription
			{
				name = "focusable",
				defaultValue = false
			};

			// Token: 0x17000818 RID: 2072
			// (get) Token: 0x06001FB1 RID: 8113 RVA: 0x00078E94 File Offset: 0x00077094
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			// Token: 0x06001FB2 RID: 8114 RVA: 0x00078EB4 File Offset: 0x000770B4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.enabledSelf = this.m_EnabledSelf.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
				int num = 0;
				bool flag2 = this.focusIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag2)
				{
					ve.tabIndex = ((num >= 0) ? num : 0);
					ve.focusable = num >= 0;
				}
				ve.tabIndex = this.m_TabIndex.GetValueFromBag(bag, cc);
				ve.focusable = this.focusable.GetValueFromBag(bag, cc);
				ve.dataSource = this.m_DataSource.GetValueFromBag(bag, cc);
				ve.dataSourcePath = new PropertyPath(this.m_DataSourcePath.GetValueFromBag(bag, cc));
			}

			// Token: 0x04000EF1 RID: 3825
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x04000EF2 RID: 3826
			private UxmlBoolAttributeDescription m_EnabledSelf = new UxmlBoolAttributeDescription
			{
				name = "enabled",
				defaultValue = true
			};

			// Token: 0x04000EF3 RID: 3827
			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			// Token: 0x04000EF4 RID: 3828
			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[] { "pickingMode" }
			};

			// Token: 0x04000EF5 RID: 3829
			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			// Token: 0x04000EF6 RID: 3830
			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			// Token: 0x04000EF8 RID: 3832
			private UxmlIntAttributeDescription m_TabIndex = new UxmlIntAttributeDescription
			{
				name = "tabindex",
				defaultValue = 0
			};

			// Token: 0x04000EFA RID: 3834
			private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
			{
				name = "class"
			};

			// Token: 0x04000EFB RID: 3835
			private UxmlStringAttributeDescription m_ContentContainer = new UxmlStringAttributeDescription
			{
				name = "content-container",
				obsoleteNames = new string[] { "contentContainer" }
			};

			// Token: 0x04000EFC RID: 3836
			private UxmlStringAttributeDescription m_Style = new UxmlStringAttributeDescription
			{
				name = "style"
			};

			// Token: 0x04000EFD RID: 3837
			private UxmlAssetAttributeDescription<Object> m_DataSource = new UxmlAssetAttributeDescription<Object>
			{
				name = "data-source"
			};

			// Token: 0x04000EFE RID: 3838
			private UxmlStringAttributeDescription m_DataSourcePath = new UxmlStringAttributeDescription
			{
				name = "data-source-path"
			};
		}

		// Token: 0x020003D6 RID: 982
		public enum MeasureMode
		{
			// Token: 0x04000F04 RID: 3844
			Undefined,
			// Token: 0x04000F05 RID: 3845
			Exactly,
			// Token: 0x04000F06 RID: 3846
			AtMost
		}

		// Token: 0x020003D7 RID: 983
		internal enum RenderTargetMode
		{
			// Token: 0x04000F08 RID: 3848
			None,
			// Token: 0x04000F09 RID: 3849
			NoColorConversion,
			// Token: 0x04000F0A RID: 3850
			LinearToGamma,
			// Token: 0x04000F0B RID: 3851
			GammaToLinear
		}

		// Token: 0x020003D8 RID: 984
		public struct Hierarchy
		{
			// Token: 0x1700081B RID: 2075
			// (get) Token: 0x06001FBC RID: 8124 RVA: 0x00079268 File Offset: 0x00077468
			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			// Token: 0x1700081C RID: 2076
			// (get) Token: 0x06001FBD RID: 8125 RVA: 0x00079285 File Offset: 0x00077485
			internal List<VisualElement> children
			{
				get
				{
					return this.m_Owner.m_Children;
				}
			}

			// Token: 0x06001FBE RID: 8126 RVA: 0x00079292 File Offset: 0x00077492
			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			// Token: 0x06001FBF RID: 8127 RVA: 0x0007929C File Offset: 0x0007749C
			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			// Token: 0x06001FC0 RID: 8128 RVA: 0x000792CC File Offset: 0x000774CC
			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				bool flag4 = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag4)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.RemoveFromHierarchy();
				bool flag5 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag5)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.layoutNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int num = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag6 = num > 0;
				if (flag6)
				{
					this.m_Owner.ChangeIMGUIContainerCount(num);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				bool flag7 = child.languageDirection == LanguageDirection.Inherit;
				if (flag7)
				{
					child.localLanguageDirection = this.m_Owner.localLanguageDirection;
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.AddedToParent, null);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				Action<VisualElement, int> elementAdded = this.m_Owner.elementAdded;
				if (elementAdded != null)
				{
					elementAdded(child, index);
				}
			}

			// Token: 0x06001FC1 RID: 8129 RVA: 0x00079468 File Offset: 0x00077668
			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This VisualElement is not my child");
				}
				int num = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(num);
			}

			// Token: 0x06001FC2 RID: 8130 RVA: 0x000794CC File Offset: 0x000776CC
			public void RemoveAt(int index)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = index < 0 || index >= this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index.ToString());
				}
				VisualElement visualElement = this.m_Owner.m_Children[index];
				visualElement.InvokeHierarchyChanged(HierarchyChangeType.RemovedFromParent, null);
				this.RemoveChildAtIndex(index);
				int num = visualElement.imguiContainerDescendantCount + (visualElement.isIMGUIContainer ? 1 : 0);
				bool flag3 = num > 0;
				if (flag3)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-num);
				}
				visualElement.hierarchy.SetParent(null);
				bool flag4 = this.childCount == 0;
				if (flag4)
				{
					this.ReleaseChildList();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
				}
				BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
				if (elementPanel != null)
				{
					elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				Action<VisualElement> elementRemoved = this.m_Owner.elementRemoved;
				if (elementRemoved != null)
				{
					elementRemoved(visualElement);
				}
			}

			// Token: 0x06001FC3 RID: 8131 RVA: 0x0007960C File Offset: 0x0007780C
			public void Clear()
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 0;
				if (flag2)
				{
					List<VisualElement> list = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.layoutNode.Clear();
					bool requireMeasureFunction = this.m_Owner.requireMeasureFunction;
					if (requireMeasureFunction)
					{
						this.m_Owner.AssignMeasureFunction();
					}
					foreach (VisualElement visualElement in list)
					{
						visualElement.InvokeHierarchyChanged(HierarchyChangeType.RemovedFromParent, null);
						visualElement.hierarchy.SetParent(null);
						visualElement.m_LogicalParent = null;
						BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
						if (elementPanel != null)
						{
							elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
						}
						Action<VisualElement> elementRemoved = this.m_Owner.elementRemoved;
						if (elementRemoved != null)
						{
							elementRemoved(visualElement);
						}
					}
					bool flag3 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag3)
					{
						int num = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							num--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-num);
					}
					VisualElementListPool.Release(list);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x06001FC4 RID: 8132 RVA: 0x00079798 File Offset: 0x00077998
			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num >= 0 && num < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, num, this.childCount);
					}
				}
			}

			// Token: 0x06001FC5 RID: 8133 RVA: 0x000797F0 File Offset: 0x000779F0
			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.MoveChildElement(child, num, 0);
					}
				}
			}

			// Token: 0x06001FC6 RID: 8134 RVA: 0x00079834 File Offset: 0x00077A34
			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = num2 > 0 && num < num2;
						if (flag3)
						{
							num2--;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x06001FC7 RID: 8135 RVA: 0x000798A0 File Offset: 0x00077AA0
			internal void PlaceInFront(VisualElement child, VisualElement under)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(under);
						bool flag3 = num > num2;
						if (flag3)
						{
							num2++;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x06001FC8 RID: 8136 RVA: 0x00079908 File Offset: 0x00077B08
			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				child.InvokeHierarchyChanged(HierarchyChangeType.RemovedFromParent, null);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.AddedToParent, null);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x1700081D RID: 2077
			// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x00079978 File Offset: 0x00077B78
			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			// Token: 0x1700081E RID: 2078
			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			// Token: 0x06001FCB RID: 8139 RVA: 0x000799C0 File Offset: 0x00077BC0
			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			// Token: 0x06001FCC RID: 8140 RVA: 0x000799E4 File Offset: 0x00077BE4
			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			// Token: 0x06001FCD RID: 8141 RVA: 0x00079A00 File Offset: 0x00077C00
			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			// Token: 0x06001FCE RID: 8142 RVA: 0x00079A20 File Offset: 0x00077C20
			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				this.m_Owner.DirtyNextParentWithEventInterests();
				this.m_Owner.SetPanel((value != null) ? value.elementPanel : null);
				bool flag = this.m_Owner.m_PhysicalParent != value;
				if (flag)
				{
					Debug.LogError("Modifying the parent of a VisualElement while it’s already being modified is not allowed and can cause undefined behavior. Did you change the hierarchy during an AttachToPanelEvent or DetachFromPanelEvent?");
				}
			}

			// Token: 0x06001FCF RID: 8143 RVA: 0x00079A8C File Offset: 0x00077C8C
			public unsafe void Sort(Comparison<VisualElement> comp)
			{
				bool flag = this.m_Owner.elementPanel != null && this.m_Owner.elementPanel.duringLayoutPhase;
				if (flag)
				{
					throw new InvalidOperationException("Cannot modify VisualElement hierarchy during layout calculation");
				}
				bool flag2 = this.childCount > 1;
				if (flag2)
				{
					this.m_Owner.m_Children.Sort(comp);
					this.m_Owner.layoutNode.Clear();
					for (int i = 0; i < this.m_Owner.m_Children.Count; i++)
					{
						this.m_Owner.layoutNode.Insert(i, *this.m_Owner.m_Children[i].layoutNode);
					}
					this.m_Owner.InvokeHierarchyChanged(HierarchyChangeType.ChildrenReordered, null);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x06001FD0 RID: 8144 RVA: 0x00079B68 File Offset: 0x00077D68
			private unsafe void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.layoutNode.Insert(this.m_Owner.layoutNode.Count, *child.layoutNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.layoutNode.Insert(index, *child.layoutNode);
				}
			}

			// Token: 0x06001FD1 RID: 8145 RVA: 0x00079BFA File Offset: 0x00077DFA
			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.layoutNode.RemoveAt(index);
			}

			// Token: 0x06001FD2 RID: 8146 RVA: 0x00079C24 File Offset: 0x00077E24
			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			// Token: 0x06001FD3 RID: 8147 RVA: 0x00079C70 File Offset: 0x00077E70
			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			// Token: 0x06001FD4 RID: 8148 RVA: 0x00079C90 File Offset: 0x00077E90
			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			// Token: 0x06001FD5 RID: 8149 RVA: 0x00079CC8 File Offset: 0x00077EC8
			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			// Token: 0x06001FD6 RID: 8150 RVA: 0x00079CF0 File Offset: 0x00077EF0
			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			// Token: 0x06001FD7 RID: 8151 RVA: 0x00079D10 File Offset: 0x00077F10
			public static bool operator !=(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return !(x == y);
			}

			// Token: 0x04000F0C RID: 3852
			private const string k_InvalidHierarchyChangeMsg = "Cannot modify VisualElement hierarchy during layout calculation";

			// Token: 0x04000F0D RID: 3853
			private readonly VisualElement m_Owner;
		}

		// Token: 0x020003D9 RID: 985
		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem
		{
			// Token: 0x1700081F RID: 2079
			// (get) Token: 0x06001FD8 RID: 8152 RVA: 0x00079D2C File Offset: 0x00077F2C
			// (set) Token: 0x06001FD9 RID: 8153 RVA: 0x00079D34 File Offset: 0x00077F34
			public VisualElement element { get; private set; }

			// Token: 0x17000820 RID: 2080
			// (get) Token: 0x06001FDA RID: 8154 RVA: 0x00079D3D File Offset: 0x00077F3D
			public bool isScheduled
			{
				get
				{
					return this.scheduler != null;
				}
			}

			// Token: 0x17000821 RID: 2081
			// (get) Token: 0x06001FDB RID: 8155 RVA: 0x00079D48 File Offset: 0x00077F48
			// (set) Token: 0x06001FDC RID: 8156 RVA: 0x00079D50 File Offset: 0x00077F50
			public bool isActive { get; private set; }

			// Token: 0x17000822 RID: 2082
			// (get) Token: 0x06001FDD RID: 8157 RVA: 0x00079D59 File Offset: 0x00077F59
			// (set) Token: 0x06001FDE RID: 8158 RVA: 0x00079D61 File Offset: 0x00077F61
			public bool isDetaching { get; private set; }

			// Token: 0x06001FDF RID: 8159 RVA: 0x00079D6A File Offset: 0x00077F6A
			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_OnAttachToPanelCallback = new EventCallback<AttachToPanelEvent>(this.OnElementAttachToPanelCallback);
				this.m_OnDetachFromPanelCallback = new EventCallback<DetachFromPanelEvent>(this.OnElementDetachFromPanelCallback);
			}

			// Token: 0x06001FE0 RID: 8160 RVA: 0x00079DA8 File Offset: 0x00077FA8
			private void SetActive(bool action)
			{
				bool flag = this.isActive != action;
				if (flag)
				{
					this.isActive = action;
					bool isActive = this.isActive;
					if (isActive)
					{
						this.element.RegisterCallback<AttachToPanelEvent>(this.m_OnAttachToPanelCallback, TrickleDown.NoTrickleDown);
						this.element.RegisterCallback<DetachFromPanelEvent>(this.m_OnDetachFromPanelCallback, TrickleDown.NoTrickleDown);
						this.SendActivation();
					}
					else
					{
						this.element.UnregisterCallback<AttachToPanelEvent>(this.m_OnAttachToPanelCallback, TrickleDown.NoTrickleDown);
						this.element.UnregisterCallback<DetachFromPanelEvent>(this.m_OnDetachFromPanelCallback, TrickleDown.NoTrickleDown);
						this.SendDeactivation();
					}
				}
			}

			// Token: 0x06001FE1 RID: 8161 RVA: 0x00079E3C File Offset: 0x0007803C
			private void SendActivation()
			{
				bool flag = this.CanBeActivated();
				if (flag)
				{
					this.OnPanelActivate();
				}
			}

			// Token: 0x06001FE2 RID: 8162 RVA: 0x00079E60 File Offset: 0x00078060
			private void SendDeactivation()
			{
				bool flag = this.CanBeActivated();
				if (flag)
				{
					this.OnPanelDeactivate();
				}
			}

			// Token: 0x06001FE3 RID: 8163 RVA: 0x00079E84 File Offset: 0x00078084
			private void OnElementAttachToPanelCallback(AttachToPanelEvent evt)
			{
				bool isActive = this.isActive;
				if (isActive)
				{
					bool flag = this.isScheduled && this.scheduler != this.element.elementPanel.scheduler;
					if (flag)
					{
						this.OnPanelDeactivate();
					}
					this.SendActivation();
				}
			}

			// Token: 0x06001FE4 RID: 8164 RVA: 0x00079ED8 File Offset: 0x000780D8
			private void OnElementDetachFromPanelCallback(DetachFromPanelEvent evt)
			{
				bool flag = !this.isActive;
				if (!flag)
				{
					this.isDetaching = true;
					try
					{
						this.SendDeactivation();
					}
					finally
					{
						this.isDetaching = false;
					}
				}
			}

			// Token: 0x06001FE5 RID: 8165 RVA: 0x00079F24 File Offset: 0x00078124
			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			// Token: 0x06001FE6 RID: 8166 RVA: 0x00079F40 File Offset: 0x00078140
			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			// Token: 0x06001FE7 RID: 8167 RVA: 0x00079F6C File Offset: 0x0007816C
			public IVisualElementScheduledItem ForDuration(long durationMs)
			{
				base.SetDuration(durationMs);
				return this;
			}

			// Token: 0x06001FE8 RID: 8168 RVA: 0x00079F88 File Offset: 0x00078188
			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			// Token: 0x06001FE9 RID: 8169 RVA: 0x00079FC4 File Offset: 0x000781C4
			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.scheduler = null;
				bool flag = !this.isDetaching;
				if (flag)
				{
					this.SetActive(false);
				}
			}

			// Token: 0x06001FEA RID: 8170 RVA: 0x00079FF7 File Offset: 0x000781F7
			public void Resume()
			{
				this.SetActive(true);
			}

			// Token: 0x06001FEB RID: 8171 RVA: 0x0007A002 File Offset: 0x00078202
			public void Pause()
			{
				this.SetActive(false);
			}

			// Token: 0x06001FEC RID: 8172 RVA: 0x0007A010 File Offset: 0x00078210
			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			// Token: 0x06001FED RID: 8173 RVA: 0x0007A044 File Offset: 0x00078244
			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					base.ResetStartTime();
					this.scheduler = this.element.elementPanel.scheduler;
					this.scheduler.Schedule(this);
				}
			}

			// Token: 0x06001FEE RID: 8174 RVA: 0x0007A08C File Offset: 0x0007828C
			public void OnPanelDeactivate()
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					IScheduler scheduler = this.scheduler;
					this.scheduler = null;
					scheduler.Unschedule(this);
				}
			}

			// Token: 0x06001FEF RID: 8175 RVA: 0x0007A0BC File Offset: 0x000782BC
			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}

			// Token: 0x04000F0F RID: 3855
			public IScheduler scheduler = null;

			// Token: 0x04000F12 RID: 3858
			private readonly EventCallback<AttachToPanelEvent> m_OnAttachToPanelCallback;

			// Token: 0x04000F13 RID: 3859
			private readonly EventCallback<DetachFromPanelEvent> m_OnDetachFromPanelCallback;
		}

		// Token: 0x020003DA RID: 986
		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			// Token: 0x06001FF0 RID: 8176 RVA: 0x0007A0F9 File Offset: 0x000782F9
			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent)
				: base(handler)
			{
				this.updateEvent = upEvent;
			}

			// Token: 0x06001FF1 RID: 8177 RVA: 0x0007A10C File Offset: 0x0007830C
			public static bool Matches(ScheduledItem item, ActionType updateEvent)
			{
				VisualElement.VisualElementScheduledItem<ActionType> visualElementScheduledItem = item as VisualElement.VisualElementScheduledItem<ActionType>;
				bool flag = visualElementScheduledItem != null;
				return flag && EqualityComparer<ActionType>.Default.Equals(visualElementScheduledItem.updateEvent, updateEvent);
			}

			// Token: 0x04000F14 RID: 3860
			public ActionType updateEvent;
		}

		// Token: 0x020003DB RID: 987
		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			// Token: 0x06001FF2 RID: 8178 RVA: 0x0007A143 File Offset: 0x00078343
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x06001FF3 RID: 8179 RVA: 0x0007A150 File Offset: 0x00078350
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = base.isScheduled;
				if (isScheduled)
				{
					this.updateEvent(state);
				}
			}
		}

		// Token: 0x020003DC RID: 988
		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			// Token: 0x06001FF4 RID: 8180 RVA: 0x0007A177 File Offset: 0x00078377
			public SimpleScheduledItem(VisualElement handler, Action updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x06001FF5 RID: 8181 RVA: 0x0007A184 File Offset: 0x00078384
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = base.isScheduled;
				if (isScheduled)
				{
					this.updateEvent();
				}
			}
		}

		// Token: 0x020003DD RID: 989
		internal class CustomStyleAccess : ICustomStyle
		{
			// Token: 0x06001FF6 RID: 8182 RVA: 0x0007A1AA File Offset: 0x000783AA
			public void SetContext(Dictionary<string, StylePropertyValue> customProperties, float dpiScaling)
			{
				this.m_CustomProperties = customProperties;
				this.m_DpiScaling = dpiScaling;
			}

			// Token: 0x06001FF7 RID: 8183 RVA: 0x0007A1BC File Offset: 0x000783BC
			public bool TryGetValue(CustomStyleProperty<float> property, out float value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out value);
					if (flag2)
					{
						return true;
					}
				}
				value = 0f;
				return false;
			}

			// Token: 0x06001FF8 RID: 8184 RVA: 0x0007A208 File Offset: 0x00078408
			public bool TryGetValue(CustomStyleProperty<int> property, out int value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.TryGetValue(property.name, StyleValueType.Float, out stylePropertyValue);
				if (flag)
				{
					float num;
					bool flag2 = stylePropertyValue.sheet.TryReadFloat(stylePropertyValue.handle, out num);
					if (flag2)
					{
						value = (int)num;
						return true;
					}
				}
				value = 0;
				return false;
			}

			// Token: 0x06001FF9 RID: 8185 RVA: 0x0007A258 File Offset: 0x00078458
			public bool TryGetValue(CustomStyleProperty<bool> property, out bool value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				bool flag2;
				if (flag)
				{
					value = stylePropertyValue.sheet.ReadKeyword(stylePropertyValue.handle) == StyleValueKeyword.True;
					flag2 = true;
				}
				else
				{
					value = false;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06001FFA RID: 8186 RVA: 0x0007A2AC File Offset: 0x000784AC
			public bool TryGetValue(CustomStyleProperty<Color> property, out Color value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					StyleValueHandle handle = stylePropertyValue.handle;
					StyleValueType valueType = handle.valueType;
					StyleValueType styleValueType = valueType;
					if (styleValueType != StyleValueType.Color)
					{
						if (styleValueType == StyleValueType.Enum)
						{
							string text = stylePropertyValue.sheet.ReadAsString(handle);
							return StyleSheetColor.TryGetColor(text.ToLowerInvariant(), out value);
						}
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(property.name, StyleValueType.Color, stylePropertyValue);
					}
					else
					{
						bool flag2 = stylePropertyValue.sheet.TryReadColor(stylePropertyValue.handle, out value);
						if (flag2)
						{
							return true;
						}
					}
				}
				value = Color.clear;
				return false;
			}

			// Token: 0x06001FFB RID: 8187 RVA: 0x0007A364 File Offset: 0x00078564
			public bool TryGetValue(CustomStyleProperty<Texture2D> property, out Texture2D value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.texture != null;
					if (flag2)
					{
						value = imageSource.texture;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x06001FFC RID: 8188 RVA: 0x0007A3DC File Offset: 0x000785DC
			public bool TryGetValue(CustomStyleProperty<Sprite> property, out Sprite value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.sprite != null;
					if (flag2)
					{
						value = imageSource.sprite;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x06001FFD RID: 8189 RVA: 0x0007A454 File Offset: 0x00078654
			public bool TryGetValue(CustomStyleProperty<VectorImage> property, out VectorImage value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					ImageSource imageSource = default(ImageSource);
					bool flag2 = StylePropertyReader.TryGetImageSourceFromValue(stylePropertyValue, this.m_DpiScaling, out imageSource) && imageSource.vectorImage != null;
					if (flag2)
					{
						value = imageSource.vectorImage;
						return true;
					}
				}
				value = null;
				return false;
			}

			// Token: 0x06001FFE RID: 8190 RVA: 0x0007A4CC File Offset: 0x000786CC
			public bool TryGetValue<T>(CustomStyleProperty<T> property, out T value) where T : Object
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				if (flag)
				{
					Object @object;
					bool flag2 = stylePropertyValue.sheet.TryReadAssetReference(stylePropertyValue.handle, out @object);
					if (flag2)
					{
						value = @object as T;
						return value != null;
					}
				}
				value = default(T);
				return false;
			}

			// Token: 0x06001FFF RID: 8191 RVA: 0x0007A54C File Offset: 0x0007874C
			public bool TryGetValue(CustomStyleProperty<string> property, out string value)
			{
				StylePropertyValue stylePropertyValue;
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(property.name, out stylePropertyValue);
				bool flag2;
				if (flag)
				{
					value = stylePropertyValue.sheet.ReadAsString(stylePropertyValue.handle);
					flag2 = true;
				}
				else
				{
					value = string.Empty;
					flag2 = false;
				}
				return flag2;
			}

			// Token: 0x06002000 RID: 8192 RVA: 0x0007A5A4 File Offset: 0x000787A4
			private bool TryGetValue(string propertyName, StyleValueType valueType, out StylePropertyValue customProp)
			{
				customProp = default(StylePropertyValue);
				bool flag = this.m_CustomProperties != null && this.m_CustomProperties.TryGetValue(propertyName, out customProp);
				bool flag3;
				if (flag)
				{
					StyleValueHandle handle = customProp.handle;
					bool flag2 = handle.valueType != valueType;
					if (flag2)
					{
						VisualElement.CustomStyleAccess.LogCustomPropertyWarning(propertyName, valueType, customProp);
						flag3 = false;
					}
					else
					{
						flag3 = true;
					}
				}
				else
				{
					flag3 = false;
				}
				return flag3;
			}

			// Token: 0x06002001 RID: 8193 RVA: 0x0007A60A File Offset: 0x0007880A
			private static void LogCustomPropertyWarning(string propertyName, StyleValueType valueType, StylePropertyValue customProp)
			{
				Debug.LogWarning(string.Format("Trying to read custom property {0} value as {1} while parsed type is {2}", propertyName, valueType, customProp.handle.valueType));
			}

			// Token: 0x04000F15 RID: 3861
			private Dictionary<string, StylePropertyValue> m_CustomProperties;

			// Token: 0x04000F16 RID: 3862
			private float m_DpiScaling;
		}

		// Token: 0x020003DE RID: 990
		internal class TypeData
		{
			// Token: 0x17000823 RID: 2083
			// (get) Token: 0x06002003 RID: 8195 RVA: 0x0007A635 File Offset: 0x00078835
			public Type type { get; }

			// Token: 0x06002004 RID: 8196 RVA: 0x0007A63D File Offset: 0x0007883D
			public TypeData(Type type)
			{
				this.type = type;
			}

			// Token: 0x17000824 RID: 2084
			// (get) Token: 0x06002005 RID: 8197 RVA: 0x0007A664 File Offset: 0x00078864
			public string fullTypeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
					if (flag)
					{
						this.m_FullTypeName = this.type.FullName;
					}
					return this.m_FullTypeName;
				}
			}

			// Token: 0x17000825 RID: 2085
			// (get) Token: 0x06002006 RID: 8198 RVA: 0x0007A69C File Offset: 0x0007889C
			public string typeName
			{
				get
				{
					bool flag = string.IsNullOrEmpty(this.m_TypeName);
					if (flag)
					{
						bool isGenericType = this.type.IsGenericType;
						this.m_TypeName = this.type.Name;
						bool flag2 = isGenericType;
						if (flag2)
						{
							int num = this.m_TypeName.IndexOf('`');
							bool flag3 = num >= 0;
							if (flag3)
							{
								this.m_TypeName = this.m_TypeName.Remove(num);
							}
						}
					}
					return this.m_TypeName;
				}
			}

			// Token: 0x04000F18 RID: 3864
			private string m_FullTypeName = string.Empty;

			// Token: 0x04000F19 RID: 3865
			private string m_TypeName = string.Empty;
		}
	}
}
