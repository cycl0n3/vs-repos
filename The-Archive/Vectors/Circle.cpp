#include "Circle.h"

#define _USE_MATH_DEFINES

#include <iostream>
#include <cmath>
#include <math.h>

Circle::Circle(const Point3D& center)
    : center(center), radius(1.0)
{
}

Circle::Circle(const Point3D& center, double radius)
    : center(center), radius(radius)
{
}

double Circle::getArea()
{
    return M_PI * pow(radius, 2);
}

double Circle::getRadius()
{
    return radius;
}

std::ostream& operator<<(std::ostream& os, const Circle& circle)
{
    os << "Circle: center = " << circle.center << ", radius = " << circle.radius;
    return os;
}

std::vector<Point3D> Circle::getPointsOnCircumference(int numPoints)
{
    std::vector<Point3D> points;
    
    double angle = 0.0;
    double angleIncrement = 2 * M_PI / numPoints;
    
    for (int i = 0; i < numPoints; i++)
    {
        double x = center.getX() + radius * cos(angle);
        double y = center.getY() + radius * sin(angle);
        double z = center.getZ();

        if (fabs(x) < EPSILON)
        {
            x = 0.0;
        }

        if (fabs(y) < EPSILON)
        {
            y = 0.0;
        }

        Point3D point(x, y, z);
        
        points.push_back(point);
        angle += angleIncrement;
    }

    return points;
}

void Circle::example()
{
    std::cout << "Circle::example()" << std::endl;
    
    Point3D center(0.0, 0.0, 0.0);
    Circle circle(center, 1.0);
    
    std::cout << circle << std::endl;
    std::cout << "Area = " << circle.getArea() << std::endl;
    std::vector<Point3D> points = circle.getPointsOnCircumference(17);
    
    for (int i = 0; i < points.size(); i++)
    {
        std::cout << points[i] << std::endl;
    }
}
