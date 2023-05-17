using OOP_LAB_4.figures;

namespace OOP_LAB_4.Decorators
{
    public class UnMarked : Decorator
    {
        public UnMarked() : base()
        {
            name = CONST_SHAPE.UnMarked;
        }
        public UnMarked(Shape new_shape) : base(new_shape)
        {
            name = CONST_SHAPE.UnMarked;
        }
    }
}
