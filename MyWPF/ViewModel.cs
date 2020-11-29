using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GiulioMVVM;


namespace MyWPF
{
    class ViewModel : ViewModelBase
    {
        private string testResult;

        public string TestResult
        {
            get { return testResult; }
            set {
                testResult = value;
                OnPropertyChanged(() => TestResult);
            }
        }

        public ViewModel()
        {
            TestResult = "Test OK!";
        }

        DelegateCommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand is null)
                {
                    clickCommand = new DelegateCommand(_ => runClick());
                }
                return clickCommand;
            }
        }

        int times = 0;
        private void runClick()
        {

            TestResult = $"clicked {++times} times";
        }
    }
}
