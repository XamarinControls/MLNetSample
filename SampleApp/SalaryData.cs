using Microsoft.ML.Runtime.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    public class SalaryData
    {
        [Column("0")]
        public float YearsExperience;

        [Column("1", name: "Label")]
        public float Salary;
    }
}
