using System;
using System.Collections.Generic;
using System.Windows;

namespace Quine_McCluskey_Algorithm
{
    class Control
    {
        MainWindow mainWindow;

        public Control(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void ProcessTruthTableString(string truthTable)
        {
            try
            {
                TruthTable newTruthTable = stringToTruthTable(truthTable);
                List<List<LogicState>> minimized = QuineMcCluskeyAlgorithm.MinimizeTruthTable(newTruthTable);
                newTruthTable.SetInputStates(minimized);
                newTruthTable.SetOutputStatesToTrue();

                mainWindow.SetOutputLabelText(newTruthTable.ToString());
                mainWindow.SetOutputEquationLabelText(BooleanAlgebra.TruthTableToEquation(newTruthTable));
            }
            catch (Exception e)
            {
                MessageBox.Show("An error occured while processing the truth table. \n\n" + e.Message);
            }
        }

        public static TruthTable stringToTruthTable(string input)
        {
            string[] lines = input.Replace("\r", "").Replace("  ", " ").Replace(" \n", "\n").Split('\n');
            string[][] cells = new string[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                cells[i] = lines[i].Split(' ');
            }

            string[] titles = new string[cells[0].Length];
            List<List<LogicState>> inputStates = new List<List<LogicState>>();
            List<LogicState> outputStates = new List<LogicState>();;

            for (int i = 0; i < cells.Length; i++)
            {
                if (i > 0)
                {
                    inputStates.Add(new List<LogicState>());
                }

                for (int j = 0; j < cells[i].Length; j++)
                {
                    if (i == 0)
                    {
                        titles[j] = cells[i][j];
                    }
                    else
                    {
                        if (j < cells[i].Length - 1)
                        {
                            inputStates[i - 1].Add(StringToLogicState(cells[i][j]));
                        }
                        else
                        {
                            outputStates.Add(StringToLogicState(cells[i][j]));
                        }
                    }
                }
            }

            return new TruthTable(titles, inputStates, outputStates);
        }

        public static LogicState StringToLogicState(string input)
        {
            switch (input)
            {
                case "0":
                    return LogicState.False;
                case "1":
                    return LogicState.True;
                case "X":
                    return LogicState.DontCare;
                default:
                    throw new ArgumentException("The truth table is damaged.");
            }
        }

        public static string LogicStateToString(LogicState state)
        {
            switch (state)
            {
                case LogicState.False:
                    return "0";
                case LogicState.True:
                    return "1";
                case LogicState.DontCare:
                    return "X";
                default:
                    throw new Exception("An unknown error occured.");
            }
        }
    }

    static class Extensions
    {
        public static List<T> Clone<T>(this List<T> list)
        {
            List<T> clone = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                clone.Add(list[i]);
            }

            return clone;
        }

    }
}
