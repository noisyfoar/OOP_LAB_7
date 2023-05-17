using OOP_LAB_4.factory;

namespace OOP_LAB_4.figures
{
    public class CGroup : Shape
    {
        protected List<Shape> shapes;

        public CGroup() : base()
        {
            shapes = new List<Shape>();
            name = CONST_SHAPE.Group;
        }

        public CGroup(Shape group) : base(group) 
        {
            shapes = new List<Shape>();
            if(group.getName() == CONST_SHAPE.Group)
            {
                add(((CGroup)group).shapes);
            }
            name = CONST_SHAPE.Group;
        }
        
        
        public virtual void add(List<Shape> new_shapes)
        {
            foreach(Shape shape in new_shapes)
            {
                if (!shapes.Contains(shape))
                {
                    shapes.Add(shape);
                }
            }
        }
        public void new_size(Size imageSize)
        {
            Point temp_point = new(imageSize.Width, imageSize.Height);
            Size temp_size = new(0,0);


            foreach (Shape shape in shapes)
            {
                Size shapeSize = shape.getSize();
                Point p0 = shape.getPoint();

                if (temp_point.X > p0.X)
                {
                    temp_point.X = p0.X;
                }
                if (temp_point.Y > p0.Y)
                {
                    temp_point.Y = p0.Y;
                }
                if (temp_point.X + temp_size.Width < p0.X + shapeSize.Width)
                {
                    temp_size.Width = p0.X + shapeSize.Width - temp_point.X;
                }
                if (temp_point.Y + temp_size.Height < p0.Y + shapeSize.Height)
                {
                    temp_size.Height = p0.Y + shapeSize.Height - temp_point.Y;
                }
            }

            p0 = temp_point;
            shapeSize= temp_size;

            move(imageSize, 0, 0);
        }
        public override void setColor(Color new_color)
        {
            foreach(Shape shape in shapes)
            {
                shape.setColor(new_color);
            }
        }
        public override void Draw(Graphics g)
        {
            foreach (Shape shape in shapes)
            {
                shape.Draw(g);
            }
        }
        public override bool inShape(int x, int y)
        {
            foreach(Shape shape in shapes)
                if(shape.inShape(x, y))
                    return true;

            return false;
        }
        public override void resize(Size imageSize, int delta)
        {
            base.resize(imageSize, delta);
            foreach(Shape shape in shapes)
            {
                shape.resize(imageSize, delta);
            }
        }
        public void unGroup(List<Shape> new_shapes)
        {
            foreach (Shape shape in shapes)
            {
                if (!new_shapes.Contains(shape))
                {
                    new_shapes.Add(shape);
                }
            }
            shapes.Clear();
        }
    }
}
