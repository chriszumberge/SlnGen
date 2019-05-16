using SlnGen.Core.Utils;

namespace SlnGen.Core.References
{
    public static class Nuget
    {
        public static readonly NugetPackage System_Json__4_5_0 = new NugetPackage("System.Json", "4.5.0");
        public static readonly NugetPackage System_Numerics_Vectors__4_5_0 = new NugetPackage("System.Numerics.Vectors", "4.5.0");

        public static readonly NugetPackage Newtonsoft_Json__10_0_1 = new NugetPackage("Newtonsoft.Json", "10.0.1")
        {
            TargetFrameworks =
            {
                { NetFrameworkPlatform.v4_5_2, "net452" },
                { NetFrameworkPlatform.v4_6_1, "net461" }
            },
            Assemblies =
            {
                new NugetAssembly("Newtonsoft.Json",
                    "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true)
            }
        };
        public static readonly NugetPackage Newtonsoft_Json__11_0_2 = new NugetPackage("Newtonsoft.Json", "11.0.2")
        {
            TargetFrameworks =
            {
                { NetFrameworkPlatform.v4_5_2, "net452" },
                { NetFrameworkPlatform.v4_6_1, "net461" }
            },
            Assemblies =
            {
                new NugetAssembly("Newtonsoft.Json",
                    "Newtonsoft.Json, Version=11.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", false)
            }
        };
        public static readonly NugetPackage Newtonsoft_Json__12_0_1 = new NugetPackage("Newtonsoft.Json", "12.0.1");
    }
}
