#pragma once

#include "Point3D.h"

#include <iostream>
#include <vector>

const double EPSILON = 1.0E-10;

class Circle
{
public:
    Circle(const Point3D& center);
    Circle(const Point3D& center, double radius);
    
    double getArea();
    double getRadius();

    std::vector<Point3D> getPointsOnCircumference(int numPoints);

    friend std::ostream& operator<<(std::ostream& os, const Circle& circle);

    static void example();
private:
    Point3D center;
    double radius;
};
