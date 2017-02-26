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

        // takes an equation like (K+L)(K+M)(L+N)(M+P)(N+Q)(P+Q)
        // expands the components to KKLMNP+KKLMNQ+... and returns the shortest
        private static List<PrimeImplicant> getRequiredPrimeImplicants(List<List<PrimeImplicant>> andConnectedEquation)
        {
            List<List<PrimeImplicant>> expanded = expand(andConnectedEquation);
            if (expanded.Count == 0)
            {
                return null;
            }
            List<PrimeImplicant> shortest = expanded[0];
            foreach (List<PrimeImplicant> term in expanded)
            {
                // remove duplicates
                for (int i = 0; i < term.Count; i++)
                {
                    while (term.LastIndexOf(term[i]) != i)
                    {
                        term.RemoveAt(term.LastIndexOf(term[i]));
                    }
                }

                if (shortest.Count > term.Count)
                {
                    shortest = term;
                }
            }
            return shortest;
        }

        private static List<List<PrimeImplicant>> expand(List<List<PrimeImplicant>> b)
        {
            List<List<PrimeImplicant>> result = new List<List<PrimeImplicant>>();
            if(b.Count <= 1)
            {
                for (int i = 0; i < b[0].Count; i++)
                {
                    result.Add(new List<PrimeImplicant>() { b[0][i] });
                }
            }
            else
            {
                List<PrimeImplicant> head = b[0];
                List<List<PrimeImplicant>> body = b;
                body.Remove(head);

                List<List<PrimeImplicant>> bodyExpanded = expand(body.Clone());

                if (head.Count == 0)
                {
                    return bodyExpanded;
                }

                for (int i = 0; i < head.Count; i++)
                {
                    if (bodyExpanded.Count == 0)
                    {
                        List<PrimeImplicant> term = new List<PrimeImplicant>();
                        term.Add(head[i]);
                        result.Add(term);
                    }
                    else
                    {
                        for (int j = 0; j < bodyExpanded.Count; j++)
                        {
                            List<PrimeImplicant> term = new List<PrimeImplicant>();
                            term.Add(head[i]);
                            term.AddRange(bodyExpanded[j]);
                            result.Add(term);
                        }
                    }
                }
            }
            return result;
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
