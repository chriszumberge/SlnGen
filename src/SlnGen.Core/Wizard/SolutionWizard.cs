using SlnGen.Core.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core.Wizard
{
    public sealed partial class SolutionWizard
    {
        public string SolutionName { get; }

        readonly string _solutionPath;

        List<Project> _projects = new List<Project>();

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
