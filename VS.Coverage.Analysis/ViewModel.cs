using Microsoft.VisualStudio.Coverage.Analysis;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace VS.Coverage.Analysis
{
    public class ViewModel
    {
        /// <summary>
        /// 打开命令
        /// </summary>
        private DelegateCommand openCommand;

        /// <summary>
        /// 导出CSV
        /// </summary>
        private DelegateCommand exportToCSVCommand;

        /// <summary>
        /// 结果
        /// </summary>
        public ObservableCollection<CoverageAnalysisResult> CoverageAnalysisResults = new ObservableCollection<CoverageAnalysisResult>();

        /// <summary>
        /// 打开命令
        /// </summary>
        public ICommand OpenCommand { get { return openCommand; } }

        /// <summary>
        /// 打开命令
        /// </summary>
        public ICommand ExportToCSVCommand { get { return exportToCSVCommand; } }

        public ViewModel()
        {
            openCommand = new DelegateCommand();
            openCommand.OnExecuted = this.Open;

            exportToCSVCommand = new DelegateCommand();
            exportToCSVCommand.OnExecuted = this.ExportToCSV;
        }

        private void Open()
        {
            Microsoft.Win32.OpenFileDialog ofdl = new Microsoft.Win32.OpenFileDialog();
            ofdl.Filter = "123";
            if (ofdl.ShowDialog() == true)
            {

            }

            CoverageDS dataSet = new CoverageDS();
            dataSet.ImportXml(@"G:\合并的结果.coveragexml");

            CoverageAnalysisResults.Clear();

            foreach (var item in dataSet.Method)
            {
                CoverageAnalysisResult coverageAnalysisResult = new CoverageAnalysisResult();
                coverageAnalysisResult.ModuleName = item.ClassRow.NamespaceTableRow.ModuleName;
                coverageAnalysisResult.NamespaceName = item.ClassRow.NamespaceTableRow.NamespaceName;
                coverageAnalysisResult.ClassName = item.ClassRow.ClassName;
                coverageAnalysisResult.LinesCovered = item.LinesCovered;
                coverageAnalysisResult.LinesNotCovered = item.LinesNotCovered;

                CoverageAnalysisResults.Add(coverageAnalysisResult);
            }
        }

        private void ExportToCSV()
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"G:\20170904.csv", false))
            {
                foreach (var item in CoverageAnalysisResults)
                {
                    sw.WriteLine("{0},{1},{2},{3},{4}", item.ModuleName, item.NamespaceName, item.ClassName, item.LinesCovered, item.LinesNotCovered);
                }
            }
        }
    }
}
