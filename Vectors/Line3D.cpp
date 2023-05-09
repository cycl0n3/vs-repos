#include "Line3D.h"

#include <iostream>

#include "Point3D.h"
#include "Vector3D.h"

Line3D::Line3D()
{
}

Line3D::Line3D(const Point3D& p, const Vector3D& v) : p(p), v(v)
{
}

Line3D::Line3D(const Line3D& line) : p(line.p), v(line.v)
{
}

Line3D::~Line3D()
{
}

Line3D& Line3D::operator=(const Line3D& line)
{
    if (this != &line)
    {
        p = line.p;
        v = line.v;
    }
    return *this;
}

Point3D Line3D::getPoint(double t) const
{
    return Point3D(p.getX() + v.getX() * t, p.getY() + v.getY() * t, p.getZ() + v.getZ() * t);
}

// distance between this and other line
double Line3D::distance(const Line3D& line) const
{
    Vector3D w = Vector3D(p, line.p);
    Vector3D u = v.cross(line.v);

    return w.dot(u) / u.norm();
}

// output equation in parametric form
std::ostream& operator<<(std::ostream& out, const Line3D& line)
{
    out << "x = " << line.p.getX() << " + " << line.v.getX() << " * t" << std::endl;
    out << "y = " << line.p.getY() << " + " << line.v.getY() << " * t" << std::endl;
    out << "z = " << line.p.getZ() << " + " << line.v.getZ() << " * t" << std::endl;
    return out;
}

void Line3D::example()
{
    std::cout << "Line3D::example()" << std::endl << std::endl;

    Point3D p1(-1, 2, -1);
    Point3D p2(1, 2, 2);

    Vector3D v1(1, -1, -2);
    Vector3D v2(-2, 2, -2);

    Line3D line1(p1, v1);
    Line3D line2(p2, v2);

    std::cout << "line1: " << line1 << std::endl;
    std::cout << "line2: " << line2 << std::endl;

    // distance between line1 and line2
    std::cout << "distance between line1 and line2: " << line1.distance(line2) << std::endl;
    
    std::cout << std::endl;
}