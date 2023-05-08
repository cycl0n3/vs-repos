#pragma once

#include <iostream>

#include "Point3D.h"

class Line3D
{
public:
    Line3D();
    Line3D(const Point3D& p1, const Point3D& p2);
    Line3D(const Line3D& line);
    ~Line3D();

    Line3D& operator=(const Line3D& line);
    Point3D& operator[](int index);
    const Point3D& operator[](int index) const;
    Point3D getPoint(double t) const;
    
    double getLength() const;

    // distance between parallel lines
    double distance(const Line3D& line) const;

    friend std::ostream& operator<<(std::ostream& out, const Line3D& line);

    static void example();
private:
    Point3D points[2];
};
