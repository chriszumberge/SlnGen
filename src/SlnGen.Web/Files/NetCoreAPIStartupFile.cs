using SlnGen.Core;
using SlnGen.Core.Code;
using System.Collections.Generic;

namespace ZESoft.SlnGen.Web.Files
{
    public class NetCoreAPIStartupFile : ProjectFile
    {
        static string s_fileName = "Startup.cs";
        string _rootNamespace;
        public NetCoreAPIStartupFile(string rootNamespace) : base(s_fileName)
        {
            _rootNamespace = rootNamespace;
        }

        List<SGAssemblyReference> _assemblyReferences = new List<SGAssemblyReference>()
        {
            new SGAssemblyReference("Microsoft.AspNetCore.Builder"),
            new SGAssemblyReference("Microsoft.AspNetCore.Hosting"),
            new SGAssemblyReference("Microsoft.AspNetCore.Mvc"),
            new SGAssemblyReference("Microsoft.Extensions.Configuration"),
            new SGAssemblyReference("Microsoft.Extensions.DependencyInjection")
        };

        public NetCoreAPIStartupFile WithAssembly(SGAssemblyReference assemblyReference)
        {
            if (!_assemblyReferences.Contains(assemblyReference))
            {
                _assemblyReferences.Add(assemblyReference);
            }
            return this;
        }

        List<string> _serviceConfigurationCode = new List<string>();
        public NetCoreAPIStartupFile WithServiceConfigurations(params string[] codeLines)
        {
            _serviceConfigurationCode.AddRange(codeLines);
            return this;
        }

        public NetCoreAPIStartupFile Build()
        {
            List<string> serviceConfigurationLines = new List<string>
            {
                "services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);"
            };
            serviceConfigurationLines.AddRange(_serviceConfigurationCode);

            List<SGMethod> methods = new List<SGMethod>
            {
                new SGMethod(new SGMethodSignature("ConfigureServices", SGAccessibilityLevel.Public)
                {
                    Arguments = { new SGArgument("IServiceCollection", "services") }
                })
                {
                    Comments = { "This method gets called by the runtime. Use this method to add services to the container." },
                    Lines = serviceConfigurationLines
                },
                new SGMethod(new SGMethodSignature("Configure", SGAccessibilityLevel.Public)
                {
                    Arguments =
                    {
                        new SGArgument("IApplicationBuilder", "app"),
                        new SGArgument("IHostingEnvironment", "env")
                    }
                })
                {
                    Comments = { "This method gets called by the runtime. Use this method to configure the HTTP request pipeline."  },
                    Lines =
                    {
                        "if (env.IsDevelopment())",
                        "{",
                        "\tapp.UseDeveloperExceptionPage();",
                        "}",
                        "else",
                        "{",
                        "\tapp.UseHsts();",
                        "}",
                        "",
                        "app.UseHttpsRedirection();",
                        "app.UseMvc();"
                    }
                }
            };

            FileContents = new SGFile(s_fileName)
            {
                AssemblyReferences = _assemblyReferences,
                Namespaces =
                {
                    new SGNamespace(_rootNamespace)
                    {
                        Classes =
                        {
                            new SGClass("Startup", SGAccessibilityLevel.Public)
                            {
                                Properties =
                                {
                                    new SGClassProperty("Configuration", "IConfiguration", SGAccessibilityLevel.Public, isReadonly: true)
                                },
                                Constructors =
                                {
                                    new SGClassConstructor("Startup", SGAccessibilityLevel.Public)
                                    {
                                        Arguments = { new SGArgument("IConfiguration", "configuration")},
                                        Lines = { "Configuration = configuration;" }
                                    }
                                },
                                Methods = methods
                            }
                        }
                    }
                }
            }.ToString();

            return this;
        }
    }
}
