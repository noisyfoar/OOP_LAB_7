using OOP_LAB_4.figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB_4.factory
{
    public abstract class shapeFactory
    {
        public abstract Shape create(CONST_SHAPE type);
    }
}
