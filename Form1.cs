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
        List<Shape> selectedShapes;

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
            selectedShapes = new();
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

            shapes.Add(createdShape);
            selectedShapes.Add(createdShape);
        }

        public void printList()
        {
            Console.WriteLine("- - - \n");
            foreach (Shape shape in shapes)
            {
                Console.WriteLine(shape.getName() + " " + shape.getPoint().ToString() + " " + shape.getSize().ToString() );
            }
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
                            if ( j != i )
                            {
                                selectedShapes.Remove(shapes[j]);
                            }
                            shapes[j] = new UnMarked(shapes[j]);
                        }
                    }

                    if (!selectedShapes.Contains(shapes[i]))
                    {
                        shapes[i] = new Marked(shapes[i]);
                        selectedShapes.Add(shapes[i]);
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
                    selectedShapes.Clear();
                }
                createShape(selectedShape);
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
                    selectedShapes.Clear();
                    break;
            }
            if(dx != 0 || dy != 0)
            {
                for (int j = 0; j < selectedShapes.Count; ++j)
                {
                    selectedShapes[j].move(panel1.Size, dx, dy);
                }
                dx = 0; dy = 0;
            }
            if(delta != 0)
            {
                for (int j = 0; j < selectedShapes.Count; ++j)
                {
                    selectedShapes[j].resize(panel1.Size, delta);
                }
                delta = 0;
            }

            panel1.Invalidate();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.Clear(SystemColors.Control);

            foreach (Shape c in shapes)
            {
                c.Draw(g);
            }
        }
        
        private void toolStripTextBoxCircle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Circle;
        }
        
        private void toolStripTextBoxRectangle_Click(object sender, EventArgs e)
        {
            selectedShape = CONST_SHAPE.Rectangle;
        }

        
        private void Form1_Resize(object sender, EventArgs e)
        {
            foreach(Shape shape in shapes)
            {
                shape.resize(panel1.Size, 0);    
            }
            
            panel1.Invalidate();
        }

        private void button_group_Click(object sender, EventArgs e)
        {

        }

        private void button_ungroup_Click(object sender, EventArgs e)
        {
            List<Shape> temp = new(); 
            int num = selectedShapes.Count;
            for(int i = 0; i < num; ++i)
            {
                if (selectedShapes[i].getName() == CONST_SHAPE.Group)
                {
                    temp.Add(selectedShapes[i]);
                    ((CGroup)((Decorator)selectedShapes[i]).getShape()).unGroup(selectedShapes);
                }
            }
            for(int i = 0; i < temp.Count; ++i)
            {
                if (selectedShapes.Contains(temp[i]))
                {
                    selectedShapes.Remove(temp[i]);
                }
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
            foreach (Shape shape in selectedShapes)
            {
                shape.setColor(color);
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
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();


    }
   
}