#include "LineDivision.h"

LineDivision::LineDivision(Point3D start, Point3D end, int division)
{
    this->start = start;
    this->end = end;

    // divide the line into division parts
    // and store the points in the vector
    for (int i = 0; i <= division; i++)
    {
        Point3D point = start + (end - start) * (i / (double)division);
        this->points.push_back(point);
    }
}

LineDivision::~LineDivision()
{
}

Point3D LineDivision::getStart()
{
    return this->start;
}

Point3D LineDivision::getEnd()
{
    return this->end;
}

std::ostream& operator<<(std::ostream& os, const LineDivision& lineDivision)
{
    os << "LineDivision: " << lineDivision.start << " -> " << lineDivision.end << std::endl;
    os << "Points: " << std::endl;
    for (int i = 0; i < lineDivision.points.size(); i++)
    {
        os << lineDivision.points[i] << std::endl;
    }
    return os;
}

void LineDivision::example()
{
    std::cout << "LineDivision::example()" << std::endl;
    
    Point3D start(2, 3, 4);
    Point3D end(3, 4, 5);
    
    LineDivision lineDivision(start, end, 5);
    
    std::cout << lineDivision << std::endl;
}
