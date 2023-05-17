using OOP_LAB_4.factory;
using OOP_LAB_4.figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB_4.Decorators
{
    public abstract class Decorator : Shape
    {
        public Shape decoratedShape;
        public Decorator() : base()
        {

        }

        public Decorator(Shape new_shape) : base(new_shape)
        {
            decoratedShape = new_shape;
        }

        public override void move(Size imageSize, int dx, int dy)
        {
            base.move(imageSize, dx, dy);
            decoratedShape.move(imageSize, dx, dy);
        }
        public override void resize(Size imageSize, int x1, int y1, int x2, int y2)
        {
            base.resize(imageSize, x1, y1, x2, y2);
            decoratedShape.resize(imageSize, x1, y1, x2, y2);
        }

        public override void resize(Size imageSize, int delta)
        {
            base.resize(imageSize, delta);
            decoratedShape.resize(imageSize, delta);
        }
        public override void setColor(Color new_color)
        {
            decoratedShape.setColor(new_color);
        }
        public override void Draw(Graphics g)
        {
            decoratedShape.Draw(g);
        }

        public override bool inShape(int x, int y)
        {
            return decoratedShape.inShape(x, y);
        }
    }
}
