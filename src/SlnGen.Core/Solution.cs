using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlnGen.Core
{
    public class Solution
    {
        readonly string _solutionName;
        public string SolutionName => _solutionName;

        List<Project> _projects = new List<Project>();
        public IReadOnlyList<Project> Projects => _projects.AsReadOnly();

        readonly Guid _solutionGuid;
        public Guid SolutionGuid => _solutionGuid;

        public Solution(string solutionName) : this (solutionName, Guid.NewGuid()) { }

        public Solution(string solutionName, Guid solutionGuid)
        {
            _solutionName = solutionName;
            _solutionGuid = solutionGuid;
        }

        public Solution WithProject(Project project)
        {
            _projects.Add(project);
            return this;
        }
    }
}
