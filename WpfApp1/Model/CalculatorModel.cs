using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    internal class CalculatorModel : ICalculatorModel
    {
        public string Calculate((List<decimal>, List<string>) tokens)
        {
            CalculateOperand(ref tokens.Item1, ref tokens.Item2, "*", (a, b) => a * b, "/", (a, b) => a / b);
            CalculateOperand(ref tokens.Item1, ref tokens.Item2, "+", (a, b) => a + b, "-", (a, b) => a - b);
            return tokens.Item1[0].ToString();
        }

        public bool CanAddNumber(string str, char new_number)
        {
            return CanBeNumber(str + new_number);
        }

        public bool CanBeNumber(string str)
        {
            try {
                Convert.ToDecimal(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public (List<decimal>, List<string>) TokenizeEqulation(string equlation)
        {
            var tokens = equlation.Split(' ').ToList();
            tokens.RemoveAt(0);

            List<decimal> numbers = new List<decimal>();
            List<string> operands = new List<string>();

            if (tokens.Count == 0)
                return (numbers, operands);

            foreach (string it in tokens)
            {
                if (!(it.Length <= 1 && !char.IsDigit(it[0])))
                {
                    numbers.Add(Convert.ToDecimal(it));
                }
                else
                {
                    operands.Add(it[0].ToString());
                }
            }

            return (numbers, operands);
        }

        private void CalculateOperand(
            ref List<decimal> numbers,
            ref List<string> operands,
            string operand0,
            Func<decimal, decimal, decimal> operand_operation0,
            string operand1,
            Func<decimal, decimal, decimal> operand_operation1
        )
        {
            decimal set_numbers;
            for (int i = 0; i < operands.Count; i++)
            {
                if (operands[i] == operand0)
                {
                    set_numbers = operand_operation0(numbers[i], numbers[i + 1]);
                    operands.RemoveAt(i);
                    numbers.RemoveAt(i);
                    numbers.RemoveAt(i);
                    numbers.Insert(i, set_numbers);
                    i--;
                }
                else if (operands[i] == operand1)
                {
                    set_numbers = operand_operation1(numbers[i], numbers[i + 1]);
                    numbers.RemoveAt(i);
                    numbers.RemoveAt(i);
                    numbers.Insert(i, set_numbers);
                    operands.RemoveAt(i);
                    i--;
                }
            }
        }

    }
}
