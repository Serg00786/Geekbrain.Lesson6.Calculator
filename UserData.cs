using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    public class UserData
    {
        public double FirstValue { get; set; }
        public string OperationSign { get; set; }
        public double SecondValue { get; set; }
        public UserData(string _FirstValue, string _Sign, string _SecondValues)
        {
            FirstValue = Convert.ToDouble(_FirstValue);
            OperationSign = _Sign;
            SecondValue = Convert.ToDouble(_SecondValues);
        }



    }
}
