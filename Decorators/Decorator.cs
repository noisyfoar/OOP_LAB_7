using OOP_LAB_4.factory;
using OOP_LAB_4.figures;

namespace OOP_LAB_4.Decorators
{
    public abstract class Decorator : Shape
    {
        protected Shape decoratedShape;

        public Decorator(Shape new_shape) : base(new_shape)
        {
            if (new_shape is Decorator)
            {
                decoratedShape = ((Decorator)new_shape).decoratedShape;
            }
            else
            {

                decoratedShape = new_shape;
            }
            getInfoFromShape();
        }

        public Shape getShape()
        {
            return decoratedShape;
        }

        public override void load(StreamReader reader, ShapeFactory factory)
        {
            decoratedShape.load(reader, factory);
        }

        public override void save(StreamWriter writer)
        {
            decoratedShape.save(writer);
        }

        protected void getInfoFromShape()
        {
            shapeSize = decoratedShape.getSize();
            p0 = decoratedShape.getPoint();
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

        public override CONST_SHAPE getName()
        {
            return decoratedShape.getName();
        }

        public override bool inShape(int x, int y)
        {
            return decoratedShape.inShape(x, y);
        }
    }
}
