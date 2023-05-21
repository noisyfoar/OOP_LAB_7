using OOP_LAB_4.factory;

namespace OOP_LAB_4.figures
{
    public abstract class Shape
    {
        protected CONST_SHAPE name { get; set; }
        protected Size shapeSize { get; set; } // size of box where will be shape
        protected Point p0 { get; set; } // topleft point
        protected Color color { get; set; }

        public Shape()
        {
            color = new Color();
            shapeSize = new Size();
            p0 = new Point();
        }
        public Shape(Shape shape) : this()
        {
            color = shape.getColor();
            shapeSize = shape.getSize();
            p0 = shape.getPoint();
        }
        public abstract void Draw(Graphics g);
        public virtual void save(StreamWriter writer)
        {
            writer.WriteLine((char)name);
            writer.WriteLine(p0.X);
            writer.WriteLine(p0.Y);
            writer.WriteLine(shapeSize.Width);
            writer.WriteLine(shapeSize.Height);
            writer.WriteLine(color.Name);
        }
        public virtual void load(StreamReader reader, ShapeFactory factory)
        {
            p0 = new(Convert.ToInt32(reader.ReadLine()), Convert.ToInt32(reader.ReadLine()));
            shapeSize = new Size(Convert.ToInt32(reader.ReadLine()), Convert.ToInt32(reader.ReadLine()));
            color = Color.FromName(reader.ReadLine());
        }
        public virtual void resize(Size imageSize, int delta)
        {
            Size newSize = new(shapeSize.Width + delta, shapeSize.Height + delta);

            int min_size = 60;

            if (newSize.Width < min_size)
            {
                newSize.Width = min_size;
            }
            if(newSize.Width + p0.X > imageSize.Width)
            {
                newSize.Width = imageSize.Width - p0.X;
            }
            if(newSize.Height < min_size)
            {
                newSize.Height = min_size;
            }
            if (newSize.Height + p0.Y > imageSize.Height)
            {
                newSize.Height = imageSize.Height - p0.Y;
            }
            shapeSize = newSize;
            move(imageSize, 0, 0);
        }

        public virtual void resize(Size imageSize, int x1, int y1 , int x2, int y2)
        {
            shapeSize = new Size();
            p0 = new Point();
            Size new_size = new Size();
            Point new_p0 = new Point();

            if (x1 > x2)
            {
                new_p0.X = x2;
                new_size.Width = x1 - x2;
            }
            else
            {
                new_p0.X = x1;
                new_size.Width = x2 - x1;
            }
            if (y1 > y2)
            {
                new_p0.Y = y2;
                new_size.Height = y1 - y2;
            }
            else
            {
                new_p0.Y = y1;
                new_size.Height = y2 - y1;
            }

            shapeSize = new_size;
            p0 = new_p0;
            resize(imageSize, 0);
        }

        public virtual void move(Size imageSize, int dx, int dy)
        {
            Point new_p0 = new Point(p0.X + dx, p0.Y + dy);

            if( new_p0.X + shapeSize.Width > imageSize.Width)
            {
                new_p0.X = imageSize.Width - shapeSize.Width;
            }
            if(new_p0.X < 0)
            {
                new_p0.X = 0;
            }
            if(new_p0.Y + shapeSize.Height > imageSize.Height)
            {
                new_p0.Y = imageSize.Height - shapeSize.Height;
            }
            if(new_p0.Y < 0)
            {
                new_p0.Y = 0;
            }
            p0 = new_p0;
        }

        public virtual void setColor(Color new_color)
        {
            this.color = new_color;
        }

        public virtual Color getColor()
        {
            return color;
        }

        public virtual CONST_SHAPE getName()
        {
            return name;
        }

        public Point getPoint()
        {
            return p0;
        }
        public Size getSize()
        {
            return shapeSize;
        }
        public abstract bool inShape(int x, int y);
    }
    public class Help_vector
    {
        public int dx, dy;
        public Help_vector(Point a, Point b)
        {
            this.dx = a.X - b.X;
            this.dy = a.Y - b.Y;
        }
        public static int operator *(Help_vector a, Help_vector b)
        {
            return a.dx * b.dy - a.dy * b.dx;
        }
    }
}
