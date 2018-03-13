namespace SlnGen.Core.References
{
    public static class Nuget
    {
        public static readonly NugetPackage NewtonsoftJson_net452 = new NugetPackage("Newtonsoft.Json", "10.0.1", "net452")
        {
            Assemblies =
            {
                new NugetAssembly("Newtonsoft.Json",
                    "Newtonsoft.Json, Version=10.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed, processorArchitecture=MSIL",
                    @"..\..\packages\Newtonsoft.Json.10.0.1\lib\net45\Newtonsoft.Json.dll", true)
            }
        };
    }
}
