using System;
using System.Collections.Generic;

namespace Quine_McCluskey_Algorithm
{
    static class QuineMcCluskeyAlgorithm
    {
        public static List<List<LogicState>> MinimizeTruthTable(TruthTable input)
        {
            if (input.OutputStates.Count != input.InputStates.Count)
            {
                throw new ArgumentException("Number of output and input states is not equal.");
            }
            else
            {

                List<List<LogicState>> trueRows = getRowsWithTrueOutput(input);

                bool minimized = false;
                while (!minimized && trueRows.Count > 0)
                {
                    List<List<List<LogicState>>> sortedTrueRows = sortByNumberOfTruesOccuring(trueRows);
                    List<List<bool>> handled = new List<List<bool>>();

                    for (int i = 0; i < sortedTrueRows.Count; i++)
                    {
                        handled.Add(new List<bool>());
                        for (int j = 0; j < sortedTrueRows[i].Count; j++)
                        {
                            // initialize with false
                            handled[i].Add(false);
                        }
                    }

                    List<List<LogicState>> newTrueRows = new List<List<LogicState>>();

                    // go through each block of rows (every block has the same amount of "trues")
                    for (int i = 0; i < sortedTrueRows.Count - 1; i++)
                    {
                        // iterate through single block
                        for (int j = 0; j < sortedTrueRows[i].Count; j++)
                        {
                            // rows in the next block
                            for (int k = 0; k < sortedTrueRows[i + 1].Count; k++)
                            {
                                // only one field is different
                                if (numberOfDifferentFields(sortedTrueRows[i][j], sortedTrueRows[i + 1][k]) == 1)
                                {
                                    handled[i][j] = true;
                                    handled[i + 1][k] = true;

                                    newTrueRows.Add(sortedTrueRows[i][j].Clone());
                                    newTrueRows[newTrueRows.Count - 1][indexOfOnlyDifferentField(sortedTrueRows[i][j], sortedTrueRows[i + 1][k])] = LogicState.DontCare;
                                }
                            }
                        }
                    }

                    bool allNotHandled = true;

                    for (int i = 0; i < handled.Count; i++)
                    {
                        for (int j = 0; j < handled[i].Count; j++)
                        {
                            if (!handled[i][j])
                            {
                                newTrueRows.Add(sortedTrueRows[i][j].Clone());
                            }
                            else
                            {
                                allNotHandled = false;
                            }
                        }
                    }

                    if (allNotHandled)
                    {
                        minimized = true;
                    }

                    trueRows = newTrueRows;
                }

                removeDoubles(trueRows);
                return PetricksMethod.RemoveNonEssentialPrimeImplicants(trueRows);
            }
        }

        private static int numberOfDifferentFields(List<LogicState> row1, List<LogicState> row2)
        {
            int count = 0;
            for (int i = 0; i < row1.Count; i++)
            {
                if (row1[i] != row2[i])
                {
                    count++;
                }
            }
            return count;
        }

        private static int indexOfOnlyDifferentField(List<LogicState> row1, List<LogicState> row2)
        {
            for (int i = 0; i < row1.Count; i++)
            {
                if (row1[i] != row2[i])
                {
                    return i;
                }
            }

            throw new InvalidOperationException("No different field has been found.");
        }

        private static List<List<LogicState>> getRowsWithTrueOutput(TruthTable input)
        {
            /* picks the rows with output equal to true
             * e.g.: 
             * 000|0
             * 001|1
             * 010|1
             * would return
             * 001|1
             * 010|1
             */


            List<List<LogicState>> output = new List<List<LogicState>>();

            for (int i = 0; i < input.OutputStates.Count; i++)
            {
                if (input.OutputStates[i] == LogicState.True)
                {
                    output.Add(input.InputStates[i].Clone());
                }
            }

            return output;
        }

        private static List<List<List<LogicState>>> sortByNumberOfTruesOccuring(List<List<LogicState>> input)
        {
            List<List<List<LogicState>>> output = new List<List<List<LogicState>>>();
            for (int i = 0; i <= input[0].Count; i++)
            {
                output.Add(new List<List<LogicState>>());
            }

            for (int i = 0; i < input.Count; i++)
            {
                output[numberOfTruesOccuring(input[i])].Add(input[i].Clone());
            }

            return output;
        }

        private static int numberOfTruesOccuring(List<LogicState> inputRow)
        {
            int count = 0;
            for (int i = 0; i < inputRow.Count; i++)
            {
                if (inputRow[i] == LogicState.True)
                {
                    count++;
                }
            }

            return count;
        }

        private static void removeDoubles(List<List<LogicState>> input)
        {
            for (int i = 0; i < input.Count; i++)
            {
                // compare row with all following
                for (int j = i + 1; j < input.Count; j++)
                {
                    bool equal = true;
                    // compare every segment of both rows
                    for (int k = 0; k < input[i].Count; k++)
                    {
                        if (input[i][k] != input[j][k])
                        {
                            equal = false;
                        }
                    }

                    if (equal)
                    {
                        input.RemoveAt(j);
                        j--;
                    }
                }
            }
        }
    }
}
