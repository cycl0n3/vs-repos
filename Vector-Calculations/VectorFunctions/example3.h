#pragma once

#include <iostream>
#include <vector>
#include <algorithm>
#include <numeric>
#include <functional>
#include <tuple>
#include <iomanip>
#include <fstream>

void runExample3()
{
    auto dot = [](auto x, auto y) -> double {
        return std::inner_product(x.begin(), x.end(), y.begin(), 0.0);
    };

    auto cross = [](auto x, auto y) {
        return std::vector<double>{
            x[1] * y[2] - x[2] * y[1],
            x[2] * y[0] - x[0] * y[2],
            x[0] * y[1] - x[1] * y[0]
        };
    };

    // curve vector = C
    auto curve = [](double t) {
        return std::vector<double>{
            std::cos(t),
            std::sin(t),
            t
        };
    };

    // unit velocity vector = T
    auto velocity = [](double t) {
        return std::vector<double>{
            -std::sin(t),
            std::cos(t),
            1.0
        };
    };

    // normal vector = N = T' / |T'|
    auto normal = [](double t) {
        return std::vector<double>{
            -std::cos(t),
            -std::sin(t),
            0.0
        };
    };

    // binormal vector = B = T x N
    auto binormal = [](double t) {
        return std::vector<double>{
            0.0,
            0.0,
            1.0
        };
    };

    // curvature = |T'| / |r'|
    auto curvature = [](double t) {
        return 1.0;
    };
}