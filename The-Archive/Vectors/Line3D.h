#pragma once

#include <iostream>

#include "Point3D.h"
#include "Vector3D.h"

class Line3D
{
public:
    Line3D();
    Line3D(const Point3D& p, const Vector3D& v);
    Line3D(const Line3D& line);
    ~Line3D();

    Line3D& operator=(const Line3D& line);
    Point3D getPoint(double t) const;

    // distance between parallel lines
    double distance(const Line3D& line) const;

    friend std::ostream& operator<<(std::ostream& out, const Line3D& line);

    static void example();
private:
    Point3D p;
    Vector3D v;
};
