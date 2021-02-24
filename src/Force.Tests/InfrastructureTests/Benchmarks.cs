using System;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Xunit;

namespace Force.Tests.InfrastructureTests
{
    public class Benchmarks
    {
        [Fact]
        public void Process()
        {
            var wb = new WorkflowBenchmark();
            wb.Process();
        }
        
        [Fact]
        public void ProcessManually()
        {
            var wb = new WorkflowBenchmark();
            wb.ProcessManually();
        }
        
        [Fact]
        public async Task MediatR()
        {
            var wb = new WorkflowBenchmark();
            await wb.MediatR();
        }

        [Fact]
        public void MagicVsManual()
        {
            var summary = BenchmarkRunner.Run<WorkflowBenchmark>();

            var magicReport = GetReportByName(summary, nameof(WorkflowBenchmark.Process));
            var manualReport = GetReportByName(summary, nameof(WorkflowBenchmark.ProcessManually));
            var manualNoIoc = GetReportByName(summary, nameof(WorkflowBenchmark.ProcessNoIoc));
            var mediatR = GetReportByName(summary, nameof(WorkflowBenchmark.MediatR));

            Assert.True(false, 
                $"{magicReport.ResultStatistics.Mean} " +
                $"/ {manualReport.ResultStatistics.Mean} " +
                $"/ {manualNoIoc.ResultStatistics.Mean} " + 
                $"/ {mediatR.ResultStatistics.Mean}" );
            
            Assert.True(magicReport.ResultStatistics.Mean - manualReport.ResultStatistics.Mean < 2000, 
                $"Mean Magic: {magicReport.ResultStatistics.Mean}" + Environment.NewLine +
                $"Mean Manual: {manualReport.ResultStatistics.Mean}");
            
            Assert.True(magicReport.ResultStatistics.Mean - manualNoIoc.ResultStatistics.Mean < 2000, 
                $"Mean Magic: {magicReport.ResultStatistics.Mean}" + Environment.NewLine +
                $"Mean Manual: {manualNoIoc.ResultStatistics.Mean}");
        }

        private static void CheckSummary(BenchmarkReport magicReport)
        {
            if (magicReport?.ResultStatistics == null)
            {
                throw new InvalidOperationException();
            }
        }

        private static BenchmarkReport GetReportByName(Summary summary, string benchmarkName)
        {
            var report = summary
                .Reports
                .First(x => x.BenchmarkCase.Descriptor.DisplayInfo ==
                            nameof(WorkflowBenchmark) + "." + benchmarkName);
            
            if (report?.ResultStatistics == null)
            {
                throw new InvalidOperationException($"\"{benchmarkName}\" is not found");
            }

            return report;
        }

    }
}