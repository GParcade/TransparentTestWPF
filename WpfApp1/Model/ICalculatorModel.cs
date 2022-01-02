using System.Collections.Generic;

namespace WpfApp1
{
    public interface ICalculatorModel
    {
        bool CanAddNumber(string str,char new_number);
        bool CanBeNumber(string str);
        (List<decimal>, List<string>) TokenizeEqulation(string equlation);
        string Calculate((List<decimal>, List<string>) tokens);
    }
}
