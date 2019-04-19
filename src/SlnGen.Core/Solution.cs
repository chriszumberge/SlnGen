using SlnGen.Core.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SlnGen.Core
{
    public class Solution
    {
        // TODO, either subclasses or factory (pass in enum version you want)
        const string DEFAULT_VISUAL_STUDIO_SOLUTION_HEADER = "Microsoft Visual Studio Solution File, Format Version 12.00";
        const string DEFAULT_VISUAL_STUDIO = "# Visual Studio 14";
        const string DEFAULT_VISUAL_STUDIO_VERSION = "VisualStudioVersion = 14.0.25420.1";
        const string DEFAULT_VISUAL_STUDIO_MINIMUM_VERSION = "MinimumVisualStudioVersion = 10.0.40219.1";

        /// <summary>
        /// Gets the name of the solution.
        /// </summary>
        /// <value>
        /// The name of the solution.
        /// </value>
        public string SolutionName { get; }

        /// <summary>
        /// Gets the projects contained in the solution.
        /// </summary>
        /// <value>
        /// The projects in the solution.
        /// </value>
        public IReadOnlyList<Project> Projects => _projects.AsReadOnly();
        List<Project> _projects = new List<Project>();

        /// <summary>
        /// Gets the solution unique identifier.
        /// </summary>
        /// <value>
        /// The solution unique identifier.
        /// </value>
        public Guid SolutionGuid { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        public Solution(string solutionName) : this (solutionName, Guid.NewGuid()) { }

        protected virtual string SolutionHeaderLine => DEFAULT_VISUAL_STUDIO_SOLUTION_HEADER;
        protected virtual string VisualStudioHeaderLine => DEFAULT_VISUAL_STUDIO;
        protected virtual string VisualStudioVersionLine => DEFAULT_VISUAL_STUDIO_VERSION;
        protected virtual string VisualStudioMinimumVersionLine => DEFAULT_VISUAL_STUDIO_MINIMUM_VERSION;

        /// <summary>
        /// Initializes a new instance of the <see cref="Solution"/> class.
        /// </summary>
        /// <param name="solutionName">Name of the solution.</param>
        /// <param name="solutionGuid">The solution unique identifier.</param>
        public Solution(string solutionName, Guid solutionGuid)
        {
            SolutionName = solutionName;
            SolutionGuid = solutionGuid;
        }

        /// <summary>
        /// Adds the given project to the solution.
        /// </summary>
        /// <param name="project">The project to add.</param>
        /// <returns>The solution.</returns>
        public Solution WithProject(Project project)
        {
            _projects.Add(project);
            return this;
        }

        public string GenerateSolutionFiles(string solutionPath)
        {
            // TODO eventually not force it, but then we have to worry about the relative paths for the csproj references and I don't want
            // to deal with that right now. Forcing this project structure... if the user cares enough to change it they probably know enough
            // to be able to
            bool createSolutionDirectory = true;

            string slnDirectoryPath = Path.Combine(solutionPath, SolutionName);
            DirectoryInfo slnDirectory = Directory.CreateDirectory(slnDirectoryPath);

            string packagesDirectoryPath = Path.Combine(slnDirectoryPath, "packages");
            Directory.CreateDirectory(packagesDirectoryPath);

            string projDirectoryPath = createSolutionDirectory ? Path.Combine(slnDirectoryPath, SolutionName) : slnDirectoryPath;

            Dictionary<Project, string> projectWithCsProjFilePath = new Dictionary<Project, string>();
            foreach (Project csproj in Projects)
            {
                string csProjFilePath = csproj.GenerateProjectFilesForSolution(projDirectoryPath, SolutionGuid);
                projectWithCsProjFilePath.Add(csproj, csProjFilePath);
            }

            StringBuilder slnTextBuilder = new StringBuilder();
            slnTextBuilder.AppendLine();
            slnTextBuilder.AppendLine(SolutionHeaderLine);
            slnTextBuilder.AppendLine(VisualStudioHeaderLine);
            slnTextBuilder.AppendLine(VisualStudioVersionLine);
            slnTextBuilder.AppendLine(VisualStudioMinimumVersionLine);

            foreach (KeyValuePair<Project, string> projectAndCsProjFilePath in projectWithCsProjFilePath)
            {
                Project project = projectAndCsProjFilePath.Key;
                string relativeDirectory = projectAndCsProjFilePath.Value.Replace(String.Concat(@"\", projDirectoryPath), String.Empty);

                slnTextBuilder.AppendLine($"Project(\"{{{SolutionGuid.ToString()}}}\") = \"{project.AssemblyName}\", \"{relativeDirectory}\", \"{{{project.AssemblyGuid.ToString()}}}");
                slnTextBuilder.AppendLine("EndProject");
            }

            slnTextBuilder.AppendLine("Global");
            slnTextBuilder.AppendLine("\tGlobalSection(SolutionConfigurationPlatforms) = preSolution");
            foreach (var supportedConfig in GetSupportedBuildConfigurations())
            {
                slnTextBuilder.AppendLine($"\t\t{supportedConfig.Configuration}|{supportedConfig.Platform} = {supportedConfig.Configuration}|{supportedConfig.Platform}");
            }
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("\tGlobalSection(ProjectConfigurationPlatforms) = postSolution");
            foreach (Project csproj in Projects)
            {
                foreach (var supportedConfig in csproj.SupportedBuildConfigurations)
                {
                    slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.{supportedConfig.Configuration}|{supportedConfig.Platform}.ActiveCfg = {supportedConfig.Configuration}|{supportedConfig.Platform}");
                    if (supportedConfig.Build)
                    {
                        slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.{supportedConfig.Configuration}|{supportedConfig.Platform}.Build.0 = {supportedConfig.Configuration}|{supportedConfig.Platform}");
                    }
                    if (supportedConfig.Deploy)
                    {
                        slnTextBuilder.AppendLine($"\t\t{{{csproj.AssemblyGuid.ToString()}}}.{supportedConfig.Configuration}|{supportedConfig.Platform}.Deploy.0 = {supportedConfig.Configuration}|{supportedConfig.Platform}");
                    }
                }
            }
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("\tGlobalSection(SolutionProperties) = preSolution");
            slnTextBuilder.AppendLine("\t\tHideSolutionNode = FALSE");
            slnTextBuilder.AppendLine("\tEndGlobalSection");
            slnTextBuilder.AppendLine("EndGlobal");

            File.WriteAllText(Path.Combine(slnDirectoryPath, String.Concat(SolutionName, ".sln")), slnTextBuilder.ToString());

            return slnDirectoryPath;
        }

        private List<SupportedBuildConfiguration> GetSupportedBuildConfigurations()
        {
            return Projects.SelectMany(x => x.SupportedBuildConfigurations)
                .DistinctBy(x => $"{x.Configuration}|{x.Platform}").ToList();
        }
    }
}
