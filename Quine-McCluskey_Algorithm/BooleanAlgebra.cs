using System.Collections.Generic;
using System.Text;

namespace Quine_McCluskey_Algorithm
{
    static class BooleanAlgebra
    {
        public static string And = "*", Or = "+", Not = "~";

        public static string TruthTableToEquation(TruthTable table)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < table.InputStates.Count; i++)
            {
                string rowEquation = TruthTableRowToEquation(table.Titles, table.InputStates[i]);
                sb.Append(rowEquation);
                if (i < table.InputStates.Count - 1 && rowEquation.Length > 0)
                {
                    sb.Append(" " + Or + " ");
                }
            }

            if (sb.Length == 0 && table.InputStates.Count == 0)
                sb.Append("0");

            return table.Titles[table.Titles.Length - 1] + " = " + sb.ToString();
        }

        public static string TruthTableRowToEquation(string[] titles, List<LogicState> row)
        {
            StringBuilder sb = new StringBuilder();
            bool alreadyAddedSomething = false;
            for (int i = 0; i < row.Count; i++)
            {
                string toAppend = "";
                switch (row[i])
                {
                    case LogicState.False:
                        toAppend = Not + titles[i];
                        break;
                    case LogicState.True:
                        toAppend = titles[i];
                        break;
                    case LogicState.DontCare:
                        break;
                    default:
                        break;
                }

                if (toAppend.Length > 0)
                {
                    if (alreadyAddedSomething)
                        sb.Append(" " + And + " ");
                    alreadyAddedSomething = true;
                    sb.Append(toAppend);
                }
            }

            if (sb.Length == 0)
            {
                sb.Append("1");
            }

            return sb.ToString();
        }
    }
    

    enum LogicState : byte
    { 
        False = 0,
        True = 1,
        DontCare = 2
    }
}
