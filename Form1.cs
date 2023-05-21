using OOP_LAB_4.Decorators;
using OOP_LAB_4.factory;
using OOP_LAB_4.figures;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace OOP_LAB_4
{

    public partial class Form1 : Form
    {
        List<Shape> shapes;

        Shape createdShape;

        int CursorX, CursorY;

        Color color;

        bool create;

        Graphics g;


        CONST_SHAPE selectedShape;

        ShapeFactory shapeFactory;

        public Form1()
        {
            shapes = new();
            shapeFactory = new MyShapeFactory();
            InitializeComponent();


            panel1.MouseUp += Form1_MouseUp;
            panel1.MouseDown += Form1_MouseDown;
            panel1.MouseMove+= Form1_MouseMove;


            color = Color.Black;
            pictureBox_color.Refresh();

            selectedShape = CONST_SHAPE.Circle;


            CursorX = 0;
            CursorY = 0;


            g = panel1.CreateGraphics();
            DoubleBuffered = true;
        }
        public void createShape(CONST_SHAPE choosenShape)
        {
            createdShape = shapeFactory.create(choosenShape);
            createdShape = new Marked(createdShape);
            createdShape.setColor(color);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            CursorX = e.X; CursorY = e.Y;
            create = true;
            for(int i = 0; i < shapes.Count; ++i)
            {
                if (shapes[i].inShape(CursorX, CursorY))
                {
                    create = false;
                    if (Form.ModifierKeys != Keys.Control)
                    {
                        for (int j = 0; j < shapes.Count; ++j)
                        {
                            shapes[j] = new UnMarked(shapes[j]);
                        }
                    }

                    if (shapes[i] is not Marked)
                    {
                        shapes[i] = new Marked(shapes[i]);
                    }
                }
            }
            if(create)
            {
                if (Form.ModifierKeys != Keys.Control)
                {
                    for (int j = 0; j < shapes.Count; ++j)
                    {
                        shapes[j] = new UnMarked(shapes[j]);
                    }
                }
                createShape(selectedShape);
                shapes.Add(createdShape);
            }
            panel1.Invalidate();
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (create) 
            { 
                createdShape.resize(panel1.Size, CursorX, CursorY, e.X, e.Y);
                panel1.Invalidate();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if(create)
            {
                create = false;
            }
            Console.WriteLine(panel1.Size);
            panel1.Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int dx = 0, dy = 0, delta = 0;
            switch (e.KeyCode)
            {
                case Keys.W:
                    dy = -10;break;
                case Keys.S:
                    dy = 10;break;
                case Keys.D:
                    dx = 10;break;
                case Keys.A:
                    dx = -10;break;
                case Keys.G:
                    delta = 10;break;
                case Keys.L:
                    delta = -10;break;
                case Keys.Delete:
                    shapes.Clear();
                    break;
            }
            if(dx != 0 || dy != 0)
            {
                foreach (Shape temp in shapes)
                {
                    if(temp is Marked)
                        temp.move(panel1.Size, dx, dy);
                }
                dx = 0; dy = 0;
            }
            if(delta != 0)
            {
                foreach (Shape temp in shapes)
                {
                    if(temp is Marked)
                        temp.resize(panel1.Size, delta);
                }
                delta = 0;
            }

            panel1.Invalidate();
        }



        private void button_group_Click(object sender, EventArgs e)
        {
            List<Shape> to_keep = new List<Shape>();

            createShape(CONST_SHAPE.Group);

            for(int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i] is Marked)
                {
                    ((CGroup)((Decorator)createdShape).getShape()).add(panel1.Size,shapes[i]);
                }
                else
                {
                    shapes[i] = new UnMarked(shapes[i]);
                    to_keep.Add(shapes[i]);
                }
            }
            shapes.Clear();

            for (int i = 0; i < to_keep.Count; i++)
            {
                shapes.Add(to_keep[i]);
            }
            shapes.Add(createdShape);
            to_keep.Clear();
            g.Clear(SystemColors.GrayText);
            panel1.Invalidate();
        }
        private void button_ungroup_Click(object sender, EventArgs e)
        {
            List<Shape> to_remove = new List<Shape>();
            for(int i = 0; i < shapes.Count; i++)
            {
                if (shapes[i].getName() == CONST_SHAPE.Group)
                {
                    to_remove.Add(shapes[i]);
                    for (int j = 0; j < ((CGroup)((Decorator)shapes[i]).getShape()).shapes.Count; ++j)
                    {
                        Shape temp = new Marked(((CGroup)((Decorator)shapes[i]).getShape()).shapes[j]);
                        shapes.Add(temp);
                    }
                    ((CGroup)((Decorator)shapes[i]).getShape()).shapes.Clear();
                }
            }
            foreach(Shape shape in to_remove)
            {
                shapes.Remove(shape);
            }
        }




        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color = colorDialog1.Color;
            pictureBox_color.Refresh();
        }
        private void button_setColor_Click(object sender, EventArgs e)
        {
            foreach (Shape temp in shapes)
            {
                if(temp is Marked)
                {
                    temp.setColor(color);
                }
            }
            panel1.Invalidate();
        }
        private void pictureBox_color_Paint(object sender, PaintEventArgs e)
        {
            pictureBox_color.BackColor = color;
        }
        private void toolStripTextBoxTriangle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Triangle;
        }
        private void toolStripTextBoxCircle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Circle;
        }
        private void toolStripTextBoxRectangle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Rectangle;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(SystemColors.GrayText);

            foreach (Shape c in shapes)
            {
                c.Draw(g);
            }
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            g = panel1.CreateGraphics();
            foreach (Shape shape in shapes)
            {
                shape.move(panel1.Size, 0,0);
                shape.resize(panel1.Size, 0);
            }

            panel1.Invalidate();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


    }
   
}