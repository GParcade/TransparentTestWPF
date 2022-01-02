using System;
using System.ComponentModel;
using System.Windows.Input;

namespace WpfApp1
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool dot_entered;
        public bool DotAviable
        {
            get => !dot_entered;
            set
            {
                dot_entered = !value;
                OnPropertyChanged("DotAviable");
            }
        }
        private string calcAlgo = "";
        private string calcValue = "";




        public string CalcAlgo
        {
            get => calcAlgo;
            set
            {
                calcAlgo = value;
                OnPropertyChanged("CalcAlgo");
            }
        }
        public string CalcValue
        {
            get => calcValue;
            set
            {
                calcValue = value;
                OnPropertyChanged("CalcValue");
            }
        }

        private readonly ICalculatorModel CurrentModel;

        public CalculatorViewModel(ICalculatorModel model)
        {
            CurrentModel = model;
            DotAviable = true;
        }

        private ICommand numberOperation;
        public ICommand NumberOperation => numberOperation ?? (numberOperation = new DelegateCommand<string>(OpeationWithNumber));

        private ICommand resultRequest;
        public ICommand ResultRequest => resultRequest ?? (resultRequest = new DelegateCommand(ResultCalculate));

        private void OpeationWithNumber(string value)
        {
            switch (value)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    if (!CurrentModel.CanBeNumber(CalcValue))
                    {
                        CalcValue = string.Empty;
                    }
                    if (CurrentModel.CanAddNumber(CalcValue, value[0]))
                    {
                        CalcValue += value;
                    }
                    break;
                case "/":
                case "*":
                case "-":
                case "+":
                    if (CalcValue.Length > 0)
                    {
                        CalcAlgo += $" {CalcValue} {value}";
                    }
                    else if (CalcAlgo.Length > 0)
                    {
                        CalcAlgo = CalcAlgo.Remove(CalcAlgo.Length - 1) + value;
                    }
                    CalcValue = string.Empty;
                    break;
                case "C":
                    CalcValue = CalcAlgo = string.Empty;
                    break;
                case "CE":
                    CalcValue = string.Empty;
                    DotAviable = true;
                    break;
                case "<":
                    if (CalcValue.Length > 0)
                    {
                        if (CalcValue[CalcValue.Length - 1] == ',')
                        {
                            DotAviable = true;
                        }
                        CalcValue = CalcValue.Remove(CalcValue.Length - 1, 1);
                    }
                    else if(CalcAlgo.Length > 0)
                    {
                        CalcAlgo = CalcAlgo.Remove(CalcAlgo.Length - 2, 2);
                        int pos = CalcAlgo.LastIndexOf(' ');
                        if(pos == -1)
                        {
                            CalcValue = CalcAlgo;
                            CalcAlgo = string.Empty;
                        }
                        else
                        {
                            CalcValue = CalcAlgo.Substring(pos + 1, CalcAlgo.Length - pos - 1);
                            CalcAlgo = CalcAlgo.Remove(pos, CalcAlgo.Length - pos);
                        }
                    }
                    break;
                case ",":
                    if (!dot_entered)
                    {
                        CalcValue += ',';
                        DotAviable = false;
                    }
                    break;
                default:
                    break;
            }
        }
        private void ResultCalculate()
        {
            if (!CurrentModel.CanBeNumber(CalcValue))
            {
                return;
            }

            CalcAlgo += $" {CalcValue}";

            try
            {
                CalcValue = CurrentModel.Calculate(CurrentModel.TokenizeEqulation(CalcAlgo));
            }
            catch (Exception ex)
            {
                CalcValue = ex.ToString();
            }
            CalcAlgo = string.Empty;
        }
    }
}
