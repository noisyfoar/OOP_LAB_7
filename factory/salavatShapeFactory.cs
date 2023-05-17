using OOP_LAB_4.figures;

namespace OOP_LAB_4.factory
{
    
    public class salavatShapeFactory : shapeFactory
    {
        public override Shape create(CONST_SHAPE type)
        {
            switch (type)
            {
                case CONST_SHAPE.Circle:
                    return new CCircle();

                case CONST_SHAPE.Rectangle:
                    return new CRectangle();

                case CONST_SHAPE.Triangle:
                    return new CTriangle();

                case CONST_SHAPE.Group:
                    return new CGroup();

                default:
                    return new CCircle();
            }
        }
    }
}
