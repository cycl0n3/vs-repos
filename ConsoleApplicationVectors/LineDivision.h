#pragma once

#include "Point3D.h"

#include <iostream>
#include <vector>

class LineDivision
{
public:
    LineDivision(Point3D start, Point3D end, int division);
    ~LineDivision();
    
    Point3D getStart();
    Point3D getEnd();
    
    friend std::ostream& operator<<(std::ostream& os, const LineDivision& lineDivision);

    static void example();
private:
    Point3D start;
    Point3D end;

    std::vector<Point3D> points;
};
