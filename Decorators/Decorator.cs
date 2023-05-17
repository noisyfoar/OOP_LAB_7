using OOP_LAB_4.figures;

namespace OOP_LAB_4.Decorators
{
    public abstract class Decorator : Shape
    {
        public Shape decoratedShape;
        public Decorator() : base()
        {
            
        }
        protected void getInfoFromShape()
        {
            shapeSize = decoratedShape.getSize();
            p0 = decoratedShape.getPoint();
        }

        public Decorator(Shape new_shape) : base(new_shape)
        {
            decoratedShape = new_shape;
            getInfoFromShape();
        }

        public override void move(Size imageSize, int dx, int dy)
        {
            decoratedShape.move(imageSize, dx, dy);
            getInfoFromShape();
        }
        public override void resize(Size imageSize, int x1, int y1, int x2, int y2)
        {
            decoratedShape.resize(imageSize, x1, y1, x2, y2);
            getInfoFromShape();
        }

        public override void resize(Size imageSize, int delta)
        {
            decoratedShape.resize(imageSize, delta);
            getInfoFromShape();
        }
        public override void setColor(Color new_color)
        {
            decoratedShape.setColor(new_color);
        }

        public override bool inShape(int x, int y)
        {
            return decoratedShape.inShape(x, y);
        }
    }
}
