using System;

namespace SlnGen.Core.Utils
{
    public static class ProjectTypeGuids
    {
        //https://www.codeproject.com/Reference/720512/List-of-Visual-Studio-Project-Type-GUIDs

        public static Guid AspNet_5 = new Guid("8BB2217D-0F2D-49D1-97BC-3654ED321F3B");
        public static Guid AspNet_MVC_3 = new Guid("E53F8FEA-EAE0-44A6-8774-FFD645390401");
        public static Guid AspNet_MVC_4 = new Guid("E3E379DF-F4C6-4180-9B81-6769533ABE47");
        public static Guid AspNet_MVC_5 = new Guid("349C5851-65DF-11DA-9384-00065B846F21");
        public static Guid Cs = new Guid("FAE04EC0-301F-11D3-BF4B-00C04F79EFBC");
        public static Guid Cpp = new Guid("8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942");
        public static Guid Database = new Guid("A9ACE9BB-CECE-4E62-9AA4-C7E7C5BD2124");
        public static Guid Fs = new Guid("F2A71F9B-5D33-465A-A702-920D77279786");
        public static Guid Mono_for_Android = new Guid("EFBA0AD7-5A72-4C68-AF49-83D382785DCF");
        public static Guid MonoTouch = new Guid("6BC8ED88-2882-458C-8E55-DFD12B67127B");
        public static Guid MonoTouch_Binding = new Guid("F5B4F3BC-B597-4E2B-B552-EF5D8A32436F");
        public static Guid Portable_Class_Library = new Guid("786C830F-07A1-408B-BD7F-6EE04809D6DB");
        public static Guid Solution_Folder = new Guid("66A26720-8FB5-11D2-AA7E-00C04F688DDE");
        public static Guid Web_Application = new Guid("349C5851-65DF-11DA-9384-00065B846F21");
        public static Guid Web_Site = new Guid("E24C65DC-7377-472B-9ABA-BC803B73C61A");
        public static Guid Xamarin_Android = Mono_for_Android;
        public static Guid Xamarin_iOS = MonoTouch;
    }
}
