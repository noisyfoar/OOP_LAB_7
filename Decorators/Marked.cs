﻿using OOP_LAB_4.figures;

namespace OOP_LAB_4.Decorators
{
    public class marked : Decorator
    {
        public marked() : base()
        {
            name = CONST_SHAPE.Marked;
        }
        public marked(Shape new_shape) : base(new_shape)
        {
            name = CONST_SHAPE.Marked;
        }

        public override void Draw(Graphics g)
        {
            decoratedShape.Draw(g);
            Rectangle rect = new Rectangle(p0, shapeSize);
            Pen pen = new Pen(Color.Red);
            g.DrawRectangle(pen, rect);
        }

    }
}
