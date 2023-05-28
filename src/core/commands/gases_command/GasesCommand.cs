using Lalalend_3.core;
using Lalalend_3.core.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lalalend_3.core.commands
{
    internal class GasesCommand : IChartCommand
    {
        List<List<string>> data;

        public GasesCommand(List<List<string>> data)
        {
            this.data = data;
        }

        public void Run(IChartPresenter presenter)
        {
            presenter.ShowGrid(new List<string>() { "Год", "Выбросы углекислого газа (млн. тонн)", "Выбросы метана (млн. тонн)", "Выбросы оксида азота (млн. тонн)", "Выбросы фторированных газов (млн. тонн)" }, data);

            Series carbonDioxide = new Series();
            Series methane = new Series();
            Series nitrogenOxide = new Series();
            Series fluorinatedGases = new Series();

            carbonDioxide.Name = "Углекислый газ";
            methane.Name = "Метан";
            nitrogenOxide.Name = "Оксид азота";
            fluorinatedGases.Name = "Фторированные газы";

            carbonDioxide.ChartType = SeriesChartType.FastLine;
            methane.ChartType = SeriesChartType.FastLine;
            nitrogenOxide.ChartType = SeriesChartType.FastLine;
            fluorinatedGases.ChartType = SeriesChartType.FastLine;

            float carbonDioxideSum = 0, methaneSum = 0, nitrogenOxideSum = 0, fluorinatedGasesSum = 0;

            foreach (var data_ in data)
            {
                carbonDioxide.Points.AddXY(data_[0], data_[1]);
                methane.Points.AddXY(data_[0], data_[2]);
                nitrogenOxide.Points.AddXY(data_[0], data_[3]);
                fluorinatedGases.Points.AddXY(data_[0], data_[4]);

                carbonDioxideSum += float.Parse(data_[1]);
                methaneSum += float.Parse(data_[2]);
                nitrogenOxideSum += float.Parse(data_[3]);
                fluorinatedGasesSum += float.Parse(data_[4]);
            }

            float[] totalEmissions = { carbonDioxideSum, methaneSum, nitrogenOxideSum, fluorinatedGasesSum };
            string answer = "";
            if (totalEmissions.Max() == carbonDioxideSum)
            {
                answer = "Углекислый газ (" + carbonDioxideSum + " млн. тонн)";
            }
            else if (totalEmissions.Max() == methaneSum)
            {
                answer = "Метан (" + methaneSum + " млн. тонн)";
            }
            else if (totalEmissions.Max() == nitrogenOxideSum)
            {
                answer = "Оксид азота (" + nitrogenOxideSum + " млн. тонн)";
            }
            else if (totalEmissions.Max() == fluorinatedGasesSum)
            {
                answer = "Фторированные газы (" + fluorinatedGasesSum + " млн. тонн)";
            }

            presenter.ShowChart(new List<Series> { carbonDioxide, methane, nitrogenOxide, fluorinatedGases });

            presenter.ShowAdditionalInfo("Наибольшие выбросы газов: " + answer);
        }
    }
}
