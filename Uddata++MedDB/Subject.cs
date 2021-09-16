using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uddata__MedDB
{
    public enum Abbreviation { Eng, Prog, Mat, OOP }
    class Subject : Person
    {
        public Abbreviation fag { get; set; }
        public int teacherId { get; set; }
    }
}
