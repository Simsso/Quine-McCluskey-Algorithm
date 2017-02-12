using System.Collections.Generic;
using System.Text;

namespace Quine_McCluskey_Algorithm
{
    class TruthTable
    {
        public string[] Titles;
        public List<List<LogicState>> InputStates;
        public List<LogicState> OutputStates;

        public TruthTable(string[] titles, List<List<LogicState>> inputStates, List<LogicState> outputStates)
        {
            this.Titles = titles;
            this.InputStates = inputStates;
            this.OutputStates = outputStates;
        }

        public void SetInputStates(List<List<LogicState>> newStates)
        {
            this.InputStates = newStates;
        }

        public void SetOutputStates(List<LogicState> newStates)
        {
            this.OutputStates = newStates;
        }

        public void SetOutputStatesToTrue()
        {
            int count = InputStates.Count;
            this.OutputStates = new List<LogicState>();
            for (int i = 0; i < count; i++)
            {
                this.OutputStates.Add(LogicState.True);
            }
        }


        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            for (int i = 0; i < Titles.Length; i++)
            {
                output.Append(Titles[i] + ((i < Titles.Length - 1) ? " " : ""));
            }

            output.Append("\r\n");


            for (int i = 0; i < InputStates.Count; i++)
            {
                for (int j = 0; j < InputStates[i].Count; j++)
                {
                    output.Append(Control.LogicStateToString(InputStates[i][j]) + " ");
                }
                output.Append(Control.LogicStateToString(OutputStates[i]) + ((i < InputStates.Count - 1) ? "\r\n" : ""));
            }

            return output.ToString();
        }
    }
}
