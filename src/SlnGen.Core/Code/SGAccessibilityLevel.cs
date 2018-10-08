///
/// AccessibilityLevel.cs
/// 
/// Author:
///     Chris Zumberge <chriszumberge@gmail.com>
/// 
/// 02/06/2018
/// 
using SlnGen.Core.Utils;
using System;

namespace SlnGen.Core.Code
{
    public sealed class SGAccessibilityLevel : TypeSafeEnum
    {
        private SGAccessibilityLevel(int value, string name) : base(value, name) { }

        public static readonly SGAccessibilityLevel Public = new SGAccessibilityLevel(AccessibilityLevels.Public, nameof(AccessibilityLevels.Public).ToLower());
        public static readonly SGAccessibilityLevel Private = new SGAccessibilityLevel(AccessibilityLevels.Private, nameof(AccessibilityLevels.Private).ToLower());
        public static readonly SGAccessibilityLevel Protected = new SGAccessibilityLevel(AccessibilityLevels.Protected, nameof(AccessibilityLevels.Protected).ToLower());
        public static readonly SGAccessibilityLevel Internal = new SGAccessibilityLevel(AccessibilityLevels.Internal, nameof(AccessibilityLevels.Internal).ToLower());
        public static readonly SGAccessibilityLevel None = new SGAccessibilityLevel(AccessibilityLevels.None, String.Empty);

        public class AccessibilityLevels
        {
            public const int Public = 1;
            public const int Private = 2;
            public const int Protected = 3;
            public const int Internal = 4;
            public const int None = 5;
        }
    }
}
