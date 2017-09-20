using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product
{
    public interface IRequirement
    {
        void ReadRequirement(string element);
        void WriteRequirement(string fileName);
    }
}
