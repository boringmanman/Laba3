using Lalalend_3.core.commands;
using System.Collections.Generic;
using System.Linq;

namespace Lalalend_3.src.core.commands.Gases_commands
{
    internal class GasesCommandFactory : AbstractCommandFactory
    {
        public override IChartCommand CreateFromCSV(string csv)
        {
            List<List<string>> splitedCsv = csv.Split('\n').ToList().Select((e) => e.Split(';').ToList()).ToList();
            return new GasesCommand(splitedCsv);
        }
    }
}
