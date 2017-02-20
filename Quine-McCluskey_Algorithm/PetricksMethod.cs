using System;
using System.Collections.Generic;

namespace Quine_McCluskey_Algorithm
{
    static class PetricksMethod
    {
        public static List<List<LogicState>> RemoveNonEssentialPrimeImplicants(List<List<LogicState>> minifiedTruthTable)
        {
            if(minifiedTruthTable.Count == 0)
            {
                return minifiedTruthTable;
            }

            List<List<LogicState>> transposedTruthTable = transpose(minifiedTruthTable);

            List<PrimeImplicant> primeImplicantChart = new List<PrimeImplicant>();
            for (int i = 0; i < minifiedTruthTable.Count; i++)
            {
                primeImplicantChart.Add(new PrimeImplicant(minifiedTruthTable[i]));
            }

            List<List<PrimeImplicant>> chartEquationAndConnected = getChartEquation((int)Math.Pow(2, minifiedTruthTable[0].Count), primeImplicantChart);

            List<PrimeImplicant> requiredPrimeImplicants = getRequiredPrimeImplicants(chartEquationAndConnected);

            List<List<LogicState>> result = new List<List<LogicState>>();
            for (int i = 0; i < requiredPrimeImplicants.Count; i++)
            {
                result.Add(requiredPrimeImplicants[i].TruthTableRow);
            }
            return result;
        }

        // returns an equation like (K+L)(K+M)(L+N)(M+P)(N+Q)(P+Q)
        private static List<List<PrimeImplicant>> getChartEquation(int rows, List<PrimeImplicant> primeImplicantChart)
        {
            List<List<PrimeImplicant>> result = new List<List<PrimeImplicant>>();
            for (int i = 0; i < rows; i++)
            {
                result.Add(getPrimeImplicantsHandlingRow(i, primeImplicantChart));
            }
            return result;
        }

        private static List<PrimeImplicant> getPrimeImplicantsHandlingRow(int row, List<PrimeImplicant> primeImplicantChart)
        {
            List<PrimeImplicant> result = new List<PrimeImplicant>();
            for (int i = 0; i < primeImplicantChart.Count; i++)
            {
                if (primeImplicantChart[i].AffectedRows.Contains(row))
                {
                    result.Add(primeImplicantChart[i]);
                }
            }
            return result;
        }

        private static List<PrimeImplicant> getRequiredPrimeImplicants(List<List<PrimeImplicant>> andConnectedEquation)
        {
            throw new NotImplementedException();
        }

        private static List<List<LogicState>> transpose(List<List<LogicState>> table)
        {
            List<List<LogicState>> transposed = new List<List<LogicState>>();

            if (table.Count == 0)
            {
                return table;
            }
            
            for (int i = 0; i < table[0].Count; i++)
            {
                transposed.Add(new List<LogicState>());
                for (int j = 0; j < table.Count; j++)
                {
                    transposed[i].Add(table[j][i]);
                }
            }

            return transposed;
        }
    }
}
