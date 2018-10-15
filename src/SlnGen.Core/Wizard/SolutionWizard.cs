using System.Collections.Generic;

namespace SlnGen.Core.Wizard
{
    public sealed partial class SolutionWizard
    {
        public string SolutionName { get; }

        readonly string _solutionPath;

        List<Project> _projects = new List<Project>();

        // TODO pass in a target framework version at the solution level, then all added projects use the
        // NetImplementationSupport util to get compatible framework for project type?
        public SolutionWizard(string solutionName, string solutionPath)
        {
            SolutionName = solutionName;
            _solutionPath = solutionPath;
        }

        public SolutionWizard WithProject(Project project)
        {
            _projects.Add(project);
            return this;
        }

        public string Build()
        {
            Solution sln = new Solution(SolutionName);

            _projects.ForEach(p => sln.WithProject(p));

            return sln.GenerateSolutionFiles(_solutionPath);
        }
    }
}
