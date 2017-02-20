using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quine_McCluskey_Algorithm
{
    class PrimeImplicant
    {
        public readonly List<int> AffectedRows;
        public readonly List<LogicState> TruthTableRow;

        public PrimeImplicant(List<LogicState> truthTableRow)
        {
            this.TruthTableRow = truthTableRow;
            this.AffectedRows = getAffectedRowsFromTruthTableRow(truthTableRow);
        }

        private List<int> getAffectedRowsFromTruthTableRow(List<LogicState> truthTableRow)
        {
            List<int> affectedRows = getPossibleRowsForTruthTable(truthTableRow.Count);
            for (int i = 0; i < affectedRows.Count; i++)
            {
                if (!andConjunction(intToRow(affectedRows[i], truthTableRow.Count), truthTableRow))
                {
                    affectedRows.RemoveAt(i);
                    i--;
                }
            }
            return affectedRows;
        }

        private List<int> getPossibleRowsForTruthTable(int columns)
        {
            List<int> rows = new List<int>();
            for (int i = 0; i < Math.Pow(2, columns); i++)
            {
                rows.Add(i);
            }
            return rows;
        }

        private List<LogicState> intToRow(int x, int columns)
        {
            List<LogicState> row = new List<LogicState>(columns);
            for (int i = columns - 1; i >= 0; i--)
            {
                row.Add(((x & (1 << i)) == 0) ? LogicState.False : LogicState.True);
            }
            return row;
        }

        private bool andConjunction(List<LogicState> a, List<LogicState> b)
        {
            if (a.Count != b.Count)
            {
                throw new ArgumentException();
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == b[i] || a[i] == LogicState.DontCare || b[i] == LogicState.DontCare)
                {
                    continue;
                }

                return false;
            }
            return true;
        }
    }
}
