using Microsoft.VisualStudio.Coverage.Analysis;
using Microsoft.Win32;
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
        public ObservableCollection<CoverageAnalysisResult> CoverageAnalysisResults { get; private set; }

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

            this.CoverageAnalysisResults = new ObservableCollection<CoverageAnalysisResult>();
        }

        private void Open()
        {
            OpenFileDialog ofdl = new OpenFileDialog();
            ofdl.Filter = "coveragexml|*.coveragexml";
            ofdl.Multiselect = false;
            if ((bool)ofdl.ShowDialog())
            {
                CoverageDS dataSet = new CoverageDS();
                dataSet.ImportXml(ofdl.FileName);

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
        }

        private void ExportToCSV()
        {
            SaveFileDialog sfdl = new SaveFileDialog();
            sfdl.Filter = "csv|*.csv";
            sfdl.AddExtension = true;
            if ((bool)sfdl.ShowDialog())
            {
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(sfdl.FileName, false))
                {
                    foreach (var item in CoverageAnalysisResults)
                    {
                        sw.WriteLine("{0},{1},{2},{3},{4}", item.ModuleName, item.NamespaceName, item.ClassName, item.LinesCovered, item.LinesNotCovered);
                    }
                }
            }
        }
    }
}
