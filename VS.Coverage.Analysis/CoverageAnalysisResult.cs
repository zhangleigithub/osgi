using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VS.Coverage.Analysis
{
    public class CoverageAnalysisResult
    {
        public string ModuleName { get; set; }

        public string NamespaceName { get; set; }

        public string ClassName { get; set; }

        public uint LinesCovered { get; set; }

        public uint LinesNotCovered { get; set; }
    }
}
