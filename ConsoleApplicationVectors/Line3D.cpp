#include "Line3D.h"

#include <iostream>

#include "Point3D.h"
#include "Vector3D.h"

Line3D::Line3D()
{
}

Line3D::Line3D(const Point3D& p1, const Point3D& p2)
{
    points[0] = p1;
    points[1] = p2;
}

Line3D::Line3D(const Line3D& line)
{
    points[0] = line.points[0];
    points[1] = line.points[1];
}

Line3D::~Line3D()
{
}

Line3D& Line3D::operator=(const Line3D& line)
{
    if (this != &line)
    {
        points[0] = line.points[0];
        points[1] = line.points[1];
    }
    return *this;
}

Point3D& Line3D::operator[](int index)
{
    return points[index];
}

const Point3D& Line3D::operator[](int index) const
{
    return points[index];
}

Point3D Line3D::getPoint(double t) const
{
    return Point3D(points[0].getX() + t * (points[1].getX() - points[0].getX()),
                          points[0].getY() + t * (points[1].getY() - points[0].getY()),
                          points[0].getZ() + t * (points[1].getZ() - points[0].getZ()));
}

double Line3D::getLength() const
{
    return points[0].distance(points[1]);
}

// distance between parallel lines
double Line3D::distance(const Line3D& line) const
{
    Vector3D v1(points[0], points[1]);
    Vector3D v2(line.points[0], line.points[1]);
    Vector3D v3(points[0], line.points[0]);
    return v1.crossProduct(v2).getLength() / v2.getLength();
}

// output equation in parametric form
std::ostream& operator<<(std::ostream& out, const Line3D& line)
{
    out << "x = " << line.points[0].getX() << " + t * " << line.points[1].getX() - line.points[0].getX() << std::endl;
    out << "y = " << line.points[0].getY() << " + t * " << line.points[1].getY() - line.points[0].getY() << std::endl;
    out << "z = " << line.points[0].getZ() << " + t * " << line.points[1].getZ() - line.points[0].getZ() << std::endl;
    return out;
}

void Line3D::example()
{
    std::cout << "Line3D::example()" << std::endl << std::endl;

    Point3D p1(1, 2, 3);
    Point3D p2(4, 5, 6);

    std::cout << "p1: " << p1 << std::endl;
    std::cout << "p2: " << p2 << std::endl;
    
    Line3D line(p1, p2);
    
    std::cout << "line: " << line << std::endl;
    std::cout << "line[0]: " << line[0] << std::endl;
    std::cout << "line[1]: " << line[1] << std::endl;
    std::cout << "line.getPoint(0.5): " << line.getPoint(0.5) << std::endl;
    std::cout << "line.getLength(): " << line.getLength() << std::endl;
    
    std::cout << std::endl;
}